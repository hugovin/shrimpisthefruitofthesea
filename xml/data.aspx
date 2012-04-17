<%@ Page Language="C#" AutoEventWireup="true" ContentType="text/xml" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Text.RegularExpressions"%>
<script runat="server">
  protected void Page_Load(object source, EventArgs e)
  {
    MainContent feature = new MainContent();
    DataSet dsfeature = new DataSet();
    XmlDocument doc = new XmlDocument();
	string strFolder = "images";
	int cont = 0;
    dsfeature = feature.getAllFeatureHome();
    // XML declaration
    XmlNode declaration = doc.CreateNode(XmlNodeType.XmlDeclaration, null, null);
    doc.AppendChild(declaration);

    string path = "";
    foreach (DataTable table in dsfeature.Tables)
        {
            // Root element: data
		    XmlElement root = doc.CreateElement("data");
		    doc.AppendChild(root);
            foreach (DataRow row in table.Rows)
            {
                if(!Regex.IsMatch(row["FeatFile"].ToString(), @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
                {
                    path = strFolder + "/";
                }else{
                    path = "";
                }
                cont++;
                XmlElement img = doc.CreateElement("img"); 
			    root.AppendChild(img);
			    
			    XmlElement src = doc.CreateElement("src");
			    src.InnerText = "" + path + row["FeatFile"].ToString() + "";
  
                img.AppendChild(src);
                XmlElement title = doc.CreateElement("title");
			    title.InnerText = "" + row["FeatTitle"].ToString() + "";
			    img.AppendChild(title);
    
                XmlElement a = doc.CreateElement("a");
				img.AppendChild(a);
				
					XmlAttribute href = doc.CreateAttribute("href");
				    href.Value = "" + row["FeatLink"].ToString() + "";
				    a.Attributes.Append(href);
				    
                if ((row["FeatLink"].ToString().IndexOf("http")) == 0){
				    XmlAttribute target = doc.CreateAttribute("target");
				    target.Value = "_blank";
				    a.Attributes.Append(target);
				    }
                else
                	{
                	XmlAttribute target = doc.CreateAttribute("target");
				    target.Value = "_self";
				    a.Attributes.Append(target);
                	}
                if (cont == 5) break;
            }
        }
        doc.Save(Response.OutputStream);
}
</script>