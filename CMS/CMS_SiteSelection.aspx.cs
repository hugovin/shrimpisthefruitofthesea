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

public partial class SiteSelection : Site
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }

        Session["CurrentPage"] = "CMS_SiteSelection.aspx";
         DataSet data = new DataSet();
        string user= ""; 
        int siteId, UserAccessL = 0;
        data = Get_UserCms_Site(Session["userid"].ToString());
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                user = Convert.ToString(row["UserId"]);
                siteId = Convert.ToInt32(row["SiteId"]);
                UserAccessL = Convert.ToInt32(row["UserAccesLevel"]);
            }

        }
        Session["accessLevel"] = UserAccessL;
        DataSet data2 = new DataSet();
        data2 = getSiteInfo();
        foreach (DataTable table1 in data2.Tables)
        {
            foreach (DataRow row2 in table1.Rows)
            {
                ddlSite.Items.Add(new ListItem(Convert.ToString(row2["SITENAME"]), Convert.ToString(row2["SITEID"])));
                //ddlSite.Items.Insert((Convert.ToInt32(row2["SITEID"])-1), Convert.ToString(row2["SITENAME"]));
            }

        }
       
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {

        Session["ContentMax"] = 6;
        Session["siteId"] = (Convert.ToInt32(ddlSite.SelectedIndex)+1);
        Response.Redirect("CMS_GroupSelection.aspx");
        
    }
}
