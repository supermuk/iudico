using System;
using System.Collections.Generic;

namespace IUDICO.Analytics.Models.AnomalyDetectionAlg
{
    public class TrainingSet
    {
        private readonly List<double[]> set;
        private readonly int dimensionsCount;

        public TrainingSet(int dimensions) 
        {
            this.set = new List<double[]>();
            this.dimensionsCount = dimensions;
        }

        public void AddRecord(double[] vector)
        {
            if (vector.GetLength(0) != this.dimensionsCount)
            {
                throw new Exception("All vectors in training set must have same size");
            }

            this.set.Add((double[])vector.Clone());
        }

        public List<double[]> GetAllRecords()
        {
            return this.set;
        }

        public int GetDimensionsCount()
        {
            return this.dimensionsCount;
        }

        public int GetCountOfRecords()
        {
            return this.set.Count;
        }
    }
}