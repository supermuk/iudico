using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Analytics.Models.Storage;
namespace IUDICO.Analytics.Models.Quality
{
    public class DisciplineModel
    {
        private List<KeyValuePair<Topic, double>> allowedTopics;
        private string disciplineName;
        private double disciplineQuality;
        public DisciplineModel(List<KeyValuePair<Topic, double>> allowedTopics, string disciplineName, double disciplineQuality)
        {
            this.allowedTopics = allowedTopics;
            this.disciplineName = disciplineName;
            this.disciplineQuality = disciplineQuality;
        }
        public string GetDisciplineName()
        {
            return this.disciplineName;
        }
        public double GetDisciplineQuality()
        {
            return this.disciplineQuality;
        }
        public bool NoData()
        {
            return this.allowedTopics == null;
        }
        public List<KeyValuePair<Topic, double>> GetAllowedTopics()
        {
            return this.allowedTopics;
        }
    }
}