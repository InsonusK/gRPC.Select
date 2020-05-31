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
    public class SelectorCondition_Test
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
                .GetService(Arg.Is(typeof(IConditionAdapter<TestSelectRequestStruct>)))
                .Returns((callback) => _adapter);
        }

        [TearDown]
        public void TearDown()
        {
            _dbInit.CleanupDB();
        }

        [Test]
        public void SelectSingle()
        {
            // Array
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _selectRequest = new TestSelectRequestStruct
            {
                WhereSimple = new SelectCondition {Condition = CompareCondition.Eq, Value = "2", PropertyName = nameof(DataModel.Id)}
            };
            var _expectedResult = _context.Table.Single(t => t.Id == 2);

            // Act
            var _assertedResult = _selector.Apply(_context.Table.AsQueryable(), _selectRequest).ToArray();

            // Assert
            Assert.AreEqual(1, _assertedResult.Count());
            Assert.AreEqual(_expectedResult, _assertedResult[0]);
        }

        [Test]
        public void SelectMany()
        {
            // Array
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _selectRequest = new TestSelectRequestStruct
            {
                Where = new SelectConditionPack
                {
                    SelectConditions =
                    {
                        new SelectCondition {Condition = CompareCondition.Eq, Value = "2", PropertyName = nameof(DataModel.Id)},
                        new SelectCondition {Condition = CompareCondition.Eq, Value = "3", PropertyName = nameof(DataModel.Id)}
                    },
                    JoinCondition = LogicCondition.Or
                }
            };
            var _expectedResult = _context.Table.Where(t => t.Id == 2 || t.Id == 3).ToArray();

            // Act
            var _assertedResult = _selector.Apply(_context.Table.AsQueryable(), _selectRequest).ToArray();

            // Assert
            Assert.IsTrue(DataModel.CompareArr(_expectedResult, _assertedResult));
        }

        [Test]
        public void SelectManyWithSubConditions()
        {
            // Array
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _filterByString = new SelectConditionPack
            {
                SelectConditions =
                {
                    new SelectCondition {Condition = CompareCondition.Eq, Value = "2", PropertyName = nameof(DataModel.StringValue)},
                    new SelectCondition {Condition = CompareCondition.Eq, Value = "3", PropertyName = nameof(DataModel.StringValue)}
                },
                JoinCondition = LogicCondition.Or
            };
            var _filterById = new SelectConditionPack
            {
                SelectConditions =
                {
                    new SelectCondition {Condition = CompareCondition.Eq, Value = "2", PropertyName = nameof(DataModel.Id)},
                    new SelectCondition {Condition = CompareCondition.Eq, Value = "3", PropertyName = nameof(DataModel.Id)}
                },
                JoinCondition = LogicCondition.Or
            };
            var _selectRequest = new TestSelectRequestStruct
            {
                Where = new SelectConditionPack
                {
                    SelectConditions =
                    {
                        new SelectCondition
                        {
                            Condition = CompareCondition.Ge, Value = "2", PropertyName = nameof(DataModel.DoubleValue)
                        },
                        new SelectCondition
                        {
                            Condition = CompareCondition.Lt, Value = "3.6", PropertyName = nameof(DataModel.FloatValue)
                        }
                    },
                    SelectConditionPacks = {_filterByString, _filterById},
                    JoinCondition = LogicCondition.And
                }
            };
            var _expectedResult = _context.Table.Where(t => t.Id == 2 || t.Id == 3).ToArray();

            // Act
            var _assertedResult = _selector.Apply(_context.Table.AsQueryable(), _selectRequest).ToArray();

            // Assert
            Assert.IsTrue(DataModel.CompareArr(_expectedResult, _assertedResult));
        }

        [Test]
        public void SelectManyNegative()
        {
            // Array
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _selectRequest = new TestSelectRequestStruct
            {
                Where = new SelectConditionPack
                {
                    SelectConditions =
                    {
                        new SelectCondition {Condition = CompareCondition.Lt, Value = "2", PropertyName = nameof(DataModel.Id)},
                        new SelectCondition {Condition = CompareCondition.Gt, Value = "3", PropertyName = nameof(DataModel.Id)}
                    },
                    JoinCondition = LogicCondition.Or,
                    Not = true
                }
            };
            var _expectedResult = _context.Table.Where(t => t.Id == 2 || t.Id == 3).ToArray();

            // Act
            var _assertedResult = _selector.Apply(_context.Table.AsQueryable(), _selectRequest).ToArray();

            // Assert
            Assert.IsTrue(DataModel.CompareArr(_expectedResult, _assertedResult));
        }

        [Test]
        public void SelectSingleAfterConvert()
        {
            // Array
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _selectRequest = new TestSelectRequestStruct
            {
                WhereSimple = new SelectCondition
                {
                    Condition = CompareCondition.Eq, Value = "aoeui", PropertyName = nameof(DataModel.StringValue),
                    Converter = Converter.ToLower
                }
            };
            var _expectedResult = _context.Table.Single(t => t.StringValue.ToLower() == "aoeui");

            // Act
            var _assertedResult = _selector.Apply(_context.Table.AsQueryable(), _selectRequest).ToArray();

            // Assert
            Assert.AreEqual(1, _assertedResult.Count());
            Assert.AreEqual(_expectedResult, _assertedResult[0]);
        }

        [Test]
        public void Empty_SelectCondition()
        {
            // Array
            using var _context = new DBContext(_dbInit.DbContextOptions);
            ISelector _selector = new Select.Selector(_serviceProvider);
            var _selectRequest = new TestSelectRequestStruct();
            var _expectedResult = _context.Table.ToArray();

            // Act
            var _assertedResult = _selector.Apply(_context.Table.AsQueryable(), _selectRequest).ToArray();

            // Assert
            Assert.IsTrue(DataModel.CompareArr(_expectedResult, _assertedResult));
        }
    }
}
