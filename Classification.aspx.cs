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

public partial class Classification : System.Web.UI.Page
{

    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string SubjId = "";
    private string classId = "";
    private string strFolder = SiteConstants.imagesPathTb;
    private string pageName = "";
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
            CurrentChilPage = "classification.aspx";
        //--       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "classification.aspx?cp=" + Request["cp"];
        classId = Request["cp"];

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }

        LoadBread();
        SubjId = Request["subjId"];
        if (SubjId == null) SubjId = "0";


        LoadClassification();
        //-- Left Menu Active
        if (Request.QueryString["am"] != null && Request.QueryString["am"] != "")
        {
            try { SiteConstants.LeftMenuActive = Convert.ToInt32(Request.QueryString["am"].ToString()); }
            catch { SiteConstants.LeftMenuActive = 4; }
        }
        //--
        //this.URLRedirect();
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Response.Redirect(pageName + "_c" + Request["cp"].ToString() + ".clahtml");
        }
    }
    protected void LoadBread()
    {
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main.pageTitleBar = "Classification - " + main.pageTitleBar;
        if (Convert.ToInt32(Request["cp"]) == 1)
        {
            main._site_breadLink = "<li class=\"last\"><a href=\"" + Session["CurrentChilPage"].ToString() + "\"><strong>" + "Feature Products" + "</strong></a></li>";
        }
        else
        {
            if (Convert.ToInt32(Request["cp"]) == 3)
            {
                main._site_breadLink = "<li class=\"last\"><a href=\"" + Session["CurrentChilPage"].ToString() + "\"><strong>" + "Best Sellers" + "</strong></a></li>";
            }
        }

    }

    protected void LoadClassification()
    {
        SiteProduct classificationProducts = new SiteProduct();
        DataSet dsclassificationProducts = new DataSet();
        StringBuilder sb = new StringBuilder();

        dsclassificationProducts = classificationProducts.Get_Title_Content_Site_Classification(Convert.ToInt32(classId));
        foreach (DataTable table in dsclassificationProducts.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<div class=\"contHead\">");
                sb.AppendLine("<h3>" + row["ClassDescription"].ToString() + "</h3>");
                pageName = row["ClassDescription"].ToString();
                sb.AppendLine(row["ClassContent"].ToString());
                sb.AppendLine("</div>");

            }
        }
        
        dsclassificationProducts = classificationProducts.Get_All_Products_By_Classification(Convert.ToInt32(SubjId), Convert.ToInt32(classId));
        foreach (DataTable table in dsclassificationProducts.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row["New_Flag"].ToString() == "1") { sb.AppendLine("<div class=\"prodContNew\">"); }
                else { sb.AppendLine("<div class=\"prodCont\">"); }

                sb.AppendLine("<div class=\"prodImg\">");
                    sb.AppendLine("<div id=\"boxContImage\" style=\"width:115px; height:115px;\">");
                    sb.AppendLine("<a href=\"product.aspx?p=" + row["titleid"].ToString() + "\"><img id=\"images\" style=\"width:102px; height:127px;\" title=\"" + row["pubname"].ToString() +" : " +row["title"].ToString() + "\" src=\"" + strFolder + "tn_" + row["ImageTN"] + "\"></a>");//onload=\"getDim(document.getElementById('boxContImage'),this)\"
                    sb.AppendLine("</div>");
                sb.AppendLine("</div>");

                sb.AppendLine("<div class=\"prodDesc\">");
                sb.AppendLine("<h2><a href=\"Product.aspx?p=" + row["TitleId"].ToString() + "\">" + row["title"].ToString() + "</a></h2>");
                sb.AppendLine("<p><em>by: </em><a href=\"result.aspx?findopt5=" + row["PubId"].ToString() + "&am=1&asm=" + 3 + "\">" + row["pubname"].ToString() + "</a></p>");
                sb.AppendLine("<h3> $" + String.Format("{0:#,0.00}", row["er_price"]) + "</h3>");

                if (Convert.ToDouble(row["youShave"].ToString()) > 0)
                {
                    sb.AppendLine("<h4>Your DISCOUNTED price</h4>");
                    sb.AppendLine("<h5>You Save: $" + String.Format("{0:#,0.00}", row["youShave"]) + "</h5>");
                }

                sb.AppendLine("<a href=\"#\" id=\"mb15\" class=\"mb\" title=\"\" rel=\"type:element\">" + Cart.CreateAddToCartLink("<img src=\"" + Global.globalSiteImagesPath + "/addToCart.jpg\" width=\"109\" height=\"26\" />", row["titleid"].ToString(), row["defaultsku"].ToString(), 1, 0) + " </a>");
                sb.AppendLine("</div>");

                sb.AppendLine("<div class=\"prodNumber\">");
                sb.AppendLine("<div class=\"prodNumCont\">");

                if ((row["Plat_Win_Flag"].ToString() == "1") && (row["Plat_Mac_Flag"].ToString() == "1"))
                { sb.AppendLine("<p>Mac / Windows</p>"); }
                else
                { sb.AppendLine("<p>" + (row["Plat_Win_Flag"].ToString() == "1" ? "Windows" : "") + "" + (row["Plat_Mac_Flag"].ToString() == "1" ? "Mac" : "") + "</p>"); }

                sb.AppendLine("<div class=\"numBG\">Grades<br /><span>" + row["grades"].ToString() + "</span></div>");
                sb.AppendLine("<p>Item #: " + row["defaultSKU"].ToString() + "</p>");
                sb.AppendLine("</div>");                

                //--Login Validate
                if (Session[SiteConstants.UserValidLogin] != null && (bool)Session[SiteConstants.UserValidLogin])
                {
                    sb.AppendLine(" <a href=\"addWish.aspx?p=" + row["TitleId"].ToString()+"&sk="+row["defaultSKU"].ToString() +"\" rel=\"width:560,height:126,ajax:true\" id=\"mb10\" class=\"mb\" title=\"Add Product\"><img src=\"" + Global.globalSiteImagesPath + "/addToWish.jpg\" border=\"0\"></a>");
                }
                else
                {
                    sb.AppendLine("<a href=\"#htmlElement\" id=\"mb15\" class=\"mb\" title=\"\" rel=\"type:element\"><img src=\"" + Global.globalSiteImagesPath + "/addToWish.jpg\" border=\"0\"></a>");
                }                
                sb.AppendLine("<a href=\"requestaquote.aspx?title=" + row["Title"].ToString() + "\"><img src=\"" + Global.globalSiteImagesPath + "/quote.jpg\" border=\"0\"></a>");
                sb.AppendLine("</div>");

                sb.AppendLine("<div class=\"prodMore\"><a href=\"Product.aspx?p=" + row["TitleId"].ToString() + "\">+  learn more</a></div>");
                sb.AppendLine("</div>");

               // cont++;
            }
        }

        PlaceHolder_Clasification.Controls.Add(new LiteralControl(sb.ToString()));

        sb = null;
        classificationProducts = null;
        dsclassificationProducts = null;

    }

}
