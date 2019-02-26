<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="AlekRuzic_eCommerce.Cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="cartForm" runat="server">
        <asp:Table ID="cartTable" runat="server" CssClass="CellStyle" Height="123px" Width="567px">
        </asp:Table>
        <asp:Button ID="Button1" runat="server" Text="Button" style="visibility:hidden" OnClick="RemoveFromCart_Click" />
        <br />
        <br />
                <asp:Label ID="LblTotal" runat="server" CssClass="LabelTotal" Text="0.00"></asp:Label>
        <br />
        <asp:Button ID="btnRecalculate" runat="server" Text="Total" OnClick="btnRecalculate_Click" />
        <asp:Button ID="btnCheckout" runat="server" Text="Checkout" OnClick="btnCheckout_Click" />
   </form>
</body>
</html>
