using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FireFly.CourseEditor.Course.Manifest;

namespace FireFly.CourseEditor.Common
{
    ///<summary>
    /// Represents typed read-only list
    ///</summary>
    ///<typeparam name="T">Type of element of list</typeparam>
    public interface IReadOnlyList<T> : IEnumerable<T>
        where T : class
    {
        ///<summary>
        /// Indexer
        ///</summary>
        ///<param name="index"></param>
        [NotNull]
        T this[int index] { get; set; }

        ///<summary>
        /// Count of elements in readonly collection
        ///</summary>
        int Count { get; }
    }

    ///<summary>
    /// Represents interface for errors container
    ///</summary>
    public interface IErrors : IReadOnlyList<Error>
    {
        ///<summary>
        /// Gets plain-text string representation summary of all errors contained by collection
        ///</summary>
        ///<returns>String representation of errors</returns>
        [NotNull]
        string GetErrorsSummary();

        ///<summary>
        /// Gets plain-text string representation summary of all errors contained by collection
        ///</summary>
        ///<returns>String representation of errors</returns>
        [NotNull]
        string GetErrorsSummary([CanBeNull] IValidateble host);
    }

    ///<summary>
    /// Defines error
    ///</summary>
    [DebuggerDisplay("[{Source}]: {Message}")]
    public class Error : IEquatable<Error>
    {
        ///<summary>
        /// Gets source of error
        ///</summary>
        [NotNull]
        public readonly IValidateble Source;

        ///<summary>
        /// Gets unique string identifier for this type of error (defined by sender)
        ///</summary>
        [NotNull]
        public readonly object ID;

        ///<summary>
        /// User message of error
        ///</summary>
        [NotNull]
        public readonly string Message;

        ///<summary>
        /// Creates new instance of <see cref="Error" />
        ///</summary>
        ///<param name="source">Source of error</param>
        ///<param name="id">Unique string identifier for this error type</param>
        ///<param name="message">User message for error</param>
        ///<exception cref="ArgumentNullException"></exception>
        [DebuggerStepThrough]
        public Error([NotNull]IValidateble source, [NotNull]object id, [NotNull]string message)
        {
#if CHECKERS
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
#endif
            ID = id;
            Message = message;
            Source = source;
        }

        public bool Equals(Error other)
        {
            return Source == other.Source && ID == other.ID;
        }

        public override bool Equals(object obj)
        {
            var r = obj as Error;
            return r != null ? Equals(r) : false;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }

    ///<summary>
    /// Represents standard error collection 
    ///</summary>
    [DebuggerVisualizer("{GetErrorsSummary()}")]
    public class ErrorsCollection : List<Error>, IErrors
    {
        [NotNull]
        public string GetErrorsSummary()
        {
            var notFirst = false;
            var result = new StringBuilder();
            foreach (var e in this)
            {
                if (notFirst)
                {
                    result.AppendLine();
                }
                else
                {
                    notFirst = true;
                }
                result.Append(e.Message);
            }
            return result.ToString();
        }

        [NotNull]
        public string GetErrorsSummary([CanBeNull]IValidateble hostSource)
        {
            Debug.Assert(Count > 0);
            var result = new StringBuilder();
            var notFirst = false;
            foreach (var e in this)
            {
                if (notFirst)
                {
                    result.AppendLine();
                }
                else
                {
                    notFirst = true;
                }
                if (e.Source != hostSource)
                {
                    result.Append(e.Source.Title);
                    result.Append(": ");
                }
                result.Append(e.Message);
            }
            return result.ToString();
        }
    }

    ///<summary>
    /// Represents types which object can be validateble
    ///</summary>
    public interface IValidateble : ITitled
    {
        ///<summary>
        /// Actual errors for control
        ///</summary>
        [NotNull]
        IErrors Errors { get; }

        ///<summary>
        /// Get information is the control valid
        ///</summary>
        bool IsValid { get; }

        /// <summary>
        /// Occurs when new error found 
        /// </summary>
        event Action<IValidateble, Error> ErrorFound;

        /// <summary>
        /// Occurs when some of error is fixed
        /// </summary>
        event Action<IValidateble, Error> ErrorFixed;

        /// <summary>
        /// Occurs before Validation process is started
        /// </summary>
        event Action<IValidateble> BeginValidate;

        /// <summary>
        /// Occurs when validation process is finished
        /// </summary>
        event Action<IValidateble> EndValidate;

        /// <summary>
        /// Force to validate control's state again
        /// </summary>
        void ReValidate();
    }

    /// <summary>
    /// Implements Event Arguments for validation purpose.
    /// </summary>
    public class ValidationEventArgs : EventArgs
    {
        #region Protected Fields

        protected bool isValid;

        #endregion

        #region Public Properties

        /// <summary>
        /// Receives list of validation fails messages.
        /// </summary>
        public List<string> Messages
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets boolean value representing if validation was succesfull('true') or failed('false').
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.isValid && (this.Messages.Count == 0);
            }
            set
            {
                this.isValid = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes IsValid value with 'true'.
        /// </summary>
        public ValidationEventArgs()
        {
            this.Messages = new List<string>();
            this.IsValid = true;
        }

        /// <summary>
        /// Initializes IsValid with parameter.
        /// </summary>
        /// <param name="isValid">Boolean value to initialize IsValid property.</param>
        public ValidationEventArgs(bool isValid)
        {
            this.IsValid = isValid;
        }

        #endregion
    }
}
