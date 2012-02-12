using System;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Search.Models.SearchResult
{
    public class DisciplineResult : ISearchResult
    {
        protected Discipline _Discipline;
        protected string _Update;

        public DisciplineResult(Discipline discipline, string update)
        {
            _Discipline = discipline;
            _Update = update;
        }

        public int GetId()
        {
            return _Discipline.Id;
        }

        public String GetName()
        {
            return _Discipline.Name;
        }

        public String GetText()
        {
            return Localization.getMessage("DisciplineName") + ": " + GetName() + "</br>" + Localization.getMessage("Updated") + ": " + _Update + "</br>" + GetUrl(); //"discipline";
        }

        public String GetUrl()
        {
            return "/Discipline/" + _Discipline.Id + "/Edit";
        }
    }
}