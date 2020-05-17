using System.Linq;
using GRPC.Selector;

namespace gRPC.Select.Interface
{
    /// <summary>
    /// Select IQueryable object using gRPC select condition
    /// </summary>
    public interface ISelector
    {
        /// <summary>
        /// Select from IQueryable using pack of conditions
        /// </summary>
        /// <param name="queryableData">IQueryable for selection</param>
        /// <param name="selectConditionPack">pack of select conditions</param>
        /// <typeparam name="TModel">Model in IQueryable</typeparam>
        /// <returns></returns>
        IQueryable<TModel> Apply<TModel>(IQueryable<TModel> queryableData, SelectConditionPack selectConditionPack);

        /// <summary>
        /// Select from IQueryable using single condition
        /// </summary>
        /// <param name="queryableData">IQueryable for selection</param>
        /// <param name="selectCondition">Select condition</param>
        /// <typeparam name="TModel">Model in IQueryable</typeparam>
        /// <returns></returns>
        IQueryable<TModel> Apply<TModel>(IQueryable<TModel> queryableData, SelectCondition selectCondition);

        /// <summary>
        /// Select from IQueryable using select request
        /// </summary>
        /// <param name="queryableData">IQueryable for selection</param>
        /// <param name="selectRequest">Select condition</param>
        /// <typeparam name="TModel">Model in IQueryable</typeparam>
        /// <returns></returns>
        IQueryable<TModel> Apply<TModel>(IQueryable<TModel> queryableData, SelectRequest selectRequest);
    }
}
