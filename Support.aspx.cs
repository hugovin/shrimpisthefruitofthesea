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

public partial class Support : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";

    private int GeneIdFaqs = 0; //ID Generic correspondiente al Faqs.
    private int GeneIdPurchasing = 0;// ID Generic correspondiente al Purchasing Information.

    private string nameSection = "";

    private int GeneDefaIdFaqs = 16;// ID Generic Default correspondiente al Faqs.
    private int GeneDefaIdPurchasing = 17;// ID Generic Default correspondiente al Purchasing Information.

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
            CurrentChilPage = "support.aspx";
        //--
        //-- Left Menu Active
        if (Request.QueryString["TypeGen"] != null && Request.QueryString["TypeGen"] != "")
        {
            try { SiteConstants.LeftMenuActive = Convert.ToInt32(Request.QueryString["id"].ToString()); }
            catch { SiteConstants.LeftMenuActive = 3; }
        }
        else
            SiteConstants.LeftMenuActive = 3;

        //--
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Response.Redirect("Support.html");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();

        //////this.URLRedirect();
        Main_MasterPage m = (Main_MasterPage)Page.Master;
        m.pageTitleBar = "Support - " + m.pageTitleBar;
        Session["CurrentChilPage"] = "support.aspx";
        //-- Left Menu Active
        if (Request.QueryString["am"] != null && Request.QueryString["am"] != "")
        {
            try { SiteConstants.LeftMenuActive = Convert.ToInt32(Request.QueryString["am"].ToString()); }
            catch { SiteConstants.LeftMenuActive = 4; }
        }
        //--
        ////this.URLRedirect();
        GeneIdFaqs = Get_Id_Generic(Convert.ToInt32(GeneDefaIdFaqs));
        GeneIdPurchasing = Get_Id_Generic(Convert.ToInt32(GeneDefaIdPurchasing));

        LoadResoucesInfoPurchasing();
        LoadResoucesInfoFaqs();

        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        uc_WhatsNew uc_wn = (uc_WhatsNew)(Page.LoadControl("uc_WhatsNew.ascx"));
        PlaceHolder_uc_WhatsNew.Controls.Add(uc_wn);
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main._site_breadLink = "<li><a href=resourcecenter.aspx?id=0>" + "Resource Center" + "</a></li>"; 
        main._site_breadLink += "<li class=\"last\"> <a href=support.aspx?id=0><strong>" + "Support" + "</strong></a></li>";               
    }

    protected int Get_Id_Generic(int genDefaId)
    {
        MainContentGeneric resourcesinfo = new MainContentGeneric();
        DataSet dsresourcesinfo = new DataSet();
        int GeneIdG = 0;

        dsresourcesinfo = resourcesinfo.Get_Id_Generic(Convert.ToInt32(genDefaId));                
        foreach (DataTable table in dsresourcesinfo.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                GeneIdG = Convert.ToInt32(row["GeneId"].ToString());
            }
        }
        if (GeneIdG.ToString() == "") return 0;
        else return GeneIdG;
    }

    protected void LoadResoucesInfoPurchasing()
    {
        MainContentGeneric resourcesinfo = new MainContentGeneric();
        DataSet dsresourcesinfo = new DataSet();
        StringBuilder sb = new StringBuilder();

        Boolean ingreso = false;
        Boolean cerrarTag = true;
        
        //---   
        int cont = 1;
        int contAux = 1;        
        dsresourcesinfo = resourcesinfo.getAllGenericResourcesPurchasing(Convert.ToInt32(GeneIdPurchasing));        
        sb.AppendLine("<div class=\"titleSupport\"><h1>Support</h1></div>");  
        foreach (DataTable table in dsresourcesinfo.Tables)
        {
            foreach (DataRow row in table.Rows)
            {

                if (ingreso == false)
                {
                    ingreso = true;                       
                    sb.AppendLine("<div class=\"boxResourceSupport\">");
                    sb.AppendLine("<div><h2>" + row["GeneTitle"].ToString() + "</h2></div>");
                    sb.AppendLine(" <div class=\"mainResourceSupport\">");
                }

                if (cont == 3)
                {
                    cont = 1;
                    cerrarTag = true;
                }

                if (cont == 1) sb.AppendLine("<div class=\"list1ResourceSupport colorLinks\">");

                sb.AppendLine("<p><span>" + contAux + ". </span><a href=\"Generic_d.aspx?id=" + GeneIdPurchasing + "&TypeGen=1#" + row["GeneDId"].ToString() + "\">" + row["GeneDTitle"].ToString() + "</a></p>");

                if (cont == 2)
                {
                    sb.AppendLine("</div>");
                    cerrarTag = false;
                }

                cont++;
                contAux++;
            }
        }

        if (cerrarTag == true) sb.AppendLine("</div>");
        if (ingreso == true)
        {
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"bottonMore contSeeMore\">");
                sb.AppendLine("<a href=\"Generic_d.aspx?id=" + GeneIdPurchasing + "&TypeGen=1\"><img src=\"" + Global.globalSiteImagesPath + "/plus_sign.gif\" width=\"8\" height=\"8\" /> see more purchasing information</a>");
                sb.AppendLine("</div>");
                sb.AppendLine("<div class=\"clear\"></div>");
            sb.AppendLine("</div>");
            PlaceHolder_Resources_Purchasing.Controls.Add(new LiteralControl(sb.ToString()));
        }
        

        sb = null;
        resourcesinfo = null;
        dsresourcesinfo = null;
    }

    protected void LoadResoucesInfoFaqs()
    {
        MainContentGeneric resourcesinfo = new MainContentGeneric();
        DataSet dsresourcesinfo = new DataSet();
        StringBuilder sb = new StringBuilder();

        //---   
        int cont = 0;
        Boolean ingreso = false;
        
        dsresourcesinfo = resourcesinfo.getAllGenericResourcesFaqs(Convert.ToInt32(GeneIdFaqs));       
        foreach (DataTable table in dsresourcesinfo.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (cont == 0)
                {
                    cont++;
                    ingreso = true;
                    sb.AppendLine("<div class=\"boxResourceSupport\">");
                        sb.AppendLine("<div><h2>" + row["GeneTitle"].ToString() + "</h2></div>");
                        sb.AppendLine("<div class=\"mainResourceSupport\">");
                            sb.AppendLine("<div class=\"accordion5\">");
                                sb.AppendLine("<dl class=\"accordion5\" id=\"slider5\">");
                }                    
                                    sb.AppendLine("<dt>Q. <span>" + row["GeneCTitle"].ToString() + "</span></dt>");
                                    sb.AppendLine("<dd>");
                                        sb.AppendLine("<div class=\"answerOpt\"><p>A.</p></div>");
                                        sb.AppendLine("<div class=\"answerInfo\">" + row["GeneCContent"].ToString() + "</div>");
                                        sb.AppendLine("<div class=\"clear\"></div>");
                                    sb.AppendLine("</dd>");                       
            }
        }
                if (ingreso == true)
                {
                                sb.AppendLine("</dl>");
                                sb.AppendLine("<script type=\"text/javascript\">");
                                    sb.AppendLine("var slider5=new accordion.slider(\"slider5\");");
                                    sb.AppendLine("slider5.init(\"slider5\",5,\"open\");");
                                sb.AppendLine("</script>");
                            sb.AppendLine("</div>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("<div class=\"clear\"></div>");
                        sb.AppendLine("<div class=\"bottonMore contSeeMore\">");
                            sb.AppendLine("<a href=\"Generic_c.aspx?id=" + GeneIdFaqs + "\"><img src=\"" + Global.globalSiteImagesPath + "/plus_sign.gif\" width=\"8\" height=\"8\" /> see more FAQs</a>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("<div class=\"clear\"></div>");
                    sb.AppendLine("</div>");
                }
        
        PlaceHolder_Resources_FAQs.Controls.Add(new LiteralControl(sb.ToString()));
        sb = null;
        resourcesinfo = null;
        dsresourcesinfo = null;
    }
}
