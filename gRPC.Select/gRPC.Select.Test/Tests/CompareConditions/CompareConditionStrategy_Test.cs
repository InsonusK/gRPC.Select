using System;
using gRPC.Select.CompareConditions;
using gRPC.Select.Interface;
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
        [TestCase(typeof(CompareConditionEq), GRPC.Selector.CompareCondition.Eq)]
        [TestCase(typeof(CompareConditionGe), GRPC.Selector.CompareCondition.Ge)]
        [TestCase(typeof(CompareConditionGt), GRPC.Selector.CompareCondition.Gt)]
        [TestCase(typeof(CompareConditionLe), GRPC.Selector.CompareCondition.Le)]
        [TestCase(typeof(CompareConditionLt), GRPC.Selector.CompareCondition.Lt)]
        [TestCase(typeof(CompareConditionNe), GRPC.Selector.CompareCondition.Ne)]
        public void Match(Type expressionBuilderType, GRPC.Selector.CompareCondition condition)
        {
            // Array

            // Act
            var _expressionBuilder = _strategy.GetExpressionBuilder(condition);

            // Assert
            Assert.IsInstanceOf(expressionBuilderType, _expressionBuilder);
        }


    }
}
