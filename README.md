# LazZiya.ImageResize
Image resizing tool for ASP.Net Core 2.x with support to add text water mark 

## Project site:
http://ziyad.info/en/articles/29-LazZiya_ImageResize

## Installation:

Install via nuget :

````
Install-Package LazZiya.ImageResize -Version 1.0.0
````

### Upload and resize an image
Handling uploaded files and resizing the images:
````
foreach (var file in Request.Form.Files)
{
    if (file.Length > 0)
    {        
        using (var stream = file.OpenReadStream())
        {
            var img = ImageResize.Resize(stream, 800, 600, ResizeMethod.Contain, TargetSpot.Center);

            img.SaveAs($"wwwroot\\images\\{file.FileName}");
        }
    }
}
````

### add text watermark to the uploaded image
Below code will draw a colored text with a transparent background in the bottom left corner of the uploaded image:

````
img.TextWatermark(text: "Watermark Text", 
                  color: "#DDAA9955", 
                  bgColor: "#55AA9955", 
                  fontFamily: "Calibri", 
                  fontSize: 24, 
                  spot: TargetSpot.BottomLeft, 
                  style: FontStyle.Italic, 
                  stick: false);
                  
img.SaveAs($"wwwroot\\images\\{file.FileName}");
````

goto project website: http://ziyad.info/en/articles/29-LazZiya_ImageResize

## License
https://github.com/LazZiya/ImageResize/blob/master/LICENSE
