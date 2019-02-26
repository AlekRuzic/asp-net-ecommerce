<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PromoPage.aspx.cs" Inherits="AlekRuzic_eCommerce.PromoPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:350px; margin:0 auto">
            <div style="width:150px; float:left"><h1>The iphone 7 is currently on sale for $600! Get one 
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">now</asp:LinkButton>!</h1></div>
            <asp:Image ID="iphone7" style="height:200px; width:200px" runat="server" />
        </div>
    </form>
</body>
</html>
