using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using uc_Left_Menu;
public partial class temp_Main : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Left_Menu uc_left_menu = (Left_Menu)(Page.LoadControl("Left_Menu.ascx"));
        uc_left_menu._Finder = true;
        uc_left_menu._Subject = true;
        uc_left_menu._Browse = true;
        uc_left_menu._Resourcecenter = true;
        uc_left_menu._Aboutus = false;
        LeftMenuPlaceHolder.Controls.Add(uc_left_menu);
        LoadHeadMenu();
        LoadFeatured();
        LoadAdds();
        LoadCentralTabs();

    }
    protected void LoadHeadMenu()
    {
        Header head = new Header();
        DataSet dsContentGroup = new DataSet();
        StringBuilder sb = new StringBuilder();
        //---
        int ContSelected = 1;
        dsContentGroup = head.getAllContentGroups();
        sb.AppendLine("<ul>");

        foreach (DataTable table in dsContentGroup.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<li><a href=\"#\" class=\"" + (row["ContId"].ToString() == ContSelected.ToString() ? "current" : "") + "\">" + row["ContTitle"].ToString() + "</a></li>");
                //sb.AppendLine("" + row["cat"].ToString() + "");
            }

        }
        sb.AppendLine("<li><a href=\"#\" class=\"request\">Request a Quote</a></li>");
        sb.AppendLine("</ul>");

        topRight.InnerHtml = sb.ToString();
    }
    protected void LoadFeatured()
    {
        string strFolder = "images";

        MainContent feature = new MainContent();
        DataSet dsfeature = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.Text.StringBuilder sbImages = new System.Text.StringBuilder();
        System.Text.StringBuilder sbLink = new System.Text.StringBuilder();
        //---

        dsfeature = feature.getAllFeature();

        sb.AppendLine("<span id=\"loading\">Loading</span>");
        sb.AppendLine("<ul id=\"pictures\">");
        foreach (DataTable table in dsfeature.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                //sb.AppendLine("<li><a href=\"#\" class=\"" + (row["ContId"].ToString() == ContSelected.ToString() ? "current" : "") + "\">" + row["ContTitle"].ToString() + "</a></li>");

                sbImages.AppendLine("<li><img src=\"" + strFolder + "/" + row["FeatFile"].ToString() + "\" alt=\"" + row["FeatAlt"].ToString() + "\" title=\"" + row["FeatTitle"].ToString() + "\" /></li>");


                sbLink.AppendLine("<li><a href=\"" + row["FeatLink"].ToString() + "\">" + row["FeatTitle"].ToString() + "</a></li>");
            }

        }
        sb.Append(sbImages.ToString());
        sb.AppendLine("</ul>");
        sb.AppendLine("<ul id=\"menu\">");
        sb.Append(sbLink.ToString());
        sb.AppendLine("</ul>");
        sb.AppendLine("<input style=\"display: none\" type=\"checkbox\" name=\"auto\" checked=\"checked\" id=\"option-auto\" />");

        slideshow.InnerHtml = sb.ToString();


    }
    protected void LoadAdds()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<div class=\"breadAdd\">");
        sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/pig.jpg\" width=\"462\" height=\"51\" />");
        sb.AppendLine("</div>");
        sb.AppendLine("<div class=\"breadDivider\">");
        sb.AppendLine("</div>");
        sb.AppendLine("<div class=\"breadAdd\">");
        sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/moneyBuble.jpg\" width=\"390\" height=\"50\" />");
        sb.AppendLine("</div>");
        bread.InnerHtml = sb.ToString();
        sb = null;
    }
    protected void LoadCentralTabs()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<h4>What’s New</h4>");
        sb.AppendLine("<div>");
        sb.AppendLine("<div class=\"product\">");
        sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/imgProduct1.jpg\" width=\"106\" height=\"145\" />");
        sb.AppendLine("<h2><a href=\"·\">some title product here on two lines</a></h2>");
        sb.AppendLine("<p>Amatemn ollo oa áva, occo</p>");
        sb.AppendLine("</div>");
        sb.AppendLine("<div class=\"tabDivider\"></div>");
        sb.AppendLine("<div class=\"product\">");
        sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/imgProduct2.jpg\" />");
        sb.AppendLine("<h2><a href=\"#\">some title product here on two lines</a></h2>");
        sb.AppendLine("<p> Aman omanollo oa áva, occo</p>");
        sb.AppendLine("</div>");
        sb.AppendLine("<div class=\"tabDivider\"></div>");
        sb.AppendLine("<div class=\"product\">");
        sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/imgProduct3.jpg\" />");
        sb.AppendLine("<h2><a href=\"#\">some title product here on two lines</a></h2>");
        sb.AppendLine("<p> Aman olemelo oa áva, occo</p>");
        sb.AppendLine("</div>");
        sb.AppendLine("<div class=\"seeMore\"><a href=\"#\">+ see more</a></div>");
        sb.AppendLine("</div>");
        sb.AppendLine("<h4>Featured Products</h4>");
        sb.AppendLine("<div>");
        sb.AppendLine("<div class=\"product\">");
        sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/imgProduct2.jpg\" />");
        sb.AppendLine("<h2><a href=\"#\">some title product here on two lines</a></h2>");
        sb.AppendLine("<p> Aman ollo oa áva, oloemcco</p>");
        sb.AppendLine("</div>");
        sb.AppendLine("<div class=\"tabDivider\"></div>");
        sb.AppendLine("<div class=\"product\">");
        sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/imgProduct3.jpg\" />");
        sb.AppendLine("<h2><a href=\"#\">some title product here on two lines</a></h2>");
        sb.AppendLine("<p> Aman olmarenlo oa áva, occo</p>");
        sb.AppendLine("</div>");
        sb.AppendLine("<div class=\"tabDivider\"></div>");
        sb.AppendLine("<div class=\"product\">");
        sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/imgProduct2.jpg\" />");
        sb.AppendLine("<h2><a href=\"#\">some title product here on two lines</a></h2>");
        sb.AppendLine("<p> Ammama an ollo oa áva, occo</p>");
        sb.AppendLine("</div>");
        sb.AppendLine("<div class=\"seeMore\"><a href=\"#\">+ see more</a></div>");
        sb.AppendLine("</div>");
        sb.AppendLine("<h4>Best Sellers</h4>");
        sb.AppendLine("<div>");
        sb.AppendLine("<div class=\"product\">");
        sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/imgProduct1.jpg\" />");
        sb.AppendLine("<h2><a href=\"#\">some title product here on two lines</a></h2>");
        sb.AppendLine("<p> Aman ollo oa áva, ocxxco</p>");
        sb.AppendLine("</div>");
        sb.AppendLine("<div class=\"tabDivider\"></div>");
        sb.AppendLine("<div class=\"product\">");
        sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/imgProduct2.jpg\" />");
        sb.AppendLine("<h2><a href=\"#\">some title product here on two lines</a></h2>");
        sb.AppendLine("<p> Amxan ollo oxa áva, occo</p>");
        sb.AppendLine("</div>");
        sb.AppendLine("<div class=\"tabDivider\"></div>");
        sb.AppendLine("<div class=\"product\">");
        sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/imgProduct3.jpg\" />");
        sb.AppendLine("<h2><a href=\"#\">some title product here on two lines</a></h2>");
        sb.AppendLine("<p> Axman oxllo oa áva, occo</p>");
        sb.AppendLine("</div>");
        sb.AppendLine("<div class=\"seeMore\"><a href=\"#\">+ see more</a></div>");
        sb.AppendLine("</div>");
        demo_block.InnerHtml = sb.ToString();
        sb = null;
    }

}


