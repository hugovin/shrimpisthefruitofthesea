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

static class RegexUtilHighLights
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

public partial class mnt_HighLights : Highlights
{
    public string str_TinyMCE;
    public string strFolder = "Images";
    Addins addins = new Addins();
    int conthighlights = 0;
    private int contnewpos = 1;
    public string linktopage = "";
    public int EditPosition = 0;
    private int position = 1;

    public bool has_landing_page = false;
    public string add_edit_landing_page1 = "New ";
    public string landingPageTitle, landingPageContent, landingPageImage = "";
    public string id_landing_page_global = ""; //to store the id of the landing page.

    public string xtractNums(string str)
    {
        return RegexUtilHighLights.MatchKey(str);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        miniSitemap.CMS_mnt_SiteMap minism = (miniSitemap.CMS_mnt_SiteMap)(Page.LoadControl("mnt_SiteMap.ascx"));
        PlaceHolderMinisitemap.Controls.Add(minism);
       
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }

        Session["CurrentPage"] = "mnt_HighLights.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }

        str_TinyMCE = addins.GettinyMCE();
        #region FirstLoad
        bool fila = false;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        DataSet data = new DataSet();
        data = getAllHighlights(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]));
        sb.AppendLine("<table width=\"578\" id='canBeSorted' border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
        sb.AppendLine("<tr align=\"center\"><td width=\"4\">&nbsp;</td>"); 
        sb.AppendLine("<td width=\"46\" height=\"40\"class=\"class_LineaVTabla\">Slot</td>");  
        sb.AppendLine("<td width=\"4\"></td>");       
        sb.AppendLine("<td width=\"153\" class=\"class_LineaVTabla\">Title</td>");  
        sb.AppendLine("<td width=\"4\"></td>");  
        sb.AppendLine("<td width=\"213\" class=\"class_LineaVTabla\">Link</td>");  
        sb.AppendLine("<td width=\"65\" align=\"center\">&nbsp;</td>");  
        sb.AppendLine("<td width=\"52\" align=\"center\">&nbsp;</td></tr>");  

        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (fila == true)
                { sb.AppendLine("<tr onMouseOver='this.style.cursor=&#39;move&#39;' style='cursor: move; '>"); }
                else { sb.AppendLine("<tr class=\"fila\" onMouseOver='this.style.cursor=&#39;move&#39;' style='cursor: move; '>"); }
                sb.AppendLine("<td>&nbsp;</td>");
                sb.AppendLine("<td align=\"center\"class=\"class_LineaVTabla\"><input type=\"text\" style=\"background-color:transparent; border:0px;width:15px\" value=\"" + row["HighOrdPos"] + "\" name=\"txtCaIt" + row["HighOrdPos"] + "\" id=\"txtCaIt" + row["HighOrdPos"] + "\"  /><input id=\"idTheater" + position + "\" value=\"" + row["HighId"] + "\" name=\"idTheater" + position + "\" type=\"hidden\" /></td>"); 
                sb.AppendLine("<td></td>");  
                sb.AppendLine("<td align=\"left\" class=\"class_LineaVTabla\">" + row["HighTitle"].ToString() + " </td> "); 
                sb.AppendLine("<td></td> "); 
                sb.AppendLine("<td align=\"left\" class=\"class_LineaVTabla\"" + row["HighLink"].ToString() + "</td> "); 
                sb.AppendLine("<td align=\"center\" class=\"class_LineaVTabla\"><a href='mnt_HighLights.aspx?HighlightId=" + row["HighId"] +"&acc=delete&press=send'  class=\"enlace\"><img src=\"images/btn_Edit.png\" border=\"0\" />Edit</a></td> "); 
                sb.AppendLine("<td align=\"left\"><a href='mnt_HighLights.aspx?DeleteId=" + row["HighId"] +"&acc=delete&press=send'  class=\"enlace\"><img src=\"images/btn_delete.png\" border=\"0\"/>&nbsp;Delete</a></td> "); 
                sb.AppendLine("<td width=\"1\" align=\"left\">&nbsp;</td></tr>");                 
                conthighlights++;
                contnewpos++;
                if (fila == true)
                { fila = false; }
                else
                { fila = true; }
                position++;
            }

        }
        
        sb.AppendLine("</TABLE>");
        div_HiglightsList.InnerHtml = sb.ToString();
        #endregion

        if (Request["NewHiglight"] != null)
        {
            div_neweditHighLight.Visible = true;
        }

        if (Request["DeleteId"] != null)
        {
            deleteHighlights(Convert.ToInt32(Request["DeleteId"]));
            Response.Redirect("mnt_HighLights.aspx");
        }

        if (Request["HighlightId"] != null)
        {
            highidHide.Value = Convert.ToString(Request["HighlightId"]);
            DataSet data2 = new DataSet();
            data2 = getHighLight_By_id(Convert.ToInt32(highidHide.Value));
            if (Request["Edithigh"] == null)
            {
                foreach (DataTable table2 in data2.Tables)
                {
                    foreach (DataRow row2 in table2.Rows)
                    {                        
                        lbErrorHighlight.Text = row2["HighFile"].ToString();
                        //hidePath.Value = row2["HighFile"].ToString();
                        txt_Title.Text = row2["HighTitle"].ToString();
                        linktopage = row2["HighLink"].ToString();
                        txt_Alt.Text = row2["HighAlt"].ToString();
                        Session["HighId"] = Convert.ToInt32(Request["HighlightId"]);
                        EditPosition = Convert.ToInt32(row2["HighOrdPos"]);
                    }

                }
            }
            div_neweditHighLight.Visible = true;
            load_landing_page();
        }
        #region Edit Positions
        if (Request["UpdPosition"] != null)
        {
            for (int i = 1; i < position; i++)
            {
                Upd_HighPosition(Convert.ToInt32(Request["idTheater" + i]), Convert.ToInt32(Request["txtCaIt" + i]));
            }
            Response.Redirect("mnt_HighLights.aspx");
        }
        #endregion 
    }

    protected void load_landing_page()
    {
        DataSet data = new DataSet();
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
    
    //-------------------------------------------------------------

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_HighLights.aspx");
    }
    //-------------------------------------------------------------
    protected void btn_New_Click(object sender, EventArgs e)
    {
        if (conthighlights < 3)
        {           
            Response.Redirect("mnt_HighLights.aspx?NewHiglight=" + true);
        }
        else {
            lbmaxhigh.Text = "Maximun capacity of this are has been reached";
        }
    }
    //-------------------------------------------------------------

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string imagePath = lbErrorHighlight.Text;
        linktopage = Convert.ToString(Request["txt_link"]);
        if (Convert.ToString(Request["rb_image_type"]) == "upload")
        {
            #region Upload first
            String UploadedFile = FUimage.PostedFile.FileName;
            if (Convert.ToString(UploadedFile) != "")
            {
                int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
                String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
                string addres = Convert.ToString(UploadedFileName);
                String lowuploadimage = UploadedFileName.ToLower();
                if (!(lowuploadimage.Contains(".jpg") || lowuploadimage.Contains(".png") || lowuploadimage.Contains(".gif")))
                {
                    return;
                }
                FUimage.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
                imagePath = addres;
                hidePath.Value = Convert.ToString(addres);
            }
            #endregion
        }
        else if (Convert.ToString(Request["rb_image_type"]) == "url")
        {
            imagePath = Convert.ToString(Request["txt_image_url"]);
        }
        if (txt_Title.Text != "")
        {
            if (Session["HighId"] == null)
            {
                Highlights hig = new Highlights(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), 0, txt_Title.Text, txt_Alt.Text, imagePath, linktopage, contnewpos, 1);
                addHighlights(hig);
                Response.Redirect("mnt_HighLights.aspx");

            }else{
                saveLandingPage();
                Highlights hig = new Highlights(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToInt32(Session["HighId"]), txt_Title.Text, txt_Alt.Text, imagePath, linktopage, Convert.ToInt32(Request["hidepos"]), 1);
                updateHighlights(hig);
                Session["HighId"] = null;
                Response.Redirect("mnt_HighLights.aspx");
            }
        }
    }

    public bool ThumbnailCallback()
    {
        return false;
    }
    
    protected void saveLandingPage()
    {
        string imageLandingPath = Convert.ToString(Request["txtImagePath"]);
        if (Convert.ToString(Request["radio"])=="landing")
        {
            int genId = 0;
            if (Convert.ToString(Request["rb_image_typeLanding"]) == "upload")
            {
                #region Upload
                String UploadedFile = UploadLanding.PostedFile.FileName;
                if (Convert.ToString(UploadedFile) != "")
                {
                    int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
                    string addres = landingPageImage;
                    String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
                    String lowuploadimage = UploadedFileName.ToLower();
                    if (!(lowuploadimage.Contains(".jpg") || lowuploadimage.Contains(".png") || lowuploadimage.Contains(".gif")))
                    {
                        return;
                    }
                    addres = Convert.ToString(UploadedFileName);
                    UploadLanding.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
                    imageLandingPath = addres;
                }
                #endregion
            }
            else if (Convert.ToString(Request["rb_image_typeLanding"]) == "url" && Convert.ToString(Request["txt_image_url_landing"])!="")
            {
                imageLandingPath = Convert.ToString(Request["txt_image_url_landing"]);
            }
            genId = addLandPage(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToString(Request["txtlandTitle"]), Convert.ToString(Request["elmlandpage"]), imageLandingPath, 1, 3);//3 MEANS HIGHLIGHTS ON GENERIC TABLE
            linktopage = "Generic_x.aspx?LandingId=" + Convert.ToInt32(genId);
        }
    }
}
