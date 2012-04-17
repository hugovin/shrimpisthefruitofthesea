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
public partial class result : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string strFolder = SiteConstants.imagesPathTb;
    public string _ba = "";
    public string _sTextFinder = "";
    public string _sTextFinderRefine = "";
    public string _show_TextFinder = "display:none;";
    public int _pg = 0, _pgg = 0;
    public int _startRecord = 0, _maxRedord = 0, _ds_nr = 0;
    private int PageSize, PageNum, FirstRow;
    private string _sb = "1";
    public string _sFindOpt1="";
    private void GetVars()
    {
        //---
        if (Session["SiteId"] != null)
        {
            SiteId = Session["SiteId"].ToString();
        }
        else {
            SiteId = Global.globalSiteId;
        }

        ContentId = Request["ci"];
        if (ContentId != null)
            Session["ContentId"] = ContentId;
        else
        {
            if (Session["ContentId"] != null)
            {
                ContentId = Session["ContentId"].ToString();
            }
            else {
                ContentId = "1";
            }
        }
        //--
        if (Session["CurrentChilPage"] != null)
            CurrentChilPage = Session["CurrentChilPage"].ToString();
        else
            CurrentChilPage = "result.aspx";
        //--
        _ba = Request["ba"];
        _sTextFinder = Request["txtadv"];
        if (_sTextFinder == "Keyword / Item #") _sTextFinder = "";
        //--
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        if (_sTextFinder != null)
        {
            main.pageTitleBar = "Results for: " + _sTextFinder + " - " + main.pageTitleBar;
        }
        else {
            main.pageTitleBar = "Results Page " + _sTextFinder + " - " + main.pageTitleBar;
        }
        //--
        string s_pg = Request["pg"];
        if (s_pg != null && s_pg != "")
        {
            _pg = Convert.ToInt32(s_pg);
        }
        else
        {
            _pg = 1;
        }
        string s_pgg = Request["pgg"];
        if (s_pgg != null && s_pgg != "")
        {
            _pgg = Convert.ToInt32(s_pgg);
        }
        else
        {
            _pgg = 1;
        }

        _sb = Request["sb"];
        if (_sb != null && _sb != "")
        {
            _sb = _sb;
        }
        else
        {
            _sb = "1";
        }
    }

    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            string op1 = "";
            if (Request["findopt1"] != null)
            {
                op1 = Request["findopt1"].ToString();
            }
            else
            {
                op1 = "";
            }


            string op2 = "";
            if (Request["findopt2"] != null)
            {
                op2 = Request["findopt2"].ToString();
            }
            else
            {
                op2 = "";
            }

            string op3 = "";
            if (Request["findopt3"] != null)
            {
                op3 = Request["findopt3"].ToString();
            }
            else
            {
                op3 = "";
            }

            string op4 = "";
            if (Request["findopt4"] != null)
            {
                op4 = Request["findopt4"].ToString();
            }
            else
            {
                op4 = "";
            }

            string op5 = "";
            if (Request["findopt5"] != null)
            {
                op5 = Request["findopt5"].ToString();
            }
            else
            {
                op5 = "";
            }

            string txt = "";
            if (Request["txtadv"] != null)
            {
                txt = Request["txtadv"].ToString();
            }
            else
            {
                txt = "";
            }
            Cleaner c = new Cleaner();
            Response.Redirect("search.pagegroup-" + _pgg + "_page-" + _pg + "_textsearch-" + c.cleanURL(txt) + "_fopt-" + op1 + "_fopt-" + op2 + "_fopt-" + op3 + "_fopt-" + op4 + "_fopt-" + op5 + ".shtml");

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int intSubject = 0;
        GetVars();
        //-- Left Menu Active
        //if (Request.QueryString["am"] != null && Request.QueryString["am"] != "")
        //{
        //    try { SiteConstants.LeftMenuActive = Convert.ToInt32(Request.QueryString["am"].ToString()); }
        //    catch { SiteConstants.LeftMenuActive = 4; }
        //}
        //else { SiteConstants.LeftMenuActive = 4; }
        //--
        //////this.URLRedirect();
        Session["CurrentChilPage"] = "result.aspx";
        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }

        if (_ba == "true")
        {
            _show_TextFinder = "visibility:visible;";
        }
        else
        {
            FinderTitle();
        }
       if (Request["findopt1"] != null && Request["findopt1"] != "")
        {
            try
            {
                _sFindOpt1 = Request["findopt1"].ToString();
                intSubject = Convert.ToInt32(Request["findopt1"]);
            }
            catch (Exception ex) { _sFindOpt1 = ""; }
        } 
 




        PlaceHolder_Finder.Controls.Add(new LiteralControl(LoadFinder().ToString()));
        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        //---------------------------------
        uc_FeatureProduct ucFeatureProduct = (uc_FeatureProduct)(Page.LoadControl("uc_FeatureProduct.ascx"));
        ucFeatureProduct.intSubjId = intSubject;
        PlaceHolder_uc_FeatureProduct.Controls.Add(ucFeatureProduct);
        //-------------------------------
        uc_Specials ucSpecials = (uc_Specials)(Page.LoadControl("uc_Specials.ascx"));
        ucSpecials.intSubjId = intSubject;
        PlaceHolder_uc_Specials.Controls.Add(ucSpecials);
        //-------------------------------
        uc_BestSellers ucBestSellers = (uc_BestSellers)(Page.LoadControl("uc_BestSellers.ascx"));
        ucBestSellers.intSubjId = intSubject;
        PlaceHolder_uc_BestSellers.Controls.Add(ucBestSellers);
        //--------------------------------
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main._site_breadLink += "<li class=\"last\"><a href=result.aspx><strong>" + "Results" + "</strong></a></li>";

    }

    protected void FinderTitle()
    {
        //--
        PageSize = 5;
        PageNum = _pg;
        FirstRow = PageNum * PageSize - PageSize;
        int CurrentRow = 0;
        //--
        Result resultTitle = new Result();

        string sTextFinder = _sTextFinder;
        string findopt1 = Request["findopt1"]; //sSubjSubCateId
        string findopt2 = Request["findopt2"]; //sTeacSubCateId
        string findopt3 = Request["findopt3"]; //sGrade
        string findopt4 = Request["findopt4"]; //sPlatformId
        string findopt5 = Request["findopt5"]; //sPubId
        string findopt6 = Request["findopt6"]; //sPubId
        if (sTextFinder == null) sTextFinder = "";
        if (findopt1 != null) findopt1 = (findopt1.Trim() == "" ? null : findopt1);
        if (findopt2 != null) findopt2 = (findopt2.Trim() == "" ? null : findopt2);
        if (findopt3 != null) findopt3 = (findopt3.Trim() == "" ? null : findopt3);
        if (findopt4 != null) findopt4 = (findopt4.Trim() == "" ? null : findopt4);
        if (findopt5 != null) findopt5 = (findopt5.Trim() == "" ? null : findopt5);
        if (findopt6 != null) findopt6 = (findopt6.Trim() == "" ? null : findopt6);
	//For to ShareThisUrl/
	string strUrlST = "";
	string am = Request["am"];
	string asm = Request["asm"];
	strUrlST = "?am=" + am + "&asm=" + asm +"&txtadv=" + sTextFinder;
	if (findopt1 != null) strUrlST = strUrlST + "&findopt1=" + findopt1;
	if (findopt2 != null) strUrlST = strUrlST + "&findopt2=" + findopt2;
	if (findopt3 != null) strUrlST = strUrlST + "&findopt3=" + findopt3;
	if (findopt4 != null) strUrlST = strUrlST + "&findopt4=" + findopt4;
	if (findopt5 != null) strUrlST = strUrlST + "&findopt5=" + findopt5;
	if (findopt6 != null) strUrlST = strUrlST + "&findopt6=" + findopt6;
	Main_MasterPage main = (Main_MasterPage)Page.Master;
	if (strUrlST != "")
	{
	main._site_ShareThisLink = Request.Url.AbsoluteUri + strUrlST;
	}
	//END For to ShareThisUrl/

        DataSet dsfinder = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //---
        dsfinder.Clear();
        dsfinder = resultTitle.Find_FinderTitleCatGroup(sTextFinder, findopt1, findopt2, findopt3, findopt4, findopt5, _sb, findopt6);

        // Added by Jordan Sherer - Sept 1, 2009
        // Need to make sure we don't have a null value;
	if(dsfinder == null || dsfinder.Tables == null || dsfinder.Tables.Count <= 0)
        {
            return; 
        }
        _ds_nr = dsfinder.Tables[0].Rows.Count;
        if (_ds_nr == 1) Response.Redirect("product.aspx?p=" + dsfinder.Tables[0].Rows[0].ItemArray[0].ToString());
        LoadFinderPagination(_ds_nr, sTextFinder, findopt1, findopt2, findopt3, findopt4, findopt5, findopt6);
        

        foreach (DataTable table in dsfinder.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if ((FirstRow <= CurrentRow) && (CurrentRow < (PageNum * PageSize)))
                {
                    sb.AppendLine("<div class=\"prodSrcRef\">");
                    sb.AppendLine("<div class=\"prodImg\">");
                    sb.AppendLine("<div id=\"boxContImage\"  style=\"width:115px; height:115px;\">");
                    sb.AppendLine("<a href=\"product.aspx?p=" + row["titleid"].ToString() + "\"><img id=\"images\" title=\"" + row["pubname"].ToString() +" : " +row["title"].ToString() + "\" style=\"width: 115px;height:115px;\" src=\"" + strFolder + "tn_" + row["ImageTn"].ToString() + "\"></a>"); //sb.AppendLine("<img src=\"" + row["imagetn"].ToString() + "\">");
                    sb.AppendLine("</div>");                                                                                                                                             //onload=\"getDim(document.getElementById('boxContImage'),this)\"
                    sb.AppendLine("</div>");
                    sb.AppendLine("<div class=\"prodSrcDesc\">");
                    sb.AppendLine("<h2><a href=\"product.aspx?p=" + row["titleid"].ToString() + "\">" + row["title"].ToString() + "</a></h2>");
                    sb.AppendLine("<p><em>by: </em><a href=\"result.aspx?findopt5=" + row["PubId"].ToString() + "&am=1&asm=" + 3 + "\">" + row["pubname"].ToString() + "</a></p>");

                    if (Convert.ToDouble(row["yousave"].ToString()) > 0)
                    {
                        sb.AppendLine("<h3> $" + String.Format("{0:#,0.00}", row["Er_price"]) + "</h3>");
                        sb.AppendLine("<h4>Your DISCOUNTED price</h4>");
                        sb.AppendLine("<h5>You Save: $" + String.Format("{0:#,0.00}", row["yousave"]) + "</h5>");
                    }
                    else
                    {
                        if (row["TitleId"].ToString() != Resources.Resource.TorchProductId)
                        {
                            sb.AppendLine("<h6> $" + String.Format("{0:#,0.00}", row["Er_price"]) + "</h6>");
                        }
                        else {
                            sb.AppendLine("<h6> Configure First</h6>");
                        }
                    }
                    if (row["TitleId"].ToString() != Resources.Resource.TorchProductId)
                    {
                        sb.AppendLine(Cart.CreateAddToCartLink("<img src=\"" + Global.globalSiteImagesPath + "/addToCart.jpg\" width=\"109\" height=\"26\" />", row["titleid"].ToString(), row["defaultsku"].ToString(), 1, 0));
                    }
                    sb.AppendLine("</div>");
                    sb.AppendLine("<div class=\"prodNumber\">");
                    sb.AppendLine("<div class=\"prodNumCont\">");
                    if ((row["plat_win_flag"].ToString() == "1") && (row["plat_mac_flag"].ToString() == "1"))
                    { sb.AppendLine("<p>Mac / Windows</p>"); }
                    else
                    { sb.AppendLine("<p>" + (row["plat_win_flag"].ToString() == "1" ? "Windows" : "") + "" + (row["plat_mac_flag"].ToString() == "1" ? "Mac" : "") + "</p>"); }

                    sb.AppendLine("<div class=\"numBG\">Grades:<br/><span> " + row["grades"].ToString() + "</span></div>");
                    sb.AppendLine("<p>Item #: " + row["sku"].ToString() + "</p>");
                    sb.AppendLine("</div>");
                    //--Login Validate
                    if ((bool)Session[SiteConstants.UserValidLogin])
                    {

                            sb.AppendLine("<a href=\"addWish.aspx?p=" + row["TitleId"].ToString() + "&sk=" + row["defaultsku"].ToString() + "&skd=" + row["SKUDesc"] + "\" rel=\"width:560,height:126,ajax:true\" id=\"mb10\" class=\"mb\" title=\"Add Product\"><img src=\"" + Global.globalSiteImagesPath + "/addToWish.jpg\" border=\"0\"></a>");

                        //duncan working 
                    }
                    else
                    {
                        sb.AppendLine("<a href=\"#htmlElement\" id=\"mb15\" class=\"mb\" title=\"\" rel=\"type:element\"><img src=\"" + Global.globalSiteImagesPath + "/addToWish.jpg\" border=\"0\"></a>");
                    }
                    sb.AppendLine("<a href=\"requestaquote.aspx?title=" + row["title"].ToString() + "\"><img src=\"" + Global.globalSiteImagesPath + "/quote.jpg\" border=\"0\"></a>");
                    sb.AppendLine("</div>");
                    sb.AppendLine("<div class=\"clear\"></div>");
                    sb.AppendLine("</div>");


                }
                if (CurrentRow > (PageNum * PageSize)) break;
                CurrentRow++;
            }
        }
        sb.AppendLine("<div class=\"prodSrcRef\">");
        sb.AppendLine("</div>");
        dsfinder.Clear();
        dsfinder.Dispose();
        dsfinder = null;
        PlaceHolder_Result.Controls.Add(new LiteralControl(sb.ToString()));
    }

    private StringBuilder LoadFinder()
    {
        DataSet dsFinder = new DataSet();
        DataSet dsFinderItem = new DataSet();
        uc_Left_Menu.LeftMenu leftmenu = new uc_Left_Menu.LeftMenu();
        StringBuilder sb = new StringBuilder();
        //--
        int ContSelected = 1;
        
        //-- Default Items --
        dsFinder = leftmenu.Get_LeftMenu_All_FinderDefault();
        //--
        foreach (DataTable table in dsFinder.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (Convert.ToInt32(row["finddefaid"].ToString()) != 1)
                {

                    if (Convert.ToInt32(row["finddefaid"].ToString()) == 2) ContSelected = 2;//Teaching
                    if (Convert.ToInt32(row["finddefaid"].ToString()) == 3) ContSelected = 3;//Grade
                    if (Convert.ToInt32(row["finddefaid"].ToString()) == 4) ContSelected = 4;//Platform
                    if (Convert.ToInt32(row["finddefaid"].ToString()) == 5) ContSelected = 5;//Publisher

                   if (ContSelected == 2)
                    { sb.AppendLine("<select  name='" + "findopt" + ContSelected.ToString() + "' style=\"width: 210px !important\" onchange=\"document.getElementById('optmeth').value=''; document.getElementById('nn').value='';\">"); }
                    else if(ContSelected == 5)
                    { sb.AppendLine("<select  name='" + "findopt" + ContSelected.ToString() + "' style=\"width: 120px !important; display:none;\">"); }
                    else 
                    { sb.AppendLine("<select  name='" + "findopt" + ContSelected.ToString() + "' style=\"width: 120px !important\">"); }

                    if (row["findtitle"].ToString() == "Teaching Method")
                    {
                        string findopt2 = Request["findopt2"];
                        sb.AppendLine("<option selected value=\"" + findopt2 + "\">Method </option>");
                    }
                    else
                    {
                        if (ContSelected == 2)
                        {
                            string findopt2 = Request["findopt2"];
                            sb.AppendLine("<option selected value=\"" + findopt2 + "\" id='optmeth'>" + row["findtitle"].ToString() + "</option>");
                        }
                        else { sb.AppendLine("<option selected value=\"\">" + row["findtitle"].ToString() + "</option>"); }
                    }

                    dsFinderItem = leftmenu.Get_LeftMenu_All_FinderSubDefault(Convert.ToInt32(SiteId), Convert.ToInt32(ContentId), Convert.ToInt32(row["finddefaid"].ToString()));
                    string comma = "";
                    int cont = 0;
                    int cont2 = 0;
                    int maxcont = 0;
                    if (dsFinderItem != null)
                    {
                        maxcont = dsFinderItem.Tables[0].Rows.Count;
                    }
                    else {
                        maxcont = 0;
                    }

                    if (maxcont > 0)
                    {
                        foreach (DataTable stable in dsFinderItem.Tables)
                        {
                            foreach (DataRow srow in stable.Rows)
                            {
                                if (Request["findopt" + ContSelected.ToString()] == srow["id"].ToString())
                                {
                                    if (cont < maxcont)
                                    {
                                        comma = ",";
                                    }
                                    _sTextFinderRefine = _sTextFinderRefine + "" + comma + "\"" + srow["title"].ToString() + "\"";

                                    cont++;
                                }
                                sb.AppendLine("<option value=\"" + srow["id"].ToString() + "\"" + (Request["findopt" + ContSelected.ToString()] == srow["id"].ToString() ? "Selected" : "") + ">" + srow["title"].ToString() + "</option>");
                                cont++;

                            }
                        }
                        sb.AppendLine("</select>");
                    }
                }
                //SelCont++;
            }
        }
        dsFinder.Dispose();
        if (dsFinderItem != null)
            dsFinderItem.Dispose();
        
        return sb;
    }

    private void LoadFinderPagination(int dsNumRec, string TextFinder, string SubjSubCateId, string TeacSubCateId, string Grade, string PlatformId, string PubId, string CategoryGroup)
    {
        int numpagesrest = dsNumRec % 5;
        int numpages = dsNumRec / 5;
        if (numpagesrest >= 1) numpages++;
        int contrec = 0;

        //"<a href=\"result.aspx?sb=" + _sb + "&pgg=" + (_pgg - 1).ToString() + "&pg=" + (_pg - 1).ToString() + "&txtadv=" + TextFinder + "&findopt1=" + SubjSubCateId + "&findopt2=" + TeacSubCateId + "&findopt3=" + Grade + "&findopt4=" + PlatformId + "&findopt5=" + PubId + "&findopt6=" + CategoryGroup + "\">PREVIOUS</a>"));
        string link_url = String.Format("result.aspx?sb={0}&pgg={1}&pg={2}&txtadv={3}&findopt1={4}&findopt2={5}&findopt3={6}&findopt4={7}&findopt5={8}&findopt6={9}", _sb, _pgg, _pg, _sTextFinder, SubjSubCateId, TeacSubCateId, Grade, PlatformId, PubId, CategoryGroup);
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main._site_ShareThisLink = String.Format("http://{0}/{1}", Request.Url.Host, link_url);
        main.LoadBread();
        /*if (link_url != "")
        {
            Control bc = main.FindControl("");
            Control c = main.FindControl("SocialTwistPlaceholder");
            Response.Write(c);
            if(c != null && c.GetType().Equals(typeof(LiteralControl)))
            {
                LiteralControl social_twist = (LiteralControl) c;
                social_twist.Text = link_url;
            }
            //main._site_ShareThisLink = Request.Url.AbsoluteUri + link_url;
        }*/

        StringBuilder sba = new StringBuilder();
        StringBuilder sbb = new StringBuilder();
        StringBuilder sbc = new StringBuilder();
        sba.AppendLine("<div id=\"resultControls2\">");
        if (dsNumRec > 0)
        {
            //--
            sbb.AppendLine("<div id=\"boxSortBy\">");
            sbb.AppendLine("<div class=\"sortBy\">");
            sbb.AppendLine("<p>Sort by:</p>");
            sbb.AppendLine("<select name=\"sb\" tabindex=\"-1\" onChange=\"window.location.href='result.aspx?sb='+this.value+'&pgg=1&pg=1&txtadv=" + TextFinder + "&findopt1=" + SubjSubCateId + "&findopt2=" + TeacSubCateId + "&findopt3=" + Grade + "&findopt4=" + PlatformId + "&findopt5=" + PubId + "&findopt6=" + CategoryGroup + "';\">");
            sbb.AppendLine("<option " + (_sb == "1" ? "selected=\"selected\"" : "") + " value=\"1\">Best Sellers</option>");
            sbb.AppendLine("<option " + (_sb == "2" ? "selected=\"selected\"" : "") + " value=\"2\">Product Name</option>");
            sbb.AppendLine("<option " + (_sb == "3" ? "selected=\"selected\"" : "") + " value=\"3\">Price - High to Low</option>");
            sbb.AppendLine("<option " + (_sb == "4" ? "selected=\"selected\"" : "") + " value=\"4\">Price - Low to High</option>");
            sbb.AppendLine("<option " + (_sb == "5" ? "selected=\"selected\"" : "") + " value=\"5\">Grade - High to Low</option>");
            sbb.AppendLine("<option " + (_sb == "6" ? "selected=\"selected\"" : "") + " value=\"6\">Grade - Low to High</option>");

            sbb.AppendLine("</select>");
            sbb.AppendLine("</div>");
            sbb.AppendLine("</div>");
        }
        //--
        sbc.AppendLine("<div id=\"pagResult\">");
        sbc.AppendLine("<div class=\"pagination\">");
        if (_pg >= 2)
        {
            sbc.AppendLine("<div id=\"pagArrowPreviuos\">");
            if (_pg == (_pgg * PageSize - 4))
            {
                sbc.AppendLine("<a href=\"result.aspx?sb=" + _sb + "&pgg=" + (_pgg - 1).ToString() + "&pg=" + (_pg - 1).ToString() + "&txtadv=" + TextFinder + "&findopt1=" + SubjSubCateId + "&findopt2=" + TeacSubCateId + "&findopt3=" + Grade + "&findopt4=" + PlatformId + "&findopt5=" + PubId + "&findopt6=" + CategoryGroup + "\">PREVIOUS</a>");
            }
            else
            {
                sbc.AppendLine("<a href=\"result.aspx?sb=" + _sb + "&pgg=" + (_pgg).ToString() + "&pg=" + (_pg - 1).ToString() + "&txtadv=" + TextFinder + "&findopt1=" + SubjSubCateId + "&findopt2=" + TeacSubCateId + "&findopt3=" + Grade + "&findopt4=" + PlatformId + "&findopt5=" + PubId + "&findopt6=" + CategoryGroup + "\">PREVIOUS</a>");
            }
            sbc.AppendLine("</div>");
        }
        sbc.AppendLine("<div class=\"pagIndex\">");
        sbc.AppendLine("<ul>");
        for (int i = (_pgg * PageSize - 4); i <= numpages; i++)
        {
            contrec++;
            if (i > ((_pgg * PageSize))) { break; }
            sbc.AppendLine("<li><a href=\"result.aspx?sb=" + _sb + "&pgg=" + (_pgg).ToString() + "&pg=" + i.ToString() + "&txtadv=" + TextFinder + "&findopt1=" + SubjSubCateId + "&findopt2=" + TeacSubCateId + "&findopt3=" + Grade + "&findopt4=" + PlatformId + "&findopt5=" + PubId + "&findopt6=" + CategoryGroup + "\">");
            if (_pg == i)
            {
                sbc.AppendLine("<span class=\"current\">" + i.ToString() + "</span>");
            }
            else { sbc.AppendLine("" + i.ToString() + ""); }
            sbc.AppendLine("</a></li>");

        }
        sbc.AppendLine("</ul>");
        sbc.AppendLine("</div>");
        if (numpages > _pg)
        {
            if (_pg == (_pgg * PageSize))
            {
                sbc.AppendLine("<div id=\"pagArrowNext\"><a href=\"result.aspx?sb=" + _sb + "&pgg=" + (_pgg + 1).ToString() + "&pg=" + (_pg + 1).ToString() + "&txtadv=" + TextFinder + "&findopt1=" + SubjSubCateId + "&findopt2=" + TeacSubCateId + "&findopt3=" + Grade + "&findopt4=" + PlatformId + "&findopt5=" + PubId + "&findopt6=" + CategoryGroup + "\">NEXT</a></div>");
            }
            else
            {
                sbc.AppendLine("<div id=\"pagArrowNext\"><a href=\"result.aspx?sb=" + _sb + "&pgg=" + (_pgg).ToString() + "&pg=" + (_pg + 1).ToString() + "&txtadv=" + TextFinder + "&findopt1=" + SubjSubCateId + "&findopt2=" + TeacSubCateId + "&findopt3=" + Grade + "&findopt4=" + PlatformId + "&findopt5=" + PubId + "&findopt6=" + CategoryGroup + "\">NEXT</a></div>");
            }
        }

        if (numpages > (_pgg * 5))
        {
            sbc.AppendLine("<div id=\"pagDivider\">|</div>");
            sbc.AppendLine("<div id=\"pagNext5\"><a href=\"result.aspx?sb=" + _sb + "&pgg=" + (_pgg + 1).ToString() + "&pg=" + ((_pgg * PageSize) + 1).ToString() + "&txtadv=" + TextFinder + "&findopt1=" + SubjSubCateId + "&findopt2=" + TeacSubCateId + "&findopt3=" + Grade + "&findopt4=" + PlatformId + "&findopt5=" + PubId + "&findopt6=" + CategoryGroup + "\">Next 5</a></div>");
        }
        sbc.AppendLine("</div>");
        sbc.AppendLine("</div>");
        sbc.AppendLine("</div>");

        PlaceHolder_resultControls_Head.Controls.Add(new LiteralControl(sba.ToString() + sbb.ToString() + sbc.ToString()));
        PlaceHolder_resultControls_Foot.Controls.Add(new LiteralControl(sba.ToString() + sbc.ToString()));

        sba = null;
        sbb = null;
        sbc = null;

    }
}
