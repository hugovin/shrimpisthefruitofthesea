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

public partial class Generic_x : System.Web.UI.Page
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
            CurrentChilPage = "generic_x.aspx";
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "generic_x.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }
        //--
        GeneId = Request["LandingId"];
        //-

        //-
        typeMenu(GeneId);
        if (Convert.ToInt32(_gentype.ToString()) == 1)
        {
            Main_MasterPage MasterPage = (Main_MasterPage)Page.Master;
            MasterPage.LeftMenu_Finder = true;
            MasterPage.LeftMenu_Finder = true;
            MasterPage.LeftMenu_Subject = true;
            MasterPage.LeftMenu_Browse = true;
            MasterPage.LeftMenu_Resourcecenter = true;
            MasterPage.LeftMenu_Aboutus = false;
        }
        //-

        LoadGeneric();
        ////this.URLRedirect();
    }




    private void URLRedirect()
    {

        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            if (Request["LandingId"] != null)
            {
                Cleaner c = new Cleaner();
                pageName = c.cleanURL(pageName);
                Response.Redirect(pageName + "--" + Request["LandingId"].ToString()+".gxhtml");
            }
            else
            { //If this is null better to redirect to the homepage.
                Response.Redirect("home.aspx");
            }
        }
    }



    protected void LoadGeneric()
    {
        MainContentGeneric generic_X = new MainContentGeneric();
        DataSet dsgeneric_X = new DataSet();
        StringBuilder sb = new StringBuilder();
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        
        //---       
        dsgeneric_X = generic_X.Get_GenericX_By_Id(Convert.ToInt32(GeneId));
        foreach (DataTable table in dsgeneric_X.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<div class=\"mainAbout\">");                    
                    sb.AppendLine("<h1>" + row["GeneXTitle"].ToString() + "</h1>");
                    main._site_breadLink += "<li class=\"last\"><a href=\"#\" onClick=\"return false;\"><strong>" + row["GeneXTitle"].ToString() + "</strong></a></li>";
                    Main_MasterPage m = (Main_MasterPage)Page.Master;
                    m.pageTitleBar = row["GeneXTitle"].ToString() + " - " + m.pageTitleBar;
                    string path = "";
                    if (!Regex.IsMatch(row["GeneXImage"].ToString(), @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
                    {
                        path = Global.globalSiteImagesPath + "/";
                    }    
                if (row["GeneXImage"].ToString() != "")
                    sb.AppendLine("<img src=\"" + path + row["GeneXImage"].ToString() + "\" alt=\"" + row["GeneXTitle"].ToString() + "\" title=\"" + row["GeneXTitle"].ToString() + "\" style=\"float: right; margin: 0px 0px 5px 5px;\">");
                        pageName = row["GeneXTitle"].ToString();
                    sb.AppendLine(row["GeneXContent"].ToString());
                sb.AppendLine("</div>");                                                              
            }
        }

        PlaceHolder_Generic_X.Controls.Add(new LiteralControl(sb.ToString()));

        sb = null;
        generic_X = null;
        dsgeneric_X = null;
    }

    protected void typeMenu(string GeneId)
    {
        MainContentGeneric generics = new MainContentGeneric();
        DataSet dsgenerics = new DataSet();
        string cosa = Request.Url.ToString();
        dsgenerics = generics.getGeneTypeId(GeneId);
        if (dsgenerics != null)
        {
            foreach (DataTable table in dsgenerics.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    _gentype = row["GeneTypeId"].ToString();
                }
            }
        }
    }
}

