using System.Linq.Expressions;

namespace gRPC.Select.Interface
{
    /// <summary>
    /// Expression builder of compare condition
    /// </summary>
    public interface ICompareCondition
    {
        /// <summary>
        /// Build math expression
        /// </summary>
        /// <param name="left">Left side</param>
        /// <param name="right">Right side</param>
        /// <returns></returns>
        BinaryExpression Build(Expression left, Expression right);
    }
}
