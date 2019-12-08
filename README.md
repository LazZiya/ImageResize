# LazZiya.ImageResize
Image resizing tool for .Net applications, with support to add text/image watermark.
This package is built on .NetStandard 2.0 so it supports wide range of compatible platforms (e.g. Asp.Net Core etc).

### Project site:
http://ziyad.info/en/articles/29-LazZiya_ImageResize

### Samples:
http://demo.ziyad.info/en/ImageResize

## Installation:

Install via nuget (enable Include prerelease to check for latest test versions):

````
Install-Package LazZiya.ImageResize
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
            using(var img = Image.FromStream(stream))
            {
                img.ScaleAndCrop(800, 600)
                .AddImageWatermark(@"wwwroot\images\icon.png")
                .AddTextWatermark("http://ziyad.info")
                .SaveAs($"wwwroot\\images\\{file.FileName}");
            }
        }
    }
}
````

### Supported resizing methods
All resizing methods will return a `System.Drawing.Image` file that can be saved in any supported image format (JPG, PNG, etc.)

- Scale :
Auto scales image by width or height, and keeps aspect ratio same as original image
````cs 
img.Scale(800, 600);

// or 
img.Scale(800, 600, new GraphicOptions { ... });
````

- Scale by width :
Scales image by provided width value, auto adjusts new height according to aspect ratio.
````cs
img.ScaleByWidth(800);

// or 
img.ScaleByWidth(800, new GraphicOptions { ... });
````

- Scale by height :
Scales image by provided height value, auto adjusts new width according to aspect ratio.
````cs
img.ScaleByHeight(600);

// or 
img.ScaleByHeight(600, new GraphicOptions { ... });
````

- Scale and crop :
Scalesthe image to fit new width or new height (which fits first), then crops out the rest of the image.
````cs
img.ScaleAndCrop(800, 600);

// or
img.ScaleAndCrop(800, 600, TargetSpot.Center);

// or
img.ScaleAndCrop(800, 600, new GraphicOptions { ... });

// or
img.ScaleAndCrop(800, 600, new GraphicOptions { ... }, TargetSpot.Center);
````

- Crop :
Directly crop a specified spot of the image, without scaling.
````cs 
img.Crop(800, 600);

// or
img.Crop(800, 600, TargetSpot.Center);

// or
img.Crop(800, 600, new GraphicOptions { ... });

// or
img.Crop(800, 600, new GraphicOptions { ... }, TargetSpot.Center);
````

## Adding Watermark
ImageResize supports adding text and image watermarks, both can be placed to any specified spot with ability to change opacity of the text or the image.

### Add text watermark to the uploaded image
Below code will draw a colored text with a transparent background in the bottom left corner of the uploaded image:

````cs
img.AddTextWatermark("http://ziyad.info");

// or
img.AddTextWatermark("http://ziyad.info", new TextWatermarkOptions { ... });
````

### Add image watermark and adjust opacity :
````cs
img.AddImageWatermark(@"wwwroot\images\logo.png");

// or
img.AddImageWatermark(@"wwwroot\images\logo.png", new ImageWatermarkOption { ... });

// or
var wm = Image.FromFile(@"wwwroot\images\logo.png");
img.AddImageWatermark(wm);

// or
var wm = Image.FromFile(@"wwwroot\images\logo.png");
img.AddImageWatermark(wm, new ImageWatermarkOption { ... });
````

## TargetSpot :
Specifies that target spot used for cropping or placing text and image watermarks.
````cs
public enum TargetSpot { TopLeft, TopMiddle, TopRight, MiddleLeft, Center, MiddleRight, BottomLeft, BottomMiddle, BottomRight }
````

## GraphicOptions
Define graphic options to ensure maximum image compatibility and quality.

## TextWatermarkOptions
Define text watermark options, like locaiton, color, text outline, etc.

## ImageWatermarkOptions
Define image watermark option, lie location, opacity and margin.


goto project website: http://ziyad.info/en/articles/29-LazZiya_ImageResize

## License
https://github.com/LazZiya/ImageResize/blob/master/LICENSE
