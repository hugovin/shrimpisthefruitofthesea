using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Web.Caching;
using System.Collections.Generic;
using System.Web.SessionState;

public class Cart
{
    #region DB Schema
    /*
      @CartId int
      @SiteId int
      @UserId varchar(50)
      @CheckoutStep int
      @ShippingOption int
      @PaymentId int
      @ShippingLocationId int
      @BillingLocationId int
      @Tax numeric
      @Shipping numeric
      @Handling numeric
      @SubTotal numeric
      @Total numeric
      @Dirty bit
      @DateCreated datetime
      @LastUpdated datetime
    */
    #endregion

    #region Private Variables
    private int _CartId;
    private int _SiteId;
    private string _UserId;
    private int _CheckoutStep;
    private int _ShippingOption;
    private int _PaymentId;
    private int _ShippingLocationId;
    private int _BillingLocationId;
    private decimal _Tax;
    private decimal _Shipping;
    private decimal _Handling;
    private decimal _SubTotal;
    private decimal _Total;
    private bool _Dirty;
    private bool _Completed;
    private DateTime _DateCreated;
    private DateTime _LastUpdated;
    private Guid _UniqueId;

    private bool _IsLoaded;
    #endregion

    #region Public Properties

    #region DB Props
    public int CartId
    {
        get { return this._CartId; }
    }
    public int SiteId
    {
        get { return this._SiteId; }
    }
    public string UserId
    {
        get { return this._UserId; }
    }
    public int CheckoutStep
    {
        get { return this._CheckoutStep; }
    }
    public int ShippingOption
    {
        get { return this._ShippingOption; }
    }
    public int PaymentId
    {
        get { return this._PaymentId; }
    }
    public int ShippingLocationId
    {
        get { return this._ShippingLocationId; }
    }
    public int BillingLocationId
    {
        get { return this._BillingLocationId; }
    }
    public decimal Tax
    {
        get { return this._Tax; }
    }
    public decimal Shipping
    {
        get { return this._Shipping; }
    }
    public decimal Handling
    {
        get { return this._Handling; }
    }
    public decimal SubTotal
    {
        get { return this._SubTotal; }
    }
    public decimal Total
    {
        get { return this._Total; }
    }
    public bool Dirty
    {
        get { return this._Dirty; }
    }
    public DateTime DateCreated
    {
        get { return this._DateCreated; }
    }
    public DateTime LastUpdated
    {
        get { return this._LastUpdated; }
    }
    public bool Completed
    {
        get { return this._Completed; }
    }
    public Guid UniqueId
    {
        get { return this._UniqueId; }
    }
    #endregion

    public bool IsLoaded
    {
        get { return this.CartId > 0 && this._IsLoaded; }
    }

    public bool HasItems
    {
        get { return this.Items.Length > 0; }
    }

    public bool HasStudentItems
    {
        get
        {
            bool student = false;

            foreach (CartItem item in Items)
            {
                if (item.IsStudent)
                {
                    student = true;
                    break;
                }
            }

            return student;
        }
    }

    public CartItem[] Items
    {
        get { return CartItem.GetCartItemsByCartID(this._CartId); }
    }

    public CartLocation ShippingLocation
    {
        get { return CartLocation.GetCartLocationByLocationID(this._ShippingLocationId); }
    }

    public CartLocation BillingLocation
    {
        get { return CartLocation.GetCartLocationByLocationID(this._BillingLocationId); }
    }

    public CartPayment Payment
    {
        get { return CartPayment.GetCartPaymentByPaymentID(this._PaymentId); }
    }

    public double ItemsWeight
    {
        get
        {
            //try
           // {
                CartDB db = new CartDB();
                return db.CalculateCartItemsWeight(this._CartId);
            /*}
            catch
            {
                return 0;
            }*/
        }
    }
    #endregion

    #region Constructors
    protected Cart() { }

    protected Cart(Guid unique_id)
    {
        this._UniqueId = unique_id;
        this.Refresh(CartRefreshMethod.UniqueID);
    }

    protected Cart(int cart_id)
	{
        this._CartId = cart_id;
        this.Refresh(CartRefreshMethod.CartID);
	}

    protected Cart(int site_id, string user_id)
    {
        this._SiteId = site_id;
        this._UserId = user_id;
        this.Refresh(CartRefreshMethod.UserID);
    }

    public static Cart CreateNew(int site_id, string user_id)
    {
        CartDB db = new CartDB();

        if (db.CartCreate(site_id, user_id))
        {
            return Cart.GetCartByUserID(site_id, user_id);
        }

        throw new CartException("Could not create cart");
    }

    public static Cart GetCartByGuid(Guid guid)
    {
        return new Cart(guid);
    }

    public static Cart GetCartByCartID(int cart_id)
    {
        return new Cart(cart_id);
    }

    public static Cart GetCartByUserID(int site_id, string user_id)
    {
        return new Cart(site_id, user_id);
    }

