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
    public partial class uc_BestSellers : System.Web.UI.UserControl
    {
        //---------------
        private int intSite;
        private int intCont;
        public int intSubjId;

        //---------------
        protected void Page_Load(object sender, EventArgs e)
        {
            int contDiv = 0;
            SiteProduct pro = new SiteProduct();
            intSite = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
            intCont = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
            DataSet data = new DataSet();
            System.Text.StringBuilder sbproduct = new System.Text.StringBuilder();
            data = pro.Get_SideBestSellers(intSite, intCont,intSubjId);            
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    
                    sbproduct.AppendLine("<li><a href=\"product.aspx?p="+row["TitleID"]+"\">"+row["Title"]+"</a></li>");
                    contDiv++;
                }
            }
            if (contDiv != 0)
            {
                Product2.Controls.Add(new LiteralControl(sbproduct.ToString()));
                div_Wrapper.Visible = true;
            }              
        }
    }
}
