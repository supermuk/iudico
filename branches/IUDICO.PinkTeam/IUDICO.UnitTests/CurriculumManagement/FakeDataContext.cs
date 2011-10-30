//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data.Linq;
//using System.Reflection;

//namespace IUDICO.UnitTests.CurriculumManagement
//{
//    public class FakeDataContextWrapper : IDataContextWrapper
//    {
//        public DataContext Context
//        {
//            get { return null; }
//        }

//        private List<object> Added = new List<object>();
//        private List<object> Deleted = new List<object>();

//        private readonly IFakeDatabase mockDatabase;

//        public FakeDataContextWrapper(IFakeDatabase database)
//        {
//            mockDatabase = database;
//        }

//        protected List<T> InternalTable<T>() where T : class
//        {
//            return (List<T>)mockDatabase.Tables[typeof(T)];
//        }

//        #region IDataContextWrapper Members

//        public virtual IQueryable<T> Table<T>() where T : class
//        {
//            return mockDatabase.GetTable<T>();
//        }

//        public virtual ITable Table(Type type)
//        {
//            return new FakeTable(mockDatabase.Tables[type], type);
//        }

//        public virtual void DeleteAllOnSubmit<T>(IEnumerable<T> entities) where T : class
//        {
//            foreach (var entity in entities)
//            {
//                DeleteOnSubmit(entity);
//            }
//        }

//        public virtual void DeleteOnSubmit<T>(T entity) where T : class
//        {
//            this.Deleted.Add(entity);
//        }

//        public virtual void InsertAllOnSubmit<T>(IEnumerable<T> entities) where T : class
//        {
//            foreach (var entity in entities)
//            {
//                InsertOnSubmit(entity);
//            }
//        }

//        public virtual void InsertOnSubmit<T>(T entity) where T : class
//        {
//            this.Added.Add(entity);
//        }

//        public virtual void SubmitChanges()
//        {
//            this.SubmitChanges(ConflictMode.FailOnFirstConflict);
//        }

//        public virtual void SubmitChanges(ConflictMode failureMode)
//        {
//            try
//            {
//                foreach (object obj in this.Added)
//                {
//                    MethodInfo validator = obj.GetType().GetMethod("OnValidate", BindingFlags.Instance | BindingFlags.NonPublic);
//                    if (validator != null)
//                    {

//                        validator.Invoke(obj, new object[] { ChangeAction.Insert });
//                    }
//                    this.mockDatabase.Tables[obj.GetType()].Add(obj);
//                }

//                this.Added.Clear();

//                foreach (object obj in this.Deleted)
//                {
//                    MethodInfo validator = obj.GetType().GetMethod("OnValidate", BindingFlags.Instance | BindingFlags.NonPublic);
//                    if (validator != null)
//                    {
//                        validator.Invoke(obj, new object[] { ChangeAction.Delete });
//                    }
//                    this.mockDatabase.Tables[obj.GetType()].Remove(obj);
//                }

//                this.Deleted.Clear();

//                foreach (KeyValuePair<Type, IList> tablePair in this.mockDatabase.Tables)
//                {
//                    MethodInfo validator = tablePair.Key.GetMethod("OnValidate", BindingFlags.Instance | BindingFlags.NonPublic);
//                    if (validator != null)
//                    {
//                        foreach (object obj in tablePair.Value)
//                        {
//                            validator.Invoke(obj, new object[] { ChangeAction.Update });
//                        }
//                    }
//                }
//            }
//            catch (TargetInvocationException e)
//            {
//                throw e.InnerException;
//            }
//        }

//        public void Dispose() { }

//        #endregion
//    }
//}