    public static Cart GetCartFromCookie(HttpCookieCollection Cookies)
    {
        
        // If we have a cart cookie, try loading from the guid found in the cookie
        if (Cookies[Constants.CookieKeys.CURRENT_CART] != null && Cookies[Constants.CookieKeys.CURRENT_CART].Values.HasKeys())
        {
            HttpCookie cart_cookie = Cookies[Constants.CookieKeys.CURRENT_CART];
            string cart_id = cart_cookie.Values.Get("cart_id");
            string guid_id = cart_cookie.Values.Get("guid_id");

            Cart cart = Cart.GetCartByGuid(new Guid(guid_id));
            if (cart != null && cart.IsLoaded && cart.CartId.ToString() == cart_id)
            {
                return cart;
            }
        }
        throw new CartException("No Cart Cookie Found");
    }

    public static Cart GetCartFromSession(HttpSessionState Session)
    {
        Cart cart = null;
        int site_id;
        bool logged_in = CartUsers.IsUserLoggedIn(Session);
        string user_id = logged_in ? CartUsers.GetUserName(Session) : "";
        if (Session[Constants.SessionKeys.SITE_ID] != null)
        {
            if (Int32.TryParse((string)Session[Constants.SessionKeys.SITE_ID], out site_id) == false)
            {
                throw new CartException("Could not find SiteID in current session");
            }
        }
        else
        {
            throw new CartException("Could not find SiteID in current session");
        }

        // This is a three step process
        // We do this because the current context of the shopping cart, should always override the saved context of the shopping cart. 
        // This makes most sense in this scenario:
        /* So say you were browsing the site without logging in, and you've added a bunch of items to your cart.
           Now, you don't hit checkout or anything, but instead decide to log in to your account you remember you had.
           Should the cart you just created override any existing (saved) cart in the user account? */

        // 0. If a user is logged in, and the current session doesn't have any items in it, and a saved session exists, use it. 
        if (logged_in)
        {
            cart = Cart.GetCartByUserID(site_id, Session.SessionID);
            if (cart != null && cart.IsLoaded && cart.HasItems == false)
            {
                cart = Cart.GetCartByUserID(site_id, user_id);
                if (cart != null && cart.IsLoaded)
                {
                    return cart;
                }
            }
        }

        // 1. If one exists in the current session, and it actually has items in it, use it. In this case, if the username has not been updated, update it. 
        cart = Cart.GetCartByUserID(site_id, Session.SessionID);
        if (cart != null && cart.IsLoaded)
        {
            if (logged_in && cart.UserId == Session.SessionID)
            {
                CartDB db = new CartDB();
                db.CartUpdateUserId(cart.CartId, user_id);
            }
            cart.Refresh();
            return cart;
        }

        // 2. If this is not the case, try loading from the user id.
        if (logged_in)
        {
            cart = Cart.GetCartByUserID(site_id, user_id);
            if (cart != null && cart.IsLoaded)
            {
                return cart;
            }
        }

        // 3. If we still do not have one, just create a new one. If logged in, use the current username, if not, use the session id
        cart = Cart.CreateNew(site_id, logged_in ? user_id : Session.SessionID);
        if (cart != null && cart.IsLoaded)
        {
            return cart;
        }

        throw new CartException("Could not retrieve a cart from the current session");
    }
    #endregion

    #region Data Methods
    #region Refreshing Data
    public void Refresh()
    {
        this.Refresh(CartRefreshMethod.CartID);
    }
    public void Refresh(CartRefreshMethod type)
    {
        CartDB db = new CartDB();
        DataSet cart_data = null;

        switch(type)
        {
            case CartRefreshMethod.UniqueID:
                cart_data = db.GetCartFromGuid(this._UniqueId);
                break;
            case CartRefreshMethod.CartID:
                if (this._CartId > 0)
                {
                    cart_data = db.GetCartFromCartID(this._CartId);
                }
                break;
            case CartRefreshMethod.UserID:
                if (this._SiteId > 0 && this._UserId != null)
                {
                    cart_data = db.GetCartFromUserID(this._SiteId, this._UserId);
                }
                break;
        }
        
        this._populate_cart_from_dataset(cart_data);
    }
    private void _populate_cart_from_dataset(DataSet ds)
    {
        bool found = false;
        if (ds != null)
        {
            using (DataTableReader data = ds.CreateDataReader())
            {
                while (data.Read())
                {
                    if (!found) found = true;

                    this._CartId = data.GetInt32(data.GetOrdinal("CartId"));
                    this._SiteId = data.GetInt32(data.GetOrdinal("SiteId"));
                    this._UserId = data.GetString(data.GetOrdinal("UserId"));
                    this._CheckoutStep = data.GetInt32(data.GetOrdinal("CheckoutStep"));
                    this._ShippingOption = data.GetInt32(data.GetOrdinal("ShippingOption"));

                    object _payment_id = data.GetValue(data.GetOrdinal("PaymentId"));
                    Int32.TryParse(_payment_id.ToString(), out this._PaymentId);

                    object _shipping_location_id = data.GetValue(data.GetOrdinal("ShippingLocationId"));
                    Int32.TryParse(_shipping_location_id.ToString(), out this._ShippingLocationId);

                    object _billing_location_id = data.GetValue(data.GetOrdinal("BillingLocationId"));
                    Int32.TryParse(_billing_location_id.ToString(), out this._BillingLocationId);

                    this._Tax = data.GetDecimal(data.GetOrdinal("Tax"));
                    this._Shipping = data.GetDecimal(data.GetOrdinal("Shipping"));
                    this._Handling = data.GetDecimal(data.GetOrdinal("Handling"));
                    this._SubTotal = data.GetDecimal(data.GetOrdinal("SubTotal"));
                    this._Total = data.GetDecimal(data.GetOrdinal("Total"));

                    this._Dirty = data.GetBoolean(data.GetOrdinal("Dirty"));
                    this._DateCreated = data.GetDateTime(data.GetOrdinal("DateCreated"));
                    this._LastUpdated = data.GetDateTime(data.GetOrdinal("LastUpdated"));

                    this._Completed = data.GetBoolean(data.GetOrdinal("Completed"));

                    this._UniqueId = data.GetGuid(data.GetOrdinal("UniqueId"));
                }
            }
        }

        this._IsLoaded = found;
    }
    #endregion

