using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Analytics.Models.AnomalyDetectionAlg
{
    public class TrainingSet
    {
        private List<double[]> set;
        private int dimensions_count;

        public TrainingSet(int dimensions) 
        {
            this.set = new List<double[]>();
            this.dimensions_count = dimensions;
        }

        public void addRecord(double[] vector)
        {
            if (vector.GetLength(0) != this.dimensions_count)
            {
                throw new Exception("All vectors in training set must have same size");
            }
            this.set.Add((double[])vector.Clone());
        }

        public List<double[]> getAllRecords()
        {
            return this.set;
        }

        public int getDimensionsCount()
        {
            return this.dimensions_count;
        }

        public int getCountOfRecords()
        {
            return this.set.Count;
        }
    }
}