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

public partial class WhatsNew : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string classId = "";
    private string strFolder = SiteConstants.imagesPathTb;

    private void GetVars()
    {
        //---
        if (Session["SiteId"] != null)
            SiteId = Session["SiteId"].ToString();

        ContentId = Request["ci"];
        if (ContentId != null)
            Session["ContentId"] = ContentId;
        else
            ContentId = Session["ContentId"].ToString();
        //--
        if (Session["CurrentChilPage"] != null)
            CurrentChilPage = Session["CurrentChilPage"].ToString();
        else
            CurrentChilPage = "whatsnew.aspx";
        //--        
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Response.Redirect("Whats_new_"+Request["cp"]+".html");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "whatsnew.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }

        
        if (Request["cp"] != null && Request["cp"].ToString().Trim() != "")
        {
            classId = Request["cp"].ToString().Trim();
            try { Convert.ToInt32(classId); }
            catch (Exception ex) { classId = "5"; }
        }
        else { classId = "5"; }

        LoadWhatNew();
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main.pageTitleBar = "Whats New? - "+ main.pageTitleBar;
        ////this.URLRedirect();
    }

    protected void LoadWhatNew()
    {
        SiteProduct classificationProducts = new SiteProduct();
        DataSet dsclassificationProducts = new DataSet();

        SiteWhatsNew whatnew = new SiteWhatsNew();
        DataSet dswhatnew = new DataSet();

        StringBuilder sb = new StringBuilder();
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main._site_breadLink = "<li><a href=# onClick=\"return false;\"><strong>" + "Whats New" + "</strong></a></li>";

        dsclassificationProducts = classificationProducts.Get_Title_Content_Site_Classification(Convert.ToInt32(classId));
        foreach (DataTable table in dsclassificationProducts.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<div class=\"contHead\">");
                sb.AppendLine("<h3>" + row["ClassDescription"].ToString() + "</h3>");
                sb.AppendLine(row["ClassContent"].ToString());
                sb.AppendLine("</div>");

            }
        }


        dswhatnew = whatnew.Get_All_Whats_New();
        foreach (DataTable table in dswhatnew.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<div class=\"prodCont\">");
                sb.AppendLine("<div class=\"prodImg\">");
                    sb.AppendLine("<div id=\"boxContImage\"  style=\"width:115px; height:115px;\">");
                    sb.AppendLine("<a href=\"product.aspx?p=" + row["titleid"].ToString() + "\"><img id=\"images\" style=\"width: 102px;height:127px;\" title=\"" + row["PubName"].ToString() +" : " +row["title"].ToString() + "\" src=\"" + strFolder + "tn_" + row["ImageTn"] + "\"></a>"); //onload=\"getDim(document.getElementById('boxContImage'),this)\"
                    sb.AppendLine("</div>");
                sb.AppendLine("</div>");


                sb.AppendLine("<div class=\"prodDesc1\">");
                sb.AppendLine("<h2><a href=\"Product.aspx?p=" + row["TitleId"].ToString() + "\">" + row["title"].ToString() + "</a></h2>");
                sb.AppendLine(row["titletext"].ToString());
                sb.AppendLine("<p><a href=\"Product.aspx?p=" + row["TitleId"].ToString() + "\">+  more information</a></p>");
                sb.AppendLine("</div>");

                sb.AppendLine("<div class=\"prodNumber1\">");
                sb.AppendLine("<a href=\"#\" id=\"A2\" class=\"mb\" title=\"\" rel=\"type:element\">" + Cart.CreateAddToCartLink("<img src=\"" + Global.globalSiteImagesPath + "/addToCart.jpg\" width=\"109\" height=\"26\" />", row["titleid"].ToString(), row["defaultsku"].ToString(), 1, 0) + " </a>");

                //--Login Validate
                if ((bool)Session[SiteConstants.UserValidLogin])
                {
		    sb.AppendLine("<a href=\"addWish.aspx?p=" + row["TitleId"].ToString() + "&sk=" + row["defaultsku"] + "\" rel=\"width:580,height:131,ajax:true\" id=\"mb10\" class=\"mb\" title=\"Add Product\"><img src=\"" + Global.globalSiteImagesPath + "/addToWish.jpg\" border=\"0\"></a>");
                }
                else
                {
                    sb.AppendLine("<a href=\"#htmlElement\" id=\"mb15\" class=\"mb\" title=\"\" rel=\"type:element\"><img src=\"" + Global.globalSiteImagesPath + "/addToWish.jpg\" border=\"0\"></a>");
                }
                
                sb.AppendLine("<a href=\"requestaquote.aspx?title=" + row["Title"].ToString() + "\"><img src=\"" + Global.globalSiteImagesPath + "/quote.jpg\" border=\"0\"></a>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
            }
        }

        PlaceHolder_WhatsNew.Controls.Add(new LiteralControl(sb.ToString()));

        sb = null;
        classificationProducts = null;
        dsclassificationProducts = null;
        whatnew = null;
        dswhatnew = null;
    }
}