    #region Update Checkout Step
    public void MoveNextStep()
    {
        this.MoveToStep(this._CheckoutStep + 1);
    }

    public void MovePrevStep()
    {
        this.MoveToStep(this._CheckoutStep - 1);
    }

    public void MoveToStep(Constants.CheckoutStep step)
    {
        this.MoveToStep((int)step);
    }

    public void MoveToStep(int step)
    {
        CartDB db = new CartDB();
        if (step >= 0 && db.UpdateCartCheckoutStep(this._CartId, step))
        {
            this._CheckoutStep = step;
        }
    }

    public void Clean()
    {
        if (this.IsLoaded)
        {
            CartDB db = new CartDB();
            db.CleanCart(this);
            Refresh();
        }
    }
    #endregion
    #endregion 

    public static string CreateAddToCartLink(string text, string TitleID, string SKU, int quantity, int wishlistId)
    {
        string title_key = Constants.QueryKeys.GET_TITLE_ID;
        string sku_key = Constants.QueryKeys.GET_SKU;
        string quantity_key = Constants.QueryKeys.GET_QUANTITY;
        string wishlist_key = Constants.QueryKeys.GET_WISHLIST_ID;

        return String.Format("<a href=\"addtocart.aspx?{0}={1}&{2}={3}&{4}={5}&{7}={8}\" onclick=\"AddToCart('{1}', '{3}', '', '{8}'); return false\" >{6}</a>", title_key, TitleID.Trim(), sku_key, SKU.Trim(), quantity_key, quantity, text.Trim(), wishlist_key, wishlistId);        


    }
    public static string CreateAddToCartLink(string text, string TitleID, string SKU, int quantity, string productName)
    {
        string title_key = Constants.QueryKeys.GET_TITLE_ID;
        string sku_key = Constants.QueryKeys.GET_SKU;
        string quantity_key = Constants.QueryKeys.GET_QUANTITY;
        int wishlist_key = 0;

        return String.Format("<a href=\"addtocart.aspx?{0}={1}&{2}={3}&{4}={5}\" onclick=\"AddToCart('{1}', '{3}','{7}','{8}'); return false\" >{6}</a>", title_key, TitleID.Trim(), sku_key, SKU.Trim(), quantity_key, quantity, text.Trim(), productName, wishlist_key);
    }
    public static string CreateAddToCartLinkProduct(string text, string TitleID, string SKU, int quantity, string productName)
    {
    	String titlecut = productName;
        String res = titlecut.Replace("'", "\\'");
        
        string title_key = Constants.QueryKeys.GET_TITLE_ID;
        string sku_key = Constants.QueryKeys.GET_SKU;
        string quantity_key = Constants.QueryKeys.GET_QUANTITY;
        int wishlist_key = 0;

        return String.Format("<a href=\"addtocart.aspx?{0}={1}&{2}={3}&{4}={5}\" onclick=\"AddToCartProduct('{1}', '{3}','{7}','{8}',document.getElementById('qtyProduct').value); return false\" >{6}</a>", title_key, TitleID.Trim(), sku_key, SKU.Trim(), quantity_key, quantity, text, res, wishlist_key);
    }


}

public enum CartRefreshMethod
{
    UniqueID, 
    CartID, 
    UserID
}

public class CartItem : CartDB
{
    #region DB Schema
    /*
    (<CartId, int,>
    ,<ProductTitleId, int,>
    ,<ProductSKUId, int,>
    ,<Quantity, int,>
    ,<UnitPrice, numeric,>
    ,<SubTotal, numeric,>
    ,<LineNumber, int,>
    ,<DateCreated, datetime,>
    ,<LastUpdated, datetime,>)
    */
    #endregion
    
    #region Private Variables
    private int _CartItemId;
    private int _CartId;
    private int _ProductTitleId;
    private string _ProductSKU;
    private int _Quantity;
    private decimal _UnitPrice;
    private decimal _SubTotal;
    private int _LineNumber;
    private bool _IsBulk;
    private bool _IsStudent;
    private DateTime _DateCreated;
    private DateTime _LastUpdated;
    #endregion
    
    #region Public Properties
    public int CartItemId
    {
        get { return this._CartItemId; }
    }

    public int CartId
    {
        get { return this._CartId; }
    }

