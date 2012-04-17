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

public partial class delWish : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string SubjId = "";
    private string wishId = "";
    private string titleId = "";
    private string sku = "";  
    private string strFolder = "images";
    private int error = 0;

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
            CurrentChilPage = "delwish.aspx";
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "delwish.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }
       
        wishId = Request["w"];
        titleId = Request["id"];
        sku = Request["sku"];

        delProductWish();
    }

    protected void delProductWish()
    {
        SiteWish wish = new SiteWish();
        StringBuilder sb = new StringBuilder();

        //error = wish.Del_Whish_Header(Convert.ToInt32(wishId));
       // if (error == 0){
            wish.Del_Whish_Detail(Convert.ToInt32(wishId),Convert.ToInt32(titleId),sku);
             if (error == 0){
                 sb.AppendLine("<div class=\"quote\">");
                 sb.AppendLine("<div class=\"quoteTop\">");
                 sb.AppendLine("<div class=\"popTitle\">Wish List.</div>");
                 sb.AppendLine("</div>");
                 sb.AppendLine("<div class=\"quoteBody\">");
                 sb.AppendLine("This item has been deleted from your Wish List.");
                 sb.AppendLine("</div>");
                 sb.AppendLine("<div class=\"quoteTButt\"/></div>");
                 sb.AppendLine("</div>");
                 PlaceHolder_Message.Controls.Add(new LiteralControl(sb.ToString()));
             }else{
                 sb.AppendLine("<div class=\"quote\">");
                 sb.AppendLine("<div class=\"quoteTop\">");
                 sb.AppendLine("<div class=\"popTitle\">Wish List.</div>");
                 sb.AppendLine("</div>");
                 sb.AppendLine("<div class=\"quoteBody\">");
                 sb.AppendLine("Error trying to add a product.");
                 sb.AppendLine("</div>");
                 sb.AppendLine("<div class=\"quoteTButt\"/></div>");
                 sb.AppendLine("</div>");
                 PlaceHolder_Message.Controls.Add(new LiteralControl(sb.ToString()));
             }
        /*}else{
            sb.AppendLine("<div class=\"quote\">");
            sb.AppendLine("<div class=\"quoteTop\">");
            sb.AppendLine("<div class=\"popTitle\">Wish List.</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"quoteBody\">");
            sb.AppendLine("Error tring to add a product.");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"quoteTButt\"/></div>");
            sb.AppendLine("</div>");
            PlaceHolder_Message.Controls.Add(new LiteralControl(sb.ToString()));
        }*/
    }
}
