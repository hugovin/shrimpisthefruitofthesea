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

public partial class getResourceVideooo : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string ProductId = "";
    private string TitleResourceId = "";

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
            CurrentChilPage = "getresourcevideooo.aspx";
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        GetVars();
        Session["CurrentChilPage"] = "getresourcevideooo.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }

        ProductId = Request["pid"];
        Video();
        
    }
    protected void addEventCapture() {
        FreeTools fs = new FreeTools();
        Login lg = new Login();
        fs.add_TitleResourceViewer(Convert.ToInt32(TitleResourceId));
    }
    protected void Video()
    {
        SiteProduct previewData = new SiteProduct();
        DataSet results = new DataSet();
        StringBuilder sb = new StringBuilder();
        results = previewData.Get_Site_Trials_Demos(Convert.ToInt32(ProductId), 53);
        foreach (DataTable table in results.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                //videoSpace.Text = row["TitleResourceLoc"].ToString();
                sb.AppendLine("<div class=\"quote\">");
                sb.AppendLine("<div class=\"quoteTop\">");
                sb.AppendLine("<div class=\"popTitle\">Demo</div>");
                sb.AppendLine("</div>");
                sb.AppendLine("<div class=\"quoteBody2\">");
                sb.AppendLine(row["TitleResourceLoc"].ToString());
                TitleResourceId =  row["TitleResourceId"].ToString();
                sb.AppendLine("</div>");
                sb.AppendLine("<div class=\"quoteTButt\"/></div>");
                sb.AppendLine("</div>");
                PlaceHolder_Video.Controls.Add(new LiteralControl(sb.ToString()));
            }
            if (table.Rows.Count == 0)
            {
                sb.AppendLine("<div class=\"quote\">");
                sb.AppendLine("<div class=\"quoteTop\">");
                sb.AppendLine("<div class=\"popTitle\">Video</div>");
                sb.AppendLine("</div>");
                sb.AppendLine("<div class=\"quoteBody\"><br><br>");
                sb.AppendLine("No video preview for this product");
                sb.AppendLine("<br><br></div>");
                sb.AppendLine("<div class=\"quoteTButt\"/></div>");
                sb.AppendLine("</div>");
                PlaceHolder_Video.Controls.Add(new LiteralControl(sb.ToString()));
            }
            else {
                addEventCapture();
            }
        }
        sb = null;
        previewData = null;
        results = null;
    }

}