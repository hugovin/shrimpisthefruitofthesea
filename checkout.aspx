<%@ Page Language="C#" MasterPageFile="~/cart.master" AutoEventWireup="true"  enableeventvalidation="false"  CodeFile="checkout.aspx.cs" Inherits="CheckoutPage" Title="Checkout" %>

<%@ Reference Control="~/boxContact.ascx" %>
<%@ Reference Control="~/uc_FeatureProduct.ascx" %>
<%@ Reference Control="~/uc_Specials.ascx" %>
<%@ Reference Control="~/uc_BestSellers.ascx" %>


<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceholder" Runat="server">
    <!--<script type="text/javascript" language="javascript" charset="utf-8" src="static/js/framework/mootools-1.2-core.js"></script>-->
    <script type="text/javascript" language="javascript" charset="utf-8" src="static/js/framework/onload.js"></script>
    <script type="text/javascript" language="javascript" charset="utf-8" src="static/js/framework/mootools-extensions.js"></script>
    <script type="text/javascript" language="javascript" charset="utf-8" src="static/js/framework/utils.js"></script>

    <style type="text/css">
        .hidden { display:none; visibility:hidden; }
        div.container { padding:20px; }
        div.button_row { padding:20px 0; } 
        
        div.summary { position:absolute; top:0px; left:600px; }
        div.input_field label { display:block; float:left; width:120px; }
        div.input_field .input_textbox { display:block; float:left; }
     
        a.button_orange { display:block; padding:10px; width:150px; margin:5px; background-color:#F68A32; text-align:center; font-family:Arial, Sans-Serif; color:#000;  text-decoration:none; }
        a.button_blue { display:block; padding:10px; width:150px; margin:5px; background-color:#4A82B5; text-align:center; font-family:Arial, Sans-Serif; color:#000;  text-decoration:none; }
        a.button_grey { display:block; padding:2px; width:30px; margin:5px; background-color:#666; text-align:center; font-family:Arial, Sans-Serif; color:#FFF;  text-decoration:none; }
    </style>    
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" Runat="server">
    <div id="cont">
        <div id="main-content-checkOut">
            <div id="printContenido">
                <asp:PlaceHolder ID="StepsPlaceholder" runat="server"></asp:PlaceHolder>

                <div id="contCheckOut" class="mainCheckOut">
                    <asp:PlaceHolder ID="ControlPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
            </div>
        </div>
        
        <div id="sidebar-content">
            <asp:PlaceHolder ID="SummaryPlaceholder" runat="server"></asp:PlaceHolder>
        
            <asp:PlaceHolder ID="PlaceHolder_boxContact" runat="server"></asp:PlaceHolder>
            <!-- End div boxContact-->
            <asp:PlaceHolder ID="PlaceHolder_uc_FeatureProduct" runat="server"></asp:PlaceHolder>
           	
		    <!-- End div boxProducts-->
		    <asp:PlaceHolder ID="PlaceHolder_uc_Specials" runat="server"></asp:PlaceHolder>
	        <!-- End div boxSpecials-->
		    <asp:PlaceHolder ID="PlaceHolder_uc_BestSellers" runat="server"></asp:PlaceHolder>					
            <!-- End div boxBestSellers-->
        </div>
    </div>
    
</asp:Content>

