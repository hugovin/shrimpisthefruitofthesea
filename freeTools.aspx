<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="freeTools.aspx.cs" Inherits="freeTools" Title="Untitled Page" %>
<%@ Reference Control="~/uc_SAPricing.ascx" %>
<%@ Reference Control="~/uc_NewsNinfo.ascx" %>
<%@ Reference Control="~/boxContact.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" Runat="Server">
    <script src="js/ajaxObject.js" type="text/javascript"></script>
    <script src="js/downloadtrials.js" type="text/javascript"></script>
    <div id="cont">
        <div id="print">
            <div id="main-content">
                <asp:PlaceHolder ID="PlaceHolder_Trials" runat="server"></asp:PlaceHolder>  
                <asp:PlaceHolder ID="PlaceHolder_Demos" runat="server"></asp:PlaceHolder>            
    		    <asp:PlaceHolder ID="PlaceHolder_Foot" runat="server"></asp:PlaceHolder>    	
            </div>
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