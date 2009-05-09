using System;
using System.Runtime.Serialization;

namespace TestingSystem.Compile
{
    /// <summary>
    /// Compile exception class. It wraps any error during compilation.
    /// </summary>
    [Serializable]
    public class CompileException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="CompileException"/> class.
        /// </summary>
        public CompileException()
            : base()
        {
            // empty
        }

        /// <summary>
        /// Creates a new instance of <see cref="CompileException"/> class.
        /// The detailed error message is given.
        /// </summary>
        ///
        /// <param name="message">
        /// A detailed error message descrbing the nature of the error.
        /// </param>
        public CompileException(string message)
            : base(message)
        {
            // empty
        }

        /// <summary>
        /// Creates a new instance of <see cref="CompileException"/> class.
        /// The detailed error message and the original cause of this error are given.
        /// </summary>
        ///
        /// <param name="message">
        /// A detailed error message descrbing the nature of the error.
        /// </param>
        ///
        /// <param name="cause">
        /// The original cause of this error.
        /// </param>
        public CompileException(string message, Exception cause)
            : base(message, cause)
        {
            // empty
        }

        /// <summary>
        /// Creates a new instance of <see cref="CompileException"/> class. It is a
        /// serialization constructor with given serialization info and streaming context.
        /// </summary>
        ///
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the serialized object data about the
        /// exception being thrown.
        /// </param>
        ///
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains contextual information about the source
        /// or destination.
        /// </param>
        protected CompileException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // empty
        }
    }
}