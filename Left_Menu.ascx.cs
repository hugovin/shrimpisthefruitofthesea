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
namespace uc_Left_Menu
{
    public partial class Left_Menu : System.Web.UI.UserControl
    {
        private int intSite;
        private int intCont;
        //--Active Menu
        int intActiveMenu;
        int intActiveSubMenu;
        //--
        private bool _aboutus = false;
        private bool _finder = true;
        private bool _subject = true;
        private bool _browse = true;
        private bool _resourcecenter = true;
        private int _activeMenu = 4;
        private int _liID = 0;
        //--
        public bool _Aboutus
        {
            get { return _aboutus; }
            set { _aboutus = value; }
        }
        public bool _Finder
        {
            get { return _finder; }
            set { _finder = value; }
        }
        public bool _Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
        public bool _Browse
        {
            get { return _browse; }
            set { _browse = value; }
        }
        public bool _Resourcecenter
        {
            get { return _resourcecenter; }
            set { _resourcecenter = value; }
        }
        public int _ActiveMenu
        {
            get { return _activeMenu; }
            set { _activeMenu = value; }
        }
        //--
        private LeftMenu leftmenu = new LeftMenu();
        private Addins addins = new Addins();

        protected void Page_Load(object sender, EventArgs e)
        {

            intSite = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
            intCont = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
            //-- Active Menu SetUp
            if (HttpContext.Current.Session["am"] != null)
                try
                {
                    intActiveMenu = Convert.ToInt32(HttpContext.Current.Session["am"]);
                }
                catch (Exception ex) { intActiveMenu = 4; }
            else
            {
                intActiveMenu = 4;
            }

            if (HttpContext.Current.Session["asm"] != null)
            {
                try
                {
                    intActiveSubMenu = Convert.ToInt32(HttpContext.Current.Session["asm"]);
                }
                catch (Exception ex) { intActiveSubMenu = 0; }
            }
            else
            {
                intActiveSubMenu = 0;
            }
            //--
            //--Product Finder
            if (_Finder)
                PlaceHolder_Finder.Controls.Add(new LiteralControl(LoadFinder().ToString()));
            else
                FinderAdvanced.Visible = false;
            //--    
            StringBuilder sb = new StringBuilder();
            sb.Append(Left_Menu_Head());
            if (_Subject) sb.Append(LoadSubject());
            if (_Browse) sb.Append(LoadBrowse());
            if (_Resourcecenter) sb.Append(LoadResourceCenter());
            if (_Aboutus) sb.Append(LoadAboutUs());
            sb.Append(Left_Menu_Foot());
            accordion2.InnerHtml = sb.ToString();

        }
        public Left_Menu()
        {

        }

