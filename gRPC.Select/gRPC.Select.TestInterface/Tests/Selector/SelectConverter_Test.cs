using System;
using System.Linq;
using System.Reflection;
using gRPC.Select.Adapter;
using gRPC.Select.Interface;
using gRPC.Select.TestDB.TestTools;
using gRPC.Select.TestInterface.Tests.Selector.Tools;
using NSubstitute;
using NUnit.Framework;
using Test.Selector;

namespace gRPC.Select.TestInterface.Tests.Selector
{
    [TestFixture]
    public class SelectConverter_Test
    {
        private DBInit _dbInit;
        private IServiceProvider _serviceProvider;
        private BaseProtoAdapter<TestSelectRequestStruct> _adapter = new BaseProtoAdapter<TestSelectRequestStruct>();
        private IModelConverter<DataModel, SomeData> _converter = new DataModelToSomeData();

        [SetUp]
        public void OnTimeSetup()
        {
            _dbInit = new DBInit(this.GetType().Name);
            _dbInit.InitDB();
            _serviceProvider = Substitute.For<IServiceProvider>();
            _serviceProvider
                .GetService(Arg.Is(typeof(IConditionAdapter<TestSelectRequestStruct>)))
                .Returns((callback) => _adapter);
        }

        [TearDown]
        public void TearDown()
        {
            _dbInit.CleanupDB();
        }

        [Test]
        public void Select_using_ModelConverter_from_ServiceCollection()
        {
            // Array
            _serviceProvider
                .GetService(Arg.Is(typeof(IModelConverter<DataModel, SomeData>)))
                .Returns((info => _converter));
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _selectRequest = new TestSelectRequestStruct
            {
                WhereSimple = new SelectCondition
                    {Condition = CompareCondition.Eq, Value = "2", PropertyName = nameof(DataModel.Id)}
            };
            var _expectedResult = _context.Table.Single(t => t.Id == 2);

            // Act
            var _assertedResult = _selector
                .Apply<DataModel, SomeData, TestSelectRequestStruct>(_context.Table.AsQueryable(), _selectRequest)
                .ToArray();

            // Assert
            Assert.AreEqual(1, _assertedResult.Count());
            Assert.AreEqual(_expectedResult.Id, _assertedResult[0].Id);
            Assert.AreEqual(_expectedResult.StringValue, _assertedResult[0].Value);
        }
        [Test]
        public void Select_using_ModelConverterExpression_from_parameters()
        {
            // Array
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _selectRequest = new TestSelectRequestStruct
            {
                WhereSimple = new SelectCondition
                    {Condition = CompareCondition.Eq, Value = "2", PropertyName = nameof(DataModel.Id)}
            };
            var _expectedResult = _context.Table.Single(t => t.Id == 2);

            // Act
            var _assertedResult = _selector
                .Apply(_context.Table.AsQueryable(), _selectRequest,new DataModelToSomeData().Expression)
                .ToArray();

            // Assert
            Assert.AreEqual(1, _assertedResult.Count());
            Assert.AreEqual(_expectedResult.Id, _assertedResult[0].Id);
            Assert.AreEqual(_expectedResult.StringValue, _assertedResult[0].Value);
        }
    }
}
