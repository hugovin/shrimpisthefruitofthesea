<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="result.aspx.cs"
    Inherits="result" Title="Untitled Page" %>

<%@ Reference Control="~/boxContact.ascx" %>
<%@ Reference Control="~/uc_FeatureProduct.ascx" %>
<%@ Reference Control="~/uc_Specials.ascx" %>
<%@ Reference Control="~/uc_BestSellers.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" runat="Server">
<!-- Cart Code -->
    <script type="text/javascript" charset="utf-8" src="static/js/framework/onload.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/framework/mootools-extensions.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/framework/utils.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/site/cart.js"></script>
    <script src="centrar.js" type="text/javascript"></script>
<!-- Cart Code -->    
    <div id="cont">
        <div id="print">
            <div id="main-content">
           
                <form action="result.aspx" method="POST" name="refine" id="refine">
                <div id="resultControls">
                    <h2>Your Search for <% if ((_sTextFinder != "" && _sTextFinder != null))
                       {%> <strong>"<% = _sTextFinder%>"</strong><%} %>
                       <% if ((_sTextFinder != "" && _sTextFinder != null) && (_sTextFinderRefine != "" && _sTextFinderRefine != null))
                       {%> <strong>,</strong><%} %>
                       
                       <% if ((_sTextFinderRefine != "" && _sTextFinderRefine != null))
                       { _sTextFinderRefine = _sTextFinderRefine.Substring(1,Convert.ToInt32(_sTextFinderRefine.Length.ToString())-1);%>
			<strong><% = _sTextFinderRefine%></strong><%} %>

			<% if (Request["nn"] != null){%>
 			<% if (_sTextFinder != null || _sTextFinderRefine !=""){%>
				<strong>,</strong><%}%>
                       <% if (Request["nn"] != null)
                       {%> <strong><% Response.Write(Request["nn"]);%></strong><%}} %>

                       
                       
                       
                        
                    returned <strong><%=_ds_nr%></strong> results</h2>
                </div>
                   
                <div class="headerTab">
                    <div class="resultTab">
                        <%if (Request["ba"] == "true"){%>
                        <p>Advanced Search</p>
                        <%}else{  %>
                        <p>Refine Your Results</p>
                        <%} %>
                    </div>
                    <div class="resultTop">
                    </div>
                </div>
                <div style="clear: both;">
                </div>
                <div class="resultMain">
                    <div class="contResulMain">
                       <p> <%if (Request["ba"] == "true"){%>
                        Advanced Search
                        <%}else{  %>
                        Refine Your Results
                        <%} %> for: <strong><% = _sTextFinder%></strong></p>
                        <div class="comboSearch">
                            <asp:PlaceHolder ID="PlaceHolder_Finder" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="btnRefine">
                            <input type="text" id="txtadv" name="txtadv" alt="" value="<% = _sTextFinder%>" style="width: 191px !important; float: left; margin: 2px 15px 0 0; <%=_show_TextFinder%>" />
                            <input type="image" src="<% = Global.globalSiteImagesPath %>/searchLeft.jpg" alt="" width="69" height="25" alt="" style="float: left;"/>
                            <input type="hidden"  id="findopt1" name = "findopt1" value="<% = _sFindOpt1%>" />
			    <input type="hidden"  id="nn" name = "nn" value="<% = Request["nn"]%>" />
                            <div style="clear: both">
                                </div>
                        </div>
                    </div>
                </div>
                </form>
                <div class="resultBotton"></div>
                <asp:PlaceHolder ID="PlaceHolder_resultControls_Head" runat="server"></asp:PlaceHolder>
                <asp:PlaceHolder ID="PlaceHolder_Result" runat="server"></asp:PlaceHolder>
                <asp:PlaceHolder ID="PlaceHolder_resultControls_Foot" runat="server"></asp:PlaceHolder>
            </div>
        </div>
        <!-- Side Bar -->
        <div id="sidebar-content">
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
