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

static class RegexUtilTemplateE
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

public partial class mnt_generic_e : Gen_E
{
    public string str_TinyMCE;
    public string strFolder = "Images";
    //Global variables to interact wiht the visual layer.
    public bool has_landing_page1 = false;
    public string landingPageTitle, landingPageContent, landingPageImage, TitleE, Place, contentLoad, GenedLink, linktopage1, Position = "";
    public string add_edit_landing_page1 = "New ";
    public int idLandingPage, tipo = 0;
    //End of visual layer variables.

    public string xtractNums(string str)
    {
        return RegexUtilTemplateE.MatchKey(str);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        miniSitemap.CMS_mnt_SiteMap minism = (miniSitemap.CMS_mnt_SiteMap)(Page.LoadControl("mnt_SiteMap.ascx"));
        PlaceHolderMinisitemap.Controls.Add(minism);
       
        Addins addins = new Addins();
        str_TinyMCE = addins.GettinyMCE();

        Session["CurrentPage"] = "mnt_generic_e.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }
        if (Request["GenericId"] != null)
        {
            Session["GenericId"] = Convert.ToInt32(Request["GenericId"]);
            Session["NewPageTemplate"] = false;
        }
        loadMainData();
        if (linktopage1!= "" && linktopage1!=null)
        {
            loadLandingPage();
        }
    }

    protected void loadMainData()
    {
        if ((Request["GenericId"] != null) || (Convert.ToBoolean(Session["NewPageTemplate"]) == false))
        {
            DataSet data = new DataSet();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            data = Get_GenericE_By_ID(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Request["GenericId"]));
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    TitleE = row["GeneETitle"].ToString();
                    Place = "false"; //row["GeneELocation"].ToString();
                    contentLoad = row["GeneEContent"].ToString();
                    GenedLink = row["GeneELinkTitle"].ToString();
                    linktopage1 = row["GeneELink"].ToString();
                    tipo = Convert.ToInt32(row["LinkType"]);
                    lbCreationDate.Text = row["GeneEDate"].ToString();
                }
            }
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
                    Place = "true";
                }
            }
        }
    }


    //--------------------------------------

    protected void btn_Save_Click(object sender, EventArgs e)
    {      
        int sessContId = Convert.ToInt32(Session["contId"]);
        int genericid = Convert.ToInt32(Session["genericid"]);
        int sessGeneType = Convert.ToInt32(Session["GeneType"]);
        int sessGeneDefaId = Convert.ToInt32(Session["GeneDefaId"]);
        int sessSiteID = Convert.ToInt32(Session["siteId"]);
        string sessPageTitle = Convert.ToString(Session["PageTitle"]);
        TitleE = Convert.ToString(Request["txt_GenATitle"]);
        Place = " "; // Convert.ToString(Request["txt_Place"])
        contentLoad = Convert.ToString(Request["elm1"]);
        GenedLink = Convert.ToString(Request["txt_linkTitleE"]);
        linktopage1 = Convert.ToString(Request["txt_link"]);
        tipo = Convert.ToInt32(Request["radiotipo"]);
        has_landing_page1 = Convert.ToBoolean(Request["landingPageFound"]);
        if (Convert.ToString(Request["radioLinkType"]) == "landing")
        {
            saveEditLandingPage();
        }
        if (Convert.ToBoolean(Session["NewPageTemplate"]) == true)
        {
            if (Convert.ToBoolean(Session["About"]) == false)
            {
                Session["GeneType"] = 1;
            }
            else
            {
                Session["GeneType"] = 2;
            }
            if (Convert.ToBoolean(Session["About"]) == true)
            {
                sessContId = 0;
            }
            addGenericE(sessSiteID, sessContId, sessGeneType, sessGeneDefaId, sessPageTitle, TitleE, Place, contentLoad, tipo, GenedLink, linktopage1, 1);
            Session["PageTitle"] = "";
            Session["TemplateChose"] = false;
            Session["NewPageTemplate"] = false;
            genericid = sessGeneType;
        }
        else
        {
            Upd_GenericE(genericid, TitleE, Place, contentLoad, tipo, GenedLink, linktopage1);
            
        }
        if (Convert.ToUInt32(genericid) > 2)
        {
            Response.Redirect("mnt_generic_e.aspx?GenericId=" + genericid);
        }
        else
        {
            Response.Redirect("mnt_Generics.aspx?Generic=1");
        }
    }

    //--------------------------------------------
    protected void saveEditLandingPage()
    {
        int genId = 0;
        string image_path = "";
        if (Convert.ToString(Request["rb_image_type"]) == "upload")
        {
            #region upload
            String UploadedFile = UploadLanding.PostedFile.FileName;
            int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
            string addres = "";
            if (Convert.ToString(UploadedFile) != "")
            {
                //to retrieve only Filename from the complete path
                String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
                String lowuploadimage = UploadedFileName.ToLower();
                // Save uploaded file to server at the in the Pics folder
                addres = Convert.ToString(UploadedFileName);
                UploadLanding.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
            }
            lbimageinfo.Text = addres;
            image_path = lbimageinfo.Text;
            #endregion
        }
        else if (Convert.ToString(Request["rb_image_type"]) == "url")
        {
            string url_image = Convert.ToString(Request["txt_image_url"]);
            if (url_image != "")
            {
                if (Regex.IsMatch(url_image, @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
                {
                    image_path = url_image;
                }
            }
        }
        landingPageTitle = Convert.ToString(Request["txtlandTitle"]);
        landingPageContent = Convert.ToString(Request["elmlandpage"]);
        if (Place == "true")
        {
            genId = idLandingPage;
            updLandPage(genId, landingPageTitle, landingPageContent, image_path);
        }
        else
        {
            genId = addLandPage(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), landingPageTitle, landingPageContent, image_path, 1, 4);//4 recongnized as genD
        }
        linktopage1 = "Generic_x.aspx?LandingId=" + genId;
    }
}