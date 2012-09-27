using System;
using System.IO;
using System.Text;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;

namespace IUDICO.DisciplineManagement.Helpers
{
   public static class Zipper
   {
      /// <summary>
      /// Puts folder's content to zip archive
      /// </summary>
      /// <param name="zipFileName">Path to result zip file</param>
      /// <param name="folder">Folder to archive</param>
      public static void CreateZip(string zipFileName, string folder)
      {
         ZipConstants.DefaultCodePage = System.Text.Encoding.Default.CodePage;
         string[] filenames = Directory.GetFiles(folder);

         // Zip up the files - From SharpZipLib Demo Code
         using (ZipOutputStream s = new
             ZipOutputStream(File.Create(zipFileName)))
         {
            s.SetLevel(9); // 0-9, 9 being the highest compression

            byte[] buffer = new byte[4096];

            foreach (string file in filenames)
            {
               ZipEntry entry = new ZipEntry(Path.GetFileName(file));

               entry.DateTime = DateTime.Now;
               entry.IsUnicodeText = true;
               s.PutNextEntry(entry);

               using (FileStream fs = File.OpenRead(file))
               {
                  int sourceBytes;
                  do
                  {
                     sourceBytes = fs.Read(buffer, 0, buffer.Length);

                     s.Write(buffer, 0, sourceBytes);

                  } while (sourceBytes > 0);
               }
            }
            s.Finish();
            s.Close();
         }

      }

      private const int ReadBufferSize = 2048;

      public static void ExtractZipFile(string zipFileName, string dirName)
      {
         var data = new byte[ReadBufferSize];

         using (var zipStream = new ZipInputStream(File.OpenRead(zipFileName)))
         {
            ZipEntry entry;
            while ((entry = zipStream.GetNextEntry()) != null)
            {
               var fullName = Path.Combine(dirName, entry.Name);

               if (entry.IsDirectory && !Directory.Exists(fullName))
               {
                  Directory.CreateDirectory(fullName);
               }
               else if (entry.IsFile)
               {
                  var dir = Path.GetDirectoryName(fullName);

                  if (!Directory.Exists(dir))
                  {
                     Directory.CreateDirectory(dir);
                  }

                  using (var fileStream = File.Create(fullName))
                  {
                     int readed;

                     while ((readed = zipStream.Read(data, 0, ReadBufferSize)) > 0)
                     {
                        fileStream.Write(data, 0, readed);
                     }
                  }
               }
            }
         }
      }

   }
}