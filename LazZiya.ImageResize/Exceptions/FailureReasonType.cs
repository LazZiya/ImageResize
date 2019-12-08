namespace LazZiya.ImageResize.Exceptions
{
    /// <summary>
    /// Failure reasone type
    /// </summary>
    public enum FailureReasonType
    {
        /// <summary>
        /// not listed
        /// </summary>
        None,

        /// <summary>
        /// Relevant encoder info not found
        /// </summary>
        EncoderNotFound,

        /// <summary>
        /// File extension is not supported
        /// </summary>
        ExtensionNotSupported,

        /// <summary>
        /// Image file format GUID is unknown
        /// </summary>
        UnknownImageFormatGuid,

        /// <summary>
        /// Zero size error
        /// </summary>
        ZeroSizeNotAllowed,

        /// <summary>
        /// GDI+ related exceptin
        /// </summary>
        GraphicsException
    }
}
