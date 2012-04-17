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
using uc_Right;
using System.Web.Services;
using System.Collections.Generic;
public partial class product : System.Web.UI.Page//SiteProduct
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    public int AdditionalLic = 0;
    public string strFolder = SiteConstants.imagesPath;
    public string strFolderTb = SiteConstants.imagesPathTb;
    public string strFolderBig = SiteConstants.imagesPathBig;
    public string strFolderAdd = SiteConstants.imagesPathAdditional;
    private int TitleId = 0;
    private string sTitleId;
    private string sSkuId;
    //--
    private int _numRelatedProduct = 0;
    public int _numRelatedSlide = 0;
    public ArrayList _ListOfSkus = new ArrayList();
    //-
    //-- Properties
    private string _plataformMac = "Mac";
    private string _plataformWin = "Windows";
    private string _title = "";
    private string _version = "";
    private string _sku = "";
    private string _skudesc = "";
    private string _pubname = "";
    private string _grades = "";
    private string _plat_win_flag = "";
    private string _plat_mac_flag = "";
    private string _srp = "";
    private string _er_price = "";
    private string _yousave = "";
    private string _imagetn = "";
    private string _long_description = "";
    private string _short_description = "";
    private string _defaultsku = "";
    private string _pubid = "";
    private string _shipping_Weight_lbs = "";
    private string _shipping_Bulk_Flag = "";
    private string _shipping_Weight_Avg_lbs = "";
    private string _student_Pricing_Flag = "";
    private string _download_Flag = "";
    private string _trial_Flag = "";
    private string _demo_Flag = "";
    private string _new_Flag = "";
    private string _price_Rule = "";
    //-
    public string productId = "";
    public string productTrialValidated = "";
    public string productNameToCart = "";

    //-
    public string lastPage = "";
    //--
    protected string _Mac
    {
        get { return _plataformMac; }
        set { _plataformMac = value; }
    }
    protected string _Win
    {
        get { return _plataformWin; }
        set { _plataformWin = value; }
    }

    protected string _TitleId
    {
        get { return sTitleId; }
        set { sTitleId = value; }
    }
    protected string _Title
    {
        get { return _title; }
        set { _title = value; }
    }
    protected string _Version
    {
        get { return _version; }
        set { _version = value; }
    }
    protected string _Sku
    {
        get { return _sku; }
        set { _sku = value; }
    }
    protected string _Skudesc
    {
        get { return _skudesc; }
        set { _skudesc = value; }
    }
    protected string _Pubname
    {
        get { return _pubname; }
        set { _pubname = value; }
    }
    protected string _Grades
    {
        get { return _grades; }
        set { _grades = value; }
    }
    protected string _Plat_win_flag
    {
        get { return _plat_win_flag; }
        set { _plat_win_flag = value; }
    }
    protected string _Plat_mac_flag
    {
        get { return _plat_mac_flag; }
        set { _plat_mac_flag = value; }
    }
    protected string _Srp
    {
        get { return _srp; }
        set { _srp = value; }
    }
    protected string _Er_price
    {
        get { return _er_price; }
        set { _er_price = value; }
    }
    protected string _Yousave
    {
        get { return _yousave; }
        set { _yousave = value; }
    }
    protected string _Imagetn
    {
        get { return _imagetn; }
        set { _imagetn = value; }
    }
    protected string _Long_description
    {
        get { return _long_description; }
        set { _long_description = value; }
    }
    protected string _Short_description
    {
        get { return _short_description; }
        set { _short_description = value; }
    }
    protected string _Defaultsku
    {
        get { return _defaultsku; }
        set { _defaultsku = value; }
    }
    protected string _Pubid
    {
        get { return _pubid; }
        set { _pubid = value; }
    }

    protected string _Shipping_Weight_lbs
    {
        get { return _shipping_Weight_lbs; }
        set { _shipping_Weight_lbs = value; }
    }
    protected string _Shipping_Bulk_Flag
    {
        get { return _shipping_Bulk_Flag; }
        set { _shipping_Bulk_Flag = value; }
    }
    protected string _Shipping_Weight_Avg_lbs
    {
        get { return _shipping_Weight_Avg_lbs; }
        set { _shipping_Weight_Avg_lbs = value; }
    }
    protected string _Student_Pricing_Flag
    {
        get { return _student_Pricing_Flag; }
        set { _student_Pricing_Flag = value; }
    }
    protected string _Price_Rule
    {
        get { return _price_Rule; }
        set { _price_Rule = value; }
    }
    protected string _Download_Flag
    {
        get { return _download_Flag; }
        set { _download_Flag = value; }
    }
    protected string _Trial_Flag
    {
        get { return _trial_Flag; }
        set { _trial_Flag = value; }
    }
    protected string _Demo_Flag
    {
        get { return _demo_Flag; }
        set { _demo_Flag = value; }
    }
    protected string _New_Flag
    {
        get { return _new_Flag; }
        set { _new_Flag = value; }
    }

    //--
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
            CurrentChilPage = "product.aspx";
        //--
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Cleaner c = new Cleaner();
            Response.Redirect(c.cleanURL(_title) + "--" + _TitleId + ".phtml");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();
        Session["CurrentChilPage"] = "product.aspx";
        if (Request["p"] != null)
        {
            productId = Request["p"].ToString();
        }
        //-- BreadCrumb
        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        uc_RelatedProducts ucRelatedProduct = (uc_RelatedProducts)(Page.LoadControl("uc_RelatedProducts.ascx"));
        try
        {
            ucRelatedProduct.intProductId = Convert.ToInt32(Request["p"]);
        }
        catch (Exception ex)
        {
            Response.Redirect("500error.html");
        }
        PlaceHolder_uc_RelatedProducts.Controls.Add(ucRelatedProduct);

        sTitleId = Request["p"];
        sSkuId = Request["s"];
        if (sTitleId != null)
        {
            TitleId = Convert.ToInt32(sTitleId);
            if (sSkuId != null)
                sSkuId = sSkuId;
            else
                sSkuId = "";
            Get_Title_by_Id();
            Get_Title_Images();
            Get_SitePrice_by_Title();
            Get_Title_RelatedProducts();
            Get_Title_SysReq_OpeSys();
            Get_Title_Review();
            Get_Title_Resources();
            Get_Title_Funding();
            Get_Title_AdditionalInfo();
            if (productId.Equals(Resources.Resource.TorchProductId))
            { 
                Get_Torch_Skus();
            }

        }
        else
        {
            Response.Write("Product No found");
            Response.End();
        }
        //-- Load BreadCrumb
        Product_MasterPage main = (Product_MasterPage)Page.Master;
        if (Request.UrlReferrer != null)
        {
            if (Request.UrlReferrer.ToString().IndexOf("result.aspx") != -1)
            {
                main._site_breadLink += "<li class=\"last\"><a href=\"#\" onClick=\"history.back(); return false;\">" + "Results" + "</a></li>";
            }
        }
        main._site_breadLink += "<li class=\"last\"><a href=\"#\" onClick=\"return false;\"><strong>" + "Product Information" + "</strong></a></li>";

        //--

        //////this.URLRedirect();
    }

    private void Get_Title_by_Id()
    {
        SiteProduct siteproduct = new SiteProduct();
        DataSet dsProduct = new DataSet();
        //---
        int _ds_nr = 0;
        string FlagSAP = "";
        dsProduct = siteproduct.Get_Title_by_Id(TitleId, sSkuId);
        if (dsProduct != null)
        {
            _ds_nr = dsProduct.Tables[0].Rows.Count;
        }
        else {
            _ds_nr = 0;
        }
        if (_ds_nr <= 0) Response.Redirect("500error.html");
        _Yousave = "0";// is = but need to convert
        foreach (DataTable table in dsProduct.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                _Title = row["title"].ToString();
                productNameToCart = row["title"].ToString();
                Product_MasterPage main = (Product_MasterPage)Page.Master;
                main.pageTitleBar = _Title + " - " + main.pageTitleBar;
                _Version = row["Version"].ToString();
                _Sku = row["Sku"].ToString().Trim();
                _Skudesc = row["Skudesc"].ToString();
                _Pubname = row["Pubname"].ToString();
                _Grades = row["Grades"].ToString();
                _Plat_win_flag = row["Plat_win_flag"].ToString();
                _Plat_mac_flag = row["Plat_mac_flag"].ToString();
                _Srp = String.Format("{0:#,0.00}", row["Srp"]);
                //_Er_price = row["Er_price"].ToString();
                _Er_price = String.Format("{0:#,0.00}", row["Er_price"]);
                //_Yousave = row["Yousave"].ToString();
                _Yousave = String.Format("{0:#,0.00}", row["Yousave"]);
                _Imagetn = row["Imagetn"].ToString();
                _Long_description = row["Long_description"].ToString();
                _Short_description = row["Short_description"].ToString();
                _Defaultsku = row["Defaultsku"].ToString();
                _Pubid = row["Pubid"].ToString();
                _Shipping_Weight_lbs = row["Shipping_Weight_lbs"].ToString();
                _Shipping_Bulk_Flag = row["Shipping_Bulk_Flag"].ToString();
                _Shipping_Weight_Avg_lbs = row["Shipping_Weight_Avg_lbs"].ToString();
                _Student_Pricing_Flag = row["Student_Pricing_Flag"].ToString();
                _Price_Rule = row["Price_Rule"].ToString();
	         FlagSAP = row["Student_Pricing_Flag"].ToString();
                _Download_Flag = row["Download_Flag"].ToString();
                _Trial_Flag = row["Trial_Flag"].ToString();
                _Demo_Flag = row["Demo_Flag"].ToString();
                _New_Flag = row["New_Flag"].ToString();
                productTrialValidated = row["TitleTrial"].ToString();
            }
        }
	if (FlagSAP != "0")
        {
            Session["SAPER"] = true;
        }
        dsProduct = null;

    }

    private void Get_SitePrice_by_Title()
    {
        Addins addins = new Addins();
        SiteProduct siteproduct = new SiteProduct();
        DataSet dsSitePrice = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //---
        dsSitePrice = siteproduct.Get_Title_LicenseOptions(TitleId);
        if (dsSitePrice.Tables[0].Rows.Count == 0)
        {
            LicOpt.Visible = false;
            opt_lic.Visible = false;
        }

        foreach (DataTable table in dsSitePrice.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<div class=\"formAccordion cellColorGray\">");
                sb.AppendLine("<div class=\"formSKU\" alt=\"" + row["SKU"].ToString() + "\">" + CutSKU(row["SKU"].ToString()) + "</div>");
                sb.AppendLine("<div class=\"formDesc1\">" + row["SKUDesc"].ToString() + "</div>");

                if ((row["plat_win_flag"].ToString() == "1") && (row["plat_mac_flag"].ToString() == "1"))
                { sb.AppendLine("<div class=\"formQty1\">Mac/Win</div>"); }
                else
                {
                    sb.AppendLine("<div class=\"formQty1\">" + (row["plat_win_flag"].ToString() == "1" ? "Win" : "") + "" + (row["plat_mac_flag"].ToString() == "1" ? "Mac" : "") + "</div>");
                }
                sb.AppendLine("<div class=\"formVersion\" title=\"" +  row["Version"].ToString() + "\">" + CutSKU2(row["Version"].ToString()) + "</div>");
                sb.AppendLine("<div class=\"formUnit\">$" + row["ER_Price"].ToString() + "</div>");
                sb.AppendLine("<div class=\"formTotal\"><a href=\"product.aspx?p=" + _TitleId + "&s=" + row["SKU"].ToString() + "\"><img src=\"images/selectBtn.jpg\"/></a></div>");
                sb.AppendLine("</div>");
                AdditionalLic++;
            }
        }
        PlaceHolder_LicOptions.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
    }

    private void Get_Title_RelatedProducts()
    {

        SiteProduct siteproduct = new SiteProduct();
        DataSet dsSitePrice = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //---
        dsSitePrice = siteproduct.Get_Title_RelatedProducts(TitleId);
        _numRelatedProduct = dsSitePrice.Tables[0].Rows.Count;
        if (_numRelatedProduct == 0)
        {
            RelPro.Visible = false;
            opt_sim.Visible = false;
        }
        else
        {
            if (_numRelatedProduct > 1 && _numRelatedProduct < 5)
            {
                _numRelatedSlide = _numRelatedProduct - 1;
            }
            else
            {
                if (_numRelatedProduct == 1)
                    _numRelatedSlide = 1;
                else
                    _numRelatedSlide = 4;
            }
        }
        foreach (DataTable table in dsSitePrice.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
sb.AppendLine("<div class=\"SlideItMoo_element\"><a href=\"product.aspx?p=" + row["titleid"].ToString() + "\" style=\"background-color:#FFF;\"><img src=\"" + strFolder + row["imagetn"].ToString() + "\" title=\"" + row["pubname"].ToString() + " : " + row["title"].ToString() + "\" height=\"100px\" /></a><p>" + row["title"].ToString() + "</p></div>");
            }
        }
        PlaceHolser_Slide.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
    }

    private void Get_Title_SysReq_OpeSys()
    {

        SiteProduct siteproduct = new SiteProduct();
        DataSet dsSitePrice = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //---
        dsSitePrice = siteproduct.Get_Title_SysReq_OpeSys(TitleId);
        if (dsSitePrice.Tables[0].Rows.Count == 0)
        {
            SysReq1.Visible = false;
            SysReq2.Visible = false;
            opt_sys.Visible = false;
        }
        foreach (DataTable table in dsSitePrice.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<p>" + row["comptypename"].ToString() + "                  " + row["sysreqname"].ToString() + "</p>");
            }
        }
        //PlaceHolder_SysReqCPU.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
        dsSitePrice = siteproduct.Get_Title_SysReq_MemCPU(TitleId);

        foreach (DataTable table in dsSitePrice.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<p>CPU PC:" + row["PCCpu"].ToString() + "</p>");
                sb.AppendLine("<p>Memory PC:" + row["PCMemory"].ToString() + "</p>");
                sb.AppendLine("<p>CPU Mac:" + row["MacCpu"].ToString() + "</p>");
                sb.AppendLine("<p>Memory Mac:" + row["MacMemory"].ToString() + "</p>");
            }
        }
        PlaceHolder_SysReqCPU.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
    }

    private void Get_Title_Images()
    {
        string MasterImagePath;
        string BigImagePath;
        SiteProduct siteproduct = new SiteProduct();
        DataSet dsSitePrice = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //---
        dsSitePrice = siteproduct.Get_Title_Images(TitleId);
        int contrec = 1;
        MasterImagePath = strFolder + _Imagetn;
        BigImagePath = strFolderBig + _Imagetn; 

        sb.AppendLine("<div id=\"lock" + contrec.ToString() + "\" style=\"width: 56px; height: 56px; float: left; padding: 0px; ");
        sb.AppendLine("border: solid 2px #333; margin: 4px; float: left; overflow: hidden;\" onclick=\"selectlock(this.id);\">");
        //sb.AppendLine("<a href=\"#\" style=\"text-decoration: none;\">");
        sb.AppendLine("<a href='#' onclick=\"changeMasterImage('" + MasterImagePath + "','" + BigImagePath + "');\" style=\"text-decoration: none;\">");

        sb.AppendLine("    <img id=\"images1\" style=\"width: 56px; height: 56px;\" src=\"" + strFolderTb + "tn_" + _Imagetn + " \"/>"); //onload=\"getDim(document.getElementById('lock" + contrec.ToString() + "'),this)\"
        sb.AppendLine("</a>");
        sb.AppendLine("</div>");
        if (dsSitePrice != null){
	        foreach (DataTable table in dsSitePrice.Tables)
	        {
	            foreach (DataRow row in table.Rows)
	            {
	                contrec++;
	                sb.AppendLine("<div id=\"lock" + contrec.ToString() + "\" style=\"width: 56px; height: 56px;");
	                sb.AppendLine("border: solid 2px #333; margin: 4px; float: left; overflow: hidden;\" onclick=\"selectlock(this.id);\">");
	                MasterImagePath = strFolderAdd + row["titleresource"].ToString();
	                BigImagePath = strFolderAdd + row["titleresource"].ToString();
	
	                sb.AppendLine("<a href='#' onclick=\"changeMasterImage('" + MasterImagePath + "','" + BigImagePath + "');\" style=\"text-decoration: none;\">");
	                sb.AppendLine("<img id=\"images\" style=\"width: 56px; height: 56px;\" src=\"" + strFolderAdd + "" + row["titleresource"].ToString() + "\" >"); //onload=\"getDim(document.getElementById('boxContImage'),this)\"/
	                sb.AppendLine("</a>");
	                sb.AppendLine("</div>");
	            }
	        }
        }
        PlaceHolder_ProImages.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
    }

    private void Get_Title_Review()
    {

        SiteProduct siteproduct = new SiteProduct();
        DataSet dsSitePrice = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //---
        dsSitePrice = siteproduct.Get_Title_Review(TitleId);
        if (dsSitePrice.Tables[0].Rows.Count == 0)
        {
            ProRev1.Visible = false;
            opt_pro.Visible = false;

        }
        foreach (DataTable table in dsSitePrice.Tables)
        {
            foreach (DataRow row in table.Rows)
            {

                sb.AppendLine("<p>" + row["TitleReview"].ToString() + "</p>");


            }
        }
        PlaceHolder_Review.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
    }

    private void Get_Title_Funding()
    {

        SiteProduct siteproduct = new SiteProduct();
        DataSet dsSitePrice = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //---
        dsSitePrice = siteproduct.Get_Title_Funding(TitleId);
        if (dsSitePrice.Tables[0].Rows.Count == 0)
        {
            Funding1.Visible = false;
            Funding2.Visible = false;
            opt_fun.Visible = false;

        }
        string strcategory = "";
        string strNcategory = "";
        foreach (DataTable table in dsSitePrice.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
            	strcategory = row["Category"].ToString();
                if (strcategory != strNcategory) { sb.AppendLine("<p><b><i> "+ row["Category"].ToString() + "</i></b></p>");}
                sb.AppendLine("<p style=\"margin-left:10px;\"><i>" + row["SubCategory"].ToString() + "</i><br/><br/>" + row["SubCategoryDesc"].ToString() + "</p>");
                strNcategory = strcategory;
                //sb.AppendLine("<p><b><i>" + row["SubCategory"].ToString() + "</i></b><br/><br/>" + row["SubCategoryDesc"].ToString() + "</p>");
            }
        }
        PlaceHolder_funding.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
    }

    private void Get_Title_AdditionalInfo()
    {

        SiteProduct siteproduct = new SiteProduct();
        DataSet dsSitePrice = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //---
        bool showpanel = false;
        //---
        dsSitePrice = siteproduct.Get_Title_AdditionalInfo(TitleId);
        if (dsSitePrice.Tables[0].Rows.Count == 0)
        {
            AdditionalInfo1.Visible = false;
            AdditionalInfo2.Visible = false;
            opt_add.Visible = false;

        }
        foreach (DataTable table in dsSitePrice.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row["AddPurchReq"].ToString().Trim() != "")
                {
                    sb.AppendLine("<p><b><i>Additional Purchasing Requirements</i></b><br/><br/>" + row["AddPurchReq"].ToString() + "</p>");
                    showpanel = true;
                }

                   //sb.AppendLine("<p>" + row["SubCategory"].ToString() + "<br/>" + row["SubCategoryDesc"].ToString() + "</p>");
                if (row["WhatCustRec"].ToString().Trim() != "")
                {
                    sb.AppendLine("<p><b><i>What the Customer Receives</i></b><br/><br/>" + row["WhatCustRec"].ToString() + "</p>");
                    showpanel = true;
                }

                if (row["AddSysReq"].ToString().Trim() != "")
                {
                    sb.AppendLine("<p><b><i>Additional System Requirements</i></b><br/><br/>" + row["AddSysReq"].ToString() + "</p>");
                    showpanel = true;
                }
            }
            AdditionalInfo1.Visible = showpanel;
            AdditionalInfo2.Visible = showpanel;
            opt_add.Visible = showpanel;
        }
        PlaceHolder_AdditionalInfo.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
    }

    private void Get_Title_Resources()
    {

        SiteProduct siteproduct = new SiteProduct();
        DataSet dsSitePrice = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //---exec [Get_Title_Resources] 1640
        dsSitePrice = siteproduct.Get_Title_Resources(TitleId);
        if (dsSitePrice.Tables[0].Rows.Count == 0)
        {
            ProRes1.Visible = false;
            ProRes2.Visible = false;
            opt_res.Visible = false;

        }
        foreach (DataTable table in dsSitePrice.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<p>");
                if (row["UrlFlag"].ToString() == "1")
                {
                    sb.AppendLine("<a href=\"" + row["titleresourceLoc"].ToString() + "\"target=\"blank\">" + row["titleresourcetypeName"].ToString() + "</a>");
                }
                else
                {
                    sb.AppendLine("<a href=\"http://external.edresources.com/ProductDocs/" + row["titleresourceLoc"].ToString() + "\"target=\"blank\"> " + row["titleresourcetypeName"].ToString() + "</a>");
                }
                sb.AppendLine("</p>");

            }
        }
        PlaceHolder_ProRes.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
    }
    private string CutSKU(string description)
    {
        String strDesc = description.Trim();
        if (strDesc.Length >= 9)
        {
            strDesc = strDesc.Substring(0, 7) + "...";
        }
        return strDesc;
    }

    private string CutSKU2(string description)
    {
        String strDesc = description.Trim();
        if (strDesc.Length >= 18)
        {
            strDesc = strDesc.Substring(0, 18) + "...";
        }
        return strDesc;
    }

    private void Get_Torch_Skus()
    {
        SiteProduct siteproduct = new SiteProduct();
        DataSet dsTorchSku = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        dsTorchSku = siteproduct.Get_TorchPrices();
        List<TorchPricesSku> ListOfSkus = new List<TorchPricesSku>();
        foreach (DataTable table in dsTorchSku.Tables)
        {
            TorchPricesSku Sku;
            foreach (DataRow row in table.Rows)
            {
                Sku = new TorchPricesSku(row["subjectid"].ToString(), row["gradeid"].ToString(), row["SKU"].ToString(), row["price"].ToString());
                ListOfSkus.Add(Sku);
            }
           
        }
        _ListOfSkus.AddRange(ListOfSkus);
        string a = "";
           
    }


    public class TorchPricesSku
    {


        public string Subject;
        public string Grade;
        public string Sku;
        public string Price;

        public TorchPricesSku()
        {
            this.Subject = "";
            this.Grade = "";
            this.Sku = "";
            this.Price = "";
            
        }

        public TorchPricesSku(string subj,string grade,string sku,string price)
        {
            this.Subject = subj;
            this.Grade = grade;
            this.Sku = sku;
            this.Price = price;
        }
    }



    //private void Get_Trial_Product() { 
    //    SiteProduct siteproduct = new SiteProduct();
    //    DataSet dsSiteTrialDemo = new DataSet();
    //    dsSiteTrialDemo = siteproduct.Get_Site_Trials_Demos(_TitleId, 7);
    //    if (dsSiteTrialDemo.Tables[0].Rows.Count == 0)
    //    {
    //        //ProRev1.Visible = false;
    //    }
    //    foreach (DataTable table in dsSiteTrialDemo.Tables)
    //    {
    //        foreach (DataRow row in table.Rows)
    //        {
    //            sb.AppendLine("<p>" + row["TitleReview"].ToString() + "</p>");
    //        }
    //    }
    //}
    //private void Get_Demos_Product()
    //{
    //    SiteProduct siteproduct = new SiteProduct();
    //    DataSet dsSiteTrialDemo = new DataSet();
    //    dsSiteTrialDemo = siteproduct.Get_Site_Trials_Demos(_TitleId, 6);
    //    if (dsSiteTrialDemo.Tables[0].Rows.Count == 0)
    //    {
    //        //ProRev1.Visible = false;
    //    }
    //    foreach (DataTable table in dsSiteTrialDemo.Tables)
    //    {
    //        foreach (DataRow row in table.Rows)
    //        {
    //            sb.AppendLine("<p>" + row["TitleReview"].ToString() + "</p>");
    //        }
    //    }
    //}

}
