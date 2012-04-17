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

public partial class CMS_Login : UserCMS
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Convert.ToBoolean(Session["authenticated"]) == true)
        {
            Session["authenticated"] = false;
            Session["siteId"] = null;
            Session["contId"] = null;
        }
        Session["CurrentPage"] = "CMS_Login.aspx";
         #region First load with or without errors
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("Username:");
        sb.AppendLine("<input type=\"text\" width=\"50px\" id=\"txtUsername\" name=\"txtUsername\"/>");
        sb.AppendLine("<br>");
        sb.AppendLine("<br>");
        sb.AppendLine("Password:");
        sb.AppendLine("<input id=\"txtpwd\" name=\"txtpwd\" type=\"password\" width=\"50px\" />");
        
        if (Request["LoginError"] != null)
        {
            sb.AppendLine("<br>"); sb.AppendLine("<br>");
            sb.AppendLine("Error!!! Please Verify your username and password");
            sb.AppendLine("<br>");
        }

        login.InnerHtml = sb.ToString();
        #endregion
       

    }
    //------------------------------------------------------------
    //------------------------------------------------------------
    private UserCMS Validation(string username, string pwd, bool proceadure)
    {
        DataSet data = new DataSet();
        UserCMS user = new UserCMS();
        data = VerifyUserPwd(username, pwd);        
        if (data != null)
        {
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row.ItemArray.Length > 2)
                    {
                        return user = new UserCMS(Convert.ToString(row["UserId"]), Convert.ToString(row["UserPassword"]), Convert.ToString(row["UserFullName"]), Convert.ToInt32(row["UserType"]), Convert.ToBoolean(row["UserState"]));
                    }
                }
            }
        }
       
        return user;
    }
    //------------------------------------------------------------

    //------------------------------------------------------------
    protected void btnSaveNewUser_Click(object sender, EventArgs e)
    {
        //string username = Request["txtNewUsername"];
        //string password = Request["txtNewpwd"];
        //string name = Request["txtFullName"];
        //int access = Convert.ToInt32(Request["txtAccessLevel"]);
        //int state = Convert.ToInt32(Request["txtUserState"]);
        //int savesu = AddUser(username, password, name, state);
        //if (savesu != 0)
        //{
        //    div_newerror.Visible = true;
        //}
        //else 
        //{ Response.Redirect("CMS_Login.aspx"); }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CMS_Login.aspx");
    }

    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
        string username = Request["txtUsername"];
        string password = Request["txtpwd"];
        UserCMS usern = Validation(username, password, true);
        Session["authenticated"] = true;
        Session["userid"] = usern.strUserId;
        Session["userfullname"] = usern.strUserFullName;
        Session["userType"] = usern.intUserType;



        if (usern.strUserId == "")
        {
            Response.Redirect("CMS_Login.aspx?LoginError=true");
        }
        else
        {

            Response.Redirect("CMS_SiteSelection.aspx?Session=" + Session.SessionID);
        }

    }
}

