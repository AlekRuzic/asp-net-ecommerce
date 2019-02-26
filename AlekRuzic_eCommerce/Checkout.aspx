<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="AlekRuzic_eCommerce.Checkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/Checkout.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <iframe id="CustomerFrame" class="CheckoutFrame" src="Customers.aspx" runat="server">
        </iframe>
        <iframe id="DetailFrame" class="CheckoutFrame" src="Details.aspx" runat="server">
        </iframe>
        </div>
    </form>
</body>
</html>
