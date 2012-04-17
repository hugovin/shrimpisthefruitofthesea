<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteSelection.aspx.cs" Inherits="SiteSelection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1> Site Selection</h1>
        <asp:DropDownList ID="ddlSite" runat="server" Height="16px" Width="199px">
            <asp:ListItem Value="1">Surburn</asp:ListItem>
            <asp:ListItem Value="2">ER</asp:ListItem>
        </asp:DropDownList>
    </div>
    <asp:Button ID="btnGo" runat="server" Text="Go!" />
    </form>
</body>
</html>