    public int ProductTitleId
    {
        get { return this._ProductTitleId; }
    }

    public string ProductSKU
    {
        get { return this._ProductSKU; }
    }

    public int Quantity
    {
        get { return this._Quantity; }
    }

    public decimal UnitPrice
    {
        get { return this._UnitPrice; }
    }

    public decimal SubTotal
    {
        get { return this._SubTotal; }
    }

    public int LineNumber
    {
        get { return this._LineNumber; }
    }

    public bool IsBulk
    {
        get { return this._IsBulk; }
    }

    public bool IsStudent
    {
        get { return this._IsStudent; }
    }

    public DateTime DateCreated
    {
        get { return this._DateCreated; }
    }

    public DateTime LastUpdated
    {
        get { return this._LastUpdated; }
    }
    #endregion

    #region Constructors
    protected CartItem()
    {
    }

    protected CartItem(int cart_item_id, int cart_id, int product_title_id, string product_sku, int quantity, decimal unit_price, decimal sub_total, int line_number, bool isbulk, bool isstudent, DateTime datecreated, DateTime lastupdated)
    {
        this._CartItemId = cart_item_id;
        this._CartId = cart_id;
        this._ProductTitleId = product_title_id;
        this._ProductSKU = product_sku;
        this._Quantity = quantity;
        this._UnitPrice = unit_price;
        this._SubTotal = sub_total;
        this._LineNumber = line_number;
        this._IsBulk = isbulk;
        this._IsStudent = isstudent;
        this._DateCreated = datecreated;
        this._LastUpdated = lastupdated;
    }

    public static CartItem GetCartItemByCartContentsID(int cart_contents_id)
    {
        return new CartItem();
    }

    public static CartItem GetCartItemByCartID(int cart_id, int lineno)
    {
        return new CartItem();
    }

    public static CartItem[] GetCartItemsByCartID(int cart_id)
    {
        List<CartItem> items = new List<CartItem>();

        CartDB db = new CartDB();
        DataSet ds = db.CartGetAllItems(cart_id);
        
        if (ds != null)
        {
            using (DataTableReader data = ds.CreateDataReader())
            {
                while (data.Read())
                {
                    int cartitemid = data.GetInt32(data.GetOrdinal("CartContentsId"));
                    int cartid = data.GetInt32(data.GetOrdinal("CartId"));
                    int producttitleid = data.GetInt32(data.GetOrdinal("ProductTitleId"));
                    string productsku = data.GetString(data.GetOrdinal("ProductSKU"));
                    int quantity = data.GetInt32(data.GetOrdinal("Quantity"));
                    decimal unitprice = data.GetDecimal(data.GetOrdinal("UnitPrice"));
                    decimal subtotal = data.GetDecimal(data.GetOrdinal("SubTotal"));
                    int linenumber = data.GetInt32(data.GetOrdinal("LineNumber"));
                    bool is_student = (int)data.GetValue(data.GetOrdinal("IsStudent")) == 1;
                    bool is_bulk = (int)data.GetValue(data.GetOrdinal("IsBulk")) == 1;
                    DateTime datecreated = data.GetDateTime(data.GetOrdinal("DateCreated"));
                    DateTime lastupdated = data.GetDateTime(data.GetOrdinal("LastUpdated"));
                    
                    CartItem item = new CartItem(cartitemid, cartid, producttitleid, productsku, quantity, unitprice, subtotal, linenumber,is_bulk, is_student, datecreated, lastupdated);
                    items.Add(item);
                }
            }
        }

        return items.ToArray();
    }

    
    #endregion

    #region Data Methods
    #endregion
}

public class CartLocation : CartDB
{
    #region DB Schema
    /*
      LocationId = LocationId, int;
      CartId = CartId, int;
      LocationType = LocationType, int;
      BusinessName = BusinessName, nvarchar(255);
      FullName = FullName, nvarchar(255);
      Address1 = Address1, nvarchar(255);
      Address2 = Address2, nvarchar(255);
      City = City, nvarchar(255);
      StateCode = StateCode, nvarchar(4);
      PostalCode = PostalCode, nvarchar(20);
      CountryCode = CountryCode, nvarchar(4);
      Phone = Phone, nvarchar(30);
      DateCreated = DateCreated, datetime;
      LastUpdated = LastUpdated, datetime;
    */
    #endregion

    #region Private Variables
    private int _LocationId;
    private int _CartId;
    private int _LocationType;
    private string _BusinessName;
    private string _FullName;
    private string _Address1;
    private string _Address2;
    private string _City;
    private string _StateCode;
    private string _PostalCode;
    private string _CountryCode;
    private string _Phone;
    private string _Email;
    private string _Title;
    private string _AreaCode;
    private string _PhoneExt;
    private string _Fax;
    private DateTime _DateCreated;
    private DateTime _LastUpdated;
    #endregion

