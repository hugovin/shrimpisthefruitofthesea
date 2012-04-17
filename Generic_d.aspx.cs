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
using System.Text.RegularExpressions;

public partial class Generic_d : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string GeneId = "";
    private int iGeneId = 0;
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
            CurrentChilPage = "generic_d.aspx";
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

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "generic_d.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }
        //--       
        GeneId = Request["id"];
        try
        {
            iGeneId = Convert.ToInt32(GeneId);
        }
        catch (Exception ex)
        {
            Response.Redirect("home.aspx");
        }

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
        //-

        
        LoadResoucesInfoPurchasing();

        ////this.URLRedirect();
        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        uc_SAPricing uc_sap = (uc_SAPricing)(Page.LoadControl("uc_SAPricing.ascx"));
        PlaceHolder_uc_SAPricing.Controls.Add(uc_sap);
        uc_NewsNinfo uc_news = (uc_NewsNinfo)(Page.LoadControl("uc_NewsNinfo.ascx"));
        PlaceHolder_uc_NewsNinfo.Controls.Add(uc_news);
    }
    private void URLRedirect()
    {
        pageType = "d";
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
    protected void LoadResoucesInfoPurchasing()
    {
        string pageNameBread = "";
        MainContentGeneric resourcesinfo = new MainContentGeneric();
        DataSet dsresourcesinfo = new DataSet();
        StringBuilder sb = new StringBuilder();

        Boolean ingreso = false;

        //---           
        dsresourcesinfo = resourcesinfo.Get_Site_Generic_D(iGeneId);
        sb.AppendLine("<div id=\"main-content\">");                          
        foreach (DataTable table in dsresourcesinfo.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (ingreso == false)
                {
                    ingreso = true;
                    sb.AppendLine("<div class=\"titleSupport\"><h1>" + row["GeneTitle"].ToString() + "</h1></div>");
                }
                pageNameBread = row["GeneTitle"].ToString();
                Main_MasterPage m = (Main_MasterPage)Page.Master;
                m.pageTitleBar = pageNameBread + " - " + m.pageTitleBar;
                pageName= row["GeneTitle"].ToString();
                sb.AppendLine("<div class=\"boxPurchasingInfo\">");
                sb.AppendLine("<div class=\"mainInfoResource wysiwig\">");
                        sb.AppendLine("<p><strong>" + row["GeneDTitle"].ToString() + "</strong></p><br />");
                        sb.AppendLine(row["GeneDContent"].ToString());
                    sb.AppendLine("</div>");

                    sb.AppendLine("<div class=\"clear\"></div>");
                    string path = "";
                    if (!Regex.IsMatch(row["GeneDFile"].ToString(), @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
                    {
                        path = Global.globalSiteImagesPath + "/";
                    }    
                if (row["GeneDFile"].ToString() != "")
                    {
                        sb.AppendLine("<div id=\"boxContImage\"  style=\"width:540px; height:104px;\">");
                        sb.AppendLine("<img id=\"images1\" style=\"height:104px;\" src=\"" + path + row["GeneDFile"].ToString() + "\" alt=\"" + row["GeneDTitle"].ToString() + "\" style=\"padding: 10px 0;\" />"); //onload=\"getDim(document.getElementById('boxContImage'),this)\" 
                        sb.AppendLine("</div>");
                    }

                    //if (row["GeneDFile"].ToString() != "")  sb.AppendLine("<div class=\"imgPurchaInfo\"><img src=\"" + Global.globalSiteImagesPath + "/" + row["GeneDFile"].ToString() + "\"></div>");

                    if (row["GeneDLinkTitle"].ToString() != "")
                    {
                        sb.AppendLine("<div class=\"mailCustomer\"><p>");
                            if (Convert.ToInt32(row["LinkTypeId"].ToString()) == 1) sb.AppendLine("<a href=\"" + row["GeneDLink"].ToString() + "\" target=\"_blank\">" + row["GeneDLinkTitle"].ToString() + "</a>");
                            else sb.AppendLine("<a href=\"" + row["GeneDLink"].ToString() + "\">" + row["GeneDLinkTitle"].ToString()+"</a>");
                        sb.AppendLine("</p></div>");
                    }
                sb.AppendLine("</div>");             
            }
        }        
        sb.AppendLine("</div>");
        PlaceHolder_Resources_Purchasing.Controls.Add(new LiteralControl(sb.ToString()));
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
            main._site_breadLink += "<li><a href=# onClick=\"return false;\">" + "About " + SiteConstants.SiteName + "</a></li>";
        }
        main._site_breadLink += "<li class=\"last\"><a href=# onClick=\"return false;\"><strong>" + pageNameBread + "</strong></a></li>";
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
