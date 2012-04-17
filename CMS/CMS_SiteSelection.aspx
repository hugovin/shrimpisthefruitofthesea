<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CMS_SiteSelection.aspx.cs" Inherits="SiteSelection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head> 
<title>CMS - Login</title> 
    <link href="css/style-box.css" rel="stylesheet" type="text/css" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
</head> 
<body>
    <form id="form1" runat="server">
     <asp:SiteMapPath ID="SiteMapPath1" runat="server" Font-Names="Verdana" Font-Size="0.8em"
                    PathSeparator=" : " ParentLevelsDisplayed="6">
                    <PathSeparatorStyle Font-Bold="True" ForeColor="#507CD1" />
                    <CurrentNodeStyle ForeColor="#333333" />
                    <NodeStyle Font-Bold="True" ForeColor="#284E98" />
                    <RootNodeStyle Font-Bold="True" ForeColor="#507CD1" />                    
                </asp:SiteMapPath>
    <div class="content">
        <div class="sites-box">
            <div class="h1">
                <div align="center">
                    Please Select a Website
                </div>
            </div>
            <div align="center">
                <br />
                <br />
                <br />
                <br />
                <asp:DropDownList ID="ddlSite" runat="server">
                </asp:DropDownList>
                <br />
                <br />
                <br />
                <br />
                <asp:ImageButton ID="btnGo" ImageUrl="imagesCss/enter-btn.jpg" runat="server" Text="Go!"
                    OnClick="btnGo_Click"></asp:ImageButton>
            </div>
        </div>
    </div>
   
</form>
</body>
</html>
