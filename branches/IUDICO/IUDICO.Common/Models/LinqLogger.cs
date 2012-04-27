using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IUDICO.Common.Models
{
    public class LinqLogger : TextWriter
    {
        private readonly StreamWriter sw;

        public LinqLogger(string fileName)
        {
#if DEBUG
            this.sw = new StreamWriter(fileName, true);
            this.sw.AutoFlush = true;
#endif
        }

        ~LinqLogger()
        {
            if (this.sw != null)
            {
                try
                {
                    this.sw.Close();
                }
                finally
                {
                    this.sw.Dispose();
                }
                
            }
        }

        public override void Write(char[] buffer, int index, int count)
        {
#if DEBUG
            this.sw.Write(buffer, index, count);
#endif
        }

        public override void Write(string value)
        {
#if DEBUG
            this.sw.Write(value);
#endif
        }

        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }

    }
}
