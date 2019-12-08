using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace LazZiya.ImageResize.Tools
{
    /// <summary>
    /// Change image opacity
    /// </summary>
    // https://stackoverflow.com/a/8843188/5519026
    public abstract class ImageOpacity
    {

        /// <summary>
        /// Change the opacity of an image, this method loops through all image pixels and changes the opacity
        /// </summary>
        /// <param name="originalImage">The original image</param>
        /// <param name="opacity">Opacity, where 100 is no opacity, 00 is full transparency, 100 full color</param>
        /// <returns>The changed image</returns>
        public static Image ChangeImageOpacityMethod1(Image originalImage, int opacity)
        {

            if ((originalImage.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed)
            {
                // Cannot modify an image with indexed colors
                return originalImage;
            }

            Bitmap bmp = (Bitmap)originalImage.Clone();

            // Specify a pixel format.
            PixelFormat pxf = PixelFormat.Format32bppArgb;

            // Lock the bitmap's bits.
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            int bytesPerPixel = 4;

            // Declare an array to hold the bytes of the bitmap.
            // This code is specific to a bitmap with 32 bits per pixels 
            // (32 bits = 4 bytes, 3 for RGB and 1 byte for alpha).
            int numBytes = bmp.Width * bmp.Height * bytesPerPixel;
            byte[] argbValues = new byte[numBytes];

            // Copy the ARGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, argbValues, 0, numBytes);

            // Manipulate the bitmap, such as changing the
            // RGB values for all pixels in the the bitmap.
            for (int counter = 0; counter < argbValues.Length; counter += bytesPerPixel)
            {
                // argbValues is in format BGRA (Blue, Green, Red, Alpha)

                // If 100% transparent, skip pixel
                if (argbValues[counter + bytesPerPixel - 1] == 0)
                    continue;

                int pos = 0;
                pos++; // B value
                pos++; // G value
                pos++; // R value

                argbValues[counter + pos] = (byte)(argbValues[counter + pos] * ((double)opacity / 100));
            }

            // Copy the ARGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, ptr, numBytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        /// <summary>  
        /// method for changing the opacity of an image, this method uses ImageAttributes matrix to change the opacity.
        /// </summary>  
        /// <param name="image">image to set opacity on</param>  
        /// <param name="opacity">opacity 0 full transparent, 100 no opacity</param>  
        /// <returns></returns>  
        public static Image ChangeImageOpacityMethod2(Image image, int opacity)
        {
            //create a Bitmap the size of the image provided  
            Bitmap bmp = new Bitmap(image.Width, image.Height);

            //create a graphics object from the image  
            using (Graphics gfx = Graphics.FromImage(bmp))
            {

                //create a color matrix object  

                //create image attributes  
                using (var attributes = new ImageAttributes())
                {
                    ColorMatrix matrix = new ColorMatrix();

                    if (opacity < 100)
                    {
                        // clear undesired border
                        gfx.Clear(Color.White);
                    }

                    //set the opacity  
                    matrix.Matrix33 = (float)opacity / 100;

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return bmp;
        }
    }
}
