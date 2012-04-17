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
using System.Text.RegularExpressions;

public partial class Generic_a : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string GeneId = "";
    private int iGeneId = 0;
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
            CurrentChilPage = "generic_a.aspx";
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
                    else {
                        Response.Redirect(pageName + "-r-" + pageType + "-" + pageId + ".ghtml");
                    }
                    
                }
                else
                {
                    if (Request["am"] != null && Request["asm"] != null)
                    {
                        Response.Redirect(pageName + "--" + pageType + "-" + pageId + ".g" + Request["am"].ToString() + "_" + Request["asm"].ToString() + "html");
                    }
                    else {
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
        Session["CurrentChilPage"] = "generic_a.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }

        //--
        GeneId = Request["id"];        
        GeneDefaId = Request["gdi"];
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
        /*if (Convert.ToInt32(_gentype.ToString()) == 2)
        {
            Main_MasterPage MasterPage = (Main_MasterPage)Page.Master;
            MasterPage.LeftMenu_Finder = false;
            MasterPage.LeftMenu_Finder = false;
            MasterPage.LeftMenu_Subject = false;
            MasterPage.LeftMenu_Browse = false;
            MasterPage.LeftMenu_Resourcecenter = false;
            MasterPage.LeftMenu_Aboutus = true;
            SiteConstants.LeftMenuActive = 0; 
        }*/
        //-
        LoadGeneric();
        //this.URLRedirect();
    }
    protected void LoadGeneric()
    {
        pageType = "a";
        MainContentGeneric generic_A = new MainContentGeneric();
        DataSet dsgeneric_A = new DataSet();
        StringBuilder sb = new StringBuilder();
        //--Bread
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        if (Request["TypeGen"] != null)
        {
            main._site_breadLink += "<li><a href=resourcecenter.aspx?id=0 onClick=\"return true;\">" + "Resource Center" + "</a></li>";
        }
        else {
            main._site_breadLink += "<li><a href=\"#\" onClick=\"return false;\">" + "About " + SiteConstants.SiteName + "</a></li>";
        }
        //--
        dsgeneric_A = generic_A.getAllGenericByTypeId(iGeneId);
        foreach (DataTable table in dsgeneric_A.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                string path = "";
                if(!Regex.IsMatch(row["GeneAImage"].ToString(), @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
                {
                    path = Global.globalSiteImagesPath + "/";
                }
                if (row["GeneAImage"].ToString() != ""){
                    sb.AppendLine("<div id=\"boxContImage\"  style=\"width:200px; height:200px; float:right; margin:10px;\">");
                    sb.AppendLine("<img id=\"images1\" style=\"width: 200px; height:200px;\" src=\"" + path + row["GeneAImage"].ToString() + "\" alt=\"" + row["GeneATitle"].ToString() + "\" />"); //onload=\"(document.getElementById('boxContImage'),this)\" 
                    sb.AppendLine("</div>");
                }

                main._site_breadLink += "<li> <a href=# onClick=\"return false;\"><strong>" + row["GeneATitle"].ToString() + "</strong></a></li>";
                pageName = row["GeneATitle"].ToString();
                main.pageTitleBar = pageName + " - " + main.pageTitleBar;
                    sb.AppendLine("<div class=\"mainAbout\">");
                    sb.AppendLine("<h1>" + row["GeneATitle"].ToString() + "</h1>");                        
                    sb.AppendLine(row["GeneAContent"].ToString());
                sb.AppendLine("</div>");
            
                //--
                if (row["GeneALink"].ToString() != "" && row["GeneALink2"].ToString() != "")
                {
                    sb.AppendLine("<div>");
                        
                        sb.AppendLine("<div class=\"btnText\">");
                            if (Convert.ToInt32(row["LinkTypeId"].ToString()) == 1) sb.AppendLine("<a href=\"" + row["GeneALink"].ToString() + "\" target=\"_blank\">");
                            else sb.AppendLine("<a href=\"" + row["GeneALink"].ToString() + "\">");
                                sb.AppendLine("<div class=\"btnTextLeft\"></div>");
                                sb.AppendLine("<div class=\"btnTextMain\"><p>" + row["GeneALinkTitle"].ToString() + "</p></div>");
                                sb.AppendLine(" <div class=\"btnTextRight\"></div>");
                                sb.AppendLine("<div class=\"clear\"></div>");
                            sb.AppendLine("</a>");
                        sb.AppendLine("</div>");

                        sb.AppendLine("<div class=\"btnText\">");
                            if (Convert.ToInt32(row["LinkTypeId2"].ToString()) == 1) sb.AppendLine("<a href=\"" + row["GeneALink2"].ToString() + "\" target=\"_blank\">");
                            else sb.AppendLine("<a href=\"" + row["GeneALink2"].ToString() + "\">");                                    
                                sb.AppendLine("<div class=\"btnTextLeft\"></div>");
                                sb.AppendLine("<div class=\"btnTextMain\"><p>" + row["GeneALink2Title"].ToString() + "</p></div>");
                                sb.AppendLine(" <div class=\"btnTextRight\"></div>");
                                sb.AppendLine("<div class=\"clear\"></div>");
                            sb.AppendLine("</a>");
                        sb.AppendLine("</div>");

                    sb.AppendLine("</div>");
                    
                }
                else if (row["GeneALink"].ToString() != "")
                {
                    sb.AppendLine("<div>");

                        sb.AppendLine("<div class=\"btnText\">");
                            if (Convert.ToInt32(row["LinkTypeId"].ToString()) == 1) sb.AppendLine("<a href=\"" + row["GeneALink"].ToString() + "\" target=\"_blank\">");
                            else sb.AppendLine("<a href=\"" + row["GeneALink"].ToString() + "\">");                          
                                sb.AppendLine("<div class=\"btnTextLeft\"></div>");
                                sb.AppendLine("<div class=\"btnTextMain\"><p>" + row["GeneALinkTitle"].ToString() + "</p></div>");
                                sb.AppendLine(" <div class=\"btnTextRight\"></div>");
                                sb.AppendLine("<div class=\"clear\"></div>");
                            sb.AppendLine("</a>");
                        sb.AppendLine("</div>");
                        
                    sb.AppendLine("</div>");
                }
                else if (row["GeneALink2"].ToString() != "")
                {
                    sb.AppendLine("<a href=\"" + row["GeneALink2"].ToString() + "\">");
                        if (Convert.ToInt32(row["LinkTypeId2"].ToString()) == 1) sb.AppendLine("<a href=\"" + row["GeneALink2"].ToString() + "\" target=\"_blank\">");
                        else sb.AppendLine("<a href=\"" + row["GeneALink2"].ToString() + "\">");  
                            sb.AppendLine("<div class=\"btnTextLeft\"></div>");
                            sb.AppendLine("<div class=\"btnTextMain\"><p>" + row["GeneALink2Title"].ToString() + "</p></div>");
                            sb.AppendLine(" <div class=\"btnTextRight\"></div>");
                            sb.AppendLine("<div class=\"clear\"></div>");
                        sb.AppendLine("</div>");
                    sb.AppendLine("</a>");
                }              
                //--     
                                                                 
            }
        }
        
        PlaceHolder_Generic_A.Controls.Add(new LiteralControl(sb.ToString()));

        sb = null;
        dsgeneric_A = null;
        generic_A = null;
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

