using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Customers
{
    public partial class customers : System.Web.UI.Page
    {
        string websiteData = HttpContext.Current.Server.MapPath(".") + @"\Data\Customers\";
        string dbConnect = @"integrated security=True;data source=(localdb)\ProjectsV13;persist security info=False;initial catalog=Store";

        public static int cusID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (AlekRuzic_eCommerce._default.cusID != 0 && !IsPostBack)
            {
                txtCustomerNumber.Text = AlekRuzic_eCommerce._default.cusID.ToString();
                finishFindCustomer();
            }
        }

        protected void btnNewCustomer_Click(object sender, EventArgs e)
        {
            flushData();
        }

        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = null;
            SqlConnection connectFill = null;
            SqlConnection connectCmd = null;
            SqlCommand cmd = null;
            SqlCommand scmd = null;

            connectCmd = new SqlConnection(dbConnect);
            connectCmd.Open();

            string sqlString = "INSERT INTO Customers (FirstName, LastName, Address, City, Province, PostalCode) VALUES (@FirstName, @LastName, @Address, @City, @Province, @PostalCode)";

            try
            {
                cmd = new SqlCommand(sqlString, connectCmd);
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                cmd.Parameters.AddWithValue("@Province", txtProvince.Text);
                cmd.Parameters.AddWithValue("@PostalCode", txtPostal.Text);
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
            txtCustomerNumber.Text = identValue.ToString();
            lblMessages.Text = "New Customer Added";
        }

        protected void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (txtCustomerNumber.Text != "")
            {
                SqlDataAdapter sqlDataAdapter = null;
                DataSet ds = null;
                SqlConnection connectFill = null;
                SqlConnection connectCmd = null;
                SqlCommand cmd = null;
                SqlCommand scmd = null;

                connectCmd = new SqlConnection(dbConnect);
                connectCmd.Open();

                string sqlString = "UPDATE Customers SET FirstName=@FirstName, LastName=@LastName, Address=@Address, City=@City, Province=@Province, PostalCode=@PostalCode WHERE CusID=@CusID";

                try
                {
                    cmd = new SqlCommand(sqlString, connectCmd);
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    cmd.Parameters.AddWithValue("@Province", txtProvince.Text);
                    cmd.Parameters.AddWithValue("@PostalCode", txtPostal.Text);
                    cmd.Parameters.AddWithValue("@CusId", txtCustomerNumber.Text);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    lblMessages.Text = ex.Message;
                    DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                    return;
                }

                lblMessages.Text = "Customer Updated";
                
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);

            }
        }

        protected void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
          

            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = null;
            SqlConnection connectFill = null;
            SqlConnection connectCmd = null;
            SqlCommand cmd = null;
            SqlCommand scmd = null;

            connectCmd = new SqlConnection(dbConnect);
            connectCmd.Open();

            string sqlString = "DELETE FROM Customers WHERE CusID = @CusID";

            try
            {
                cmd = new SqlCommand(sqlString, connectCmd);
                cmd.Parameters.AddWithValue("@CusID", txtCustomerNumber.Text);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
                return;
            }
            lblMessages.Text= "Customer deleted";

            DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);

            flushData();
        }

        private void finishFindCustomer()
        {
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

                string sqlString = "SELECT * FROM Customers WHERE CusID = @CusID";

                scmd = new SqlCommand(sqlString, connectFill);

                scmd.Parameters.AddWithValue("@CusID", txtCustomerNumber.Text);

                sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = scmd;
                sqlDataAdapter.Fill(ds, "Customers");
            }
            catch (Exception ex)
            {
                lblMessages.Text = ex.Message;
                DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
            }

            if (ds.Tables["Customers"].Rows.Count == 1)
            {
                txtFirstName.Text = ds.Tables["Customers"].Rows[0]["FirstName"].ToString();
                txtLastName.Text = ds.Tables["Customers"].Rows[0]["LastName"].ToString();
                txtAddress.Text = ds.Tables["Customers"].Rows[0]["Address"].ToString();
                txtCity.Text = ds.Tables["Customers"].Rows[0]["City"].ToString();
                txtProvince.Text = ds.Tables["Customers"].Rows[0]["Province"].ToString();
                txtPostal.Text = ds.Tables["Customers"].Rows[0]["PostalCode"].ToString();
                btnDeleteCustomer.Enabled = true;
                btnUpdateCustomer.Enabled = true;
                lblMessages.Text = "";
                cusID = int.Parse(txtCustomerNumber.Text);
                AlekRuzic_eCommerce._default.cusID = cusID;
            }
            else
            {
                lblMessages.Text = "Customer Number not found!";
            }

            DisposeResources(ref sqlDataAdapter, ref ds, ref connectFill, ref connectCmd, ref cmd, ref scmd);
        }

        protected void btnFindCustomer_Click(object sender, EventArgs e)
        {
            finishFindCustomer();
        }



        private void flushData()
        {
            txtCustomerNumber.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtProvince.Text = "";
            txtPostal.Text = "";

            lblMessages.Text = "";
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