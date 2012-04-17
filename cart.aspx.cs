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

using uc_Right;
public partial class CartPage : System.Web.UI.Page
{
    Cart CurrentCart;
    public ArrayList _ConfigurationList = new ArrayList();
    public string strFolder = SiteConstants.imagesPath;
    public string strFolderTb = SiteConstants.imagesPathTb;
    public string _Imagetn = "";
    private string completeNames = "";
    private string completeQty = "";
    public bool overweigth = false;
    protected void Page_Load(object sender, EventArgs e)
    {
	 if (Request.IsSecureConnection != false)
        {
            string redirect = Request.Url.ToString().Replace("https://", "http://");
            Response.Redirect(redirect);
            return;
        }

        try
        {
            string Torch_Order_Detail = "";
            //-- Left Menu Active
            if (Request.QueryString["am"] != null && Request.QueryString["am"] != "")
            {
                try { SiteConstants.LeftMenuActive = Convert.ToInt32(Request.QueryString["am"].ToString()); }
                catch { SiteConstants.LeftMenuActive = 4; }
            }
            else { SiteConstants.LeftMenuActive = 4; }
            //--
            SetChildPage("cart.aspx");
            this.CurrentCart = Cart.GetCartFromSession(Session);


            if (this.CurrentCart.CheckoutStep >= (int)Constants.CheckoutStep.ThankYou)
            {
                CartDB db = new CartDB();
                db.CartUpdateCompleted(this.CurrentCart.CartId, true);
                db.CartDeleteAllByUserID(this.CurrentCart.UserId);
                Response.Redirect("cart.aspx");
            }


            if (this.CurrentCart.Dirty) this.CurrentCart.Clean();

            CartDisplayPanel.Visible = this.CurrentCart.HasItems;
            CartEmptyPanel.Visible = !CartDisplayPanel.Visible;
            StudentItemMessagePanel.Visible = CartEmptyPanel.Visible == false && this.CurrentCart.HasStudentItems;

            DebugPanel.Controls.Add(new LiteralControl(Helper.VarDump(CurrentCart)));
            if (CurrentCart.ItemsWeight > 100)
            {
                overweigth = true;
            }
            DebugPanel.Visible = false;

            //ItemsGrid.RowCommand += new GridViewCommandEventHandler(ItemsGrid_RowCommand);
            //ItemsGrid.RowDataBound += new GridViewRowEventHandler(ItemsGrid_RowDataBound);

            ItemsList.ItemDataBound += new RepeaterItemEventHandler(ItemsList_ItemDataBound);
            ItemsList.ItemCommand += new RepeaterCommandEventHandler(ItemsList_ItemCommand);

		    string SocialTwistScript = "<div style='width: 100px; float: right; margin-top: -20px;margin-right:12px;'>" + Global.globalSocialTwist + "</a></div>";
		    string link = String.Format("http://"+Request.Url.Host.ToString()+"/cart_share.aspx?id={0}", CurrentCart.UniqueId.ToString());		
		    CartDisplayPanel.Controls.AddAt(0, new LiteralControl(SocialTwistScript.Replace("window.location", "'"+link+"'")));
	    
            LoadControls();

            if (CartDisplayPanel.Visible)
            {
                this.BindData(CurrentCart.Items);
            }

            Page.RegisterStartupScript("RegisterControls", String.Format(" <script type='text/javascript'>{0}</script>", Helper.GenerateScriptToAccessControls(Page.Controls)));
            //Helper.MakeGridViewAccessible(ref ItemsGrid);
        }
        catch (CartException ex)
        {
            Response.Write("exception!" + ex.Message);
            this.DisplayMessage(ex.Message);
            CartDisplayPanel.Visible = false;
            CartEmptyPanel.Visible = false;
            StudentItemMessagePanel.Visible = false;
        }
        if (completeNames == "")
        {
            QuoteThisButton.Visible = false;
        }
        else {
            QuoteThisButton.Visible = true;
        }
    }

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

    private void SetChildPage(string name)
    {
        Session["CurrentChilPage"] = name;
    }


    #region Row Operations


    void ItemsGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable dt = (DataTable)ItemsGrid.DataSource;
        if (e.Row.RowIndex < 0) return;

        DataRow dr = dt.Rows[e.Row.RowIndex];

