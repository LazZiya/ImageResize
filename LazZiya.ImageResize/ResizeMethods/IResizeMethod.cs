using System.Drawing;

namespace LazZiya.ImageResize.ResizeMethods
{
    public interface IResizeMethod
    {
        Rectangle SourceRect { get; }
        Rectangle TargetRect { get; }
    }
}
