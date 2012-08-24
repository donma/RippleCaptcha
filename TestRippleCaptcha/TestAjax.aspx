<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestAjax.aspx.cs" Inherits="TestRippleCaptcha.TestAjax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://code.jquery.com/jquery-1.8.0.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <img src="CaptchaCreator.aspx" id="imgCaptcha" />
            <asp:TextBox ID="txtCaptcha" runat="server"></asp:TextBox>
            <input type="button" id="input_Refresh" value="Refresh" />
            <br />

            <asp:Button ID="btnCheck" runat="server" Text="Check" OnClick="btnCheck_Click" />
            <br />
            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>

            <script type="text/javascript">


                $('#input_Refresh').click(function () {
                    $('#imgCaptcha').attr('src', 'CaptchaCreator.aspx?' + RandomNumber(1, 9999));
                });


                function RandomNumber(min, max) {
                    return Math.floor(Math.random() * (max - min + 1) + min);
                }
            </script>
        </div>
    </form>
</body>
</html>
