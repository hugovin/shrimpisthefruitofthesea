<%@ Page Language="C#" AutoEventWireup="true" ContentType="text/xml" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Data" %>
<script runat="server">
  protected void Page_Load(object source, EventArgs e)
  {
    MainContent maincontent = new MainContent();
    DataSet dsbrands = new DataSet();
    dsbrands = maincontent.Get_Site_FeaturedBrands();
    XmlDocument doc = new XmlDocument();
	string strFolder = "images";
	int cont = 0;
    
    // XML declaration
    XmlNode declaration = doc.CreateNode(XmlNodeType.XmlDeclaration, null, null);
    doc.AppendChild(declaration);
    
    foreach (DataTable table in dsbrands.Tables)
        {
            // Root element: data
		    XmlElement root = doc.CreateElement("data");
		    doc.AppendChild(root);
		    
            foreach (DataRow row in table.Rows)
            {
                cont++;
                XmlElement img = doc.CreateElement("img"); 
			    root.AppendChild(img);
			    
			    XmlElement link = doc.CreateElement("link");
			    link.InnerText = "result.aspx?findopt5=" + row["PubId"].ToString() + "&am=1&asm=" + 3  + "";
			    img.AppendChild(link);
			    
			    XmlElement src = doc.CreateElement("src");
			    src.InnerText = "" + strFolder + "/" + row["featfile"].ToString() + "";
			    //src.InnerText = "images/ER_microg_theatre_011910.jpg";
			    img.AppendChild(src);
    
                XmlElement title = doc.CreateElement("title");
			    title.InnerText = "" + row["pubname"].ToString() + "";
			    img.AppendChild(title);		
					
                //if (cont == 5) break;
            }
        }
 doc.Save(Response.OutputStream);
  }
</script>