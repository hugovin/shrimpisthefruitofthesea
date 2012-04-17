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

    public partial class uc_NewsNinfo : System.Web.UI.UserControl
    {
        private int intSite;
        private int intCont;
        public string newid = "";

        Addins addins = new Addins();
        private SiteProduct product = new SiteProduct();

        protected void Page_Load(object sender, EventArgs e)
        {
            int contdiv = 0;
            intSite = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
            intCont = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());    
            DataSet data = new DataSet();
            data = product.Get_SideNewsNInfo(intSite, intCont);
            System.Text.StringBuilder sbproduct = new System.Text.StringBuilder();
            if (data.Tables["table"].Columns.Count > 2)
            {
                foreach (DataTable table in data.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        if (row[0].ToString() != "")
                            newid = row["GeneId"].ToString();
                        sbproduct.AppendLine("<p><strong>" + row["GeneBTitle"] + "</strong><br />" + addins.cutDescription(addins.StripHTML(Convert.ToString(row["GeneBContent"])), 120) + "</p>");
                        contdiv++;
                    }
                }
                if (contdiv != 0)
                {
                    Product.Controls.Add(new LiteralControl(sbproduct.ToString()));
                    div_Wrapper.Visible = true;
                }
            }
        }
    }
}
