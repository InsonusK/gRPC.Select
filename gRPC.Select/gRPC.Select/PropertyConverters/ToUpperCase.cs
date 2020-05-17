using System.Linq.Expressions;
using gRPC.Select.Exceptions;
using gRPC.Select.Interface;

namespace gRPC.Select.PropertyConverters
{
    public class ToUpperCase : IValueConverter
    {
        public Expression Convert(MemberExpression memberExpression)
        {
            if (memberExpression.Type != typeof(string))
            {
                throw new ConverterException(
                    $"Property {memberExpression.Member.Name} couldn't be convert to upper case");
            }

            return Expression(memberExpression);
        }

        public Expression Convert(ParameterExpression parameterExpression)
        {
            if (parameterExpression.Type != typeof(string))
            {
                throw new ConverterException(
                    $"Parameter {parameterExpression.Name} couldn't be convert to upper case");
            }

            return Expression(parameterExpression);
        }

        private static Expression Expression(Expression memberExpression)
        {
            return System.Linq.Expressions.Expression.Call(memberExpression,
                typeof(string).GetMethod("ToUpper", System.Type.EmptyTypes));
        }
    }
}
