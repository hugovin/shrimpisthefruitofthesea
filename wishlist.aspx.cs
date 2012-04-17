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
using uc_Right;

public partial class wishList : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string strFolder = SiteConstants.imagesPathTb;
    private int TitleId = 0;

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
            CurrentChilPage = "wishlist.aspx";
        //--   
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[SiteConstants.UserFullName] == "" || Session[SiteConstants.UserFullName] == null)
        {
            Response.Redirect("home.aspx");
        }
        GetVars();
        Session["CurrentChilPage"] = "wishlist.aspx";
        Main_MasterPage m = (Main_MasterPage)Page.Master;
        m.pageTitleBar = "Wish List - " + m.pageTitleBar;
        //////this.URLRedirect();
        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }
                      
        if (Convert.ToInt32(Request["up"]) != 0){         
            updateComments(Convert.ToInt32(Request["w"]), Request["txt_comment"]);
        }

        LoadWishList();
        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        uc_FeatureProduct ucFeatureProduct = (uc_FeatureProduct)(Page.LoadControl("uc_FeatureProduct.ascx"));
        PlaceHolder_uc_FeatureProduct.Controls.Add(ucFeatureProduct);
        uc_Specials ucSpecials = (uc_Specials)(Page.LoadControl("uc_Specials.ascx"));
        PlaceHolder_uc_Specials.Controls.Add(ucSpecials);
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main._site_breadLink += "<li class=\"last\"><a href=#><strong>" + "Wish List" + "</strong></a></li>"; 

    }

    protected void LoadWishList()
    {
        SiteWish wishList = new SiteWish();
        DataSet dswishList = new DataSet();
        StringBuilder sb = new StringBuilder();
        Boolean entro = false;
        bool share_added = false;

        sb.AppendLine("<div class=\"mainAccount\">");
        sb.AppendLine("<h1>" + Session[SiteConstants.UserFullName] + ":<strong> Wish List</strong></h1>");
        sb.AppendLine("</div>");    
    
        dswishList = wishList.Get_All_Wish_By_Session();
        foreach (DataTable table in dswishList.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (!share_added)
                {
                    //sb.AppendLine(String.Format("<a href='wishlist_share.aspx?id={0}'>Share Me</a>", row["UniqueID"]));
                    string SocialTwistScript = "<style type='text/css'>div#bread div.iconSubmenu div.linkShare { display:none; }</style><div style='width: 100px; float: right; margin-top: -20px;margin-right:12px;'>" + Global.globalSocialTwist + "</a></div>";
                    string link = String.Format("http://" + Request.Url.Host.ToString() + "/wishlist_share.aspx?id={0}", row["UniqueID"]);
                    sb.AppendLine(SocialTwistScript.Replace("window.location", "'"+link+"'"));

                    share_added = true;
                }
                
                sb.AppendLine("<div class=\"prodwish\">");
                    sb.AppendLine("<div class=\"prodImg\">");
                        sb.AppendLine("<div id=\"boxContImage\"  style=\"width:115px; height:115px;\">");
                        sb.AppendLine("<a href=\"product.aspx?p=" + row["titleid"].ToString() + "\"><img id=\"images\" style=\"width: 102px;height:127px;\" title=\"" + row["pubname"].ToString() + " : " + row["title"].ToString() + "\" src=\"" + strFolder + "tn_" + row["imageTN"].ToString() + "\"></a>"); //onload=\"getDim(document.getElementById('boxContImage'),this)\"
                        sb.AppendLine("</div>");
                    sb.AppendLine("</div>");

                    sb.AppendLine("<div class=\"prodSrcDesc\">");
                        sb.AppendLine("<h2><a href=\"Product.aspx?p=" + row["TitleId"].ToString() + "&s=" + row["SKU"].ToString() + "\">" + row["title"].ToString() + "</a></h2>");
                        sb.AppendLine("<p><em>by: </em> <a href=\"result.aspx?findopt5=" + row["PubId"].ToString() + "&am=1&asm=" + 3 + "\">" + row["pubname"].ToString() + "</a></p>");
                        sb.AppendLine("<h3> $" + String.Format("{0:#,0.00}", row["price"]) + "</h3>");

                        if (Convert.ToDouble(row["youShave"].ToString()) > 0)
                        {
                            sb.AppendLine("<h4>Your DISCOUNTED price</h4>");
                            sb.AppendLine("<h5>You Save: $" + String.Format("{0:#,0.00}", row["youShave"]) + "</h5>");
                        }
                        
                        sb.AppendLine("Date Added: " + row["CreateDate"].ToString() + "<br />");
                        sb.AppendLine("Add Comment:<br />");
				sb.AppendLine("<form action=\"wishList.aspx?w=" + row["WishListDetailId"].ToString() + "&up=1\" method=\"post\">");
                            sb.AppendLine("<input type=\"text\" value=\"" + row["Comment"].ToString() + " \" name=\"txt_comment\"/><br><br>");                           
                            sb.AppendLine("<input type=\"image\" src=\"" + Global.globalSiteImagesPath + "/bottonSave.jpg\"/><br><br>");
                        sb.AppendLine("</form>");                                                                           
                sb.AppendLine("</div>");

                sb.AppendLine("<div class=\"prodNumber\">");
                    sb.AppendLine("<div class=\"prodNumCont\">");
                        if ((row["Plat_Win_Flag"].ToString() == "1") && (row["Plat_Mac_Flag"].ToString() == "2"))
                        { sb.AppendLine("<p>Mac / Windows</p>"); }
                        else
                        { sb.AppendLine("<p>" + (row["Plat_Win_Flag"].ToString() == "1" ? "Windows" : "") + "" + (row["Plat_Mac_Flag"].ToString() == "2" ? "Mac" : "") + "</p>"); }

                        sb.AppendLine("<div class=\"numBG\">Grades<br /><span>" + row["grades"].ToString() + "</span></div>");
                            sb.AppendLine("<p>Item #: " + row["SKU"].ToString() + "</p>");
                        sb.AppendLine("</div>");

                        sb.AppendLine("<a href=\"#\" id=\"A2\" class=\"mb\" title=\"\" rel=\"type:element\">" + Cart.CreateAddToCartLink("<img src=\"" + Global.globalSiteImagesPath + "/btnMovetoCart.jpg\"/>", row["titleId"].ToString(), row["SKU"].ToString(), 1, Convert.ToInt32(row["WishListId"].ToString())) + "</a>");
			sb.AppendLine("<a href=\"delWish.aspx?w=" + row["WishListId"].ToString() + "&id=" + row["TitleId"].ToString() + "&sku=" + row["SKU"].ToString() + "\" rel=\"width:567,height:131,ajax:true\" id=\"mb10\" class=\"mb\" title=\"Delete Product\" onClick=\"find_div_class();\"><img src=\"" + Global.globalSiteImagesPath + "/btnDelete.jpg\" border=\"0\"></a>");                        
                    sb.AppendLine("</div>");
                sb.AppendLine("</div>");

                TitleId = Convert.ToInt32(row["TitleId"]);
            }

            

            SiteProduct siteproduct = new SiteProduct();
            DataSet dsSitePrice = new DataSet();
            dsSitePrice = siteproduct.Get_Title_RelatedProducts(TitleId);
            foreach (DataTable tableProducts in dsSitePrice.Tables)
            {
                foreach (DataRow row in tableProducts.Rows)
                {
                    if (entro == false){
                        entro = true;    
                        sb.AppendLine("<div id=\"resultControls\">");
                        sb.AppendLine("<h2>Similar Products</h2>");
                        sb.AppendLine("<div id=\"SlideItMoo_outer\">");
                        sb.AppendLine("<div id=\"SlideItMoo_inner\">");
                        sb.AppendLine("<div id=\"SlideItMoo_items\">");
                    }
                    sb.AppendLine("<div class=\"SlideItMoo_element\"><a href=\"product.aspx?p=" + row["titleid"].ToString() + "\" style=\"background-color:#FFF;\"><img src=\"" + strFolder + "tn_" + row["imageTN"].ToString() + "\" title=\"" + row["PubName"].ToString() + " : " + row["title"].ToString() + "\" /></a><p>" + row["title"].ToString() + "</p></div>");
                }
            }
                    if (entro == true){
                                    sb.AppendLine("</div>");
                                sb.AppendLine("</div>");
                            sb.AppendLine("</div>");
                        sb.AppendLine("</div>");
                    }
        }
     
        PlaceHolder_Wish.Controls.Add(new LiteralControl(sb.ToString()));

        sb = null;
        wishList = null;
        dswishList = null;
    }

    protected void updateComments(int idWishList, string comments)
    {
        SiteWish wishList = new SiteWish();
        //Response.Redirect("home.aspx?" + comments + "&" + idWishList);

        wishList.Upd_Comment_Whish_Detail(idWishList, comments);
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Response.Redirect("Wish_List");
        }
    }
}
