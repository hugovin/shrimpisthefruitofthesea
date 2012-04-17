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
/// Summary description for SiteWhatsNew
/// </summary>
public class SiteWhatsNew
{
    private int SiteId = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
    private int ContId = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
    WhatsNew whatsNew = new WhatsNew();

	public SiteWhatsNew()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataSet Get_All_Whats_New()
    {
        return whatsNew.Get_All_Whats_New_Product(SiteId, ContId);
    }
}
