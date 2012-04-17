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

static class RegexUtilResourceCenter
{
    static Regex _regex = new Regex(@"\b[w-]*\d+[w-]*\b");
    static string catchException = "";

    static public string MatchKey(string input)
    {
        try
        {
            Match myMatch = _regex.Match(input);
            string key = "";
            if (myMatch.Success)
            {
                key = myMatch.Groups[0].Value;
            }
            return key;
        }
        catch(ArgumentException e){
            return catchException;
        }
    }
}

public partial class mnt_ResourCenter : ResourceCenter
{
    public string str_TinyMCE;
    public string strFolder = "Images";
    Addins addins = new Addins();
    public string title1, title2, content1, content2, email1, email2, phone1, phone2, linktopage1, linktopage2, imagemain, imageleft, imageright, txtmoreLink, txtmoreLink2 = "";
    private int contRC = 0;
    public int RadioTipo1, RadioTipo2, RadioTipoMore1, RadioTipoMore2 = 0;
    /*Variables to load the data of the landing pages*/
    public string landingPageTitle1, landingPageTitle2, landingPageTitle3, landingPageTitle4,
        landingPageContent1, landingPageContent2, landingPageContent3, landingPageContent4,
        landingPageImage1, landingPageImage2, landingPageImage3, landingPageImage4 = "";

    public string xtractNums(string str)
    {
        return RegexUtilResourceCenter.MatchKey(str);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }

        Session["CurrentPage"] = "mnt_ResourCenter.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }
        str_TinyMCE = addins.GettinyMCE();


