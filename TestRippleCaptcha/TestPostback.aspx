<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPostback.aspx.cs" Inherits="TestRippleCaptcha.TestPostback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <img alt="captcha" ID="Image1" runat="server" src="" />

            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
            <br />
            <asp:TextBox ID="txtCaptcha" runat="server"></asp:TextBox>
            <asp:Button ID="btnCheck" runat="server" Text="Check" OnClick="btnCheck_Click" />
            <br />
            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
