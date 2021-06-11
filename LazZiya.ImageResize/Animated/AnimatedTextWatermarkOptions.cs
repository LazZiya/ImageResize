namespace LazZiya.ImageResize.Animated
{
    /// <summary>
    /// Define options for animated text
    /// </summary>
    public class AnimatedTextWatermarkOptions
    {
        /// <summary>
        /// Pre-defined text animations
        /// </summary>
        public TextAnimation TextAnimation { get; set; } = TextAnimation.Typing;
    }

    /// <summary>
    /// Pre defined text animations
    /// </summary>
    public enum TextAnimation
    {
        /// <summary>
        /// increment char-by-char
        /// </summary>
        Typing = 0
    }
}
