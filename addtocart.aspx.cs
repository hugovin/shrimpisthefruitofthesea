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
using System.Collections.Generic;
using System.Collections.Specialized;

public partial class AddToCartPage : System.Web.UI.Page
{
    Cart CurrentCart;    

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "text/plain";
        Response.Write(" ");

        try
        {
            this.CurrentCart = Cart.GetCartFromSession(Session);//Duncan Here it gets the Cart related to the User Session.   
            if (this.CurrentCart.Dirty) this.CurrentCart.Clean();

            if (Request.RequestType == "GET" && Request.QueryString.HasKeys())
            {
                ProcessGet();
            }
            else if (Request.RequestType == "POST" && Request.Form.HasKeys())
            {
                ProcessPost();
            }
        }
        catch (CartException ex)
        {
            Response.Write(this.BuildResponse(AddResponse.Failure));
        }
    }

    private void ProcessPost()
    {
        List<string> post = new List<string>(Request.Form.AllKeys);
        int title_id;
        if (post.Contains(Constants.QueryKeys.POST_TITLE_ID) && Int32.TryParse(Request.Form[Constants.QueryKeys.POST_TITLE_ID], out title_id) && post.Contains(Constants.QueryKeys.POST_SKU))
        {
            string sku = Request.Form[Constants.QueryKeys.POST_SKU];
            string w = Request.Form[Constants.QueryKeys.POST_WISHLIST_ID];

            int quantity = 1;
            if (Request.Form[Constants.QueryKeys.POST_QUANTITY] != null && Int32.TryParse(Request.Form[Constants.QueryKeys.POST_QUANTITY], out quantity)) { }

            SiteWish wish = new SiteWish();
            wish.Del_Whish_Header(Convert.ToInt32(w));

            AddResponse r = AddToCart(title_id, sku, quantity);
	    
	    Result result = new Result();
	    string Sap = "0";
	    Sap= result.Get_Title_by_Student_Pricing(title_id);

	    string output;

            if (Sap=="1" && r == AddResponse.Success)
                output = this.BuildResponse(AddResponse.Student);
            else
                output = this.BuildResponse(r);

            Response.Write(output);
        }
    }

    private void ProcessGet()
    {
        List<string> get = new List<string>(Request.QueryString.AllKeys);
        int title_id;
        if (get.Contains(Constants.QueryKeys.GET_TITLE_ID) && Int32.TryParse(Request.QueryString[Constants.QueryKeys.GET_TITLE_ID], out title_id) && get.Contains(Constants.QueryKeys.GET_SKU))
        {
            string sku = Request.QueryString[Constants.QueryKeys.GET_SKU];
            string w = Request.QueryString[Constants.QueryKeys.GET_WISHLIST_ID];

            int quantity = 1;
            if (Request.Form[Constants.QueryKeys.GET_QUANTITY] != null && Int32.TryParse(Request.Form[Constants.QueryKeys.GET_QUANTITY], out quantity)) { }

            SiteWish wish = new SiteWish();
            wish.Del_Whish_Header(Convert.ToInt32(w));

            AddResponse r = AddToCart(title_id, sku, quantity);

            //string output = this.BuildResponse(r);
            //Response.Write(output);
            Response.Redirect(String.Format("{0}?result={1}", Constants.Pages.CART, HttpUtility.UrlEncode(r.ToString())));
        }
    }

    private AddResponse AddToCart(int title_id, string sku, int quantity)
    {
        AddResponse r = AddResponse.Failure;
        
        // Add to cart code here
        // We need to do a number of things
        // 0) First, we need to make sure that the title_id is a valid product that can be added to a cart
        // 1) Check to see if a cart exists
        //    a) If it doesn't, create a new 
        // 2) Then we need to add the item to the current cart
        // 3) We need to check to make sure that it was successfully added
        //    a) If it wasn't, generate the approriate error message
        // 4) Then we need to build a response, so the calling page can handle the add to cart status appropriately

        if (this.AddItemToCart(title_id, sku, quantity, out r))
        {
        }
        
        //Actually add to the cart.
        return r;
    }

    private bool AddItemToCart(int title_id, string sku, int quantity, out AddResponse result)
    {
        result = AddResponse.Failure;

        bool success = false; //Duncan Working   
        CartDB db = new CartDB();

        DataSet ds = db.CartProductGetBySKU(sku);
        if (ds == null) return false;
        
        bool is_bulk = false, is_student = false;

        using (DataTableReader data = ds.CreateDataReader())
        {
            while (data.Read())
            {
                is_bulk = data.GetInt32(data.GetOrdinal("IsBulk")) == 1;
                is_student = data.GetInt32(data.GetOrdinal("IsStudent")) == 1;
                break;
            }
        }

        if (is_bulk)
        {
            result |= AddResponse.Bulk;
            return false;
        }

        success = db.CartAddSKU(CurrentCart.CartId, sku, quantity);

        if (success)
        {
            result = AddResponse.Success;
        }

        if (is_student)
        {
            result |= AddResponse.Student;
        }

        return success;
    }

    private string BuildResponse(AddResponse response_type)
    {
        StringBuilder output = new StringBuilder();
        //output.Append(Enum.Format(typeof(AddResponse), response_type, "f"));
        output.Append(Enum.Format(typeof(AddResponse), response_type, "d"));
        return output.ToString();
    }

    private void Redirect(int title_id, AddResponse r)
    {
        if (Request.UrlReferrer != null)
        {
            StringBuilder redirect_uri = new StringBuilder(Request.UrlReferrer.OriginalString);

            string prefix = "";
            string url = string.Format("add_tid={2}&add_response={1}", title_id, (int)r);

            if (Request.UrlReferrer.Query != null && Request.UrlReferrer.Query.Length > 0)
            {
                prefix = "&";
            }
            else
            {
                prefix = "?";
            }

            redirect_uri.AppendFormat("{0}{1}", prefix, url);

            Response.Redirect(redirect_uri.ToString());
        }
    }
}

public enum AddResponse
{
    None=0,
    Success=1,
    Bulk=2,
    Student=4,
    Failure=8
}