        Control c = e.Row.FindControl("CompanyNameLinkLiteral");
        if (c != null && c.GetType().Equals(typeof(Literal)))
        {
            ((Literal)c).Text = PubLink((string)dr["PubName"], (string)dr["PubUrl"]);
        }
    }

    void ItemsList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataTable dt = (DataTable)ItemsList.DataSource;
        if (e.Item.ItemIndex < 0) return;
        string TmpStr; //patch mark
        DataRow dr = dt.Rows[e.Item.ItemIndex];
        
        Control c;
        
        c = e.Item.FindControl("CompanyNameLinkLiteral");
        if (c != null && c.GetType().Equals(typeof(Literal)))
        {
            ((Literal)c).Text = PubLink((string)dr["PubName"], (string)dr["PubUrl"]);
        }

        c = e.Item.FindControl("ProductImageLiteral");
        if (c != null && c.GetType().Equals(typeof(Literal)))
        {
            string basepath = strFolder;
            string imageurl = Get_Title_Image_by_Id((int)dr["ProductTitleID"]);
            //((Literal)c).Text = ProductImage(String.Format("{0}{1}", basepath, imageurl));
            //((Literal)c).Text = "<img src=\"" + strFolderTb + "tn_" + imageurl.ToString() + "\" onload=\"(document.getElementById('boxContImage'),this)\">";
		//((Literal)c).Text = "<img src=\"" + strFolderTb + "tn_" + imageurl.ToString() + "\" title=\"" + (string)dr["PubName"]+" : "+ titleAlt((string)dr["Title"]) +"\" >";
            if (DiscountManager.NeedsProof((int)dr["ProductTitleId"]))
            {
                TmpStr = "<img src='" + Global.globalSiteImagesPath + "/needs_proff.jpg' alt='needs academic proof' >" + "<img src=\"" + strFolderTb + "tn_" + imageurl.ToString() + "\" title=\"" + (string)dr["PubName"] + " : " + titleAlt((string)dr["Title"]) + "\" >";
            }
            else
            {
                TmpStr = "<img src=\"" + strFolderTb + "tn_" + imageurl.ToString() + "\" title=\"" + (string)dr["PubName"] + " : " + titleAlt((string)dr["Title"]) + "\" >";
            }

            ((Literal)c).Text = TmpStr;	
        // patch
        
        }        

        c = e.Item.FindControl("WishlistLiteral");
        if (c != null && c.GetType().Equals(typeof(Literal)))
        {
            ((Literal)c).Text = BuildWishlistLink((int)dr["ProductTitleID"], Convert.ToString(dr["ProductSKU"]));
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
            if (e.CommandName == "AddToWishlist")//Duncan Wish
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



    protected void ItemsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Helper.StringExists(e.CommandName))
        {
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
            DataTable dt = BuildDataSource(CurrentCart.Items);
            DataRow dr = dt.Rows[row.RowIndex];

            int cart_contents_id = (int)dr["CartItemId"];

            CartDB db = new CartDB();
            if (e.CommandName == "AddToWishlist")
            {
                //Response.Write("Adding to Wishlist: " + dr["CartItemId"].ToString());
                throw new NotImplementedException("Add To Wishlist Not Implemented");
            }
            else if (e.CommandName == "UpdateQuantity")
            {
                Control c = row.FindControl("QtyTextbox");
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
            row["Title"] = "<a href=\"product.aspx?p=" + item.ProductTitleId + "&s=" + item.ProductSKU + "\">" + title + "</a>";
            completeNames += title + "|";
            row["PubName"] = pubname;
            row["PubURL"] = puburl;
            row["Quantity"] = item.Quantity;
            completeQty += item.Quantity.ToString()+"|";
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
        if (items.Length > 0)
        {
            for (int i = 0; i < items.Length; i++)
            {
                CartItem first = (CartItem)items[i];
                if (Get_Title_RelatedProducts(first.ProductTitleId))
                {
                    RelatedProductsPanel.Visible = true;
                }
            }
        }

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
        //Response.Write(completeNames+completeQty);
        Session["quoteSession"] = null;
        Response.Redirect("requestaquote.aspx?title="+completeNames+"&qty="+completeQty);
        //throw new NotImplementedException("Quote this not implemented");
    }

    protected string PubLink(string name, string url)
    {
        Result res = new Result();
        DataSet data = new DataSet();
        data = res.Get_PubId(name);
        int id = 0;
        if (data != null)
        {
	        foreach (DataTable table in data.Tables)
	        {
	            foreach (DataRow row in table.Rows)
	            {
	                id = Convert.ToInt32(row["PubId"]);
	            }
	        }
	    }
        return "<a href=\"result.aspx?findopt5=" + id + "&am=1&asm=" + 3 + "\">" + name + "</a>";
        //if (Helper.StringExists(url))
        //{
        //    return Helper.URLToLink(url, name); //String.Format("<a href=\"{0}\">{1}</a>", url, name);
        //}
        //else
        //{
        //    return name;
        //}
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
                    return row["Imagetn"].ToString();
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

                    sb.AppendLine("<div class=\"SlideItMoo_element\"><a href=\"product.aspx?p=" + row["titleid"].ToString() + "\" style=\"background-color:#FFF;\"><img src=\"" + strFolder + row["imagetn"].ToString() + "\" title=\"" + row["PubName"].ToString() + " : " + row["title"].ToString() + "\" height=\"100px\"/></a><p>" + row["title"].ToString() + "</p></div>");
                }
            }
        }
        PlaceHolser_Slide.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;

        return has_related;
    }

    private string BuildWishlistLink(int TitleId,string sku)
    {

        //--Login Validate
        if (Session[SiteConstants.UserValidLogin] != null && (bool)Session[SiteConstants.UserValidLogin])
        {
            return String.Format("<a href=\"addWish.aspx?p={0}&sk={1}&skd=1\" rel=\"width:560,height:126,ajax:true\" id=\"mb10\" class=\"mb\" title=\"Add To Wishlist\"><img src=\"" + Global.globalSiteImagesPath + "/addToWish.jpg\" alt=\"Add To Wishlist\" /></a>", TitleId,sku);
        }
        else
        {
            return "<a href=\"#htmlElement\" id=\"mb15\" class=\"mb\" title=\"\" rel=\"type:element\"><img src=\"" + Global.globalSiteImagesPath + "/addToWish.jpg\" border=\"0\"></a>";
        }
        
    }
    public string titleAlt(string data)
    {
        string data2 = "";
        data2 = data.Remove(0, charTofind(data));
        data2 = data2.Replace("</a>","");
        		

        return data2;
    }

    public int charTofind(string data)
    {
        int result = 0;
        for (int i = 0; data[i].ToString() != ">"; i++)
        {
            result = i;
        }
        return result+2;

    }
}
