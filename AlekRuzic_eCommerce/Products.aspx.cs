using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlekRuzic_eCommerce
{
    public partial class Products : System.Web.UI.Page
    {
        string websiteData = HttpContext.Current.Server.MapPath(".") + @"\Data\Products\";
        string dbConnect = @"integrated security=True;data source=(localdb)\ProjectsV13;persist security info=False;initial catalog=Store"; 

        public static string websitePics = HttpContext.Current.Server.MapPath(".") + @"\ImageBoxPics";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] fileList = Directory.GetFiles(websitePics, "*.*");
                listbox.Items.Clear();

                for (int i = 0; i < fileList.Length; i++)
                {
                    string fileName = Path.GetFileName(fileList[i]);
                    listbox.Items.Add(fileName);
                }
            } 
        }

        protected void newButton_Click(object sender, EventArgs e)
        {
            flushData();
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = null;
            SqlConnection connectFill = null;
            SqlConnection connectCmd = null;
            SqlCommand cmd = null;
            SqlCommand scmd = null;

            connectCmd = new SqlConnection(dbConnect);
            connectCmd.Open();

            string sqlString = "INSERT INTO Products (ManufacCode, Description, Picture, QOH, Price) VALUES (@ManufacCode, @Description, @Picture, @QOH, @Price)";

            try
            {
                cmd = new SqlCommand(sqlString, connectCmd);
                cmd.Parameters.AddWithValue("@ManufacCode", txtManufac.Text);
                cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
                cmd.Parameters.AddWithValue("@Picture", txtPic.Text);
                cmd.Parameters.AddWithValue("@QOH", txtQOH.Text);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                return;
            }

            string identRequest = "SELECT IDENT_CURRENT('Customers') FROM Customers";
            cmd = new SqlCommand(identRequest, connectCmd);
            int identValue = Convert.ToInt32(cmd.ExecuteScalar());
            txtProductId.Text = identValue.ToString();
            lblMessages.Text = "Product Added";

            DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd); 

        }

        protected void updateButton_Click(object sender, EventArgs e)
        {
            if (txtProductId.Text != "")
            {
                SqlDataAdapter sqlDataAdapter = null;
                DataSet ds = null;
                SqlConnection connectFill = null;
                SqlConnection connectCmd = null;
                SqlCommand cmd = null;
                SqlCommand scmd = null;

                connectCmd = new SqlConnection(dbConnect);
                connectCmd.Open();

                string sqlString = "UPDATE Products SET ManufacCode=@ManufacCode, Description=@Description, Picture=@Picture, QOH=@QOH, Price=@Price WHERE ProdID=@ProdID";

                try
                {
                    cmd = new SqlCommand(sqlString, connectCmd);
                    cmd.Parameters.AddWithValue("@ManufacCode", txtManufac.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDesc.Text);
                    cmd.Parameters.AddWithValue("@Picture", txtPic.Text);
                    cmd.Parameters.AddWithValue("@QOH", txtQOH.Text);
                    cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@ProdId", txtProductId.Text);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    lblMessages.Text = ex.Message;
                    DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                    return;
                }

                lblMessages.Text = "Product Updated";

                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
            }
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            string dbConnect = @"integrated security=True;data source=(localdb)\ProjectsV13;persist security info=False;initial catalog=Store";

            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = null;
            SqlConnection connectFill = null;
            SqlConnection connectCmd = null;
            SqlCommand cmd = null;
            SqlCommand scmd = null;

            connectCmd = new SqlConnection(dbConnect);
            connectCmd.Open();

            string sqlString = "DELETE FROM Products WHERE ProdID = @ProdID";

            try
            {
                cmd = new SqlCommand(sqlString, connectCmd);
                cmd.Parameters.AddWithValue("@ProdID", txtProductId.Text);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                return;
            }
            
            lblMessages.Text = "Product Deleted";

            DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);

            flushData();

        }

        protected void findButton_Click(object sender, EventArgs e)
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

                    string sqlString = "SELECT * FROM Products WHERE ProdID = @ProdID";

                    scmd = new SqlCommand(sqlString, connectFill);

                    scmd.Parameters.AddWithValue("@ProdID", txtProductId.Text);

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


                if (ds.Tables["Products"].Rows.Count == 1)
                {
                    txtManufac.Text = ds.Tables["Products"].Rows[0]["ManufacCode"].ToString();
                    txtDesc.Text = ds.Tables["Products"].Rows[0]["Description"].ToString();
                    txtPic.Text = ds.Tables["Products"].Rows[0]["Picture"].ToString();
                    txtQOH.Text = ds.Tables["Products"].Rows[0]["QOH"].ToString();
                    txtPrice.Text = ds.Tables["Products"].Rows[0]["Price"].ToString();

                    imageBox.ImageUrl = "ImageBoxPics/" + txtPic.Text;
                }
                else
                {
                    lblMessages.Text = "Product Not Found";
                }

                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);

                lblMessages.Text = "";
            
        }

        private void flushData()
        {
            txtProductId.Text = "";
            txtManufac.Text = "";
            txtDesc.Text = "";
            txtPic.Text = "";
            txtQOH.Text = "";
            txtPrice.Text = "";

            lblMessages.Text = "";
            imageBox.ImageUrl = "";
        }

        protected void listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string imageSource = listbox.SelectedItem.Text;
            txtPic.Text = imageSource;

            imageBox.ImageUrl = "ImageBoxPics/" + listbox.SelectedItem.Text;
            
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