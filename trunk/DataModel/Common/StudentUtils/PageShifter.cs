using System;
using System.Collections.Generic;
using System.Text;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.StudentUtils
{
    class PageShifter
    {
        private const char PagesIdSplitter = '*';

        private readonly List<int> _usedPages = new List<int>();

        private readonly List<int> _notUsedPages = new List<int>();


        public PageShifter(int themeId)
        {
            var theme = ServerModel.DB.Load<TblThemes>(themeId);
            var pagesIds = ServerModel.DB.LookupIds<TblPages>(theme, null);

            if (theme.PageOrderRef == FxPageOrders.Random.ID)
                RandomShuffle(pagesIds);

            int indexOfDivading = GetIndexOfDivading(theme.PageCountToShow, pagesIds.Count);
            DivadeUsedAndNotUsedPages(pagesIds, indexOfDivading);
        }

        public string GetRequestParameter()
        {
            var pagesIdsParameter = new StringBuilder();

            foreach (var id in _usedPages)
                pagesIdsParameter.Append(id + PagesIdSplitter.ToString());

            var pagesIdsParameterString = pagesIdsParameter.ToString();

            if (pagesIdsParameterString.Equals(string.Empty))
                return pagesIdsParameterString;

            return pagesIdsParameterString.Remove(pagesIdsParameterString.Length - 1); //Remove last character and return
        }

        public IList<TblPages> UsedPages
        {
            get
            {
                return ServerModel.DB.Load<TblPages>(_usedPages);
            }
        }

        public IList<TblPages> NotUsedPages
        {
            get
            {
                return ServerModel.DB.Load<TblPages>(_notUsedPages);
            }
        }


        public static string[] GetPagesFromParameter(string pagesIds)
        {
            return (pagesIds == null) ? new string[0] : pagesIds.Split(PagesIdSplitter);
        }


        private void DivadeUsedAndNotUsedPages(List<int> pagesIds, int indexOfDivading)
        {
            for (int i = 0; i < indexOfDivading; i++)
                _usedPages.Add(pagesIds[i]);

            for (int j = indexOfDivading; j < pagesIds.Count; j++)
                _notUsedPages.Add(pagesIds[j]);

        }

        private static int GetIndexOfDivading(int? countOfPageToShow, int pageCount)
        {
            return (countOfPageToShow == null) ? pageCount : Math.Min(pageCount, (int)countOfPageToShow);
        }

        private static void RandomShuffle(List<int> collectionOfPagesIds)
        {
            collectionOfPagesIds.Sort(new RandomPageComparer());
        }
    }

    class RandomPageComparer : Comparer<int>
    {
        private readonly Random _randomizer = new Random();

        public override int Compare(int x, int y)
        {
            if (x.Equals(y))
                return 0;

            return _randomizer.Next(-1, 1);
        }
    }
}
