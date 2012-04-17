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
using System.Text.RegularExpressions;

public partial class mnt_generic_b : Gen_B
{
    public string str_TinyMCE;
    public string strFolder = "PDFs";
    public string contentLoad = "";
    public string Title = "";
    public string printCdroCaja2 = "";  
    private int cont = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }

        if (Request["GenericId"] != null)
        {
            Session["GenericId"] = Convert.ToInt32(Request["GenericId"]);
            Session["NewPageTemplate"] = false;
        }
        Session["GenericId"] = Convert.ToInt32(Request["GenericId"]);
        #region FirstLoad
        bool fila = true;
        Addins addins = new Addins();
        str_TinyMCE = addins.GettinyMCE();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        DataSet data = new DataSet();
        data = Get_Generic_B_By_Type(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["GenericId"]));
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
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if(fila == true)
                    {sb.AppendLine("<tr class=\"fila\"> ");}else{sb.AppendLine("<tr> ");}
                    sb.AppendLine("<TD align=\"center\" class=\"class_LineaVTabla\">");
                    sb.AppendLine("" + row["GeneBOrdPos"].ToString() + "");
                    sb.AppendLine("</TD>");
                    sb.AppendLine("<TD class=\"class_LineaVTabla\">");
                    sb.AppendLine("" + row["GeneBTitle"].ToString() + "");
                    sb.AppendLine("</TD>");
                    sb.AppendLine("<TD align=\"center\">");
                    sb.AppendLine("<a class=\"enlace\" href=mnt_generic_b.aspx?GenericId=" + Session["GenericId"] + "&EditBId=" + row["GeneBId"] + "><img src=\"images/btn_Edit.png\" border=\"0\" />Edit</a></td>");
                    sb.AppendLine("<TD>");
                    sb.AppendLine("<a href=mnt_generic_b.aspx?GenericId=" + Session["GenericId"] + "&DeleteId=" + row["GeneBId"] + " onclick=\"return confirm('Do you want to continue?  ');\" class=\"enlace\"><img src=\"images/btn_delete.png\" border=\"0\">Delete</a></td>");
                    sb.AppendLine("</TR>");
                    cont++;
                    if (fila == true)
                    { fila = false; }
                    else 
                    { fila = true; }
                }
            }
            sb.AppendLine("</TABLE>");  
            if ((cont != 0)|| (Convert.ToBoolean(Session["NewPageTemplate"])==false))
            {
                div_NewInfo.InnerHtml = sb.ToString();
                if (Convert.ToInt32(Request["GenericId"]) == 0)
                {
                    Session["newTemplate"] = true;
                }
            }
            else {
                div_NewEdit.Visible = true;
                printCdroCaja2 = "style=\"display:none\"";
                btn_NewNI.Visible = false;
            }
        #endregion

        #region New
        if (Request["New"] != null)
        {
            div_NewEdit.Visible = true;
        }
        #endregion

        #region Edit
        if (Request["EditBId"] != null)
        {
            DataSet data2 = new DataSet();
            data2 = Get_GenericB_By_ID(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToInt32(Request["EditBId"]));
            foreach (DataTable table2 in data2.Tables)
            {
                foreach (DataRow row2 in table2.Rows)
                {
                    Title = row2["GeneBTitle"].ToString();
                    contentLoad = row2["GeneBContent"].ToString();
                    cb_Sharethis.Checked = Convert.ToBoolean(row2["GeneBShare"]);
                    lbDate.Text = row2["GeneBDate"].ToString();
                    lbl_error.Text = row2["GeneBFIle"].ToString();
                    lbl_error.Visible = true;
                }
            }
            div_NewEdit.Visible = true;
        }
        #endregion

        if (Request["Deleteid"] != null)
        {
            Delete_Generic_B(Convert.ToInt32(Request["Deleteid"]));
            Response.Redirect("mnt_generic_b.aspx?GenericId=" + Convert.ToInt32(Session["GenericId"]));
        }
    }
    protected void btn_NewNI_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_generic_b.aspx?New=" + true + "&GenericId=" + Session["GenericId"]);
    }
    protected void btn_CancelNI_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Generics.aspx?Generic=1");
    }

    protected void btn_SaveIN_Click(object sender, EventArgs e)
    {
        string path = "";
        if (Convert.ToString(Request["rb_image_type"]) == "upload")
        {
            #region Upload
            string uplopath = "";
            String UploadedFile = FUnewPDF.PostedFile.FileName;
            int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
            string address = "";
            if (Convert.ToString(UploadedFile) != "")
            {
                String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
                address = Convert.ToString(UploadedFileName);
                //to retrieve only Filename from the complete path
                String lowuploadimage = UploadedFileName.ToLower();
                if (!(lowuploadimage.Contains(".pdf")))
                {
                    lbl_error.Text = "Please select a correct document format ( .pdf )";
                    return;
                }
                try
                {
                    uplopath = System.IO.Path.Combine(Request.PhysicalApplicationPath, strFolder);
                    FUnewPDF.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\PDFs\\" + UploadedFileName);
                }
                catch (Exception ex)
                {
                    Response.Write("An error occurred - " + ex.ToString());
                }
                lbl_error.Text = address;
                path = lbl_error.Text;
                hidePath.Value = Convert.ToString(address);
            }
            #endregion
        }
        else if (Convert.ToString(Request["rb_image_type"]) == "url")
        {
            string url_image = Convert.ToString(Request["txt_image_url"]);
            if (url_image != "")
            {
                if (Regex.IsMatch(url_image, @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:pdf|PDF|Pdf)$"))
                {
                    hidePath.Value = Convert.ToString(url_image);
                    path = url_image;
                }
                else
                {
                    lbl_error.Text = "Please select a correct file format ( .pdf ) and url";
                    return;
                }
            }
        }
        mjtest.Text += path;
        if (Convert.ToInt32(Session["GeneType"]) == 0)
        {
            if (Convert.ToBoolean(Session["About"]) == false)
            {
                Session["GeneType"] = 1;
            }
            else
            {
                Session["GeneType"] = 2;
            }

        }
        if (Convert.ToString(Session["GeneDefaId"]) == "")
        {
            Session["GeneDefaId"] = 0;
        }
        if (Convert.ToString(Request["txt_Title"]) != "")
        {
            if (Request["EditBId"] == null)
            {
                if(Convert.ToBoolean(Session["NewPageTemplate"])!=false)
                {
                    if (Convert.ToInt32(Session["GeneType"]) != 0)
                    {
                        if (Convert.ToBoolean(Session["About"]) == false)
                        {
                            addGenericB(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), 0, Convert.ToInt32(Session["GeneType"]), Convert.ToInt32(Session["GeneDefaId"]), Convert.ToString(Session["PageTitle"]),
                                Convert.ToString(Request["txt_Title"]), Convert.ToString(Request["elm1"]), path, 1, cb_Sharethis.Checked);
                        }
                        else
                        {
                            addGenericB(Convert.ToInt32(Session["siteId"]), 0, 0, Convert.ToInt32(Session["GeneType"]), Convert.ToInt32(Session["GeneDefaId"]), Convert.ToString(Session["PageTitle"]),
                            Convert.ToString(Request["txt_Title"]), Convert.ToString(Request["elm1"]), path, 1, cb_Sharethis.Checked);
                        }
                    }
                    Session["PageTitle"] = "";
                    Session["TemplateChose"] = false;
                    Session["NewPageTemplate"] = false;
                    Response.Redirect("mnt_Generics.aspx?Generic=" + Convert.ToInt32(Session["GeneType"]));
                }else{
                    if (Convert.ToBoolean(Session["About"]) == false)
                    {
                        addGenericB(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToInt32(Session["GenericId"]), Convert.ToInt32(Session["GeneType"]), Convert.ToInt32(Session["GeneDefaId"]), Convert.ToString(Session["PageTitle"]),
                            Convert.ToString(Request["txt_Title"]), Convert.ToString(Request["elm1"]), path, 1, cb_Sharethis.Checked);
                    }
                    else
                    {
                        addGenericB(Convert.ToInt32(Session["siteId"]), 0, Convert.ToInt32(Session["GenericId"]), Convert.ToInt32(Session["GeneType"]), Convert.ToInt32(Session["GeneDefaId"]), Convert.ToString(Session["PageTitle"]),
                        Convert.ToString(Request["txt_Title"]), Convert.ToString(Request["elm1"]), path, 1, cb_Sharethis.Checked);
                    }
                    Response.Redirect("mnt_generic_b.aspx?GenericId=" + Convert.ToInt32(Session["GenericId"]));
                } 
            }
            else
            {
                Upd_GenericB(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Request["EditBId"]), Convert.ToString(Request["txt_Title"]),Convert.ToString(Request["elm1"]), path, cb_Sharethis.Checked);
                Response.Redirect("mnt_generic_b.aspx?GenericId=" + Convert.ToInt32(Session["GenericId"]));
            }
        }
    }
}