// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Log.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

#if DEBUG

namespace Microsoft.LearningComponents.Frameset
{
    /// <summary>
    /// Summary description for Log
    /// </summary>
    public sealed class Log : IDisposable
    {
        // NOTE: Change return value to enable logging.
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private bool IsLogEnabled
        {
            get
            {
                return false;
            }
        }

        private bool mIsDisposed;

        private const string Filename = @"c:\backup\framesetLog.txt";

        private string mFilePath;

        private StreamWriter mWriter;

        /// <summary>
        /// Open the log. 
        /// </summary>
        /// <param name="filePath">The path to the log file.</param>
        private Log(string filePath)
        {
            // Even in debug builds don't log anything unless compiled to do so.
            if (!this.IsLogEnabled)
            {
                return;
            }

            this.mWriter = File.AppendText(filePath);
            this.mFilePath = filePath;
        }

        /// <summary>Initializes a new instance of <see cref="Log"/>.</summary>
        public Log()
            : this(Filename)
        {
        }

        /// <summary>See <see cref="IDisposable.Dispose"/>.</summary>
        public void Dispose()
        {
            if (this.mIsDisposed)
            {
                return;
            }

            // Even in debug builds don't log anything unless compiled to do so.
            if (!this.IsLogEnabled)
            {
                return;
            }

            if (this.mWriter != null)
            {
                this.mWriter.Dispose();
                this.mWriter = null;
            }
            this.mIsDisposed = true;

            GC.SuppressFinalize(this);
        }

        /// <summary>Writes a message to the log.</summary>
        /// <param name="message">The message to write.</param>
        public void WriteMessage(string message)
        {
            // Even in debug builds don't log anything unless compiled to do so.
            if (!this.IsLogEnabled)
            {
                return;
            }

            this.mWriter.Write("\r\nLog Entry : ");
            this.mWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            this.mWriter.WriteLine("  :{0}", message);
            this.mWriter.WriteLine("-------------------------------");
            // Update the underlying file.
            this.mWriter.Flush();
        }

        /// <summary>The log file path.</summary>
        public string FilePath
        {
            get
            {
                return this.mFilePath;
            }
        }
    }
}

#endif