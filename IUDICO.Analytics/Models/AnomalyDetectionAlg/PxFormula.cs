using System;

using Accord.Math;

namespace IUDICO.Analytics.Models.AnomalyDetectionAlg
{
    public class PxFormula
    {
        private readonly double[] nu;
        private readonly double[,] sigmaInverse;
        private readonly double sigmaDet;
        private double e = -1;

        public PxFormula(double[] nu, double[,] sigma)
        {
            this.nu = nu;
            this.sigmaDet = sigma.Determinant();
            this.sigmaInverse = sigma.Inverse();
        }

        public double Calculate(double[] x)
        {
            var substractNu = x.Subtract(this.nu);
            var matrixMultiply = substractNu.Multiply(this.sigmaInverse);
            var matrixCalculations = 0.0;

            var dimensionCount = this.nu.GetLength(0);

            for (var i = 0; i < dimensionCount; i++)
            {
                matrixCalculations += matrixMultiply[i] * substractNu[i];
            }

            var pX = Math.Exp(-0.5 * matrixCalculations) / (Math.Pow(2 * Math.PI, dimensionCount / 2) * Math.Sqrt(this.sigmaDet));

            return pX;
        }

        /// <summary>
        /// This method return true/false answer for question, if passed values are anomalies or not.
        /// </summary>
        /// <param name="x">vector of values to check</param>
        /// <returns> true - is anomaly, false - normal entry</returns>
        public bool IsAnomaly(double[] x)
        {
            return this.Calculate(x) < this.e;
        }

        public double[] GetNu()
        {
            return (double[])this.nu.Clone();
        }

        public void SetE(double newE)
        {
            this.e = newE;
        }
    }
}