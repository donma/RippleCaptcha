using System;

namespace TestRippleCaptcha
{
    public partial class TestAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            lblResult.Text = (Session["CAPTCHA"].ToString().ToUpper() == txtCaptcha.Text.Trim().ToUpper()).ToString();
        }
    }
}