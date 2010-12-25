using System;

namespace IUDICO.Common.Models.Shared.Statistics
{
    /// <summary>
    /// Class which represents score (points).
    /// </summary>
    public class Score
    {
        #region Private Fields

        /// <summary>
        /// Value from -1 to 1 inclusive: [-1; 1].
        /// </summary>
        private float? _ScaledScore;

        #endregion

        #region Public Properties

        public float? ScaledScore 
        { 
            get
            {
                return _ScaledScore;
            }

            protected set
            {
                if (value != null)
                {
                    if (value < -1 || value > 1)
                        throw new ArgumentOutOfRangeException("scaledScore", "Value of scaled score should be in range of [-1;1]");
                }

                _ScaledScore = value;
            }
        }

        #endregion

        #region Constructors

        public Score(float? scaledScore)
        {
            ScaledScore = scaledScore;
        }

        #endregion

        #region Public Methods

        public double? ToPercents()
        {
            return ScaledScore * 100;
        }

        public int? ToInt()
        {
            return (int)ToPercents();
        }
    
        #endregion    
    }
}
