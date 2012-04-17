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

namespace uc_Right
{
    public partial class boxContact : System.Web.UI.UserControl
    {
        private int intSite;
        private int intCont;
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
			string ServiceRepresentative;
			string ServiceRepresentativeZipCode;
			string ServiceRepresentativeText;

			string CurrentTitle=""; // this is the kind  of CR
			DataSet CostRepList = new DataSet();
		// see if we have a ZipCode. If we are not loguet we will not have one
			try{
				ServiceRepresentativeZipCode=HttpContext.Current.Session["UserZipCode"].ToString();	
			}catch(Exception){
				ServiceRepresentativeZipCode="XX-XX-XX"; // this will make  the CustomerRepresentative.GetUserCustomerRepByZipCode STK method to get the default email 
			}
			
            //DataSet dsAboutUs = new DataSet();
            StringBuilder sb = new StringBuilder();
            //--
            //dsAboutUs = leftmenu.Get_LeftMenu_All_AboutUs(intSite);
            //--
            sb.AppendLine("<div class=\"infoCurriculum\">");
          
            // build the links
			try{
				int SiteId = 1;//Convert.ToInt32(HttpContext.Current.Session["SiteId"].ToString());
    			//int ContId = Convert.ToInt32(HttpContext.Current.Session["ContentId"].ToString());
    			
				CostRepList=CustomerRepresentative.GetUserCustomerRepByZipCode(Global.globalSiteNameAbr,ServiceRepresentativeZipCode,SiteId);
				//CustomerRepresentative CostRepL = new CustomerRepresentative();
                //CostRepL = CostRepL.GetUserCustomerRepByZipCode("ER", ServiceRepresentativeZipCode, 1);
                int count = 0;
                foreach (DataTable table in CostRepList.Tables){				
					foreach(DataRow row in table.Rows){
						if(CurrentTitle!=row["Title"].ToString()){  //add the new title
							sb.AppendLine("<h3>"+row["Title"].ToString()+":</h3><br />");
						}
                        sb.AppendLine("<p style=\"color:white;\" class=\"name\">" + row["FullName"].ToString() + "</p>");
						sb.AppendLine("<p style=\"color:white;\"><a href=\"mailto:"+row["Email"].ToString()+"\">"+row["Email"].ToString()+"</a>");
						sb.AppendLine("<span>Office:</span>"+row["Phone"].ToString()+" ext "+row["PhoneExt"].ToString()+"<br />");
						sb.AppendLine("<span>E Fax:</span>"+row["Fax"].ToString()+" </p><br />");
                        if(count<table.Rows.Count){
                            sb.AppendLine("<hr class=\"blueDotts\" />");
                        }
					}
				}			
			}catch(Exception){
				//ServiceRepresentativeEmail="custserv@edresources.com"; // this should come from the database.
				 sb.AppendLine("<p style=\"color:white;\"><a href=\"mailto:"+SiteContEmailCus+"\">"+SiteContEmailCus+"</a></p><br />");
			}
            sb.AppendLine("<h4>Customer Service</h4>");
 			//ServiceRepresentativeEmail="custserv@edresources.com"; // this should come from the database.                       
            sb.AppendLine("<p style=\"color:white;\"><a href=\"mailto:"+SiteContEmailCus+"\">"+SiteContEmailCus+"</a></p><br />");
            sb.AppendLine("<p><span >Office:</span> "+SiteConPhone+"</p>");
            sb.AppendLine("<p><span >E Fax:</span> (800) 610-5005</p>");
            sb.AppendLine("</div>			");

            //sb.AppendLine("<div class=\"DottedWhite\"></div>	");

            
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