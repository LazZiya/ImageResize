namespace LazZiya.ImageResize.Exceptions
{
    public class ImageResizeResult
    {
        public bool Success { get; set; }
        public string Value { get; set; }

        public FailureReasonType Reason { get; set; }
    }
}
