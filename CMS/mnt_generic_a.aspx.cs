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

static class RegexUtilTemplateA
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

public partial class mnt_generic_a : Gen_A
{
    public bool hasErrors, has_landing_page1, has_landing_page2= false;
    public string strFolder = "Images";
    //This variables communicate with the visual layer for the main page content (write values to the inputs, etc).
    public string contentLoad, imageA, rbLinkType1, rbLinkType2, titleMainPage, txtLink1, txtLink2, txtLinkTitle1, txtLinkTitle2 = "";
    public int rbLinkType1ID, rbLinkType2ID = 0;
    //This variables communicate with the visual layer for the landing page 1 and 2 content (write values to the inputs, etc).
    public string txt_gen_page_content_1, txt_gen_page_content_2, txt_gen_page_image_1, txt_gen_page_image_2, 
        txt_gen_page_title_1, txt_gen_page_title_2 = "";
    public int idLandingPage1, idLandingPage2 = -1;
    //Session variables:
    public bool sessAbout = false;
    public int contId = 0;
    public int geneDefaId = 0;
    public int genericId = 0;
    public int geneType = 0;
    public int siteId = 0;
    public string sessPageTitle = "";
    //Other global variables:
    public string str_TinyMCE = ""; //Content of the TinyMCE script, assigned when page loads.
    public string add_edit_landing_page1, add_edit_landing_page2 = "New "; //Only purpose is write New or Edit in the visual layer.
    public string regexImage = @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$";

