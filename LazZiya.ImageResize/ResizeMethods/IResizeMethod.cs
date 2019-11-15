using System.Drawing;

namespace LazZiya.ImageResize.ResizeMethods
{
    /// <summary>
    /// Common properties for image resize methods
    /// </summary>
    public interface IResizeMethod
    {
        /// <summary>
        /// The source image size and position
        /// </summary>
        Rectangle SourceRect { get; }

        /// <summary>
        /// The target image size and position
        /// </summary>
        Rectangle TargetRect { get; }
    }
}
