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

public partial class CMS_mnt_Specials : Specials
{
    private int contTitle = 0;
    DataSet data2 = new DataSet();
    int[] subsIds = new int[50];
    int[] vector = new int[100];
    int[] ids = new int[100];
    private int contnewpos = 1;
    private string IndexPos = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        Session["CurrentPage"] = "mnt_Specials.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }

        string posi = "";



        IndexPos = Request["SubjID"];
        ddlSubject_Load();
        div_SubjectSelection.Visible = true;
        
        btnAddFP.Visible = true;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        DataSet data = new DataSet();

        if (Request["SubjId"] != null)
        {
            //div_SubjectSelection.Visible = true;
            data = getAllSpecialsBySubjectID(Session["siteId"].ToString(), Session["contId"].ToString(), Convert.ToString(Request["SubjId"].ToString()));

                data = getAllSpecialsBySubjectID(Session["siteId"].ToString(), Session["contId"].ToString(), Convert.ToString(Request["SubjId"].ToString()));

            //ddlSubject.SelectedIndex = Convert.ToInt32(Request["Index"]);            

        }
        else
        {
      
                data = getAllSpecialsBySubjectID(Session["siteId"].ToString(), Session["contId"].ToString(), Convert.ToString(0));
           
        }
        int concat = 0;
        bool fila = false;             
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
                if (Convert.ToInt32(row["SpecOrdPos"]) == 0)
                {
                    posi = "";
                }
                else
                {
                    posi = Convert.ToString(row["SpecOrdPos"]);
                    vector[concat] = Convert.ToInt32(posi);
                }
                if (fila == true)
                {
                    sb.AppendLine("<tr>");
                }
                else
                {
                    sb.AppendLine("<tr class=\"whiteTable\"> ");
                }
                sb.AppendLine("<td class=\"tableSelect\"><a href='mnt_Specials.aspx?Delete=" + row["SpecId"] + "&acc=delete&press=send'  onclick=\"return confirm('Do you want to delete this Special?');\"><img src=\"imagesCss/bulletTable.jpg\"/></a></td>");
                sb.AppendLine("<td><img src=\"\" /></td>");
                sb.AppendLine("<td class=\"productName\"><p>" + searchTitle(Convert.ToInt32(row["TitleID"])) + "</p></td>");
                sb.AppendLine("<td class=\"item\"><p>" + row["TitleID"].ToString() + "</p></td>");
                sb.AppendLine("<td class=\"descriptionTable\"><p></p></td>");
                sb.AppendLine("<td class=\"priceTable\"><p></p></td></tr>");                
                concat++;
                contnewpos++;
                if (fila == true)
                { fila = false; }
                else
                { fila = true; }
            }
        }
        sb.AppendLine("</TABLE>");
        div_Specials.InnerHtml = sb.ToString();


        if (Request["AddNew"] != null)
        {

            System.Text.StringBuilder sbSearch = new System.Text.StringBuilder();

            sbSearch.AppendLine("Search <br>");
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

        #region Search by criteria
        if (Request["SearchId"] != null)
        {
            System.Text.StringBuilder sbNew = new System.Text.StringBuilder();
            string a = Convert.ToString(Request["type"]);
            if ((Convert.ToString(Request["type"])) == "text")
            {
                data2 = getSpecialByTitle(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]),Convert.ToString(Request["SearchID"]));
            }
            else
            {
                data2 = getSpecialById(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]),Convert.ToInt32(Request["SearchID"]));
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
                    else
                    {
                        sbNew.AppendLine("<tr class=\"whiteTable\"> ");
                    }
                    sbNew.AppendLine("<td class=\"tableSelect\"><input type=\"checkbox\" value=\"" + row["TitleID"] + "\" name=\"chk" + contTitle + "\" /> </td>");
                    sbNew.AppendLine("<td><img src=\"\" /></td>");
                    sbNew.AppendLine("<td class=\"productName\"><p>" + row["Title"].ToString() + "</p></td>");
                    sbNew.AppendLine("<td class=\"item\"><p>" + row["TitleID"].ToString() + "</p></td>");
                    sbNew.AppendLine("<td class=\"descriptionTable\"><p></p></td>");
                    sbNew.AppendLine("<td class=\"priceTable\"><p></p></td></tr>");
                    contTitle++;
                    if (fila == true)
                    {
                        fila = false;
                    }
                    else
                    {
                        fila = true;
                    }    
                    if (contTitle < 100)
                    {
                        ids[contTitle] = Convert.ToInt32(row["PubID"]);
                    }
                    sbNew.AppendLine("</TD></TR>");
                    contTitle++;
                }

            }
            sbNew.AppendLine("</TABLE>");
            if (contTitle > 0)
            {
                btnCancelFeat.Visible = true;
                btnSaveFeat.Visible = true;
                sbNew.AppendLine("<input name=\"contTitle\" type=\"hidden\" value=\"" + contTitle + "\" />");
                div_SearchResult.InnerHtml = sbNew.ToString();
            }
        }
        #endregion

        #region Delete
        if (Request["Delete"] != null)
        {

            deleteSpecials(Convert.ToInt32(Request["Delete"]));
            Response.Redirect("mnt_Specials.aspx?Mainsite=" );
        }
        #endregion

    }
    #region Buttons methods
    //-------------------------------------------------------
    protected void btnAddFP_Click(object sender, EventArgs e)
    {


        Response.Redirect("mnt_Specials.aspx?AddNew=" + true + "&SubjID=" + Request["SubjID"] + "&Subject=" + Request["Subject"] + "&Index=" + Request["SubjID"] + "&Mainsite=" );
        //SubjID=21&Subject=1 to 1 Learning&Index=1

    }
    //-------------------------------------------------------
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        bool flag = false;
        string search = "";
        int cont = 0;
        if (Convert.ToString(Request["txtCriteria"]) != "")
        {
            search = Convert.ToString(Request["txtCriteria"]);
            for (int i = 0; i < search.Length; i++)
            {
                if (((search[i] == '0') || (search[i] == '1') || (search[i] == '2') || (search[i] == '3') || (search[i] == '4')
                    || (search[i] == '5') || (search[i] == '6') || (search[i] == '7') || (search[i] == '8') || (search[i] == '9')) && (i > 0))
                {
                    cont++;
                }
                if (cont == (search.Length - 1))
                {
                    flag = true;
                }
            }

            if (flag == true)
            {
                int id = Convert.ToInt32(Request["txtCriteria"].ToString());
                Response.Redirect("mnt_Specials.aspx?AddNew=" + true + "&SearchId=" + id + "&type=number &SubjID=" + Request["SubjID"] + "&Subject=" + Request["Subject"] + "&Index=" + Request["SubjID"] + "&Mainsite=" );

            }
            else
            {
                Response.Redirect("mnt_Specials.aspx?AddNew=" + true + "&SearchId=" + search + "&type=text&SubjID=" + Request["SubjID"] + "&Subject=" + Request["Subject"] + "&Index=" + Request["SubjID"] + "&Mainsite=" );
            }
        }

    }
    //-------------------------------------------------------
    protected void btnCancelFeat_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Specials.aspx&Mainsite=" );

    }
    //-------------------------------------------------------
    protected void btnSaveFeat_Click(object sender, EventArgs e)
    {
        string csub = Request["contTitle"];
        if (contTitle != 0)
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
        int subject = 0;
        if(Convert.ToString(Request["SubjID"])!="")
        {
            subject = Convert.ToInt32(Request["SubjID"]);
        }
        string vsub;
        int isub;
        string search = Convert.ToString(Request["txtPosition"]);
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
                        Specials fea = new Specials(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), subject , isub, isub, Convert.ToInt32(search), true);
                        addSpecial(fea);
                    }
                    else
                    {
                        lbPositionError.Text = "**This product is already feature product**";
                        lbPositionError.Visible = true;
                    }
                }
            }
        }

            Response.Redirect("mnt_Specials.aspx?SubjID=" + Request["SubjID"] + "&Subject=" + Request["Subject"] + "&Index=" + Request["SubjID"]);


    }
    //------------------------------------------------------------
    #endregion

    protected string searchTitle(int titleID)
    {

        DataSet title = new DataSet();
        string tittle = "";
        title = getSpecialById(Convert.ToInt32(Session["siteId"]),Convert.ToInt32(Session["contId"]),titleID);

        foreach (DataTable table in title.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                tittle = Convert.ToString(row["Title"]);
            }
        }
        return tittle;

    }
    //------------------------------------------------------------
 
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
    //-----------------------------------
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
    //-------------------------------------
    protected void ddlSubject_Load()
    {
        DataSet Subject = new DataSet();
        Subject = getAllSubject(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]));

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine(" option value=\"0\"selected=\"selected\">None</option>");
        foreach (DataTable table in Subject.Tables)
        {
            foreach (DataRow row2 in table.Rows)
            {

                int task = Convert.ToInt32(row2["active"]);
                if (task != 0)
                {
                    sb.AppendLine("<option value=\"" + row2["SubjId"].ToString() + "\" " + (row2["SubjId"].ToString() == IndexPos ? "selected=\"selected\"" : "") + ">" + row2["cat"].ToString() + "</option>");
                }

            }
        }
        PLaceHolder_Cmb_Test.Controls.Add(new LiteralControl(sb.ToString()));
    }


   
}
