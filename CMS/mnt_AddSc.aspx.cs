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

static class RegexUtil
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
 

public partial class CMS_mnt_AddSc : Adds
{
    public string strFolder = "Images";
    public string str_TinyMCE = "";
    private int cont, addscont = 0;
    public string linktopage1, PageTitle, PageContent, GenedLink, Position, radioType, TitleGen, ContGen, Gen_Image, id_landing_page_global = "";
    public bool has_landing_page = false;
    public bool Details = false;
    public bool AddEdit = false;
    public bool Information = false;

   
    public string xtractNums(string str){
        return RegexUtil.MatchKey(str);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["CurrentPage"] = "mnt_AddSc.aspx";
        bool fila = false;
        DataSet data = new DataSet();
        int id_landing_page = -1;
        MainContentGeneric generic_X = new MainContentGeneric();

        miniSitemap.CMS_mnt_SiteMap minism = (miniSitemap.CMS_mnt_SiteMap)(Page.LoadControl("mnt_SiteMap.ascx"));
        PlaceHolderMinisitemap.Controls.Add(minism);
        Addins addins = new Addins();
        str_TinyMCE = addins.GettinyMCE(); 
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (Request["pages"] != null)
        {
            data = getAdds(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]), 1);
        }
        else
        {
            opciones.Visible = false;
            data = getAdds(Convert.ToInt32(Session["siteId"]),0, 2);
            btn_Saveadd.Text = ".";
        }
        fila = false;
        sb.AppendLine(" <table width=\"512\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\"><tr>");
        sb.AppendLine("<td width=\"4\" height=\"10\">&nbsp;</td>");
        sb.AppendLine("<td width=\"40\" class=\"class_LineaVTabla\">&nbsp;</td>");
        sb.AppendLine("<td width=\"269\" class=\"class_LineaVTabla\">&nbsp;</td> ");
        sb.AppendLine("<td width=\"80\" class=\"class_LineaVTabla\" align=\"center\">&nbsp;</td>");
        sb.AppendLine("</tr>");
        sb.AppendLine("<tr> <td>&nbsp;</td>");
        sb.AppendLine("<td class=\"class_LineaVTabla\">Select</td>");
        sb.AppendLine("<td class=\"class_LineaVTabla\">Name</td>");
        sb.AppendLine("<td  class=\"class_LineaVTabla\" align=\"center\">&nbsp;</td>");
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
                sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\">" + row2["AddsId"].ToString() + "</td>");
                sb.AppendLine("<td align=\"left\" class=\"class_LineaVTabla\">" + row2["AddsImage"].ToString() + "</td>");
                if (Request["pages"] != null)
                {
                    sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><a href=mnt_AddSc.aspx?pages=true&EditAddId=" + row2["AddsId"] + "><img src=\"images/btn_Edit.png\" border=\"0\" />Edit</a></td>");
                    sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><a href=mnt_AddSc.aspx?pages=true&DeleteId=" + row2["AddsId"] + "><img src=\"images/btn_delete.png\" border=\"0\" />Delete</a></td>");

                }else{
                    sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><a href=mnt_AddSc.aspx?EditAddId=" + row2["AddsId"] + "><img src=\"images/btn_Edit.png\" border=\"0\" />Edit</a></td>");
                    sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><a href=mnt_AddSc.aspx?DeleteId=" + row2["AddsId"] + "><img src=\"images/btn_delete.png\" border=\"0\" />Delete</a></td>");
                }
                sb.AppendLine(" </tr>");
                if (fila == true)
                {
                    fila = false;
                }
                else
                {
                    fila = true;
                }
                addscont++;
            }
        }
        sb.AppendLine("</TABLE>");
        div_displayadds.InnerHtml = sb.ToString();

        #region editAdds 
        if (Request["EditAddId"] != null)
        {
            AddEdit = true;
            sb = new System.Text.StringBuilder();
            data = new DataSet();
            data = Get_Adds_By_Id(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Request["EditAddId"]));
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (lbAdd.Text == "")
                    {
                    lbAdd.Text = row["AddsImage"].ToString();
                    }
                    linktopage1 = row["AddsLink"].ToString();
                    GenedLink = row["AddsAlt"].ToString();
                    Session["addIdM"] = row["AddsId"].ToString();
                }
            }
            if (Request["pages"] != null)
            {
                btn_Saveadd.Text = "..";
            }
            else
            {
                btn_Saveadd.Text = ".";
            }
            load_landing_page(ref data, generic_X);
            div_editNewadd.Visible = true;
            div_addButtons.Visible = true;
            div_adduploadBTn.Visible = true;

        }
        #endregion adds

        #region new Add
        if (Request["NewAdd"] != null)
        {
            AddEdit = true;
            div_editNewadd.Visible = true;
            div_addButtons.Visible = true;
            div_adduploadBTn.Visible = true;
            if (Request["pages"] != null)
            {
                btn_Saveadd.Text = "..";
            }
            else
            {
                btn_Saveadd.Text = ".";
            }
            btn_Saveadd.CssClass = "class_btnSave";
        }
        #endregion

        #region delete
        if (Request["DeleteId"] != null)
        {
            Del_Adds(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Request["DeleteId"]));
            if (Request["pages"] != null)
            {
                Response.Redirect("mnt_AddSc.aspx?pages=true");
            }
            else
            {
                Response.Redirect("mnt_AddSc.aspx");
            }
        }
        #endregion
    }

    private void load_landing_page(ref DataSet data, MainContentGeneric generic_X)
    {
        string id_lp = xtractNums(linktopage1);
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
                    TitleGen = row["GeneXTitle"].ToString();
                    ContGen = row["GeneXContent"].ToString();
                    Gen_Image = row["GeneXImage"].ToString();
                }
                has_landing_page = true;
            }
        }
    }
   
    protected void btn_Saveadd_Click(object sender, EventArgs e)
    {
        int cont_id = -1;
        int ExtractPos = -1;
        int genId = -1;
        int site_id = -1;
        int site_id_m = -1;
        string image_path = "";
        string rb_image_type_main_reference = "";
        string rb_image_type_landing_reference = "";
        string rb_value_link = "";
        string ta_landing_page_content = "";
        string txt_landing_page_title = "";
        string txt_link = "";
        string txt_link_return = "";
        string UploadedFile = "";
        string url_image = "";
        string txtGeneDlinkTitle = "";

        cont_id = Convert.ToInt32(Session["contId"]);
        site_id = Convert.ToInt32(Session["siteId"]);
        site_id_m = Convert.ToInt32(Session["addIdM"]);
        txtGeneDlinkTitle = Convert.ToString(Request["txtGeneDlinkTitle"]); // Alt: Add New
        txt_link = Convert.ToString(Request["txt_link"]); // Link: Add New.
        url_image = Convert.ToString(Request["txt_image_url_landing"]); //Url image for the new langing

        #region save_landing
        //Functionality to save the landing page, in case the option is activated using the radio buttons.
        txt_link_return = Add_Update_LandingPage(cont_id, ref ExtractPos, ref genId, site_id, ref image_path, ref rb_image_type_landing_reference, ref rb_value_link, ref ta_landing_page_content, ref txt_landing_page_title, ref txt_link, ref UploadedFile, url_image);

        #endregion
        #region save_add
        Add_Update_Adds(cont_id, site_id, site_id_m, ref image_path, ref rb_image_type_main_reference, txt_link, ref url_image, txtGeneDlinkTitle);
        #endregion
        //Redirect the page once all the content is saved
        if (btn_Saveadd.Text == "..")
        {
            Response.Redirect("mnt_AddSc.aspx?pages=true");
        }
        else
        {
            Response.Redirect("mnt_AddSc.aspx");
        }
    }

    private void Add_Update_Adds(int cont_id, int site_id, int site_id_m, ref string image_path, ref string rb_image_type_main_reference, string txt_link, ref string url_image, string txtGeneDlinkTitle)
    {
        rb_image_type_main_reference = Convert.ToString(Request["rb_image_type"]);
        image_path = "";
        if (rb_image_type_main_reference == "upload")
        {
            image_path = lbAdd.Text;
        }
        else if (rb_image_type_main_reference == "url")
        {
            url_image = Convert.ToString(Request["txt_image_url"]);
            if (Regex.IsMatch(url_image, @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
            {
                image_path = url_image;
            }
        }
        if (btn_Saveadd.CssClass == "class_btnSave")
        {
            if (btn_Saveadd.Text == "..")
            {
                Add_Adds(site_id, cont_id, 1, image_path, image_path, txtGeneDlinkTitle, txt_link);
            }
            else
            {
                Add_Adds(site_id, 0, 2, image_path, image_path, txtGeneDlinkTitle, txt_link);
            }
        }
        else
        {
            Upd_Adds(site_id, site_id_m, image_path, image_path, txt_link, txtGeneDlinkTitle, "");
        }
        Session["addIdM"] = null;
    }

    private string Add_Update_LandingPage(int cont_id, ref int ExtractPos, ref int genId, int site_id, ref string image_path, ref string rb_image_type_landing_reference, ref string rb_value_link, ref string ta_landing_page_content, ref string txt_landing_page_title, ref string txt_link, ref string UploadedFile, string url_image)
    {
        image_path = Gen_Image;
        rb_value_link = Convert.ToString(Request["rb_option_new_page"]); //Value to determine if it is an internal, external or landing page link.
        if (rb_value_link == "new_landing")
        {
            rb_image_type_landing_reference = Convert.ToString(Request["rb_image_type_landing"]);
            radioType = Convert.ToString(Request["radiotipo"]);
            if (rb_image_type_landing_reference == "upload")
            {
                UploadedFile = Convert.ToString(UploadLanding.PostedFile.FileName);
                ExtractPos = UploadedFile.LastIndexOf("\\") + 1; //is an int
                if (UploadedFile != "")
                {
                    //to retrieve only Filename from the complete path
                    UploadedFile = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
                    // Save uploaded file to server at the in the Pics folder
                    lbimageinfo.Text = UploadedFile;
                    image_path = UploadedFile;
                    UploadLanding.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFile);
                }
            }
            else
            {
                if (rb_image_type_landing_reference == "url")
                {
                    if (Regex.IsMatch(url_image, @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
                    {
                        image_path = url_image;
                    }
                }
            }

            if (LbgenID.Text == "")
            {
                txt_landing_page_title = Convert.ToString(Request["txtlandTitle"]); //Landing page title
                ta_landing_page_content = Convert.ToString(Request["elmlandpage"]); //Landing page content
                if (has_landing_page)
                {
                    genId = Convert.ToInt32(id_landing_page_global);
                    updLandPage(genId, txt_landing_page_title, ta_landing_page_content, image_path);
                }
                else
                {
                    genId = addLandPage(site_id, cont_id, txt_landing_page_title, ta_landing_page_content, image_path, 1, 4);//4 recongnized as genD
                }
            }
            linktopage1 = "Generic_x.aspx?LandingId=" + Convert.ToInt32(genId);
            txt_link = linktopage1;
        }
        return txt_link;
    }

    protected void btn_New_Click(object sender, EventArgs e)
    {
        if (addscont == 2)
        {
            //berror.Text = "Maximun Capacity has been Reached";
        }
        else
        {
            if (Request["pages"] != null)
            {
                Response.Redirect("mnt_AddSc.aspx?pages="+true+"&NewAdd=" + true);
            }
            else
            {
                Response.Redirect("mnt_AddSc.aspx?NewAdd=" + true);
            }
        }
    }

    protected void btnCancelnew_Click(object sender, EventArgs e)
    {
        if (Request["pages"] != null)
        {
            Response.Redirect("mnt_AddSc.aspx?pages=true");
        }
        else {
            Response.Redirect("mnt_AddSc.aspx");
        }
    }
    public bool ThumbnailCallback()
    {
        return false;
    }

    protected void btn_cancelAdd_Click(object sender, EventArgs e)
    {
        if (Request["pages"] != null)
        {
            Response.Redirect("mnt_AddSc.aspx?pages=true");
        }
        else
        {
            Response.Redirect("mnt_AddSc.aspx");
        }
    }

    protected void btn_CancelLand_Click(object sender, EventArgs e)
    {
        if (Request["pages"] != null)
        {
            Response.Redirect("mnt_AddSc.aspx?pages=true");
        }
        else
        {
            Response.Redirect("mnt_AddSc.aspx");
        }

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
            lbAdd.Visible = true;
            lbAdd.Text = "Please select a correct image format ( .jpg  .gif  .png )";
            return;
        }
        // Save uploaded file to server at the in the Pics folder
        string addres = Convert.ToString(UploadedFileName);
        UploadNew.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
        lbAdd.Text = addres;
    }

    protected void btn_SaveLand_Click(object sender, EventArgs e)
    {
        int genId = 0;
        string image_path = "";
        if (Convert.ToString(Request["rb_image_type_landing"]) == "upload")
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
                }
                // Save uploaded file to server at the in the Pics folder
                addres = Convert.ToString(UploadedFileName);
                lbimageinfo.Text = addres;
                image_path = lbimageinfo.Text;
                UploadLanding.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
            }
            #endregion
        }
        else
        {
            if (Convert.ToString(Request["rb_image_type_landing"]) == "url")
            {
                string url_image = "";
                url_image = Convert.ToString(Request["txt_image_url_landing"]);
                if (Regex.IsMatch(url_image, @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
                {
                    image_path = url_image;
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
        linktopage1 = "Generic_x.aspx?LandingId=" + Convert.ToInt32(genId);
    }

    protected void btn_NewAdd_Click(object sender, EventArgs e)
    {
        if (addscont < 2)
        {
            if (Request["pages"] != null)
            {
                Response.Redirect("mnt_AddSc.aspx?pages=true&NewAdd=" + true);
            }
            else
            {
                Response.Redirect("mnt_AddSc.aspx?NewAdd=" + true);
            }
        }
    }
}
