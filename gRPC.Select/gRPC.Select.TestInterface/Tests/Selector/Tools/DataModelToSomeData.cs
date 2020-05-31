using System;
using System.Linq.Expressions;
using gRPC.Select.Interface;
using gRPC.Select.TestDB.TestTools;

namespace gRPC.Select.TestInterface.Tests.Selector.Tools
{
    public class DataModelToSomeData:IModelConverter<DataModel,SomeData>
    {
        public Expression<Func<DataModel, SomeData>> Expression { get; } = (data) => new SomeData(){Id = data.Id,Value = data.StringValue};
    }
}
