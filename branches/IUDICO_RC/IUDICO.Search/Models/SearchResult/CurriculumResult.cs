using System;

using IUDICO.Common.Models.Shared;

namespace IUDICO.Search.Models.SearchResult
{
    using IUDICO.Common;

    public class DisciplineResult : ISearchResult
    {
        protected Discipline discipline;
        protected string update;

        public DisciplineResult(Discipline discipline, string update)
        {
            this.discipline = discipline;
            this.update = update;
        }

        public int GetId()
        {
            return this.discipline.Id;
        }

        public string GetName()
        {
            return this.discipline.Name;
        }

        public string GetText()
        {
            return Localization.GetMessage("DisciplineName") + ": " + this.GetName() + "</br>" + Localization.GetMessage("Updated") + ": " + this.update + "</br>" + this.GetUrl(); // "discipline";
        }

        public string GetUrl()
        {
            return "/Discipline/" + this.discipline.Id + "/Edit";
        }
    }
}