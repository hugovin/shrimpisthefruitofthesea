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

public partial class CMS_mnt_Site : Site
{
    public string strFolder = "Images";
    public string str_TinyMCE = "";
    public string linktopage1, PageTitle, PageContent, GenedLink, Position, radioType, TitleGen, ContGen = "";
    public bool Details = false;
    public bool AddEdit = false;
    public bool Information = false;
    private int Privacy = 0;
    private int TermOfUse = 0;
    private int contacts = 1;
    public string SiteContTitle, SiteContAddress, SiteContEmailCus, SiteContEmailSal, SiteContOrdPos, ActualImage, sitecontid;

    protected void Page_Load(object sender, EventArgs e)
    {
        miniSitemap.CMS_mnt_SiteMap minism = (miniSitemap.CMS_mnt_SiteMap)(Page.LoadControl("mnt_SiteMap.ascx"));
        PlaceHolderMinisitemap.Controls.Add(minism);
        bool fila = false;
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        Addins addins = new Addins();
        str_TinyMCE = addins.GettinyMCE();
        DataSet data = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        #region Load
        if (Request["Update"] == null)
        {        
            data = Get_Site_Information(Convert.ToInt32(Session["siteId"]));
            sb.AppendLine("<h1>Site Information</h1>");
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    sb.AppendLine("<div class=\"cdro_caja\">");
                    sb.AppendLine("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    
                    sb.AppendLine("<tr><td width=\"88\"></td><td>&nbsp;</td><td align=\"center\"></td><td>&nbsp;</td></tr>");

                    sb.AppendLine("<tr><td width=\"88\">&nbsp;</td>");
                    sb.AppendLine("<td width=\"58\" align=\"right\">Name&nbsp;&nbsp;</td>");
                    sb.AppendLine("<td width=\"310\">   <input type=\"text\" class=\"class_file\" value=\"" + row["SiteName"].ToString() + "\"name=\"txtTitle\"  /></td>");
                    sb.AppendLine("<td width=\"175\"></td></tr>");
                    sb.AppendLine("<tr><td colspan=\"5\">&nbsp;</td></tr>");
                    sb.AppendLine("<tr><td width=\"88\"></td>");
                    sb.AppendLine("<td align=\"right\">Description&nbsp;&nbsp;</td>");
                    sb.AppendLine("<td><input type=\"text\" class=\"class_file\" value=\"" + row["SiteDescription"].ToString() + "\"name=\"txtDescription\"  /></td>");
                    sb.AppendLine("<td></td></tr>");
                    sb.AppendLine("<tr><td colspan=\"5\">&nbsp;</td></tr>");
                    sb.AppendLine("<tr><td width=\"88\" height=\"20\"></td>");
                    sb.AppendLine("<td align=\"right\">URL:&nbsp;&nbsp;</td><td><input type=\"text\" class=\"class_file\" value=\"" + row["SiteURL"].ToString() + "\"name=\"txtURL\"  /></td>");
                    sb.AppendLine("<td>&nbsp;</td></tr>");
                    sb.AppendLine("<tr><td colspan=\"5\">&nbsp;</td></tr>");
                    sb.AppendLine("<tr><td width=\"88\"></td><td align=\"right\">Phone&nbsp;&nbsp;</td>");
                    sb.AppendLine("<td><input type=\"text\" class=\"class_file\" value=\"" + row["SitePhone"].ToString() + "\"name=\"txtPhone\"  /></td>");
                    sb.AppendLine("<td>&nbsp;</td> </tr>");
                    sb.AppendLine("<tr><td colspan=\"5\">&nbsp;</td></tr>");
                    sb.AppendLine("<tr><td width=\"88\"></td><td align=\"right\">Tag Line&nbsp;&nbsp;</td>");
                    sb.AppendLine("<td><input type=\"text\" class=\"class_file\" value=\"" + row["SiteTagLine"].ToString() + "\"name=\"txtTagLine\"  /></td>");
                    sb.AppendLine("<td>&nbsp;</td> </tr>");
                    sb.AppendLine("<tr><td colspan=\"5\">&nbsp;</td></tr>");
                    sb.AppendLine("<tr><td width=\"88\"></td><td align=\"right\">CopyRight:&nbsp;&nbsp;</td>");
                    sb.AppendLine("<td><input type=\"text\" class=\"class_file\" value=\"" + row["SiteCopy"].ToString() + "\"name=\"txtCopy\"  /></td>");
                    sb.AppendLine("<td>&nbsp;</td> </tr>");
                    sb.AppendLine("<tr><td colspan=\"5\">&nbsp;</td></tr>");
                    sb.AppendLine("<tr><td width=\"88\"></td><td align=\"right\">Key Words:&nbsp;&nbsp;</td>");
                    sb.AppendLine("<td><input type=\"text\" class=\"class_file\"s value=\"" + row["SiteKeyWords"].ToString() + "\"name=\"txtKeyWords\"  /></td>");
                    sb.AppendLine("<td>&nbsp;</td> </tr>");
                    sb.AppendLine("<tr><td colspan=\"5\">&nbsp; </td> </tr>");
                    sb.AppendLine("<tr><td width=\"88\"></td>");
                    sb.AppendLine("<td>&nbsp;</td>");
                    sb.AppendLine("<td align=\"center\">");
                    sb.AppendLine("</td>");
                    sb.AppendLine("<td>&nbsp;</td></tr></table></div>");
                    Privacy = Convert.ToInt32(row["SitePrivacy"]);
                    TermOfUse = Convert.ToInt32(row["SiteTerm"]);

                }
            }
            data.Dispose();
            div_SiteInfo.InnerHtml = sb.ToString();

            sb = new System.Text.StringBuilder();
            data = new DataSet();
            data = Get_SiteContact(Convert.ToInt32(Session["siteId"]));
            fila = false;
            sb.AppendLine(" <table width=\"512\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\"><tr>");
            sb.AppendLine("<td width=\"4\" height=\"10\">&nbsp;</td>");
            sb.AppendLine("<td width=\"200\" class=\"class_LineaVTabla\">&nbsp;</td>");
            sb.AppendLine("<td width=\"109\" class=\"class_LineaVTabla\">&nbsp;</td> ");
            sb.AppendLine("<td width=\"113\" class=\"class_LineaVTabla\" align=\"center\">&nbsp;</td>");
            sb.AppendLine("<td width=\"113\" align=\"center\">&nbsp;</td></tr>");
            sb.AppendLine("<tr>"); 
            sb.AppendLine("<td>&nbsp;</td>");
            sb.AppendLine("<td class=\"class_LineaVTabla\">Contact Title</td>");
            sb.AppendLine("<td class=\"class_LineaVTabla\">Contact Image</td>");
            sb.AppendLine("<td  class=\"class_LineaVTabla\" align=\"center\">&nbsp;</td>");
            //sb.AppendLine("<td align=\"center\">&nbsp;</td>");
            sb.AppendLine("</tr>");
            foreach (DataTable table2 in data.Tables)
            {
                foreach (DataRow row2 in table2.Rows)
                {
                    if (fila == true)
                    {
                        sb.AppendLine("<tr>");
                    }
                    else
                    {
                        sb.AppendLine("<tr class=\"whiteTable\"> ");
                    }
                    sb.AppendLine("<td>&nbsp;</td>");
                    sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\">" + row2["SiteContTitle"].ToString() + "</td>");
                    sb.AppendLine("<td align=\"left\" class=\"class_LineaVTabla\">" + row2["SiteContImage"].ToString() + "</td>");
                    sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><a href=mnt_Site.aspx?SiteContId=" + row2["SiteContId"] + "><img src=\"images/btn_Edit.png\" border=\"0\" />Edit</a></td>");
                    sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><a href=mnt_Site.aspx?DeleteId=" + row2["SiteContId"] + "><img src=\"images/btn_delete.png\" border=\"0\" />Delete</a></td>");
                    //sb.AppendLine("<td align=\"left\">&nbsp;</td>");
                    sb.AppendLine(" </tr>");
                    if (fila == true)
                    { fila = false; }
                    else
                    { fila = true; }
                    contacts++;
                  }

            }
            sb.AppendLine("</TABLE>");
            if (contacts == 3)
            { btn_New.Visible = false; }
            div_ContactInfo.InnerHtml = sb.ToString();
            data.Dispose();
            sb = new System.Text.StringBuilder();
            data = new DataSet();
            
        }
        //div_displayadds.InnerHtml = sb.ToString();
        sb = new System.Text.StringBuilder();
        sb.AppendLine(" <a href=\"mnt_site.aspx?Privacy="+Privacy+"&EditPrivacy=true\">Privacy Policy</a> | ");  
        sb.AppendLine(" <a href=\"mnt_site.aspx?Term=" + TermOfUse + "&EditPrivacy=true\">Terms of Use</a>");
        div_AdditionalInfo.InnerHtml = sb.ToString();


        #endregion         
        #region edit
        if ((Request["SiteContId"] != null) && (Convert.ToString(Request["SiteContId"]) != ""))
        {
            if (Request["UpdateInfo"] == null)
            {
                Details = true;
                sb = new System.Text.StringBuilder();
                data = GetAllSiteContactById(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Request["SiteContId"]));
                Session["SiteContId"] = Convert.ToInt32(Request["SiteContId"]);
                foreach (DataTable table in data.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        sitecontid = Request["SiteContId"].ToString();
                        SiteContTitle= row["SiteContTitle"].ToString();
                        SiteContAddress = row["SiteContAddress"].ToString();
                        SiteContEmailCus = row["SiteContEmailCus"].ToString();
                        SiteContEmailSal = row["SiteContEmailSal"].ToString();
                        SiteContOrdPos = row["SiteContOrdPos"].ToString();
                        ActualImage = row["SiteContImage"].ToString();
                        if (ActualImage != "")
                        {
                            div_actualimage.Visible = true;
                        }
                        lbimage.Text = ActualImage;
                        lbimage.Visible = false;
                    }
                }
            }
            btn_UpdateCont.CssClass = "class_btnUpdate";
            btn_UpdateCont.Text = ".";

        }
        #endregion
        #region delete
        if (Request["DeleteId"] != null)
        {
            delSiteContact(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Request["DeleteId"]));
            Response.Redirect("mnt_Site.aspx");
        }

        #endregion
        #region new
        if (Request["New"] != null)
        {
            Details = true; 
        }
        #endregion

        #region editAdds

        if (Request["EditAddId"] != null)
        {
            AddEdit = true;
            sb = new System.Text.StringBuilder();
            data = new DataSet();
            if (Request["UpdateInfoAdd"] == null)
            {
                data = Get_Adds_By_Id(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Request["EditAddId"]));
                foreach (DataTable table in data.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        lbAdd.Text = row["AddsImage"].ToString();
                        linktopage1 = row["AddsLink"].ToString();
                        GenedLink = row["AddsAlt"].ToString();
                        Session["addIdM"] = row["AddsId"].ToString();
                    }
                }
            }
            div_addButtons.Visible = true;

        }
        #endregion adds

        #region new Add
        if (Request["NewAdd"] != null)
        {
            AddEdit = true;
            div_addButtons.Visible = true;
            btn_Saveadd.Text = ".";
            btn_Saveadd.CssClass = "class_btnSave";
        }
        #endregion

        #region term or Privacy load
        data = new DataSet();
        if (Request["EditPrivacy"] != null)
        {
            if (Convert.ToInt32(Request["Privacy"]) != 0)
            {
                data = Get_GenericX_By_Id(Convert.ToInt32(Request["Privacy"]));
                foreach (DataTable table in data.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        TitleGen = row["GeneXTitle"].ToString();
                        ContGen = row["GeneXContent"].ToString();
                        LbgenID.Text = row["GeneXId"].ToString();
                        LbgenID.Visible = false;
                        lbimageinfo.Text = row["GeneXImage"].ToString();
                        
                        

                    }
                }
                Information = true;
               
            }else{
                if (Convert.ToInt32(Request["Term"]) != 0)
                {
                    data = Get_GenericX_By_Id(Convert.ToInt32(Request["Term"]));
                    foreach (DataTable table in data.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            TitleGen = row["GeneXTitle"].ToString();
                            ContGen = row["GeneXContent"].ToString();
                            LbgenID.Text = row["GeneXId"].ToString();
                            LbgenID.Visible = false;
                            lbimageinfo.Text = row["GeneXImage"].ToString();



                        }
                    }
                    Information = true;

                }else{
                    Information = true;
                }
            }

        }
        #endregion
    }
    //---------------------------------------------------------
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        Upd_Site(Convert.ToInt32(Session["siteId"]), Convert.ToString(Request["txtTitle"]), Convert.ToString(Request["txtDescription"]), Convert.ToString(Request["txtUrl"]),
            Convert.ToString(Request["txtPhone"]), Convert.ToString(Request["txtCopy"]), Convert.ToString(Request["txtTagline"]), Convert.ToString(Request["txtKeyWords"]));
        Response.Redirect("mnt_Site.aspx");
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("CMS_MainSite.aspx");
    }
    protected void btn_CancelCont_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Site.aspx");
    }

    protected void btn_UpdateCont_Click(object sender, EventArgs e)
    {
        string image_path = "";
        if (Convert.ToString(Request["rb_image_type"]) == "upload")
        {
            image_path = Convert.ToString(lbimage.Text);
        }
        else
        {
            if (Convert.ToString(Request["rb_image_type"]) == "url")
            {
                image_path = Convert.ToString(Request["txt_image_url"]);
            }
        }
        if (btn_UpdateCont.Text == ".")
        {
            Upd_SiteCont(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Request["sitecontid"]),
             Convert.ToString(Request["txtContTitle"]), Convert.ToString(Request["elm1"]), Convert.ToString(Request["txtEmailCus"]),
           Convert.ToString(Request["txtEmailSal"]), image_path);
            Response.Redirect("mnt_Site.aspx");
        }
        else
        {
            Add_SiteCont(Convert.ToInt32(Session["siteId"]), contacts,
             Convert.ToString(Request["txtContTitle"]), Convert.ToString(Request["elm1"]), Convert.ToString(Request["txtEmailCus"]),
           Convert.ToString(Request["txtEmailSal"]), image_path);
            Response.Redirect("mnt_Site.aspx");
        }
    }
    protected bool validationpos(string search)
    {
        int cont = 0;
        bool flag = false;
        for (int i = 0; i < search.Length; i++)
        {
            if (((search[i] == '0') || (search[i] == '1') || (search[i] == '2') || (search[i] == '3') || (search[i] == '4')
                || (search[i] == '5') || (search[i] == '6') || (search[i] == '7') || (search[i] == '8') || (search[i] == '9')) && (i > 0))
            {
                cont++;
            }
        }
     if (cont == (search.Length - 1))
     {
         flag = true;
     }
     return flag;
     }
    protected void btn_Upload_Click(object sender, EventArgs e)
    {
        string UploadedFile = Upload.PostedFile.FileName;
        int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;

        //to retrieve only Filename from the complete path
        String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
        String lowuploadimage = UploadedFileName.ToLower();
        if (!(lowuploadimage.Contains(".jpg") || lowuploadimage.Contains(".png") || lowuploadimage.Contains(".gif")))
        {
//            lbCurrentImage.Text = "Please select a correct image format ( .jpg  .gif  .png )";
            // .InnerHtml = "Please select a correct image format ( .jpg  .gif  .png )";
            return;
        }
        //if (UploadedFileName == "") return;
        // Save uploaded file to server at the in the Pics folder

        string addres = "";
        addres = Convert.ToString(UploadedFileName);
        Upload.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
        SiteContTitle = Request["txtContTitle"].ToString();
        SiteContAddress = Request["elm1"].ToString();
        SiteContEmailCus = Request["txtEmailCus"].ToString();
        SiteContEmailSal = Request["txtEmailSal"].ToString();
        SiteContOrdPos = Request["txtPosition"].ToString();
        ActualImage = addres;
        lbimage.Text = ActualImage;
        lbimage.Visible = false;
        div_actualimage.Visible = true;

    }

    public bool ThumbnailCallback()
    {
        return false;
    }
    protected void btn_New_Click(object sender, EventArgs e)
    {

            Response.Redirect("mnt_site.aspx?New=" + true);
    }
    protected void btn_Saveadd_Click(object sender, EventArgs e)
    {
        if ( btn_Saveadd.Text == "Save")
        {
            addAdds(Convert.ToInt32(Session["siteId"]),1, lbAdd.Text, Convert.ToString(Request["txtGeneDlinkTitle"]), Convert.ToString(Request["txt_link"]));
        }
        else
        {
            Upd_Adds(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["addIdM"]), Convert.ToString(lbAdd.Text),
                           Convert.ToString(lbAdd.Text), Request["txt_link"].ToString(), Request["txtGeneDlinkTitle"].ToString(), "");
        }
        Response.Redirect("mnt_Site.aspx");
    }
    protected void btn_Uploadsnew_Click(object sender, EventArgs e)
    {
        string UploadedFile = UploadNew.PostedFile.FileName;
        int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
        //to retrieve only Filename from the complete path
        String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
        String lowuploadimage = UploadedFileName.ToLower();
        if (!(lowuploadimage.Contains(".jpg") || lowuploadimage.Contains(".png") || lowuploadimage.Contains(".gif")))
        {
            return;
        }
        string addres = Convert.ToString(UploadedFileName);
        UploadNew.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
        lbAdd.Text = addres;
    }
    protected void btn_cancelAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Site.aspx");
    }

    protected void btn_CancelLand_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Site.aspx");

    }
    protected void btn_SaveLand_Click(object sender, EventArgs e)
    {
        int genId = 0;
        string image_path = "";
        if (Convert.ToString(Request["rb_image_type_privacy"]) == "upload")
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
                if (!(lowuploadimage.Contains(".jpg") || lowuploadimage.Contains(".png") || lowuploadimage.Contains(".gif")))
                {
                    //Do nothing.
                }
                // Save uploaded file to server at the in the Pics folder
                addres = Convert.ToString(UploadedFileName);
                lbimageinfo.Text = addres;
                UploadLanding.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
                image_path = lbimageinfo.Text;
            }
            #endregion
        }
        else
        {
            if (Convert.ToString(Request["rb_image_type_privacy"]) == "url")
            {
                string url_image = Convert.ToString(Request["txt_image_url_privacy"]);
                if (url_image != "")
                {
                    if (Regex.IsMatch(url_image, @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
                    {
                        image_path = url_image;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        radioType = Convert.ToString(Request["radiotipo"]);
        PageTitle = Convert.ToString(Request["txtGeneDTitle"]);
        PageContent = Convert.ToString(Request["elm1"]);
        Position = Convert.ToString(Request["txtGeneDPos"]);
        GenedLink = Convert.ToString(Request["txtGeneDlinkTitle"]);
        if (LbgenID.Text == "")
        {
            genId = addLandPage(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToString(Request["txtlandTitle"]), Convert.ToString(Request["elmlandpage"]), image_path, 1, 4);//4 recongnized as genD
        }
        
        if (Request["EditPrivacy"] == null)
        {
            lbGenx.Text = genId.ToString();
            linktopage1 = "Generic_x.aspx?LandingId=" + Convert.ToInt32(genId);
        }
        else {
            if (Request["Privacy"] != null)
            {

                if (LbgenID.Text == "")
                {
                    Upd_TermOrPrivacy(Convert.ToInt32(Session["siteId"]), 1, genId);
                    Response.Redirect("mnt_site.aspx");
                }
                else {
                    Upd_GenX(Convert.ToInt32(LbgenID.Text), Convert.ToString(Request["txtlandTitle"]), Convert.ToString(Request["elmlandpage"]), image_path);
                    Response.Redirect("mnt_site.aspx" + image_path);
                }
            }
            else {
                if (LbgenID.Text == "")
                {
                    Upd_TermOrPrivacy(Convert.ToInt32(Session["siteId"]), 2, genId);
                    Response.Redirect("mnt_site.aspx");
                }
                else
                {
                    Upd_GenX(Convert.ToInt32(LbgenID.Text), Convert.ToString(Request["txtlandTitle"]), Convert.ToString(Request["elmlandpage"]), image_path);
                    Response.Redirect("mnt_site.aspx");
                }
            }
        }

        // txt_link.Attributes.Add("readonly","0");

    }
    protected void btn_NewAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_site.aspx?NewAdd=" + true);
    }
}
