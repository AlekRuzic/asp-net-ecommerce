using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlekRuzic_eCommerce
{
    public partial class _default : System.Web.UI.Page
    {

        public static string webSiteData = HttpContext.Current.Server.MapPath(".") + @"\Data\Products";

        const int MAXPRODUCTS = 10; 

        public static string[] modelNum;
        public static string[] descrip;
        public static string[] pics;
        public static string[] qty;
        public static string[] price;

        public static string[] qtySold = new string[MAXPRODUCTS];
        public static int[] cartInfo = new int[MAXPRODUCTS];
        public static int cusID = 0;
        public static int numItems = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 0; i < MAXPRODUCTS; i++)
                    qtySold[i] = "1";

                containerFrame.Attributes.Add("src", "PromoPage.aspx");
            }
        }

        protected void BtnPromoPage_Click(object sender, EventArgs e)
        {
            containerFrame.Attributes.Add("src", "PromoPage.aspx");
        }

        protected void BtnCatalog_Click(object sender, EventArgs e)
        {
            containerFrame.Attributes.Add("src", "Catalog.aspx");
        }

        protected void BtnCart_Click(object sender, EventArgs e)
        {
            containerFrame.Attributes.Add("src", "Cart.aspx");
        }

        protected void BtnCustomers_Click(object sender, EventArgs e)
        {
            containerFrame.Attributes.Add("src", "Customers.aspx");
        }

        protected void BtnProducts_Click(object sender, EventArgs e)
        {
            containerFrame.Attributes.Add("src", "Products.aspx");
        }

        //public static void ResetArrays()
        //{
        //    countFiles = Directory.GetFiles(webSiteData, "*.*");

        //    ProdID = new string[countFiles.Length];
        //    manufactCode = new string[countFiles.Length];
        //    descrip = new string[countFiles.Length];
        //    pics = new string[countFiles.Length];
        //    qty = new string[countFiles.Length];
        //    price = new string[countFiles.Length];
        //}
    }
}