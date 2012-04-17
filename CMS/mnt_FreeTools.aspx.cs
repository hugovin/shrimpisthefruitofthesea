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

public partial class CMS_mnt_FreeTools : FreeTools
{
    public string str_TinyMCE;
    public string Title1, SubTitle1, Content1, Title2, SubTitle2, Content2, Title3, SubTitle3, Content3 = "";
    private int siteid;

    protected void Page_Load(object sender, EventArgs e)
    {
        Addins addins = new Addins();
        str_TinyMCE = addins.GettinyMCE();

        Session["CurrentPage"] = "mnt_FreeTools.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }
        if (Request["GenericId"] != null)
        {
            Session["GenericId"] = Convert.ToInt32(Request["GenericId"]);
        }

        siteid = Convert.ToInt32(HttpContext.Current.Session["siteId"].ToString());
        DataSet data = new DataSet();
        data = Get_Site_FreeTools(siteid);
        if (data.Tables["table"].Rows.Count != 0)
        {
            foreach (DataTable table in data.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    Title1 = row["FreeTitle1"].ToString();
                    SubTitle1 = row["FreeSubTitle1"].ToString();
                    Content1 = row["FreeContent1"].ToString();
                    Title2 = row["FreeTitle2"].ToString();
                    SubTitle2 = row["FreeSubTitle2"].ToString();
                    Content2 = row["FreeContent2"].ToString();
                    Title3 = row["FreeTitle3"].ToString();
                    SubTitle3 = row["FreeSubTitle3"].ToString();
                    Content3 = row["FreeContent3"].ToString();
                    btn_Save.CssClass = "class_btnUpdate";
                    btn_Save.Text = ".";
                }
            }
        }


    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (btn_Save.Text != ".")
        {
            Add_FreeTools_Info(siteid,Convert.ToString(Request["title"]),Convert.ToString(Request["subtitle1"]),Convert.ToString(Request["content1"])
                ,Convert.ToString(Request["title2"]),Convert.ToString(Request["subtitle2"]),Convert.ToString(Request["content2"]),
                 Convert.ToString(Request["title3"]),Convert.ToString(Request["subtitle3"]),Convert.ToString(Request["content3"]));
        }
        else
        {
            Upd_FreeTools_Info(siteid, Convert.ToString(Request["title1"]), Convert.ToString(Request["subtitle1"]), Convert.ToString(Request["content1"])
                , Convert.ToString(Request["title2"]), Convert.ToString(Request["subtitle2"]), Convert.ToString(Request["content2"]),
                 Convert.ToString(Request["title3"]), Convert.ToString(Request["subtitle3"]), Convert.ToString(Request["content3"]));
        }
        Response.Redirect("mnt_FreeTools.aspx");

    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("mnt_Generics.aspx?Generic=1");
    }
}
