using System;
using IUDICO.Common.Models;

namespace IUDICO.Search.Models.SearchResult
{
    public class CurriculumResult : ISearchResult
    {
        protected Curriculum _Curriculum;
        protected string _Update;

        public CurriculumResult(Curriculum curriculum, string update)
        {
            _Curriculum = curriculum;
            _Update = update;
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
            return Localization.getMessage("CurriculumName") + ": " + GetName() + "</br>" + Localization.getMessage("Updated") + ": " + _Update + "</br>" + GetUrl(); //"curriculum";
        }

        public String GetUrl()
        {
            return "/Curriculum/" + _Curriculum.Id + "/Edit";
        }
    }
}