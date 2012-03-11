using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Analytics.Models.AnomalyDetectionAlg.Accuracy
{
    public interface IAccuracy
    {
       void calcAccuracy(TrainingSet nonAnomaliesSet, TrainingSet anomaliesSet, PxFormula formula);
    }
}
