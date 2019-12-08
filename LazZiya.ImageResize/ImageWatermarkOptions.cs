using System.ComponentModel.DataAnnotations;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Define options for adding image watermark over the image, like margin, opacity, and location.
    /// </summary>
    public class ImageWatermarkOptions
    {
        /// <summary>
        /// Margin in pixels. Depends on watermark location. default value 10
        /// </summary>
        public int Margin { get; set; } = 10;

        /// <summary>
        /// The location to draw the image watermark. Choose from pre-defined 9 main locations (3 cols, 3 rows).
        /// Default value TargetSpot.TopRight.
        /// See <see cref="TargetSpot"/>
        /// </summary>
        public TargetSpot Location { get; set; } = TargetSpot.TopRight;

        /// <summary>
        /// Set opacity value of the image watermark (0 - 100). 
        /// Default value 100 full color.
        /// </summary>
        [Range(0, 100)]
        public int Opacity { get; set; } = 100;
    }
}
