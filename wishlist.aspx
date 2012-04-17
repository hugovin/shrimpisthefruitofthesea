<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="wishList.aspx.cs" Inherits="wishList" Title="Untitled Page" %>
    <%@ Reference Control="~/boxContact.ascx" %>
    <%@ Reference Control="~/uc_FeatureProduct.ascx" %>
    <%@ Reference Control="~/uc_Specials.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" Runat="Server">        
<!-- Cart Code -->
    <script type="text/javascript" charset="utf-8" src="static/js/framework/onload.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/framework/mootools-extensions.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/framework/utils.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/site/cart.js"></script>
    <script language="javascript" type="text/javascript" src="js/slideitmoo-1.1.js"></script>
    
<!-- Cart Code --> 
    <script language="javascript" type="text/javascript" src="js/slideitmoo-1.1.js"></script>
	<script language="javascript" type="text/javascript">
        window.addEvents({
            'domready': function(){
                /* thumbnails example , div containers */
                new SlideItMoo({
                            overallContainer: 'SlideItMoo_outer',
                            elementScrolled: 'SlideItMoo_inner',
                            thumbsContainer: 'SlideItMoo_items',		
                            itemsVisible:3,
                            itemsSelector: '.SlideItMoo_element',
                            itemWidth: 165,
                            showControls:1});
            },
            'load': function(){
                /* banner rotator example */	
                new SlideItMoo({overallContainer: 'SlideItMoo_banners_outer',
                                elementScrolled: 'SlideItMoo_banners_inner',
                                thumbsContainer: 'SlideItMoo_banners_items',		
                                itemsVisible:1,
                                itemsSelector: '.banner',
                                showControls:0,
                                autoSlide: 3000,
                                transition: Fx.Transitions.Bounce.easeOut,
                                duration: 1800,
                                direction:-1});
                                
                /* info rotator example */	
                
            }
        });
    </script>    


	<script type="text/javascript" src="js/MooFlow.js"></script>
    
    <script language="javascript" type="text/javascript">   
        var divCollection
         
        function callClose(i){
            location.href = location.href;      
            return true;        
        }    
        
        function find_div_class(){
        divCollection = document.getElementsByTagName("div");
        for (var i=0; i<divCollection.length; i++) {
                if (divCollection[i].className == "MultiBoxClose"){                    
                    divCollection[i].setAttribute('onClick', 'return callClose();');
                    document.getElementById('Overlay').setAttribute('onClick','return callClose('+i+');');
                }
        }
    }
    </script>

    <div id="cont">
        <div id="main-content">
            <div id="print">
                <asp:PlaceHolder ID="PlaceHolder_Wish" runat="server"></asp:PlaceHolder>
            </div>
        </div>
         <div id="sidebar-content">
                 <asp:PlaceHolder ID="PlaceHolder_boxContact" runat="server"></asp:PlaceHolder>
                 <asp:PlaceHolder ID="PlaceHolder_uc_FeatureProduct" runat="server"></asp:PlaceHolder>
                 <asp:PlaceHolder ID="PlaceHolder_uc_Specials" runat="server"></asp:PlaceHolder>
         </div>
         <div class="clear"></div>  
    </div>
</asp:Content>