    #region Public Properties
    public int LocationId
    {
        get { return this._LocationId; }
    }
    public int CartId
    {
        get { return this._CartId; }
    }
    public int LocationType
    {
        get { return this._LocationType; }
    }
    public string BusinessName
    {
        get { return this._BusinessName; }
    }
    public string FullName
    {
        get { return this._FullName; }
    }
    public string Address1
    {
        get { return this._Address1; }
    }
    public string Address2
    {
        get { return this._Address2; }
    }
    public string City
    {
        get { return this._City; }
    }
    public string StateCode
    {
        get { return this._StateCode; }
    }
    public string PostalCode
    {
        get { return this._PostalCode; }
    }
    public string CountryCode
    {
        get { return this._CountryCode; }
    }
    public string Phone
    {
        get { return this._Phone; }
    }
    public string Email
    {
        get { return this._Email; }
    }
    public string UTitle
    {
        get { return _Title; }
    }
    public string AreaCode
    {
        get { return this._AreaCode; }
    }
    public string PhoneExt
    {
        get { return this._PhoneExt; }
    }
    public string Fax
    {
        get { return this._Fax; }
    }

    public DateTime DateCreated
    {
        get { return this._DateCreated; }
    }
    public DateTime LastUpdated
    {
        get { return this._LastUpdated; }
    }

    #endregion

    #region Constructors
    protected CartLocation() { }

    protected CartLocation(int locationid, int cartid, int locationtype, string businessname, string fullname, string address1, string address2, string city, string statecode, string postalcode, string countrycode, string phone, string email, DateTime datecreated, DateTime lastupdated,string title,string areacode,string phoneext,string fax)
    {
        this._LocationId = locationid;
        this._CartId = cartid;
        this._LocationType = locationtype;
        this._BusinessName = businessname;
        this._FullName = fullname;
        this._Address1 = address1;
        this._Address2 = address2;
        this._City = city;
        this._StateCode = statecode;
        this._PostalCode = postalcode;
        this._CountryCode = countrycode;
        this._Phone = phone;
        this._Email = email;
        this._DateCreated = datecreated;
        this._LastUpdated = lastupdated;
        this._Title = title;
        this._AreaCode = areacode;
        this._PhoneExt = phoneext;
        this._Fax = fax;
    }

    public static CartLocation GetCartLocationByLocationID(int location_id)
    {

        CartDB db = new CartDB();
        DataSet ds = db.GetLocationFromLocationId(location_id);

        if (ds != null)
        {
            using (DataTableReader data = ds.CreateDataReader())
            {
                if (data.Read())
                {
                    int locationid = data.GetInt32(data.GetOrdinal("LocationId"));
                    int cartid = data.GetInt32(data.GetOrdinal("CartId"));
                    int locationtype = data.GetInt32(data.GetOrdinal("LocationType"));
                    string businessname = Helper.IsString(data.GetValue(data.GetOrdinal("BusinessName")), "");
                    string fullname = Helper.IsString(data.GetValue(data.GetOrdinal("FullName")), "");
                    string address1 = Helper.IsString(data.GetValue(data.GetOrdinal("Address1")), "");
                    string address2 = Helper.IsString(data.GetValue(data.GetOrdinal("Address2")), "");
                    string city = Helper.IsString(data.GetValue(data.GetOrdinal("City")), "");
                    string statecode = Helper.IsString(data.GetValue(data.GetOrdinal("StateCode")), "");
                    string postalcode = Helper.IsString(data.GetValue(data.GetOrdinal("PostalCode")), "");
                    string countrycode = Helper.IsString(data.GetValue(data.GetOrdinal("CountryCode")), "");
                    string phone = Helper.IsString(data.GetValue(data.GetOrdinal("Phone")), "");
                    string email = Helper.IsString(data.GetValue(data.GetOrdinal("Email")), "");
                    DateTime datecreated = data.GetDateTime(data.GetOrdinal("DateCreated"));
                    DateTime lastupdated = data.GetDateTime(data.GetOrdinal("LastUpdated"));
                    string Title = "";// CompletedCart.BillingLocation.UTitle;
                    string AreaCode =""; //CompletedCart.BillingLocation.AreaCode;
                    string PhoneExt = "";//CompletedCart.BillingLocation.PhoneExt;
                    string Fax = "";//CompletedCart.BillingLocation.Fax;  
     
                    return new CartLocation(locationid, cartid, locationtype, businessname, fullname, address1, address2, city, statecode, postalcode, countrycode, phone,
                        email, datecreated, lastupdated, Title, AreaCode, PhoneExt, Fax);
                }
            }
        }

        return null;
    }

