using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Analytics.Models.AnomalyDetectionAlg
{
    public class PxFormula
    {
        // to do
        private double[] nu;
        //private Matrix teta;
        private double e;

        public PxFormula(double[] nu /*, Matrix sigma,*/, double e)
        {
            this.nu = nu;
            this.e = e;
        }

        public double calculate(double[] x)
        {
            // to do
            return 1;
        }

        /// <summary>
        /// This method return true/false answer for question, if passed values are anomalies or not.
        /// </summary>
        /// <param name="x">vector of values to check</param>
        /// <returns> true - is anomaly, false - normal entry</returns>
        public bool isAnomaly(double[] x)
        {
            return calculate(x) < e;
        }
    }
}