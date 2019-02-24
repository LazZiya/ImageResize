using System.ComponentModel.DataAnnotations;

namespace LazZiya.ImageResize
{
    /// <summary>
    /// if the cropped image is larger than the new image size 
    /// then select the spot to crop
    /// </summary>
    public enum TargetSpot
    {
        [Display(Name = "Top left")]
        TopLeft,

        [Display(Name = "Top middle")]
        TopMiddle,

        [Display(Name = "Top right")]
        TopRight,

        [Display(Name = "Middle left")]
        MiddleLeft,

        [Display(Name = "Center")]
        Center,

        [Display(Name = "Middle right")]
        MiddleRight,

        [Display(Name = "Bottom left")]
        BottomLeft,

        [Display(Name = "Bottom middle")]
        BottomMiddle,

        [Display(Name = "Bottom right")]
        BottomRight,
    }
}
