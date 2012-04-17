<%@ Page Language="C#" MasterPageFile="~/Product.master" AutoEventWireup="true" CodeFile="SiteMap.aspx.cs" Inherits="SiteMap" %>

<asp:Content ContentPlaceHolderID="HomeContent_Master" runat="server">
<div id="print">
    <div class="sitemap">
        <div class="sitemapColI">
            <div class="sitemapTitle">
                About ER</div>
            <div class="siteMapList">
                <ul>
                <asp:PlaceHolder ID="PlaceHolder_AboutUs" runat="server"></asp:PlaceHolder>
                </ul>
            </div>
            <div class="sitemapTitle">
                Subjects</div>
            <div class="siteMapList">
                <ul>
                    <asp:PlaceHolder ID="PlaceHolder_Subjects" runat="server"></asp:PlaceHolder>
                </ul>
            </div>
        </div>
        <div class="sitemapColII">
            <div class="sitemapTitle">
                Resource Center</div>
            <div class="siteMapList">
                <ul>
                   <asp:PlaceHolder ID="PlaceHolder_ResourceCenter" runat="server"></asp:PlaceHolder>
                </ul>
            </div>
        </div>
        <div class="sitemapColI">
            <div class="sitemapTitle">
                Products</div>
            <div class="siteMapList">
                <ul>
                    <li><a href="WhatsNew.aspx?cp=5">WhatsNew</a></li>
                    <li><a href="Classification.aspx?cp=1">Feature Products</a></li>
                    <li><a href="Classification.aspx?cp=3">Best Sellers</a></li>
                </ul>
            </div>
        </div>
    </div>
    </div>
</asp:Content> 

