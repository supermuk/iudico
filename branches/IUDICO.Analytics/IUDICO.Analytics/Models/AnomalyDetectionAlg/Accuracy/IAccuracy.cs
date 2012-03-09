using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Analytics.Models.AnomalyDetectionAlg.Accuracy
{
    interface IAccuracy
    {
        public double calcAccuracy(TrainingSet nonAnomaliesSet, TrainingSet anomaliesSet, PxFormula formula);
    }
}
