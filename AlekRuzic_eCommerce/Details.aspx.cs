using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace AlekRuzic_eCommerce
{
    public partial class Details : System.Web.UI.Page
    {
        public static Logger Log = new Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            CreateDetailGrid();
            CalculateTotal();
        }

        private void CreateDetailGrid()
        {
            detailsTable.Rows.Clear();
            for (int i = 0; i < _default.numItems; i++)
            {
                // add new empty row object
                TableRow row = new TableRow();
                for (int j = 0; j < 4; j++)
                {
                    // add new empty cell object
                    TableCell cell = new TableCell();

                    if (j == 0)
                    {
                        cell.Text = _default.modelNum[_default.cartInfo[i]];
                    }
                    else if (j == 1)
                    {
                        cell.Text = _default.descrip[_default.cartInfo[i]];
                    }
                    else if (j == 2)
                    {
                        Label price = new Label();
                        price.Text = _default.price[_default.cartInfo[i]];
                        cell.Controls.Add(price);
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
                        text.Enabled = false;

                        cell.Controls.Add(text);
                    }
                    row.Cells.Add(cell);
                }
                detailsTable.Rows.Add(row);
            }
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            for (int i = 0; i < _default.numItems; i++)
            {
                TableRow row = detailsTable.Rows[i];
                decimal rowPrice = 0;

                for (int j = 0; j < 4; j++)
                {
                    TableCell cell = row.Cells[j];

                    if (j == 2)
                    {
                        Control ctrl = cell.Controls[0];
                        Label lbl = (Label)ctrl;
                        string price = lbl.Text;
                        rowPrice = decimal.Parse(price);
                    }
                    else if (j == 3)
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

            LblTotal.Text = total.ToString("$##,##0.#0");
        }

        protected void BtnPay_Click(object sender, EventArgs e)
        {
            if (_default.numItems == 0)
            {
                MessageBox.Show(this, "No items in the cart");
                return;
            }

            string dbConnect = @"integrated security=True;data source=(localdb)\ProjectsV13;persist security info=False;initial catalog=Store";

            // test data used to process a sale
            int customerID = Customers.customers.cusID;
            string[] productArray = new string[_default.numItems];
            int[] qtyArray = new int[_default.numItems];
            decimal[] priceArray = new decimal[_default.numItems];

            for (int i = 0; i < _default.numItems; i++)
            {
                productArray[i] = _default.modelNum[_default.cartInfo[i]];
                qtyArray[i] = int.Parse(_default.qtySold[_default.cartInfo[i]]);
                priceArray[i] = decimal.Parse(_default.price[_default.cartInfo[i]]);
            }

            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = null;
            SqlConnection connectFill = null;
            SqlConnection connectCmd = null;
            SqlCommand cmd = null;
            SqlCommand scmd = null;

            connectCmd = new SqlConnection(dbConnect);
            connectCmd.Open();

            // begin the transaction here
            SqlTransaction dbTrans = connectCmd.BeginTransaction();
            Log.LogMessage("MessageStream", "PayForOrder Starting Sales Transaction");
            // create a date/time stamp
            DateTime dtStamp = DateTime.Now;

            // 1. Create the SalesMain record
            string sqlString = "INSERT INTO SalesMain (CusID, DateTimeStamp) VALUES (@CusID, @DateTimeStamp)";

            try
            {
                cmd = new SqlCommand(sqlString, connectCmd);

                // start building the SQL transaction package
                cmd.Transaction = dbTrans;
                cmd.Parameters.AddWithValue("@CusID", customerID);
                cmd.Parameters.AddWithValue("@DateTimeStamp", dtStamp);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                Log.LogMessage("MessageStream", "PayForOrder 1.0 " + ex.Message);
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                return;
            }

            // get the primary key identity just inserted
            // this is the new invoice number which we need below
            cmd = new SqlCommand("SELECT IDENT_CURRENT('SalesMain') FROM SalesMain", connectCmd);
            cmd.Transaction = dbTrans;
            int invoiceNum = Convert.ToInt32(cmd.ExecuteScalar());

            // 2. Add all the SalesDetail records (1 for each product)

            for (int i = 0; i < productArray.Length; i++)
            {
                sqlString = "INSERT INTO SalesDetail (InvID, ProdId, Qty, Price) VALUES (@InvID, @ProdId, @Qty, @Price)";

                try
                {
                    cmd = new SqlCommand(sqlString, connectCmd);

                    // continue building the SQL transaction package
                    cmd.Transaction = dbTrans;
                    cmd.Parameters.AddWithValue("@InvID", invoiceNum);
                    cmd.Parameters.AddWithValue("@ProdId", productArray[i]);
                    cmd.Parameters.AddWithValue("@Qty", qtyArray[i]);
                    cmd.Parameters.AddWithValue("@Price", priceArray[i]);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    Log.LogMessage("MessageStream", "PayForOrder 2.0 " + ex.Message);
                    DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                    return;
                }
            }

            // 3. Update the QtyOnHand for each product record

            int qtyOnHand = 0;
            for (int i = 0; i < productArray.Length; i++)
            {
                // get the current quantity on hand
                try
                {
                    ds = new DataSet();
                    connectFill = new SqlConnection(dbConnect);

                    sqlString = "SELECT QOH FROM Products WHERE ProdID = @ProdID";
                    scmd = new SqlCommand(sqlString, connectFill);
                    scmd.Parameters.AddWithValue("@ProdID", productArray[i]);

                    sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = scmd;
                    sqlDataAdapter.Fill(ds, "Products");
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    Log.LogMessage("MessageStream", "PayForOrder 3.0 " + ex.Message);
                    DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                    return;
                }

                if (ds.Tables["Products"].Rows.Count == 1)
                {
                    qtyOnHand = (int)ds.Tables["Products"].Rows[0]["QOH"];
                }
                else
                {
                    dbTrans.Rollback();
                    DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                    return;
                }

                sqlString = "UPDATE Products SET QOH = @QOH WHERE ProdID = @ProdID";

                try
                {
                    cmd = new SqlCommand(sqlString, connectCmd);

                    // finish building the SQL transaction package
                    cmd.Transaction = dbTrans;
                    cmd.Parameters.AddWithValue("@ProdID", productArray[i]);
                    cmd.Parameters.AddWithValue("@QOH", qtyOnHand - qtyArray[i]);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    Log.LogMessage("MessageStream", "PayForOrder 4.0 " + ex.Message);
                    DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                    return;
                }
            }

            // we haven't yet changed any database tables
            // if we get to this location, commit the transaction and complete all table changes
            dbTrans.Commit();
            Log.LogMessage("MessageStream", "PayForOrder Sales Transaction Completed for Customer " + customerID);
            DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
        }


        // **************************************************************
        // method releases all database resources that have been assigned
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

        protected void ConfirmAction_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "You clicked OK");
        }

        protected void ChkMailingList_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkMailingList.Checked)
            {
                // add
                bool add = true;
            }
            else
            {
                // remove
                bool add = false;
            }
        }
    }
}
