using System;
using System.Linq;
using gRPC.Select.Adapter;
using gRPC.Select.Interface;
using gRPC.Select.TestDB.TestTools;
using NSubstitute;
using NUnit.Framework;
using Test.Selector;

namespace gRPC.Select.TestInterface.Tests.Selector
{
    [TestFixture]
    public class SelectLines_Test
    {
        private DBInit _dbInit;
        private IServiceProvider _serviceProvider;
        private BaseProtoAdapter<TestSelectRequestStruct> _adapter = new BaseProtoAdapter<TestSelectRequestStruct>();

        [SetUp]
        public void OnTimeSetup()
        {
            _dbInit = new DBInit(this.GetType().Name);
            _dbInit.InitDB();
            _serviceProvider = Substitute.For<IServiceProvider>();
            _serviceProvider
                .GetService(
                    Arg.Is<Type>((type =>
                        type == typeof(IConditionAdapter<TestSelectRequestStruct>))))
                .Returns((callback) => _adapter);
        }

        [TearDown]
        public void TearDown()
        {
            _dbInit.CleanupDB();
        }

        [Test]
        public void SelectAll_NullSelectLines()
        {
            // Array
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _selectRequest = new TestSelectRequestStruct
            {
                WhereSimple =
                    new SelectCondition {Condition = CompareCondition.Eq, Value = "many", PropertyName = nameof(DataModel.StringValue)},
            };
            var _expectedResult = _context.Table.Where(t => t.StringValue == "many").ToArray();

            // Act
            var _assertedResult = _selector.Apply(_context.Table.AsQueryable(), _selectRequest).ToArray();

            // Assert
            Assert.IsTrue(DataModel.CompareArr(_expectedResult, _assertedResult));
        }

        [Test]
        public void SelectAll_with_empty()
        {
            // Array
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _selectRequest = new TestSelectRequestStruct
            {
                WhereSimple =
                    new SelectCondition {Condition = CompareCondition.Eq, Value = "many", PropertyName = nameof(DataModel.StringValue)},
                Lines = new SelectLines()
            };
            var _expectedResult = _context.Table.Where(t => t.StringValue == "many").ToArray();

            // Act
            var _assertedResult = _selector.Apply(_context.Table.AsQueryable(), _selectRequest).ToArray();

            // Assert
            Assert.IsTrue(DataModel.CompareArr(_expectedResult, _assertedResult));
        }

        [Test]
        public void SelectFrom3()
        {
            // Array
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _selectRequest = new TestSelectRequestStruct
            {
                WhereSimple =
                    new SelectCondition {Condition = CompareCondition.Eq, Value = "many", PropertyName = nameof(DataModel.StringValue)},
                Lines = new SelectLines {From = 3}
            };
            var _expectedResult = _context.Table
                .Where(t => t.StringValue == "many" && t.DoubleValue >= 13).ToArray();
            // .Where(t => t.StringValue == "many").Where((model, i) => i > 2).ToArray();

            // Act
            var _assertedResult = _selector.Apply(_context.Table.AsQueryable(), _selectRequest).ToArray();

            // Assert
            Assert.IsTrue(DataModel.CompareArr(_expectedResult, _assertedResult));
        }

        [Test]
        public void SelectTill3()
        {
            // Array
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _selectRequest = new TestSelectRequestStruct
            {
                WhereSimple =
                    new SelectCondition {Condition = CompareCondition.Eq, Value = "many", PropertyName = nameof(DataModel.StringValue)},
                Lines = new SelectLines {Till = 3}
            };
            var _expectedResult = _context.Table
                .Where(t => t.StringValue == "many" && t.DoubleValue <= 13).ToArray();

            // Act
            var _assertedResult = _selector.Apply(_context.Table.AsQueryable(), _selectRequest).ToArray();

            // Assert
            Assert.IsTrue(DataModel.CompareArr(_expectedResult, _assertedResult));
        }

        [Test]
        public void SelectFrom3Till7()
        {
            // Array
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _selectRequest = new TestSelectRequestStruct
            {
                WhereSimple =
                    new SelectCondition {Condition = CompareCondition.Eq, Value = "many", PropertyName = nameof(DataModel.StringValue)},
                Lines = new SelectLines {From = 3, Till = 7}
            };
            var _expectedResult = _context.Table
                .Where(t => t.StringValue == "many" && t.DoubleValue >= 13 && t.DoubleValue <= 17).ToArray();

            // Act
            var _assertedResult = _selector.Apply(_context.Table.AsQueryable(), _selectRequest).ToArray();

            // Assert
            Assert.IsTrue(DataModel.CompareArr(_expectedResult, _assertedResult));
        }
    }
}
