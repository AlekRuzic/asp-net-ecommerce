using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlekRuzic_eCommerce
{
    public partial class PromoPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            iphone7.ImageUrl = "ImageBoxPics/iphone7.jpg";
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Server.Transfer("Catalog.aspx");
        }
    }
}