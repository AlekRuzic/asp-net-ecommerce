<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="AlekRuzic_eCommerce._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E-Commerce Page</title>
    <link href="Styles/default.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="topbar" class="buttonsBar" align="center">
            <asp:Panel ID="ControlPanel" runat="server">
                <asp:Button ID="BtnCatalog" class="buttons" runat="server" Text="Catalog" OnClick="BtnCatalog_Click" />
                <asp:Button ID="BtnCart" class="buttons" runat="server" Text="Cart" OnClick="BtnCart_Click" />
                <asp:Button ID="BtnCustomers" class="buttons" runat="server" Text="Customer Maintenance" OnClick="BtnCustomers_Click" />
                <asp:Button ID="BtnProducts" class="buttons" runat="server" Text="Product Maintenance" OnClick="BtnProducts_Click" />
                <asp:Button ID="BtnPromoPage" class="buttons" runat="server" Text="Promotions" OnClick="BtnPromoPage_Click" /> 
            </asp:Panel>  
        </div>
        <div id="frame" align="center" class="iFrameDiv">
             <iframe id="containerFrame" class="containerFrame" src="" runat="server"
                    style="left:10px; top:80px; height: 400px; width: 800px;">

                </iframe>
        </div>
     
    </form>
</body>
</html>
