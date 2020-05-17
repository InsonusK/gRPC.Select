using System.Linq.Expressions;

namespace gRPC.Select.Interface
{
    /// <summary>
    /// Expression builder of property converter
    /// </summary>
    public interface IValueConverter
    {
        /// <summary>
        /// Convert property
        /// </summary>
        /// <param name="memberExpression">Property expression</param>
        /// <returns></returns>
        Expression Convert(MemberExpression memberExpression);
        /// <summary>
        /// Convert property
        /// </summary>
        /// <param name="parameterExpression">Property expression</param>
        /// <returns></returns>
        Expression Convert(ParameterExpression parameterExpression);
    }
}
