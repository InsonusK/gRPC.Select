using System.Linq.Expressions;
using gRPC.Select.Interface;

namespace gRPC.Select.PropertyConverters
{
    public class NullConverter : IValueConverter
    {
        public Expression Convert(MemberExpression memberExpression)
        {
            return memberExpression;
        }

        public Expression Convert(ParameterExpression parameterExpression)
        {
            return parameterExpression;
        }
    }
}