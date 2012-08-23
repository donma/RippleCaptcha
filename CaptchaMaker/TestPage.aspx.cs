using System;
using RippleCaptcha;

namespace RippleCaptcher
{
    /// <summary>
    /// Demo CaptchaMaker.Helper
    /// </summary>
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Helper helper=new Helper();
            var result = helper.GetCaptcha("ABCDE", "Verdana", Server.MapPath("bk.jpg"), 100, 40, 20, 12);

            result.Save(Server.MapPath("s1.jpg"));

            Image1.ImageUrl = "s1.jpg";
        }
    }
}