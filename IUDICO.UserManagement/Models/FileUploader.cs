using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;

namespace IUDICO.UserManagement.Models
{
    public class FileUploader
    {
        public static void UploadAvatar(Guid id, HttpPostedFileBase file, HttpServerUtilityBase server)
        {
            if (file != null)
            {
                string fileName = Path.GetFileName(id.ToString() + ".png");
                string fullPath = Path.Combine(server.MapPath("~/Data/Avatars"), fileName);
                FileInfo fileInfo = new FileInfo(fullPath);

                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }

                file.SaveAs(fullPath);
            }
        }
        public static void DeleteAvatar(Guid id, HttpServerUtilityBase server)
        {
            string fileName = Path.GetFileName(id.ToString() + ".png");
            string fullPath = Path.Combine(server.MapPath("~/Data/Avatars"), fileName);
            FileInfo fileInfo = new FileInfo(fullPath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }
    }
}