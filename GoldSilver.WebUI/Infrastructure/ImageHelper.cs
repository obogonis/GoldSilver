using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace GoldSilver.WebUI.Infrastructure
{
    public static class ImageHelper
    {
        public static byte[] AddWatermark(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image image = Image.FromStream(ms);

            using (Image watermarkImage = Image.FromFile(@"C:\Users\obo\Documents\Visual Studio 2012\Projects\GoldSilver\GoldSilver.WebUI\Content\img\watermark.png"))
            using (Graphics imageGraphics = Graphics.FromImage(image))
            using (TextureBrush watermarkBrush = new TextureBrush(watermarkImage))
            {
                int x = (image.Width / 5 - watermarkImage.Width / 5);
                int y = (image.Height / 5 - watermarkImage.Height / 5);
                watermarkBrush.TranslateTransform(x, y);
                imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), new Size(watermarkImage.Width + 1, watermarkImage.Height)));
                
                ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }
}