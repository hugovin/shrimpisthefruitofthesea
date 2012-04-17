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

public partial class Generic_b : System.Web.UI.Page
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
            CurrentChilPage = "generic_b.aspx";
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
                Response.Redirect(pageName + "--" + pageType + "-" + pageId+".ghtml");
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
        Session["CurrentChilPage"] = "generic_b.aspx";
       
        pageType = "b";
        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }
        //--  
        if (Request["id"] != null && Request["id"].ToString().Trim() != "")
        {
            GeneId = Request["id"].ToString().Trim();
            try { Convert.ToInt32(GeneId); }
            catch (Exception ex) { GeneId = "0"; }
        }
        else{GeneId = "0";}

        if (Request["gid"] != null && Request["gid"].ToString().Trim() != "")
        {
            GeneDefaId = Request["gdi"];
            try { Convert.ToInt32(GeneDefaId); }
            catch (Exception ex) { GeneDefaId = "0"; }
        }
        else
        {
            GeneDefaId = "0";
        }
        LoadGeneric();
        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        uc_SAPricing uc_sap = (uc_SAPricing)(Page.LoadControl("uc_SAPricing.ascx"));
        PlaceHolder_uc_SAPricing.Controls.Add(uc_sap);
        uc_NewsNinfo uc_news = (uc_NewsNinfo)(Page.LoadControl("uc_NewsNinfo.ascx"));
        PlaceHolder_uc_NewsNinfo.Controls.Add(uc_news);
    }

    protected void LoadGeneric()
    {
        string pageNameBread = "";
        MainContentGeneric generic_B = new MainContentGeneric();
        Addins curtText = new Addins();
        DataSet dsgeneric_B = new DataSet();
        StringBuilder sb = new StringBuilder();        

        Boolean ingreso = false;

        //---       
        dsgeneric_B = generic_B.Get_Site_Generic_B(Convert.ToInt32(GeneId));
        sb.AppendLine("<div id=\"main-content\">");
        string path = "";
        foreach (DataTable table in dsgeneric_B.Tables)
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
                m.pageTitleBar = pageNameBread+" - " + m.pageTitleBar;
                pageName = row["GeneTitle"].ToString();
                sb.AppendLine(" <div class=\"boxResourceSupport\">");
                sb.AppendLine("<div><h2>" + row["GeneBTitle"].ToString() + "</h2></div>");
                if (!Regex.IsMatch(row["GeneBFile"].ToString(), @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:pdf|PDF|Pdf)$"))
                {
                    path = "PDFs/";
                }
                else
                {
                    path = "";
                }
                if (row["GeneBFile"].ToString() != "")
                {
                    sb.AppendLine("<div class=\"linkPrinter\"><p><a href=\"" + path + row["GeneBFile"].ToString() + "\" target=\"_blank\"><img src=\"" + Global.globalSiteImagesPath + "/iconAcrobat.gif\" alt=\"download\" /> Download</a></p></div>");
                    //**
                }
                sb.AppendLine(" <div class=\"mainResourceSupport\">");
                //* Error in content in the database.
                sb.AppendLine("<div style=\"line-height:18px; color:#666;\"><p id='content'>" + getFirstParagraph(row["GeneBContent"].ToString()) + "</p></div>");   //curtText.cutDescription(RemoveHTMLTags(row["GeneBContent"].ToString()), 300)
                //*
                sb.AppendLine("</div>");
                //**
                sb.AppendLine("<div class=\"bottonMore\">");
                sb.AppendLine("<a href=\"genTemB.aspx?idB=" + row["GeneBId"].ToString() + "\"><img src=\"" + Global.globalSiteImagesPath + "/plus_sign.gif\" width=\"8\" height=\"8\" /> read more</a>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
            } 
        }

        sb.AppendLine("</div>");
        PlaceHolder_Generic_B.Controls.Add(new LiteralControl(sb.ToString()));

        sb = null;
        dsgeneric_B = null;
        generic_B = null;
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        if (Request["TypeGen"] != null)
        {
            main._site_breadLink += "<li><a href=\"resourcecenter.aspx?id=0\" onClick=\"return true;\">" + "Resource Center" + "</a></li>";
        }
        else
        {
            main._site_breadLink += "<li><a href=# onClick=\"return false;\">" + "About " + SiteConstants.SiteName.ToString() + "</a></li>";
        }
        main._site_breadLink += "<li class=\"last\"> <a href=# onClick=\"return false;\"><strong>" + pageNameBread + "</strong></a></li>";
    }
    private string RemoveHTMLTags(string source)
    {
        string expn = "<.*?>";
        return Regex.Replace(source, expn, string.Empty);
    }

    private string getFirstParagraph(string text)
    {
        string myText = text;
        Regex regex = new Regex (@".*<p>.*</p>.*");
        MatchCollection myMatch = regex.Matches(myText);
        if (myMatch.Count > 0)
        {
            myText = Convert.ToString(myMatch[0]);
        }
        else
        {
            myText = "";
        }
        return myText;//Convert.ToString(myMatch[0]);
    }
}
