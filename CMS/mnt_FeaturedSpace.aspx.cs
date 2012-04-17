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
using System.Drawing.Imaging;
using miniSitemap;
using System.Text.RegularExpressions;

static class RegexUtilFeaturedSpace
{
    static Regex _regex = new Regex(@"\b[w-]*\d+[w-]*\b");

    static public string MatchKey(string input)
    {
        Match myMatch = _regex.Match(input);
        string key = "";
        if (myMatch.Success)
        {
            key = myMatch.Groups[0].Value;
        }
        return key;
    }
}

public partial class mnt_FeaturedSpace : Featured
{
    private bool flag = false;
    private int contcat = 0;
    private int contSub = 0;
    private int catID = 0;
    private int position = 1;
    
    DataSet data = new DataSet();
    DataSet data2 = new DataSet();
    
    int contSpaces = 0;

    public bool has_landing_page = false;
    public string str_TinyMCE;
    public string linktopage = "";
    public string txtFrom, txtTo, ActualImage = "";
    public string strFolder = "Images";
    public string hidden_from_hour = "01";
    public string hidden_from_minute = "00";
    public string hidden_from_ampm = "AM";
    public string hidden_to_hour = "01";
    public string hidden_to_minute = "00";
    public string hidden_to_ampm = "AM";
    public string id_landing_page_global = ""; //to store the id of the landing page.
    public string add_edit_landing_page1 = "New ";
    public string landingPageTitle, landingPageContent, landingPageImage = "";

