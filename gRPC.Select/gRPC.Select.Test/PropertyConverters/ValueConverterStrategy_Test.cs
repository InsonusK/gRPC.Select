using System;
using gRPC.Select.Interface;
using gRPC.Select.PropertyConverters;
using GRPC.Selector;
using NUnit.Framework;

namespace gRPC.Select.Test.PropertyConverters
{
    [TestFixture]
    public class ValueConverterStrategy_Test
    {
        private IValueConverterStrategy _strategy;

        [SetUp]
        public void SetUp()
        {
            _strategy = new ValueConverterStrategy();
        }

        [Test]
        [TestCase(typeof(ToLowerCase), Converter.ToLower)]
        [TestCase(typeof(ToUpperCase), Converter.ToUpper)]
        [TestCase(typeof(NullConverter), Converter.Non)]
        public void Match(Type propertyConverterBuilderType, Converter converter)
        {
            // Array

            // Act
            var _expressionBuilder = _strategy.GetExpressionBuilder(converter);

            // Assert
            Assert.IsInstanceOf(propertyConverterBuilderType, _expressionBuilder);
        }


    }
}
