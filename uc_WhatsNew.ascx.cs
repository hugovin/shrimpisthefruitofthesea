using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using uc_Right;

namespace uc_Right
{
    public partial class uc_WhatsNew : System.Web.UI.UserControl
    {
         //---------------
            private int intSite;
            private int intCont;
        //---------------
        private SiteProduct product = new SiteProduct();
        Addins addins = new Addins();

        protected void Page_Load(object sender, EventArgs e)
        {

            intSite = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
            intCont = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
            
            DataSet data = new DataSet();
            data = product.Get_SideWhatsNew(intSite, intCont);
            System.Text.StringBuilder sbproduct = new System.Text.StringBuilder();
            int contDiv = 0;
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (contDiv == 0)
                    {
                        sbproduct.AppendLine("<div id=\"div_Product1\" class=\"boxProduct\">");
                    }
                    else
                    {
                        sbproduct.AppendLine("<div class=\"clear\"></div>");
                        sbproduct.AppendLine("<div class=\"DottedGray dividerProduct\"></div>");
                        sbproduct.AppendLine("<div id=\"div_Product2\" class=\"boxProduct\">");
                    
                    }
                    sbproduct.AppendLine("<div class=\"boxImageProduct\">");
                    sbproduct.AppendLine("<img src=\"" + SiteConstants.imagesPathTb + "tn_" + row["ImageTn"] + "\" alt=\"\" width=\"66\" height=\"88\" /></div>");
                    sbproduct.AppendLine("<div class=\"boxTextProduct\">");
                    sbproduct.AppendLine("<div class=\"clear\"></div>");
                    sbproduct.AppendLine("<h3><a href=\"product.aspx?p=" + row["TitleID"] + "\">" + row["Title"] + "</a> </h3>");
                    sbproduct.AppendLine("</div></div>");
                    
                    contDiv++;
                }
            }
            if (contDiv != 0)
            {
                Product1.Controls.Add(new LiteralControl(sbproduct.ToString()));
                div_Wrapper.Visible = true;
            }
        }
       
    }
}
