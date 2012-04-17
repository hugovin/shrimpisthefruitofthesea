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

public partial class CMS_mn_HelpFullLinks : HelpfulLinks
{
    private int cont = 0;
    public string str_TinyMCE;
    protected void Page_Load(object sender, EventArgs e)
    {
        Addins addins = new Addins();
        str_TinyMCE = addins.GettinyMCE();
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        Session["CurrentPage"] = "mnt_HelpFulLinks.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }
        //---------------------------------------------------------------

        #region First load
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        DataSet data = new DataSet();
        data = getGelpFullinks(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]));
        sb.AppendLine("<TABLE>");
        sb.AppendLine("<TR>");
        sb.AppendLine("<TH>Description</TH>");
        sb.AppendLine("</TR>");
        sb.AppendLine("<TR>");
        int id = 0;
        foreach (DataTable table2 in data.Tables)
        {
            foreach (DataRow row2 in table2.Rows)
            {
                sb.AppendLine("<TR><TD>");
                sb.AppendLine("" + row2["HelpFulContent"].ToString() + "");
                sb.AppendLine("</TD>");;
                sb.AppendLine("<TD>");
                id = Convert.ToInt32(row2["HelpfulId"]);
                sb.AppendLine("</TR></TD>");
                cont++;
            }
        }
        if (cont == 0)
        {
            btn_new.Visible = true;           
        }
        if (cont > 0)
        {
            sb.AppendLine("<a href= mnt_HelpFulLinks.aspx?EditId=" + id + "> Edit</a>");
        }
        sb.AppendLine("</TR>");
        sb.AppendLine("</TABLE>");
        
        div_list.InnerHtml = sb.ToString();
        #endregion

        #region new
        if (Request["New"] != null)
        {
            sb = new System.Text.StringBuilder();
            sb.AppendLine("Message:<br>");
            sb.AppendLine("<textarea id=\"elm1\" name=\"elm1\" rows=\"8\" cols=\"40\"></textarea><br>");
            div_description.InnerHtml = sb.ToString();
            div_Edit.Visible = true;
        }
        #endregion

        #region edit
        if (Request["EditId"] != null)
        {
            data = new DataSet();
            sb = new System.Text.StringBuilder();
            data = getGelpFullinksById(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]),Convert.ToInt32(Request["EditId"]));
            foreach (DataTable table2 in data.Tables)
            {
                foreach (DataRow row2 in table2.Rows)
                {
                    sb.AppendLine("Message:<br>");
                    sb.AppendLine("<textarea id=\"elm1\" name=\"elm1\" rows=\"8\" cols=\"40\">"+row2["HelpFulContent"]+"</textarea><br>");
                }
            }           

            div_description.InnerHtml = sb.ToString();
            div_Edit.Visible = true;

        }
        #endregion

    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_HelpFulLinks.aspx?New="+true);
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (Request["New"] != null)
        {
            AddHelpFulLinks(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToString(Request["elm1"]));
            Response.Redirect("mnt_HelpFulLinks.aspx");
        }
        else
        {
            UpdHelpFulLinks(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToInt32(Request["EditId"]), Convert.ToString(Request["elm1"]));
            Response.Redirect("mnt_HelpFulLinks.aspx");
        }
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_HelpFulLinks.aspx");
    }
}
