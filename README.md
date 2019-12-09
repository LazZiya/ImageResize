# LazZiya.ImageResize
Image resizing tool for .Net applications, with support to add text/image watermark.
This package is built on .NetStandard 2.0 so it supports wide range of compatible platforms (e.g. Asp.Net Core etc).

### Project site (documentation to be updated soon...):
http://ziyad.info/en/articles/29-LazZiya_ImageResize

### Samples (to be updated soon...):
http://demo.ziyad.info/en/ImageResize

## Latest version
 - v3.0.0-preview1
 - Release date 08 Sep 2019
 - [Release notes][4]

## Installation:

Install via nuget (enable Include prerelease to check for latest test versions):

````
Install-Package LazZiya.ImageResize
````
### Resize image file
````cs
using System.Drawing;
using LazZiya.ImageResize;

using(var img = Image.FromFile(@"wwwroot\images\image-file.jpg"))
{
    img.ScaleByWidth(600)
       .SaveAs(@"wwwroot\images\resized-image.jpg");
}
````

### Add  text watermark and change color, opacity, ...etc.
`AddTextWatermark` method accepts argument of type [`TextWaterMarkOptions`][2] that allows to customize the text.

To change opacity of the text/outline/background just use the relevant `Color` with specified **alpha** value (0 - 255), 0 full opacity, 255 full color.

````cs
using(var img = Image.FromFile(@"wwwroot\images\image-file.jpg"))
{
    var twmOps = new TextWatermarkOptions
    {
        // Change text color and opacity
        // Text opacity range depends on Color's alpha channel (0 - 255)
        TextColor = Color.FromArgb(50, Color.White),
        
        // Add text outline
        // Outline color opacity range depends on Color's alpha channel (0 - 255)
        OutlineColor = Color.FromArgb(255, Color.Black)
    };
    
    img.AddTextWatermark("http://ziyad.info", twmOps)
       .SaveAs(@"wwwroot\images\new-image.jpg");
}
````

### Add image watermark and change location, opacity, ...etc.
`AddImageWatermark` method accepts argument of type [`ImageWatermarkOptions`][3] that allows to specify watermark position etc.
````cs
using(var img = Image.FromFile(@"wwwroot\images\image-file.jpg"))
{
    var iwmOps = new ImageWatermarkOptions
    {
        // Change image opacity (0 - 100)
        Opacity = 50,
        
        // Change image watermark location
        Location = TargetSpot.BottomRight
    };
    
    img.AddImageWatermark(@"wwwroot\images\logo.png", iwmOps)
       .SaveAs(@"wwwroot\images\new-image.jpg");
}
````

### Upload and resize an image
All ImageResize methods can be chained together to provide easy image processing. Below sample shows how to handle uploaded files, resize them, add image and text watermarks:

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
img.AddImageWatermark(@"wwwroot\images\logo.png", new ImageWatermarkOptions { ... });

// or
var wm = Image.FromFile(@"wwwroot\images\logo.png");
img.AddImageWatermark(wm);

// or
var wm = Image.FromFile(@"wwwroot\images\logo.png");
img.AddImageWatermark(wm, new ImageWatermarkOptions { ... });
````

## TargetSpot :
Specifies that target spot used for cropping or placing text and image watermarks.
````cs
public enum TargetSpot { TopLeft, TopMiddle, TopRight, MiddleLeft, Center, MiddleRight, BottomLeft, BottomMiddle, BottomRight }
````

## GraphicOptions
Define graphic options to ensure maximum image compatibility and quality. 
See [GraphicOptions][1]

## TextWatermarkOptions
Define text watermark options, like locaiton, color, text outline, etc. See [TextWatermarkOptions][2]

## ImageWatermarkOptions
Define image watermark option, lie location, opacity and margin. See [ImageWatermarkOptions][3]


goto project website: http://ziyad.info/en/articles/29-LazZiya_ImageResize

## License
https://github.com/LazZiya/ImageResize/blob/master/LICENSE

[1]: LazZiya.ImageResize/GraphicOptions.cs
[2]: LazZiya.ImageResize/TextWatermarkOptions.cs
[3]: LazZiya.ImageResize/ImageWatermarkOptions.cs
[4]: https://github.com/LazZiya/ImageResize/releases/tag/v3.0.0-preview1
