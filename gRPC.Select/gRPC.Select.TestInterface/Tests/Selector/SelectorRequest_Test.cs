using System;
using System.Linq;
using gRPC.Select.Exceptions;
using gRPC.Select.Interface;
using gRPC.Select.TestDB.TestTools;
using NSubstitute;
using NUnit.Framework;
using Test;

namespace gRPC.Select.TestInterface.Tests.Selector
{
    public class SelectorRequest_Test
    {
        private DBInit _dbInit;
        private IServiceProvider _serviceProvider;

        [SetUp]
        public void OnTimeSetup()
        {
            _dbInit = new DBInit(this.GetType().Name);
            _dbInit.InitDB();
            _serviceProvider = Substitute.For<IServiceProvider>();
        }

        [TearDown]
        public void TearDown()
        {
            _dbInit.CleanupDB();
        }

        [Test]
        public void WrongStream()
        {
            // Array
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _selectRequest = new Another();
            var _expectedResult = _context.Table.ToArray();

            // Act
            var _exception = Assert.Catch<ConditionException>(() =>
                _selector.Apply(_context.Table.AsQueryable(), _selectRequest).ToArray());

            // Assert
            Assert.AreEqual("An expected type of condition message", _exception.Message);
        }
    }
}
