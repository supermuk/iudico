using System;
using IUDICO.DataModel.Common;

namespace IUDICO.DataModel.Dao.Entity
{
    public class CourseEntity
    {
        private readonly string description;
        private readonly int id;

        private readonly string name;

        private readonly DateTime uploadDate;

        private readonly int version;


        public CourseEntity(string name, string description, int version)
        {
            id = UniqueId.Generate();
            this.name = name;
            this.description = description;
            this.version = version;
            uploadDate = DateTime.Now;
        }

        public int Version
        {
            get { return version; }
        }

        public DateTime UploadDate
        {
            get { return uploadDate; }
        }

        public string Description
        {
            get { return description; }
        }

        public string Name
        {
            get { return name; }
        }

        public int Id
        {
            get { return id; }
        }
    }
}