<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customers.aspx.cs" Inherits="Customers.customers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customers</title>
    <link href="styles/Customers.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <table id="customerForm">
            <tr><td><asp:Label ID="Label1" CssClass="customerLabel" Text="Customer #" runat="server" ></asp:Label></td>
                <td><asp:TextBox ID="txtCustomerNumber" CssClass="TextBoxes" runat="server"></asp:TextBox></td></tr>
            <tr><td><asp:Label ID="Label2" CssClass="customerLabel" Text="First Name" runat="server" ></asp:Label></td>
                <td><asp:TextBox ID="txtFirstName" CssClass="TextBoxes" runat="server"></asp:TextBox></td></tr>
            <tr><td><asp:Label ID="Label3" CssClass="customerLabel" Text="Last Name" runat="server" ></asp:Label></td>
                <td><asp:TextBox ID="txtLastName" CssClass="TextBoxes"  runat="server"></asp:TextBox></td></tr>
            <tr><td><asp:Label ID="Label4" CssClass="customerLabel" Text="Address" runat="server" ></asp:Label></td>
                <td><asp:TextBox ID="txtAddress" CssClass="TextBoxes"  runat="server"></asp:TextBox></td></tr>
            <tr><td><asp:Label ID="Label5" CssClass="customerLabel" Text="City" runat="server" ></asp:Label></td>
                <td><asp:TextBox ID="txtCity" CssClass="TextBoxes"  runat="server"></asp:TextBox></td></tr>
            <tr><td><asp:Label ID="Label6" CssClass="customerLabel" Text="Province" runat="server" ></asp:Label></td>
                <td><asp:TextBox ID="txtProvince" CssClass="TextBoxes"  runat="server"></asp:TextBox></td></tr>
            <tr><td><asp:Label ID="Label7" CssClass="customerLabel" Text="Postal Code" runat="server" ></asp:Label></td>
                <td><asp:TextBox ID="txtPostal" CssClass="TextBoxes"  runat="server"></asp:TextBox></td></tr>
                </table>

            <div id="buttons">
                <asp:Button ID="btnNewCustomer" CssClass="Buttons" runat="server" Text="New" ToolTip="Create a new customer" OnClick="btnNewCustomer_Click" />
                <asp:Button ID="btnAddCustomer" CssClass="Buttons" runat="server" Text="Add" ToolTip="Add new customer" OnClick="btnAddCustomer_Click" />
                <asp:Button ID="btnUpdateCustomer" CssClass="Buttons" runat="server" Text="Update" ToolTip="Update customer" OnClick="btnUpdateCustomer_Click" />
                <asp:Button ID="btnDeleteCustomer" CssClass="Buttons" runat="server" Text="Delete" ToolTip="Delete customer" OnClick="btnDeleteCustomer_Click" />
                <asp:Button ID="btnFindCustomer" CssClass="Buttons"  runat="server" Text="Find" ToolTip="Find customer" OnClick="btnFindCustomer_Click" />
            </div>
        <asp:Panel ID="Panel1" runat="server">
            <asp:Label ID="lblMessages" CssClass="lblMessages" runat="server" Height="25px" Width="300px"></asp:Label>
        </asp:Panel>
    </form>
</body>
</html>

