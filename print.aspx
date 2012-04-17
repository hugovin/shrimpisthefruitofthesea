<%@ Page Language="C#" MasterPageFile="~/Print.master" AutoEventWireup="true" CodeFile="print.aspx.cs" Inherits="print" Title="Untitled Page" %>

<%@ Reference Control="~/boxContactPrint.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_boxContact" Runat="Server">
    <%Response.Write(stilos); %>
    <asp:PlaceHolder ID="PlaceHolder_boxContactPrint" runat="server"></asp:PlaceHolder>  
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HomeContent_Master" Runat="Server">          	    
    <asp:PlaceHolder ID="PlaceHolder_Div" runat="server"></asp:PlaceHolder>             
</asp:Content>