        DataSet data = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        data = Get_ResourceCenter_MP(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]));
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row["ResoMainImage"].ToString() == "")
                {
                    if (Request["imagemain"] != null)
                    {
                        imagemain = Request["imagemain"].ToString();
                    }
                }
                else
                {
                    imagemain = row["ResoMainImage"].ToString();
                }

                title1 = row["ResoTitle1"].ToString();
                content1 = row["ResoContent1"].ToString();
                email1 = row["ResoEmail1"].ToString();
                phone1 = row["ResoContact1"].ToString();
                title2 = row["ResoTitle2"].ToString();
                content2 = row["ResoContent2"].ToString();
                email2 = row["ResoEmail2"].ToString();
                phone2 = row["ResoContact2"].ToString();
                RadioTipo1 = Convert.ToInt32(row["TypeLink1"]);
                if (row["ResoImage"].ToString() == "")
                {
                    if (Request["imageleft"] != null)
                    {
                        imageleft = Request["imageleft"].ToString();
                    }
                }
                else
                {
                    imageleft = row["ResoImage"].ToString();
                }
                 
                linktopage1 = row["ResoLink1"].ToString();
                RadioTipo2 = Convert.ToInt32(row["TypeLink2"]);
                if (row["ResoImage2"].ToString() == "")
                {
                    if (Request["imageright"] != null)
                    {
                        imageright = Request["imageright"].ToString();
                    }
                }
                else
                {
                     imageright = row["ResoImage2"].ToString();
                }
               
                linktopage2 = row["ResoLink2"].ToString();
                txtmoreLink = row["ResoMoreLink"].ToString();
                txtmoreLink2 = row["ResoMoreLink2"].ToString();
                if (Convert.ToString(row["ResoMoreLinkType"]) != "")
                {
                    RadioTipoMore1 = Convert.ToInt32(row["ResoMoreLinkType"]);
                }
                if (Convert.ToString(row["ResoMoreLinkType2"]) != "")
                {
                    RadioTipoMore2 = Convert.ToInt32(row["ResoMoreLinkType2"]);
                }
                contRC++;
            }
        }
        landingPageTitle1 = load_landing_page_Title(linktopage1);
        landingPageTitle2 = load_landing_page_Title(linktopage2);
        landingPageTitle3 = load_landing_page_Title(txtmoreLink);
        landingPageTitle4 = load_landing_page_Title(txtmoreLink2);
        landingPageContent1 = load_landing_page_Content(linktopage1);
        landingPageContent2 = load_landing_page_Content(linktopage2);
        landingPageContent3 = load_landing_page_Content(txtmoreLink);
        landingPageContent4 = load_landing_page_Content(txtmoreLink2);
        landingPageImage1 = load_landing_page_Image(linktopage1);
        landingPageImage2 = load_landing_page_Image(linktopage2);
        landingPageImage3 = load_landing_page_Image(txtmoreLink);
        landingPageImage4 = load_landing_page_Image(txtmoreLink2);

        miniSitemap.CMS_mnt_SiteMap minism = (miniSitemap.CMS_mnt_SiteMap)(Page.LoadControl("mnt_SiteMap.ascx"));
        PlaceHolderMinisitemap.Controls.Add(minism);
    }

    protected string load_landing_page_Title(string url)
    {
        string contentLoaded = "";
        DataSet data = new DataSet();
        MainContentGeneric generic_X = new MainContentGeneric();
        string id_lp = xtractNums(url);
        if (id_lp != "")
        {
            int id_landing_page = Convert.ToInt32(id_lp);
            data = new DataSet();
            data = generic_X.Get_GenericX_By_Id(id_landing_page);
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    contentLoaded = row["GeneXTitle"].ToString();
                }
            }
        }
        return contentLoaded;
    }

    protected string load_landing_page_Content(string url)
    {
        string contentLoaded = "";
        DataSet data = new DataSet();
        MainContentGeneric generic_X = new MainContentGeneric();
        string id_lp = xtractNums(url);
        if (id_lp != "")
        {
            int id_landing_page = Convert.ToInt32(id_lp);
            data = new DataSet();
            data = generic_X.Get_GenericX_By_Id(id_landing_page);
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    contentLoaded = row["GeneXContent"].ToString();
                }
            }
        }
        return contentLoaded;
    }

    protected string load_landing_page_Image(string url)
    {
        string contentLoaded = "";
        DataSet data = new DataSet();
        MainContentGeneric generic_X = new MainContentGeneric();
        string id_lp = xtractNums(url);
        if (id_lp != "")
        {
            int id_landing_page = Convert.ToInt32(id_lp);
            data = new DataSet();
            data = generic_X.Get_GenericX_By_Id(id_landing_page);
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    contentLoaded = row["GeneXImage"].ToString();
                }
            }
        }
        return contentLoaded;
    }

    protected void btn_CancelLand_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_ResourCenter.aspx");
    }
    
    protected void btn_SaveLand_Click(object sender, EventArgs e)
    {
        linktopage1 = Convert.ToString(Request["txt_link"]);
        linktopage2 = Convert.ToString(Request["txt_link2"]);
        txtmoreLink = Convert.ToString(Request["txtLinkmore"]) ;
        txtmoreLink2 = Convert.ToString(Request["txtLinkmore2"]);
        int genId = 0;

        #region Upload
        String UploadedFile = UploadLanding.PostedFile.FileName;
        if (Convert.ToString(UploadedFile) != "")
        {
            int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
            string addres = "";
            //to retrieve only Filename from the complete path
            String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
            String lowuploadimage = UploadedFileName.ToLower();
            if (!(lowuploadimage.Contains(".jpg") || lowuploadimage.Contains(".png") || lowuploadimage.Contains(".gif")))
            {
                return;
            }
            lbimageinfo.Text = Convert.ToString(UploadedFileName);
            UploadLanding.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
        }
        #endregion
        genId = addLandPage(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToString(Request["txtlandTitle"]), Convert.ToString(Request["elmlandpage"]), lbimageinfo.Text, 1, 3);//3 MEANS HIGHLIGHTS ON GENERIC TABLE
        if (Convert.ToInt32(Request["LinkTypeT"]) == 1)
        {
            if (Convert.ToInt32(Request["LinkID"]) == 1)
            {
                linktopage1 = "Generic_x.aspx?LandingId=" + Convert.ToInt32(genId);
            }
            else
            {
                linktopage2 = "Generic_x.aspx?LandingId=" + Convert.ToInt32(genId);
            }
        }
        else
        {
            if (Convert.ToInt32(Request["LinkIDmore"]) == 1)
            {
                txtmoreLink = "Generic_x.aspx?LandingId=" + Convert.ToInt32(genId);
            }
            else
            {
                txtmoreLink2 = "Generic_x.aspx?LandingId=" + Convert.ToInt32(genId);
            }
        }
        /*Begin of Machetazo*/
        string imagePath = "";
        string imagePathLeft = "";
        string imagePathRight = "";
        if (Convert.ToString(Request["rb_image_type"]) == "upload")
        {
            imagePath = Convert.ToString(Request["imagemain"]);
        }
        else if (Convert.ToString(Request["rb_image_type"]) == "url")
        {
            imagePath = Convert.ToString(Request["txt_image_url"]);
        }
        if (Convert.ToString(Request["rb_image_type_left"]) == "upload")
        {
            imagePathLeft = Convert.ToString(Request["imageleft"]);
        }
        else if (Convert.ToString(Request["rb_image_type_left"]) == "url")
        {
            imagePathLeft = Convert.ToString(Request["txt_image_url_left"]);
        }
        if (Convert.ToString(Request["rb_image_type_right"]) == "upload")
        {
            imagePathRight = Convert.ToString(Request["imageright"]);
        }
        else if (Convert.ToString(Request["rb_image_type_right"]) == "url")
        {
            imagePathRight = Convert.ToString(Request["txt_image_url_right"]);
        }
        if (contRC == 0)
        {
            add_ResourceCenter(
            Convert.ToInt32(Session["siteId"]),
            Convert.ToInt32(Session["contId"]),
            imagePath,
            Convert.ToString(Request["txt_title1"]),
            Convert.ToString(Request["elm1"]),
            Convert.ToString(Request["txt_email1"]),
            Convert.ToString(Request["txt_contact1"]),
            Convert.ToString(Request["txt_title2"]),
            Convert.ToString(Request["elm2"]),
            Convert.ToString(Request["txt_email2"]),
            Convert.ToString(Request["txt_contact2"]),
            Convert.ToInt32(Request["radiotipo"]),
            linktopage1,
            imagePathLeft,
            Convert.ToInt32(Request["radiotipo2"]),
            linktopage2,
            imagePathRight,
            Convert.ToInt32(Request["radiotipomore"]),
            txtmoreLink,
            Convert.ToInt32(Request["radiotipomore2"]),
            txtmoreLink2);
        }
        else
        {
            Upd_ResourceCenter(
            Convert.ToInt32(Session["siteId"]),
            Convert.ToInt32(Session["contId"]),
            imagePath,
            Convert.ToString(Request["txt_title1"]),
            Convert.ToString(Request["elm1"]),
            Convert.ToString(Request["txt_email1"]),
            Convert.ToString(Request["txt_contact1"]),
            Convert.ToString(Request["txt_title2"]),
            Convert.ToString(Request["elm2"]),
            Convert.ToString(Request["txt_email2"]),
            Convert.ToString(Request["txt_contact2"]),
            Convert.ToInt32(Request["radiotipo"]),
            linktopage1,
            imagePathLeft,
            Convert.ToInt32(Request["radiotipo2"]),
            linktopage2,
            imagePathRight,
            Convert.ToInt32(Request["radiotipomore"]),
            txtmoreLink,
            Convert.ToInt32(Request["radiotipomore2"]),
            txtmoreLink2);
        }
        Response.Redirect("mnt_ResourCenter.aspx");
        /*End of Machetazo*/
    }
    public bool ThumbnailCallback()
    {
        return false;
    }
    protected void btnSaveRC_Click(object sender, EventArgs e)
    {
        string imagePath = "";
        string imagePathLeft = "";
        string imagePathRight = "";
        if (Convert.ToString(Request["rb_image_type"])=="upload")
        {
            imagePath = Convert.ToString(Request["imagemain"]);
        }
        else if (Convert.ToString(Request["rb_image_type"]) == "url")
        {
            imagePath = Convert.ToString(Request["txt_image_url"]);
        }
        if (Convert.ToString(Request["rb_image_type_left"]) == "upload")
        {
            imagePathLeft = Convert.ToString(Request["imageleft"]);
        }
        else if (Convert.ToString(Request["rb_image_type_left"]) == "url")
        {
            imagePathLeft = Convert.ToString(Request["txt_image_url_left"]);
        }
        if (Convert.ToString(Request["rb_image_type_right"]) == "upload")
        {
            imagePathRight = Convert.ToString(Request["imageright"]);
        }
        else if (Convert.ToString(Request["rb_image_type_right"]) == "url")
        {
            imagePathRight = Convert.ToString(Request["txt_image_url_right"]);
        }

        if (contRC == 0)
        {
            add_ResourceCenter(
            Convert.ToInt32(Session["siteId"]),
            Convert.ToInt32(Session["contId"]),
            imagePath,
            Convert.ToString(Request["txt_title1"]),
            Convert.ToString(Request["elm1"]),
            Convert.ToString(Request["txt_email1"]),
            Convert.ToString(Request["txt_contact1"]),
            Convert.ToString(Request["txt_title2"]),
            Convert.ToString(Request["elm2"]),
            Convert.ToString(Request["txt_email2"]),
            Convert.ToString(Request["txt_contact2"]),
            Convert.ToInt32(Request["radiotipo"]),
            Convert.ToString(Request["txt_link"]),
            imagePathLeft,
            Convert.ToInt32(Request["radiotipo2"]),
            Convert.ToString(Request["txt_link2"]),
            imagePathRight,
            Convert.ToInt32(Request["radiotipomore"]),
            Convert.ToString(Request["txtLinkmore"]),
            Convert.ToInt32(Request["radiotipomore2"]),
            Convert.ToString(Request["txtLinkmore2"]));
        }
        else
        {
            Upd_ResourceCenter(
            Convert.ToInt32(Session["siteId"]),
            Convert.ToInt32(Session["contId"]),
            imagePath,
            Convert.ToString(Request["txt_title1"]),
            Convert.ToString(Request["elm1"]),
            Convert.ToString(Request["txt_email1"]),
            Convert.ToString(Request["txt_contact1"]),
            Convert.ToString(Request["txt_title2"]),
            Convert.ToString(Request["elm2"]),
            Convert.ToString(Request["txt_email2"]),
            Convert.ToString(Request["txt_contact2"]),
            Convert.ToInt32(Request["radiotipo"]),
            Convert.ToString(Request["txt_link"]),
            imagePathLeft,
            Convert.ToInt32(Request["radiotipo2"]),
            Convert.ToString(Request["txt_link2"]),
            imagePathRight,
            Convert.ToInt32(Request["radiotipomore"]),
            Convert.ToString(Request["txtLinkmore"]),
            Convert.ToInt32(Request["radiotipomore2"]),
            Convert.ToString(Request["txtLinkmore2"]));
        }
        Response.Redirect("mnt_ResourCenter.aspx");
    }
    protected void Btn_UploadMain_Click(object sender, EventArgs e)
    {
        String UploadedFile = UploadMain.PostedFile.FileName;
        imagemain =  Convert.ToString(UploadMain.PostedFile.FileName);
        if (Convert.ToString(UploadedFile) != "")
        {
            int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
            //to retrieve only Filename from the complete path
            String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
            String lowuploadimage = UploadedFileName.ToLower();
            if (!(lowuploadimage.Contains(".jpg") || lowuploadimage.Contains(".png") || lowuploadimage.Contains(".gif")))
            {
                imagemain =  "Please select a correct image format ( .jpg  .gif  .png )";
                return;
            }
            imagemain =  Convert.ToString(UploadedFileName);
            UploadMain.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
        }
    }
    protected void btnUploadleft_Click(object sender, EventArgs e)
    {
        String UploadedFile = UploadImage1.PostedFile.FileName;
        string addres = "";
        if (Convert.ToString(UploadedFile) != "")
        {
            int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
            //to retrieve only Filename from the complete path
            String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
            addres = Convert.ToString(UploadedFileName );
            String lowuploadimage = UploadedFileName.ToLower();
            if (!(lowuploadimage.Contains(".jpg") || lowuploadimage.Contains(".png") || lowuploadimage.Contains(".gif")))
            {
                imageleft = "Please select a correct image format ( .jpg  .gif  .png )";
                return;
            }
            UploadImage1.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
        }
        imageleft = addres;
    }
    protected void btnUploadRight_Click(object sender, EventArgs e)
    {
        String UploadedFile = UploadImage2.PostedFile.FileName;
        imageright = Convert.ToString(UploadImage2.PostedFile.FileName);
        if (Convert.ToString(UploadedFile) != "")
        {
            int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
            //to retrieve only Filename from the complete path
            String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
            String lowuploadimage = UploadedFileName.ToLower();
            if (!(lowuploadimage.Contains(".jpg") || lowuploadimage.Contains(".png") || lowuploadimage.Contains(".gif")))
            {
                imageright = "Please select a correct image format ( .jpg  .gif  .png )";
                return;
            }
            imageright = Convert.ToString(UploadedFileName);
            UploadImage2.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
        }
    }
}
