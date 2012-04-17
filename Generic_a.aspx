<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="Generic_a.aspx.cs" Inherits="Generic_a" Title="Untitled Page" %>
<%@ Reference Control="~/uc_SAPricing.ascx" %>
<%@ Reference Control="~/uc_NewsNinfo.ascx" %>
<%@ Reference Control="~/boxContact.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" Runat="Server">    
    <script src="js/centerImages.js" type="text/javascript"></script>
    <div id="cont">
        <div id="print">
            <div id="mainAbout">
                <asp:PlaceHolder ID="PlaceHolder_Generic_A" runat="server"></asp:PlaceHolder>  
            </div>         
        </div>
    </div>  
</asp:Content>