    public string xtractNums(string str)
    {
        return RegexUtilTemplateA.MatchKey(str);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Addins addins = new Addins();
        miniSitemap.CMS_mnt_SiteMap minism = (miniSitemap.CMS_mnt_SiteMap)(Page.LoadControl("mnt_SiteMap.ascx"));
        str_TinyMCE = addins.GettinyMCE();

        int id_landing_page = -1;

        sessAbout = Convert.ToBoolean(Session["About"]);
        contId = Convert.ToInt32(Session["contId"]);
        geneDefaId = Convert.ToInt32(Session["GeneDefaId"]);
        genericId = Convert.ToInt32(Session["GenericId"]);
        geneType = Convert.ToInt32(Session["GeneType"]);
        siteId = Convert.ToInt32(Session["siteId"]);
        sessPageTitle = Convert.ToString(Session["PageTitle"]);

        Session["CurrentPage"] = "mnt_generic_a.aspx";
        
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }
        if (Request["GenericId"] != null)
        {
            Session["GenericId"] = Convert.ToInt32(Request["GenericId"]);
            Session["NewPageTemplate"] = false;
        }
        PlaceHolderMinisitemap.Controls.Add(minism);
        #region FirstLoad
        if ((Request["GenericId"] != null))
        {
            DataSet data = loadMainPage();
            load_landing_page(ref data);
        }
        #endregion
    }

    private DataSet loadMainPage()
    {
        DataSet data = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        data = get_GenA_By_Id(Convert.ToInt32(Request["GenericId"]));
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                titleMainPage = row["GeneATitle"].ToString();
                contentLoad = row["GeneAContent"].ToString();
                imageA = row["GeneAImage"].ToString();
                rbLinkType1ID = Convert.ToInt32(row["LinkTypeId"]);
                txtLinkTitle1 = row["GeneALinkTitle"].ToString();
                txtLink1 = row["GeneALink"].ToString();
                rbLinkType2ID = Convert.ToInt32(row["LinkTypeId2"]);
                txtLinkTitle2 = row["GeneALink2Title"].ToString();
                txtLink2 = row["GeneALink2"].ToString();
                lbCreationDate.Text = row["GeneADate"].ToString();
            }
        }
        btn_Save.CssClass = "class_btnUpdate";
        return data;
    }

    private void load_landing_page(ref DataSet data)
    {
        MainContentGeneric generic_X = new MainContentGeneric();

        int id_landing_page = -1;
        string id_lp = "";
        if (txtLink1 != null)
        {
            id_lp = xtractNums(txtLink1);
            if (id_lp != "")
            {
                id_landing_page = Convert.ToInt32(id_lp);
                idLandingPage1 = id_landing_page;
                data = new DataSet();
                data = generic_X.Get_GenericX_By_Id(id_landing_page);
                foreach (DataTable table in data.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        txt_gen_page_title_1 = row["GeneXTitle"].ToString();
                        txt_gen_page_content_1 = row["GeneXContent"].ToString();
                        txt_gen_page_image_1 = row["GeneXImage"].ToString();
                    }
                    add_edit_landing_page1 = "Edit ";
                    has_landing_page1 = true;
                }
            }
        }
        if (txtLink2 != null)
        {
            id_lp = xtractNums(txtLink2);
            if (id_lp != "")
            {
                id_landing_page = Convert.ToInt32(id_lp);
                idLandingPage2 = id_landing_page;
                data = new DataSet();
                data = generic_X.Get_GenericX_By_Id(id_landing_page);
                foreach (DataTable table in data.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        txt_gen_page_title_2 = row["GeneXTitle"].ToString();
                        txt_gen_page_content_2 = row["GeneXContent"].ToString();
                        txt_gen_page_image_2 = row["GeneXImage"].ToString();
                    }
                    add_edit_landing_page2 = "Edit ";
                    has_landing_page2 = true;
                }
            }
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {    
        rbLinkType1 = Convert.ToString(Request["rbLinkType1"]);
        rbLinkType2 = Convert.ToString(Request["rbLinkType2"]);
        contentLoad = Request["elm1"].ToString();
        titleMainPage = Request["txt_GenATitle"].ToString();
        imageA = assignImagePath();
        txtLink1 = Request["txtLink1"].ToString();
        txtLink2 = Request["txtLink2"].ToString();
        txtLinkTitle1 = Request["txtLinkTitle1"].ToString();
        txtLinkTitle2 = Request["txtLinkTitle2"].ToString();
        txt_gen_page_title_1 = Request["txtlandTitle1"].ToString();
        txt_gen_page_title_2 = Request["txtlandTitle2"].ToString();
        txt_gen_page_content_1 = Request["elmlandpage1"].ToString();
        txt_gen_page_content_2 = Request["elmlandpage2"].ToString();
        txt_gen_page_image_1 = assignImagePathLandingPage("1");
        txt_gen_page_image_2 = assignImagePathLandingPage("2");
        if (rbLinkType1 == "landing")
        {
            saveLanding("1", idLandingPage1, has_landing_page1, txt_gen_page_title_1, txt_gen_page_content_1, txt_gen_page_image_1);
        }
        if (rbLinkType2 == "landing")
        {
            saveLanding("2", idLandingPage2, has_landing_page2, txt_gen_page_title_2, txt_gen_page_content_2, txt_gen_page_image_2);
        }
        if (hasErrors == false)
        {
            if (Convert.ToBoolean(Session["NewPageTemplate"]) == true)
            {
                NewTemplateAddGenericA();
            }
            else
            {
                EditTemplateGenericA();
            }
            Response.Redirect("mnt_Generics.aspx?Generic=1");
        }
    }

    private string assignImagePath()
    {
        string error_Message = "";
        string localPath = "";
        if (Convert.ToString(Request["rb_image_type"]) == "upload")
        {
            string uploadedFile = FUimage.PostedFile.FileName;
            int ExtractPos = uploadedFile.LastIndexOf("\\") + 1;
            if (Convert.ToString(uploadedFile) != "")
            {
                uploadedFile = (uploadedFile.Substring(ExtractPos, uploadedFile.Length - ExtractPos)).ToLower();
                if (!(uploadedFile.Contains(".jpg") || uploadedFile.Contains(".png") || uploadedFile.Contains(".gif")))
                {
                    error_Message = "Please select a correct image format ( .jpg  .gif  .png )";
                    hasErrors = true;
                }
                localPath = uploadedFile;
                FUimage.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + uploadedFile);
            }
        }
        else if (Convert.ToString(Request["rb_image_type"]) == "url")
        {
            localPath = Convert.ToString(Request["txt_image_url"]);
            if (localPath != "")
            {
                if (!Regex.IsMatch(localPath, regexImage))
                {
                    localPath = "";
                    error_Message = "Please select a correct image format ( .jpg  .gif  .png ) and url";
                    hasErrors = true;
                }
            }
        }
        Lberror.Text = error_Message;
        return localPath;
    }

    private string assignImagePathLandingPage(string landingPageNumber)
    {
        string imageURL = "";
        string localPath = "";
        string uploadedFile = "";
        string selectedImageOption = "";

        if (landingPageNumber == "1")
        {
            uploadedFile = UploadLanding1.PostedFile.FileName;
            imageURL = Request["txt_url_image_landing1"].ToString();
            selectedImageOption = Request["rb_image_type_landing1"].ToString();
        }
        else if (landingPageNumber == "2")
        {
            uploadedFile = UploadLanding2.PostedFile.FileName;
            imageURL = Request["txt_url_image_landing2"].ToString();
            selectedImageOption = Request["rb_image_type_landing2"].ToString();
        }
        if (Convert.ToString(uploadedFile) != "" && selectedImageOption == "upload")
        {
            int ExtractPos = uploadedFile.LastIndexOf("\\") + 1;
            //to retrieve only Filename from the complete path
            uploadedFile = (uploadedFile.Substring(ExtractPos, uploadedFile.Length - ExtractPos)).ToLower();
            if (!(uploadedFile.Contains(".jpg") || uploadedFile.Contains(".png") || uploadedFile.Contains(".gif")))
            {
                if (landingPageNumber == "1")
                {
                    lbErrorUploadImageLP1.Text = "Please select a correct image format ( .jpg  .gif  .png )";
                }
                else
                {
                    lbErrorUploadImageLP2.Text = "Please select a correct image format ( .jpg  .gif  .png )";
                }
                hasErrors = true;
            }
            else
            {
                localPath = uploadedFile;
                if (landingPageNumber == "1")
                {
                    UploadLanding1.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + uploadedFile);
                }
                else
                {
                    UploadLanding2.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + uploadedFile);
                }
            }
        }
        else if (selectedImageOption == "url")
        {
            localPath = imageURL;
            if (!Regex.IsMatch(localPath, regexImage))
            {
                localPath = "";
                hasErrors = true;
            }
        }
        return localPath;
    }

    protected void saveLanding(string landingPageNumber, int idLandingPage, bool loadedLandingPage, string titleLandingPage, string contentLandingPage, string imageLandingPage)
    {
        int genId = -1;
        if (hasErrors == false)
        {
            if (loadedLandingPage)
            {
                genId = idLandingPage;
                updLandPage(genId, titleLandingPage, contentLandingPage, imageLandingPage);
            }
            else
            {
                genId = addLandPage(siteId, contId, titleLandingPage, contentLandingPage, imageLandingPage, 1, 3);//3 MEANS HIGHLIGHTS ON GENERIC TABLE
            }
            if (landingPageNumber == "1")
            {
                txtLink1 = "Generic_x.aspx?LandingId=" + genId;
                rbLinkType1ID = 3;
            }
            else
            {
                txtLink2 = "Generic_x.aspx?LandingId=" + genId;
                rbLinkType2ID = 3;
            }
        }
    }

    private void EditTemplateGenericA()
    {
        Upd_GenaA(genericId, titleMainPage, contentLoad, imageA, rbLinkType1ID, txtLinkTitle1, txtLink1, rbLinkType2ID, txtLinkTitle2, txtLink2, 0);
    }

    private void NewTemplateAddGenericA()
    {
        if (sessAbout == false)
        {
            Session["GeneType"] = 1;
        }
        else
        {
            Session["GeneType"] = 2;
        }
        geneType = Convert.ToInt32(Session["GeneType"]);
        if (txtLink1 != "" && txt_gen_page_title_1 == "")
        {
            txt_gen_page_title_1 = "Link 1";
        }
        if (txtLink2 != "" && txt_gen_page_title_2 == "")
        {
            txt_gen_page_title_2 = "Link 2";
        }
        if (sessAbout == true)
        {
            contId = 0;
        }
        addGenericA(siteId, contId, geneType, geneDefaId, sessPageTitle,titleMainPage, contentLoad, imageA, rbLinkType1ID, 
            txt_gen_page_title_1,txtLink1, rbLinkType2ID, txt_gen_page_title_2, txtLink2, 0);
        Session["PageTitle"] = "";
        Session["TemplateChose"] = false;
        Session["NewPageTemplate"] = false;
    }

    //--------------------------------------
    public bool ThumbnailCallback()
    {
        return false;
    }
    //------------------------------------------
    protected void btn_CancelLand_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_generic_a.aspx?wtf=1");
    }


    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_generic_a.aspx?wtf=1");
    }
}