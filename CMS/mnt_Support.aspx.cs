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

public partial class CMS_mnt_Support : Gen_D
{
    public string str_TinyMCE;
    public string strFolder = "Images";
    public string contentLoad = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Addins addins = new Addins();
        str_TinyMCE = addins.GettinyMCE();

        Session["CurrentPage"] = "mnt_Support.aspx";
        if (Request["ContentGroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["ContentGroupId"]);
        }

    }
}
