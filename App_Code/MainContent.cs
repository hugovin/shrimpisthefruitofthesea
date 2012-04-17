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
/// Summary description for MainContent
/// </summary>
public class MainContent
{
    private int SiteId = 0;
    private int ContId = 0;
    public MainContent()
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
            //ContId = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
             try
            {
                ContId = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
            }
            catch (Exception ex) { ContId = 1; }
        }
        else
        {
            ContId = 1;
        }
    }

    public DataSet getAllFeature()
    {
        //DataSet dataset = new DataSet();
        Featured content = new Featured();
        return content.getAllFeature(SiteId, ContId);
    }
	public DataSet getAllFeatureHome()
    {
        //DataSet dataset = new DataSet();
        Featured content = new Featured();
        return content.getAllFeatureHome(SiteId, ContId);
    }
    public DataSet Get_Site_WhatsNew() {
        WhatsNew whatsnew = new WhatsNew();
        return whatsnew.Get_Site_WhatsNew(SiteId, ContId);
    }

    public DataSet Get_Site_BestSellers() {
        BestSellers bestsellers = new BestSellers();
        return bestsellers.Get_Site_BestSellers(SiteId, ContId);
    }
    public DataSet Get_Site_FeaturedProduct() {
        FeaturedProducts featuredproducts = new FeaturedProducts();
        return featuredproducts.Get_Site_FeaturedProduct(SiteId, ContId);
    }
    public DataSet Get_Site_FeaturedBrands()
    {
        int SubjId = 0;
        
        FeatureBrands featuredbrands = new FeatureBrands();
        return featuredbrands.Get_Site_FeaturedBrands(SiteId, ContId);
    }
    public DataSet Get_Site_HighLights()
    {

        Highlights highlights = new Highlights();
        return highlights.getAllHighlights(SiteId, ContId);
    }

}
