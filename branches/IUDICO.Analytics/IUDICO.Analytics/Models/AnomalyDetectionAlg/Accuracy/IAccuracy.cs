using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Analytics.Models.AnomalyDetectionAlg.Accuracy
{
    public interface IAccuracy
    {
        /// <summary>
        /// This method require two training sets and it automaticaly calculate and set e value in PxFormula object.
        /// </summary>
        /// <param name="nonAnomaliesSet">training set with ONLY normal entries</param>
        /// <param name="anomaliesSet">training set with ONLY ANOMALY entries</param>
        /// <param name="formula">formula with setted nu and sigme values</param>
        void calcAccuracy(TrainingSet nonAnomaliesSet, TrainingSet anomaliesSet, PxFormula formula);
    }
}
