<%@ Page Language="C#" MasterPageFile="~/printProduct.master" AutoEventWireup="true" CodeFile="printProduct.aspx.cs" Inherits="printProduct" Title="Untitled Page" %>

<%@ Reference Control="~/boxContactPrint.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolder_boxImage" Runat="Server">
    <asp:PlaceHolder ID="PlaceHolder_boxImage" runat="server"></asp:PlaceHolder>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_boxContact" Runat="Server">
    <asp:PlaceHolder ID="PlaceHolder_boxContact" runat="server"></asp:PlaceHolder>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HomeContent_Master" Runat="Server">
    <div class="prodInfoDesc">
        <h1><% =_Title %></h1>
        <h2><strong><%=_Version %></strong> <a href="#LicenseOptions">See Additional Licensing Options</a></h2>
        <p>Item #<%=_Sku %></p>
        <p><em>by</em>: <a href="PublisherList.aspx?idP=<%=_Pubid %>"><%=_Pubname %></a></p>
        <h3>Grade Levels: <span><%=_Grades %></span></h3>        
        <div class="prodInfoL">
            <p>
                <%if (_Plat_mac_flag == "1"){%><%=_Mac%><%} %>
                <%if (_Plat_mac_flag == "1" && _Plat_win_flag == "1")
                  {%>/<%} %>
                <%if (_Plat_win_flag == "1")
                  {%><%=_Win%><%} %></p>
            <h4>List Price: $<%=_Srp %></h4>
            <h2>$<%=_Er_price%></h2>
            <% if (Convert.ToDouble(_Yousave) > 0){%>
            <h3>YOUR DISCOUNTED PRICE</h3>
            <h4>You Save: $<%=_Yousave%></h4>
            <% } %>
            <p></p>                               
        </div>   
        <div class="prodInfo2">
            <h1>Description</h1>
            <p><%=_Short_description%></p>
            <p><%=_Long_description %></p>
        </div>  
        <div id="SysReq" runat="server">
            <div class="prodInfo2">
                <h1>System Requirements</h1>
                <p><asp:PlaceHolder ID="PlaceHolder_SysReq" runat="server"></asp:PlaceHolder></p>
            </div>
        </div>    
        <div id="Resources" runat="server">
            <div class="prodInfo2">
                <h1>Resources</h1>
                <p><asp:PlaceHolder ID="PlaceHolder_Resources" runat="server"></asp:PlaceHolder></p>
            </div>
        </div> 
         <div id="Funding" runat="server">
            <div class="prodInfo2">
                <h1>Funding & Usage</h1>
                <p><asp:PlaceHolder ID="PlaceHolder_Funding" runat="server"></asp:PlaceHolder></p>
            </div>
        </div>
        <div id="Additional" runat="server">
            <div class="prodInfo2">
                <h1>Additional Information</h1>
                <p><asp:PlaceHolder ID="PlaceHolder_Additional" runat="server"></asp:PlaceHolder></p>
            </div>
        </div>  
        <div id="RelPro" runat="server">  
            <div class="prodInfo2">   
                <h1>Similar Products</h1>
                <div id="printBox">
                    <div id="SlideItMoo_inner">
                        <div id="SlideItMoo_items">                        
                            <asp:PlaceHolder ID="PlaceHolser_Slide" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



