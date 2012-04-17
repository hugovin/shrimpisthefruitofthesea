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

public partial class mnt_Subjects : Subjects
{
    private int contcat = 0;
    private int contSub = 0;
    private int catID = 0;
    public bool subItems = false;
    DataSet data = new DataSet();
    DataSet data2 = new DataSet();
    private int position = 1;

    //------------------------------------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        Session["CurrentPage"] = "mnt_Subjects.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }
        #region First Load
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        data = getAllSubject(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]));
        bool fila = false;
        sb.AppendLine(" <table width=\"512\" id='canBeSorted' border=\"0\" cellspacing=\"0\" cellpadding=\"2\"><tr>"); 
         sb.AppendLine("<td width=\"4\" height=\"10\">&nbsp;</td>"); 
         sb.AppendLine("<td width=\"40\" class=\"class_LineaVTabla\">&nbsp;</td>"); 
         sb.AppendLine("<td width=\"39\" class=\"class_LineaVTabla\">&nbsp;</td>"); 
         sb.AppendLine("<td width=\"269\" class=\"class_LineaVTabla\">&nbsp;</td> ");
         sb.AppendLine("<td width=\"113\" class=\"class_LineaVTabla\" align=\"center\">&nbsp;</td>"); 
         sb.AppendLine("<td width=\"5\" align=\"center\">&nbsp;</td></tr>"); 
         sb.AppendLine("<tr> <td>&nbsp;</td>"); 
         sb.AppendLine("<td class=\"class_LineaVTabla\">Select</td>"); 
         sb.AppendLine("<td class=\"class_LineaVTabla\">Order</td>"); 
         sb.AppendLine("<td class=\"class_LineaVTabla\">Name</td>"); 
         sb.AppendLine("<td  class=\"class_LineaVTabla\" align=\"center\">&nbsp;</td>");
         sb.AppendLine("<td align=\"center\">&nbsp;</td></tr>");
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                string act = "";
                string posi = "";
                if (Convert.ToInt32(row["active"]) == 1)
                {
                    act = "checked";
                } if (Convert.ToInt32(row["pos"]) == 0)
                {
                    posi = Convert.ToString(position);
                   
                }
                else { posi = Convert.ToString(row["pos"]);
                }
                 if (fila == true)
                    {
                        sb.AppendLine("<tr onMouseOver='this.style.cursor=&#39;move&#39;' style='cursor: move; '>");
                    }
                    else {
                        sb.AppendLine("<tr class=\"whiteTable\" onMouseOver='this.style.cursor=&#39;move&#39;' style='cursor: move; '>"); 
                    }
               
                  sb.AppendLine("<td>&nbsp;</td>"); 
                  sb.AppendLine("<td  class=\"class_LineaVTabla\" align=\"center\"><input type=\"checkbox\" value=\"" + row["id"] + "\" name=\"chk" + contcat + "\"" + act + " /></td>");
                  sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><input style=\"background-color:transparent; border:0px;width:15px\" type=\"text\" value=\""+ position +"\"name=\"txt_o"+ contcat + "\"  /></td>"); 
                  sb.AppendLine("<td align=\"left\" class=\"class_LineaVTabla\">" + row["cat"].ToString() + " &amp; Info </td>"); 
                  if (row["SubjId"].ToString() != "0")
                  {
                      sb.AppendLine("<td class=\"class_LineaVTabla\" align=\"center\"><a href='mnt_Subjects.aspx?CategoryId=" + row["SubjId"] + "' id=\"mb13\" class=\"mb\" title=\"HTML element\" rel=\"width:300,type:element\">Select SubItems</a></td>"); 
                  }
                  sb.AppendLine("<td align=\"left\">&nbsp;</td>");                             
                  sb.AppendLine(" </tr>");
                  if (fila == true)
                  {
                      fila = false;
                  }
                  else
                  {
                      fila = true;
                  }
                  contcat++;
                  position++;
                
               }
            
        }
        sb.AppendLine("</TABLE>");
        sb.AppendLine("<input name=\"csub\" type=\"hidden\" value=\""+contcat+"\" />");
        div_category.InnerHtml = sb.ToString();
        #endregion

        #region SubjectSubCategory
        if (Request["CategoryId"] != null)
        {
            subItems = true;


            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            catID = Convert.ToInt32(Request["CategoryId"]);
            data2 = getAllSubSubjects(Convert.ToInt32(Request["CategoryId"]));
            sb1.AppendLine("<TABLE>");
            sb1.AppendLine("<TR>");
            sb1.AppendLine("<TH>ID</TH>");
            sb1.AppendLine("<TH>NAME</TH>");
            sb1.AppendLine("</TR>");
            foreach (DataTable table2 in data2.Tables)
            {
                foreach (DataRow row2 in table2.Rows)
                {
                    string active = "";
                   
                    if (Convert.ToInt32(row2["active"]) == 1)
                    {
                        active = "checked";
                    }
                    sb1.AppendLine("<TR><TD>");
                    sb1.AppendLine("<input type=\"checkbox\" name=\"chk2" + contSub + "\" value=\"" + row2["subcatid"] + "\"" + active + "/> ");
                    sb1.AppendLine("</TD>");
                    sb1.AppendLine("<TD>");
                    sb1.AppendLine("" + row2["subcat"].ToString() + "");
                    sb1.AppendLine("</TD>");
                    sb1.AppendLine("<TD>");
                    sb1.AppendLine("</TD></TR>");
                    contSub++;                    
                }
               
            }
            sb1.AppendLine("</TABLE>");
            div_sub_Subjects.Visible = true;
            sb1.AppendLine("<input name=\"csubsub\" type=\"hidden\" value=\"" + contSub + "\" />");
            div_SubCategory.InnerHtml = sb1.ToString();

        }
        #endregion
    }
    //------------------------------------------------------------
    protected void btnSaveSubj_Click(object sender, EventArgs e)
    {
        string csub = Request["csub"];
        if (csub != null)
        {
            if (Convert.ToInt32(csub) > 0)
            {
                SaveSubjects(Convert.ToInt32(csub));
            }
        }

    }
    //------------------------------------------------------------
    private void SaveSubjects(int count)
    {
        
        string vsub;
        string posSub;
        int isub = 0; 
        int ipos = 0;
        deleteAllSubjects(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]));
        for (int i = 0; i <= count; i++)
        {
                vsub = Request["chk"+i];
                isub = Convert.ToInt32(vsub);
                posSub = Request["txt_o" + i];
                if (posSub != "")
                { ipos = Convert.ToInt32(posSub); }
                if (vsub != null && posSub != "")
                {

                    addSubject(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), isub, ipos);
                    
                }
         }
        Response.Redirect("mnt_Subjects.aspx");
    }
    //------------------------------------------------------------
    private void SaveSubSubjects(int count)
    {
        if (Request["CategoryId"] != null)
            Del_All_SubjectSubCategory(Convert.ToInt32(Request["CategoryId"].ToString()));

        string vsubsubj;
        int isubsub = 0;
        for (int i = 0; i <= count; i++)
        {
            vsubsubj = Request["chk2" + i];
            isubsub = Convert.ToInt32(vsubsubj);
            if (vsubsubj != null)
            {

                addSubSubject(catID, isubsub, true);
            }
        }
    }
    //------------------------------------------------------------
    protected void btnCancelSubj_Click(object sender, EventArgs e)
    {
        if (Request["CategoryId"] != null)
        {
            Response.Redirect("mnt_Subjects.aspx");
        }
        else
        {
            Response.Redirect("CMS_MainSite.aspx");
        }
            
    }
    //------------------------------------------------------------
    protected void btn_SaveSub_Click(object sender, EventArgs e)
    {
        int csub = Convert.ToInt32(Request["csubsub"]);
        if (csub != 0)
        {

            SaveSubSubjects(Convert.ToInt32(csub));

        }
        Response.Redirect("mnt_Subjects.aspx");
    }
    protected void btn_CancelSub_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Subjects.aspx");
    }
}