    //------------------------------------------------------------
    public string xtractNums(string str)
    {
        return RegexUtilFeaturedSpace.MatchKey(str);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Addins addins = new Addins();
        str_TinyMCE = addins.GettinyMCE();
        miniSitemap.CMS_mnt_SiteMap minism = (miniSitemap.CMS_mnt_SiteMap)(Page.LoadControl("mnt_SiteMap.ascx"));
        PlaceHolderMinisitemap.Controls.Add(minism);

        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        Session["CurrentPage"] = "mnt_FeaturedSpace.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }

        #region First Load
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        data = getAllFeature(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]));
        bool fila = false;
        sb.AppendLine("<table id='canBeSorted'  border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
        sb.AppendLine("<tr align=\"center\">");
        sb.AppendLine("<td width=\"30\" height=\"40\"class=\"class_LineaVTabla\">Position</td>");
        sb.AppendLine("<td width=\"46\" height=\"40\"class=\"class_LineaVTabla\">Name</td>");
        sb.AppendLine("<td width=\"153\" class=\"class_LineaVTabla\">From</td>");
        sb.AppendLine("<td width=\"213\" class=\"class_LineaVTabla\">To</td>");
        sb.AppendLine("<td width=\"65\" align=\"center\">&nbsp;</td>");
        sb.AppendLine("<td width=\"52\" align=\"center\">&nbsp;</td></tr>");
        string active = "";
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (Convert.ToBoolean(row["FeatActive"]) == true)
                {
                    active = "checked";
                }
                else { 
                    active = ""; 
                }
                if (fila == true)
                { 
                    sb.AppendLine("<tr onMouseOver='this.style.cursor=&#39;move&#39;' style='cursor: move; '>"); 
                }
                else { 
                    sb.AppendLine("<tr class=\"fila\" onMouseOver='this.style.cursor=&#39;move&#39;' style='cursor: move;'>"); 
                }
                sb.AppendLine("<td align=\"center\"class=\"class_LineaVTabla\"><input type=\"text\" style=\"background-color:transparent; border:0px;width:15px\" value=\"" + row["FeatOrdPos"] + "\" name=\"txtCaIt" + row["FeatOrdPos"] + "\" id=\"txtCaIt" + row["FeatOrdPos"] + "\"  /><input id=\"idTheater" + position + "\" value=\"" + row["FeatId"] + "\" name=\"idTheater" + position + "\" type=\"hidden\" /></td>");
                sb.AppendLine("<td align=\"center\"class=\"class_LineaVTabla\">" + row["FeatTitle"].ToString() + "</td>");
                sb.AppendLine("<td align=\"left\" class=\"class_LineaVTabla\">" + row["FeatFrom"].ToString() + " </td> ");
                sb.AppendLine("<td align=\"left\" class=\"class_LineaVTabla\">" + row["FeatTo"].ToString() + "</td> ");
                sb.AppendLine("<td align=\"center\" class=\"class_LineaVTabla\"><a href='mnt_FeaturedSpace.aspx?FeatureId=" + row["FeatId"] + "'  class=\"enlace\"><img src=\"images/btn_Edit.png\" border=\"0\" />Edit</a></td> ");
                sb.AppendLine("<td align=\"left\"><a href='mnt_FeaturedSpace.aspx?DeleteId=" + row["FeatId"] + "&acc=delete&press=send'  class=\"enlace\"><img src=\"images/btn_delete.png\" border=\"0\"/>&nbsp;Delete</a></td> ");
                sb.AppendLine("<td width=\"1\" align=\"left\">&nbsp;</td></tr>");
                if (fila == true)
                { 
                    fila = false; 
                }
                else
                { 
                    fila = true; 
                }
                contcat++;
                contSpaces++;
                position++;
            }
        }
        sb.AppendLine("</TABLE>");
        sb.AppendLine("<input name=\"csub\" type=\"hidden\" value=\""+contcat+"\" />");
        if ((contSpaces >= 0)&& (contSpaces<5))
        {
            div_new.Visible = true;
        }
        div_featureSpace.InnerHtml = sb.ToString();
        #endregion
        #region New feature Space
        if (Request["NewFS"] != null)
        {
            div_dates.Visible = true;
        }
        #endregion
        #region Edit 
        if (Request["FeatureId"] != null)
        {
            editItem(Convert.ToInt32(Request["FeatureID"]));
            flag = true;
            load_landing_page();
        }
        #endregion

        if(Request["DeleteId"]!=null) 
        {
            deleteFeature(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]), Convert.ToInt32(Request["DeleteId"]));
            Response.Redirect("mnt_FeaturedSpace.aspx");
        }
        #region Edit Positions
        if (Request["UpdPosition"] != null)
        {
            for (int i = 1; i < position; i++)
            {
                Upd_TheaterPositions(Convert.ToInt32(Request["idTheater"+i]), Convert.ToInt32(Request["txtCaIt"+i]));
            }
            Response.Redirect("mnt_FeaturedSpace.aspx");
        }
        #endregion 
    }
    #region  Button Methods

    protected void load_landing_page()
    {
        MainContentGeneric generic_X = new MainContentGeneric();
        string id_lp = xtractNums(linktopage);
        id_landing_page_global = id_lp;
        if (id_lp != "")
        {
            int id_landing_page = Convert.ToInt32(id_lp);
            data = new DataSet();
            data = generic_X.Get_GenericX_By_Id(id_landing_page);
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    landingPageTitle = row["GeneXTitle"].ToString();
                    landingPageContent = row["GeneXContent"].ToString();
                    landingPageImage = row["GeneXImage"].ToString();
                }
                add_edit_landing_page1 = "Edit ";
                has_landing_page = true;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_FeaturedSpace.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        hidden_from_hour = Convert.ToString(Request["hidden_from_hour"]);
        hidden_from_minute = Convert.ToString(Request["hidden_from_minute"]);
        hidden_from_ampm = Convert.ToString(Request["hidden_from_ampm"]);
        hidden_to_hour = Convert.ToString(Request["hidden_to_hour"]);
        hidden_to_minute = Convert.ToString(Request["hidden_to_minute"]);
        hidden_to_ampm = Convert.ToString(Request["hidden_to_ampm"]);
        Addins addin = new Addins();
        linktopage = addin.encodingAmp(Convert.ToString(Request["txt_link"]));

        btn_SaveLand_Click();

        string image_path = "";
        if (Convert.ToString(Request["rb_image_type"]) == "upload")
        {
            image_path = hidePath.Value;
        }
        else
        {
            if (Convert.ToString(Request["rb_image_type"]) == "url")
            {
                image_path = Convert.ToString(Request["txt_image_url"]);
            }
        }

        if ((image_path != "") && (Convert.ToString(Request["txtFrom"]) != "") && (Convert.ToString(Request["txtTo"]) != "") && (flag == false))
        {
            string from = "" + Convert.ToString(Request["txtFrom"]) + " " + hidden_from_hour + ":" + hidden_from_minute + ":00 " + hidden_from_ampm + " ";
            string To = "" + Convert.ToString(Request["txtTo"]) + " " + hidden_to_hour + ":" + hidden_to_minute + ":00 " + hidden_to_ampm + "";
            DateTime dateFrom = Convert.ToDateTime(from);
            DateTime dateto = Convert.ToDateTime(To);
            addFeature(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), position, txt_Title.Text, image_path, txt_Alt.Text, linktopage, dateFrom, dateto, true, true);
            Response.Redirect("mnt_FeaturedSpace.aspx");
        }
        else
        {
            if ((image_path != "") && (Convert.ToString(Request["txtFrom"]) != "") && (Convert.ToString(Request["txtTo"]) != "") && (flag == true))
            {
                string from = "" + Convert.ToString(Request["txtFrom"]) + " " + hidden_from_hour + ":" + hidden_from_minute + ":00 " + hidden_from_ampm + " ";
                string To = "" + Convert.ToString(Request["txtTo"]) + " " + hidden_to_hour + ":" + hidden_to_minute + ":00 " + hidden_to_ampm + "";
                DateTime dateFrom = Convert.ToDateTime(from);
                DateTime dateto = Convert.ToDateTime(To);
                updatefeature(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToInt32(Request["FeatureId"]), txt_Title.Text, image_path, txt_Alt.Text, dateFrom, dateto, true, true, linktopage);
                Response.Redirect("mnt_FeaturedSpace.aspx");
            }
            lbError.Visible = true;
        }
    }
    #endregion

    protected void editItem(int id)
    {
        DataSet data3 = getFeatureById(id);
        DateTime dtiFrom, dtito;
        DateTime dtiTo;
        bool iischeck = false;
        bool ischeck; 
        string ffrom = "";
        string fto = "";
        string maintTitle = "";
        
        foreach (DataTable table2 in data3.Tables)
        {
            foreach (DataRow row2 in table2.Rows)
            {
                ffrom = Convert.ToString(row2["FeatFrom"]);
                fto = Convert.ToString(row2["FeatTo"]);
                iischeck = Convert.ToBoolean(row2["FeatActive"]);
                ActualImage = Convert.ToString(row2["FeatFile"]);
                if (btnSave.Text != ".")
                {
                    txt_Alt.Text = Convert.ToString(row2["FeatAlt"]);
                }
                linktopage = Convert.ToString(row2["FeatLink"]);
                maintTitle = Convert.ToString(row2["FeatTitle"]);
            }
        }
        if (ActualImage == "")
        { 
            show_image.Visible = false; 
        }
        if (btnSave.Text != ".")
        {
            lbInfo.Text = ActualImage;
            hidePath.Value = ActualImage;
            txt_Title.Text = maintTitle;
        }
        ischeck = iischeck;
        dtiFrom = Convert.ToDateTime(ffrom);
        dtiTo = Convert.ToDateTime(fto);
        lbInfo.Visible = false;
        txtFrom = dtiFrom.Date.ToShortDateString();
        ddlHourFrom.Text = dtiFrom.ToString("hh");
        ddlMinFrom.Text = dtiFrom.ToString("mm");
        ddlAMPMFrom.Text = dtiFrom.ToString("tt");
        //Get the values for the "to" date.
        txtTo = dtiTo.Date.ToShortDateString();
        ddlHourTo.Text = dtiTo.ToString("hh");
        ddlMinTo.Text = dtiTo.ToString("mm");
        ddlAMPMTo.Text = dtiTo.ToString("tt");       
        btnSave.Text = ".";
        div_dates.Visible = true;
    }
    //--------------------------------------------

    protected string convertToAmPm(int toConvert)
    {
        if (toConvert > 12)
        {
            toConvert = toConvert - 12;
        }
        return Convert.ToString(toConvert);
    }
    //--------------------------------------------
    protected void btn_Upload_Click(object sender, EventArgs e)
    {
        #region upload
        String UploadedFile = FUimage.PostedFile.FileName;
        int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
        string addres = "";

        //to retrieve only Filename from the complete path
        String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
        String lowuploadimage = UploadedFileName.ToLower();
        if (!(lowuploadimage.Contains(".jpg") || lowuploadimage.Contains(".png") || lowuploadimage.Contains(".swf") || lowuploadimage.Contains(".gif")))
        {
            return;
        }
        addres = UploadedFileName.ToString();// Request.PhysicalApplicationPath + "CMS\\" + strFolder + "\\" + UploadedFileName;
        FUimage.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
        imageUploaded.Src = "..\\" + strFolder + "\\" + UploadedFileName;
        hidePath.Value = addres;
        #endregion
        ActualImage = addres;
        show_image.Visible = true;
    }

    public bool ThumbnailCallback()
    {
        return false;
    }
    protected void btn_New_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_FeaturedSpace.aspx?NewFS=" + true);
    }

    protected void btn_SaveLand_Click()
    {
        landingPageContent = Convert.ToString(Request["elm1"]);
        landingPageTitle = Convert.ToString(Request["txtlandTitle"]);
        if (Convert.ToString(Request["radioLinkType"]) == "landing")
        {
            int genId = 0;
            #region upload
            String UploadedFile = UploadLanding.PostedFile.FileName;
            int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
            string addres = landingPageImage;
            if(Convert.ToString(Request["rb_image_typeLanding"])=="upload"){
                if (Convert.ToString(UploadedFile) != "")
                {
                    //to retrieve only Filename from the complete path
                    String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
                    String lowuploadimage = UploadedFileName.ToLower();
                    addres = Convert.ToString(UploadedFileName); 
                    UploadLanding.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
                }
            }
            else if (Convert.ToString(Request["rb_image_typeLanding"]) == "url")
            {
                addres = Convert.ToString(Request["txt_image_url_landing"]);
            }
            #endregion
            genId = addLandPage(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), landingPageTitle, landingPageContent, addres, 1, 5);//5 recongnized as genD Theater
            linktopage = "Generic_x.aspx?LandingId=" + Convert.ToInt32(genId);
        }
    }
}