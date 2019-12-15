namespace LazZiya.ImageResize.ColorFormats
{
    /// <summary>
    /// Image pixel formats
    /// </summary>
    public enum ImagePixelFormat
    {
        /// <summary>
        /// Indexed
        /// </summary>
        PixelFormatIndexed = 0x00010000,

        /// <summary>
        /// 32bit CMYK
        /// </summary>
        PixelFormat32bppCMYK = 0x200F,

        /// <summary>
        /// 16bit Gtrayscale
        /// </summary>
        PixelFormat16bppGrayScale = (4 | (16 << 8))
    }
}
