# LazZiya.ImageResize
Image resizing tool for ASP.Net Core 2.x with support to add text water mark 

## Project site:
http://ziyad.info/en/articles/29-LazZiya_ImageResize

## Installation:

Install via nuget :

````
Install-Package LazZiya.ImageResize -Version 2.0.0
````

### Upload and resize an image
Handling uploaded files and resizing the images:
````cs
using System.Drawing;
using LazZiya.ImageResize;

foreach (var file in Request.Form.Files)
{
    if (file.Length > 0)
    {        
        using (var stream = file.OpenReadStream())
        {
            var uploadedImage = System.Drawing.Image.FromStream(stream);
            
            var img = ImageResize.Scale(uploadedImage, 800, 600); // returns System.Drawing.Image file

            img.SaveAs($"wwwroot\\images\\{file.FileName}");
        }
    }
}
````

### Supported resizing methods
All resizing methods will return a `System.Drawing.Image` file that can be saved in any supported image format (JPG, PNG, etc.)

- Scale :
Auto scales image by width or height, and keeps aspect ratio same as original image
````cs 
var img = ImageResize.Scale(uploadedImage, 800, 600);
````

- Scale by width :
Scales image by provided width value, auto adjusts new height according to aspect ratio.
````cs
var img = ImageResize.ScaleByWidth(uploadedImage, 800);
````

- Scale by height :
Scales image by provided height value, auto adjusts new width according to aspect ratio.
````cs
var img = ImageResize.ScaleByHeight(uploadedImage, 600);
````

- Scale and crop :
Scalesthe image to fit new width or new height (which fits first), then crops out the rest of the image.
````cs
var img = ImageResize.ScaleAndCrop(uploadedImage, 800, 600, TargetSpot.Center);
````

- Crop :
Directly crop a specified spot of the image, without scaling.
````cs 
var img = ImageResize.Crop(uploadedImage, 800, 600, TargetSpot.Center);
````

## Adding Watermark
ImageResize supports adding text and image watermarks, both can be placed to any specified spot with ability to change opacity of the text or the image.

`TextWatermark` and `ImageWatermark`are extension methods to `System.Drawing.Image` and they are located under `LazZiya.ImageResize.Watermark` namespace.

### Add text watermark to the uploaded image
Below code will draw a colored text with a transparent background in the bottom left corner of the uploaded image:

````cs
using System.Drawing;
using LazZiya.ImageResize;
using LazZiya.ImageResize.Watermark;

img.TextWatermark("http://ziyad.info", 
                  "#DDAA9955",   //font color, hex8 value. DD is for opacity (00 - FF)
                  "#55AA9955",   //background color, hex8 value. 55 is for opacity (00 - FF)
                  "Calibri",     //font family
                  24,            //font size
                  TargetSpot.BottomLeft, //place the text on bottom left spot
                  style: FontStyle.Italic, //font style
                  margin: 10);   //margin from the border
                  
img.SaveAs($"wwwroot\\images\\{file.FileName}");
````

### Add image watermark and adjust opacity :
````cs
using System.Drawing;
using LazZiya.ImageResize;
using LazZiya.ImageResize.Watermark;

img.ImageWatermark("wwwroot\\images\\myimage.png",   //local path to the image watermark
                   TargetSpot.BottomRight, //add the image watermark to the bottom right area of the uploaded image
                   10,                     //keep 10px margin from the borders
                   40);                    //adjust watermark opacity to be 40 (0 - 100)
                  
img.SaveAs($"wwwroot\\images\\{file.FileName}");
````

## TargetSpot :
Specifies that target spot used for cropping or placing text and image watermarks.
````cs
public enum TargetSpot { TopLeft, TopMiddle, TopRight, MiddleLeft, Center, MiddleRight, BottomLeft, BottomMiddle, BottomRight }
````

goto project website: http://ziyad.info/en/articles/29-LazZiya_ImageResize

## License
https://github.com/LazZiya/ImageResize/blob/master/LICENSE
