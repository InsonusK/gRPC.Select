using System;
using System.Linq.Expressions;
using gRPC.Select.Interface;
using gRPC.Select.PropertyConverters;
using NUnit.Framework;

namespace gRPC.Select.Test.Tests.PropertyConverters
{
    [TestFixture]
    public class NullConverter_Test
    {
        class TestClass
        {
            public string StringValue { get; set; }
            public int IntValue { get; set; }
            public double DoubleValue { get; set; }
            public bool BoolValue { get; set; }
        }

        private IValueConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new NullConverter();
        }

        [Test]
        public void ConvertParameter_String()
        {
            // Array
            var _parameter = Expression.Parameter(typeof(string), "x");

            // Act
            var _assertedExpression = _converter.Convert(_parameter);
            var _lambdaExpression =
                Expression.Lambda<Func<string, string>>(_assertedExpression, new ParameterExpression[] {_parameter});
            var _assertedLambda = _lambdaExpression.Compile();

            // Assert
            Assert.AreEqual("aOEUi", _assertedLambda("aOEUi"));
        }

        [Test]
        public void ConvertParameter_Int()
        {
            // Array
            var _parameter = Expression.Parameter(typeof(int), "x");

            // Act
            var _assertedExpression = _converter.Convert(_parameter);
            var _lambdaExpression =
                Expression.Lambda<Func<int, int>>(_assertedExpression, new ParameterExpression[] {_parameter});
            var _assertedLambda = _lambdaExpression.Compile();

            // Assert
            Assert.AreEqual(1, _assertedLambda(1));
        }

        [Test]
        public void ConvertParameter_Double()
        {
            // Array
            var _parameter = Expression.Parameter(typeof(double), "x");

            // Act
            var _assertedExpression = _converter.Convert(_parameter);
            var _lambdaExpression =
                Expression.Lambda<Func<double, double>>(_assertedExpression, new ParameterExpression[] {_parameter});
            var _assertedLambda = _lambdaExpression.Compile();

            // Assert
            Assert.AreEqual(1.234, _assertedLambda(1.234));
        }

        [Test]
        public void ConvertParameter_Bool()
        {
            // Array
            var _parameter = Expression.Parameter(typeof(bool), "x");

            // Act
            var _assertedExpression = _converter.Convert(_parameter);
            var _lambdaExpression =
                Expression.Lambda<Func<bool, bool>>(_assertedExpression, new ParameterExpression[] {_parameter});
            var _assertedLambda = _lambdaExpression.Compile();

            // Assert
            Assert.AreEqual(true, _assertedLambda(true));
        }

        [Test]
        public void ConvertProperty_String()
        {
            // Array
            var _testClass = new TestClass()
                {StringValue = "AOEuI", IntValue = 1, DoubleValue = 2.232, BoolValue = true};
            var _parameter = Expression.Parameter(typeof(TestClass), "x");

            var _property = Expression.Property(_parameter, nameof(TestClass.StringValue));

            // Act
            var _assertedExpression = _converter.Convert(_property);
            var _lambdaExpression = Expression.Lambda<Func<TestClass, string>>(_assertedExpression,
                new ParameterExpression[] {_parameter});
            var _assertedLambda = _lambdaExpression.Compile();

            // Assert
            Assert.AreEqual(_testClass.StringValue, _assertedLambda(_testClass));
        }

        [Test]
        public void ConvertProperty_Int()
        {
            // Array
            var _testClass = new TestClass()
                {StringValue = "AOEuI", IntValue = 1, DoubleValue = 2.232, BoolValue = true};
            var _parameter = Expression.Parameter(typeof(TestClass), "x");

            var _property = Expression.Property(_parameter, nameof(TestClass.IntValue));

            // Act
            var _assertedExpression = _converter.Convert(_property);
            var _lambdaExpression = Expression.Lambda<Func<TestClass, int>>(_assertedExpression,
                new ParameterExpression[] {_parameter});
            var _assertedLambda = _lambdaExpression.Compile();

            // Assert
            Assert.AreEqual(_testClass.IntValue, _assertedLambda(_testClass));
        }

        [Test]
        public void ConvertProperty_Double()
        {
            // Array
            var _testClass = new TestClass()
                {StringValue = "AOEuI", IntValue = 1, DoubleValue = 2.232, BoolValue = true};
            var _parameter = Expression.Parameter(typeof(TestClass), "x");

            var _property = Expression.Property(_parameter, nameof(TestClass.DoubleValue));

            // Act
            var _assertedExpression = _converter.Convert(_property);
            var _lambdaExpression = Expression.Lambda<Func<TestClass, double>>(_assertedExpression,
                new ParameterExpression[] {_parameter});
            var _assertedLambda = _lambdaExpression.Compile();

            // Assert
            Assert.AreEqual(_testClass.DoubleValue, _assertedLambda(_testClass));
        }

        [Test]
        public void ConvertProperty_Bool()
        {
            // Array
            var _testClass = new TestClass()
                {StringValue = "AOEuI", IntValue = 1, DoubleValue = 2.232, BoolValue = true};
            var _parameter = Expression.Parameter(typeof(TestClass), "x");

            var _property = Expression.Property(_parameter, nameof(TestClass.BoolValue));

            // Act
            var _assertedExpression = _converter.Convert(_property);
            var _lambdaExpression = Expression.Lambda<Func<TestClass, bool>>(_assertedExpression,
                new ParameterExpression[] {_parameter});
            var _assertedLambda = _lambdaExpression.Compile();

            // Assert
            Assert.AreEqual(_testClass.BoolValue, _assertedLambda(_testClass));
        }
    }
}
