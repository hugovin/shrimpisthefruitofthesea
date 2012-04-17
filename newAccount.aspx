<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="newAccount.aspx.cs" Inherits="newAccount" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" Runat="Server">
<div id="print">

<div id="cont">
<%if (newuser == false)
  { %>
    <iframe frameborder="0" id="registrationFrame" src="Https://<% Response.Write(Global.globalMySiteUrl); %>/User/New_User.aspx?Frame=1" height="550px" width="100%"></iframe>
    <%}
  else
  { %>
    <iframe frameborder="0" id="registrationFrame" src="Https://<% Response.Write(Global.globalMySiteUrl); %>/User/New_User.aspx?Frame=1&Checkout=1" height="550px" width="100%"></iframe>
    <% }%>
</div>

</div>
</asp:Content>