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
    public partial class uc_Adds : System.Web.UI.UserControl
    {
        private int intSite;
        Adds adds = new Adds();

        protected void Page_Load(object sender, EventArgs e)
        {
            intSite = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
            DataSet data = new DataSet();
            data = adds.Get_SideAdds(intSite);//product.Get_SideFeaturedProducts(intSite, intCont, intSubjId);
            System.Text.StringBuilder sbproduct = new System.Text.StringBuilder();
            int contDiv = 0;
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (contDiv == 0)
                    {
                        sbproduct.AppendLine("<a href=\"generic_x?LandingId=" + row["AddsLink"] + "\"><div class=\"pubMeetOut\"><img src=\"images\\" + row["AddsImage"] + "\"/> </div></a>");

                    }
                    else
                    {
                        sbproduct.AppendLine("<a href=\"generic_x?LandingId=" + row["AddsLink"] + "\"><div class=\"pubNeedHelp\"><img src=\"images\\" + row["AddsImage"] + "\"/> </div></a>");

                    }
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
