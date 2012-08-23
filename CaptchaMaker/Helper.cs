using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace RippleCaptcha
{
    public class Helper
    {


        /// <summary>
        /// 製作Captcha
        /// </summary>
        /// <param name="text">文字</param>
        /// <param name="fontName">字體名稱</param>
        /// <param name="backgroundImagePath">採樣的背景圖</param>
        /// <param name="width">結果的圖片寬度</param>
        /// <param name="height">結果的圖片高度</param>
        /// <param name="fontSize"> 字體大小</param>
        /// <param name="waterEffect">增加漣漪複雜度 </param>
        /// <returns></returns>
        public Bitmap GetCaptcha(string text, string fontName, string backgroundImagePath, int width, int height, int fontSize, short waterEffect = 10)
        {

            var backImage = new Bitmap(backgroundImagePath);

            //如果原圖過小 就以原圖大小為主
            if (backImage.Width < width || backImage.Height < height)
            {
                throw new Exception("結果尺寸不得小於採樣大小的圖片");
            }

            //隨機取一個位置 並且按造欲需求的大小裁切
            //請注意隨機採樣範圍 記得為 圖片寬度-欲取的寬度 高度也是 ，不然會發生裁切超過範圍
            backImage =
               backImage.Clone(
                   new Rectangle(new Random().Next(0, backImage.Width - width),
                                 new Random().Next(0, backImage.Height - height), width, height), backImage.PixelFormat);

            Graphics graphics = Graphics.FromImage(backImage);

            //合成的文字為黑色
            Brush br = new SolidBrush(Color.Black);



            //將字體畫上圖片
            //字體的大小 為 自訂
            graphics.DrawString(text, new Font(new FontFamily(fontName), fontSize), br, new PointF(0, 0));

            //增加複雜度
            backImage = AdjustRippleEffect(backImage, waterEffect);

            return backImage;

        }




        /// <summary>
        /// 水波紋效果
        /// </summary>
        /// <param name="src"></param>
        /// <param name="nWave">坡度</param>
        /// <returns></returns>
        public Bitmap AdjustRippleEffect(Bitmap src, short nWave)
        {

            int nWidth = src.Width;
            int nHeight = src.Height;

            // 透過公式進行水波紋的採樣

            FloatPoint[,] fp = new FloatPoint[nWidth, nHeight];

            Point[,] pt = new Point[nWidth, nHeight];

            Point mid = new Point();
            mid.X = nWidth / 2;
            mid.Y = nHeight / 2;

            double newX, newY;
            double xo, yo;

            //先取樣將水波紋座標跟RGB取出
            for (int x = 0; x < nWidth; ++x)
                for (int y = 0; y < nHeight; ++y)
                {
                    xo = (nWave * Math.Sin(2.0 * 3.1415 * y / 128.0));
                    yo = (nWave * Math.Cos(2.0 * 3.1415 * x / 128.0));

                    newX = (x + xo);
                    newY = (y + yo);

                    if (newX > 0 && newX < nWidth)
                    {
                        fp[x, y].X = newX;
                        pt[x, y].X = (int)newX;
                    }
                    else
                    {
                        fp[x, y].X = 0.0;
                        pt[x, y].X = 0;
                    }


                    if (newY > 0 && newY < nHeight)
                    {
                        fp[x, y].Y = newY;
                        pt[x, y].Y = (int)newY;
                    }
                    else
                    {
                        fp[x, y].Y = 0.0;
                        pt[x, y].Y = 0;
                    }
                }


            //進行合成
            Bitmap bSrc = (Bitmap)src.Clone();

            // 依照 Format24bppRgb 每三個表示一 Pixel 0: 藍 1: 綠 2: 紅
            BitmapData bitmapData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite,
                                           PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite,
                                             PixelFormat.Format24bppRgb);

            int scanline = bitmapData.Stride;

            IntPtr scan0 = bitmapData.Scan0;
            IntPtr srcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)scan0;
                byte* pSrc = (byte*)(void*)srcScan0;

                int nOffset = bitmapData.Stride - src.Width * 3;

                int xOffset, yOffset;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        xOffset = pt[x, y].X;
                        yOffset = pt[x, y].Y;

                        if (yOffset >= 0 && yOffset < nHeight && xOffset >= 0 && xOffset < nWidth)
                        {
                            p[0] = pSrc[(yOffset * scanline) + (xOffset * 3)];
                            p[1] = pSrc[(yOffset * scanline) + (xOffset * 3) + 1];
                            p[2] = pSrc[(yOffset * scanline) + (xOffset * 3) + 2];
                        }

                        p += 3;
                    }
                    p += nOffset;
                }
            }



            src.UnlockBits(bitmapData);
            bSrc.UnlockBits(bmSrc);

            return src;
        }

        private struct FloatPoint
        {
            public double X { get; set; }
            public double Y { get; set; }
        }




    }
}