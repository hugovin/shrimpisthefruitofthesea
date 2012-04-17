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
    public partial class uc_SAPricing : System.Web.UI.UserControl
    {
                //---------------
        private int intSite;
        private SiteProduct product = new SiteProduct();

        protected void Page_Load(object sender, EventArgs e)
        {
             intSite = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
            Addins adds = new Addins();
            DataSet data = new DataSet();
            data = product.Get_SideSAPricing(intSite);

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
                        sbproduct.AppendLine("<div class=\"DottedGray\"></div>");
                        sbproduct.AppendLine("<div id=\"div_Product2\" class=\"boxProduct\">");

                    }
                    sbproduct.AppendLine("<div class=\"colorLinks\">");
                    sbproduct.AppendLine("<p><a href=product.aspx?p=\""+row["TitleId"]+"\">"+row["Title"]+"</a><br />"+adds.cutDescription(adds.StripHTML(Convert.ToString(row["WebDesc"])), 39)+"</p>");                   
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
