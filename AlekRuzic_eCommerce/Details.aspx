<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="AlekRuzic_eCommerce.Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Table ID="detailsTable" Height="150px" Width="300px" BorderStyle="Solid" runat="server"></asp:Table>
            <asp:Label ID="LblTotal" runat="server" CssClass="LabelTotal" Text="0.00"></asp:Label><br />
            <asp:CheckBox ID="ChkMailingList" runat="server" AutoPostback="false" Text="Add me to the Mailing List" OnCheckedChanged="ChkMailingList_CheckedChanged"/><br />
            <asp:Button ID="BtnPay" runat="server" Text="Pay for my Order" CssClass="Buttons" style="left:10px;bottom:20px" Enabled="True" OnClick="BtnPay_Click"/>
            </div>
    </form>
</body>
</html>
