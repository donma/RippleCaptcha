using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Security;
using RippleCaptcha;

namespace TestRippleCaptcha
{
    public partial class CaptchaCreator : System.Web.UI.Page
    {
        private string CurrentCaptcha
        {
            get { return Session["CAPTCHA"].ToString(); }
            set { Session["CAPTCHA"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            
            
            CurrentCaptcha = FormsAuthentication.HashPasswordForStoringInConfigFile(Guid.NewGuid().ToString(), "MD5").Substring(0, 4);
            Helper helper=new Helper();

            var result = helper.GetCaptcha(CurrentCaptcha, "Verdana", Server.MapPath("bk.jpg"), 95, 65, 22, 7);

            //設定 ContentType 為 jpg圖片
            // Set the ContentType to jpg picture.
            Response.ContentType = "image/jpeg";
            //注意這邊要用writefile 其中帶入圖片路徑
            byte[] data = null;

            using (MemoryStream oMemoryStream = new MemoryStream())
            {

                //儲存圖片到 MemoryStream 物件，並且指定儲存影像之格式
                //Save image to MemoryStream  and set it to jpeg format.
                result.Save(oMemoryStream, ImageFormat.Jpeg);
                //設定資料流位置
                //Set stream position start from zero
                oMemoryStream.Position = 0;
                //設定 buffer 長度
                //Set buffer length
                data = new byte[oMemoryStream.Length];
                //將資料寫入 buffer
                //Wrire data to buffer
                oMemoryStream.Read(data, 0, Convert.ToInt32(oMemoryStream.Length));
                //將所有緩衝區的資料寫入資料流
                //Flush memory.
                oMemoryStream.Flush();

            }
            //將buffer 中的stream全部送出
            Response.BinaryWrite(data);
            Response.Flush();

        }
    }
}