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
            this.sw = new StreamWriter(fileName, true);
            this.sw.AutoFlush = true;
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
            this.sw.Write(buffer, index, count);
        }

        public override void Write(string value)
        {
            this.sw.Write(value);
        }

        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }

    }
}
