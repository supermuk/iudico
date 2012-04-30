using System.Collections.Generic;
using System.Linq;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using IUDICO.Common.Models.Shared;
using IUDICO.Analytics.Models.AnomalyDetectionAlg;

namespace IUDICO.Analytics.Models.DecisionTrees
{
    /// <summary>
    /// Create training sets for anomaly detection algorithm.
    /// </summary>
    public class TrainingSetsCreator
    {
        public static TrainingSet[] GenerateTrainingSets(IEnumerable<KeyValuePair<User, double[]>> studentsAndMarks, string[] normalRecords, string[] anomalies)
        {
            var countOfEntries = normalRecords.Length + anomalies.Length;
            var inputData = new double[countOfEntries][];
            var outputData = new int[countOfEntries];
            var counter = 0;

            foreach (var studentAndMarks in studentsAndMarks)
            {
                if (normalRecords.Contains(studentAndMarks.Key.OpenId))
                {
                    inputData[counter] = studentAndMarks.Value;
                    outputData[counter++] = 1;
                }

                if (!anomalies.Contains(studentAndMarks.Key.OpenId))
                {
                    continue;
                }

                inputData[counter] = studentAndMarks.Value;
                outputData[counter++] = 0;
            }

            var countOfFeatures = studentsAndMarks.ElementAt(0).Value.Length;
            var features = new DecisionVariable[countOfFeatures];
            features[0] = new DecisionVariable("0", DecisionAttributeKind.Continuous, new AForge.DoubleRange(80, 1200));
            
            for (var i = 1; i < countOfFeatures; i++)
            {
                features[i] = new DecisionVariable(i.ToString(), DecisionAttributeKind.Continuous, new AForge.DoubleRange(0, 10));
            }

            // Create the Decision tree with only 2 result values
            var tree = new DecisionTree(features, 2);

            // Creates a new instance of the C4.5 learning algorithm
            var c45 = new C45Learning(tree);

            // Learn the decision tree
            var error = c45.Run(inputData, outputData);

            // Split all data into normal and anomalies
            var setOfNormalRecords = studentsAndMarks.Where(x => tree.Compute(x.Value) == 1);
            var setOfAnomalies = studentsAndMarks.Where(x => tree.Compute(x.Value) == 0);
                        
            // Split normal records into 2 groups (one for training set and one for anomaly detection ocurency detection)
            var setOfNormalRecordsList = setOfNormalRecords.ToList();
            var splitCount = setOfNormalRecordsList.Count * 2 / 3;
            var setOfNormalRecordsTr1 = setOfNormalRecordsList.GetRange(0, splitCount);
            var setOfNormalRecordsTr2 = setOfNormalRecordsList.GetRange(splitCount, setOfNormalRecordsList.Count - splitCount);
            // Create Training Sets
            var trSetNormalFirst = CreateTrainingSetFromResources(setOfNormalRecordsTr1);
            var trSetNormalSecond = CreateTrainingSetFromResources(setOfNormalRecordsTr2);
            var trSetAnomalies = CreateTrainingSetFromResources(setOfAnomalies);

            return new[] { trSetNormalFirst, trSetNormalSecond, trSetAnomalies };
        }

        private static TrainingSet CreateTrainingSetFromResources(IEnumerable<KeyValuePair<User, double[]>> studentsAndMarks)
        {
            var resultTrainingSet = new TrainingSet(studentsAndMarks.First().Value.Length);
            
            foreach (var studentAndMark in studentsAndMarks)
            {
                resultTrainingSet.AddRecord(studentAndMark.Value);
            }

            return resultTrainingSet;
        }
    }
}