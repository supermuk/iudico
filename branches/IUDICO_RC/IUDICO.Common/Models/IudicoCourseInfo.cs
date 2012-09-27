using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models
{
        /// <summary>
        /// Class that represent course info, such as nodes info and overall maximum score 
        /// </summary>
        public class IudicoCourseInfo
        {
            /// <summary>
            /// Course Id in database
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// List of nodes
            /// </summary>
            public List<IudicoNodeInfo> NodesInfo { get; set; }

            /// <summary>
            /// Overall maximum score 
            /// Obtained by calculating maximum score of each node
            /// </summary>
            public double OverallMaxScore
            {
                get
                {
                    double overallMaxScore = 0.0;
                    foreach (IudicoNodeInfo ni in this.NodesInfo)
                    {
                        overallMaxScore += ni.MaxScore;
                    }

                    return overallMaxScore;
                }
            }

            public IudicoCourseInfo()
            {
                this.NodesInfo = new List<IudicoNodeInfo>();
            }
        }

        public class IudicoNodeInfo
        {
            /// <summary>
            /// Node Id in database
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Parent course Id in database
            /// </summary>
            public int CourseId { get; set; }

            /// <summary>
            /// List of questions
            /// </summary>
            public List<IudicoQuestionInfo> QuestionsInfo { get; set; }

            /// <summary>
            /// Maximum score of node
            /// Obtained by calculating maximum score of each question
            /// </summary>
            public double MaxScore
            {
                get
                {
                    double nodeMaxScore = 0.0;
                    foreach (IudicoQuestionInfo qi in this.QuestionsInfo)
                    {
                        nodeMaxScore += qi.MaxScore;
                    }

                    return nodeMaxScore;
                }
            }

            public IudicoNodeInfo()
            {
                this.QuestionsInfo = new List<IudicoQuestionInfo>();
            }
        }

        public abstract class IudicoQuestionInfo
        {
            /// <summary>
            /// Type of question
            /// </summary>
            public IudicoQuestionType Type { get; set; }

            /// <summary>
            /// Question text
            /// </summary>
            public string QuestionText { get; set; }

            /// <summary>
            /// Question maximum score
            /// </summary>
            public double MaxScore { get; set; }
        }

        public class IudicoSimpleQuestion : IudicoQuestionInfo
        {
            /// <summary>
            /// Correct answer of simple question
            /// </summary>
            public string CorrectAnswer { get; set; }
        }

        public class IudicoChoiceQuestion : IudicoQuestionInfo
        {
            /// <summary>
            /// Correct choices, i.e.: "BCD", "A"
            /// </summary>
            public string CorrectChoice { get; set; }

            /// <summary>
            /// List of options, i.e.: ("option0", "cat"), ("option1", "dog")
            /// </summary>
            public List<Tuple<string, string>> Options { get; set; }

            public IudicoChoiceQuestion()
            {
                this.Options = new List<Tuple<string, string>>();
            }
        }

        public class IudicoCompiledTest : IudicoQuestionInfo
        {
            /// <summary>
            /// Test inputs in compile test, i.e.: ("testInput0", "5 0 1 6 2"), ("testInput1", "4 8 2 12 7 0 11 6")
            /// </summary>
            public List<Tuple<string, string>> TestInputs { get; set; }

            /// <summary>
            /// Correct test outputs in compile test, i.e.: ("testOutput0", "0 1 2 5 6"), ("testInput1", "0 2 4 6 7 8 11 12")
            /// </summary>
            public List<Tuple<string, string>> TestOutputs { get; set; }

            /// <summary>
            /// Number of test inputs and outputs
            /// </summary>
            public int NumberOfTests
            {
                get
                {
                    if (this.TestInputs.Count != this.TestOutputs.Count)
                        throw new Exception("Mismatch number of test inputs and outputs");

                    return this.TestInputs.Count;
                }
            }

            public IudicoCompiledTest()
            {
                this.TestInputs = new List<Tuple<string, string>>();
                this.TestOutputs = new List<Tuple<string, string>>();
            }
        }

        /// <summary>
        /// Type of questions in iudico system
        /// </summary>
        public enum IudicoQuestionType
        {
            IudicoSimple,
            IudicoChoice,
            IudicoCompile
        }
}
