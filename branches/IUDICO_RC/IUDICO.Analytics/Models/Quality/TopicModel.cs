using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Analytics.Models.Quality
{
    public class TopicModel
    {
        private List<KeyValuePair<Topic, double>> allowedTopics;
        private string disciplineName;
        private double disciplineQuality;
        public TopicModel(ILmsService ilmsService, long selectDisciplineId)
        {
            IEnumerable<Topic> temp_allowedTopics = ilmsService.FindService<IDisciplineService>().GetTopicsByDisciplineId((int)selectDisciplineId);
            this.disciplineName = ilmsService.FindService<IDisciplineService>().GetDiscipline((int)selectDisciplineId).Name;

            if (temp_allowedTopics != null & temp_allowedTopics.Count() != 0)
            {
                double tempDisciplineQuality = 0;
                var temp = new List<KeyValuePair<Topic, double>>();
                foreach (var topic in temp_allowedTopics)
                {
                    List<double> quality = new List<double>();
                    quality.Add(0);
                    tempDisciplineQuality += quality.Sum() / quality.Count;
                    temp.Add(new KeyValuePair<Topic, double>(topic, quality.Sum() / quality.Count));
                }

                this.disciplineQuality = tempDisciplineQuality / temp.Count;
                this.allowedTopics = temp;
            }
            else
            {
                this.disciplineQuality = 0;
                this.allowedTopics = null;
            }
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