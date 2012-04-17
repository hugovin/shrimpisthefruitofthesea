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
/// Summary description for Header
/// </summary>
public class Header
{
    private int SiteId = 0;
    private int ContId = 0;
    public Header()
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

	
    //----------------------------------------------------
    public DataSet getAllContentGroups(){
        ContentGroup content = new ContentGroup();
        return content.getAllContentGroups(SiteId);
    }
    public DataSet getAllAdds() {
        Adds adds = new Adds();
        
        return adds.getAdds(SiteId,ContId,1);//--1 by  the SIte
    }
    

}
