<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="relatedproduct.aspx.cs" Inherits="relatedproduct" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" Runat="Server"> 
<!-- Cart Code -->
    <script type="text/javascript" charset="utf-8" src="static/js/framework/onload.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/framework/mootools-extensions.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/framework/utils.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/site/cart.js"></script>
    <script language="javascript" type="text/javascript" src="js/slideitmoo-1.1.js"></script>
    
<!-- Cart Code --> 
    <div id="cont">     
        <div id="print">
            <asp:PlaceHolder ID="PlaceHolder_Clasification" runat="server"></asp:PlaceHolder>    
        </div>                     
    </div>
</asp:Content>
