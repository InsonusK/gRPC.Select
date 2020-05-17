using System;
using System.Linq.Expressions;
using gRPC.Select.Interface;
using gRPC.Select.LogicConditions;
using NUnit.Framework;

namespace gRPC.Select.Test.LogicConditions
{
    [TestFixture]
    public class LogicConditionAdd_Test
    {
        private ILogicCondition _logicCondition;

        [SetUp]
        public void SetUp()
        {
            _logicCondition = new LogicConditionAnd();
        }

        [Test]
        [TestCase(true,true)]
        public void True(bool leftValue,  bool rightValue)
        {
            // Array;
            var _left = Expression.Constant(leftValue);
            var _right = Expression.Constant(rightValue);

            // Act
            var _assertValue = _logicCondition.Build(_left, _right);
            var _lambda = Expression.Lambda<Func<bool>>(_assertValue);

            // Assert
            Assert.IsTrue(_lambda.Compile().Invoke());
        }

        [Test]
        [TestCase(true,false)]
        [TestCase(false,true)]
        [TestCase(false,false)]
        public void False(bool leftValue,  bool rightValue)
        {
            // Array
            var _left = Expression.Constant(leftValue);
            var _right = Expression.Constant(rightValue);

            // Act
            var _assertValue = _logicCondition.Build(_left, _right);
            var _lambda = Expression.Lambda<Func<bool>>(_assertValue);

            // Assert
            Assert.IsFalse(_lambda.Compile().Invoke());
        }

        [Test]
        [TestCaseSource(typeof(LogicCondition_TestCases), nameof(LogicCondition_TestCases.NotLogic))]
        public void Errors(Type type, object leftValue, object rightValue)
        {
            // Array
            Assert.IsInstanceOf(type, leftValue);
            Assert.IsInstanceOf(type, rightValue);
            var _left = Expression.Constant(leftValue);
            var _right = Expression.Constant(rightValue);

            // Act Assert
            Assert.Catch<InvalidOperationException>(() => _logicCondition.Build(_left, _right));
        }

        [Test]
        public void StartExpression()
        {
            // Array

            // Act
            var _assertValue = _logicCondition.Start();
            var _lambda = Expression.Lambda<Func<bool>>(_assertValue);

            // Assert
            Assert.IsTrue(_lambda.Compile().Invoke());
        }
    }
}
