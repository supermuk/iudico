using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Analytics.Models.AnomalyDetectionAlg
{
    public class TrainingSet
    {
        public List<double[]> set;

        public TrainingSet() 
        {
            this.set = new List<double[]>();
        }
    }
}