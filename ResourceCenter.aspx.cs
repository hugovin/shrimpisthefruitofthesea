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

public partial class ResourceCenter : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string classId = "";
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
            CurrentChilPage = "resourcecenter.aspx";
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "resourcecenter.aspx";

        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }

        LoadResourcesCenter();
        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        uc_SAPricing uc_sap = (uc_SAPricing)(Page.LoadControl("uc_SAPricing.ascx"));
        PlaceHolder_uc_SAPricing.Controls.Add(uc_sap);
        uc_NewsNinfo uc_news = (uc_NewsNinfo)(Page.LoadControl("uc_NewsNinfo.ascx"));
        PlaceHolder_uc_NewsNinfo.Controls.Add(uc_news);
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main._site_breadLink += "<li><a href=\"#\">" + "Resource Center" + "</a></li><li class=\"last\"><a href=resourcecenter.aspx?id=0><strong>" + "Resources" + "</strong></a></li>";
        //-- Left Menu Active
        if (Request.QueryString["am"] != null && Request.QueryString["am"] != "")
        {
            try { SiteConstants.LeftMenuActive = Convert.ToInt32(Request.QueryString["am"].ToString()); }
            catch { SiteConstants.LeftMenuActive = 4; }
        }
        //--
        ////this.URLRedirect();

    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Response.Redirect("Resource_Center.html");
        }
    }
    protected void LoadResourcesCenter()
    {
        SiteResourceCenter resourceCenter = new SiteResourceCenter();
        DataSet dsresourceCenter = new DataSet();
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<div class=\"main-content-resource\">");
        dsresourceCenter = resourceCenter.Get_All_Resource_Center();
        string path = "";
        foreach (DataTable table in dsresourceCenter.Tables)
        {
            foreach (DataRow row in table.Rows)
            {

                if (row["ResoMainImage"].ToString() != "")
                {
                    sb.AppendLine("<div class=\"boxHelpGuide\"></div>");
                }
                sb.AppendLine("<div class=\"contHelpGuide\">");
                    
                    sb.AppendLine("<div class=\"support\">");
                        sb.AppendLine("<div class=\"boxSupport\">");
                            sb.AppendLine("<div class=\"boxTopSupport\"></div>");                                  
                            sb.AppendLine("<div class=\"contSupport\">");
                                sb.AppendLine("<div class=\"mainSupport\">");                                    
                                    sb.AppendLine("<div style=\"margin-bottom:20px;\">");
                                        sb.AppendLine(" <h1> " + row["ResoTitle1"].ToString()  + " </h1>");
                                        sb.AppendLine(row["ResoContent1"].ToString());                                                    
                                    sb.AppendLine("</div>");
                                    sb.AppendLine("<div class=\"mailSupport\"><p>Email us at: <a href=\"mailto:" + row["ResoEmail1"].ToString() + "\">" + row["ResoEmail1"].ToString() + "</a></div>");
                                    sb.AppendLine("<div class=\"phoneSupport\"><p>Call Us at: <strong>" + row["ResoContact1"].ToString() +"</strong></p></div>");
                                    sb.AppendLine("<div class=\"contcontrolSupport\">");
                                    sb.AppendLine("<div class=\"bottonMore mainControlSupport\"><a href=\"" + row["ResoMoreLink"] + "\"><img src=\"" + strFolder + "/plus_sign.gif\" width=\"8\" height=\"8\" /> more</a></div>");
                                    sb.AppendLine("</div>");                                    
                                sb.AppendLine("</div>");
                            sb.AppendLine("</div>");
                            sb.AppendLine("<div style=\"background:url("+ strFolder + "/boxResourceBotton.jpg) no-repeat; width:268px; height:4px;\"></div>");
                        sb.AppendLine("</div>");
                    sb.AppendLine("</div>");

                    sb.AppendLine("<div class=\"softwareTools\">");
                        sb.AppendLine("<div class=\"boxSupport\">");
                            sb.AppendLine("<div class=\"boxTopSupport\"></div>");
                            sb.AppendLine("<div class=\"contSupport\">");
                                sb.AppendLine("<div class=\"mainSupport\">");
                                    sb.AppendLine("<div style=\"margin-bottom:20px;\">");
                                        sb.AppendLine(" <h1> " + row["ResoTitle2"].ToString() + " </h1>");
                                        sb.AppendLine(row["ResoContent2"].ToString());
                                    sb.AppendLine("</div>");
                                    sb.AppendLine("<div class=\"mailSupport\"><p>Email us at: <a href=\"mailto:" + row["ResoEmail2"].ToString() + "\">" + row["ResoEmail2"].ToString() + "</a></div>");
                                    sb.AppendLine("<div class=\"phoneSupport\"><p>Call Us at: <strong>" + row["ResoContact2"].ToString() + "</strong></p></div>");
                                    sb.AppendLine("<div class=\"contcontrolSupport\">");
                                    sb.AppendLine("<div class=\"bottonMore mainControlSupport\"><a href=\"" + row["ResoMoreLink2"] + "\"><img src=\"" + strFolder + "/plus_sign.gif\" width=\"8\" height=\"8\" /> more</a></div>");
                                sb.AppendLine("</div>");
                            sb.AppendLine("</div>");
                            sb.AppendLine("</div>");
                            sb.AppendLine("<div style=\"background:url(" + strFolder + "/boxResourceBotton.jpg) no-repeat; width:268px; height:4px;\"></div>");
                        sb.AppendLine("</div>");
                    sb.AppendLine("</div>");

                    sb.AppendLine("<div class=\"clear\"></div>");
                sb.AppendLine("</div>");

                sb.AppendLine("<div>");
                if (!Regex.IsMatch(row["ResoImage"].ToString(), @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
                {
                    path = strFolder + "/";
                }
                else
                {
                    path = "";
                }
                sb.AppendLine("<a href=\"" + row["ResoLink1"].ToString() + "\"><div style=\"background:url(" + path + row["ResoImage"].ToString() + ") no-repeat; width:269px; height:167px; float:left;\"></div></a>");
                if (!Regex.IsMatch(row["ResoImage2"].ToString(), @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
                {
                    path = strFolder + "/";
                }
                else
                {
                    path = "";
                }
                sb.AppendLine("<a href=\"" + row["ResoLink2"].ToString() + "\"><div style=\"background:url(" + path + row["ResoImage2"].ToString() + ")  no-repeat; width:269px; height:167px; float:right;\"></div></a>");
                sb.AppendLine("</div>");                
                
            }
        }

        sb.AppendLine("</div>");
        PlaceHolder_Resource_Center.Controls.Add(new LiteralControl(sb.ToString()));

        sb = null;
        resourceCenter = null;
        dsresourceCenter = null;    
    }

}
