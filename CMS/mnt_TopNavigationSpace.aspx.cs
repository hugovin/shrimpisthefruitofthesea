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

public partial class CMS_mnt_TopNavigationSpace : ContentGroup
{
    int MaxTopNavigation = 0;
    bool change = false;
    DataSet data = new DataSet();
    int contcat = 0;
    int contPos = 0;
    private int contnewpos = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        Session["CurrentPage"] = "mnt_TopNavigationSpace.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }
        #region FirstLoad
        change = false;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        data = getAllContentGroups(Convert.ToInt32(Session["siteId"]));
        sb.AppendLine("<h1>Top Navigation Space</h1>");
        sb.AppendLine("<TABLE>");
        sb.AppendLine("<TR>");
        sb.AppendLine("<TH>ID</TH>");
        sb.AppendLine("<TH>Position</TH>");
        sb.AppendLine("<TH COLSPAN= 2>GROUP NAME</TH>");
        sb.AppendLine("</TR>");

        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {

                sb.AppendLine("<TR><TD>");
                sb.AppendLine(""+row["ContId"].ToString()+"");
               
                sb.AppendLine("</TD>");
                sb.AppendLine("<TD>");
                sb.AppendLine(""+row["ContOrdPos"].ToString()+"");
                sb.AppendLine("</TD>");
                sb.AppendLine("<TD>");
                sb.AppendLine(""+row["ContTitle"].ToString()+"");
                sb.AppendLine("</TD>");
                sb.AppendLine("<TD>");
                sb.AppendLine("<a href= mnt_TopNavigationSpace.aspx?GroupId=" + row["ContId"] + "> Edit</a> | <a href= mnt_TopNavigationSpace.aspx?DeleteId=" + row["ContId"] + " onclick=\"return confirm('Do you want to continue?  ');\">Delete</A></td>");
                sb.AppendLine("</TD></TR>");
                contcat++;
                contPos++;
                contnewpos++;
                MaxTopNavigation++;
            }
        }

        sb.AppendLine("</TABLE>");
        sb.AppendLine("<input name=\"csub\" type=\"hidden\" value=\"" + contcat + "\" />");
        div_GroupName.InnerHtml = sb.ToString();
        #endregion
        #region new Group
        if (Request["NewGroup"]!=null)
        {
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            sb2.AppendLine("<TABLE>");
            sb2.AppendLine("<TR>");
            sb2.AppendLine("<TH>Position</TH>");
            sb2.AppendLine("<TH>Group Name</TH>");
            sb2.AppendLine("</TR>");
            sb2.AppendLine("<TR><TD>");
            sb2.AppendLine("<input id=\"txtPosition\"type=\"text\" readonly value=\""+contnewpos+"\" name=\"txtPosition\" />");
            sb2.AppendLine("</TD>");
            sb2.AppendLine("<TD>");
            sb2.AppendLine("<input id=\"txtGroupName\"type=\"text\" name=\"txtGroupName\" />");
            sb2.AppendLine("</TD></TR>");
            sb2.AppendLine("</TABLE>");
            sb2.AppendLine("</BR>");
            sb2.AppendLine("Description: ");
            sb2.AppendLine("<BR>");
            sb2.AppendLine("<textarea id=\"txtDescription\" name=\"txtDescription\" cols=\"35\" rows=\"2\"></textarea>");
            div_New.InnerHtml = sb2.ToString();
            div_btnSave.Visible = true;
        }
#endregion new group
        #region Edit Content Group

        if (Request["GroupId"] != null)
        {
            DataSet data2 = new DataSet();
            data2 = getContentGroupByID(Convert.ToInt32(Request["GroupId"]));            
            System.Text.StringBuilder sb3 = new System.Text.StringBuilder();
            change = true;
            sb3.AppendLine("<TABLE>");
            sb3.AppendLine("<TR>");
            sb3.AppendLine("<TH>Position</TH>");
            sb3.AppendLine("<TH>Group Name</TH>");
            sb3.AppendLine("</TR>");
            foreach (DataTable table2 in data2.Tables)
            {
                foreach (DataRow row2 in table2.Rows)
                {
                    sb3.AppendLine("<TR><TD>");
                    sb3.AppendLine("<input id=\"txtPosition\"type=\"text\" name=\"txtPosition\" value=\""+row2["ContOrdPos"].ToString()+"\" />");
                    sb3.AppendLine("</TD>");
                    sb3.AppendLine("<TD>");
                    sb3.AppendLine("<input id=\"txtGroupName\"type=\"text\" name=\"txtGroupName\"value=\"" + row2["ContTitle"].ToString() + "\" />");
                    sb3.AppendLine("</TD></TR>");
                    sb3.AppendLine("</TABLE>");
                    sb3.AppendLine("</BR>");
                    sb3.AppendLine("Description: ");
                    sb3.AppendLine("<BR>");
                    sb3.AppendLine("<textarea id=\"txtDescription\" name=\"txtDescription\" cols=\"35\" rows=\"2\">" + row2["ContDescription"].ToString() +"</textarea>");
                    div_New.InnerHtml = sb3.ToString();
                    div_btnSave.Visible = true;
                }
            }
        }
        #endregion
        #region Delete
        if (Request["DeleteId"] != null)
        {
            if(contPos>0)
            {
                deleteTopNavigation(Convert.ToInt32(Request["DeleteId"]));
                Response.Redirect("mnt_TopNavigationSpace.aspx");
            }
        }
        #endregion
        
    }
    //-------------------------
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (MaxTopNavigation < 6)
        {
            Response.Redirect("mnt_TopNavigationSpace.aspx?NewGroup=true");
        }
        else {
            lbError1.Text = " * Only 6 items can be added on this area.";
        }
        
    }
    //-------------------------
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_TopNavigationSpace.aspx");
    }
    //-------------------------
    protected void btnSaveGroup_Click(object sender, EventArgs e)
    {

        if ((Convert.ToString(Request["txtGroupName"]) != "") && (NumericValidation() != false) && (contPos<6))
        {
            if (Convert.ToInt32(Request["txtPosition"]) < 7)
            {
                if (change == false)
                {
                    addContentGroup(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Request["txtPosition"]), Convert.ToString(Request["txtGroupName"]), Convert.ToString(Request["txtDescription"]), true);
                    Response.Redirect("mnt_TopNavigationSpace.aspx");
                }
                else
                {
                    string test = Convert.ToString(Request["txtDescription"]);
                    updateContentGroup(Convert.ToInt32(Request["GroupId"]), Convert.ToInt32(Request["txtPosition"]), Convert.ToString(Request["txtGroupName"]), Convert.ToString(Request["txtDescription"]), true);
                    Response.Redirect("mnt_TopNavigationSpace.aspx");
                }
            }
        }
        else
        {
            lbError.Visible = true;
        }

    }
    //-------------------------
    protected bool NumericValidation()
    {
        bool flag = true;
        int cont = 0;
        string search = Convert.ToString(Request["txtPosition"]);
        for (int i = 0; i < search.Length; i++)
        {
            if (((search[i] == '0') || (search[i] == '1') || (search[i] == '2') || (search[i] == '3') || (search[i] == '4')
                || (search[i] == '5')) && (i > 0))
            {
                cont++;
            }            
        }
        if (cont != (search.Length - 1))
        {
            flag = false;
        }

        return flag;

    }
}

