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

public partial class mnt_FeaturedBrands : FeatureBrands
{
    public string strFolder = "Images";
    private int contcat = 0;
    private int contTitle = 0;
    DataSet data2 = new DataSet();
    int[] vector = new int[5000];
    int[] ids = new int[5000];
    private int ContBrands = 0;
    private int contpos = 1;
    private int positions = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        Session["CurrentPage"] = "mnt_FeaturedBrands.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }

        #region First Load
        string posi ="";        
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        DataSet data = new DataSet();
          bool fila = false;
        data = getAllFeatureBrands(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]));
        sb.AppendLine("<table id='canBeSorted' width=\"578\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
        sb.AppendLine("<tr align=\"center\"> <td width=\"4\"> &nbsp;</td>");
        sb.AppendLine("<td width=\"46\" height=\"40\" class=\"class_LineaVTabla\">Slot</td>");
        sb.AppendLine("<td width=\"4\"></td>");
        sb.AppendLine("<td width=\"153\" class=\"class_LineaVTabla\">Product Title</td>");
        sb.AppendLine("<td width=\"4\"></td>");
        sb.AppendLine("<td width=\"65\" align=\"center\">&nbsp;</td></tr>");
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (Convert.ToInt32(row["FeatOrdPos"]) == 0)
                {
                    posi = "";
                }
                 else
                 {
                     posi = Convert.ToString(row["FeatOrdPos"]);
                     vector[contcat] = Convert.ToInt32(posi);
                }
                if (fila == true)
                { sb.AppendLine("<tr onMouseOver='this.style.cursor=&#39;move&#39;' style='cursor: move; '>"); }
                else { sb.AppendLine("<tr class=\"fila\" onMouseOver='this.style.cursor=&#39;move&#39;' style='cursor: move; '>"); }
                sb.AppendLine("<td>&nbsp;</td><td align=\"center\"class=\"class_LineaVTabla\"><input type=\"text\" style=\"background-color:transparent; border:0px;width:15px\" value=\"" + row["FeatOrdPos"] + "\" name=\"txtCaIt" + row["FeatOrdPos"] + "\" id=\"txtCaIt" + row["FeatOrdPos"] + "\"  /><input id=\"idTheater" + positions + "\" value=\"" + row["FeatId"] + "\" name=\"idTheater" + positions + "\" type=\"hidden\" /></td>"); 
                sb.AppendLine("<td></td>");
                sb.AppendLine("<td align=\"left\" class=\"class_LineaVTabla\">" + searchTitle(Convert.ToInt32(row["TitleID"])) + "</td>");
                sb.AppendLine("<td></td>");
                sb.AppendLine("<td align=\"left\"><a href='mnt_FeaturedBrands.aspx?Delete=" + row["FeatID"] + "&acc=delete&press=send' class=\"enlace\"><img src=\"images/btn_delete.png\" border=\"0\"/>&nbsp;Delete</a></td>");
                sb.AppendLine("<td align=\"left\">&nbsp;</td></tr>");
                if (fila == true)
                { fila = false; }
                else
                { fila = true; }
                contcat++;
                ContBrands++;
                contpos++;
                positions++;
            }

        }
        sb.AppendLine("</TABLE>");

        div_FeatureProducts.InnerHtml = sb.ToString();
        #endregion
        #region Search Options
        if (Request["AddNew"] != null)
        {
            System.Text.StringBuilder sbSearch = new System.Text.StringBuilder();

            sbSearch.AppendLine("Search: ");
            if (Request["SearchId"] != null)
            {
                sbSearch.AppendLine("<input id=\"txtCriteria\" name=\"txtCriteria\" runat=\"server\" value=\"" + Convert.ToString(Request["SearchID"]) + "\" />");
            }
            else
            {
                sbSearch.AppendLine("<input id=\"txtCriteria\" name=\"txtCriteria\" runat=\"server\" />");
            }
            btnSearch.Visible = true;
            div_AddFP.InnerHtml = sbSearch.ToString();
        }
        #endregion

        #region Search by criteria

        if (Request["SearchId"] != null)
        {
            System.Text.StringBuilder sbNew = new System.Text.StringBuilder();
            string a = Convert.ToString(Request["type"]);
            if ((Convert.ToString(Request["type"])) == "text")
            {
                data2 = getFeatureBrandsByTitle(Convert.ToString(Request["SearchID"]));
            }
            else
            {
                data2 = getFeatureBrandsById(Convert.ToInt32(Request["SearchID"]));
            }
            
           // sbNew.AppendLine("<br/>Position:");
            sbNew.AppendLine("<input id=\"txtPosition\" type=\"hidden\" readonly value=\""+contpos+"\" name=\"txtPosition\"/>");
            sbNew.AppendLine("<table width=\"578\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
            sbNew.AppendLine("<tr align=\"center\"> <td width=\"4\"> &nbsp;</td>");
            sbNew.AppendLine("<td width=\"46\" height=\"40\" class=\"class_LineaVTabla\">Slot</td>");
            sbNew.AppendLine("<td width=\"4\"></td>");
            sbNew.AppendLine("<td width=\"153\" class=\"class_LineaVTabla\">Product Title</td>");
            sbNew.AppendLine("<td width=\"4\"></td>");
            sbNew.AppendLine("<td width=\"65\" align=\"center\">&nbsp;</td></tr>");
            foreach (DataTable table2 in data2.Tables)
            {
                foreach (DataRow row in table2.Rows)
                {
                    if (fila == true)
                { sbNew.AppendLine("<tr>"); }
                else { sbNew.AppendLine("<tr class=\"fila\">"); }
                    sbNew.AppendLine("<td>&nbsp;</td><td align=\"center\"class=\"class_LineaVTabla\"><input type=\"checkbox\" value=\"" + row["PubID"] + "\" name=\"chk" + contTitle + "\" /></td>"); 
                sbNew.AppendLine("<td></td>");
                sbNew.AppendLine("<td align=\"left\" class=\"class_LineaVTabla\">" + row["PubName"].ToString() + "</td>");
                sbNew.AppendLine("<td></td>");
                sbNew.AppendLine("<td align=\"left\">" + row["PubCode"].ToString() + "</td>");
                sbNew.AppendLine("<td align=\"left\">&nbsp;</td></tr>");
                if (fila == true)
                { fila = false; }
                else
                { fila = true; }
                contTitle++;
                }

            }
            sbNew.AppendLine("</TABLE>");
            if (contTitle > 0)
            {
                btnCancelFeat.Visible = true;
                btnSaveFeat.Visible = true;
                sb.AppendLine("<input name=\"contTitle\" type=\"hidden\" value=\"" + contTitle + "\" />");
                div_SearchResult.InnerHtml = sbNew.ToString();
                div_saveAndcancel.Visible = true;
                search_result.Visible = true;
            }
        }
        #endregion
        #region Delete
        if (Request["Delete"] != null)
        {

            deleteFeatureBrands(Convert.ToInt32(Request["Delete"]));
            Response.Redirect("mnt_FeaturedBrands.aspx");
        }
        #endregion

        #region Edit Positions
        if (Request["UpdPosition"] != null)
        {

            for (int i = 1; i < positions; i++)
            {
                Upd_BrandsPositions(Convert.ToInt32(Request["idTheater" + i]), Convert.ToInt32(Request["txtCaIt" + i]));
            }
            Response.Redirect("mnt_FeaturedBrands.aspx");
        }
        #endregion 
    }
    #region Buttons methods
    //-------------------------------------------------------
    protected void btnAddFP_Click(object sender, EventArgs e)
    {
         Response.Redirect("mnt_FeaturedBrands.aspx?AddNew=" + true);       

    }
    //-------------------------------------------------------
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
        bool flag = false;
        string search = "";
        int cont = 0;
        string texttosearch = Convert.ToString(Request["txtCriteria"]);
        if ((texttosearch != "")&&(texttosearch.Length >2))
        {
            search = Convert.ToString(Request["txtCriteria"]);
            if (validation(search) == true)
            {
                int id = Convert.ToInt32(Request["txtCriteria"].ToString());
                Response.Redirect("mnt_FeaturedBrands.aspx?AddNew=" + true + "&SearchId=" + id + "&type=number");

            }
            else
            {
                Response.Redirect("mnt_FeaturedBrands.aspx?AddNew=" + true + "&SearchId=" + search + "&type=text");
            }
        }

    }
    //-------------------------------------------------------
    protected void btnCancelFeat_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_FeaturedBrands.aspx");

    }
    //-------------------------------------------------------
    protected void btnSaveFeat_Click(object sender, EventArgs e)
    {
        string UploadedFile = FUimage.PostedFile.FileName;
        int ExtractPos = UploadedFile.LastIndexOf("\\") + 1;
        string addres = "";
        if (UploadedFile != "")
        {
            
            //to retrieve only Filename from the complete path
            String UploadedFileName = UploadedFile.Substring(ExtractPos, UploadedFile.Length - ExtractPos);
            addres = Convert.ToString(UploadedFileName);
            String lowuploadimage = UploadedFileName.ToLower();
            if (!(lowuploadimage.Contains(".jpg") || lowuploadimage.Contains(".png") || lowuploadimage.Contains(".gif")))
            {
                // lbCurrentImage.Text = "Please select a correct image format ( .jpg  .gif  .png )";
                // .InnerHtml = "Please select a correct image format ( .jpg  .gif  .png )";
                return;
            }

            FUimage.PostedFile.SaveAs(Request.PhysicalApplicationPath + "\\" + strFolder + "\\" + UploadedFileName);
           
        }

        hidePath.Value = Convert.ToString(addres);
        string csub = Request["contTitle"];
        if (contTitle != null)
        {
            if (Convert.ToInt32(contTitle) > 0)
            {
                SaveFeatureProducts(Convert.ToInt32(contTitle));
            }
        }
    }
    //------------------------------------------------------------
    private void SaveFeatureProducts(int count)
    {
        string vsub;
        int isub;
        string search = Convert.ToString(Request["txtPosition"]);
        if (validationpos(search) == true )
        {
            for (int i = 0; i <= count; i++)
            {
                vsub = Request["chk" + i];
                isub = Convert.ToInt32(vsub);
                if (vsub != null )
                {
                    if (validateId(isub) == false)
                    {
                        FeatureBrands feat = new FeatureBrands(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), isub,hidePath.Value,Convert.ToInt32(search), true);
                        addFeatureB(feat);
                        Response.Redirect("mnt_FeaturedBrands.aspx");
                    }
                    else {
                        lbPositionError.Text = "**This product is already feature product**";
                        lbPositionError.Visible = true;
                    }
                }
            }
        }
        else {
            lbPositionError.Text = "**"+search+"is an invalid position**";
            lbPositionError.Visible = true;
        }        
        
    }
    //------------------------------------------------------------
    #endregion
    protected string searchTitle(int titleID)
    {

        DataSet title = new DataSet();
        string tittle = "";
        title = getFeatureBrandsById(titleID);

        foreach (DataTable table in title.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                tittle = Convert.ToString(row["PubName"]);
            }
        }
        return tittle;

    }


    protected bool validation(string search)
    {
        bool flag = false;
        int cont = 0;
        int leng = search.Length;
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
    protected bool validationpos(string search)
    {
        bool flag = false;
        int cont = 0;
        int position = 0;
        int leng = search.Length;
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
            position = Convert.ToInt32(search);
            if ( position < 20)
            {
                flag = true;
                for (int j = 0; j < vector.Length; j++)
                {
                    if (vector[j] == position)
                    {
                        flag = false;
                    }
                }
            }
        }
        return flag;
    }
    protected bool validateId(int id)
    {
        bool flag = false;
        for (int i = 0; i < ids.Length; i++)
        {
            if (id == ids[i])
            {
               flag = true;

            }
        }
        return flag;
   }
    protected void btn_upload_Click(object sender, EventArgs e)
    {
        
    }

    public bool ThumbnailCallback()
    {
        return false;

    }
    protected void btnCancelFeat_Click1(object sender, EventArgs e)
    {
        Response.Redirect("mnt_FeaturedBrands.aspx");

    }

}
