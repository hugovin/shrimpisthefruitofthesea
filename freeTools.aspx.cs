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

public partial class freeTools : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string strTitleText = "";

    private int GeneDefaIdTrials = 6;// ID Generic Default correspondiente a los Trials.
    private int GeneDefaIdDemos = 7;// ID Generic Default correspondiente a los Demos

    private int titleResourceTypeIdTrials = 52;// ID Generic Default correspondiente al Resource Type Id For Trials.
    private int titleResourceTypeIdDemos = 53;// ID Generic Default correspondiente al Resource Type Id For Demos.

    string[] title = new string[3];
    string[] subTitle = new string[3];
    string[] content = new string[3];

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
            CurrentChilPage = "freeTools.aspx";
        //--      
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            if (Request["asm"] != null && Request["am"] != null)
            {
                Response.Redirect("Free_Tools." + Request["am"] + "html" + Request["asm"]);
            }
            else {
                Response.Redirect("Free_Tools.html");
            }
            
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "freeTools.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }
        //--        

        LoadTitSubConFreeTools();
        LoadInfoTrials();
        LoadInfoDemos();
        LoadInfoFood();

        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        uc_SAPricing uc_sap = (uc_SAPricing)(Page.LoadControl("uc_SAPricing.ascx"));
        PlaceHolder_uc_SAPricing.Controls.Add(uc_sap);
        uc_NewsNinfo uc_news = (uc_NewsNinfo)(Page.LoadControl("uc_NewsNinfo.ascx"));
        PlaceHolder_uc_NewsNinfo.Controls.Add(uc_news);
        //--Bread
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main.pageTitleBar = "Free Tools - " + main.pageTitleBar;
        main._site_breadLink += "<li><a href=resourcecenter.aspx?id=0>" + "Resource Center" + "</a></li>";
        main._site_breadLink += "<li class=\"last\"> <a href=#><strong>Free Tools</strong></a></li>";

        //--
        //-- Left Menu Active
        if (Request.QueryString["am"] != null && Request.QueryString["am"] != "")
        {
            try { SiteConstants.LeftMenuActive = Convert.ToInt32(Request.QueryString["am"].ToString()); }
            catch { SiteConstants.LeftMenuActive = 4; }
        }
        //--
        //this.URLRedirect();
    }

    protected void LoadTitSubConFreeTools()
    {
        SiteProduct classificationProducts = new SiteProduct();
        DataSet dsclassificationProducts = new DataSet();
        int cont = 0;

        dsclassificationProducts = classificationProducts.Get_Title_Content_Site_Free_Tools();
        foreach (DataTable table in dsclassificationProducts.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                title[cont] = row["FreeTitle1"].ToString();
                subTitle[cont] = row["FreeSubTitle1"].ToString();
                content[cont] = row["FreeContent1"].ToString();

                cont++;

                title[cont] = row["FreeTitle2"].ToString();
                subTitle[cont] = row["FreeSubTitle2"].ToString();
                content[cont] = row["FreeContent2"].ToString();

                cont++;

                title[cont] = row["FreeTitle3"].ToString();
                subTitle[cont] = row["FreeSubTitle3"].ToString();
                content[cont] = row["FreeContent3"].ToString();
            }
        }
    }

    protected void LoadInfoTrials()
    {

        SiteProduct classificationProducts = new SiteProduct();
        DataSet dsclassificationProducts = new DataSet();
        StringBuilder sb = new StringBuilder();
        int cont = 1;


        sb.AppendLine("<div class=\"mainAccount\"><h1>" + title[0].ToString() + "</h1></div>");
        sb.AppendLine("<div class=\"mainAccount1\">");
        sb.AppendLine("<h2>" + subTitle[0].ToString() + "</h2>");
        sb.AppendLine("<p>" + content[0].ToString() + "</p>");

        sb.AppendLine("<div class=\"FreeToolsTab\"><img src=\"" + Global.globalSiteImagesPath + "/top5Trials.jpg\" /></div>");
        sb.AppendLine("<div class=\"FreeTools\">");
        sb.AppendLine("<div class=\"FreeToolsTop\">");
        sb.AppendLine("<div class=\"FreeToolsButt\">");
        sb.AppendLine("<ul>");

        SiteProduct freeTools = new SiteProduct();
        DataSet dsfreeTools = new DataSet();
        dsfreeTools = freeTools.Get_All_Freetools_By_Id(Convert.ToInt32(GeneDefaIdTrials), Convert.ToInt32(titleResourceTypeIdTrials));
        foreach (DataTable table in dsfreeTools.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<li>");
                sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/list0" + cont + ".jpg\" width=\"19\" height=\"18\" />");
                //sb.AppendLine("<p><a href=\"#\">" + row["title"].ToString() + "</a></p>");
                //--Login Validate
                if (Session[SiteConstants.UserValidLogin] != null && (bool)Session[SiteConstants.UserValidLogin])
                {
                    sb.AppendLine("<p><a href=\"" + "#" + "\" onClick=\"downloadTrial(" + row["titleId"] + "); return false;\">" + row["Title"] + "</a>");
                }
                else
                {
                    sb.AppendLine("<a href=\"#htmlElement\" id=\"mb15\" class=\"mb\" title=\"\" rel=\"type:element\">" + row["Title"] + "</a>");
                }
                strTitleText = row["titleText"].ToString();
                string strTitleTextB = strTitleText.Substring(0, 55);
                sb.AppendLine("<p>" + strTitleTextB + "...</p>");

                sb.AppendLine("</li>");
                cont++;
            }
        }

        sb.AppendLine("</ul>");
        sb.AppendLine("<div class=\"toolMore\"><a href=\"trials.aspx\">+ view more</a></div>");
        sb.AppendLine("<div class=\"clear\"></div>");
        sb.AppendLine("</div>");
        sb.AppendLine("</div>");
        sb.AppendLine("</div>");
        sb.AppendLine("</div>");
        PlaceHolder_Trials.Controls.Add(new LiteralControl(sb.ToString()));

        sb = null;
        classificationProducts = null;
        dsclassificationProducts = null;
        freeTools = null;
        dsfreeTools = null;
    }

    protected void LoadInfoDemos()
    {

        SiteProduct classificationProducts = new SiteProduct();
        DataSet dsclassificationProducts = new DataSet();
        StringBuilder sb = new StringBuilder();
        int cont = 1;

        sb.AppendLine("<div class=\"mainAccount\"><h1>" + title[1].ToString() + "</h1></div>");
        sb.AppendLine("<div class=\"mainAccount1\">");
        sb.AppendLine("<h2>" + subTitle[1].ToString() + "</h2>");
        sb.AppendLine("<p>" + content[1].ToString() + "</p>");

        sb.AppendLine("<div class=\"FreeToolsTab\"><img src=\"" + Global.globalSiteImagesPath + "/top5Prev.jpg\" /></div>");
        sb.AppendLine("<div class=\"FreeTools\">");
        sb.AppendLine("<div class=\"FreeToolsTop\">");
        sb.AppendLine("<div class=\"FreeToolsButt\">");
        sb.AppendLine("<ul>");

        SiteProduct freeTools = new SiteProduct();
        DataSet dsfreeTools = new DataSet();
        dsfreeTools = freeTools.Get_All_Freetools_By_Id(Convert.ToInt32(GeneDefaIdDemos), Convert.ToInt32(titleResourceTypeIdDemos));
        foreach (DataTable table in dsfreeTools.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<li>");
                sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/list0" + cont + ".jpg\" width=\"19\" height=\"18\" />");
                //sb.AppendLine("<p><a href=\"#\">" + row["title"].ToString() + "</a></p>");
                if (Session[SiteConstants.UserValidLogin] != null && (bool)Session[SiteConstants.UserValidLogin])
                {
                    //sb.AppendLine("<p><a href=\"#hiddenVideo\" id=\"A1\" class=\"mb\" title=\"Demo\" alt=\"View Demo\" rel=\"type:element\" onClick=\"changeVideo(" + row["TitleId"] + ")\">" + row["Title"] + "</a></p>");
                    sb.AppendLine("<a href=\"getResourceVideo.aspx?pid=" + row["TitleId"].ToString() + "\" rel=\"width:600,height:436,ajax:true\" id=\"A2\" class=\"mb\" title=\"See Video\">" + row["Title"] + "</a>");
                }
                else
                {
                    sb.AppendLine("<p><a href=\"#htmlElement\" id=\"mb15\" class=\"mb\" title=\"\" rel=\"type:element\">" + row["Title"] + "</a></p>");
                }


                strTitleText = row["titleText"].ToString();
                string strTitleTextB = strTitleText.Substring(0, 55);
                sb.AppendLine("<p>" + strTitleTextB + "...</p>");

                sb.AppendLine("</li>");
                cont++;
            }
        }

        sb.AppendLine("</ul>");
        sb.AppendLine("<div class=\"toolMore\"><a href=\"preview.aspx\">+ view more</a></div>");
        sb.AppendLine("<div class=\"clear\"></div>");
        sb.AppendLine("</div>");
        sb.AppendLine("</div>");
        sb.AppendLine("</div>");
        sb.AppendLine("</div>");
        PlaceHolder_Trials.Controls.Add(new LiteralControl(sb.ToString()));

        sb = null;
        classificationProducts = null;
        dsclassificationProducts = null;
        freeTools = null;
        dsfreeTools = null;
    }

    protected void LoadInfoFood()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<div class=\"mainAccount\">");
        sb.AppendLine("<h1>" + title[2].ToString() + "</h1>");
        sb.AppendLine("<div class=\"mainAccount1\">");
        sb.AppendLine("<p>" + subTitle[2].ToString() + "</p>");
        sb.AppendLine("<p>" + content[2].ToString() + "</p>");
        sb.AppendLine("</div>");
        sb.AppendLine("</div>");

        PlaceHolder_Foot.Controls.Add(new LiteralControl(sb.ToString()));

        sb = null;
    }
}