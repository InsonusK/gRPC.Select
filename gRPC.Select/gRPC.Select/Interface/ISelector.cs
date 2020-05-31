using System;
using System.Linq;
using System.Linq.Expressions;

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

        /// <summary>
        /// Select from IQueryable using select request
        /// </summary>
        /// <param name="queryableData">IQueryable for selection</param>
        /// <param name="conditionMessage">Select condition</param>
        /// <typeparam name="TDbModel">Model in DB IQueryable</typeparam>
        /// <typeparam name="TConditionMessage">ProtoMessage Type</typeparam>
        /// <typeparam name="TReturnModel">Return model</typeparam>
        /// <returns></returns>
        IQueryable<TReturnModel> Apply<TDbModel, TReturnModel, TConditionMessage>(IQueryable<TDbModel> queryableData,
            TConditionMessage conditionMessage);

        /// <summary>
        /// Select from IQueryable using select request
        /// </summary>
        /// <param name="queryableData">IQueryable for selection</param>
        /// <param name="conditionMessage">Select condition</param>
        /// <param name="modelConvertExpression">Expression to convert DB model to return model</param>
        /// <typeparam name="TDbModel">Model in DB IQueryable</typeparam>
        /// <typeparam name="TConditionMessage">ProtoMessage Type</typeparam>
        /// <typeparam name="TReturnModel">Return model</typeparam>
        /// <returns></returns>
        IQueryable<TReturnModel> Apply<TDbModel, TReturnModel, TConditionMessage>(IQueryable<TDbModel> queryableData,
            TConditionMessage conditionMessage, Expression<Func<TDbModel,TReturnModel>> modelConvertExpression);
    }
}
