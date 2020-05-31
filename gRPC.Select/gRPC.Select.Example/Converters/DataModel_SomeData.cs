using System;
using System.Linq.Expressions;
using Example;
using gRPC.Select.Interface;
using gRPC.Select.TestDB.TestTools;

namespace GrpcService.Example.Converters
{
    public class DataModel_SomeData : IModelConverter<DataModel, SomeData>
    {
        public Expression<Func<DataModel, SomeData>> Expression { get; } = (data) => new SomeData()
            {Id = data.Id, Value = data.StringValue};
    }
}
