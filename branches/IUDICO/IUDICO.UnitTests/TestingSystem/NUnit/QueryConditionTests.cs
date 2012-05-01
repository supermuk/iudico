// -----------------------------------------------------------------------
// <copyright file="QueryConditionTests.cs" company="">
// 
// </copyright>
// -----------------------------------------------------------------------

namespace IUDICO.UnitTests.TestingSystem.NUnit
{
    using System;
    using global::NUnit.Framework;
    using IUDICO.TestingSystem.Models.VOs;
    using Microsoft.LearningComponents.Storage;

    /// <summary>
    /// Unit tests for <see cref="QueryCondition"/>.
    /// </summary>
    [TestFixture]
    public class QueryConditionTests
    {
        [Test]
        public void CreateQueryConditionWithDefaultConstructor()
        {
            var queryCondition = new QueryCondition();

            Assert.IsNull(queryCondition.ColumnName);
            Assert.IsNull(queryCondition.Value);
            Assert.AreEqual(LearningStoreConditionOperator.Equal, queryCondition.ConditionOperator);
        }

        [Test]
        public void CreateQueryConditionWithFewParameters()
        {
            const string ColumnName = "some column";
            var obj = TimeZone.CurrentTimeZone;

            var queryCondition = new QueryCondition(ColumnName, obj);

            Assert.AreEqual(ColumnName, queryCondition.ColumnName);
            Assert.AreSame(obj, queryCondition.Value);
            Assert.AreEqual(LearningStoreConditionOperator.Equal, queryCondition.ConditionOperator);
        }

        [Test]
        public void CreateQueryConditionWithAllParameters()
        {
            const string ColumnName = "some column 2";
            const LearningStoreConditionOperator Condition = LearningStoreConditionOperator.LessThan;
            var obj = TimeZone.CurrentTimeZone;

            var queryCondition = new QueryCondition(ColumnName, Condition, obj);

            Assert.AreEqual(ColumnName, queryCondition.ColumnName);
            Assert.AreEqual(Condition, queryCondition.ConditionOperator);
            Assert.AreSame(obj, queryCondition.Value);
        }
    }
}