        private StringBuilder Left_Menu_Head()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<dl class=\"accordion2\" id=\"slider2\">");
            return sb;
        }
        private StringBuilder Left_Menu_Foot()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("</dl>");
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("var slider2=new accordion.slider(\"slider2\");");
            sb.AppendLine("slider2.init(\"slider2\"," + (intActiveMenu == 3 ? (intActiveMenu - 3).ToString() : intActiveMenu.ToString()) + ",\"open\");");
            sb.AppendLine("</script>");
            return sb;
        }
        private StringBuilder LoadFinder()
        {
            DataSet dsFinder = new DataSet();
            DataSet dsFinderItem = new DataSet();

            StringBuilder sb = new StringBuilder();
            //--
            int ContSelected = 1;
            int SelCont = 1;

            ////-- Default Items --
            dsFinder = leftmenu.Get_LeftMenu_All_FinderDefault();
            //--
            foreach (DataTable table in dsFinder.Tables)
            {

                foreach (DataRow row in table.Rows)
                {
                    if (Convert.ToInt32(row["finddefaid"].ToString()) == 1) ContSelected = 1;
                    if (Convert.ToInt32(row["finddefaid"].ToString()) == 2) ContSelected = 2;
                    if (Convert.ToInt32(row["finddefaid"].ToString()) == 3) ContSelected = 3;
                    if (Convert.ToInt32(row["finddefaid"].ToString()) == 4) ContSelected = 4;
                    if (Convert.ToInt32(row["finddefaid"].ToString()) == 5) ContSelected = 5;

                    sb.AppendLine("<select style='width: 150px; margin: 2px;' name='" + "findopt" + ContSelected.ToString() + "'>");
                    sb.AppendLine("<option selected value=\"\">" + row["findtitle"].ToString() + "</option>");
                    dsFinderItem = leftmenu.Get_LeftMenu_All_FinderSubDefault(intSite, intCont, Convert.ToInt32(row["finddefaid"].ToString()));
                    if (dsFinderItem != null){
	                    foreach (DataTable stable in dsFinderItem.Tables)
	                    {
	                        foreach (DataRow srow in stable.Rows)
	                        {
	                            sb.AppendLine("<option title=\"" + srow["title"].ToString() + "\" alt=\"" + srow["title"].ToString() + "\" value=\"" + srow["id"].ToString() + "\">" + srow["title"].ToString() + "</option>");
	                        }
	                    }
                        dsFinderItem.Dispose();
                    }
                    sb.AppendLine("</select>");
                    sb.AppendLine("<br />");
                }
            }
            sb.AppendLine("</ul>");
            sb.AppendLine("</dd>");
            //--
            dsFinder.Dispose();
           
            return sb;

            StringBuilder sbf = new StringBuilder();
            sbf.AppendLine("");

        }
        private StringBuilder LoadSubject()
        {

            DataSet dsSubject = new DataSet();
            DataSet dsSubjectItem = new DataSet();

            StringBuilder sb = new StringBuilder();
            //--
            int asm = 1;
            //--
            dsSubject = leftmenu.Get_LeftMenu_All_Subjects(intSite, intCont);
            //--

            //-- Begin Group Menu
            sb.AppendLine("<dt>Subjects</dt>");
            sb.AppendLine("<dd>");
            sb.AppendLine("<ul>");
            
            if(dsSubject!=null){ 
            foreach (DataTable table in dsSubject.Tables)
            {
                //int num = 0;
                foreach (DataRow row in table.Rows)
                {
                    sb.AppendLine("<li id='li" + _liID.ToString() + "' ><a " + (asm == intActiveSubMenu && intActiveMenu == 0 ? "class='Chossed'" : "") + " href=\"result.aspx?findopt1=" + row["id"].ToString() + "&am=0&asm=" + asm.ToString() + "&nn=" + row["cat"].ToString() + "\" >" + CutDescription(row["cat"].ToString()) + "</a>");
                    dsSubjectItem = leftmenu.Get_LeftMenu_All_SubSubjects(intSite, intCont, Convert.ToInt32(row["id"].ToString()));
                    foreach (DataTable stable in dsSubjectItem.Tables)
                    {
                        if (dsSubjectItem.Tables["table"].Rows.Count == 0)
                            break;
                        sb.AppendLine("<div id=\"navBox\">");
                        sb.AppendLine("<div class=\"top\">");
                        sb.AppendLine("</div>");
                        sb.AppendLine("<div class=\"middle\">");
                        sb.AppendLine("<ul>");
                        foreach (DataRow srow in stable.Rows)
                        {
                            sb.AppendLine("<li><a href=\"result.aspx?findopt2=" + srow["subcategoryid"] + "&am=0&asm=" + asm.ToString() + "&nn=" + srow["subcategory"].ToString() + "\">" + CutSubDescription(srow["subcategory"].ToString()) + "</a></li>");
                        }
                        sb.AppendLine("</ul>");
                        sb.AppendLine("</div>");
                        sb.AppendLine("<div class=\"bottom\">");
                        sb.AppendLine("</div>");
                        sb.AppendLine("</div>");

                    }
                    sb.AppendLine("</li>");
                    _liID++;
                    asm++;
                    //sb.AppendLine("<li><a href=\"#\" class=\"" + (row["ContId"].ToString() == ContSelected.ToString() ? "current" : "") + "\">" + row["ContTitle"].ToString() + "</a></li>");
                }
            }
            }
            sb.AppendLine("</ul>");
            sb.AppendLine("</dd>");
            //-- End Group Menu

            return sb;
            //--
            dsSubject.Dispose();
            dsSubjectItem.Dispose();
        }

        private StringBuilder LoadBrowse()
        {
            DataSet dsBrowse = new DataSet();
            DataSet dsBrowseItem = new DataSet();

            StringBuilder sb = new StringBuilder();
            //--
            int ContSelected = 1;
            int asm = 1;
            string str_linkbrowse = "";
            //--
            //--
            sb.AppendLine("<dt>Browse</dt>");
            //-- Category Items --
            sb.AppendLine("<dd>");
            sb.AppendLine("<ul>");
            //-- Default Items --
            dsBrowse = leftmenu.Get_LeftMenu_All_BrowseDefault();
            //--
            if (dsBrowse != null)
            {
                foreach (DataTable table in dsBrowse.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {

                        if (Convert.ToInt32(row["browdefaid"].ToString()) == 1)
                        {
                            ContSelected = 1;
                            str_linkbrowse = "findopt6=1&am=1&asm=" + asm.ToString() + "";
                        }
                        if (Convert.ToInt32(row["browdefaid"].ToString()) == 2)
                        {
                            ContSelected = 1;
                            str_linkbrowse = "findopt6=8&am=1&asm=" + asm.ToString() + "";
                        }
                        if (Convert.ToInt32(row["browdefaid"].ToString()) == 3)
                        {
                            ContSelected = 5;
                            str_linkbrowse = "txtadv=&am=1&asm=" + asm.ToString() + "";
                        }
                        if (Convert.ToInt32(row["browdefaid"].ToString()) == 4)
                        {
                            ContSelected = 3;
                            str_linkbrowse = "txtadv=&am=1&asm=" + asm.ToString() + "";
                        }

                        sb.AppendLine("<li id='li" + _liID.ToString() + "'><a " + (asm == intActiveSubMenu && intActiveMenu == 1 ? "class='Chossed'" : "") + " alt=\"" + row["BrowDefaTitle"].ToString() + " \" >" + CutDescription(row["BrowDefaTitle"].ToString()) + "</a>");
                        dsBrowseItem = leftmenu.Get_LeftMenu_All_BrowseSubDefault(intSite, intCont, Convert.ToInt32(row["browdefaid"].ToString()));
                        if (dsBrowseItem != null)
                        {
                            foreach (DataTable stable in dsBrowseItem.Tables)
                            {
                                sb.AppendLine("<div id=\"navBox\">");
                                sb.AppendLine("<div class=\"top\">");
                                sb.AppendLine("</div>");
                                sb.AppendLine("<div class=\"middle\">");
                                sb.AppendLine("<ul>");
                                foreach (DataRow srow in stable.Rows)
                                {
                                    if ((Convert.ToInt32(asm.ToString()) != 3) == (Convert.ToInt32(asm.ToString()) != 4))
                                    {
                                        sb.AppendLine("<li><a href=\"result.aspx?findopt" + ContSelected.ToString() + "=" + srow["Id"].ToString() + "&am=1&asm=" + asm.ToString() + "&nn=" + srow["Title"].ToString() + "\">" + CutSubDescription(srow["Title"].ToString()) + "</a></li>");
                                    }
                                    else
                                    {
                                        sb.AppendLine("<li><a href=\"result.aspx?findopt" + ContSelected.ToString() + "=" + srow["Id"].ToString() + "&am=1&asm=" + asm.ToString() + "\">" + CutSubDescription(srow["Title"].ToString()) + "</a></li>");
                                    }
                                }
                                sb.AppendLine("</ul>");
                                sb.AppendLine("</div>");
                                sb.AppendLine("<div class=\"bottom\">");
                                sb.AppendLine("</div>");
                                sb.AppendLine("</div>");
                            }
                            dsBrowseItem.Dispose();
                        }
                        sb.AppendLine("</li>");
                        _liID++;
                        asm++;
                    }

                }
                dsBrowse.Dispose();
            }
            sb.AppendLine("</ul>");
            sb.AppendLine("</dd>");
            //--
            
            return sb;

        }
        private StringBuilder LoadResourceCenter()
        {
            //Get_LeftMenu_All_ResourceCenter

            DataSet dsResourceCenter = new DataSet();

            StringBuilder sb = new StringBuilder();
            //--
            int ContSelected = 1;
            int asm = 1;
            //--
            dsResourceCenter = leftmenu.Get_LeftMenu_All_ResourceCenter(intSite, intCont);
            //--

            //-- Begin Group Menu
            sb.AppendLine("<dt>Resource Center</dt>");
            sb.AppendLine("<dd>");
            sb.AppendLine("<ul>");
            foreach (DataTable table in dsResourceCenter.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if ((row["tempsitepage"].ToString() == "requestACatalog.aspx") && (((bool)Session[SiteConstants.UserValidLogin]) == false))
                    {
                        sb.AppendLine("<li><a href=\"#htmlElement\" id=\"mb15\" class=\"mb\" style=\"padding:4px 0px 6px 10px; text-decoration: none;\" rel=\"type:element\" onClick=\"followLink='" + row["tempsitepage"].ToString() + "?id=" + row["geneid"].ToString() + "&am=2&TypeGen=1&asm=" + asm.ToString() + "';clear_follow();\">Request A Catalog</a></li>");

                    }
                    else
                    {
                        sb.AppendLine("<li  id='li" + _liID.ToString() + "'><a " + (asm == intActiveSubMenu && intActiveMenu == 2 ? "class='Chossed'" : "") + " href=\"" + row["tempsitepage"].ToString() + "?id=" + row["geneid"].ToString() + "&am=2&TypeGen=1&asm=" + asm.ToString() + "\" alt=\"" + row["genetitle"].ToString() + "\">" + CutDescription(row["genetitle"].ToString()) + "</a>");
                        sb.AppendLine("</li>");
                    }

                    asm++;
                    _liID++;
                }
            }
            sb.AppendLine("</ul>");
            sb.AppendLine("</dd>");
            //-- End Group Menu
            dsResourceCenter.Dispose();
            return sb;
            //--

        }

        private StringBuilder LoadAboutUs()
        {
            //Get_LeftMenu_All_ResourceCenter

            DataSet dsAboutUs = new DataSet();

            StringBuilder sb = new StringBuilder();
            //--
            int ContSelected = 1;
            int asm = 1;
            //--
            dsAboutUs = leftmenu.Get_LeftMenu_All_AboutUs(intSite);
            //--

            //-- Begin Group Menu
            sb.AppendLine("<dt style=\"margin-top: -5px;\">About Us</dt>");
            sb.AppendLine("<dd>");
            sb.AppendLine("<ul>");
            foreach (DataTable table in dsAboutUs.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    sb.AppendLine("<li><a " + (asm == intActiveSubMenu && intActiveMenu == 3 ? "class='Chossed'" : "") + " href=\"" + row["tempsitepage"].ToString() + "?id=" + row["geneid"].ToString() + "&am=3&asm=" + asm.ToString() + "\" alt=\"" + row["genetitle"].ToString() + "\">" + CutDescription(row["genetitle"].ToString()) + "</a>");
                    sb.AppendLine("</li>");
                    asm++;
                    //sb.AppendLine("<li><a href=\"#\" class=\"" + (row["ContId"].ToString() == ContSelected.ToString() ? "current" : "") + "\">" + row["ContTitle"].ToString() + "</a></li>");
                }
            }
            sb.AppendLine("</ul>");
            sb.AppendLine("</dd>");
            //-- End Group Menu
            dsAboutUs.Dispose();
            return sb;
            //--
            ;
        }

        private string CutDescription(string description)
        {
            String strDesc = description;
            if (strDesc.Length >= 26)
            {
                strDesc = strDesc.Substring(0, 24) + "...";
            }
            return strDesc;
        }
        private string CutSubDescription(string description)
        {
            String strDesc = description;
            if (strDesc.Length >= 30)
            {
                strDesc = strDesc.Substring(0, 30) + "...";
            }
            return strDesc;
        }

    }
}