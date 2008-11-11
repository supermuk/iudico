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

        private FilesEntity(int pid, byte[] file, string name)
        {
            id = UniqueId.Generate();
            this.pid = pid;
            this.file = file;
            this.name = name;
            isDirectory = 0;
        }

        private FilesEntity(int pageRef, string name)
        {
            id = UniqueId.Generate();
            this.pageRef = pageRef;
            this.name = name;
            isDirectory = 1;
        }

        public static FilesEntity newDirectory(int pageRef, string name)
        {
            return new FilesEntity(pageRef, name);
        }

        public static FilesEntity newFile(int pid, byte[] file, string name)
        {
            return new FilesEntity(pid, file, name);
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