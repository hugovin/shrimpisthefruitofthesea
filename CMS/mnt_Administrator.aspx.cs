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

public partial class CMS_mnt_Administrator : UserCMS
{
    public bool AddEdit = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet data = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        data = Get_All_Users();
        bool fila = false;
        sb.AppendLine(" <table width=\"512\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\"><tr>");
        sb.AppendLine("<td width=\"4\" height=\"10\">&nbsp;</td>");
        sb.AppendLine("<td width=\"200\" class=\"class_LineaVTabla\">&nbsp;</td>");
        sb.AppendLine("<td width=\"109\" class=\"class_LineaVTabla\">&nbsp;</td> ");
        sb.AppendLine("<td width=\"113\" class=\"class_LineaVTabla\" align=\"center\">&nbsp;</td>");
        sb.AppendLine("<td width=\"113\" align=\"center\">&nbsp;</td></tr>");
        sb.AppendLine("<tr>");
        sb.AppendLine("<td>&nbsp;</td>");
        sb.AppendLine("<td class=\"class_LineaVTabla\">User Name</td>");
        sb.AppendLine("<td class=\"class_LineaVTabla\">Full Name</td>");
        sb.AppendLine("<td  class=\"class_LineaVTabla\" align=\"center\">&nbsp;</td>");
        sb.AppendLine("</tr>");
        foreach (DataTable table2 in data.Tables)
        {
            foreach (DataRow row2 in table2.Rows)
            {
                if (fila == true)
                {
                    sb.AppendLine("<tr>");
                }
                else
                {
                    sb.AppendLine("<tr class=\"whiteTable\"> ");
                }
                sb.AppendLine("<td>&nbsp;</td>");
                sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\">" + row2["UserId"].ToString() + "</td>");
                sb.AppendLine("<td align=\"left\" class=\"class_LineaVTabla\">" + row2["UserFullName"].ToString() + "</td>");
                sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><a href=mnt_Administrator.aspx?EditUser=" + row2["UserNumber"] + "><img src=\"images/btn_Edit.png\" border=\"0\" />Edit</a></td>");
                sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><a href=mnt_Administrator.aspx?DeleteUser=" + row2["UserNumber"] + "><img src=\"images/btn_delete.png\" border=\"0\" />Delete</a></td>");
                //sb.AppendLine("<td align=\"left\">&nbsp;</td>");
                sb.AppendLine(" </tr>");
                if (fila == true)
                { fila = false; }
                else
                { fila = true; }
            }

        }
        sb.AppendLine("</TABLE>");
        div_ListOfUser.InnerHtml = sb.ToString();

        #region New user
        if (Request["NewUser"] != null)
        {
            AddEdit = true;
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            sb2.AppendLine("Fill the information:<br>");
            sb2.AppendLine("<br>");
            sb2.AppendLine("Username:");
            sb2.AppendLine("<input type=\"text\" id=\"txtNewUsername\" name=\"txtNewUsername\" /><br>");
            sb2.AppendLine("<br>");
            sb2.AppendLine("Password:");
            sb2.AppendLine("<input type=\"password\" id=\"txtNewpwd\" name=\"txtNewpwd\" /><br>");
            sb2.AppendLine("<br>");
            sb2.AppendLine("Re-Type Password:");
            sb2.AppendLine("<input type=\"password\" id=\"txtREpwd\" name=\"txtREpwd\" /><br>");
            sb2.AppendLine("<br>");
            sb2.AppendLine("Full Name:");
            sb2.AppendLine("<input type=\"text\" id=\"txtFullName\" name=\"txtFullName\" /><br>");
            sb2.AppendLine("<br>");
            div_UserInfo.InnerHtml = sb2.ToString();
        }
        #endregion

        #region Edit user
        if (Request["EditUser"] != null)
        {
            DataSet data3 = new DataSet();
            data3 = Get_User_By_Id(Convert.ToInt32(Request["EditUser"]));
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            AddEdit = true;
            foreach (DataTable table3 in data3.Tables)
            {
                foreach (DataRow row3 in table3.Rows)
                {                   
            sb2.AppendLine("Edit the information:<br>");
            sb2.AppendLine("<br>");
            sb2.AppendLine("Username:");
            sb2.AppendLine("<input type=\"text\" id=\"txtNewUsername\" name=\"txtNewUsername\" value=\""+row3["UserId"].ToString()+" \"/><br>");
            sb2.AppendLine("<br>");
            sb2.AppendLine("Password:");
            sb2.AppendLine("<input type=\"password\" id=\"txtNewpwd\" name=\"txtNewpwd\"  value=\"" + row3["UserPassword"].ToString() +"\" /><br>");
            sb2.AppendLine("<br>");
            sb2.AppendLine("Re-Type Password:");
            sb2.AppendLine("<input type=\"password\" id=\"txtREpwd\" name=\"txtREpwd\"  value=\"" + row3["UserPassword"].ToString() +"\"  /><br>");
            sb2.AppendLine("<br>");
            sb2.AppendLine("Full Name:");
            sb2.AppendLine("<input type=\"text\" id=\"txtFullName\" name=\"txtFullName\" value=\"" + row3["UserFullName"].ToString() +"\" /><br>");
            sb2.AppendLine("<br>");
                }
            }
            btnSaveUser.CssClass = "class_btnUpdate";
            div_UserInfo.InnerHtml = sb2.ToString();
        }
        #endregion

        #region delete
        if (Request["DeleteUser"] != null)
        {
            Del_UserCms(Convert.ToInt32(Request["DeleteUser"]));
            Response.Redirect("mnt_Administrator.aspx");
        }
        #endregion

    }
    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Administrator.aspx?NewUser=true");
    }
    protected void btnCancelUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Administrator.aspx");
    }
    protected void btnSaveUser_Click(object sender, EventArgs e)
    {
        string username = Request["txtNewUsername"];
        string password = Request["txtNewpwd"];
        string name = Request["txtFullName"];
        if (Convert.ToString(Request["txtREpwd"]) == password)
        {
            if (Request["EditUser"] == null)
            {
                int savesu = AddUser(username, password, name, 2);
                if (savesu != 0)
                {
                    //div_newerror.Visible = true;
                }
                else
                {
                    Response.Redirect("mnt_Administrator.aspx");
                }
            }
            else
            {
                Upd_UserCMS(Convert.ToInt32(Request["EditUser"]), username, password, name);
                Response.Redirect("mnt_Administrator.aspx");
            }
        }
        else
        {
            lbError.Text = "Verify Your Password";
        }

    }
}
