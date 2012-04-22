using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accord.Math;

namespace IUDICO.Analytics.Models.AnomalyDetectionAlg
{
    public class PxFormula
    {
        private double[] nu;
        private double[,] sigma_inverse;
        private double sigma_det;
        private double e = -1;

        public PxFormula(double[] nu, double[,] sigma)
        {
            this.nu = nu;
            this.sigma_det = Matrix.Determinant(sigma);
            this.sigma_inverse = Matrix.Inverse(sigma);
        }

        public double calculate(double[] x)
        {
            double[] x_substract_nu = Matrix.Subtract(x, nu);
            double[] matrix_multiply = Matrix.Multiply(x_substract_nu, sigma_inverse);
            double matrix_calculations = 0;
            int dimension_count = nu.GetLength(0);
            for (int i = 0; i < dimension_count; i++)
            {
                matrix_calculations += matrix_multiply[i] * x_substract_nu[i];
            }
            double p_x = Math.Exp(-0.5 * matrix_calculations) / (Math.Pow(2 * Math.PI, dimension_count / 2) * Math.Sqrt(sigma_det));
            return p_x;
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

        public double[] getNu()
        {
            return (double[]) this.nu.Clone();
        }

        public void setE(double e)
        {
            this.e = e;
        }
    }
}