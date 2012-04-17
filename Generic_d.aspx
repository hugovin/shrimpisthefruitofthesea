<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="Generic_d.aspx.cs" Inherits="Generic_d" Title="Untitled Page" %>
<%@ Reference Control="~/uc_SAPricing.ascx" %>
<%@ Reference Control="~/uc_NewsNinfo.ascx" %>
<%@ Reference Control="~/boxContact.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" Runat="Server">
    <script src="js/centerImages.js" type="text/javascript"></script>
    <div id="cont">
        <div id="print">
            <asp:PlaceHolder ID="PlaceHolder_Resources_Purchasing" runat="server"></asp:PlaceHolder>   
        </div>
       <div id="sidebar-content">		       
                  	
            <asp:PlaceHolder ID="PlaceHolder_boxContact" runat="server"></asp:PlaceHolder>
            <!-- End div boxContact-->
             <asp:PlaceHolder ID="PlaceHolder_uc_SAPricing" runat="server"></asp:PlaceHolder>
	       	
			<!-- End div Special academic pricing-->
			<asp:PlaceHolder ID="PlaceHolder_uc_NewsNinfo" runat="server"></asp:PlaceHolder>
		    <!-- End div boxNews And info-->

	    </div> 
    </div>    
</asp:Content>


