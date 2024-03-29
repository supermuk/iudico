﻿using System;

namespace IUDICO.Common.Models.Shared.Statistics
{
    /// <summary>
    /// Class which represents score (points).
    /// </summary>
    public class Score
    {
        #region Private Fields
                
        private float? _ScaledScore;

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
                return _ScaledScore;
            }

            protected set
            {
                if (value != null)
                {
                    if (value < -1 || value > 1)
                        throw new ArgumentOutOfRangeException("ScaledScore", "Value of scaled score should be in range of [-1;1]");
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

        /// <summary>
        /// Returns scaled score in percent representation.
        /// Double nullable value in range [-100; 100] inclusive.
        /// </summary>
        /// <returns>Double nullable value.</returns>
        public double? ToPercents()
        {
            return ScaledScore * 100;
        }
    
        #endregion    
    }
}
