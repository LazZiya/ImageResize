namespace LazZiya.ImageResize.Exceptions
{
    public enum FailureReasonType
    {
        None,
        EncoderNotFound,
        ExtensionNotSupported,
        UnknownImageFormatGuid,
        ZeroSizeNotAllowed,
        GraphicsException
    }
}
