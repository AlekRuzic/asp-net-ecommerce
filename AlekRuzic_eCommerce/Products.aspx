<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="AlekRuzic_eCommerce.Products" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Products Page</title>
    <link href="Styles/Products.css" rel="stylesheet" />
    <style type="text/css">

    </style>
</head>
<body>
    <form id="productsForm" runat="server">

        <div id="containerDiv">
           <table id="productsTable">
            <tr><td><asp:Label ID="lblProduct" CssClass="Labels" runat="server" Text="Product ID"></asp:Label></td>
                <td><asp:TextBox ID="txtProductId" runat="server"></asp:TextBox></tr>
            <tr><td><asp:Label ID="lblManufact" CssClass="Labels" runat="server" Text="Manufact. Code"></asp:Label> </td>
                <td><asp:TextBox ID="txtManufac" runat="server"></asp:TextBox></tr>
            <tr><td><asp:Label ID="lblDesc" CssClass="Labels" runat="server" Text="Description"></asp:Label></td>
                <td><asp:TextBox ID="txtDesc" runat="server"></asp:TextBox></tr> 
            <tr><td><asp:Label ID="lblPic" CssClass="Labels" runat="server" Text="Picture"></asp:Label></td>
                <td><asp:TextBox ID="txtPic" runat="server"></asp:TextBox></tr>
            <tr><td><asp:Label ID="lblQOH" CssClass="Labels" runat="server" Text="QOH"></asp:Label></td>
                <td><asp:TextBox ID="txtQOH" runat="server"></asp:TextBox></tr>
            <tr><td><asp:Label ID="lblPrice" CssClass="Labels" runat="server" Text="Price"></asp:Label></td>
                <td><asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td></tr>
            </table>

            <asp:Image ID="imageBox" runat="server" />  
            <asp:ListBox ID="listbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="listbox_SelectedIndexChanged">
            </asp:ListBox>
        </div> 
          <div id="buttonsDiv">
                <asp:Button ID="newButton" CssClass="button" runat="server" Text="New" OnClick="newButton_Click" />
                <asp:Button ID="addButton" CssClass="button" runat="server" Text="Add" OnClick="addButton_Click" />
                <asp:Button ID="updateButton" CssClass="button" runat="server" Text="Update" OnClick="updateButton_Click" />
                <asp:Button ID="deleteButton" CssClass="button" runat="server" Text="Delete" OnClick="deleteButton_Click" />
                <asp:Button ID="findButton" CssClass="button" runat="server" Text="Find" OnClick="findButton_Click" />
          </div>

         <asp:Panel ID="Panel1" runat="server">
            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                <asp:Label ID="lblMessages" CssClass="lblMessages" runat="server" Height="25px" Width="70%"></asp:Label>
            </div>
            </asp:Panel>
    </form>
    
</body>
</html>
