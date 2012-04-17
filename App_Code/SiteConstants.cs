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
/// Summary description for SiteConstants
/// </summary>
public class SiteConstants
{
    public const string LoginId = "LoginId";
    public const string LoginGUID = "LoginGUID";
    public const string UserId = "UserId";
    // START: Added June 10, 2009 by Jordan Sherer
    public const string UserLoginName = "UserLoginName";
    // END
    public const string UserFullName = "UserFullName";
    public const string UserFirstName = "UserFirstName";
    public const string UserLastName = "UserLastName";
    public const string UserValidLogin = "UserValidLogin";
    public const string UserZipCode = "UserZipCode";
    public const string SocialTwistScript = "<script type=\"text/javascript\" src=\"http://cdn.socialtwist.com/2009060418608/script.js\"></script><a class=\"st-taf\" href=\"http://tellafriend.socialtwist.com:80\" onclick=\"return false;\" style=\"border:0;padding:0;margin:0;\"><img alt=\"SocialTwist Tell-a-Friend\" style=\"border:0;padding:0;margin:0;\" src=\"http://images.socialtwist.com/2009060418608/button.png\" onmouseout=\"STTAFFUNC.hideHoverMap(this)\" onmouseover=\"STTAFFUNC.showHoverMap(this, '2009060418608', window.location, document.title)\" onclick=\"STTAFFUNC.cw(this, {id:'2009060418608', link: window.location, title: document.title });\"/></a>";
    public const string SocialTwistScriptSB = "<script type=\"text/javascript\" src=\"http://cdn.socialtwist.com/2009060418608-3/script.js\"></script><a class=\"st-taf\" href=\"http://tellafriend.socialtwist.com:80\" onclick=\"return false;\" style=\"border:0;padding:0;margin:0;\"><img alt=\"SocialTwist Tell-a-Friend\" style=\"border:0;padding:0;margin:0;\" src=\"http://images.socialtwist.com/2009060418608-3/button.png\"onmouseout=\"STTAFFUNC.hideHoverMap(this)\" onmouseover=\"STTAFFUNC.showHoverMap(this, '2009060418608-3', window.location, document.title)\" onclick=\"STTAFFUNC.cw(this, {id:'2009060418608-3', link: window.location, title: document.title });\"/></a>";

    public const string globalGAErscript = "<script type=\"text/javascript\">var gaJsHost = ((\"https:\" == document.location.protocol) ? \"https://ssl.\" : \"http://www.\");document.write(unescape(\"%3Cscript src='\" + gaJsHost + \"google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E\"));</script><script type=\"text/javascript\">try {var pageTracker = _gat._getTracker(\"UA-1311046-30\");pageTracker._trackPageview();} catch(err) {}</script>";
    public const string globalGASBscript = "<script type=\"text/javascript\">var gaJsHost = ((\"https:\" == document.location.protocol) ? \"https://ssl.\" : \"http://www.\");document.write(unescape(\"%3Cscript src='\" + gaJsHost + \"google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E\"));</script><script type=\"text/javascript\">try {var pageTracker = _gat._getTracker(\"UA-1311046-38\");pageTracker._trackPageview();} catch(err) {}</script>";

    public static string imagesPath = ConfigurationManager.AppSettings["SiteImagesPath"].ToString();
    public static string imagesPathTb = ConfigurationManager.AppSettings["SiteImagesPathThumb"].ToString();
    public static string imagesPathBig = ConfigurationManager.AppSettings["SiteImagesPathBig"].ToString();
    public static string imagesPathAdditional = ConfigurationManager.AppSettings["SiteImagesPathAdditional"].ToString();
    
    //--BreadCrumbVariable
        public static string breadCrumb = "";
    //--
    //-- Left Menu Active
        public static int LeftMenuActive = 4;
    //--
    //-- Site Information
        //Properties
        private static string _site_TagLine;
        private static string _site_Url;
        private static string _site_Name;
        //public static string SiteName = "";
        public static string SiteTagLine = "";
        //public static string SiteUrl = "";
    //--
    public const string globalMySiteUrlEr = "my.edresources.com";
    public const string globalMySiteUrlSb = "my.sunburst.com";
    public const string globalSiteErName = "edresources";
    public const string globalSiteErNameAbr = "ER";
    public const string globalSiteSbName = "sunburst";
    public const string globalSiteSbNameAbr = "SB";

    public const string globalSiteErImages = "images";
    public const string globalSiteErStyles = "css";
    public const string globalSiteErSiteId = "1";
    public const string globalSiteSbImages = "images2";
    public const string globalSiteSbStyles = "css2";
    public const string globalSiteSbSiteId = "2";

    public const string globalTeatherFlashSb = "SunBurst.swf";
    public const string globalTeatherFlashEr = "EducationResources.swf";

    public static string SiteName
    {
        get
        {
            LoadSite();
            return _site_Name;
        }
        set
        {
            _site_Name = value;
        }
    }
    public static string SiteUrl
    {
        get
        {
            LoadSite();
            return _site_Url;
        }
        set
        {
            _site_Url = value;
        }
    }

    public SiteConstants()
    {

        LoadSite();
        
    }
    private static void LoadSite()
    {
        Footer footer = new Footer();
        DataSet dsSite = new DataSet();

        dsSite = footer.GetSiteInformation();
        foreach (DataTable table in dsSite.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                //_site_Phone = row["sitephone"].ToString();
                //_site_Copy = row["sitecopy"].ToString();
                _site_Name = row["sitename"].ToString();
                //_site_Description = row["sitedescription"].ToString();
                _site_TagLine = row["sitetagline"].ToString();
                _site_Url = row["siteurl"].ToString();
                //_site_Privacy = row["siteprivacy"].ToString();
                //_site_Term = row["siteterm"].ToString();
                //_site_keyWords = row["SiteKeyWords"].ToString();
            }
        }


        dsSite = null;

    }
}
