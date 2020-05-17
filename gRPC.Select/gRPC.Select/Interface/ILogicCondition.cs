using System.Linq.Expressions;

namespace gRPC.Select.Interface
{
    /// <summary>
    /// Expression builder of logic condition
    /// </summary>
    public interface ILogicCondition
    {
        /// <summary>
        /// Build logic expression
        /// </summary>
        /// <param name="left">Left side</param>
        /// <param name="right">Right side</param>
        /// <returns></returns>
        BinaryExpression Build(Expression left, Expression right);
        /// <summary>
        /// Get begin expression.
        /// Usefull when use for/foreach to solve problems with first record
        /// </summary>
        /// <returns></returns>
        Expression Start();
    }
}
