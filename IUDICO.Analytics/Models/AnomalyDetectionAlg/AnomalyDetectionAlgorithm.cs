using System.Collections.Generic;
using System.Linq;
using IUDICO.Analytics.Models.AnomalyDetectionAlg.Accuracy;
using Accord.Math;
using IUDICO.Common.Models.Shared;

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
        public PxFormula TrainAlgorithm(TrainingSet trainingSet, TrainingSet nonAnomaliesSet, TrainingSet anomaliesSet)
        {
            var formula = CalcFormula(trainingSet);
            
            this.accuracy.CalcAccuracy(nonAnomaliesSet, anomaliesSet, formula);

            return formula;
        }

        /// <summary>
        /// This method creates PxFormula class object and set in it calculated nu vector and sigma matrix values.
        /// </summary>
        /// <param name="trainingSet">This is TrainingSet class object. Must contain training set with ONLY normal records.</param>
        /// <returns>PxFormula class object with setted nu vector and sigma matrix values.</returns>
        private static PxFormula CalcFormula(TrainingSet trainingSet)
        {
            var trainingSetCount = trainingSet.GetCountOfRecords();
            var size = trainingSet.GetAllRecords().First().Length;

            // calculate nu vector value
            var nu = new double[size];

            nu = trainingSet.GetAllRecords().Aggregate(nu, (current, featureVector) => current.Add(featureVector));
            nu = nu.Divide(trainingSetCount);

            // calculate sigme matrix
            var sigma = new double[size, size];

            sigma = trainingSet.GetAllRecords().Select(featureVector => featureVector.Subtract(nu)).Aggregate(sigma, (current, xNu) => current.Add(Matrix.ColumnVector(xNu).Multiply(Matrix.RowVector(xNu))));

            sigma = sigma.Divide(trainingSetCount);

            // return PxFormula class object with setted nu and sigma values
            return new PxFormula(nu, sigma);
        }

        /// <summary>
        /// Set accuracy for this training process. All accuracy clases must implement IAccuracy interface.
        /// </summary>
        /// <param name="newAccuracy">Object that implement IAccuracy interface</param>
        public void SetAccuracy(IAccuracy newAccuracy)
        {
            this.accuracy = newAccuracy;
        }

        /// <summary>
        /// Get accuracy from this object. All accuracy clases must implement IAccuracy interface.
        /// </summary>
        /// <param name="newAccuracy">Object that implement IAccuracy interface</param>
        public IAccuracy GetAccuracy()
        {
            return this.accuracy;
        }

        /// <summary>
        /// Train and run anomaly detection algorithm for passes students.
        /// </summary>
        /// <param name="studentsAndMarks">all students, that we want to check</param>
        /// <param name="trainingSet1">Training set</param>
        /// <param name="trainingSet2Normal">Cross validation set with only normal records</param>
        /// <param name="trainingSet2Anomalies">Cross validation set with inly anomalies records</param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<KeyValuePair<User, double[]>, bool>> RunAlg(IEnumerable<KeyValuePair<User, double[]>> studentsAndMarks, TrainingSet trainingSet1, TrainingSet trainingSet2Normal, TrainingSet trainingSet2Anomalies)
        {            
            var train = new AnomalyDetectionAlgorithm();
            var formula = train.TrainAlgorithm(trainingSet1, trainingSet2Normal, trainingSet2Anomalies);

            return (from studentAndMark in studentsAndMarks
                    let isAnomaly = formula.IsAnomaly(studentAndMark.Value)
                    select new KeyValuePair<KeyValuePair<User, double[]>, bool>(studentAndMark, isAnomaly)).ToList();
        }
    }
}