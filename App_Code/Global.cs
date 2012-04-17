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
/// Summary description for Global
/// </summary>
public static class Global
{
    static string _globalSocialTwist;
    static string _globalSiteId;
    static string _globalSiteNameAbr;
    static string _globalSiteImagesPath;
    static string _globalSiteStylePath;
    static string _globalMySiteUrl;
    static string _globalTeatherFlash;
	static string _globalGAscript;
	static string _globalSiteNameD;

    /// <summary>
    /// Get or set the static important data.
    /// </summary>
    ///
    public static string globalGAscript
    {
        get
        {
            string pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (pageUrl.IndexOf(SiteConstants.globalSiteSbName) > 0)
            {
                _globalGAscript = SiteConstants.globalGASBscript;
            }
            else
            {
                _globalGAscript = SiteConstants.globalGAErscript;
            }
            return _globalGAscript;
        }
        set
        {
            _globalGAscript = value;
        }
    }
    
    public static string globalTeatherFlash
    {
        get
        {
            string pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (pageUrl.IndexOf(SiteConstants.globalSiteSbName) > 0)
            {
                _globalTeatherFlash = SiteConstants.globalTeatherFlashSb;
            }
            else
            {
                _globalTeatherFlash = SiteConstants.globalTeatherFlashEr;
            }
            return _globalTeatherFlash;
        }
        set
        {
            _globalTeatherFlash = value;
        }
    }

     public static string globalMySiteUrl
    {
        get
        {
            string pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (pageUrl.IndexOf(SiteConstants.globalSiteSbName) > 0)
            {
                _globalMySiteUrl = SiteConstants.globalMySiteUrlSb;
            }
            else
            {
                _globalMySiteUrl = SiteConstants.globalMySiteUrlEr;
            }
            return _globalMySiteUrl;
        }
        set
        {
            _globalMySiteUrl = value;
        }
    }
    public static string globalSocialTwist
    {
        get
        {
            string pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (pageUrl.IndexOf(SiteConstants.globalSiteSbName) > 0)
            {
                _globalSocialTwist = SiteConstants.SocialTwistScriptSB;
            }
            else
            {
                _globalSocialTwist = SiteConstants.SocialTwistScript;
            }
            return _globalSocialTwist;
        }
        set
        {
            _globalSocialTwist = value;
        }
    }
    public static string globalSiteId
    {
        get
        {
            string pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (pageUrl.IndexOf(SiteConstants.globalSiteSbName) > 0)
            {
                _globalSiteId = SiteConstants.globalSiteSbSiteId;
            }
            else
            {
                _globalSiteId = SiteConstants.globalSiteErSiteId;
            }
            return _globalSiteId;
        }
        set
        {
            _globalSiteId = value;
        }
    }

    public static string globalSiteNameAbr
    {
        get
        {
            string pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (pageUrl.IndexOf(SiteConstants.globalSiteSbName) > 0)
            {
                _globalSiteNameAbr = SiteConstants.globalSiteSbNameAbr;
            }
            else
            {
                _globalSiteNameAbr = SiteConstants.globalSiteErNameAbr;
            }
            return _globalSiteNameAbr;
        }
        set
        {
            _globalSiteNameAbr = value;
        }
    }
    public static string globalSiteNameD
    {
        get
        {
            string pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (pageUrl.IndexOf(SiteConstants.globalSiteSbName) > 0)
            {
                _globalSiteNameD = SiteConstants.globalSiteSbName;
            }
            else
            {
                _globalSiteNameD = SiteConstants.globalSiteErName;
            }
            return _globalSiteNameD;
        }
        set
        {
            _globalSiteNameD = value;
        }
    }
    public static string globalSiteImagesPath
    {
        get
        {
            string pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (pageUrl.IndexOf(SiteConstants.globalSiteSbName) > 0)
            {
                _globalSocialTwist = SiteConstants.SocialTwistScriptSB;
                _globalSiteImagesPath = SiteConstants.globalSiteSbImages;
            }
            else
            {
                _globalSocialTwist = SiteConstants.SocialTwistScript;
                _globalSiteImagesPath = SiteConstants.globalSiteErImages;
            }
            return _globalSiteImagesPath;
        }
        set
        {
            _globalSiteImagesPath = value;
        }
    }
    public static string globalSiteStylePath
    {
        get
        {
            string pageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (pageUrl.IndexOf(SiteConstants.globalSiteSbName) > 0)
            {
                _globalSiteStylePath = SiteConstants.globalSiteSbStyles;
            }
            else
            {
                _globalSiteStylePath = SiteConstants.globalSiteErStyles;
            }
            return _globalSiteStylePath;
        }
        set
        {
            _globalSiteStylePath = value;
        }
    }


}
