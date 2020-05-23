using System.Linq;
using gRPC.Select.Interface;
using gRPC.Select.Test.TestTools;
using GRPC.Selector;
using NUnit.Framework;

namespace gRPC.Select.Test.Tests.Selector
{
    [TestFixture]
    public class SelectLines_Test
    {
        private DBInit _dbInit;

        [SetUp]
        public void OnTimeSetup()
        {
            _dbInit = new DBInit(this.GetType().Name);
            _dbInit.InitDB();
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
            ISelector _selector = new Select.Selector();
            var _selectRequest = new SelectRequest()
            {
                SelectCondition =
                    new SelectCondition()
                        {Condition = CompareCondition.Eq, Value = "many", PropertyName = nameof(DataModel.StringValue)},
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
            ISelector _selector = new Select.Selector();
            var _selectRequest = new SelectRequest()
            {
                SelectCondition =
                    new SelectCondition()
                        {Condition = CompareCondition.Eq, Value = "many", PropertyName = nameof(DataModel.StringValue)},
                SelectLines = new SelectLines()
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
            ISelector _selector = new Select.Selector();
            var _selectRequest = new SelectRequest()
            {
                SelectCondition =
                    new SelectCondition()
                        {Condition = CompareCondition.Eq, Value = "many", PropertyName = nameof(DataModel.StringValue)},
                SelectLines = new SelectLines(){From = 3}
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
            ISelector _selector = new Select.Selector();
            var _selectRequest = new SelectRequest()
            {
                SelectCondition =
                    new SelectCondition()
                        {Condition = CompareCondition.Eq, Value = "many", PropertyName = nameof(DataModel.StringValue)},
                SelectLines = new SelectLines(){Till = 3}
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
            ISelector _selector = new Select.Selector();
            var _selectRequest = new SelectRequest()
            {
                SelectCondition =
                    new SelectCondition()
                        {Condition = CompareCondition.Eq, Value = "many", PropertyName = nameof(DataModel.StringValue)},
                SelectLines = new SelectLines(){From = 3, Till = 7}
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
