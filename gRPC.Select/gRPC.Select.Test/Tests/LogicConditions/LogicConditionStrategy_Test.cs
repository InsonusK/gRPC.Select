using System;
using gRPC.Select.Interface;
using gRPC.Select.LogicConditions;
using NUnit.Framework;

namespace gRPC.Select.Test.Tests.LogicConditions
{
    [TestFixture]
    public class LogicConditionStrategy_Test
    {
        private ILogicConditionStrategy _strategy;

        [SetUp]
        public void SetUp()
        {
            _strategy = new LogicConditionStrategy();
        }

        [Test]
        [TestCase(typeof(LogicConditionAnd), GRPC.Selector.LogicCondition.And)]
        [TestCase(typeof(LogicConditionOr), GRPC.Selector.LogicCondition.Or)]
        public void Match(Type expressionBuilderType, GRPC.Selector.LogicCondition condition)
        {
            // Array

            // Act
            var _expressionBuilder = _strategy.GetExpressionBuilder(condition);

            // Assert
            Assert.IsInstanceOf(expressionBuilderType, _expressionBuilder);
        }


    }
}
