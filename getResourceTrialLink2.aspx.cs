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

public partial class getResourceTrialLink : System.Web.UI.Page
{
    private string TitleResourceId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["resourceId"] != null)
        {
            SiteProduct previewData = new SiteProduct();
            DataSet results = new DataSet();
            results = previewData.Get_Site_Trials_Demos(Convert.ToInt32(Request["resourceId"].ToString()), 52);
            foreach (DataTable table in results.Tables)
            {
                foreach (DataRow row in table.Rows)
                {

                    Response.Write(row["TitleResourceLoc"].ToString());
                    FreeTools fs = new FreeTools();
                    Login lg = new Login();
                    fs.add_TitleResourceViewer(Convert.ToInt32(row["TitleResourceId"]));
                }

            }
            previewData = null;
        }
        else {
            Response.Write("Error");
        }
    }
}
