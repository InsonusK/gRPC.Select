using System.Linq;
using Google.Protobuf;

namespace gRPC.Select.Interface
{
    /// <summary>
    /// Select IQueryable object using gRPC select condition
    /// </summary>
    public interface ISelector
    {
        /// <summary>
        /// Select from IQueryable using select request
        /// </summary>
        /// <param name="queryableData">IQueryable for selection</param>
        /// <param name="conditionMessage">Select condition</param>
        /// <typeparam name="TModel">Model in IQueryable</typeparam>
        /// <typeparam name="TConditionMessage">ProtoMessage Type</typeparam>
        /// <returns></returns>
        IQueryable<TModel> Apply<TModel, TConditionMessage>(IQueryable<TModel> queryableData,
            TConditionMessage conditionMessage);
    }
}
