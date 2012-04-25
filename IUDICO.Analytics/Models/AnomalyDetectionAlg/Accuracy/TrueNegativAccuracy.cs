using System.Collections.Generic;
using System.Linq;

namespace IUDICO.Analytics.Models.AnomalyDetectionAlg.Accuracy
{
    /// <summary>
    /// This class represent accurancy that count only CORECT finding of anomalies.
    /// More corectly finded anomalies is better, even if some normal records marked as anomalies.
    /// </summary>
    public class TrueNegativAccuracy : IAccuracy
    {
        private double[] nonAnomaliesPxValues;
        private double[] anomaliesPxValues;

        public void CalcAccuracy(TrainingSet nonAnomaliesSet, TrainingSet anomaliesSet, PxFormula formula)
        {
            this.CalculatePxValues(nonAnomaliesSet, anomaliesSet, formula);
            
            var maxPxValue = formula.Calculate(formula.GetNu());
            var list = new List<KeyValuePair<int, double>>();
            var h = maxPxValue / 50;

            for (var i = 0; i < 50; i++)
            {
                var e = 0 + h * i;
                var count = this.anomaliesPxValues.Count(value => value < e);

                list.Add(new KeyValuePair<int, double>(count, e));
            }

            formula.SetE(list.First(x => x.Key == list.Max(y => y.Key)).Value);
        }

        private void CalculatePxValues(TrainingSet nonAnomaliesSet, TrainingSet anomaliesSet, PxFormula formula)
        {
            this.nonAnomaliesPxValues = new double[nonAnomaliesSet.GetCountOfRecords()];

            for (var i = 0; i < nonAnomaliesSet.GetCountOfRecords(); i++)
            {
                this.nonAnomaliesPxValues[i] = formula.Calculate(nonAnomaliesSet.GetAllRecords()[i]);
            }

            this.anomaliesPxValues = new double[anomaliesSet.GetCountOfRecords()];

            for (var i = 0; i < anomaliesSet.GetCountOfRecords(); i++)
            {
                this.anomaliesPxValues[i] = formula.Calculate(anomaliesSet.GetAllRecords()[i]);
            }
        }
    }
}