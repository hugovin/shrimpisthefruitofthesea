<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="PublisherList.aspx.cs" Inherits="PublisherList" Title="Untitled Page" %>

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
            <asp:PlaceHolder ID="PlaceHolder_PublisherList_Head" runat="server"></asp:PlaceHolder>    
            <asp:PlaceHolder ID="PlaceHolder_resultControls_Head" runat="server"></asp:PlaceHolder>            	        
            <asp:PlaceHolder ID="PlaceHolder_PublisherList_Content" runat="server"></asp:PlaceHolder>             
            <asp:PlaceHolder ID="PlaceHolder_resultControls_Foot" runat="server"></asp:PlaceHolder>
        </div>
    </div>
</asp:Content>

