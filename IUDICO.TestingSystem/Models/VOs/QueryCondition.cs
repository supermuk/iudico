// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryCondition.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.LearningComponents.Storage;

namespace IUDICO.TestingSystem.Models.VOs
{
    public struct QueryCondition
    {
        #region Public Readonly Fields

        public readonly string ColumnName;

        public readonly LearningStoreConditionOperator ConditionOperator;

        public readonly object Value;

        #endregion

        #region Constructors

        public QueryCondition(string columnName, LearningStoreConditionOperator conditionOperator, object value)
        {
            this.ColumnName = columnName;
            this.ConditionOperator = conditionOperator;
            this.Value = value;
        }

        public QueryCondition(string columnName, object value)
            : this(columnName, LearningStoreConditionOperator.Equal, value)
        {
        }

        #endregion
    }
}