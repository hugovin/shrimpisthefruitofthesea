<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" Runat="Server">
<div id="">
<div id="cont">
    <iframe frameborder="0" id="registrationFrame" src="http://<% Response.Write(Global.globalMySiteUrl); %>/user/forgot_password.aspx?Frame=1" height="550px" width="100%"></iframe>
</div>
</div>
</asp:Content>
