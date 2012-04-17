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

public partial class session_dump : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(Helper.VarDump(Session));

        Hashtable ht = new Hashtable();

        foreach (string key in Session.Keys)
        {
            ht.Add(key, Session[key]);
        }

        Response.Write(Helper.VarDump(ht));


        Hashtable cookies = new Hashtable();
        foreach (string key in Request.Cookies.AllKeys)
        {
            cookies.Add(key, Request.Cookies[key].Value);
        }


        Response.Write(Helper.VarDump(cookies));



    }
}
