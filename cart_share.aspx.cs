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
    string _contenido = "";
     
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (Request["contenido"] != null)
            _contenido = Request["contenido"];
        else
            _contenido = "";
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

    public string strFolder = SiteConstants.imagesPath;
    public string strFolderTb = SiteConstants.imagesPathTb;

    protected void loadPage()
    {
        try
        {
            SetChildPage("cart_share.aspx");

            if (Helper.StringExists(Request.QueryString["id"]))
            {
                Guid guid = new Guid(Request.QueryString["id"]);
                if (guid != null)
                {
                    this.CurrentCart = Cart.GetCartByGuid(guid);
                    this.CurrentCart.Refresh();
                    if (this.CurrentCart == null) throw new CartException("Cannot load cart data");

                    if (this.CurrentCart.Dirty) this.CurrentCart.Clean();

                    CartDisplayPanel.Visible = this.CurrentCart.HasItems;
                    CartEmptyPanel.Visible = !CartDisplayPanel.Visible;
                    StudentItemMessagePanel.Visible = CartEmptyPanel.Visible == false && this.CurrentCart.HasStudentItems;

                    //ItemsGrid.RowCommand += new GridViewCommandEventHandler(ItemsGrid_RowCommand);
                    //ItemsGrid.RowDataBound += new GridViewRowEventHandler(ItemsGrid_RowDataBound);

                    ItemsList.ItemDataBound += new RepeaterItemEventHandler(ItemsList_ItemDataBound);
                    ItemsList.ItemCommand += new RepeaterCommandEventHandler(ItemsList_ItemCommand);

                    //LoadControls();

                    if (!IsPostBack && CartDisplayPanel.Visible)
                    {
                        this.BindData(CurrentCart.Items);
                    }

                    Page.RegisterStartupScript("RegisterControls", String.Format("<script type='text/javascript'>{0}</script>", Helper.GenerateScriptToAccessControls(Page.Controls)));
                    //Helper.MakeGridViewAccessible(ref ItemsGrid);
                }
            }
            else
            {
                throw new CartException("");
            }
        }
        catch (CartException ex)
        {
            this.DisplayMessage(ex.Message);
            CartDisplayPanel.Visible = false;
            CartEmptyPanel.Visible = false;
            StudentItemMessagePanel.Visible = false;
        }
        
         if (Request.IsSecureConnection == false)
        {
            string redirect = Request.Url.ToString().Replace("http://", "https://");
            Response.Redirect(redirect);
            return;
        }
    }

    /*
    private void LoadControls()
    {
        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        //---------------------------------
        uc_FeatureProduct ucFeatureProduct = (uc_FeatureProduct)(Page.LoadControl("uc_FeatureProduct.ascx"));
        ucFeatureProduct.intSubjId = 0;
        PlaceHolder_uc_FeatureProduct.Controls.Add(ucFeatureProduct);
        //-------------------------------
        uc_Specials ucSpecials = (uc_Specials)(Page.LoadControl("uc_Specials.ascx"));
        ucSpecials.intSubjId = 0;
        PlaceHolder_uc_Specials.Controls.Add(ucSpecials);
        //-------------------------------
        uc_BestSellers ucBestSellers = (uc_BestSellers)(Page.LoadControl("uc_BestSellers.ascx"));
        ucBestSellers.intSubjId = 0;
        PlaceHolder_uc_BestSellers.Controls.Add(ucBestSellers);
    }
     * */

    private void SetChildPage(string name)
    {
        Session["CurrentChilPage"] = name;
    }


    #region Row Operations
    void ItemsList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataTable dt = (DataTable)ItemsList.DataSource;
        if (e.Item.ItemIndex < 0) return;

        DataRow dr = dt.Rows[e.Item.ItemIndex];
        
        Control c;
        
        c = e.Item.FindControl("CompanyNameLinkLiteral");
        if (c != null && c.GetType().Equals(typeof(Literal)))
        {
            ((Literal)c).Text = PubLink((string)dr["PubName"], (string)dr["PubUrl"]);
        }

        c = e.Item.FindControl("ProductTitleLinkLiteral");
        if (c != null && c.GetType().Equals(typeof (Literal)))
        {
            ((Literal) c).Text = String.Format("<a href='{1}'>{0}</a>", dr["Title"],
                                               String.Format("product.aspx?p={0}", dr["ProductTitleID"]));
        }

        c = e.Item.FindControl("ProductImageLiteral");
        if (c != null && c.GetType().Equals(typeof(Literal)))
        {
            string basepath = strFolderTb;
            string imageurl = Get_Title_Image_by_Id((int)dr["ProductTitleID"]);
            ((Literal)c).Text = ProductImage(String.Format("{0}tn_{1}", basepath, imageurl));
        }

        c = e.Item.FindControl("WishlistLiteral");
        if (c != null && c.GetType().Equals(typeof(Literal)))
        {
            ((Literal)c).Text = BuildWishlistLink((int)dr["ProductTitleID"]);
        }
    }

    protected void ItemsList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (Helper.StringExists(e.CommandName))
        {
            DataTable dt = BuildDataSource(CurrentCart.Items);
            DataRow dr = dt.Rows[e.Item.ItemIndex];

            int cart_contents_id = (int)dr["CartItemId"];

            CartDB db = new CartDB();
            if (e.CommandName == "AddToWishlist")
            {
                //Response.Write("Adding to Wishlist: " + dr["CartItemId"].ToString());
                throw new NotImplementedException("Add To Wishlist Not Implemented");
            }
            else if (e.CommandName == "UpdateQuantity")
            {
                Control c = e.Item.FindControl("QtyTextbox");
                if (c != null && c.GetType().Equals(typeof(TextBox)))
                {
                    TextBox t = (TextBox)c;
                    int quantity;
                    if (Int32.TryParse(t.Text, out quantity))
                    {
                        // Update Quantity
                        // If 0, Remove
                        if (quantity > 0)
                        {
                            decimal subtotal = (decimal)dr["UnitPrice"] * quantity;

                            db.CartUpdateItemQuantity(cart_contents_id, quantity, subtotal);
                        }
                        else
                        {
                            db.CartDeleteItem(cart_contents_id);
                        }
                    }
                }
            }
            else if (e.CommandName == "RemoveItem")
            {
                db.CartDeleteItem(cart_contents_id);
            }

            Response.Redirect(Constants.Pages.CART);
        }
    }


    #endregion

    #region GridView
    private DataTable CreateDataSource()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CartItemId", typeof(int));
        dt.Columns.Add("ProductTitleId", typeof(int));
        dt.Columns.Add("ProductSKU", typeof(string));
        dt.Columns.Add("Title", typeof(string));
        dt.Columns.Add("PubName", typeof(string));
        dt.Columns.Add("PubURL", typeof(string));
        dt.Columns.Add("IsBulk", typeof(bool));
        dt.Columns.Add("IsStudent", typeof(bool));
        dt.Columns.Add("Quantity", typeof(int));
        dt.Columns.Add("UnitPrice", typeof(decimal));
        dt.Columns.Add("SubTotal", typeof(decimal));
        dt.Columns.Add("UnitPriceDollar", typeof(string));
        dt.Columns.Add("SubTotalDollar", typeof(string));
        return dt;
    }

    private DataTable BuildDataSource(CartItem[] items)
    {
        DataTable dt = CreateDataSource();
        CartDB db = new CartDB();

        foreach (CartItem item in items)
        {
            string title = "", pubname = "", puburl = "";
            
            DataSet ds = db.CartProductGetBySKU(item.ProductSKU);
            if (ds != null)
            {
                using (DataTableReader dtr = ds.CreateDataReader())
                {
                    while (dtr.Read())
                    {
                        
                        title = Helper.IsString(dtr.GetValue(dtr.GetOrdinal("Title")), "");
                        
                        pubname = Helper.IsString(dtr.GetValue(dtr.GetOrdinal("PubName")), "");
                        
                        puburl = Helper.IsString(dtr.GetValue(dtr.GetOrdinal("PubUrl")), "");
                    }
                    dtr.Close();
                }
            }


            DataRow row = dt.NewRow();
            row["CartItemId"] = item.CartItemId;
            row["ProductTitleId"] = item.ProductTitleId;
            row["ProductSKU"] = item.ProductSKU;
            row["Title"] = title;
            row["PubName"] = pubname;
            row["PubURL"] = puburl;
            row["Quantity"] = item.Quantity;
            row["UnitPrice"] = item.UnitPrice;
            row["SubTotal"] = item.SubTotal;
            row["UnitPriceDollar"] = Helper.Dollar(item.UnitPrice);
            row["SubTotalDollar"] = Helper.Dollar(item.SubTotal);
            dt.Rows.Add(row);
        }

        dt.AcceptChanges();
        return dt;
    }

    private void BindData(CartItem[] items)
    {
        /*if (items.Length > 0)
        {
            CartItem first = (CartItem)items[0];
            if (Get_Title_RelatedProducts(first.ProductTitleId))
            {
                RelatedProductsPanel.Visible = true;
            }
        }*/

        ItemsList.DataSource = BuildDataSource(items);
        ItemsList.DataBind();
        //ItemsGrid.DataSource = BuildDataSource(items);
        //ItemsGrid.DataBind();

        CartDisplayPanel.Visible = items.Length > 0;
        CartEmptyPanel.Visible = !CartDisplayPanel.Visible;

        TotalValueLiteral.Text = Helper.Dollar(CurrentCart.SubTotal);
    }
    #endregion

    private void DisplayMessage(string message)
    {
        MessagePanel.Visible = true;
        MessagePanel.Controls.Add(new LiteralControl(message));
    }

    protected void CheckoutButton_Clicked(object sender, EventArgs e)
    {
        if (Session[Constants.SessionKeys.SUMMARY_EDIT] != null && (bool)Session[Constants.SessionKeys.SUMMARY_EDIT] == true)
        {
            Session.Remove(Constants.SessionKeys.SUMMARY_EDIT);
            CartDB db = new CartDB();
            db.CartUpdateDirty(CurrentCart.CartId, true);
            CurrentCart.MoveToStep(Constants.CheckoutStep.Summary);
            Response.Redirect(Constants.Pages.CHECKOUT);
            return;
        }

        CurrentCart.MoveToStep(0);
        Response.Redirect(Constants.Pages.CHECKOUT);
    }

    protected void QuoteThisButton_Clicked(object sender, EventArgs e)
    {
        throw new NotImplementedException("Quote this not implemented");
    }

    protected string PubLink(string name, string url)
    {
        if (Helper.StringExists(url))
        {
            return Helper.URLToLink(url, name); //String.Format("<a href=\"{0}\">{1}</a>", url, name);
        }
        else
        {
            return name;
        }
    }

    protected string ProductImage(string url)
    {
        if (Helper.StringExists(url))
        {
            return Helper.Image(url, "");
        }
        else
        {
            return "&nbsp;";
        }
    }


    private string Get_Title_Image_by_Id(int TitleId)
    {
        SiteProduct siteproduct = new SiteProduct();
        DataSet dsProduct = siteproduct.Get_Title_by_Id(TitleId);
        if (dsProduct != null && dsProduct.Tables != null)
        {
            foreach (DataTable table in dsProduct.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    return row["ImageTn"].ToString();
                }
            }
        }
        dsProduct = null;
        return "";
    }

    private bool Get_Title_RelatedProducts(int TitleId)
    {
        bool has_related = false;

        SiteProduct siteproduct = new SiteProduct();
        
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        DataSet dsSitePrice = siteproduct.Get_Title_RelatedProducts(TitleId);

        if (dsSitePrice != null && dsSitePrice.Tables != null)
        {
            foreach (DataTable table in dsSitePrice.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!has_related) has_related = true;

                    sb.AppendLine("<div class=\"SlideItMoo_element\"><a href=\"product.aspx?p=" + row["titleid"].ToString() + "\" style=\"background-color:#FFF;\"><img src=\"" + strFolder + row["imagetn"].ToString() + "\" /></a><p>" + row["title"].ToString() + "</p></div>");
                }
            }
        }
        //PlaceHolser_Slide.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;

        return has_related;
    }

    private string BuildWishlistLink(int TitleId)
    {
        return String.Format("<a href=\"addWish.aspx?p={0}\" rel=\"width:800,height:300,ajax:true\" id=\"mb10\" class=\"mb\" title=\"Add To Wishlist\"><img src=\"" + Global.globalSiteImagesPath + "/addToWish.jpg\" alt=\"Add To Wishlist\" /></a>", TitleId);
    }
}
