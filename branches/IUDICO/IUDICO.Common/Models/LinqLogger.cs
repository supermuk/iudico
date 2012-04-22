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
            sw = new StreamWriter(fileName, true);
            sw.AutoFlush = true;
        }

        ~LinqLogger()
        {
            if (sw != null)
            {
                sw.Close();
                sw.Dispose();
            }
        }

        public override void Write(char[] buffer, int index, int count)
        {
            sw.Write(buffer, index, count);
        }

        public override void Write(string value)
        {
            sw.Write(value);
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.Default; }
        }

    }
}
