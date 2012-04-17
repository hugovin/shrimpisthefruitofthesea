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

public partial class requestAQuote : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    public string newOne = "";
    public bool FineREQ = false;
    public int quoteStatus = 0; //0 for normal, 1 for finish and 2 for error
    public bool error = false;

    //user Variables
    public string _user_email = "";
    public string _user_fullname = "";
    public string _user_schoolname = "";
    public string _user_add1 = "";
    public string _user_add2 = "";
    public string _user_city = "";
    public string _user_state = "";
    public string _user_zip = "";
    public string _user_phone = "";
    public string _user_title = "";
    public string _user_country = "";
    public string[] _quote_session = null;
    public string[,] _quoteThis = null;
    public string[] _requested_quote_title = null;
    public string[] _requested_quote_qty = null;

    //-- user information variables
       public string userLoginName = "";
        public string userLoginMail = "";
        public string completePost = "";
    //--
    //* Request a quote redirection params
    private string requestTitle = "";
    //*
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
            CurrentChilPage = "requestaquote.aspx";
        //--
    }
    private string[] addNewArrayString(string[] oldArray){
        string[] newArray = new string[oldArray.Length+1];
        Array.Copy(oldArray, newArray, oldArray.Length);
        return newArray;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "requestaquote.aspx";
        if (Session["quoteSession"] != null)
        {
            //_quoteThis = Convert.ToString(Session["quoteThis"]);
            _quoteThis = (string[,])Session["quoteSession"];
        }
        //Request a quote session variables.
        btnImageQuote.ImageUrl = Global.globalSiteImagesPath +"/btnReaAQuo.jpg";
        if(Request["title"] != null){
            string qtyProduct = "1";
            
            if (Request["qty"] != null)
            {
                qtyProduct = HttpUtility.HtmlEncode(Request["qty"].ToString());
                qtyProduct = qtyProduct.Replace("'", "");
            }

            newOne = HttpUtility.HtmlEncode(Request["title"].ToString());
            newOne = newOne.Replace("'", "");
            requestTitle = newOne;
            _requested_quote_title = newOne.Split('|');
            _requested_quote_qty = qtyProduct.Split('|');
            //--++
            //--++
            //--++
            
           
            if (_quoteThis == null)
            {
                _quoteThis = new string[2,_requested_quote_title.Length];
                for (int i = 0; i < _requested_quote_title.Length; i++) {
                    _quoteThis[0, i] = _requested_quote_title[i];
                    _quoteThis[1, i] = _requested_quote_qty[i];
                }
                Session["quoteSession"] = _quoteThis;
            }
            else {
                for (int internalCont = 0; internalCont < _requested_quote_title.Length; internalCont++)
                {
                    string newOneAux = _requested_quote_title[internalCont];
                    string newQtyAux = "1";
                    try {
                        newQtyAux = _requested_quote_qty[internalCont];
                    }catch(Exception exe){
                        
                    }
                    
                    int quantityProductsStored = _quoteThis.GetLength(1);
                    bool finderTitle = false;
                    for (int i = 0; i < quantityProductsStored; i++)
                    {
                        if (_quoteThis[0, i] == newOneAux)
                        {
                            finderTitle = true;
                            _quoteThis[1, i] = newQtyAux;
                        }
                    }
                    if (!finderTitle)
                    {
                        string[,] auxQuoteThis = new string[2, _quoteThis.GetLength(1) + 1];
                        for (int i = 0; i < _quoteThis.GetLength(1); i++)
                        {
                            for (int j = 0; j < _quoteThis.GetLength(0); j++)
                            {
                                auxQuoteThis[j, i] = _quoteThis[j, i];
                            }
                        }
                        _quoteThis = new string[2, _quoteThis.GetLength(1) + 1];
                        _quoteThis = auxQuoteThis;
                        _quoteThis[0, _quoteThis.GetLength(1) - 1] = newOneAux;
                        _quoteThis[1, _quoteThis.GetLength(1) - 1] = newQtyAux;
                        Session["quoteSession"] = _quoteThis;
                    }
                }
            }
            //--++
        }
        //this.URLRedirect();
        
        Site s = new Site();
        userGUIDTextBox.Text = "<input type=\"hidden\" name=\"Token\" id=\"Token\" value=\"" + s.getUserGUID() +"\">";
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main._site_breadLink += "<li><a href=\"#\"><strong>Request a Quote</strong></a></li>";
        getUserInfo();
        
        if (Request["end"] != null)
        {
            FineREQ = true;
              //completePost = "<br><br><div style=\"display: block; color:#0000FF;\" ><p><strong>SUCCESS:</strong> Quote sent successfully</p></div>";
        }
        fillOptions();

    }
    private void getUserInfo() {
        DataSet siteCQ = new DataSet();
        SiteCatalogQuote siteCQObject = new SiteCatalogQuote();
        Site s = new Site();
        siteCQ = siteCQObject.getUserIndo(s.getUserGUID().ToString());
        foreach(DataTable table in siteCQ.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                       _user_email = row["Email"].ToString();
                       _user_fullname = row["Fullname"].ToString();
                       _user_schoolname = row["BldgName"].ToString();
                       _user_add1 = row["Adress1"].ToString();
                       _user_add2 = row["Adress2"].ToString();
                       _user_city = row["City"].ToString();
                       _user_state = row["State"].ToString();
                       _user_country = row["Country"].ToString();
                       _user_zip = row["Zip"].ToString();
                       _user_phone = row["Phone"].ToString();
                       _user_title = row["Title"].ToString();
            }
        }
    
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            if (requestTitle == "")
            { // There is nothing
                if (Request["finishQuote"] == null)
                {
                    Response.Redirect("Request_A_Quote.html");
                }
                else {
                    Response.Redirect("Finish_Request_A_Quote.html");
                }
                
            }
            else 
            {
                Cleaner c = new Cleaner();
                requestTitle = c.cleanURL(requestTitle);
                requestTitle = requestTitle.Replace(":","");
                Response.Redirect("Request_A_Quote-_-"+requestTitle+".html");
            }
        }
    }
    private bool processInformation(){

        return false;
    }
    protected void btnImageQuote_Click(object sender, ImageClickEventArgs e)
    {
       
        //Event for post. Here must be the information.
        string fullName = Request["FullName"];
        string jobTitle = Request["JobTitle"];
        string bldgName = Request["BldgName"];
        string conPurchFor = Request["ConPurchFor"];
        string address1 = Request["Address1"];
        string address2 = Request["Address2"];
        string city = Request["City"];
        string state = Request["State"];
        string zip = Request["Zip"];
        string country = Request["Country"];
        string phone = Request["Phone"];
        string email = Request["Email"];
        string notes = Request["Notes"];
        SiteCatalogQuote siteCatalogQuote = new SiteCatalogQuote();
        string newQuote= siteCatalogQuote.addQuote(fullName, bldgName, address1, address2, city, state, zip, country, phone, email, jobTitle, conPurchFor, notes);
         string[] titles = new string[50];
        string[] plataform = new string[50];
        string[] quantity = new string[50];
        try
        {
            titles = Request.Form["Titles"].Split(',');
            plataform = Request.Form["Platforms"].Split(',');
            quantity = Request.Form["Qty"].Split(',');
        }catch(Exception err)
        {
            error = true;
        }
               string titlesComma = "";
        if (error != true)
        {
            if (titles[0] != "")
            {
            	if (titles.Length == plataform.Length && titles.Length == quantity.Length){ 
	                for (int i = 0; i < titles.Length; i++)
	                {
	                    siteCatalogQuote.addQuoteDetails(newQuote, titles[i], plataform[i], quantity[i]);
	                }
	                Response.Redirect("requestaquote.aspx?recurl=1&end=1");
                }
            }
            else
            {
                error = true;
            }
        }
        
    }

    protected void fillOptions()
    {
        System.Text.StringBuilder sbproduct = new System.Text.StringBuilder();
        sbproduct.AppendLine("<option value=\"\"></option>");
        if (_user_title == "Computer/Tech Coordinator")
        {
            sbproduct.AppendLine("<option value=\"Computer/Tech Coordinator\" selected >Computer/Tech Coordinator</option>");
        }
        else {
            sbproduct.AppendLine("<option value=\"Computer/Tech Coordinator\" >Computer/Tech Coordinator</option>");
        }
        ///-------------------------------
        if (_user_title == "Teacher")
        {
            sbproduct.AppendLine("<option value=\"Teacher\" selected>Teacher</option>");
        }
        else
        {
            sbproduct.AppendLine("<option value=\"Teacher\">Teacher</option>");
        }
        ///-------------------------------
        if (_user_title == "Administrator")
        {
            sbproduct.AppendLine("<option value=\"Administrator\" selected>Administrator</option>");
        }
        else
        {
            sbproduct.AppendLine("<option value=\"Administrator\">Administrator</option>");
        }
        ///-------------------------------
        if (_user_title == "Librarian/Media Specialist")
        {
            sbproduct.AppendLine("<option value=\"Librarian/Media Specialist\" selected>Librarian/Media Specialist</option>");
        }
        else
        {
            sbproduct.AppendLine("<option value=\"Librarian/Media Specialist\">Librarian/Media Specialist</option>");
        }
        ///-------------------------------
        if (_user_title == "Curriculum Specialist")
        {
            sbproduct.AppendLine("<option value=\"Curriculum Specialist\" selected>Curriculum Specialist</option>");
        }
        else
        {
            sbproduct.AppendLine("<option value=\"Curriculum Specialist\">Curriculum Specialist</option>");
        }
        ///-------------------------------
        if (_user_title == "Title 1 Coordinator")
        {
            sbproduct.AppendLine("<option value=\"Title 1 Coordinator\" selected>Title 1 Coordinator</option>");
        }
        else
        {
            sbproduct.AppendLine("<option value=\"Title 1 Coordinator\">Title 1 Coordinator</option>");
        }
        ///-------------------------------
        if (_user_title == "Parent/Home Purchaser")
        {
            sbproduct.AppendLine("<option value=\"Parent/Home Purchaser\" selected>Parent/Home Purchaser</option>");
        }
        else
        {
            sbproduct.AppendLine("<option value=\"Parent/Home Purchaser\">Parent/Home Purchaser</option>");
        }
        ///-------------------------------
        if (_user_title == "Home School Teacher")
        {
            sbproduct.AppendLine("<option value=\"Home School Teacher\" selected>Home School Teacher</option>");            
        }
        else
        {
            sbproduct.AppendLine("<option value=\"Home School Teacher\">Home School Teacher</option>");
        }
        ///-------------------------------
        if (_user_title == "Assessment Coordinator")
        {
            sbproduct.AppendLine("<option value=\"Assessment Coordinator\" selected>Assessment Coordinator</option>");
        }
        else
        {
            sbproduct.AppendLine("<option value=\"Assessment Coordinator\">Assessment Coordinator</option>");
        }
        ///-------------------------------
        if (_user_title == "Youth/Ed Director")
        {
            sbproduct.AppendLine("<option value=\"Youth/Ed Director\" selected>Youth/Ed Director</option>");
        }
        else
        {
            sbproduct.AppendLine("<option value=\"Youth/Ed Director\">Youth/Ed Director</option>");
        }
        ///-------------------------------
        if (_user_title == "Other")
        {
            sbproduct.AppendLine("<option value=\"Other\" selected>Other</option>");
        }
        else
        {
            sbproduct.AppendLine("<option value=\"Other\">Other</option>");
        }
        Options.Controls.Add(new LiteralControl(sbproduct.ToString()));
    }

}