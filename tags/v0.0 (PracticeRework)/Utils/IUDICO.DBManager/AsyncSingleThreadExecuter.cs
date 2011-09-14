using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using LEX.CONTROLS;

namespace IUDICO.DBManager
{
    public interface IAsyncExecuterContext
    {
        IVariable<bool> EnableUserActions { get; }
        void AsyncOperationsCompleted();
        void AsyncOperationBegins(string title);
        void AsyncError(string error);
    }

    public class AsyncSingleThreadExecuter<TContext> : CriticalFinalizerObject, IDisposable
        where TContext : IAsyncExecuterContext
    {
        private struct TaskData
        {
            public TaskData(Func<TContext, object> action, string name, Action<TContext, object> notifier)
            {
                Action = action;
                Name = name;
                Notifier = notifier;
            }

            public readonly Func<TContext, object> Action;
            public readonly string Name;
            public readonly Action<TContext, object> Notifier;
        }

        public AsyncSingleThreadExecuter(TContext context)
        {
            _Context = context;
            _Thread = new Thread(ProcessAsyncOperations);
            _Thread.Start(context);
        }

        public void AsyncEnQueueOperation(Func<TContext, object> action, string title)
        {
            AsyncEnQueueOperation(action, title, null);
        }

        public void AsyncEnQueueOperation(Func<TContext, object> action, string title, Action<TContext, object> notifier)
        {
            lock (this)
            {
                _Context.EnableUserActions.Value = false;

                _OperationQueue.Enqueue(new TaskData(action, title, notifier));
                _OperationSemaphore.Release();
            }
        }

        private TaskData AsyncDeQueueOperation()
        {
            lock (this)
            {
                return _OperationQueue.Dequeue();
            }
        }

        private void ProcessAsyncOperations(object ownerForm)
        {
            var f = (TContext)ownerForm;
            var w = _OperationSemaphore;
            while (true)
            {
                w.WaitOne();

                Thread.BeginCriticalRegion();
                try
                {
                    if (_OperationQueue.Count > 0)
                    {
                        var op = AsyncDeQueueOperation();
                        try
                        {
                            f.AsyncOperationBegins(op.Name);
                            var res = op.Action(f);
                            if (op.Notifier != null)
                            {
                                op.Notifier(f, res);
                            }
                        }
                        finally
                        {
                            f.AsyncOperationsCompleted();
                        }
                    }
                }
                catch (SqlCommandException e)
                {
                    _OperationQueue.Clear();
                    f.AsyncError(e.Message);
                }
                catch (InvalidDBVersionException e)
                {
                    _OperationQueue.Clear();
                    f.AsyncError(string.Format("Invalid DB Version: {0}. Cannot run script!!!", e.Version));
                }
                catch (Exception e)
                {
                    _OperationQueue.Clear();
                    f.AsyncError(e.Message);
                }
                finally
                {
                    Thread.EndCriticalRegion();
                    OperationCompleted();
                }
            }
        }

        private void OperationCompleted()
        {
            lock (this)
            {
                if (_OperationQueue.Count == 0)
                {
                    _Context.AsyncOperationsCompleted();
                }
            }
        }

        private readonly Thread _Thread;
        private readonly Semaphore _OperationSemaphore = new Semaphore(0, int.MaxValue);
        private readonly Queue<TaskData> _OperationQueue = new Queue<TaskData>();
        private readonly IAsyncExecuterContext _Context;

        #region Implementation of IDisposable

        public void Dispose()
        {
            _Thread.Abort();
            GC.SuppressFinalize(_Thread);
        }

        #endregion
    }

}
