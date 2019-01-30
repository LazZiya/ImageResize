namespace LazZiya.ImageResize
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

    public enum ResizeMethod
    {
        // don't resize
        None,

        /// <summary>
        /// resized image will contain the whole original image
        /// the image will be scaled till W & H fits the new size, no pixels will be cut, 
        /// final result will have original image aspect ratio
        /// </summary>
        Contain,

        /// <summary>
        /// resized image will have excactly the defined new dimentions
        /// original image will be scaled to fit W or H which fits first, then extra pixels will be cropped
        /// result will have new image aspect ratio
        /// </summary>
        Crop,

        /// <summary>
        /// the image will not be scaled down,
        /// crop operation will be applied without scaling original image
        /// </summary>
        SpotCrop
    }

    /// <summary>
    /// if the cropped image is larger than the new image size 
    /// then select the spot to crop
    /// </summary>
    public enum CroppingSpot
    {
        TopLeft,
        TopMiddle,
        TopRight,
        MiddleLeft,
        Center,
        MiddleRight,
        BottomLeft,
        BottomMiddle,
        BottomRight
    }
}
