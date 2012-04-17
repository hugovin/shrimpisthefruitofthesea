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

public partial class print : System.Web.UI.Page
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
            CurrentChilPage = "wishlist_share2.aspx";
        //--   
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "wishlist_share2.aspx";

        loadContenido();
        loadContact();
        loadPage();
    }

    protected void loadContenido()
    {
        //PlaceHolder_Div.Controls.Add(new LiteralControl(_contenido.ToString()));
    }

    private void loadContact()
    {
        boxContactPrint boxContactPrint = (boxContactPrint)(Page.LoadControl("boxContactPrint.ascx"));
        PlaceHolder_boxContactPrint.Controls.Add(boxContactPrint);
    }

    Cart CurrentCart;

    public string strFolderTb = SiteConstants.imagesPathTb;

    protected void loadPage()
    {
        try
        {
            SetChildPage("wishlist_share2.aspx");

            if (Helper.StringExists(Request.QueryString["id"]))
            {
                Guid guid = new Guid(Request.QueryString["id"]);
                if (guid != null)
                {
                    WishlistDisplayPanel.Controls.Add(new LiteralControl(LoadWishList(guid)));
                }
            }
            else
            {
                throw new CartException("");
            }
        }
        catch (CartException ex)
        {
            
        }
        
       /*  if (Request.IsSecureConnection == false)
        {
            string redirect = Request.Url.ToString().Replace("http://", "https://");
            Response.Redirect(redirect);
            return;
        }*/
    }


    protected string LoadWishList(Guid id)
    {
        int TitleId = 0;
        SiteWish wishList = new SiteWish();
        DataSet dswishList = new DataSet();
        StringBuilder sb = new StringBuilder();
        Boolean entro = false;
    
        //dswishList = wishList.Get_All_Wish_By_Session();
        dswishList = wishList.Get_All_Wish_By_GUID(id);
        if (dswishList == null) return "";
        foreach (DataTable table in dswishList.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<div class=\"prodwish\">");
                    sb.AppendLine("<div class=\"prodImg\">");
                        sb.AppendLine("<div id=\"boxContImage\"  style=\"width:115px; height:115px;\">");
                        sb.AppendLine("<a href=\"product.aspx?p=" + row["titleid"].ToString() + "\"><img id=\"images\" style=\"width: 102px;height:127px;\" title=\"" + row["pubname"].ToString() + " : " + row["title"].ToString() + "\" src=\"" + strFolder + "tn_" + row["imageTN"].ToString() + "\"></a>"); //onload=\"getDim(document.getElementById('boxContImage'),this)\"
                        sb.AppendLine("</div>");
                    sb.AppendLine("</div>");

                    sb.AppendLine("<div class=\"prodSrcDesc\">");
                        sb.AppendLine("<h2><a href=\"Product.aspx?p=" + row["TitleId"].ToString() + "&s=" + row["SKU"].ToString() + "\">" + row["title"].ToString() + "</a></h2>");
                        sb.AppendLine("<p><em>by: </em> <a href=\"result.aspx?findopt5=" + row["PubId"].ToString() + "&am=1&asm=" + 3 + "\">" + row["pubname"].ToString() + "</a></p>");
                        sb.AppendLine("<h3> $" + Convert.ToDouble(row["price"].ToString()) + "</h3>");

                        if (Convert.ToDouble(row["youShave"].ToString()) > 0)
                        {
                            sb.AppendLine("<h4>Your DISCOUNTED price</h4>");
                            sb.AppendLine("<h5>You Save: $" + Convert.ToDouble(row["youShave"].ToString()) + "</h5>");
                        }

                        sb.AppendLine("Date Added: " + row["CreateDate"].ToString() + "<br />");
                                                                
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

                        //sb.AppendLine("<a href=\"#\" id=\"A2\" class=\"mb\" title=\"\" rel=\"type:element\">" + Cart.CreateAddToCartLink("<img src=\"" + Global.globalSiteImagesPath + "/btnMovetoCart.jpg\"/>", row["titleId"].ToString(), row["SKU"].ToString(), 1, Convert.ToInt32(row["WishListId"].ToString())) + "</a>");
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

        return sb.ToString();
    }

    private void SetChildPage(string name)
    {
        Session["CurrentChilPage"] = name;
    }

}
