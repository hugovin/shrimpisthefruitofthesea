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

public partial class addWish : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string titleId = "";   
    private string message = "";

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
            CurrentChilPage = "addwish.aspx";
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "addwish.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }

        titleId = Request["p"];

        addProductWish();
    }

    protected void addProductWish()
    {
        SiteProduct classificationProducts = new SiteProduct();        
        DataSet dsclassificationProducts = new DataSet();
        StringBuilder sb = new StringBuilder();
        bool HeaderRun = false;
        SiteWish wish = new SiteWish();        
        DataSet dsWish = new DataSet();
      	string sSku = Request["sk"];
      	string sSkuDesc = Request["skd"];
        dsclassificationProducts = classificationProducts.Get_Product_by_Id(Convert.ToInt32(titleId));
      // wish.Add_Whish_Header();
        foreach (DataTable table in dsclassificationProducts.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                    wish._TitleId = Convert.ToInt32(row["titleId"].ToString());
                    wish._Title = row["title"].ToString();
                    //wish._DefaultSKU = row["SKU"].ToString();
                    wish._DefaultSKU = sSku;
                    //wish._TitleText = row["titleText"].ToString(); 
                    wish._TitleText = sSkuDesc;
                    wish.Add_Whish_Header();
            }
        }
        dsWish = wish.Get_Wish_by_IdSession();
        foreach (DataTable table in dsWish.Tables)
        {

            foreach (DataRow row in table.Rows)
            {
                wish._WishListId = Convert.ToInt32(row["WishListId"].ToString());
                message = wish.Add_Whish_Detail();   
                sb.AppendLine("<div class=\"quote\">");
                sb.AppendLine("<div class=\"quoteTop\">");
                sb.AppendLine("<div class=\"popTitle\">Add to Wish List</div>");
                sb.AppendLine("</div>");
                sb.AppendLine("<div class=\"quoteBody\">");
                sb.AppendLine(message);
                sb.AppendLine("</div>");
                sb.AppendLine("<div class=\"quoteTButt\"/></div>");
                sb.AppendLine("</div>");
                PlaceHolder_Message.Controls.Add(new LiteralControl(sb.ToString()));
            }
        }
        //PlaceHolder_Message.Controls.Add(new LiteralControl(sb.ToString()));
        wish = null;
        classificationProducts = null;
        dsclassificationProducts = null;
    }

}
