using System.Web.UI;

namespace NEWS
{
    public static class Redirector
    {
        public static string HomeLink()
        {
            return "~/Home.aspx";
        }

        public static string NewsLink(int newsID)
        {
            return "~/News.aspx?ID=" + newsID;
        }

        public static string AddNewsLink(int categoryID)
        {
            return "~/AddNews.aspx?CategoryID=" + categoryID;
        }

        public static string CategoryLink(int categoryID)
        {
            return "~/Category.aspx?ID=" + categoryID;
        }

        public static void GoHome(this Page currentPage)
        {
            currentPage.Response.Redirect(HomeLink(), true);
        }

        public static void GoToAddNews(this Page currentPage, int categoryID)
        {
            currentPage.Response.Redirect(AddNewsLink(categoryID), true);
        }

        public static void GoToNews(this Page currentPage, int newsID)
        {
            currentPage.Response.Redirect(NewsLink(newsID));
        }

        public static void GoToCategory(this Page currentPage, int categoryID)
        {
            currentPage.Response.Redirect(CategoryLink(categoryID));
        }
    }
}
