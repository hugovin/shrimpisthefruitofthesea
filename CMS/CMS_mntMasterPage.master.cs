using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CMS_mntMasterPage : System.Web.UI.MasterPage
{
    public string UserName;
    protected void Page_Load(object sender, EventArgs e)
    {
         System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (Convert.ToInt32(Session["userType"]) == 1)
        { 
            sb.AppendLine("<div class=\"LoginName\">");
            sb.AppendLine("<a href=mnt_administrator.aspx style=\"border:0px\"><img src=\"imagesCss/user-icon.gif\" style=\"border:0px\" alt=\"user\" align=\"absmiddle\" /></a> Administrator ");
            sb.AppendLine("</div>");
        }else
        {
             sb.AppendLine("<div class=\"LoginName\">");
            sb.AppendLine("<img src=\"imagesCss/user-icon.gif\" alt=\"user\" align=\"absmiddle\" />&nbsp;" + Convert.ToString(Session["userfullname"]) + "");
            sb.AppendLine("</div>");
        }
        sb.AppendLine("<div class=\"LoginName\" id=\"cms_documentation\"><a class=\"linkHeader\" href=\"documents/ER-CMSUserManual.doc\" target=\"_blank\"><img align=\"absmiddle\" alt=\"user\" style=\"border: 0px none;\" src=\"imagesCss/Word.gif\"> CMS Documentation</a></div>");
        admin_name.InnerHtml = sb.ToString();
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
           Response.Redirect("CMS_Login.aspx");
        }
        sb = new System.Text.StringBuilder();

            /*if (Session["contId"] != null)
            {*/
                ContentNavigation content = new ContentNavigation();
                DataSet data = new DataSet();
                string[] Vector = new string[6 + 1];
                data = content.getAllContent(Convert.ToInt32(Session["siteId"]));
                foreach (DataTable table in data.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {

                        string link = "<a class=MenuText  href=" + Convert.ToString(Session["CurrentPage"]) + "?ContentGroupId=" + Convert.ToInt32(row["ContId"]) + ">" + Convert.ToString(row["ContTitle"]) + "</a>";
                        Vector[Convert.ToInt32(row["ContOrdPos"])] = link;
                    }

                }

                for (int i = 1; i < Vector.Length; i++)
                {
                    sb.AppendLine(Convert.ToString(Vector[i]));
                }

                sb.AppendLine(" <br/>");
                div_TopNavigation.InnerHtml = sb.ToString();
                Div_Content.Visible = true;

                //if (Convert.ToBoolean(Session["authenticated"]) == true)
                //{
                //    refLogout.Visible = true;
                //}
            //}
        
    }
}
