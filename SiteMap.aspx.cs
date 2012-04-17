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
using uc_Left_Menu;

public partial class SiteMap : System.Web.UI.Page
{
    private int intSite;
    private int intCont;
    //--
    protected void Page_Load(object sender, EventArgs e)
    {
        intSite = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
        intCont = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
        LoadAboutUs();
        LoadResourceCenter();
        LoadSubject();
        Product_MasterPage main = (Product_MasterPage)Page.Master;
        main._site_breadLink = "<li class=\"last\"><a  href=# onClick=\"return false;\"><strong>" + "Site Map" + "</strong></a></li>";
        main.pageTitleBar = "Site Map - "+main.pageTitleBar;  
        ////this.URLRedirect();
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Response.Redirect("Site_Map.html");
        }
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
                sb.AppendLine("<li><a href=\"" + row["tempsitepage"].ToString() + "?id=" + row["geneid"].ToString() + "\">" + row["genetitle"].ToString() + "</a></li>");
            }
        }

        PlaceHolder_AboutUs.Controls.Add(new LiteralControl(sb.ToString()));
        dsAboutUs = null;
        sb = null;
    }
    //--------------------------------------------------------------------------------------
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
                sb.AppendLine("<li><a href=\"" + row["tempsitepage"].ToString() + "?id=" + row["geneid"].ToString() + "\">" + row["genetitle"].ToString() + "</a></li>");
            }
        }

        PlaceHolder_ResourceCenter.Controls.Add(new LiteralControl(sb.ToString()));
        dsResource = null;
        sb = null;
    }
    //--------------------------------------------------------------------------------------
    private void LoadSubject()
    {
        uc_Left_Menu.LeftMenu leftmenu = new uc_Left_Menu.LeftMenu();
            DataSet dsSubject = new DataSet();
            DataSet dsSubjectItem = new DataSet();

            StringBuilder sb = new StringBuilder();
            //--
            int ContSelected = 1;
            int contSubject = 1;
            //--
            dsSubject = leftmenu.Get_LeftMenu_All_Subjects(intSite, intCont);
            //--
            
            //-- Begin Group Menu
            foreach (DataTable table in dsSubject.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    sb.AppendLine("<li><a href=\"result.aspx?findopt1=" + row["id"].ToString() + "&am=0&asm=" + contSubject + "\" >" + row["cat"].ToString() + "</a>");
                    dsSubjectItem = leftmenu.Get_LeftMenu_All_SubSubjects(intSite, intCont, Convert.ToInt32(row["id"].ToString()));
                    foreach (DataTable stable in dsSubjectItem.Tables)
                    {
                        if (dsSubjectItem.Tables["table"].Rows.Count == 0)
                            break;                        
                        sb.AppendLine("<ul>");
                        foreach (DataRow srow in stable.Rows)
                        {
				sb.AppendLine("<li><a href=\"result.aspx?findopt2=" + srow["subcategoryid"] + "&am=0&asm=" + contSubject + "&nn=" + srow["subcategory"].ToString() + "\">" + srow["subcategory"].ToString() + "</a></li>");
                        }
                        sb.AppendLine("</ul>");
                    }
                    sb.AppendLine("</li>");
                    //sb.AppendLine("<li><a href=\"#\" class=\"" + (row["ContId"].ToString() == ContSelected.ToString() ? "current" : "") + "\">" + row["ContTitle"].ToString() + "</a></li>");
			  contSubject++;
                }
            }
            //-- End Group Menu
            //--
            dsSubject.Dispose();
            dsSubjectItem.Dispose();
        
        PlaceHolder_Subjects.Controls.Add(new LiteralControl(sb.ToString()));
    }
}
