using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private float? _scaledScore;

        #endregion

        #region Public Properties

        public float? ScaledScore 
        { 
            get
            {
                return _scaledScore;
            }
            protected set
            {
                if (value != null)
                {
                    if (value < -1 || value > 1)
                        throw new ArgumentOutOfRangeException("scaledScore", "Value of scaled score should be in range of [-1;1]");
                }

                _scaledScore = value;
            }
        }

        #endregion

        #region Constructors

        public Score(float? scaledScore)
        {
            this.ScaledScore = scaledScore;
        }

        #endregion

        #region Public Methods

        public double? ToPercents()
        {
            return this.ScaledScore * 100;
        }

        public int? ToInt()
        {
            return (int)ToPercents();
        }
    
        #endregion    
    }
}
