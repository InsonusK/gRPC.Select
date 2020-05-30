using GRPC.Selector;

namespace gRPC.Select.Tools
{
    public static class SelectLinesExtension
    {
        public static bool NotNullOrEmpty(this SelectLines selectLines)
        {
            return selectLines != null &&
                   (selectLines.From > 0 || selectLines.Till > 0);
        }
    }
}
