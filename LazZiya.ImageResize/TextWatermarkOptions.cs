using System.Drawing;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Define options for adding text watermark over the image, like text color, opacity, text outline, etc.
    /// </summary>
    public class TextWatermarkOptions
    {
        /// <summary>
        /// Value for the text color. Use alpha channel to specify transparency
        /// </summary>
        public Color TextColor { get; set; } = Color.FromArgb(150, Color.White);

        /// <summary>
        /// Font size in pixel
        /// </summary>
        public int FontSize { get; set; } = 24;

        /// <summary>
        /// Font style
        /// </summary>
        public FontStyle FontStyle { get; set; } = FontStyle.Regular;

        /// <summary>
        /// Font family
        /// </summary>
        public string FontName { get; set; } = "Arial";

        /// <summary>
        /// Value for the text background color. Use alpha channel to specify transparency
        /// Set transparency to 0 to remove background.
        /// </summary>
        public Color BGColor { get; set; } = Color.FromArgb(0, Color.White);

        /// <summary>
        /// Top/Bottom margin in pixels. Depends on watermark horizontal alignment.
        /// </summary>
        public int Margin { get; set; } = 10;

        /// <summary>
        /// The location to draw the text watermark. Choose from pre-defined 9 main locations (3 cols, 3 rows)
        /// </summary>
        public TargetSpot Location { get; set; } = TargetSpot.BottomLeft;

        /// <summary>
        /// Value for the text outline color. Use alpha channel to specify transparency.
        /// Set transparency to 0 to remove outline.
        /// </summary>
        public Color OutlineColor { get; set; } = Color.FromArgb(200, Color.Black);

        /// <summary>
        /// Text outline width in pixels
        /// </summary>
        public float OutlineWidth { get; set; } = 3.5f;
    }
}
