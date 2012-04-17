using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Login
/// </summary>
public class Login
{
    //Login Id
    //-- 
    //protected internal static int LoginID;
    //protected internal static string LoginGUID;
    //--
    //private int LoginID;
    private string Fullname;
    private string Firstname;
    private string Lastname;
    private string Zipcode;
    private string Authenticate;

    // Added June 10, 2009 by Jordan Sherer
    private string UserLoginName;

    //public int _LoginID
    //{
    //    get { return LoginID; }
    //    //set { LoginID = value; }
    //}
    public int _LoginID
    {
        get { return getLoginId(); }
    }

    // Added June 10, 2009 by Jordan Sherer
    public string _UserLoginName
    {
        get { return UserLoginName; }
    }

    public string _Fullname
    {
        get { return Fullname; }
        //set { Fullname = value; }
    }
    public string _Firstname
    {
        get { return Firstname; }
        //set { Firstname = value; }
    }
    public string _Lastname
    {
        get { return Lastname; }
        //set { Lastname = value; }
    }
    public string _Zipcode
    {
        get { return Zipcode; }
        //set { Zipcode = value; }
    }
    public string _Authenticate
    {
        get { return Authenticate; }
        //set { Authenticate = value; }
    }

    public Login()
    {

    }
    protected internal int getLoginId()
    {
        int loginid;
        if (HttpContext.Current.Session[SiteConstants.LoginId] != null && HttpContext.Current.Session[SiteConstants.LoginId].ToString() != "")
            loginid = Convert.ToInt32(HttpContext.Current.Session[SiteConstants.LoginId].ToString());
        else
            loginid = 0;
        return loginid;
    }
    protected internal string getLoginGUID()
    {
        string loginguid;
        if (HttpContext.Current.Session[SiteConstants.LoginGUID] != null && HttpContext.Current.Session[SiteConstants.LoginGUID].ToString() != "")
        loginguid =  HttpContext.Current.Session[SiteConstants.LoginGUID].ToString();
        else
        loginguid = "";

        return loginguid;
    }

    public bool UserLogin(string UserID, string Password)
    {
        bool validLogin = false;
        DataSet dsLogin = new DataSet();
        string Token = "D2AED9027CAC4E90BF0A0603995D0ABE";
        ws_Login.LogMeSecure logins = new ws_Login.LogMeSecure();
        //--
        dsLogin = logins.SecUserInfo(UserID, Password,Token);
        //--
        foreach (DataTable table in dsLogin.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                Authenticate = row["Authenticated"].ToString();

                if (Authenticate.ToUpper() == "YES")
                {
                    // START: Added June 10, 2009 by Jordan Sherer
                    UserLoginName = UserID;
                    // END

                    Fullname = row["FullName"].ToString();
                    Zipcode = row["ZipCode"].ToString();
                    Firstname = row["FirstName"].ToString();
                    Lastname = row["LastName"].ToString();
                    //LoginID = Convert.ToInt32(row["LoginID"].ToString());
                    //LoginGUID = row["LoginGUID"].ToString();
                    validLogin = true;
                    HttpContext.Current.Session[SiteConstants.LoginId] = Convert.ToInt32(row["LoginID"].ToString());
                    HttpContext.Current.Session[SiteConstants.LoginGUID] = row["LoginGUID"].ToString();
                }
                else
                {
                    validLogin = false;
                }
            }
        }
        logins.Dispose();
        dsLogin = null;
        return validLogin;
    }
    public void UserLogout()
    {
        ws_Login.LogMeSecure logins = new ws_Login.LogMeSecure();
        string Token = "D2AED9027CAC4E90BF0A0603995D0ABE";
        int iLogout = logins.SecUserLogOut(getLoginId(), getLoginGUID(),Token);
        logins.Dispose();
        //-- Clear Variables
        UserLoginName = "";
        Fullname = "";
        Zipcode = "";
        Firstname = "";
        Lastname = "";
        HttpContext.Current.Session[SiteConstants.LoginId] = null;
        HttpContext.Current.Session[SiteConstants.LoginGUID] = null;
        //-- Clear Session
        HttpContext.Current.Session[SiteConstants.UserLoginName] = null;
        HttpContext.Current.Session[SiteConstants.UserFullName] = null;
        HttpContext.Current.Session[SiteConstants.UserFirstName] = null;
        HttpContext.Current.Session[SiteConstants.UserLastName] = null;
        HttpContext.Current.Session[SiteConstants.UserValidLogin] = false;
        HttpContext.Current.Session[SiteConstants.UserZipCode] = null;
    }
    public void Dispose()
    {
        Fullname = "";
        Zipcode = "";
        Firstname = "";
        Lastname = "";
        //LoginID = 0;.
        HttpContext.Current.Session[SiteConstants.LoginId] = null;
        HttpContext.Current.Session[SiteConstants.LoginGUID] = null;
    }
}
