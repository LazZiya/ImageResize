using System.Drawing;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Define options for adding text watermark over the image, like text color, opacity, text outline, etc.
    /// </summary>
    public class TextWatermarkOptions
    {
        /// <summary>
        /// Value for the text color. Use alpha channel to specify transparency (0 - 255). 
        /// Set alpha to 0 to remove text color.
        /// Default value Color.FromArgb(255, Color.White) full color.
        /// See <see cref="Color"/>
        /// </summary>
        public Color TextColor { get; set; } = Color.FromArgb(255, Color.White);

        /// <summary>
        /// Font size in pixel.
        /// Default value 24
        /// </summary>
        public int FontSize { get; set; } = 24;

        /// <summary>
        /// Font style. Default value FontStyle.Regular.
        /// See <see cref="FontStyle"/>
        /// </summary>
        public FontStyle FontStyle { get; set; } = FontStyle.Regular;

        /// <summary>
        /// Font family. Default value "Arial"
        /// </summary>
        public string FontName { get; set; } = "Arial";

        /// <summary>
        /// Value for the text background color. Use alpha channel to specify transparency (0 - 255).
        /// Set alpha to 0 to remove background.
        /// Default value Color.FromArgb(0, Color.White) no background.
        /// See <see cref="Color"/>
        /// </summary>
        public Color BGColor { get; set; } = Color.FromArgb(0, Color.White);

        /// <summary>
        /// Top/Bottom margin in pixels. Depends on watermark horizontal alignment.
        /// Default value 10
        /// </summary>
        public int Margin { get; set; } = 10;

        /// <summary>
        /// The location to draw the text watermark. Choose from pre-defined 9 main locations (3 cols, 3 rows).
        /// Default value TargetSpot.BottomLeft.
        /// See <see cref="TargetSpot"/>
        /// </summary>
        public TargetSpot Location { get; set; } = TargetSpot.BottomLeft;

        /// <summary>
        /// Value for the text outline color. Use alpha channel to specify transparency (0 - 255).
        /// Set alpha to 0 to remove outline.
        /// Default value Color.FromArgb(255, Color.Black)
        /// See <see cref="Color"/>
        /// </summary>
        public Color OutlineColor { get; set; } = Color.FromArgb(255, Color.Black);

        /// <summary>
        /// Text outline width in pixels. Default value 3.5f
        /// </summary>
        public float OutlineWidth { get; set; } = 3.5f;
    }
}
