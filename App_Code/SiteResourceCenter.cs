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
/// Summary description for SiteResourceCenter
/// </summary>
public class SiteResourceCenter
{
    private int SiteId = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
    private int ContId = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
    ResourceCenter resourceCenter = new ResourceCenter();

	public SiteResourceCenter()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataSet Get_All_Resource_Center()
    {
        return resourceCenter.Get_ResourceCenter_MP(SiteId, ContId);
    }


}
