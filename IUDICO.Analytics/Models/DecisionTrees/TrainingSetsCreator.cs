using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accord.Math;
using Accord.MachineLearning;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Analytics.Models.AnomalyDetectionAlg;

namespace IUDICO.Analytics.Models.DecisionTrees
{
    /// <summary>
    /// Create training sets for anomaly detection algorithm.
    /// </summary>
    public class TrainingSetsCreator
    {
        public static TrainingSet[] generateTrainingSets(IEnumerable<KeyValuePair<User, double[]>> studentsAndMarks, string[] normalRecords, string[] anomalies)
        {
            int countOfEntries = normalRecords.Length + anomalies.Length;
            double[][] inputData = new double[countOfEntries][];
            int[] outputData = new int[countOfEntries];
            int counter = 0;
            foreach (KeyValuePair<User, double[]> studentAndMarks in studentsAndMarks)
            {
                if (normalRecords.Contains(studentAndMarks.Key.OpenId))
                {
                    inputData[counter] = studentAndMarks.Value;
                    outputData[counter++] = 1;
                }
                if (anomalies.Contains(studentAndMarks.Key.OpenId))
                {
                    inputData[counter] = studentAndMarks.Value;
                    outputData[counter++] = 0;
                }
            }

            int countOfFeatures = studentsAndMarks.ElementAt(0).Value.Length;
            DecisionVariable[] features = new DecisionVariable[countOfFeatures];
            features[0] = new DecisionVariable("0", DecisionAttributeKind.Continuous,new AForge.DoubleRange(80,1200));
            for(int i = 1; i < countOfFeatures; i++)
            {
                features[i] = new DecisionVariable(i.ToString(), DecisionAttributeKind.Continuous, new AForge.DoubleRange(0, 10));
            }

            // Create the Decision tree with only 2 result values
            DecisionTree tree = new DecisionTree(features, 2);

            // Creates a new instance of the C4.5 learning algorithm
            C45Learning c45 = new C45Learning(tree);

            // Learn the decision tree
            double error = c45.Run(inputData, outputData);

            // Split all data into normal and anomalies
            var setOfNormalRecords = studentsAndMarks.Where(x => tree.Compute(x.Value) == 1);
            var setOfAnomalies = studentsAndMarks.Where(x => tree.Compute(x.Value) == 0);
                        
            // Split normal records into 2 groups (one for training set and one for anomaly detection ocurency detection)
            var setOfNormalRecordsList = setOfNormalRecords.ToList();
            int splitCount = setOfNormalRecordsList.Count * 2 / 3;
            var setOfNormalRecords_Tr1 = setOfNormalRecordsList.GetRange(0, splitCount);
            var setOfNormalRecords_Tr2 = setOfNormalRecordsList.GetRange(splitCount, setOfNormalRecordsList.Count - splitCount);
            // Create Training Sets
            TrainingSet trSetNormalFirst = createTrainingSetFromResources(setOfNormalRecords_Tr1);
            TrainingSet trSetNormalSecond = createTrainingSetFromResources(setOfNormalRecords_Tr2);
            TrainingSet trSetAnomalies = createTrainingSetFromResources(setOfAnomalies);

            return new TrainingSet[] { trSetNormalFirst, trSetNormalSecond, trSetAnomalies };
        }

        private static TrainingSet createTrainingSetFromResources(IEnumerable<KeyValuePair<User, double[]>> studentsAndMarks)
        {
            TrainingSet resultTrainingSet = new TrainingSet(studentsAndMarks.First().Value.Length);
            foreach (KeyValuePair<User, double[]> studentAndMark in studentsAndMarks)
            {
                resultTrainingSet.addRecord(studentAndMark.Value);
            }
            return resultTrainingSet;
        }
    }
}