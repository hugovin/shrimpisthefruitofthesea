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

public partial class Content : System.Web.UI.UserControl
{
   
    
    protected void Page_Load(object sender, EventArgs e)
    {

        ContentNavigation content = new ContentNavigation();
        
        DataSet data = new DataSet();
        
        string[] Vector = new string[6 + 1];
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        data = content.getAllContent(Convert.ToInt32(Session["siteId"]));

        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                
                string link = "<a href= CMS_MainSite.aspx?GroupId=" + Convert.ToInt32(row["ContId"]) + ">" + Convert.ToString(row["ContTitle"]) + "</a>";

                Vector[Convert.ToInt32(row["ContOrdPos"])] = link;
            }

        }

        for (int i = 1; i < Vector.Length; i++)
        {
            sb.AppendLine(Convert.ToString(Vector[i]));
        }
        sb.AppendLine("<hr /> <br/>");
        div_Group_Selector.InnerHtml = sb.ToString();
    }
}
