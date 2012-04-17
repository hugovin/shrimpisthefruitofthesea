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

static class RegexUtilTemplateD
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

public partial class mnt_generic_d : Gen_D
{
    public bool hasErrors, has_landing_page1 = false;
    public int cont, idLandingPage, PageGeneDPos = 0;
    public string str_TinyMCE;
    public string strFolder = "Images";
    public string GenedLink, landingPageContent, landingPageImage, landingPageTitle, linktopage1, PageContent, PageImage, PageTitle, Position = " ";
    public string radioType = "1";
    public string add_edit_landing_page1 = "New "; //Only purpose is write New or Edit in the visual layer.
    //Session vars
    bool sessAbout, sessNewTemplate = false;
    int sessContId, sessGeneDefaId, sessGeneType, sessSiteId = 0;
    string sessPageTitle = "";
    //End of session vars

    public string xtractNums(string str)
    {
        return RegexUtilTemplateD.MatchKey(str);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Session vars
        sessAbout = Convert.ToBoolean(Session["About"]);
        sessNewTemplate = Convert.ToBoolean(Session["NewPageTemplate"]);
        sessContId = Convert.ToInt32(Session["contId"]);
        sessGeneDefaId = Convert.ToInt32(Session["GeneDefaId"]);
        sessGeneType = Convert.ToInt32(Session["GeneType"]);
        sessSiteId = Convert.ToInt32(Session["siteId"]);
        //End of session vars
        Addins addins = new Addins();
        str_TinyMCE = addins.GettinyMCE();
        miniSitemap.CMS_mnt_SiteMap minism = (miniSitemap.CMS_mnt_SiteMap)(Page.LoadControl("mnt_SiteMap.ascx"));
        PlaceHolderMinisitemap.Controls.Add(minism);
        Session["CurrentPage"] = "generic_d.aspx";

        if (Request["GenericId"] != "" && Request["GenericId"] != null)
        {
            DataSet data = new DataSet();
            data = Get_Generic_Name(Convert.ToInt32(Request["GenericId"]));
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    sessPageTitle = Convert.ToString(row["GeneTitle"]);
                }
            }
        }
        else
        {
            sessPageTitle = Convert.ToString(Session["PageTitle"]);
        }
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }
        loadSections();
        if (Request["MGeneDId"] != null)
        {
            editPage();
        }
        if (Request["NewGend"] != null)
        {
            div_editNewgenD.Visible = true;
        }

        if (Request["DeleteId"]!=null)
        {
             deleteGenericDbyId(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Request["GeneId"]), Convert.ToInt32(Request["DeleteId"]));
             Response.Redirect("mnt_generic_d.aspx?GenericId=" + Request["GeneId"]);
        }
        if (Request["NewLanding"] != null)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            sb.AppendLine("Title:");
            sb.AppendLine("<input type=\"text\" name=\"txtlandTitle\"  /><br>");
            sb.AppendLine("Content:");
            sb.AppendLine("<textarea id=\"elm1\" name=\"elm1\" rows=\"20\" cols=\"20\"></textarea><br>");
            Session["NewlandD"] = true;
        }
    }

    protected void loadSections()
    {
        bool fila = true;
        DataSet data = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        data = Get_Generic_D_By_Type(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Request["GenericId"]));
        sb.AppendLine("<TABLE width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\">");
        sb.AppendLine("<tr><td width=\"47\" class=\"class_LineaVTabla\">&nbsp;</td>");
        sb.AppendLine("<td width=\"330\" class=\"class_LineaVTabla\">&nbsp;</td>");
        sb.AppendLine("<td width=\"47\" align=\"center\">&nbsp;</td>");
        sb.AppendLine("<td width=\"56\" align=\"center\">&nbsp;</td></tr>");
        sb.AppendLine("<tr class=\"class_LineaHTabla\">");
        sb.AppendLine("<td class=\"class_LineaVTabla\">Position</td>");
        sb.AppendLine("<td class=\"class_LineaVTabla\">Name</td>");
        sb.AppendLine("<td align=\"center\">&nbsp;</td>");
        sb.AppendLine("<td align=\"center\">&nbsp;</td></tr>");
        int nextposition = 1;
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (fila == true)
                { sb.AppendLine("<tr class=\"fila\"> "); }
                else { sb.AppendLine("<tr> "); }
                sb.AppendLine("<TD align=\"center\" class=\"class_LineaVTabla\">");
                sb.AppendLine("" + row["GeneDOrdPos"].ToString() + "");
                sb.AppendLine("</TD>");
                sb.AppendLine("<TD class=\"class_LineaVTabla\">");
                sb.AppendLine("" + row["GeneDTitle"].ToString() + "");
                sb.AppendLine("</TD>");
                sb.AppendLine("<TD align=\"center\">");
                sb.AppendLine("<a class=\"enlace\" href= mnt_generic_d.aspx?GenericId=" + Request["GenericId"] + "&MGeneDId=" + row["GeneDId"].ToString() + "&GeneId=" + Request["GenericId"] + "><img src=\"images/btn_Edit.png\" border=\"0\" />Edit</a></td>");
                sb.AppendLine("<TD>");
                sb.AppendLine("<a onclick=\"return confirm('Do you want to delete?');\" href= mnt_generic_d.aspx?DeleteId=" + row["GeneDId"].ToString() + "&GeneId=" + Request["GenericId"] + " ><img src=\"images/btn_delete.png\" border=\"0\">Delete</a></td>");
                sb.AppendLine("</TD></TR>");
                nextposition++;
                cont++;
                if (fila == true)
                { fila = false; }
                else
                { fila = true; }
            }
        }
        Session["nextpos"] = nextposition;
        Position = Convert.ToString(Session["nextpos"]);
        data.Dispose();
        sb.AppendLine("</TABLE>");
        if ((cont != 0) || (Convert.ToBoolean(Session["NewPageTemplate"]) == false))
        {
            div_Generid_information.Visible = true;
            div_Generic_D.InnerHtml = sb.ToString();
            if (Convert.ToInt32(Request["GenericId"]) == 0)
            {
                Session["newTemplate"] = true;
            }
        }
        else
        {
            div_Generid_information.Visible = false;
            div_editNewgenD.Visible = true;
        }
    }

    protected void editPage()
    {
        DataSet data = new DataSet();
        data = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
        data = get_GenericD_ById(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Request["MGeneDId"]));
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                PageTitle = Convert.ToString(row["GeneDTitle"]);
                PageContent = Convert.ToString(row["GeneDContent"]);
                lbimage.Visible = true;
                lbimage.Text = row["GeneDFile"].ToString();
                PageImage = row["GeneDFile"].ToString();
                GenedLink = Convert.ToString(row["GeneDLinkTitle"]);
                linktopage1 = row["GeneDlink"].ToString();
                Position = Convert.ToString(row["GeneDOrdPos"]);
                radioType = row["LinkTypeId"].ToString();
            }
        }
        div_editNewgenD.Visible = true;
        if (linktopage1 != null)
        {
            loadLandingPage();
        }
    }

    protected void loadLandingPage()
    {
        MainContentGeneric generic_X = new MainContentGeneric();
        DataSet data = new DataSet();
        string id_lp = "";
        id_lp = xtractNums(linktopage1);
        if (id_lp != "")
        {
            idLandingPage = Convert.ToInt32(id_lp);
            data = new DataSet();
            data = generic_X.Get_GenericX_By_Id(idLandingPage);
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    landingPageTitle = row["GeneXTitle"].ToString();
                    landingPageContent = row["GeneXContent"].ToString();
                    landingPageImage = row["GeneXImage"].ToString();
                    add_edit_landing_page1 = "Edit ";
                    has_landing_page1 = true;
                }
            }
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_generic_d.aspx?NewGend=true&GenericId=" + Request["GenericId"]);
    }
    
    protected void btn_Cancel_edit_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_generic_d.aspx?GenericId=" + Request["GenericId"]);
    }
    
    protected void btn_Save_new_Click(object sender, EventArgs e)
    {
        int tipo, contId = 0;
        int genericId = Convert.ToInt32(Request["GenericId"]);
        string imageTypeMain = Convert.ToString(Request["rb_image_type"]);

        tipo = Convert.ToInt32(Request["radiotipo"]);
        PageTitle = Convert.ToString(Request["txtGeneDTitle"]);
        PageContent = Convert.ToString(Request["elm1"]);
        GenedLink = Convert.ToString(Request["txtGeneDlinkTitle"]);
        linktopage1 = Convert.ToString(Request["txt_link"]);
        landingPageTitle = Convert.ToString(Request["txtlandTitle"]);
        landingPageContent = Convert.ToString(Request["elm2"]);
        PageGeneDPos = Convert.ToInt32(Request["txtGeneDPos"]);
        
        //Select image path for main page
        if (imageTypeMain == "upload")
        {
            PageImage = uploadImageMainPage();
        }
        else if (imageTypeMain == "url")
        {
            PageImage = validateImageName("txt_image_url");
        }
        //Select image path for landing page Pending
        if (sessGeneType == 0)
        {
            if (sessAbout == false)
            {
                Session["GeneType"] = 1;
                sessGeneType = 1;
            }
            else
            {
                Session["GeneType"] = 2;
                sessGeneType = 2;
            }
        }
        saveLandingPage();
        if (PageImage == "")
        {
            PageImage = " ";
        }
        if (Request["MGeneDId"] == null)
        {
            if (sessNewTemplate == true)
            {
                if (sessGeneType != 0)
                {
                    if (sessAbout == false)
                    {
                        contId = sessContId;
                    }
                    Add_genericD(sessSiteId, contId, 0, sessGeneType, sessGeneDefaId, sessPageTitle, PageTitle, PageContent, 
                        tipo, GenedLink, linktopage1, PageImage, PageGeneDPos);
                }
                Session["TemplateChose"] = false;
                Session["NewPageTemplate"] = false;
                Session["PageTitle"] = "";
                Response.Redirect("mnt_Generics.aspx?Generic=1");
                //Response.Redirect("mnt_Generics.aspx?Generic=" + sessGeneType);
            }
            else 
            {
                if (sessAbout == false)
                {
                    contId = Convert.ToInt32(Session["contId"]);
                }
                Add_genericD(sessSiteId, contId, genericId, sessGeneType, sessGeneDefaId, sessPageTitle, PageTitle, PageContent, tipo,
                GenedLink, linktopage1, PageImage, PageGeneDPos);
            }
            
            Response.Redirect("mnt_generic_d.aspx?GenericId=" + genericId);    
        }
        else
        {
            Upd_genericD(sessSiteId, Convert.ToInt32(Request["GeneId"]), Convert.ToInt32(Request["MGeneDId"]), PageTitle, PageContent, tipo, GenedLink, linktopage1, PageImage, PageGeneDPos);
            Response.Redirect("mnt_generic_d.aspx?GenericId=" +Convert.ToInt32(Request["GeneId"]));
        }
    }

    protected void saveLandingPage()
    {
        string imageLandingPageMethod = Convert.ToString(Request["rbImageLandingPage"]);
        int genId = 0;
        if (Convert.ToString(Request["radioLinkType"]) == "landingPage")
        {
            if (imageLandingPageMethod=="upload")
            {
                landingPageImage = uploadImageLandingPage();
            }
            else if (imageLandingPageMethod == "url")
            {
                landingPageImage = validateImageName("txt_image_urlLandingPage");
            }
            if(landingPageImage==""){
                landingPageImage = " ";
            }
            if(has_landing_page1){
                genId = idLandingPage;
                updLandPage(genId, landingPageTitle, landingPageContent, landingPageImage);
            }else{
                genId = addLandPage(sessSiteId, sessContId, landingPageTitle, landingPageContent, landingPageImage, 1, 4);//4 recongnized as genD
            }
            linktopage1 = "Generic_x.aspx?LandingId=" + genId;
        }
    }

    protected string uploadImageMainPage()
    {
        string UploadedFile = Upload.PostedFile.FileName;
        string path = "";
        int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
        if (Convert.ToString(UploadedFile) != "")
        {
            //to retrieve only Filename from the complete path
            UploadedFile = (UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos)).ToLower();
            if (!(UploadedFile.Contains(".jpg") || UploadedFile.Contains(".png") || UploadedFile.Contains(".gif")))
            {
                lbimage.Text = "Please select a correct image format ( .jpg, .jpeg, .png, .gif ) and url";
            }
            else
            {
                // Save uploaded file to server at the in the Pics folder
                path = UploadedFile;
                Upload.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFile);
            }
        }
        return path;
    }

    protected string uploadImageLandingPage()
    {
        string UploadedFile = UploadLanding.PostedFile.FileName;
        string path = "";
        int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
        if (Convert.ToString(UploadedFile) != "")
        {
            //to retrieve only Filename from the complete path
            UploadedFile = (UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos)).ToLower();
            if (!(UploadedFile.Contains(".jpg") || UploadedFile.Contains(".png") || UploadedFile.Contains(".gif")))
            {
                lbimage.Text = "Please select a correct image format ( .jpg, .jpeg, .png, .gif ) and url";
            }
            else
            {
                // Save uploaded file to server at the in the Pics folder
                path = UploadedFile;
                UploadLanding.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFile);
            }
        }
        return path;
    }

    protected string validateImageName(string imageUrl)
    {
        string url_image = "";
        string urlValue = Convert.ToString(Request[imageUrl]);
        if (urlValue != "")
        {
            if (Regex.IsMatch(urlValue, @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
            {
                url_image = urlValue;
            }
            else
            {
                lbimage.Text = "Please select a correct image format ( .jpg, .jpeg, .png, .gif ) and url";
            }
        }
        return url_image;
    }
}