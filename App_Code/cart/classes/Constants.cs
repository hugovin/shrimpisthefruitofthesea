using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public static class Constants
{
    public static class SessionKeys
    {
        //public readonly static string CART = "CurrentCartObject";
        public readonly static string CART_ID = "CurrentCartID";
        public readonly static string SITE_ID = "SiteID";

        public const string ORDER_ID = "OrderId";
        public const string ORDER_PAYMENT_ID = "OrderPaymentId";

        public const string USER_ID = "UserId";
        public const string USER_FULLNAME = "UserFullName";
        public const string USER_FIRSTNAME = "UserFirstName";
        public const string USER_LASTNAME = "UserLastName";
        public const string USER_ZIPCODE = "UserZipCode";
        public const string USER_VALID = "UserValidLogin";
        
        public const string SUMMARY_EDIT = "SummaryEdit";
    }

    public static class ShippingTypes
    {
        public static double MIN_SHIPPING = 6.00;

        public static string[] SHIPPING
        {
            get { return new string[] { "", "03", "02", "01" }; } 
        }

        public static readonly string GROUND = "03";
        public static readonly string THREE_DAY_SELECT = "12";
        public static readonly string SECOND_DAY = "02";
        public static readonly string SECOND_DAY_AM = "59";
        public static readonly string NEXT_DAY = "01";
        public static readonly string NEXT_DAY_SAVER = "13";
    }

    public static class CookieKeys
    {
        public readonly static string CURRENT_CART = "CurrentCartCookie";
    }

    public static class DataSetKeys
    {
        public readonly static string CART_TABLE = "CartData";
    }

    public static class QueryKeys
    {
        public readonly static string POST_TITLE_ID = "title_id";
        public readonly static string POST_SKU = "sku";
        public readonly static string POST_QUANTITY = "q";
        public readonly static string POST_WISHLIST_ID = "w";

        public readonly static string GET_TITLE_ID = "tid";
        public readonly static string GET_SKU = "sku";
        public readonly static string GET_QUANTITY = "q";
        public readonly static string GET_WISHLIST_ID = "w";
    }

    public static class ConfigKeys
    {
        public readonly static string UPS_KEY = "UPS_XML_ACCESS_KEY";
        public readonly static string PGP_PUB_KEY = "PGP_PUB_KEY";
        public readonly static string ORDER_EMAIL = "PO_EMAIL";
    }

    public static class Pages
    {
        public readonly static string LOGIN = "login.aspx";
        public readonly static string ADDTOCART = "addtocart.aspx";
        public readonly static string CART = "cart.aspx";
        public readonly static string CHECKOUT = "checkout.aspx";
    }

    public static class UserControls
    {
        public readonly static string CHECKOUT_STEPS_DIR = "app_controls/checkout/steps/";
        public readonly static string CHECKOUT_CONTROLS_DIR = "app_controls/checkout/controls/";
    }

    public static class Labels
    {
        public readonly static string[] CHECKOUT_STEPS = new string[] {
            "User Type",
            "Check Out", 
            "Shipping", 
            "Payment", 
            "Summary", 
            "Thank You",
            "Purchase Order"
        };
    }

    public enum CheckoutStep
    {
        UserType,
        Locations,
        ShippingOptions,
        Payment,
        Summary,
        ThankYou,
        PurchaseOrder
    }
}
