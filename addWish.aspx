<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addWish.aspx.cs" Inherits="addWish" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <% Response.Write("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + Global.globalSiteStylePath + "/global.css\" />"); %>
    <title>Add Wish To List</title>
</head>
<body>
    <asp:PlaceHolder ID="PlaceHolder_Message" runat="server"></asp:PlaceHolder>  
</body>
</html>