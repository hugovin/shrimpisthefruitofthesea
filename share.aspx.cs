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

public partial class share : System.Web.UI.Page
{
    //--    
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string strFolder = "images";
    private int TitleId = 0;
    private string sTitleId;
    //--
    public string title = "";
    public string content = "";
    public string sContent = "";
    public string urls = "";
    public string username = "No username indicated";

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
            CurrentChilPage = "share.aspx";
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "share.aspx";
        //--
        if (Request["title"] != null) { title=Request["title"].ToString(); };
        if (Request["content"] != null) { content = Request["content"].ToString(); };
        if (Request["sContent"] != null) { sContent = Request["sContent"].ToString(); };
        if (Request["urls"] != null) { urls = Request["urls"].ToString(); };
        if (Request["username"] != null) { username = Request["username"].ToString(); };
    }
}
