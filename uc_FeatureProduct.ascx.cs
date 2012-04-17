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
    public partial class uc_FeatureProduct : System.Web.UI.UserControl
    {
        //---------------
        private int intSite;
        private int intCont;
        public int intSubjId;
        //---------------     
        Addins addins = new Addins();
        private SiteProduct product = new SiteProduct();
        protected void Page_Load(object sender, EventArgs e)
        {
            intSite = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
            intCont = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());            
            DataSet data = new DataSet();
            data = product.Get_SideFeaturedProducts(intSite, intCont, intSubjId);
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
                    sbproduct.AppendLine("<img src=\"" + SiteConstants.imagesPathTb + "tn_" + row["ImageTn"] + "\" title=\""+row["PubName"]+" : "+ row["Title"]+"\" width=\"70\" height=\"70\" /></div>");
                    sbproduct.AppendLine("<div class=\"boxTextProduct\">");
                    sbproduct.AppendLine("<h3><a href=\"product.aspx?p=" + row["TitleID"] + "\">" + row["Title"] + "</a> </h3>");
                    sbproduct.AppendLine("</div><div class=\"clear\"></div></div>");
                    sbproduct.AppendLine("<div class=\"clear\"></div>");
                    
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

