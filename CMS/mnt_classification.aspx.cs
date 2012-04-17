using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class CMS_mnt_classification : Title
{
    private string _description = "";
    private string _content = "";
    private int _classId = 0;

    //Get and Set
    public string _Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public string _Content
    {
        get { return _content; }
        set { _content = value; }
    }

    public int _ClassId
    {
        get { return _classId; }
        set { _classId = value; }
    }
    //************************************************************************/

    protected void Page_Load(object sender, EventArgs e)
    {
        bool fila = true;
        DataSet data = new DataSet();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (Request["DeleteId"] != null)
        {
            delClassification(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToInt32(Request["DeleteId"]));
            Response.Redirect("mnt_classification.aspx");
        }

        if (Request["ModId"] != null)
        {
            data = Get_Title_Content_Classification_Product(Convert.ToInt32(Request["ModId"]));
            
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    _ClassId = Convert.ToInt32(Request["ModId"]);
                    _Description = row["ClassDescription"].ToString();
                    _Content = row["ClassContent"].ToString();
                }
            }

            contenido.Visible = true;
            div_update.Visible = true;
        }

        data = null;

        data = Get_All_Classification(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]));

        sb.AppendLine("<TABLE width=\"512\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
        sb.AppendLine("<tr>");
        sb.AppendLine("<td width=\"9\" height=\"10\">&nbsp;</td> ");
        sb.AppendLine("<td width=\"44\">&nbsp;</td>");
        sb.AppendLine("<td width=\"240\" class=\"class_LineaVTabla\">&nbsp;</td>");
        sb.AppendLine("<td width=\"76\" align=\"center\">&nbsp;</td> ");
        sb.AppendLine("<td width=\"58\" align=\"center\">&nbsp;</td>  ");
        sb.AppendLine("<td width=\"12\">&nbsp;</td>");
        sb.AppendLine("</tr>");
        sb.AppendLine("<tr>");
        sb.AppendLine("<td>&nbsp;</td> ");
        sb.AppendLine("<td>Id</td>");
        sb.AppendLine("<td class=\"class_LineaVTabla\">Name</td> ");
        sb.AppendLine("<td align=\"center\">&nbsp;</td><td align=\"center\">&nbsp;</td> ");
        sb.AppendLine("<td>&nbsp;</td>");
        sb.AppendLine("</tr>");
        foreach (DataTable table in data.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (fila == true)
                { sb.AppendLine("<tr class=\"fila\"> "); }
                else { sb.AppendLine("<tr> "); }
                sb.AppendLine("<td>&nbsp;</td>");
                sb.AppendLine("<TD align=\"center\">");
                sb.AppendLine("" + row["ClassId"].ToString() + "");
                sb.AppendLine("</TD>");
                sb.AppendLine("<TD align=\"left\" class=\"class_LineaVTabla\">");
                sb.AppendLine("" + row["ClassDescription"].ToString() + "");
                sb.AppendLine("</TD>");
                sb.AppendLine("<TD align=\"center\"><a class=\"enlace\" href= mnt_Classification.aspx?ModId=" + row["ClassId"].ToString() + "><img src=\"images/btn_Edit.png\" border=\"0\" /> Edit</a></TD>");
                sb.AppendLine("<td align=\"center\"><a class=\"enlace\" href= mnt_Classification.aspx?DeleteId=" + row["ClassId"] + "><img src=\"images/btn_delete.png\" border=\"0\" onclick=\"return confirm('Do you want to continue?  ');\">Delete</A></td>");
                sb.AppendLine(" <td align=\"left\">&nbsp;</td></tr>");
                if (fila == true)
                { fila = false; }
                else { fila = true; }
            }
        }
        sb.AppendLine("<tr><td>&nbsp;</td><td align=\"center\">&nbsp;</td>");
        sb.AppendLine("<td align=\"left\"class=\"class_LineaVTabla\">&nbsp;</td>");
        sb.AppendLine("<td align=\"center\">&nbsp;</td>");
        sb.AppendLine("<td align=\"center\">&nbsp;</td>");
        sb.AppendLine("<td align=\"left\">&nbsp;</td></tr>");

        sb.AppendLine("</TABLE>");        
        btn_New.Visible = true;

        PlaceHolder_Classification.Controls.Add(new LiteralControl(sb.ToString()));
    }

    protected void btn_New_Click(object sender, EventArgs e)
    {
        contenido.Visible = true;
        div_insert.Visible = true;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        addClassification(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToString(Request["txt_Title"]), Convert.ToString(Request["Comments"]));
        contenido.Visible = false;
        div_insert.Visible = false;
        Response.Redirect("mnt_classification.aspx");
    }

    protected void btn_Update_Click(object sender, EventArgs e)
    {
        updClassification(Convert.ToInt32(Session["siteId"]), Convert.ToInt32(Session["contId"]), Convert.ToInt32(Request["txt_ClassId"]), Convert.ToString(Request["txt_Title"]), Convert.ToString(Request["Comments"]));
        contenido.Visible = false;
        div_update.Visible = false;
        Response.Redirect("mnt_classification.aspx");
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        contenido.Visible = false;
        div_insert.Visible = false;
        Response.Redirect("mnt_classification.aspx");
    }

    protected void btn_Cancel_Upd_Click(object sender, EventArgs e)
    {
        contenido.Visible = false;
        div_update.Visible = false;
        Response.Redirect("mnt_classification.aspx");
    }
}
