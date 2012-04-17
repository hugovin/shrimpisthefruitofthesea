using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Web.SessionState;

public class CartUsers
{
    public CartUsers() { }

    public static int GetLoginID()
    {
        Login l = new Login();
        return l._LoginID;
    }

    public static string GetUserName(HttpSessionState Session)
    {
        return Helper.IsString(Session[SiteConstants.UserLoginName], "");
    }

    public static bool IsUserLoggedIn(HttpSessionState Session)
    {
        return Helper.StringExists(GetUserName(Session)) && (bool)Session[SiteConstants.UserValidLogin] == true;
    }

    public static bool IsAuthenticUser(string user, string pass)
    {
        return false;
    }
}