    public static CartLocation GetBillingLocationByUserID(int user_id)
    {
        CartDB db = new CartDB();
        DataSet ds = db.LoginGetBillingAddress(user_id);

        if (ds != null)
        {
            using (DataTableReader data = ds.CreateDataReader())
            {
                if (data.Read())
                {
                    int locationid = 0;
                    int cartid = 0;
                    int locationtype = 0;
                    string businessname = Helper.IsString(data.GetValue(data.GetOrdinal("BldgName")), "");
                    string fullname = Helper.IsString(data.GetValue(data.GetOrdinal("FullName")), "");
                    string address1 = Helper.IsString(data.GetValue(data.GetOrdinal("Address1")), "");
                    string address2 = Helper.IsString(data.GetValue(data.GetOrdinal("Address2")), "");
                    string city = Helper.IsString(data.GetValue(data.GetOrdinal("City")), "");
                    string statecode = Helper.IsString(data.GetValue(data.GetOrdinal("State")), "");
                    string postalcode = Helper.IsString(data.GetValue(data.GetOrdinal("Zip")), "");
                    string countrycode = Helper.IsString(data.GetValue(data.GetOrdinal("Country")), "");
                    string phone = Helper.IsString(data.GetValue(data.GetOrdinal("Phone")), "");
		            string email = Helper.IsString(data.GetValue(data.GetOrdinal("Email")), "");
                    DateTime datecreated = DateTime.Now;
                    DateTime lastupdated = DateTime.Now;
                    
                    string Title = Helper.IsString(data.GetValue(data.GetOrdinal("Title")), "");
                    string AreaCode = Helper.IsString(data.GetValue(data.GetOrdinal("AreaCode")), "");
                    string PhoneExt = Helper.IsString(data.GetValue(data.GetOrdinal("PhoneExt")), "");
                    string Fax = Helper.IsString(data.GetValue(data.GetOrdinal("Fax")), "");                    
                    return new CartLocation(locationid, cartid, locationtype, businessname, fullname, address1, address2, city, statecode, postalcode, countrycode, phone, email, datecreated, lastupdated, Title, AreaCode, PhoneExt, Fax);
                }
            }
        }

        return null;
    }

    public static CartLocation GetCartLocationByCartID(int cart_id, int location_type)
    {
        return new CartLocation();
    }
    #endregion

    #region Data Methods
    /*
    this._LocationId = dtr.GetInt32(dtr.GetOrdinal("LocationId"));
    this._CartId = dtr.GetInt32(dtr.GetOrdinal("CartId"));
    this._LocationType = dtr.GetInt32(dtr.GetOrdinal("LocationType"));
    this._BusinessName = dtr.GetString(dtr.GetOrdinal("BusinessName"));
    this._FullName = dtr.GetString(dtr.GetOrdinal("FullName"));
    this._Address1 = dtr.GetString(dtr.GetOrdinal("Address1"));
    this._Address2 = dtr.GetString(dtr.GetOrdinal("Address2"));
    this._City = dtr.GetString(dtr.GetOrdinal("City"));
    this._StateCode = dtr.GetString(dtr.GetOrdinal("StateCode"));
    this._PostalCode = dtr.GetString(dtr.GetOrdinal("PostalCode"));
    this._CountryCode = dtr.GetString(dtr.GetOrdinal("CountryCode"));
    this._Phone = dtr.GetString(dtr.GetOrdinal("Phone"));
    this._DateCreated = dtr.GetDateTime(dtr.GetOrdinal("DateCreated"));
    this._LastUpdated = dtr.GetDateTime(dtr.GetOrdinal("LastUpdated"));
    */
    #endregion
}

public class CartPayment : CartDB
{    
    #region DB Schema
    /*[CartId] = <CartId, int,>
      ,[PaymentType] = <PaymentType, int,>
      ,[CCLastFourDigits] = <CCLastFourDigits, nvarchar(4),>
      ,[EncryptedCC] = <EncryptedCC, nvarchar(512),>
      ,[EncryptedCCExpiration] = <EncryptedCCExpiration, nvarchar(512),>
      ,[EncryptedCCCVV] = <EncryptedCCCVV, nvarchar(512),>
      ,[POFileUpload] = <POFileUpload, varbinary(max),>
      ,[DateCreated] = <DateCreated, datetime,>
      ,[LastUpdated] = <LastUpdated, datetime,>*/
    #endregion
    
    #region Private Variables
    private int _CartId;
    private int _PaymentType;
    private string _CCType;
    private string _CCLastFourDigits;
    private string _EncryptedCC;
    private string _EncryptedCCExpiration;
    private string _EncryptedCCCVV;
    private string _POFileName;
    private byte[] _POFileUpload;

    private int _Order_Id;
    private int _Order_Payment_Id;

    private DateTime _DateCreated;
    private DateTime _LastUpdated;
    #endregion
    
    #region Public Properties
    public int CartId
    {
        get { return this._CartId; }
    }
    public int PaymentType
    {
        get { return this._PaymentType; }
    }
    public string CCType
    {
        get { return this._CCType; }
    }
    public string CCLastFourDigits
    {
        get { return this._CCLastFourDigits; }
    }
    public string EncryptedCC
    {
        get { return this._EncryptedCC; }
    }
    public string EncryptedCCExpiration
    {
        get { return this._EncryptedCCExpiration; }
    }
    public string EncryptedCCCVV
    {
        get { return this._EncryptedCCCVV; }
    }
    public string POFileName
    {
        get { return this._POFileName; }
    }
    public byte[] POFileUpload
    {
        get { return this._POFileUpload; }
    }
    public DateTime DateCreated
    {
        get { return this._DateCreated; }
    }
    public DateTime LastUpdated
    {
        get { return this._LastUpdated; }
    }

    public int ORD_ID
    {
        get { return this._Order_Id; }
    }

    public int ORD_PAYMENT_ID
    {
        get { return this._Order_Payment_Id; }
    }

    #endregion

    #region Constructors
    protected CartPayment()
    {
    }

