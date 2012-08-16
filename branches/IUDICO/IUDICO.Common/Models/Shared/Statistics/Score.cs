using System;

namespace IUDICO.Common.Models.Shared.Statistics
{
    /// <summary>
    /// Class which represents score (points).
    /// </summary>
    public class Score
    {
        #region Private Fields

        private float? minScore;

        private float? maxScore;

        private float? rawScore;
                
        private float? scaledScore;

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Value from -1 to 1 inclusive: [-1; 1]. 
        /// Represents points from -100% to 100%.
        /// Even scaled score supports negative [-1;0) scores, negative values are conformance exceptions.
        /// But be aware! They can be used.
        /// This is also a Nullable value, because sometimes it is not possible to retrieve points for test.
        /// </summary>
        public float? ScaledScore 
        { 
            get
            {
                return this.scaledScore;
            }

            set
            {
                if (value != null)
                {
                    if (value < -1 || value > 1)
                        throw new ArgumentOutOfRangeException("value", "Value of scaled score should be in range of [-1;1]");
                }

                this.scaledScore = value;
            }
        }

        public float? MinScore
        {
            get { return this.minScore; }
            set
            {
                if (value != null)
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("value", "Value of minimum score should be greater or equal than zero");

                    this.minScore = value;
                }
            }
        }

        public float? MaxScore
        {
            get { return this.maxScore; }
            set
            {
                if (value != null)
                {
                    if (value <= 0)
                        throw new ArgumentOutOfRangeException("value", "Value of maximum score should be greater than zero");

                    this.maxScore = value;
                }
            }
        }

        public float? RawScore
        {
            get { return this.rawScore; }
            set
            {
                if (value != null)
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("value", "Value of raw score should be greater than zero");

                    this.rawScore = value;
                }
            }
        }

        #endregion

        #region Constructors

        public Score(float? minScore, float? maxScore, float? rawScore, float? scaledScore)
        {
            this.MinScore = minScore;
            this.MaxScore = maxScore;
            this.RawScore = rawScore;
            this.ScaledScore = scaledScore;
        }

        public Score()
        {
            this.minScore = 0;
            this.maxScore = 0;
            this.rawScore = 0;
            this.scaledScore = 0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns scaled score in percent representation.
        /// Double nullable value in range [-100; 100] inclusive.
        /// </summary>
        /// <returns>Double nullable value.</returns>
        public double? ToPercents()
        {
            return this.ScaledScore * 100;
        }
    
        #endregion    
    }
}
