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
using System.Text;
using uc_Right;

public partial class print : System.Web.UI.Page
{
    string _contenido = "";
    public string stilos = "";
     
    protected void Page_Load(object sender, EventArgs e)
    { 

        if (Session["CurrentChilPage"] != null && Session["CurrentChilPage"].ToString() == "requestaquote.aspx")
        {
            stilos = "<style type=\"text/css\"><!-- .prodNumber img, .prodNumber a img, .prodSrcDesc img, input, .prodCartDesc img, prodCartDesc a img, .prodDesc img, .prodDesc a img, .prodMore a{display: block;}--></style>";               
        }
        if (Request["contenido"] != null)
            _contenido = Request["contenido"];
        else
            _contenido = "";
        loadContenido();
        loadContact();
    }

    protected void loadContenido()
    {
        PlaceHolder_Div.Controls.Add(new LiteralControl(_contenido.ToString()));
    }

    private void loadContact()
    {
        boxContactPrint boxContactPrint = (boxContactPrint)(Page.LoadControl("boxContactPrint.ascx"));
        PlaceHolder_boxContactPrint.Controls.Add(boxContactPrint);
    }
}
