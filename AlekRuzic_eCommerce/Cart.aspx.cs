using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlekRuzic_eCommerce
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateCartGrid();
            CalculateTotal();
        }

        protected void RemoveFromCart_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int row = int.Parse(b.ID);

            _default.qtySold[_default.cartInfo[row]] = "1";

            for (int i = row; i < _default.numItems; i++)
                _default.cartInfo[i] = _default.cartInfo[i + 1];

            _default.numItems--;
            CreateCartGrid();
            CalculateTotal();
        }

        private void CreateCartGrid()
        {
            cartTable.Rows.Clear();
            for (int i = 0; i < _default.numItems; i++)
            {
                TableRow row = new TableRow();
                for (int j = 0; j < 7; j++)
                {
                    TableCell cell = new TableCell();
                    
                    if (j == 0)
                    {
                        Image image = new Image();
                        image.ImageUrl = "ImageBoxPics/" + _default.pics[_default.cartInfo[i]];
                        image.Height = 100;
                        image.Width = 100;
                        cell.Controls.Add(image);
                    }

                    else if (j == 1)
                    {
                        cell.Text = _default.modelNum[_default.cartInfo[i]];
                    }

                    else if (j == 2)
                    {
                        cell.Text = _default.descrip[_default.cartInfo[i]];
                    }

                    else if (j == 3)
                    {
                        cell.Text = _default.qty[_default.cartInfo[i]];
                    }

                    else if (j == 4)
                    {
                        Label price = new Label();
                        price.Text = _default.price[_default.cartInfo[i]];

                        // this is the line of code we were missing in class
                        cell.Controls.Add(price);
                    }
                    else if (j == 5)
                    {

                        Button btnRemoveFromCart = new Button();
                        btnRemoveFromCart.ID = i.ToString();
                      
                        btnRemoveFromCart.Click += new EventHandler(RemoveFromCart_Click);
                        btnRemoveFromCart.Text = "Remove from Cart";
                        cell.Controls.Add(btnRemoveFromCart);
                    }
                    else 
                    {

                        TextBox text = new TextBox();
                        text.Text = _default.qtySold[_default.cartInfo[i]];
                        text.Style["font-family"] = "helvetica";
                        text.Style["color"] = "blue";
                        text.Style["background-color"] = "white";
                        text.Style["width"] = "20px";
                        text.Style["border"] = "solid 1px #002594";

                        cell.Controls.Add(text);
                    }

                    row.Cells.Add(cell);
                }
                
                cartTable.Rows.Add(row);
            }
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            for (int i = 0; i < _default.numItems; i++)
            {
                TableRow row = cartTable.Rows[i];
                decimal rowPrice = 0;

                for (int j = 0; j < 7; j++)
                {
                    TableCell cell = row.Cells[j];

                    if (j == 4)
                    {
                        Control ctrl = cell.Controls[0];
                        Label lbl = (Label)ctrl;
                        string price = lbl.Text;
                        rowPrice = decimal.Parse(price);
                    }
                    else if (j == 6)
                    {
                        Control ctrl = cell.Controls[0];
                        TextBox txt = (TextBox)ctrl;
                        string qty = txt.Text;
                        _default.qtySold[_default.cartInfo[i]] = qty;

                        decimal rowTotal = rowPrice * int.Parse(qty);
                        total += rowTotal;
                    }
                }
            }

            LblTotal.Text = total.ToString("$##,##0.##");
        }

        protected void btnRecalculate_Click(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Server.Transfer("Checkout.aspx");
        }
    }
}