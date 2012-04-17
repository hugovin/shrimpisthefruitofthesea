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

public partial class printProduct : System.Web.UI.Page
{
    private string _contenido = "";
    private string strFolder = "";
    private string titleId = "";

    //-- Properties
    private string _plataformMac = "Mac";
    private string _plataformWin = "Windows";

    private string _title = "";
    private string _version = "";
    private string _sku = "";
    private string _pubid = "";
    private string _pubname, _long_description = "";
    private string _grades = "";
    private string _plat_win_flag = "";
    private string _plat_mac_flag = "";
    private string _srp = "";
    private string _er_price = "";
    private string _yousave = "";
    private string _imagetn = "";
    private string _trial_Flag = "";
    private string _demo_Flag = "";

    private string _short_description = "";

    //-
    string lastPage = "";
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
    protected string _Pubid
    {
        get { return _pubid; }
        set { _pubid = value; }
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

    protected string _Short_description
    {
        get { return _short_description; }
        set { _short_description = value; }
    }
    protected string _Imagetn
    {
        get { return _imagetn; }
        set { _imagetn = value; }
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
    protected string _Long_description
    {
        get { return _long_description; }
        set { _long_description = value; }
    }

    private void GetVars()
    {
        //---
        if (Session["SiteImagesPath"] != null)
            strFolder = Session["SiteImagesPath"].ToString();
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetVars();

        if (Request["contenido"] != null)
            _contenido = Request["contenido"];
        else
            _contenido = "";

        if (Request["p"] != null)
            titleId = Request["p"];
        else titleId = "";
        if (Request["p"] != null)
        {
            loadContenido();
            lodaImage();
            loadContact();
        }
        else
        {
            Response.Redirect("home.aspx");
        }

    }

    protected void loadContenido()
    {
        SiteProduct siteproduct = new SiteProduct();
        DataSet dsProduct = new DataSet();

        //---
        dsProduct = siteproduct.Get_Title_by_Id(Convert.ToInt32(titleId));
        _Yousave = "0";// is = but need to convert
        foreach (DataTable table in dsProduct.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                _Title = row["title"].ToString();
                _Version = row["Version"].ToString();
                _Sku = row["Sku"].ToString();
                _Pubid = row["Pubid"].ToString();
                _Pubname = row["Pubname"].ToString();
                _Grades = row["Grades"].ToString();
                _Plat_win_flag = row["Plat_win_flag"].ToString();
                _Plat_mac_flag = row["Plat_mac_flag"].ToString();
                //_Srp = row["Srp"].ToString();
                _Srp = String.Format("{0:#,0.00}", row["Srp"]);
                //_Er_price = row["Er_price"].ToString();
                _Er_price = String.Format("{0:#,0.00}", row["Er_price"]);
                //_Yousave = row["Yousave"].ToString();
                _Yousave = String.Format("{0:#,0.00}", row["Yousave"]);
                _Long_description = row["Long_description"].ToString();
                _Imagetn = row["Imagetn"].ToString();
                _Short_description = row["Short_description"].ToString();
                _Trial_Flag = row["Trial_Flag"].ToString();
                _Demo_Flag = row["Demo_Flag"].ToString();

                Load_SysReq();
                Load_Resources();
                Load_RelatedProducts();
 				Get_Title_Funding();
                Get_Title_AdditionalInfo();

            }
        }
        dsProduct = null;
    }

    private void Load_SysReq()
    {

        SiteProduct siteproduct = new SiteProduct();
        DataSet dsSitePrice = new DataSet();
        StringBuilder sb = new System.Text.StringBuilder();
        //---
        dsSitePrice = siteproduct.Get_Title_SysReq_OpeSys(Convert.ToInt32(titleId));
        if (dsSitePrice.Tables[0].Rows.Count == 0) SysReq.Visible = false;

        foreach (DataTable table in dsSitePrice.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<p>" + row["comptypename"].ToString() + "                 " + row["sysreqname"].ToString() + "</p>");
            }
        }
        PlaceHolder_SysReq.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
        siteproduct = null;
        sb = null;
    }
	private void Get_Title_AdditionalInfo()
    {

        SiteProduct siteproduct = new SiteProduct();
        DataSet dsSitePrice = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //---
        bool showpanel = false;
        //---
        dsSitePrice = siteproduct.Get_Title_AdditionalInfo(Convert.ToInt32(titleId));
       
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
        }
        PlaceHolder_Additional.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
    }
    private void Get_Title_Funding()
    {

        SiteProduct siteproduct = new SiteProduct();
        DataSet dsSitePrice = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //---
        dsSitePrice = siteproduct.Get_Title_Funding(Convert.ToInt32(titleId));
       
        string strcategory = "";
        string strNcategory = "";
        foreach (DataTable table in dsSitePrice.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                strcategory = row["Category"].ToString();
                if (strcategory != strNcategory) { sb.AppendLine("<p><b><i> " + row["Category"].ToString() + "</i></b></p>"); }
                sb.AppendLine("<p style=\"margin-left:10px;\"><i>" + row["SubCategory"].ToString() + "</i><br/><br/>" + row["SubCategoryDesc"].ToString() + "</p>");
                strNcategory = strcategory;
                //sb.AppendLine("<p><b><i>" + row["SubCategory"].ToString() + "</i></b><br/><br/>" + row["SubCategoryDesc"].ToString() + "</p>");
            }
        }
        PlaceHolder_Funding.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
    }
    
    private void Load_Resources()
    {

        SiteProduct siteproduct = new SiteProduct();
        DataSet dsSitePrice = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        dsSitePrice = siteproduct.Get_Title_Resources(Convert.ToInt32(titleId));
        if (dsSitePrice.Tables[0].Rows.Count == 0) Resources.Visible = false;

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
        PlaceHolder_Resources.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
        siteproduct = null;
        sb = null;
    }

    private void Load_RelatedProducts()
    {

        SiteProduct siteproduct = new SiteProduct();
        DataSet dsSitePrice = new DataSet();
        StringBuilder sb = new System.Text.StringBuilder();
        //---
        dsSitePrice = siteproduct.Get_Title_RelatedProducts(Convert.ToInt32(titleId));
        if (dsSitePrice.Tables[0].Rows.Count == 0) RelPro.Visible = false;
        foreach (DataTable table in dsSitePrice.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<div class=\"SlideItMoo_element\"><a href=\"product.aspx?p=" + row["titleid"].ToString() + "\" style=\"background-color:#FFF;\"><img src=\"" + strFolder + row["imagetn"].ToString() + "\" height=\"100px\" /></a><p>" + row["title"].ToString() + "</p></div>");
            }
        }
        PlaceHolser_Slide.Controls.Add(new LiteralControl(sb.ToString()));
        dsSitePrice = null;
    }

    private void lodaImage()
    {
        StringBuilder sb = new System.Text.StringBuilder();

        sb.AppendLine("<div id=\"boxContImage\"  style=\"width:200px; height:200px;\">");
        sb.AppendLine("<img id=\"images\" style=\"width: 200px; height:200px;\" src=\"" + strFolder + "/" + _Imagetn + "\" />"); //onload=\"getDim(document.getElementById('boxContImage'),this)\"
        sb.AppendLine("</div>");

        if (_Trial_Flag == "1") sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/printTrial.jpg\" />");
        if (_Demo_Flag == "1") sb.AppendLine("<img src=\"" + Global.globalSiteImagesPath + "/printDemo.jpg\" /> ");

        PlaceHolder_boxImage.Controls.Add(new LiteralControl(sb.ToString()));

        sb = null;
    }

    private void loadContact()
    {
        boxContactPrint boxContactPrint = (boxContactPrint)(Page.LoadControl("boxContactPrint.ascx"));
        //boxContact boxContact = (boxContact)(Page.LoadControl("boxContactPrint.ascx"));
        //PlaceHolder_boxContactPrint.Controls.Add(boxContactPrint);
        PlaceHolder_boxContact.Controls.Add(boxContactPrint);
    }
}