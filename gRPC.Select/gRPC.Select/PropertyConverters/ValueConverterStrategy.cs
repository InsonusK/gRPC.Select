using gRPC.Select.Interface;
using GRPC.Selector.Enum;

namespace gRPC.Select.PropertyConverters
{
    public class ValueConverterStrategy : IValueConverterStrategy
    {
        public IValueConverter GetExpressionBuilder(Converter converter)
        {
            return converter switch
            {
                Converter.ToLower => new ToLowerCase(),
                Converter.ToUpper => new ToUpperCase(),
                Converter.Non => new NullConverter()
            };
        }
    }
}
