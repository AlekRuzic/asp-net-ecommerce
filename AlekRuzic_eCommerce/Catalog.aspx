<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="AlekRuzic_eCommerce.catalog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Catalog</title>
    <link href="Styles/Catalog.css" rel="stylesheet" />
</head>
<body>
    <div id="wrap">
    <form id="catalogForm" runat="server">
        <asp:Table ID="catalogueTable" runat="server"  Height="123px" Width="660px" CssClass="catalogTable">
            <asp:TableRow style="font-weight:bold; text-decoration: underline">
                <asp:TableCell>Product Id:</asp:TableCell>
                <asp:TableCell>Description</asp:TableCell>
                <asp:TableCell>Image</asp:TableCell>
                <asp:TableCell>Quantity On Hand</asp:TableCell>
                <asp:TableCell>Price</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>        </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="catalogButton" runat="server" Text="Button" style="visibility:hidden" OnClick="AddToCart_Click" />
        <br />
        <br />
        <asp:Label ID="lblRowSelected" runat="server" CssClass="Labels" Text="...select a button" ></asp:Label>
    
        <asp:Panel ID="Panel1" runat="server">
            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                <asp:Label ID="lblMessages" CssClass="lblMessages" runat="server" Height="25px" Width="70%"></asp:Label>
            </div>
            </asp:Panel>
    
    </form>   
    </div>
</body>
</html>
