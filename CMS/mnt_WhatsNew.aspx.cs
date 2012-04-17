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

public partial class mnt_WhatsNew : WhatsNew
{
    private int contcat = 0;
    private int contTitle = 0;
    DataSet data2 = new DataSet();
    int[] vector = new int[5000];
    int[] ids = new int[5000];
    int conthome = 0;
    int contnewpos = 1;
    int contmain = 0;
    private string strFolder = SiteConstants.imagesPath;
    Addins addin = new Addins();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }

        Session["CurrentPage"] = "mnt_WhatsNew.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }

        #region First Load
        string posi = "";
        string active = "";
        string text = "";
        bool fila = true;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        DataSet data = new DataSet();
        if (Request["Home"] != null)
        {
            data = getAllWhatsNew(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]));
            WrapperProduct.Style.Value = "margin-left:80px; float:left";
            Selection_Area.Visible = true;
        }
        else
        {
            data = Get_WhatsNewSide(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]));
        }
             sb.AppendLine("<table width=\"943\">");
            sb.AppendLine("<tr class=\"topTable\">");
            sb.AppendLine("<td width=\"50\" height=\"30\" class=\"tableSelect\">Select</td>");
            sb.AppendLine("<td width=\"95\"></td>");
            sb.AppendLine("<td width=\"230\" class=\"productName\">Product Name</td>");
            sb.AppendLine("<td width=\"78\" class=\"item\">Item #</td>");
            sb.AppendLine("<td width=\"387\" class=\"descriptionTable\">Description</td>");
            sb.AppendLine("<td width=\"87\" class=\"priceTable\">Price</td></tr>");

            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (Convert.ToInt32(row["WhatOrdPos"]) == 0)
                    {
                        posi = "";
                    }
                    else
                    {
                        posi = Convert.ToString(row["WhatOrdPos"]);
                        vector[contcat] = Convert.ToInt32(posi);
                    }
                    if (Convert.ToBoolean(row["WhatHome"]) == true)
                    {
                        active = "checked";
                        text = "remove";
                        conthome++;
                    }
                    else
                    {
                        text = "Add";
                        active = "";
                    }
                    if (fila == true)
                    { sb.AppendLine("<tr>"); }
                    else { sb.AppendLine("<tr class=\"whiteTable\">"); }
                    if (Request["Home"] != null)
                    {
                        sb.AppendLine("<td class=\"tableSelect\"><a href='mnt_WhatsNew.aspx?Home=True&DeleteId=" + row["WhatID"] + "&acc=delete&press=send' onclick=\"return confirm('Do you want to delete this Whats New?');\"><img src=\"imagesCss/bulletTable.jpg\" /></a></td>");

                    }
                    else
                    {
                        sb.AppendLine("<td class=\"tableSelect\"><a href='mnt_WhatsNew.aspx?SubjID=" + Request["SubjID"] + "&DeleteId=" + row["WhatID"] + " &acc=delete&press=send'  onclick=\"return confirm('Do you want to delete this Whats New?');\"><img src=\"imagesCss/bulletTable.jpg\"/></a></td>");

                    }
                    sb.AppendLine("<td><img src=\"" + strFolder + row["ImageTN"].ToString() + "\" /></td>");
                    sb.AppendLine("<td class=\"productName\"><p>" + searchTitle(Convert.ToInt32(row["TitleID"])) + "</p></td>");
                    sb.AppendLine("<td class=\"item\"><p>" + row["TitleID"].ToString() + "</p></td>");
                    sb.AppendLine("<td class=\"descriptionTable\"><p>" + addin.cutDescription(row["Titletext"].ToString(), 241) + "</p></td>");
                    sb.AppendLine("<td class=\"priceTable\"><p>" + row["ER_Price"].ToString() + "</p></td></tr>");
                    contcat++;
                    contnewpos++;
                    contmain++;
                    if (fila == true)
                    { fila = false; }
                    else
                    { fila = true; }
                }

            }
            sb.AppendLine("</TABLE>");

            div_FeatureProducts.InnerHtml = sb.ToString();
        #endregion
        #region Search Options
        if (Request["AddNew"] != null)
        {
            System.Text.StringBuilder sbSearch = new System.Text.StringBuilder();

            sbSearch.AppendLine("Search <br>");
            if (Request["SearchId"] != null)
            {
                sbSearch.AppendLine("<input id=\"txtCriteria\" name=\"txtCriteria\" size=\"25\" runat=\"server\" value=\"" + Convert.ToString(Request["SearchID"]) + "\" />");
            }
            else
            {
                sbSearch.AppendLine("<input id=\"txtCriteria\" name=\"txtCriteria\" size=\"25\" runat=\"server\" />");
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
                data2 = getWhatsNewByTitle(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]),Convert.ToString(Request["SearchID"]));
            }
            else
            {
                data2 = getWhatsNewById(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]),Convert.ToInt32(Request["SearchID"]));
            }
            fila = true;
            sbNew.AppendLine("<input id=\"txtPosition\" readonly value=\"" + contnewpos + "\" type=\"hidden\" name=\"txtPosition\"/>");
            sbNew.AppendLine("<table width=\"943\">");
            sbNew.AppendLine("<tr class=\"topTable\">");
            sbNew.AppendLine("<td width=\"50\" height=\"30\" class=\"tableSelect\">Select</td>");
            sbNew.AppendLine("<td width=\"95\"></td>");
            sbNew.AppendLine("<td width=\"230\" class=\"productName\">Product Name</td>");
            sbNew.AppendLine("<td width=\"78\" class=\"item\">Item #</td>");
            sbNew.AppendLine("<td width=\"387\" class=\"descriptionTable\">Description</td>");
            sbNew.AppendLine("<td width=\"87\" class=\"priceTable\">Price</td></tr>");
            foreach (DataTable table2 in data2.Tables)
            {
                foreach (DataRow row in table2.Rows)
                {
                    if (fila == true)
                    {
                        sbNew.AppendLine("<tr>");
                    }
                    else {
                        sbNew.AppendLine("<tr class=\"whiteTable\"> "); 
                    }
                    sbNew.AppendLine("<td class=\"tableSelect\"><input type=\"checkbox\" value=\"" + row["TitleID"] + "\" name=\"chk" + contTitle + "\" /> </td>");
                    sbNew.AppendLine("<td><img src=\"" + strFolder + row["ImageTN"].ToString() + "\" /></td>");
                    sbNew.AppendLine("<td class=\"productName\"><p>" + row["Title"].ToString() + "</p></td>");
                    sbNew.AppendLine("<td class=\"item\"><p>" + row["TitleID"].ToString() + "</p></td>");
                    sbNew.AppendLine("<td class=\"descriptionTable\"><p>" + addin.cutDescription(row["Titletext"].ToString(), 241) + "</p></td>");
                    sbNew.AppendLine("<td class=\"priceTable\"><p>" + row["ER_Price"].ToString() + "</p></td></tr>");

                        
                    contTitle++;
                    if (fila == true)
                    {
                        fila = false; 
                    }
                    else
                    {
                        fila = true; 
                    }                
                }

            }
            sbNew.AppendLine("</table>");
            if (contTitle > 0)
            {
                btnCancelFeat.Visible = true;
                btnSaveFeat.Visible = true;
                sb.AppendLine("<input name=\"contTitle\" type=\"hidden\" value=\"" + contTitle + "\" />");
                div_SearchResult.InnerHtml = sbNew.ToString();
            }
        }
        #endregion
        #region Delete
        if (Request["DeleteId"] != null)
        {
            deleteWhatsNew(Convert.ToInt32(Request["DeleteId"]));
            if (Request["Home"] != null)
            {
                Response.Redirect("mnt_WhatsNew.aspx?Home=True");
            }
            else
            {
                Response.Redirect("mnt_WhatsNew.aspx");
            }

            
           
        }
        #endregion               
            


    }
    #region Buttons methods
    //-------------------------------------------------------
    protected void btnAddFP_Click(object sender, EventArgs e)
    {
        if (Request["Home"] != null)
        {
            if (contmain == 3)
            {
                lbErroMax.Text = "Only 3 elements can be added as Whats New on the main site";
            }
            else
            {
                Response.Redirect("mnt_WhatsNew.aspx?Home=" + true + "&AddNew=" + true);
                
            }
        }
        else
        {
            if (contmain == 2)
            {
                lbErroMax.Text = "Only 2 elements can be added as Whats New on the Sidebar";
            }
            else
            {
                Response.Redirect("mnt_WhatsNew.aspx?AddNew=" + true);
                
            }
        }


    }
    //-------------------------------------------------------
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        bool flag = false;
        string search = "";
        int cont = 0;
        string texttosearch = Convert.ToString(Request["txtCriteria"]);

        if ((texttosearch != "") && (texttosearch.Length > 2))
        {
            search = Convert.ToString(Request["txtCriteria"]);
            if (validation(search) == true)
            {
                int id = Convert.ToInt32(Request["txtCriteria"].ToString());
                if (Request["Home"] != null)
                {
                    Response.Redirect("mnt_WhatsNew.aspx?Home=" + true + "&AddNew=" + true + "&SearchId=" + id + "&type=number");
                }
                else
                {
                    Response.Redirect("mnt_WhatsNew.aspx?AddNew=" + true);
                }

            }
            else
            {
                if (Request["Home"] != null)
                {
                    Response.Redirect("mnt_WhatsNew.aspx?Home=" + true + "&AddNew=" + true + "&SearchId=" + search + "&type=text");
                }
                else
                {
                    Response.Redirect("mnt_WhatsNew.aspx?AddNew=" + true + "&SearchId=" + search + "&type=text");
                }

            }
        }

    }
    //-------------------------------------------------------
    protected void btnCancelFeat_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_WhatsNew.aspx");

    }
    //-------------------------------------------------------
    protected void btnSaveFeat_Click(object sender, EventArgs e)
    {
        string strScript;
        strScript = "<script>";
        strScript += "return confirm('Abrir Ventana?')";
        strScript += "</script>";
        //Page.RegisterStartupScript("script",strScript);
        ClientScript.RegisterStartupScript(GetType(), "alert", strScript);

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
        int whathome = 0;
        string search = Convert.ToString(Request["txtPosition"]);
        if (Request["Home"] != null)
        {
            whathome = 1;
        }
        else
        {
            whathome = 0;
        }
        if (validationpos(search) == true)
        {
            for (int i = 0; i <= count; i++)
            {
                vsub = Request["chk" + i];
                isub = Convert.ToInt32(vsub);
                if (vsub != null)
                {
                    if (validateId(isub) == false)
                    {
                        WhatsNew feat = new WhatsNew(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), isub,whathome, Convert.ToInt32(search), true);
                        addWhatsNew(feat);
                        if (Request["Home"] != null)
                        {
                            Response.Redirect("mnt_WhatsNew.aspx?Home=" + true);
                        }
                        else
                        {
                            Response.Redirect("mnt_WhatsNew.aspx");
                        }
                    }
                    else
                    {
                        lbPositionError.Text = "**This product is already feature product**";
                        lbPositionError.Visible = true;
                    }
                }
            }
        }
        else
        {
            lbPositionError.Text = "**" + search + "is an invalid position**";
            lbPositionError.Visible = true;
        }

    }
    //------------------------------------------------------------
    #endregion

    protected string searchTitle(int titleID)
    {

        DataSet title = new DataSet();
        string tittle = "";
        title = getWhatsNewById(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]),titleID);

        foreach (DataTable table in title.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                tittle = Convert.ToString(row["Title"]);
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
            if (position < 20)
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
    protected void btnCancelFeat_Click1(object sender, EventArgs e)
    {
        if (Request["Home"] != null)
        {
            Response.Redirect("mnt_WhatsNew.aspx?Home=" + true);
        }
        else
        {
            Response.Redirect("mnt_WhatsNew.aspx");
        }
    }
}
