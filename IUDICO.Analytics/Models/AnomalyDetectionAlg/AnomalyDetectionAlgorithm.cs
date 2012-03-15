using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Analytics.Models.AnomalyDetectionAlg.Accuracy;
using Accord.Math;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Shared.Statistics;

namespace IUDICO.Analytics.Models.AnomalyDetectionAlg
{
    public class AnomalyDetectionAlgorithm
    {
        /// <summary>
        /// This class contain only static fields and methods
        /// </summary>
        public AnomalyDetectionAlgorithm() { }

        /// <summary>
        /// This field contain object that implement IAccuracy interface.
        /// This object automaticaly calculate value of PxFormula e field.
        /// </summary>
        private IAccuracy accuracy = new TrueNegativAccuracy();

        /// <summary>
        /// This method required training sets for algorithm training and return PxFormula with setted algorithm values.
        /// </summary>
        /// <param name="trainingSet">This is TrainingSet class object. Must contain training set with ONLY normal records.</param>
        /// <param name="nonAnomaliesSet">This is TrainingSet class object. Must contain training set with ONLY normal records.</param>
        /// <param name="anomaliesSet">This is TrainingSet class object. Must contain training set with ONLY ANOMALY records.</param>
        /// <returns></returns>
        public PxFormula trainAlgorithm (TrainingSet trainingSet, TrainingSet nonAnomaliesSet, TrainingSet anomaliesSet)
        {
            PxFormula formula = calcFormula(trainingSet);
            accuracy.calcAccuracy(nonAnomaliesSet, anomaliesSet, formula);
            return formula;
        }

        /// <summary>
        /// This method creates PxFormula class object and set in it calculated nu vector and sigma matrix values.
        /// </summary>
        /// <param name="trainingSet">This is TrainingSet class object. Must contain training set with ONLY normal records.</param>
        /// <returns>PxFormula class object with setted nu vector and sigma matrix values.</returns>
        private PxFormula calcFormula (TrainingSet trainingSet)
        {
            int training_set_count = trainingSet.getCountOfRecords();

            // calculate nu vector value
            double[] nu = new double[2];
            foreach (double[] feature_vector in trainingSet.getAllRecords())
            {
                nu = Matrix.Add(nu, feature_vector);
            }
            nu = Matrix.Divide(nu,training_set_count);

            // calculate sigme matrix
            double[,] sigma = new double[2,2];
            foreach (double[] feature_vector in trainingSet.getAllRecords())
            {
                double[] x_nu = Matrix.Subtract(feature_vector, nu);
                sigma = Matrix.Add(sigma, Matrix.Multiply(Matrix.ColumnVector(x_nu), Matrix.RowVector(x_nu)));
            }
            sigma = Matrix.Divide(sigma, training_set_count);

            // return PxFormula class object with setted nu and sigma values
            return new PxFormula(nu, sigma);
        }

        /// <summary>
        /// Set accuracy for this training process. All accuracy clases must implement IAccuracy interface.
        /// </summary>
        /// <param name="newAccuracy">Object that implement IAccuracy interface</param>
        public void setAccuracy (IAccuracy newAccuracy)
        {
            accuracy = newAccuracy;
        }


        // For demo

        public static IEnumerable<KeyValuePair<KeyValuePair<User, AttemptResult>, bool>> runAlg(IEnumerable<KeyValuePair<User, AttemptResult>> studentsAndMarks, string[] ts1, string[] ts2n, string[] ts2a)
        {
            var trainingSet1Resource = studentsAndMarks.Where(x => ts1.Contains(x.Key.OpenId) == true);
            var trainingSet2NormalResource = studentsAndMarks.Where(x => ts2n.Contains(x.Key.OpenId) == true);
            var trainingSet2AnomaliesResource = studentsAndMarks.Where(x => ts2a.Contains(x.Key.OpenId) == true);

            var trainingSet1 = createTrainingSetFromResources(trainingSet1Resource);
            var trainingSet2Normal = createTrainingSetFromResources(trainingSet2NormalResource);
            var trainingSet2Anomalies = createTrainingSetFromResources(trainingSet2AnomaliesResource);

            AnomalyDetectionAlgorithm train = new AnomalyDetectionAlgorithm();

            var formula = train.trainAlgorithm(trainingSet1, trainingSet2Normal, trainingSet2Anomalies);
            List<KeyValuePair<KeyValuePair<User, AttemptResult>, bool>> res = new List<KeyValuePair<KeyValuePair<User, AttemptResult>, bool>>();
            foreach (KeyValuePair<User, AttemptResult> studentAndMark in studentsAndMarks.Except(trainingSet1Resource).Except(trainingSet2NormalResource).Except(trainingSet2AnomaliesResource))
            {
                bool isAnomaly = formula.isAnomaly(getFeaturesValues(studentAndMark));

                res.Add(new KeyValuePair<KeyValuePair<User, AttemptResult>, bool>(studentAndMark, isAnomaly));
            }

            return res;
        }

        private static TrainingSet createTrainingSetFromResources(IEnumerable<KeyValuePair<User, AttemptResult>> studentsAndMarks)
        {
            TrainingSet resultTrainingSet = new TrainingSet(2);
            foreach (KeyValuePair<User, AttemptResult> studentAndMark in studentsAndMarks)
            {
                double[] newRecord = new double[2];
                double mark = studentAndMark.Value.Score.ToPercents().Value;
                double time = studentAndMark.Value.FinishTime.Value.Subtract(studentAndMark.Value.StartTime.Value).TotalSeconds;
                newRecord[0] = mark;
                newRecord[1] = time;
                resultTrainingSet.addRecord(newRecord);
            }
            return resultTrainingSet;
        }

        private static double[] getFeaturesValues(KeyValuePair<User, AttemptResult> studentAndMark)
        {
            double[] newRecord = new double[2];
            double mark = studentAndMark.Value.Score.ToPercents().Value;
            double time = studentAndMark.Value.FinishTime.Value.Subtract(studentAndMark.Value.StartTime.Value).TotalSeconds;
            newRecord[0] = mark;
            newRecord[1] = time;
            return newRecord;
        }
    }
}