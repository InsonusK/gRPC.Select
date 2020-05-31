using System;
using System.Linq.Expressions;

namespace gRPC.Select.Interface
{
    public interface IModelConverter<TDbModel, TReturnModel>
    {
        Expression<Func<TDbModel,TReturnModel>> Expression { get; }
    }
}
