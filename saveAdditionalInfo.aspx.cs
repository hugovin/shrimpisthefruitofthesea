using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class saveAdditionalInfo : System.Web.UI.Page
{
    Cart CurrentCart;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "text/plain";
        Response.Write(" ");

        string cartid = String.Empty;
        string state = String.Empty;
        string sku = String.Empty;
        string spanish = String.Empty;
        string whiteboard = String.Empty;
        string repsystem = String.Empty;
        string description = String.Empty;

        cartid = Request.QueryString["cid"];
        state = Request.QueryString["sta"];
        sku = Request.QueryString["sku"];
        spanish = Request.QueryString["spa"];
        whiteboard = Request.QueryString["whb"];
        repsystem = Request.QueryString["res"];
        description = Request.QueryString["des"];
        this.CurrentCart = Cart.GetCartFromSession(Session);
        CartAdditionInfo cartinfo = new CartAdditionInfo();
        if (spanish.ToUpper() == "YES") 
            spanish = "1";
            else
            spanish = "0";

        try {
            cartinfo.addcartAdditionalInfo(CurrentCart.CartId, state, sku, int.Parse(spanish), whiteboard, repsystem,description);
            Response.Write("1");// Send a number 1 when insert is Ok.
        }
        catch (Exception ex)
        {
            Response.Write("0");// Send a number 0 when insert fails.
        }

    }
}
