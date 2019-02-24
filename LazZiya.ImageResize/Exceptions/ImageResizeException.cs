using System;

namespace LazZiya.ImageResize.Exceptions
{
    public class ImageResizeException : Exception
    {
        public ImageResizeResult Result { get; }

        public ImageResizeException(ImageResizeResult result) : base(BuildMessage(result))
        {
            Result = result;
        }

        private static string BuildMessage(ImageResizeResult result)
        {
            switch (result.Reason)
            {
                case FailureReasonType.EncoderNotFound:
                    return $"Can't find image encoder type for extension {result.Value}.";

                case FailureReasonType.ZeroSizeNotAllowed:
                    return $"New image size WxH must be larger than zero.";

                case FailureReasonType.GraphicsException:
                    return $"Can't draw graphics image. see result: {result.Value}.";

                case FailureReasonType.ExtensionNotSupported:
                    return $"Extension not supported: {result.Value}.";

                case FailureReasonType.UnknownImageFormatGuid:
                    return $"unknown image format guid: {result.Value}.";

                case FailureReasonType.None:
                default:
                    return $"Unknown exception during image resize.";
            }
        }
    }
}
