using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlekRuzic_eCommerce
{
    public partial class catalog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dbConnect = @"integrated security=True;data source=(localdb)\ProjectsV13;persist security info=False;initial catalog=Store";
            
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = null;
            SqlConnection connectFill = null;
            SqlConnection connectCmd = null;
            SqlCommand cmd = null;
            SqlCommand scmd = null;

            try
            {
                ds = new DataSet();
                connectFill = new SqlConnection(dbConnect);

                string sqlString = "SELECT * FROM Products";
                scmd = new SqlCommand(sqlString, connectFill);

                sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = scmd;
                sqlDataAdapter.Fill(ds, "Products");
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                return;
            }

            int numProducts = 0;

            if (ds.Tables["Products"].Rows.Count > 0)
            {
                numProducts = ds.Tables["Products"].Rows.Count;

                _default.modelNum = new string[numProducts];
                _default.pics = new string[numProducts];
                _default.descrip = new string[numProducts];
                _default.qty = new string[numProducts];
                _default.price = new string[numProducts];

                for (int i = 0; i < numProducts; i++)
                {
                    _default.modelNum[i] = ((int)ds.Tables["Products"].Rows[i]["ProdID"]).ToString();
                    _default.descrip[i] = ds.Tables["Products"].Rows[i]["Description"].ToString();
                    _default.pics[i] = ds.Tables["Products"].Rows[i]["Picture"].ToString();
                    _default.qty[i] = ((int)ds.Tables["Products"].Rows[i]["QOH"]).ToString();
                    _default.price[i] = ((decimal)ds.Tables["Products"].Rows[i]["Price"]).ToString();
                }
            }

            DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);



            for (int i = 0; i < numProducts; i++)
            {
                TableRow row = new TableRow();
                for (int j = 0; j < 6; j++)
                {
                    TableCell cell = new TableCell();

                    if (j == 0)
                    {
                        cell.Text = _default.modelNum[i];
                    }

                    else if (j == 1)
                    {
                        cell.Text = _default.descrip[i];
                    }

                    else if (j == 2)
                    {
                        Image image = new Image();
                        image.ImageUrl = "ImageBoxPics/" + _default.pics[i];
                        image.Height = 100;
                        image.Width = 100;
                        cell.Controls.Add(image);
                    }
                    else if (j == 3)
                    {
                        cell.Text = _default.qty[i];
                    }
                    else if (j == 4)
                    {
                        cell.Text = "$" + _default.price[i];
                    }
                    else
                    {

                        Button btnAddToCart = new Button();
                        btnAddToCart.ID = i.ToString();

                        btnAddToCart.Click += new EventHandler(AddToCart_Click);
                        btnAddToCart.Text = "Add To Cart";
                        cell.Controls.Add(btnAddToCart);
                    }
                    row.Cells.Add(cell);
                }
                catalogueTable.Rows.Add(row);
            }
        }

    protected void AddToCart_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int row = int.Parse(b.ID);

            lblRowSelected.Text = "You selected " + _default.descrip[row];

            if (_default.numItems > 0)
            {
                bool matchRow = false;
                for (int i = 0; i < _default.numItems; i++)
                {
                    if (row == _default.cartInfo[i])
                        matchRow = true;
                }

                if (!matchRow)
                {
                    _default.cartInfo[_default.numItems] = row;
                    _default.numItems++;
                }
            }
            else
            {
                _default.cartInfo[_default.numItems] = row;
                _default.numItems++;
            }
           }

        private static void DisposeResources(ref SqlDataAdapter sqlDataAdapter,
            ref DataSet ds,
            ref SqlConnection connectFill,
            ref SqlConnection connectCmd,
            ref SqlCommand cmd,
            ref SqlCommand scmd)
        {
            if (sqlDataAdapter != null)
                sqlDataAdapter.Dispose();
            if (ds != null)
                ds.Dispose();
            if (connectFill != null)
                connectFill.Dispose();
            if (connectCmd != null)
                connectCmd.Dispose();
            if (cmd != null)
                cmd.Dispose();
            if (scmd != null)
                scmd.Dispose();
        }
    }
}