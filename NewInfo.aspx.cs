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

public partial class NewInfo : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string GeneId = "";
    private string GeneDefaId = "";
    private string strFolder = "images";

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
            CurrentChilPage = "newinfo.aspx";
        //--
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Response.Redirect("New_Info.html");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "newinfo.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }
        //--
        GeneId = Request["id"];
        GeneDefaId = Request["gdi"];
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main.pageTitleBar = "New Info - " + main.pageTitleBar;
        main._site_breadLink += "<li class=\"last\"><a href=\"#\" onClick=\"return false;\"><strong>New Info</strong></a></li>";
        LoadResoucesInfo();
        ////this.URLRedirect();   
        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        uc_WhatsNew uc_wn = (uc_WhatsNew)(Page.LoadControl("uc_WhatsNew.ascx"));
        PlaceHolder_uc_WhatsNew.Controls.Add(uc_wn);
    }

    protected void LoadResoucesInfo()
    {
        MainContentGeneric resourcesinfo = new MainContentGeneric();
        DataSet dsresourcesinfo = new DataSet();
        StringBuilder sb = new StringBuilder();
        String strContent;
        //---   
        int cont = 0;
        //dsresourcesinfo = resourcesinfo.getAllGenericResourcesInfo(Convert.ToInt32(GeneId), Convert.ToInt32(GeneDefaId));
        dsresourcesinfo = resourcesinfo.getAllGenericResourcesInfo(Convert.ToInt32(GeneId));
        sb.AppendLine("<div id=\"main-content\">");
        foreach (DataTable table in dsresourcesinfo.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (cont == 0)
                {
                    cont++;
                    sb.AppendLine("<div class=\"titleSupport\"><h1>" + row["GeneTitle"].ToString() + "</h1></div>");                   
                }

                strContent = row["GeneBContent"].ToString();
                string strContentB = strContent.Substring(0, 200);

                sb.AppendLine("<div class=\"boxResourceSupport\">");
                    sb.AppendLine("<div><h2>" + row["GeneBTitle"].ToString() + "</h2></div>");
                    sb.AppendLine("<div class=\"mainResourceSupport\">");
                        sb.AppendLine("<div style=\"line-height:18px; color:#666;\"> " + strContentB + "...</div>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<div class=\"bottonMore\">");
                        sb.AppendLine("<a href=\"Generic_b.aspx?id=" + row["GeneId"].ToString() + "&gdi=" + GeneDefaId + "\"><img src=\"" + strFolder  + "/plus_sign.gif\" width=\"8\" height=\"8\" /> Read More</a>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<div class=\"clear\"></div>");
                sb.AppendLine("</div>");
            }
        }
        sb.AppendLine("</div>");
        PlaceHolder_Resources_NewInformation.Controls.Add(new LiteralControl(sb.ToString()));
        sb = null;
        resourcesinfo = null;
        dsresourcesinfo = null;
    }

}
