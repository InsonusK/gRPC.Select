using GRPC.Selector.Enum;

namespace gRPC.Select.Interface
{
    /// <summary>
    /// Repository of property converters
    /// </summary>
    public interface IValueConverterStrategy
    {
        /// <summary>
        /// Get value converter
        /// </summary>
        /// <param name="converter">Converter</param>
        /// <returns></returns>
        IValueConverter GetExpressionBuilder(Converter converter);
    }
}
