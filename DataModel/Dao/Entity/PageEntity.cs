using CourseImport.Dao.Entity;
using IUDICO.DataModel.Common;

namespace IUDICO.DataModel.Dao.Entity
{
    public class PageEntity
    {
        private readonly int id;
        private readonly int themeRef;
        private readonly string pageName;
        private readonly int pageRank;
        private readonly int pageType;
        private readonly byte[] pageFile;


        public PageEntity(int id, int themeRef, string pageName, byte[] pageFile, PageTypeEnum pageType, int pageRank)
        {
            this.id = id;
            this.themeRef = themeRef;
            this.pageName = pageName;
            this.pageFile = pageFile;
            this.pageType = (int)pageType;
            this.pageRank = pageRank;
        }

        public PageEntity(int themeRef, string pageName, byte[] pageFile, PageTypeEnum pageType, int pageRank)
        {
            id = UniqueId.Generate();
            this.themeRef = themeRef;
            this.pageName = pageName;
            this.pageFile = pageFile;
            this.pageType = (int)pageType;
            this.pageRank = pageRank;
        }

        public PageEntity(int themeRef, string pageName, byte[] pageFile, PageTypeEnum pageType)
        {
            id = UniqueId.Generate();
            this.themeRef = themeRef;
            this.pageName = pageName;
            this.pageFile = pageFile;
            this.pageType = (int)pageType;
        }

        public int Id
        {
            get { return id; }
        }

        public int ThemeRef
        {
            get { return themeRef; }
        }

        public string PageName
        {
            get { return pageName; }
        }

        public int PageRank
        {
            get { return pageRank; }
        }

        public int PageType
        {
            get { return pageType; }
        }

        public byte[] PageFile
        {
            get { return pageFile; }
        }
    }
}