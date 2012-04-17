using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Text.RegularExpressions;

public partial class home : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string strFolder = "images";
    private string strRemoteImages = SiteConstants.imagesPath;
    private string strRemoteImagesTn = SiteConstants.imagesPathTb + "tn_";
    //--
    Addins addins = new Addins();
    //--
    private void GetVars()
    {
        //---
        if (Session["SiteId"] != null)
            SiteId = Session["SiteId"].ToString();

        ContentId = Request["ci"];
        if (ContentId != null)
	{
            Session["ContentId"] = ContentId;
            List<string> keys = new List<string>();

            // retrieve application Cache enumerator
            IDictionaryEnumerator enumerator = Cache.GetEnumerator();

            // copy all keys that currently exist in Cache
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }

            // delete every key from cache
            for (int i = 0; i < keys.Count; i++)
            {
                Cache.Remove(keys[i]);
            }
	}
    else
	{
        ContentId = Session["ContentId"].ToString();
	}
        //--
        if (Session["CurrentChilPage"] != null)
            CurrentChilPage = Session["CurrentChilPage"].ToString();
        else
            CurrentChilPage = "home.aspx";
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        SiteConstants.breadCrumb = ""; //Clean the Variable
        Session["CurrentChilPage"] = "home.aspx";
        //Seting Master Page Left Menu
        SiteConstants.LeftMenuActive = 4;
        Main_MasterPage MasterPage = (Main_MasterPage)Page.Master;
        MasterPage.LeftMenu_Finder = true;
        MasterPage.LeftMenu_Finder = true;
        MasterPage.LeftMenu_Subject = true;
        MasterPage.LeftMenu_Browse = true;
        MasterPage.LeftMenu_Resourcecenter = true;
        MasterPage.LeftMenu_Aboutus = false;
        MasterPage.pageTitleBar = "Homepage - " + MasterPage.pageTitleBar;

        LoadFeatured();
        LoadCentralTabs();
        LoadBrands();
        LoadHighLights();
    }

    protected void LoadFeatured()
    {
        //MainContent feature = new MainContent();
        //DataSet dsfeature = new DataSet();
        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.Text.StringBuilder sbImages = new System.Text.StringBuilder();
        //System.Text.StringBuilder sbLink = new System.Text.StringBuilder();
        //---
        //int cont = 0;
        //dsfeature = feature.getAllFeature();
        /*foreach (DataTable table in dsfeature.Tables)
        {
            sb.AppendLine("<data>");
            foreach (DataRow row in table.Rows)
            {
                cont++;
                sb.AppendLine("<img>");
                sb.AppendLine("<src>" + strFolder + "/" + row["FeatFile"].ToString() + "</src>");
                sb.AppendLine("<title>" + row["FeatTitle"].ToString() + "</title>");
                if ((row["FeatLink"].ToString().IndexOf("http")) ==0)
                sb.AppendLine("<a href=\"" + row["FeatLink"].ToString() + "\" target=\"_blank\" /> ");
                else
                sb.AppendLine("<a href=\"" + row["FeatLink"].ToString() + "\" target=\"_self\" /> ");

                sb.AppendLine("</img>");
                if (cont == 5) break;
            }
            sb.AppendLine("</data>");
        }*/
        //Cache.Remove("Data.xml");
        //addins.Write_xml(sb, Request.PhysicalApplicationPath + "xml", "Data.xml");

        sbImages.AppendLine("<div id=\"slideshow\">");
        sbImages.AppendLine("<div id=\"altContent\">");
        sbImages.AppendLine("<p><a href=\"http://www.adobe.com/go/getflashplayer\"><img src=\"http://www.adobe.com/" + Global.globalSiteImagesPath + "/shared/download_buttons/get_flash_player.gif\" ");
        sbImages.AppendLine("	alt=\"Get Adobe Flash player\" /></a></p>");
        sbImages.AppendLine("</div>");
        sbImages.AppendLine("</div>");
        PlaceHolder_slideshow.Controls.Add(new LiteralControl(sbImages.ToString()));
        //this.URLRedirect();

    }

    protected void LoadCentralTabs()
    {
        StringBuilder sb = new StringBuilder();
        int cont = 0;
        Boolean plusSeeMore = true;
        MainContent maincontent = new MainContent();
        DataSet dswhatsnew = new DataSet();
        dswhatsnew = maincontent.Get_Site_WhatsNew();
        //--SiteId  ContId  whatid  TitleId titleid  title  imagetn  webdesc50  webdesc
        //--
        sb.AppendLine("<div id=\"demo-block\" >");
        sb.AppendLine("<h4>What's New</h4>");
        sb.AppendLine("<div>");

        if (dswhatsnew == null || dswhatsnew.Tables[0].Rows.Count == 0)
            plusSeeMore = false;
            
		if (dswhatsnew != null){
	        foreach (DataTable table in dswhatsnew.Tables)
	        {
	            foreach (DataRow row in table.Rows)
	            {
	                if (cont >= 1) sb.AppendLine("<div class=\"tabDivider\"></div>");
	                sb.AppendLine("<div class=\"product\">");
	                sb.AppendLine("<div id=\"boxContImageTheather\" style=\"width:115px; height:115px;\">");
	                sb.AppendLine("<img id=\"images\" style=\"width: 115px; height:115px;\" title=\"" + row["pubname"].ToString() +" : " +row["title"].ToString() + "\" src=\"" + strRemoteImagesTn + row["imagetn"].ToString() + "\"/>");//onload=\"getDim(document.getElementById('boxContImageTheather'),this)\"
	                sb.AppendLine("</div>");               
	                sb.AppendLine("<h2><a href=\"product.aspx?p=" + row["titleid"].ToString() + "\">" + row["title"].ToString() + "</a></h2>");
	                sb.AppendLine("<p><em>by</em>: <a href=\"result.aspx?findopt5=" + row["PubId"].ToString() + "&am=1&asm=" + 3 + "\" style='color:#81B5F4; text-decoration:underline;'>" + row["pubname"].ToString() + "</a></p>");
	                //sb.AppendLine("<p>" + addins.cutDescription(addins.StripHTML(row["webdesc"].ToString()), 50) + "...</p>");
	                sb.AppendLine("</div>");
	                cont++;
	            }
	        }
        }

        if (plusSeeMore == true)
            sb.AppendLine("<div class=\"seeMore\"><a href=\"WhatsNew.aspx?cp=5\">+ see more</a></div>");

        sb.AppendLine("</div>");

        //PlaceHolder_AboutUs.Controls.Add(new LiteralControl(sb.ToString()));
        dswhatsnew = null;

        //--
        cont = 0;
        plusSeeMore = true;
        DataSet dsfeaturedproduct = new DataSet();
        dsfeaturedproduct = maincontent.Get_Site_FeaturedProduct();
        //--SiteId      ContId      SubjId      FeatPro     TitleId     titleid     title     
        sb.AppendLine("<h4>Featured Products</h4>");
        sb.AppendLine("<div>");

        if (dsfeaturedproduct.Tables[0].Rows.Count == 0)
            plusSeeMore = false;

        foreach (DataTable table in dsfeaturedproduct.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (cont >= 1) sb.AppendLine("<div class=\"tabDivider\"></div>");
                sb.AppendLine("<div class=\"product\">");
                sb.AppendLine("<div id=\"boxContImageTheather\"  style=\"width:115px; height:115px;\">");
                sb.AppendLine("<img id=\"images\" style=\"width: 115px;height:115px;\" title=\"" + row["pubname"].ToString() +" : "+row["title"].ToString() + "\" src=\"" + strRemoteImagesTn + row["imagetn"].ToString() + "\" />");     //onload=\" (document.getElementById('boxContImageTheather'),this)\"           
                sb.AppendLine("</div>");
                sb.AppendLine("<h2><a href=\"product.aspx?p=" + row["titleid"].ToString() + "\">" + row["title"].ToString() + "</a></h2>");
                sb.AppendLine("<p><em>by</em>: <a href=\"result.aspx?findopt5=" + row["PubId"].ToString() + "&am=1&asm=" + 3 + "\" style='color:#81B5F4; text-decoration:underline;'>" + row["pubname"].ToString() + "</a></p>");
                //sb.AppendLine("<p>" + addins.cutDescription(addins.StripHTML(row["webdesc"].ToString()), 50) + "...</p>");
                sb.AppendLine("</div>");
                cont++;
            }

        }

        if (plusSeeMore == true)
            sb.AppendLine("<div class=\"seeMore\"><a href=\"classification.aspx?cp=1\">+ see more</a></div>");
        
        sb.AppendLine("</div>");

        //PlaceHolder_AboutUs.Controls.Add(new LiteralControl(sb.ToString()));
        dsfeaturedproduct = null;
        //--
        cont = 0;
        plusSeeMore = true;
        DataSet dsbestsellers = new DataSet();
        dsbestsellers = maincontent.Get_Site_BestSellers();
        //--SiteId      ContId      SubjId      BestId      TitleId     titleid     title  
        sb.AppendLine("<h4>Best Sellers</h4>");
        sb.AppendLine("<div>");


        if (dsbestsellers.Tables[0].Rows.Count == 0)
            plusSeeMore = false;

        foreach (DataTable table in dsbestsellers.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (cont >= 1) sb.AppendLine("<div class=\"tabDivider\"></div>");
                sb.AppendLine("<div class=\"product\">");
                sb.AppendLine("<div id=\"boxContImageTheather\"  style=\"width:115px; height:115px;\">");
                sb.AppendLine("<img id=\"images\" style=\"width: 115px;height:115px;\" title=\"" + row["pubname"].ToString() + " : " +row["title"].ToString() + "\"  src=\"" + strRemoteImagesTn + row["imagetn"].ToString() + "\" />");    //onload=\"getDim(document.getElementById('boxContImageTheather'),this)\"            
                sb.AppendLine("</div>");
                sb.AppendLine("<h2><a href=\"product.aspx?p=" + row["titleid"].ToString() + "\">"+ (row["title"].ToString()) + "</a></h2>");
                sb.AppendLine("<p><em>by</em>:<a href=\"result.aspx?findopt5=" + row["PubId"].ToString() + "&am=1&asm=" + 3 + "\" style='color:#81B5F4; text-decoration:underline;'>" + row["pubname"].ToString() + "</a></p>");
                //sb.AppendLine("<p>" + addins.cutDescription(addins.StripHTML(row["webdesc"].ToString()), 50) + "...</p>");
                sb.AppendLine("</div>");
                cont++;
            }
        }

        if (plusSeeMore == true)
            sb.AppendLine("<div class=\"seeMore\"><a href=\"classification.aspx?cp=3\">+ see more</a></div>");

        sb.AppendLine("</div>");
        sb.AppendLine("</div>");
        PlaceHolder_demo_block.Controls.Add(new LiteralControl(sb.ToString()));
        dsbestsellers = null;
        sb = null;

    }

    protected void LoadBrands()
    {
        StringBuilder sb = new StringBuilder();
        //StringBuilder sbimages = new StringBuilder();
        //MainContent maincontent = new MainContent();
        //DataSet dsbrands = new DataSet();
        //dsbrands = maincontent.Get_Site_FeaturedBrands();
        /*foreach (DataTable table in dsbrands.Tables)
        {
            sbimages.AppendLine("<data>");
            foreach (DataRow row in table.Rows)
            {
                sbimages.AppendLine("<img>");               
                sbimages.AppendLine("<link>result.aspx?findopt5=" + row["PubId"].ToString() + "&am=1&asm=" + 3  + "</link>");
                sbimages.AppendLine("<src>" + strFolder + "/" + row["featfile"].ToString() + "</src>");
                sbimages.AppendLine("<title>" + row["pubname"].ToString() + "</title>");
                sbimages.AppendLine("</img>");
            }
        } sbimages.AppendLine("</data>");*/
        //Cache.Remove("carouselData.xml");
        //addins.Write_xml(sbimages, Request.PhysicalApplicationPath + "xml", "carouselData.xml");


        sb.AppendLine("<p><a href=\"http://www.adobe.com/go/getflashplayer\"><img ");
        sb.AppendLine("src=\"http://www.adobe.com/" + Global.globalSiteImagesPath + "/shared/download_buttons/get_flash_player.gif\" ");
        sb.AppendLine("alt=\"Get Adobe Flash player\" /></a></p>");

        PlaceHolder_Brands.Controls.Add(new LiteralControl(sb.ToString()));
        //dsbrands = null;
        sb = null;


    }

    protected void LoadHighLights()
    {
        StringBuilder sb = new StringBuilder();
        MainContent maincontent = new MainContent();
        DataSet dshigh = new DataSet();
        int cont = 0;
        dshigh = maincontent.Get_Site_HighLights();
        string path = "";
        foreach (DataTable table in dshigh.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (!Regex.IsMatch(row["highfile"].ToString(), @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
                {
                    path = strFolder + "/";
                }
                else
                {
                    path = "";
                }
                switch (cont)
                {
                    case 0:
                        //sb.AppendLine("<div class=\"blueBox\"><img src=\"" + strFolder + "/" + row["highfile"].ToString() + "\" width=\"244\" height=\"169\" /></div>");
                        sb.AppendLine("<div class=\"blueBox\"><a href=\"" + row["highlink"].ToString() + "\"><img src=\"" + path + row["highfile"].ToString() + "\" width=\"244\" height=\"169\" title=\"" + row["highAlt"].ToString() + "\" /></a></div>");
                        break;
                    case 1:
                        //sb.AppendLine("<div class=\"helpBox\"><img src=\"" + strFolder + "/" + row["highfile"].ToString() + "\" width=\"243\" height=\"169\" /></div>");
                        sb.AppendLine("<div class=\"helpBox\"><a href=\"" + row["highlink"].ToString() + "\"><img src=\"" + path + row["highfile"].ToString() + "\" title=\"" + row["highAlt"].ToString() + "\"  width=\"243\" height=\"169\" /></a></div>");
                        break;
                    case 2:
                        //sb.AppendLine("<div class=\"greenBox\"><img src=\"i" + strFolder + "/" + row["highfile"].ToString() + "\" width=\"244\" height=\"169\" /></div>");
                        sb.AppendLine("<div class=\"greenBox\"><a href=\"" + row["highlink"].ToString() + "\"><img src=\"" + path + row["highfile"].ToString() + "\" title=\"" + row["highAlt"].ToString() + "\"  width=\"244\" height=\"169\" /></a></div>");
                        break;
                    default: break;
                }
                cont++;
            }
        }
        //sb.AppendLine("<div class='blueBox'><img src='" + Global.globalSiteImagesPath + "/blueBox.jpg' width='244' height='169' /></div>");
        //sb.AppendLine("<div class='helpBox'><img src='" + Global.globalSiteImagesPath + "/needHelp.jpg' width='243' height='169' /></div>");
        //sb.AppendLine("<div class='greenBox'><img src='" + Global.globalSiteImagesPath + "/greenBox.jpg' width='244' height='169' /></div>");
        PlaceHolder_Highlights.Controls.Add(new LiteralControl(sb.ToString()));
        dshigh = null;
        sb = null;
    }

    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            if (Request["ci"] != null)
            {
                switch (Request["ci"].ToString())
                {
                    case "1":
                        Response.Redirect("Educators_homepage.html");
                        break;
                    case "2":
                        Response.Redirect("Schools_homepage.html");
                        break;
                    case "3":
                        Response.Redirect("Parents_homepage.html");
                        break;
                    case "4":
                        Response.Redirect("Students_homepage.html");
                        break;
                    default:
                        Response.Redirect("Educators_homepage");
                        break;

                }
            }
            else
            {
                if (Request["ulo"] != null)
                {
                    Response.Redirect("Logout.html");
                }
                else {
                    Response.Redirect("Educators_homepage.html");
                }
                
            }


        }
    }
}
