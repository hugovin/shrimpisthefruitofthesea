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
/// Summary description for ucCMSSitemap
/// </summary>
public class ucCMSSitemap:ConnectSql
{
	public ucCMSSitemap()
	{
        
        
	   
	}
    public DataSet getallHiglights(int siteid, int contid)
    {
        Highlights high = new Highlights();
        return high.getAllHighlights(siteid, contid);
    }

    public DataSet getallFeaturebrands(int siteid, int contid)
    {
        FeatureBrands feat = new FeatureBrands();
        return feat.getAllFeatureBrands(siteid, contid);
    }
    public DataSet getbrandTitle(int title)
    {
        FeatureBrands feat = new FeatureBrands();
        return feat.getFeatureBrandsById(title);
    }
    public DataSet getAlltheater(int siteid, int contid)
    {
        Featured feat = new Featured();
        return feat.getAllFeature(siteid, contid);
    }

    //-----------------------------------------------------
    public DataSet Get_LeftMenu_All_ResourceCenter(int siteid,int contid)
    {
        Generics generics = new Generics();
        return generics.Get_Site_All_GenericByType(siteid, contid, 1);
    }

    public DataSet Get_LeftMenu_All_AboutUs(int Siteid)
    {
        Generics generics = new Generics();
        return generics.Get_Site_All_GenericByType(Siteid, 0, 2);
        // generics.Dispose();
    }

}
