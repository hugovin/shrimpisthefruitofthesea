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

public partial class Generic_c : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string GeneId = "";
    private string _gentype;
    //-- Variable for the URL Rewrite
    private string pageName = "";
    private string pageType = "";
    private string pageId = "";
    //--

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
            CurrentChilPage = "generic_c.aspx";
        //--
        //-- Left Menu Active
        if (Request.QueryString["am"] != null && Request.QueryString["am"] != "")
        {
            try { SiteConstants.LeftMenuActive = Convert.ToInt32(Request.QueryString["am"].ToString()); }
            catch { SiteConstants.LeftMenuActive = 4; }
        }
        else
            SiteConstants.LeftMenuActive = 4;
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            if (Request["id"] != null)
            {
                pageId = Request["id"].ToString();
                Cleaner c = new Cleaner();
                pageName = c.cleanURL(pageName);
                pageName = pageName.Replace(":", "");
                if (Request["TypeGen"] != null)
                {
                    if (Request["am"] != null && Request["asm"] != null)
                    {
                        Response.Redirect(pageName + "-r-" + pageType + "-" + pageId + ".g" + Request["am"].ToString() + "_" + Request["asm"].ToString() + "html");
                    }
                    else
                    {
                        Response.Redirect(pageName + "-r-" + pageType + "-" + pageId + ".ghtml");
                    }
                }
                else
                {
                    if (Request["am"] != null && Request["asm"] != null)
                    {
                        Response.Redirect(pageName + "--" + pageType + "-" + pageId + ".g" + Request["am"].ToString() + "_" + Request["asm"].ToString() + "html");
                    }
                    else
                    {
                        Response.Redirect(pageName + "--" + pageType + "-" + pageId + ".ghtml");
                    }
                }
                Response.Redirect(pageName + "--" + pageType + "-" + pageId + ".ghtml");
            }
            else
            { //If this is null better to redirect to the homepage.
                Response.Redirect("home.aspx");
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "generic_c.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }
        //--       
        GeneId = Request["id"];
        LoadResoucesInfoFaqs();
        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        uc_SAPricing uc_sap = (uc_SAPricing)(Page.LoadControl("uc_SAPricing.ascx"));
        PlaceHolder_uc_SAPricing.Controls.Add(uc_sap);
        uc_NewsNinfo uc_news = (uc_NewsNinfo)(Page.LoadControl("uc_NewsNinfo.ascx"));
        PlaceHolder_uc_NewsNinfo.Controls.Add(uc_news);
    }

    protected void LoadResoucesInfoFaqs()
    {
        string pageNameBread = "";
        pageType = "c";
        MainContentGeneric resourcesinfo = new MainContentGeneric();
        DataSet dsresourcesinfo = new DataSet();
        StringBuilder sb = new StringBuilder();

        Boolean ingreso = false;
        //---           
        dsresourcesinfo = resourcesinfo.Get_Site_Generic_C(Convert.ToInt32(GeneId));
       
        foreach (DataTable table in dsresourcesinfo.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (ingreso == false)
                {
                    sb.AppendLine("<div id=\"main-content-FAQ\">");
                        sb.AppendLine("<div class=\"titleSupport\"><h1>" + row["GeneTitle"].ToString() + "</h1></div>");
                        pageNameBread = row["GeneTitle"].ToString();
                        Main_MasterPage m = (Main_MasterPage)Page.Master;
                        m.pageTitleBar = pageNameBread + " - " + m.pageTitleBar;
                        pageName = row["GeneTitle"].ToString();
                        sb.AppendLine("<div class=\"boxResourceSupport\">");
                        sb.AppendLine("<div class=\"mainFAQ\">");
                            sb.AppendLine("<div class=\"accordion5\">");
                                sb.AppendLine("<dl class=\"accordion5\" id=\"slider5\">");
                                    ingreso = true;                                    
                }
                                    sb.AppendLine("<dt>Q. <span>" + row["GeneCTitle"].ToString() + "</span></dt>");
                                    sb.AppendLine("<dd>");
                                        sb.AppendLine("<div class=\"answerOpt\"><p>A.</p></div>");
                                        sb.AppendLine("<div class=\"answerInfo wysiwig\">" + row["GeneCContent"].ToString() + "</div>");
                                        sb.AppendLine("<div class=\"clear\"></div>");
                                    sb.AppendLine("</dd>");
            }
        }
                                sb.AppendLine("</dl>");

                                sb.AppendLine("<script type=\"text/javascript\">");
                                    sb.AppendLine("var slider5=new accordion.slider(\"slider5\");");
                                    sb.AppendLine("slider5.init(\"slider5\",5,\"open\");");
                                sb.AppendLine("</script>");

                            sb.AppendLine("</div>");
                        sb.AppendLine("</div>");
                    sb.AppendLine("</div>");
                sb.AppendLine("</div>");

        PlaceHolder_Resources_Faqs.Controls.Add(new LiteralControl(sb.ToString()));
        sb = null;
        resourcesinfo = null;
        dsresourcesinfo = null;
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        if (Request["TypeGen"] != null)
        {
            main._site_breadLink += "<li><a href=resourcecenter.aspx?id=0>" + "Resource Center" + "</a></li>";
        }
        else
        {
            main._site_breadLink += "<li><a href=# onClick=\"return false;\">" + "About " + SiteConstants.SiteName.ToString() + "</a></li>";
        }
        main._site_breadLink += "<li class=\"last\"> <a href=# onClick=\"return false;\"><strong>" + pageNameBread + "</strong></a></li>";   
    }
}
