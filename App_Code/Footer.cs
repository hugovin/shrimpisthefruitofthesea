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
/// Summary description for Footer
/// </summary>
public class Footer
{
    private int SiteId = 0;
    private int ContId = 0;
    public Footer()
    {
        if (HttpContext.Current.Session["SiteId"] != null)
        {
            SiteId = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
        }
        else
        {
            SiteId = Convert.ToInt32(Global.globalSiteId);
        }
        if (HttpContext.Current.Session["ContentId"] != null)
        {
            ContId = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
        }
        else
        {
            ContId = 1;
        }
    }
	
    public DataSet GetAllSiteContact()
    {
        Site site = new Site();
        return site.GetAllSiteContact(SiteId);
    }
    public DataSet Get_LeftMenu_All_ResourceCenter()
    {
        Generics generics = new Generics();
        return generics.Get_Site_All_GenericByType(SiteId, ContId, 1);
        //generics.Dispose();
    }

    public DataSet Get_LeftMenu_All_AboutUs()
    {
        Generics generics = new Generics();
        return generics.Get_Site_All_GenericByType(SiteId, 0, 2);
       // generics.Dispose();
    }
    public DataSet GetSiteInformation()
    {
        Site site = new Site();
        return site.Get_Site_Information(SiteId);
    }
}
