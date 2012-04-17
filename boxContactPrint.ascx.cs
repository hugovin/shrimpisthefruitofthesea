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

namespace uc_Right
{

    public partial class boxContactPrint : System.Web.UI.UserControl{
        private int intSite;
        private int intCont;
        //--
          private string SiteContTitle;
	    private string SiteContAddress;
	    private string SiteContEmailCus;
	    private string SiteContEmailSal;
	    private string SiteConPhone;
	    private string SiteCopy;
	    private string SiteURL;
	    private string SiteTagLine;
	    //--
        private bool _aboutus = false;
        private bool _finder = false;
        private bool _subject = false;
        private bool _browse = false;
        private bool _resourcecenter = false;
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
        //--
        //private LeftMenu leftmenu = new LeftMenu();

        protected void Page_Load(object sender, EventArgs e)
        {

            intSite = Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
            intCont = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
			Footer footer = new Footer();
	        DataSet dsContact = new DataSet();
	        DataSet dsSite = new DataSet();
	        dsContact = footer.GetAllSiteContact();
	        dsSite = footer.GetSiteInformation();
	        foreach (DataTable table in dsContact.Tables)
	        {
	            foreach (DataRow row in table.Rows)
	            {
	                SiteContTitle       = row["SiteContTitle"].ToString();
	                SiteContAddress     = row["SiteContAddress"].ToString();
	                SiteContEmailCus    = row["SiteContEmailCus"].ToString();
	                SiteContEmailSal    = row["SiteContEmailSal"].ToString();
	
	            }
	        }
	         foreach (DataTable table in dsSite.Tables)
	        {
	            foreach (DataRow row in table.Rows)
	            {
	                SiteConPhone = row["sitephone"].ToString();
	                SiteCopy = row["sitecopy"].ToString();
	                SiteURL = row["siteurl"].ToString();
	                SiteTagLine = row["sitetagline"].ToString();
	            }
	        }
            LoadContact();

        }

        private void LoadContact()
        {
            //DataSet dsAboutUs = new DataSet();
            StringBuilder sb = new StringBuilder();
            //--
            //dsAboutUs = leftmenu.Get_LeftMenu_All_AboutUs(intSite);
            //--
           
            sb.AppendLine("<div class=\"infoCurriculum\">");
            sb.AppendLine("<h3>Curriculum Support Specialist</h3><br />");
            sb.AppendLine("<h4>Customer Service</h4>");
            sb.AppendLine("<p><span>"+SiteContEmailCus+"</span></p><br />");
            sb.AppendLine("<p><span>Office:</span> "+SiteConPhone+"</p>");
            sb.AppendLine("<p><span>E Fax:</span> (800) 610-5005</p>");
            sb.AppendLine("</div>");

            //-- Begin Group Menu
            //sb.AppendLine("<dt>About Us</dt>");
            //sb.AppendLine("<dd>");
            //sb.AppendLine("<ul>");
            //foreach (DataTable table in dsAboutUs.Tables)
            //{
            //    foreach (DataRow row in table.Rows)
            //    {
            //        sb.AppendLine("<li><a href=\"" + row["geneid"].ToString() + "\">" + row["genetitle"].ToString() + "</a></li>");
            //    }
            //}
            //sb.AppendLine("</ul>");
            //sb.AppendLine("</dd>");
            //-- End Group Menu
            //--
            //dsAboutUs.Dispose();
            PlaceHolder_Contact.Controls.Add(new LiteralControl(sb.ToString()));
        }
    }
}