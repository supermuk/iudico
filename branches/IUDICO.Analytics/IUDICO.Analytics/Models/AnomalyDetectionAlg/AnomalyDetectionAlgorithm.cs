using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Analytics.Models.AnomalyDetectionAlg.Accuracy;

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
        private IAccuracy accuracy;

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
            accuracy.calcAccuracy(nonAnomaliesSet,anomaliesSet,formula);
            return formula;
        }

        /// <summary>
        /// This method creates PxFormula class object and set in it calculated nu vector and sigma matrix values.
        /// </summary>
        /// <param name="trainingSet">This is TrainingSet class object. Must contain training set with ONLY normal records.</param>
        /// <returns>PxFormula class object with setted nu vector and sigma matrix values.</returns>
        private PxFormula calcFormula (TrainingSet trainingSet)
        {
            return new PxFormula(new double[] { 0, 0 }, 0);
        }

        /// <summary>
        /// Set accuracy for this training process. All accuracy clases must implement IAccuracy interface.
        /// </summary>
        /// <param name="newAccuracy">Object that implement IAccuracy interface</param>
        public void setAccuracy (IAccuracy newAccuracy)
        {
            accuracy = newAccuracy;
        }
    }
}