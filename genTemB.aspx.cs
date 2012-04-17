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
using uc_Left_Menu;
using System.Text.RegularExpressions;

public partial class genTemB : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string GeneBId = "";
    private string GeneDefaId = "";

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
            CurrentChilPage = "genTemB.aspx";
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["idB"] != null && Request["idB"].ToString().Trim() != "")
        {
            GeneBId = Request["idB"].ToString().Trim();
            try { Convert.ToInt32(GeneBId); }
            catch (Exception ex) { GeneBId = "0"; }
        }
        else { 
            GeneBId = "0"; 
        }

        Session["CurrentChilPage"] = "genTemB.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }

        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main._site_breadLink += "<li><a href=resourcecenter.aspx?id=0>" + "Resource Center" + "</a></li>";
        //main._site_breadLink += "<li <a href=generic_b.aspx?id=26><strong>News & Info</strong></a></li>";

        LoadGeneric(main);

        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        uc_SAPricing uc_sap = (uc_SAPricing)(Page.LoadControl("uc_SAPricing.ascx"));
        PlaceHolder_uc_SAPricing.Controls.Add(uc_sap);
        uc_NewsNinfo uc_news = (uc_NewsNinfo)(Page.LoadControl("uc_NewsNinfo.ascx"));
        PlaceHolder_uc_NewsNinfo.Controls.Add(uc_news);
    }

    protected void LoadGeneric(Main_MasterPage main)
    {
        MainContentGeneric generic_B = new MainContentGeneric();
        DataSet dsgeneric_B = new DataSet();
        StringBuilder sb = new StringBuilder();        

        //---       
        dsgeneric_B = generic_B.Get_Generic_B_By_GenBId(Convert.ToInt32(GeneBId));
        sb.AppendLine("<div id=\"main-content\">");
        string path = "";
        foreach (DataTable table in dsgeneric_B.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                main._site_breadLink += "<li class=\"last\"> <a href=#><strong>" + row["GeneBTitle"].ToString() + "</strong></a></li>";
                Main_MasterPage m = (Main_MasterPage)Page.Master;
                m.pageTitleBar = row["GeneBTitle"].ToString() + " - " + m.pageTitleBar;
                if (!Regex.IsMatch(row["GeneBFile"].ToString(), @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:pdf|PDF|Pdf)$"))
                {
                    path = "PDFs/";
                }  
                sb.AppendLine("<div class=\"mainInfoResource\">");                        
                sb.AppendLine("<div class=\"titleSupport\"><h1>" + row["GeneBTitle"].ToString() + "</h1></div>");

                if (row["GeneBFile"].ToString() != "")
                {
                    sb.AppendLine("<div class=\"linkPrinter\"><p><a href=\"" + path + row["GeneBFile"].ToString() + "\" target=\"_blank\"><img src=\"" + Global.globalSiteImagesPath + "/iconAcrobat.gif\" alt=\"download\" /> Download</a></p></div>");
                }
                sb.AppendLine(" <div class=\"mainResourceSupport\">");
                sb.AppendLine("<div style=\"line-height:18px; color:#666;\">" + row["GeneBContent"].ToString() + "</div>");
                sb.AppendLine("</div>"); 
                sb.AppendLine("</div>");                    
                sb.AppendLine("<div class=\"bottonMore\">");
                sb.AppendLine("<div class=\"mailCustomer\"><p><a href=\"javascript:history.back(1)\"> return </a></p></div>");
                sb.AppendLine("</div>");           
            }
        }
        sb.AppendLine("</div>");
        PlaceHolder_GenTem_B.Controls.Add(new LiteralControl(sb.ToString()));
        sb = null;
        dsgeneric_B = null;
        generic_B = null;
    }
}
