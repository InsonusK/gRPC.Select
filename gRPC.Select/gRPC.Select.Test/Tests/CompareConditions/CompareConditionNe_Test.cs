using System;
using System.Linq.Expressions;
using gRPC.Select.CompareConditions;
using gRPC.Select.Interface;
using NUnit.Framework;

namespace gRPC.Select.Test.Tests.CompareConditions
{
    [TestFixture]
    public class MathConditionNe_Test
    {
        private ICompareCondition _compareCondition;

        [SetUp]
        public void SetUp()
        {
            _compareCondition = new CompareConditionNe();
        }

        [Test]
        [TestCaseSource(typeof(CompareCondition_TestCases), nameof(CompareCondition_TestCases.NotEqualComparable))]
        [TestCaseSource(typeof(CompareCondition_TestCases), nameof(CompareCondition_TestCases.NotEqualNotComparable))]
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
        [TestCaseSource(typeof(CompareCondition_TestCases), nameof(CompareCondition_TestCases.EqualComparable))]
        [TestCaseSource(typeof(CompareCondition_TestCases), nameof(CompareCondition_TestCases.EqualNotComparable))]
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
    }
}
