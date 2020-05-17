using System;
using System.Linq.Expressions;
using gRPC.Select.Exceptions;
using gRPC.Select.Interface;
using gRPC.Select.PropertyConverters;
using NUnit.Framework;

namespace gRPC.Select.Test.PropertyConverters
{
    [TestFixture]
    public class ToUpperCase_Test
    {
        class TestClass
        {
            public string Value { get; set; }
            public int ErrorValue { get; set; }
        }

        private IValueConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new ToUpperCase();
        }

        [Test]
        public void ConvertParameter()
        {
            // Array
            var _parameter = Expression.Parameter(typeof(string), "x");

            // Act
            var _assertedExpression = _converter.Convert(_parameter);
            var _lambdaExpression =
                Expression.Lambda<Func<string, string>>(_assertedExpression, new ParameterExpression[] {_parameter});
            var _assertedLambda = _lambdaExpression.Compile();

            // Assert
            Assert.AreEqual("AOEUI", _assertedLambda("AOeuI"));
        }

        [Test]
        public void ConvertProperty()
        {
            // Array
            var _testClass = new TestClass() {Value = "AOeuI"};
            var _parameter = Expression.Parameter(typeof(TestClass), "x");

            var _property = Expression.Property(_parameter, nameof(_testClass.Value));

            // Act
            var _assertedExpression = _converter.Convert(_property);
            var _lambdaExpression = Expression.Lambda<Func<TestClass, string>>(_assertedExpression,
                new ParameterExpression[] {_parameter});
            var _assertedLambda = _lambdaExpression.Compile();

            // Assert
            Assert.AreEqual("AOEUI", _assertedLambda(_testClass));
        }

        [Test]
        public void Check_parameter_type()
        {
            // Array
            var _parameter = Expression.Parameter(typeof(int), "x");

            // Act

            // Assert
            Assert.Catch<ConverterException>(() => _converter.Convert(_parameter));
        }

        [Test]
        public void Check_property_type()
        {
            // Array
            var _parameter = Expression.Parameter(typeof(TestClass), "x");
            var _property = Expression.Property(_parameter, nameof(TestClass.ErrorValue));

            // Act

            // Assert
            Assert.Catch<ConverterException>(() => _converter.Convert(_property));
        }
    }
}
