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
using System.Collections.Generic;

public partial class PageTest : System.Web.UI.Page
{
    public List<UsStates> _States = new List<UsStates>();

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadStates();

    }

    public void LoadStates()
    {
       
    }


    public class UsStates
    {
        public string value;
        public string text;

        public UsStates(string value, string text)
        {
            this.value = value;
            this.text = text;
        }
        public UsStates()
        {
            this.value = "";
            this.text = "";
        }


    }
}
