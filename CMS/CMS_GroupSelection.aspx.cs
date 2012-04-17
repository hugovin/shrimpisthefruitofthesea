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

public partial class CMSsite : ContentGroup
{
    protected void Page_Load(object sender, EventArgs e)
    {       

        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        Session["contId"] = null;
        Session["CurrentPage"] = "CMS_GroupSelection.aspx";
        DataSet data = new DataSet();
        int size = Convert.ToInt32(Session["ContentMax"].ToString());
        string[] Vector = new string[size+1];
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        data = getAllContentGroups(Convert.ToInt32(Session["siteId"]));

        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                string link = "<a class=\"SelectAudience2\" href= CMS_MainSite.aspx?GroupId=" + Convert.ToInt32(row["ContId"]) + ">" + Convert.ToString(row["ContTitle"]) + "</a>";
                              
                Vector[Convert.ToInt32(row["ContOrdPos"])] =link;
            }

        }

        for (int i = 1; i < Vector.Length; i++)
        {
            sb.AppendLine(Convert.ToString(Vector[i]));
        }
        div_selectionGroup.InnerHtml = sb.ToString();
    }
}
