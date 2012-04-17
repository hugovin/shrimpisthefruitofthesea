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

public partial class Generic_e : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string GeneId = "";
    private string GeneDefaId = "";
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
            CurrentChilPage = "generic_e.aspx";
        //--
        //-- Left Menu Active
        if (Request.QueryString["am"] != null && Request.QueryString["am"] != "")
        {
            try { SiteConstants.LeftMenuActive = Convert.ToInt32(Request.QueryString["am"].ToString()); }
            catch { SiteConstants.LeftMenuActive = 4; }
        }
        else
            SiteConstants.LeftMenuActive = 4;

        //--
    }
    private void URLRedirect()
    {
        pageType = "e";
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
        Session["CurrentChilPage"] = "generic_e.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }
        //--
        GeneId = Request["id"];
        GeneDefaId = Request["gdi"];

        //-
        //typeMenu(GeneId);
        //if (Convert.ToInt32(_gentype.ToString()) == 1)
        //{
        //    Main_MasterPage MasterPage = (Main_MasterPage)Page.Master;
        //    MasterPage.LeftMenu_Finder = true;
        //    MasterPage.LeftMenu_Finder = true;
        //    MasterPage.LeftMenu_Subject = true;
        //    MasterPage.LeftMenu_Browse = true;
        //    MasterPage.LeftMenu_Resourcecenter = true;
        //    MasterPage.LeftMenu_Aboutus = false;
        //}
        //if (Convert.ToInt32(_gentype.ToString()) == 2)
        //{
        //    Main_MasterPage MasterPage = (Main_MasterPage)Page.Master;
        //    MasterPage.LeftMenu_Finder = false;
        //    MasterPage.LeftMenu_Finder = false;
        //    MasterPage.LeftMenu_Subject = false;
        //    MasterPage.LeftMenu_Browse = false;
        //    MasterPage.LeftMenu_Resourcecenter = false;
        //    MasterPage.LeftMenu_Aboutus = true;
        //    SiteConstants.LeftMenuActive = 0;
        //}
        //-
        LoadGenericE();

        //this.URLRedirect();
        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        uc_SAPricing uc_sap = (uc_SAPricing)(Page.LoadControl("uc_SAPricing.ascx"));
        PlaceHolder_uc_SAPricing.Controls.Add(uc_sap);
        uc_NewsNinfo uc_news = (uc_NewsNinfo)(Page.LoadControl("uc_NewsNinfo.ascx"));
        PlaceHolder_uc_NewsNinfo.Controls.Add(uc_news);
    }

    protected void LoadGenericE()
    {
        string pageNameBread = "";
        MainContentGeneric generic_E = new MainContentGeneric();
        DataSet dsgeneric_E= new DataSet();
        StringBuilder sb = new StringBuilder();

        Boolean ingreso = false;
        //---       
        //dsgeneric_A = generic_A.getAllGenericEByTypeId(Convert.ToInt32(GeneId), Convert.ToInt32(GeneDefaId));
        dsgeneric_E = generic_E.Get_Site_Generic_E(Convert.ToInt32(GeneId));

        sb.AppendLine("<div id=\"main-content\">");       
        foreach (DataTable table in dsgeneric_E.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (ingreso == false)
                {
                    ingreso = true;
                    sb.AppendLine("<div class=\"titleSupport\"><h1>" + row["GeneTitle"].ToString() + "</h1></div>");
                    pageNameBread = row["GeneTitle"].ToString();
                    Main_MasterPage m = (Main_MasterPage)Page.Master;
                    m.pageTitleBar = pageNameBread + " - " + m.pageTitleBar;
                }
                pageName = row["GeneTitle"].ToString();
                sb.AppendLine("<div class=\"boxPurchasingInfo\">");
                    sb.AppendLine("<div class=\"mainInfoResource wysiwig\">");
                        sb.AppendLine("<p><strong>" + row["GeneETitle"].ToString() + "</strong></p><br />");
                        sb.AppendLine(row["GeneELocation"].ToString());
                        sb.AppendLine(row["GeneEContent"].ToString()+"</br>");

                        //--
                        if (row["GeneELink"].ToString() != "")
                        {                            
                            sb.AppendLine("<br /><div class=\"bottonMore\">");
                            if (Convert.ToInt32(row["LinkType"].ToString()) == 1) sb.AppendLine("<a href=\"" + row["GeneELink"].ToString() + "\" target=\"_blank\">" + row["GeneELinkTitle"].ToString() + "</a>");
                            else sb.AppendLine("<a href=\"" + row["GeneELink"].ToString() + "\">" + row["GeneELinkTitle"].ToString() + "</a>");                                 
                            sb.AppendLine("</div>");
                        }               
                        //--   
                    sb.AppendLine("</div>");
                sb.AppendLine("</div>");                                
            }
        }

        sb.AppendLine("</div>");
        PlaceHolder_Generic_E.Controls.Add(new LiteralControl(sb.ToString()));

        sb = null;
        dsgeneric_E = null;
        generic_E = null;
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

    //protected void typeMenu(string GeneId)
    //{
    //    MainContentGeneric generics = new MainContentGeneric();
    //    DataSet dsgenerics = new DataSet();

    //    dsgenerics = generics.getGeneTypeId(GeneId);
    //    foreach (DataTable table in dsgenerics.Tables)
    //    {
    //        foreach (DataRow row in table.Rows)
    //        {
    //            _gentype = row["GeneTypeId"].ToString();
    //        }
    //    }
    //}

}
