using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using uc_Left_Menu;

public partial class Generic : System.Web.UI.MasterPage
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
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
            CurrentChilPage = "home.aspx";
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Left_Menu uc_left_menu = (Left_Menu)(Page.LoadControl("Left_Menu.ascx"));
        uc_left_menu._Finder = false;
        uc_left_menu._Subject = false;
        uc_left_menu._Browse = false;
        uc_left_menu._Resourcecenter = false;
        uc_left_menu._Aboutus = true;
        LeftMenuPlaceHolder.Controls.Add(uc_left_menu);
        LoadHeadMenu();
        LoadAdds();
        LoadSiteContact();
        LoadResourceCenter();
        LoadAboutUs();

    }
    protected void LoadHeadMenu()
    {
        Header head = new Header();
        DataSet dsContentGroup = new DataSet();
        StringBuilder sb = new StringBuilder();

        dsContentGroup = head.getAllContentGroups();
        sb.AppendLine("<div id=\"topRight\" >");
        sb.AppendLine("<ul>");

        foreach (DataTable table in dsContentGroup.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<li><a href=\"" + CurrentChilPage + "?ci=" + row["ContId"].ToString() + "\" class=\"" + (row["ContId"].ToString() == ContentId ? "current" : "") + "\">" + row["ContTitle"].ToString() + "</a></li>");
                //sb.AppendLine("" + row["cat"].ToString() + "");
            }
        }
        sb.AppendLine("<li><a href=\"#\" class=\"request\">Request a Quote</a></li>");
        sb.AppendLine("</ul>");
        sb.AppendLine("</div>");
        PlaceHolder_topRight.Controls.Add(new LiteralControl(sb.ToString()));
        head = null;
    }

    protected void LoadAdds()
    {
        Header head = new Header();
        DataSet dsAdds = new DataSet();
        StringBuilder sb = new StringBuilder();
        dsAdds = head.getAllAdds();
        int cont = 0;
        sb.AppendLine("<div id=\"bread\">");
        foreach (DataTable table in dsAdds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (cont >= 1) sb.AppendLine("<div class=\"breadDivider\"></div>");
                sb.AppendLine("<div class=\"breadAdd\"><a href=\"Adds.aspx?id=" + row["AddsId"].ToString() + "\"><img src=\"" + Global.globalSiteImagesPath + "/" + row["AddsImage"].ToString() + "\" border=0 /></a></div>");
                cont++;


            }
        }
        sb.AppendLine("</div>");
        PlaceHolder_bread.Controls.Add(new LiteralControl(sb.ToString()));
        sb = null;
    }

    protected void LoadSiteContact()
    {
        Footer footer = new Footer();
        DataSet dsContact = new DataSet();
        StringBuilder sb = new StringBuilder();
        dsContact = footer.GetAllSiteContact();
        foreach (DataTable table in dsContact.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<img src='" + Global.globalSiteImagesPath + "/" + row["SiteContImage"].ToString() + "' width='78' height='36' />");
                sb.AppendLine("<h3>" + row["SiteContTitle"].ToString() + "</h3>");
                sb.AppendLine("<p>" + row["SiteContAddress"].ToString() + "</p>");
                sb.AppendLine("<h3>Email</h3>");
                sb.AppendLine("<p><span>Customer Service:</span> <a href='mailto:" + row["SiteContEmailCus"].ToString() + "'>" + row["SiteContEmailCus"].ToString() + "</a>");
                sb.AppendLine("<br />");
                sb.AppendLine("<span>Sales:</span> <a href='mailto:" + row["SiteContEmailSal"].ToString() + "'>" + row["SiteContEmailSal"].ToString() + "</a>");
                sb.AppendLine("</p>");
                break;
            }
        }

        PlaceHolder_SiteContact.Controls.Add(new LiteralControl(sb.ToString()));
        sb = null;
    }

    protected void LoadResourceCenter()
    {
        Footer footer = new Footer();
        DataSet dsResource = new DataSet();
        StringBuilder sb = new StringBuilder();
        dsResource = footer.Get_LeftMenu_All_ResourceCenter();
        foreach (DataTable table in dsResource.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<li><a href=\"" + row["geneid"].ToString() + "\">" + row["genetitle"].ToString() + "</a>");
                sb.AppendLine("</li>");
            }
        }

        PlaceHolder_ResourceCenter.Controls.Add(new LiteralControl(sb.ToString()));
        dsResource = null;
        sb = null;
    }
    protected void LoadAboutUs()
    {
        Footer footer = new Footer();
        DataSet dsAboutUs = new DataSet();
        StringBuilder sb = new StringBuilder();
        dsAboutUs = footer.Get_LeftMenu_All_AboutUs();
        foreach (DataTable table in dsAboutUs.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<li><a href=\"AboutUs.aspx?id=" + row["geneid"].ToString() + "\">" + row["genetitle"].ToString() + "</a>");
                sb.AppendLine("</li>");
            }
        }

        PlaceHolder_AboutUs.Controls.Add(new LiteralControl(sb.ToString()));
        dsAboutUs = null;
        sb = null;
    }
}

