<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntMasterPage.master"AutoEventWireup="true" CodeFile="mnt_BestSellers.aspx.cs" Inherits="mnt_BestSellers" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="Selection_Area" visible="false" runat="server">
       <div  class="titulos3">Please Selecat an Area&nbsp;&nbsp;&nbsp;<img src="images/arrow1.png" /></div> 
                        <div class="newOpciones"> 
                            <a href="#"onmouseover="ocultar_mnu();"><div class="newItem"><img src="images/point.png" border="0" />&nbsp;&nbsp;Ads</div></a> 
                            <a href="mnt_FeaturedSpace.aspx"onmouseover="ocultar_mnu();"><div class="newItem"><img src="images/point.png" border="0" />&nbsp;&nbsp;Theater</div></a> 
                            <a href="#" onmouseover="ver_mnu();">   <div class="newItem" id="puerta"><img src="images/point.png" border="0" />&nbsp;&nbsp;Tabs</div></a> 
                            <div id="contSubMenu"> 
                                  <a href="mnt_WhatsNew.aspx?Home=True"><div class="newSubItem">What’s New</div></a> 
                                  <a href="mnt_FeatureProducts.aspx?Home=True"><div class="newSubItem">Featured Products</div></a> 
                                  <a href="mnt_BestSellers.aspx?Home=True"><div class="newSubItem">Best Sellers</div></a> 
                            </div> 
                            <a href="mnt_HighLights.aspx" onmouseover="ocultar_mnu();"><div class="newItem"><img src="images/point.png" border="0" />&nbsp;&nbsp;Highligths</div></a> 
                            <a href="mnt_FeaturedBrands.aspx" onmouseover="ocultar_mnu();"><div class="newItem"><img src="images/point.png" border="0" />&nbsp;&nbsp;Featured Brands</div></a> 
         </div>
       </div>
    
    
    <div id="div_SubjectSelection" runat="server" visible ="false">
    <h3>Select Subject</h3>    
   <select name="cmb_test" id="cmb_test" onchange="window.location.href='mnt_BestSellers.aspx?SubjID=' + this.value;">
     <option value="0" selected ="selected">None</option>
         <asp:PlaceHolder ID="PLaceHolder_Cmb_Test" runat="server">
         </asp:PlaceHolder>
     </select>
        <br />
        <br />
        
    </div>
  <div id="WrapperProduct" runat="server">
     <div class="underBreadTitle">
        <h2>
            You are editing: <span>best Sellers</span>
        </h2>
    </div>
    <div class="clear">
    </div>

    <div class="bestSellerBox">
        <div class="titleBox">
            <h3>
                This Products are Best Sellers</h3>
            <img src="images/arrow02.jpg" width="18" height="10" /></div>
        <div class="data">
            <div id="div_FeatureProducts" runat="server" class="test">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="newProdTable">
            <asp:Button ID="btnAddFP" runat="server" CssClass="class_addNew" OnClick="btnAddFP_Click" /><asp:Label
                ID="lbErroMax" runat="server" Text="" Visible="true"></asp:Label>
        </div>
        <div class="newProdTable1">
            <table>
                <tr>
                    <td>
                        <div id="div_AddFP" runat="server">
                        </div>
                    </td>
                    <td>
                        <asp:ImageButton ID="btnSearch" src="images/goBtnTable.jpg" runat="server" Text="Search"
                            OnClick="btnSearch_Click" Width="60" Height="23" Visible="false" />
                        <asp:Label ID="lbPositionError" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="saveBtnTable">
        </div>
        <div class="clear">
        </div>
        <div class="data">
            <div id="div_SearchResult" runat="server">
            </div>
        </div>
        <div id="div_saveAndcancel" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnCancelFeat" CssClass="class_Cancel" runat="server" Visible="False"
                            OnClick="btnCancelFeat_Click" OnClientClick="return confirm('Do you want to leave without save?')" />
                    </td>
                    <td>
                        <asp:Button ID="btnSaveFeat" runat="server" CssClass="class_btnSave" OnClientClick="return confirm('Add New Product as Feature Product?')"
                            OnClick="btnSaveFeat_Click" Visible="False" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </div>
</asp:Content>
