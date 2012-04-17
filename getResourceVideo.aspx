<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getResourceVideo.aspx.cs" Inherits="getResourceVideooo" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<% Response.Write("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + Global.globalSiteStylePath + "/global.css\" />"); %>
    <title>Video</title>
</head>
<body style="background-image: none; background-color: #eee">

        <div style="text-align:center;>

            <asp:PlaceHolder ID="PlaceHolder_Video" runat="server"></asp:PlaceHolder>
        
        </div>

</body>
</html>