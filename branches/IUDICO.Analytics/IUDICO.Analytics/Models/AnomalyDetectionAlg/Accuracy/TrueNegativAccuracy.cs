using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Analytics.Models.AnomalyDetectionAlg.Accuracy
{
    public class TrueNegativAccuracy: IAccuracy
    {
        public void calcAccuracy(TrainingSet nonAnomaliesSet, TrainingSet anomaliesSet, PxFormula formula)
        {
            double max_p_x_value = formula.calculate(formula.getNu());
            double[] nonAnomaliesValues = new double[nonAnomaliesSet.set.Count];
            for (int i = 0; i < nonAnomaliesSet.set.Count; i++)
            {
                nonAnomaliesValues[i] = formula.calculate(nonAnomaliesSet.set[i]);
            }
            double[] anomaliesValues = new double[anomaliesSet.set.Count];
            for (int i = 0; i < anomaliesSet.set.Count; i++)
            {
                anomaliesValues[i] = formula.calculate(anomaliesSet.set[i]);
            }
            
            List<KeyValuePair<int, double>> list = new List<KeyValuePair<int, double>>();
            double h = max_p_x_value / 50;
            for (int i = 0; i < 50; i++)
            {
                double e = max_p_x_value - h*i;
                int count = 0;
                foreach(double value in anomaliesValues)
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
    }
}