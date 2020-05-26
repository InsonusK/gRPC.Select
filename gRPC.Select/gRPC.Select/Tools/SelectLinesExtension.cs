using GRPC.Selector;

namespace gRPC.Select.Tools
{
    public static class SelectLinesExtension
    {
        public static bool NotNullOrEmpty(this SelectLines SelectLines)
        {
            return SelectLines != null &&
                   (SelectLines.From > 0 || SelectLines.Till > 0);
        }
    }
}
