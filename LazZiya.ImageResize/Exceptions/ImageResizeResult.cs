namespace LazZiya.ImageResize.Exceptions
{
    /// <summary>
    /// Image resize result object
    /// </summary>
    public class ImageResizeResult
    {
        /// <summary>
        /// Resize result status, true for success, false for failure
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// String message value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Specify failure reason
        /// </summary>
        public FailureReasonType Reason { get; set; }
    }
}
