using System;
using gRPC.Select.CompareConditions;
using gRPC.Select.Interface;
using GRPC.Selector.Enum;
using NUnit.Framework;

namespace gRPC.Select.Test.Tests.CompareConditions
{
    [TestFixture]
    public class MathConditionStrategy_Test
    {
        private ICompareConditionStrategy _strategy;

        [SetUp]
        public void SetUp()
        {
            _strategy = new CompareConditionStrategy();
        }

        [Test]
        [TestCase(typeof(CompareConditionEq), CompareCondition.Eq)]
        [TestCase(typeof(CompareConditionGe), CompareCondition.Ge)]
        [TestCase(typeof(CompareConditionGt), CompareCondition.Gt)]
        [TestCase(typeof(CompareConditionLe), CompareCondition.Le)]
        [TestCase(typeof(CompareConditionLt), CompareCondition.Lt)]
        [TestCase(typeof(CompareConditionNe), CompareCondition.Ne)]
        public void Match(Type expressionBuilderType, CompareCondition condition)
        {
            // Array

            // Act
            var _expressionBuilder = _strategy.GetExpressionBuilder(condition);

            // Assert
            Assert.IsInstanceOf(expressionBuilderType, _expressionBuilder);
        }


    }
}
