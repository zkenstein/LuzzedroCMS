﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace LuzzedroCMS.Infrastructure.Concrete
{
    public class ImageModifier
    {
        public Image ResizeImage(HttpPostedFileBase httpPostedFileBase, int maxWidth, int maxHeight)
        {
            Image img = Image.FromStream(httpPostedFileBase.InputStream, true, true);
            return _ResizeImage(img, maxWidth, maxHeight);
        }

        public Image ResizeImage(Image img, int maxWidth, int maxHeight)
        {
            return _ResizeImage(img, maxWidth, maxHeight);

        }

        private Image _ResizeImage(Image img, int maxWidth, int maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                Double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(img.Width / ratio);
                int nny = (int)Math.Floor(img.Height / ratio);
                Bitmap cpy = new Bitmap(nnx, nny, PixelFormat.Format32bppArgb);
                using (Graphics gr = Graphics.FromImage(cpy))
                {
                    gr.Clear(Color.Transparent);

                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = InterpolationMode.High;

                    gr.DrawImage(img,
                        new Rectangle(0, 0, nnx, nny),
                        new Rectangle(0, 0, img.Width, img.Height),
                        GraphicsUnit.Pixel);
                }
                return cpy;
            }
        }
    }
}