    protected CartPayment(int cartid, int paymenttype, string cctype, string cclastfourdigits, string encryptedcc, string encryptedccexpiration, string encryptedcccvv, string pofilename, byte[] pofileupload, int order_id, int order_payment_id, DateTime datecreated, DateTime lastupdated)
    {
        this._CartId = cartid;
        this._PaymentType = paymenttype;
        this._CCType = cctype;
        this._CCLastFourDigits = cclastfourdigits;
        this._EncryptedCC = encryptedcc;
        this._EncryptedCCExpiration = encryptedccexpiration;
        this._EncryptedCCCVV = encryptedcccvv;
        this._POFileName = pofilename;
        this._POFileUpload = pofileupload;
        this._Order_Id = order_id;
        this._Order_Payment_Id = order_payment_id;
        this._DateCreated = datecreated;
        this._LastUpdated = lastupdated;
    }

    public static CartPayment GetCartPaymentByPaymentID(int payment_id)
    {
        CartDB db = new CartDB();
        DataSet ds = db.GetPaymentFromPaymentId(payment_id);

        if (ds != null)
        {
            using (DataTableReader data = ds.CreateDataReader())
            {
                while (data.Read())
                {
                    int cartid = data.GetInt32(data.GetOrdinal("CartId"));
                    int paymenttype = data.GetInt32(data.GetOrdinal("PaymentType"));
                    string cctype = Helper.IsString(data.GetValue(data.GetOrdinal("CCType")), "");
                    string cclastfourdigits = Helper.IsString(data.GetValue(data.GetOrdinal("CCLastFourDigits")), "");
                    string encryptedcc = Helper.IsString(data.GetValue(data.GetOrdinal("EncryptedCC")), "");
                    string encryptedccexpiration = Helper.IsString(data.GetValue(data.GetOrdinal("EncryptedCCExpiration")), "");
                    string encryptedcccvv = Helper.IsString(data.GetValue(data.GetOrdinal("EncryptedCCCVV")), "");
                    string pofilename = Helper.IsString(data.GetValue(data.GetOrdinal("POFileName")), "");
                    object obj_pofileupload = data.GetValue(data.GetOrdinal("POFileUpload"));
                    byte[] pofileupload = null;
                    if (obj_pofileupload != null && obj_pofileupload.GetType().Equals(typeof(byte[])))
                    {
                       pofileupload = (byte[])obj_pofileupload;
                    }

                    int ord_id = 0, ord_payment_id = 0;

                    object obj_ord_id = data.GetValue(data.GetOrdinal("ORD_ID"));
                    if (obj_ord_id != null && obj_ord_id.GetType().Equals(typeof(int)))
                    {
                        ord_id = (int)obj_ord_id;
                    }
                    
                    object obj_ord_payment_id = data.GetValue(data.GetOrdinal("ORD_Payment_ID"));
                    if (obj_ord_payment_id != null && obj_ord_payment_id.GetType().Equals(typeof(int)))
                    {
                        ord_payment_id = (int)obj_ord_payment_id;
                    }

                    DateTime datecreated = data.GetDateTime(data.GetOrdinal("DateCreated"));
                    DateTime lastupdated = data.GetDateTime(data.GetOrdinal("LastUpdated"));

                    return new CartPayment(cartid, paymenttype, cctype, cclastfourdigits, encryptedcc, encryptedccexpiration, encryptedcccvv, pofilename, pofileupload, ord_id, ord_payment_id, datecreated, lastupdated);
                }
            }
        }

        return null;
    }

    public static CartPayment GetCartPaymentByCartID(int cart_id)
    {
        //return new CartPayment();
        throw new NotImplementedException();
    }
    #endregion

    #region Data Methods
    #endregion
}

public enum PaymentType
{
    CC=1, 
    PO
}

public class ShippingOption : CartDB
{
    public int ShippingOptionID;
    public string Label;
    public string Code;

    public ShippingOption(int option_id, string label, string code)
    {
        this.ShippingOptionID = option_id;
        this.Label = label;
        this.Code = code;
    }

    public static ShippingOption[] GetActiveShippingOptions()
    {
        // TODO: Implement this correctly to pull the active shipping options from the DB
        List<ShippingOption> active = new List<ShippingOption>();

        active.Add(new ShippingOption(1, "UPS Ground", Constants.ShippingTypes.GROUND));
        //active.Add(new ShippingOption(2, "UPS 2nd Day", Constants.ShippingTypes.SECOND_DAY));
        //active.Add(new ShippingOption(3, "UPS Next Day", Constants.ShippingTypes.NEXT_DAY));

        return active.ToArray();
    }
}

public class LoginAddress
{
    #region DB Schema
    /*
    @LoginID int
    ,@AddressName varchar(100)
    ,@BldgName varchar(100)
    ,@Address1 varchar(100)
    ,@Address2 varchar(100)
    ,@City varchar(100)
    ,@StateCode varchar(2)
    ,@Province varchar(50)
    ,@Zip varchar(10)
    ,@CountryCode varchar(2)
    */
    #endregion

    #region Private Variables
    private int _LoginAddressID;
    private int _LoginID;
    private string _AddressName;
    private string _BldgName;
    private string _Address1;
    private string _Address2;
    private string _City;
    private string _StateCode;
    private string _Province;
    private string _Zip;
    private string _CountryCode;
    private string _Phone;
    #endregion

