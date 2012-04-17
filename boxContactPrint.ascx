<%@ Control ClassName="boxContactPrint" Language="C#" AutoEventWireup="true" CodeBehind="boxContactPrint.ascx.cs"
    CodeFile="boxContactPrint.ascx.cs" Inherits="uc_Right.boxContactPrint" %>
<div class="boxContact-print">
    <div class="boxContactTop""></div>
    <div class="MainBoxContact">
        <div class="titleContact"><img src="<% = Global.globalSiteImagesPath %>/titleContact.gif" /></div>
        <asp:PlaceHolder ID="PlaceHolder_Contact" runat="server"></asp:PlaceHolder>
    </div>
    <div class="boxContactBotton">
    </div>
</div>
