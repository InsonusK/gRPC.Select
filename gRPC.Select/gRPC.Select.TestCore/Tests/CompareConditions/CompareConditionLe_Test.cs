using System;
using System.Linq.Expressions;
using gRPC.Select.CompareConditions;
using gRPC.Select.Interface;
using NUnit.Framework;

namespace gRPC.Select.Test.Tests.CompareConditions
{
    [TestFixture]
    public class MathConditionLe_Test
    {
        private ICompareCondition _compareCondition;

        [SetUp]
        public void SetUp()
        {
            _compareCondition = new CompareConditionLe();
        }

        [Test]
        [TestCaseSource(typeof(CompareCondition_TestCases), nameof(CompareCondition_TestCases.EqualComparable))]
        [TestCaseSource(typeof(CompareCondition_TestCases), nameof(CompareCondition_TestCases.LeftLesser))]
        public void True(Type type, object leftValue, object rightValue)
        {
            // Array
            Assert.IsInstanceOf(type, leftValue);
            Assert.IsInstanceOf(type, rightValue);
            var _left = Expression.Constant(leftValue);
            var _right = Expression.Constant(rightValue);

            // Act
            var _assertValue = _compareCondition.Build(_left, _right);
            var _lambda = Expression.Lambda<Func<bool>>(_assertValue);

            // Assert
            Assert.IsTrue(_lambda.Compile().Invoke());
        }

        [Test]
        [TestCaseSource(typeof(CompareCondition_TestCases), nameof(CompareCondition_TestCases.LeftGreater))]
        public void False(Type type, object leftValue, object rightValue)
        {
            // Array
            Assert.IsInstanceOf(type, leftValue);
            Assert.IsInstanceOf(type, rightValue);
            var _left = Expression.Constant(leftValue);
            var _right = Expression.Constant(rightValue);

            // Act
            var _assertValue = _compareCondition.Build(_left, _right);
            var _lambda = Expression.Lambda<Func<bool>>(_assertValue);

            // Assert
            Assert.IsFalse(_lambda.Compile().Invoke());
        }

        [Test]
        [TestCaseSource(typeof(CompareCondition_TestCases), nameof(CompareCondition_TestCases.NotComparable))]
        public void Errors(Type type, object leftValue, object rightValue)
        {
            // Array
            Assert.IsInstanceOf(type, leftValue);
            Assert.IsInstanceOf(type, rightValue);
            var _left = Expression.Constant(leftValue);
            var _right = Expression.Constant(rightValue);

            // Act Assert
            Assert.Catch<InvalidOperationException>(() => _compareCondition.Build(_left, _right));
        }
    }
}
