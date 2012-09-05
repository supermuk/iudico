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
      private bool enableLogging;

      public LinqLogger(string fileName)
      {
#if DEBUG
         this.sw = new StreamWriter(fileName, true);
         this.sw.AutoFlush = true;
#endif
         System.Configuration.Configuration rootWebConfig =
          System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
         if (rootWebConfig.AppSettings.Settings.Count > 0)
         {
            System.Configuration.KeyValueConfigurationElement customSetting =
               rootWebConfig.AppSettings.Settings["EnableLogging"];
            if (customSetting != null)
            {
               this.enableLogging = bool.Parse(customSetting.Value);
            }
         }
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

      #region Write and WriteLine methods
      public override void Write(char[] buffer, int index, int count)
      {
         if (!this.enableLogging)
         {
            return;
         }
#if DEBUG
         this.sw.Write(buffer, index, count);
#endif
      }

      public override void Write(string value)
      {
         if (!this.enableLogging)
         {
            return;
         }
#if DEBUG
         this.sw.Write(value);
#endif
      }

      public override void Write(string format, params object[] arg)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(format, arg);
      }

      public override void Write(bool value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(value);
      }

      public override void Write(char value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(value);
      }

      public override void Write(char[] buffer)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(buffer);
      }

      public override void Write(decimal value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(value);
      }

      public override void Write(double value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(value);
      }

      public override void Write(float value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(value);
      }
      public override void Write(int value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(value);
      }

      public override void Write(long value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(value);
      }

      public override void Write(object value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(value);
      }

      public override void Write(string format, object arg0)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(format, arg0);
      }

      public override void Write(string format, object arg0, object arg1)
      {
         if (!this.enableLogging)
         {
            return;
         }
         
         base.Write(format, arg0, arg1);
      }

      public override void Write(string format, object arg0, object arg1, object arg2)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(format, arg0, arg1, arg2);
      }

      public override void Write(uint value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(value);
      }

      public override void Write(ulong value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.Write(value);
      }

      public override void WriteLine()
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine();
      }

      public override void WriteLine(bool value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(value);
      }

      public override void WriteLine(char value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(value);
      }

      public override void WriteLine(decimal value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(value);
      }

      public override void WriteLine(double value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(value);
      }

      public override void WriteLine(float value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         
         base.WriteLine(value);
      }

      public override void WriteLine(int value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(value);
      }

      public override void WriteLine(long value)
      {
         if (!this.enableLogging)
         {
            return;
         } 
         base.WriteLine(value);
      }

      public override void WriteLine(string format, object arg0)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(format, arg0);
      }

      public override void WriteLine(string format, object arg0, object arg1)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(format, arg0, arg1);
      }

      public override void WriteLine(string format, object arg0, object arg1, object arg2)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(format, arg0, arg1, arg2);
      }

      public override void WriteLine(string format, params object[] arg)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(format, arg);
      }

      public override void WriteLine(uint value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(value);
      }

      public override void WriteLine(ulong value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(value);
      }

      public override void WriteLine(object value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(value);
      }

      public override void WriteLine(char[] buffer)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(buffer);
      }

      public override void WriteLine(char[] buffer, int index, int count)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(buffer, index, count);
      }

      public override void WriteLine(string value)
      {
         if (!this.enableLogging)
         {
            return;
         }
         base.WriteLine(value);
      }
      #endregion

      public override Encoding Encoding
      {
         get { return Encoding.Default; }
      }

   }
}