    #region Public Properties
    public int LoginAddressID
    {
        get { return this._LoginAddressID; }
    }
    public int LoginID
    {
        get { return this._LoginID; }
    }
    public string AddressName
    {
        get { return this._AddressName; }
    }
    public string BldgName
    {
        get { return this._BldgName; }
    }
    public string Address1
    {
        get { return this._Address1; }
    }
    public string Address2
    {
        get { return this._Address2; }
    }
    public string City
    {
        get { return this._City; }
    }
    public string StateCode
    {
        get { return this._StateCode; }
    }
    public string Province
    {
        get { return this._Province; }
    }
    public string Zip
    {
        get { return this._Zip; }
    }
    public string CountryCode
    {
        get { return this._CountryCode; }
    }
    public string Phone
    {
        get { return this._Phone; }
    }

    #endregion

    #region Constructors
    protected LoginAddress() { }

    public LoginAddress(int loginaddressid, int loginid, string addressname, string bldgname, string address1, string address2, string city, string statecode, string province, string zip, string countrycode,string phone)
    {
        this._LoginAddressID = loginaddressid;
        this._LoginID = loginid;
        this._AddressName = addressname;
        this._BldgName = bldgname;
        this._Address1 = address1;
        this._Address2 = address2;
        this._City = city;
        this._StateCode = statecode;
        this._Province = province;
        this._Zip = zip;
        this._CountryCode = countrycode;
        this._Phone = phone;
    }
    #endregion
    
    #region Data Methods
    public static LoginAddress GetAddressFromLoginAddressID(int login_address_id)
    {
        CartDB db = new CartDB();
        DataSet ds = db.LoginGetAddress(login_address_id);
        if (ds != null)
        {
            using (DataTableReader data = ds.CreateDataReader())
            {
                while (data.Read())
                {
                    int loginaddressid = data.GetInt32(data.GetOrdinal("LoginAddressID"));
                    int loginid = data.GetInt32(data.GetOrdinal("LoginID"));
                    string addressname = Helper.IsString(data.GetValue(data.GetOrdinal("AddressName")), "");
                    string bldgname = Helper.IsString(data.GetValue(data.GetOrdinal("BldgName")), "");
                    string address1 = Helper.IsString(data.GetValue(data.GetOrdinal("Address1")), "");
                    string address2 = Helper.IsString(data.GetValue(data.GetOrdinal("Address2")), "");
                    string city = Helper.IsString(data.GetValue(data.GetOrdinal("City")), "");
                    string statecode = Helper.IsString(data.GetValue(data.GetOrdinal("StateCode")), "");
                    string province = Helper.IsString(data.GetValue(data.GetOrdinal("Province")), "");
                    string zip = Helper.IsString(data.GetValue(data.GetOrdinal("Zip")), "");
                    string countrycode = Helper.IsString(data.GetValue(data.GetOrdinal("CountryCode")), "");
                    string phone = Helper.IsString(data.GetValue(data.GetOrdinal("Phone")), "");

                    return new LoginAddress(loginaddressid, loginid, addressname, bldgname, address1, address2, city, statecode, province, zip, countrycode,phone);
                }
            }
        }
        return null;
    }



    public static LoginAddress[] GetAddressesFromLoginID(int login_id)
    {
        List<LoginAddress> addresses = new List<LoginAddress>();
        
        CartDB db = new CartDB();
        DataSet ds = db.LoginGetAddresses(login_id);
        
        if (ds != null)
        {
            using (DataTableReader data = ds.CreateDataReader())
            {
                while (data.Read())
                {
                    int loginaddressid = data.GetInt32(data.GetOrdinal("LoginAddressID"));
                    int loginid = data.GetInt32(data.GetOrdinal("LoginID"));
                    string addressname = Helper.IsString(data.GetValue(data.GetOrdinal("AddressName")), "");
                    string bldgname = Helper.IsString(data.GetValue(data.GetOrdinal("BldgName")), "");
                    string address1 = Helper.IsString(data.GetValue(data.GetOrdinal("Address1")), "");
                    string address2 = Helper.IsString(data.GetValue(data.GetOrdinal("Address2")), "");
                    string city = Helper.IsString(data.GetValue(data.GetOrdinal("City")), "");
                    string statecode = Helper.IsString(data.GetValue(data.GetOrdinal("StateCode")), "");
                    string province = Helper.IsString(data.GetValue(data.GetOrdinal("Province")), "");
                    string zip = Helper.IsString(data.GetValue(data.GetOrdinal("Zip")), "");
                    string countrycode = Helper.IsString(data.GetValue(data.GetOrdinal("CountryCode")), "");
                    string phone = Helper.IsString(data.GetValue(data.GetOrdinal("Phone")), "");

                    addresses.Add(new LoginAddress(loginaddressid, login_id, addressname, bldgname, address1, address2, city, statecode, province, zip, countrycode,phone));
                }
            }
        }
        return addresses.ToArray();
    }
    #endregion
}

public class CartException : System.ApplicationException
{
    public CartException(string msg) : base(msg) { }
}
