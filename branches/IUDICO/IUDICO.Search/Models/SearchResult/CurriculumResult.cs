using System;
using IUDICO.Common.Models;

namespace IUDICO.Search.Models.SearchResult
{
    public class CurriculumResult : ISearchResult
    {
        protected Curriculum _Curriculum;

        public CurriculumResult(Curriculum curriculum)
        {
            _Curriculum = curriculum;
        }

        public int GetId()
        {
            return _Curriculum.Id;
        }

        public String GetName()
        {
            return _Curriculum.Name;
        }

        public String GetText()
        {
            return "curriculum";
        }

        public String GetUrl()
        {
            return "/Curriculum/" + _Curriculum.Id + "/Edit";
        }
    }
}