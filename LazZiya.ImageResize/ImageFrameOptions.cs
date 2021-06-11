using System.Drawing;
using System.Drawing.Drawing2D;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// Define options for image frame
    /// </summary>
    public class ImageFrameOptions
    {
        /// <summary>
        /// Frame thikness in pixels. Default is 2
        /// </summary>
        public int Thickness { get; set; } = 2;

        /// <summary>
        /// Frame color. Default is LightGray
        /// </summary>
        public Color FrameColor { get; set; } = Color.LightGray;

        /// <summary>
        /// Fill the space beween the border and the image with a specific coloe. Default is transparent.
        /// </summary>
        public Color FillColor { get; set; } = Color.Transparent;

        /// <summary>
        /// The shapre of the frame.
        /// </summary>
        public ImageFrameShape FrameShape { get; set; } = ImageFrameShape.Rectangle;

        /// <summary>
        /// How the frame corners will be joined. Default is Miter.
        /// </summary>
        public LineJoin LineJoin { get; set; } = LineJoin.Miter;

        /// <summary>
        /// Mitter limit. Default is 10.
        /// </summary>
        public float MitterLimit { get; set; } = 10;

        /// <summary>
        /// Dash style. Default is Solid.
        /// </summary>
        public DashStyle DashStyle { get; set; } = DashStyle.Solid;

        /// <summary>
        /// Define custom dash pattern. Dash style must be set to Custom.
        /// </summary>
        public float[] DashPattern { get; set; } = { 1 };
    }
}
