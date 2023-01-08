# LazZiya.ImageResize
Image resizing tool for .Net applications, with support to add text/image watermark.
This package is built on .NetStandard 2.0 so it supports wide range of compatible platforms (e.g. Asp.Net Core etc).
### Docs
https://docs.ziyad.info

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
    var tOps = new TextWatermarkOptions
    {
        // Change text color and opacity
        // Text opacity range depends on Color's alpha channel (0 - 255)
        TextColor = Color.FromArgb(50, Color.White),
        
        // Add text outline
        // Outline color opacity range depends on Color's alpha channel (0 - 255)
        OutlineColor = Color.FromArgb(255, Color.Black)
    };
    
    img.AddTextWatermark("http://ziyad.info", tOps)
       .SaveAs(@"wwwroot\images\new-image.jpg");
}
````

### Add image watermark and change location, opacity, ...etc.
`AddImageWatermark` method accepts argument of type [`ImageWatermarkOptions`][3] that allows to specify watermark position etc.
````cs
using(var img = Image.FromFile(@"wwwroot\images\image-file.jpg"))
{
    var iOps = new ImageWatermarkOptions
    {
        // Change image opacity (0 - 100)
        Opacity = 50,
        
        // Change image watermark location
        Location = TargetSpot.BottomRight
    };
    
    img.AddImageWatermark(@"wwwroot\images\logo.png", iOps)
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

goto project website: http://ziyad.info/en/articles/29-LazZiya_ImageResize

## License
https://github.com/LazZiya/ImageResize/blob/master/LICENSE

[4]: https://github.com/LazZiya/ImageResize/releases/
