using IUDICO.DataModel.Common;

namespace IUDICO.DataModel.Dao.Entity
{
    public class FilesEntity
    {
        private readonly int id;
        private readonly int pid;
        private readonly int pageRef;
        private readonly byte[] file;
        private readonly string name;
        private readonly int isDirectory;

        private FilesEntity(int pid, byte[] file, string name, int pageRef, bool isDirectory)
        {
            id = UniqueId.Generate();
            this.pid = pid;
            this.file = file;
            this.name = name;
            this.pageRef = pageRef;
            this.isDirectory = isDirectory ? 1 : 0;
        }

        private FilesEntity(int id, int pid, byte[] file, string name, int pageRef, bool isDirectory)
        {
            this.id = id;
            this.pid = pid;
            this.file = file;
            this.name = name;
            this.pageRef = pageRef;
            this.isDirectory = isDirectory ? 1 : 0;
        }

        public static FilesEntity newDirectory(int pageRef, string name)
        {
            return new FilesEntity(0, null, name, pageRef, true);
        }

        public static FilesEntity newDirectory(int id, int pageRef, string name)
        {
            return new FilesEntity(id, 0, null, name, pageRef, true);
        }

        public static FilesEntity newFile(int pid, byte[] file, string name, int pageRef)
        {
            return new FilesEntity(pid, file, name, pageRef, false);
        }

        public static FilesEntity newFile(int id, int pid, byte[] file, string name, int pageRef)
        {
            return new FilesEntity(id, pid, file, name, pageRef, false);
        }

        public int PageRef
        {
            get { return pageRef; }
        }

        public int IsDirectory
        {
            get { return isDirectory; }
        }

        public string Name
        {
            get { return name; }
        }

        public byte[] File
        {
            get { return file; }
        }

        public int Pid
        {
            get { return pid; }
        }

        public int Id
        {
            get { return id; }
        }
    }
}