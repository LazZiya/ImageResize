namespace LazZiya.ImageResize
{
    /// <summary>
    /// Use target spot to specify the reading sport of the source image, 
    /// if the cropped image is larger than the new image size 
    /// then select the spot to crop.
    /// <para>The TargetSpot pre-defines 9 main spots in the image, 3 columns and 3 rows.
    /// it helps easily select which area to read from the image for resize and crop.</para>
    /// </summary>
    //  ______________________________________
    // |            |            |            |
    // | TopLeft    | TopMiddle  | TopRight   |
    // |____________|____________|____________|
    // |            |            |            |
    // | MiddleLeft |  Center    | MiddleRight|
    // |____________|____________|____________|
    // |            |            |            |
    // | BottomLeft |BottomMiddle|BottomRight |
    // |____________|____________|____________|

    public enum TargetSpot
    {
        /// <summary>
        /// Takes the top left area of the image
        /// </summary>
        TopLeft,

        /// <summary>
        /// Takes the top middle area of the image
        /// </summary>
        TopMiddle,

        /// <summary>
        /// Takes the top right area
        /// </summary>
        TopRight,

        /// <summary>
        /// Takes the middle left area
        /// </summary>
        MiddleLeft,

        /// <summary>
        /// Takes the center area of the image
        /// </summary>
        Center,

        /// <summary>
        /// Takes the middle right area of the image
        /// </summary>
        MiddleRight,

        /// <summary>
        /// Takes the bottom left area of the image
        /// </summary>
        BottomLeft,
        
        /// <summary>
        /// Takes the bottom middle area of the image
        /// </summary>
        BottomMiddle,

        /// <summary>
        /// Takes the bottom left area of the image
        /// </summary>
        BottomRight,
    }
}
