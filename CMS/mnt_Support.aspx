<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMS/CMS_mntMasterPage.master" CodeFile="mnt_Support.aspx.cs" Inherits="CMS_mnt_Support" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<!-- TinyMCE -->
<script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>
    <% Response.Write(str_TinyMCE); %>
<!-- /TinyMCE -->
    <a href="generic_d.aspx?GeneId=9">Purchasing Information</a><br />
    <a href="mnt_generic_c.aspx">FAQs</a>
    <
</asp:Content>
