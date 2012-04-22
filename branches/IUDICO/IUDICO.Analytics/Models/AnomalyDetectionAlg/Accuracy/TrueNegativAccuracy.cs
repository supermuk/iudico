using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Analytics.Models.AnomalyDetectionAlg.Accuracy
{
    /// <summary>
    /// This class represent accurancy that count only CORECT finding of anomalies.
    /// More corectly finded anomalies is better, even if some normal records marked as anomalies.
    /// </summary>
    public class TrueNegativAccuracy: IAccuracy
    {
        private double[] nonAnomaliesPxValues;
        private double[] anomaliesPxValues;

        public void calcAccuracy(TrainingSet nonAnomaliesSet, TrainingSet anomaliesSet, PxFormula formula)
        {
            calculatePxValues(nonAnomaliesSet, anomaliesSet, formula);
            double max_p_x_value = formula.calculate(formula.getNu());
            List<KeyValuePair<int, double>> list = new List<KeyValuePair<int, double>>();
            double h = max_p_x_value / 50;
            for (int i = 0; i < 50; i++)
            {
                double e = 0 + h*i;
                int count = 0;
                foreach (double value in anomaliesPxValues)
                {
                    if (value < e)
                    {
                        count++;
                    }
                }
                list.Add(new KeyValuePair<int, double>(count, e));
            }
            formula.setE(list.First(x => x.Key == list.Max(y => y.Key)).Value);
        }

        private void calculatePxValues(TrainingSet nonAnomaliesSet, TrainingSet anomaliesSet, PxFormula formula)
        {
            nonAnomaliesPxValues = new double[nonAnomaliesSet.getCountOfRecords()];
            for (int i = 0; i < nonAnomaliesSet.getCountOfRecords(); i++)
            {
                nonAnomaliesPxValues[i] = formula.calculate(nonAnomaliesSet.getAllRecords()[i]);
            }
            anomaliesPxValues = new double[anomaliesSet.getCountOfRecords()];
            for (int i = 0; i < anomaliesSet.getCountOfRecords(); i++)
            {
                anomaliesPxValues[i] = formula.calculate(anomaliesSet.getAllRecords()[i]);
            }
        }
    }
}