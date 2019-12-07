using System.ComponentModel.DataAnnotations;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Define options for adding text watermark over the image, like text color, opacity, text outline, etc.
    /// </summary>
    public class ImageWatermarkOptions
    {
        /// <summary>
        /// Margin in pixels. Depends on watermark location.
        /// </summary>
        public int Margin { get; set; } = 10;

        /// <summary>
        /// The location to draw the image watermark. Choose from pre-defined 9 main locations (3 cols, 3 rows)
        /// </summary>
        public TargetSpot Location { get; set; } = TargetSpot.BottomLeft;

        /// <summary>
        /// Set opacity value of the image watermark
        /// </summary>
        [Range(0, 100)]
        public int Opacity { get; set; }
    }
}
