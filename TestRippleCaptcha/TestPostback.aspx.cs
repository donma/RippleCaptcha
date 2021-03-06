﻿using System;
using System.Drawing;
using System.IO;
using System.Web.Security;
using RippleCaptcha;

namespace TestRippleCaptcha
{
    public partial class TestPostback : System.Web.UI.Page
    {

        
        private string CurrentCaptcha
        {
            //存放在Session 中
            get { return Session["CAPTCHA"].ToString(); }
            set { Session["CAPTCHA"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //如果只是postback 就不更新
            if (!IsPostBack)
            {
                //取一個新的GUID 然後取MD5 因為GUID只有到F 並取四碼
                CurrentCaptcha = FormsAuthentication.HashPasswordForStoringInConfigFile(Guid.NewGuid().ToString(), "MD5").Substring(0, 4);
                Helper helper = new Helper();
                var result = helper.GetCaptcha(CurrentCaptcha, "Verdana", Server.MapPath("bk.jpg"), 95, 65, 22, 2);
                Image1.Src = "data:image/" + GetImageFormat(result) + ";base64," + ImageToBase64(result);
            }
        }


        /// <summary>
        /// 更新按下時 充新換Key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {

            ////取一個新的GUID 然後取MD5 因為GUID只有到F 並取四碼
            CurrentCaptcha = FormsAuthentication.HashPasswordForStoringInConfigFile(Guid.NewGuid().ToString(), "MD5").Substring(0, 4);
            Helper helper = new Helper();
            var result = helper.GetCaptcha(CurrentCaptcha, "Verdana", Server.MapPath("bk.jpg"), 95, 65, 22, 2);
            Image1.Src = "data:image/" + GetImageFormat(result) + ";base64," + ImageToBase64(result);

        }

        /// <summary>
        /// 自動判斷圖片格式
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static System.Drawing.Imaging.ImageFormat GetImageFormat(Image img)
        {
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                return System.Drawing.Imaging.ImageFormat.Jpeg;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                return System.Drawing.Imaging.ImageFormat.Bmp;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                return System.Drawing.Imaging.ImageFormat.Png;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Emf))
                return System.Drawing.Imaging.ImageFormat.Emf;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Exif))
                return System.Drawing.Imaging.ImageFormat.Exif;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                return System.Drawing.Imaging.ImageFormat.Gif;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
                return System.Drawing.Imaging.ImageFormat.Icon;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.MemoryBmp))
                return System.Drawing.Imaging.ImageFormat.MemoryBmp;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
                return System.Drawing.Imaging.ImageFormat.Tiff;
            return System.Drawing.Imaging.ImageFormat.Wmf;
        }


        /// <summary>
        /// 將 Image 物件轉 Base64
        /// Convert image to base64
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public string ImageToBase64(Bitmap image)
        {

            MemoryStream ms = new MemoryStream();


            // 將圖片轉成 byte[]
            // Comvert image to byte[]
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imageBytes = ms.ToArray();

            // 將 byte[] 轉 base64
            // byte to base64
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;

        }

        /// <summary>
        /// 按下確認後認證
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            //印出來是否正確
            lblResult.Text = (CurrentCaptcha.ToUpper() == txtCaptcha.Text.Trim().ToUpper()).ToString() + CurrentCaptcha;
        }

    }
}