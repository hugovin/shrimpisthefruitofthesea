<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMS/CMS_mntMasterPage.master" CodeFile="mnt_Specials.aspx.cs" Inherits="CMS_mnt_Specials" %>



<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <anonymoustemplate>
    <h1>Specials</h1><br />
    
       
    <div id="div_SubjectSelection" runat="server" visible ="false">
    <h3>Select Subject</h3>    
     <select name="cmb_test" id="cmb_test" onchange="window.location.href='mnt_Specials.aspx?SubjID=' + this.value;">
     <option value="0" selected ="selected">None</option>
         <asp:PlaceHolder ID="PLaceHolder_Cmb_Test" runat="server">
         </asp:PlaceHolder>
     </select>
        <br />
        <br />
        
    </div>
    
    <div class="underBreadTitle">
        <h2>
            You are editing: <span>Specials</span>
        </h2>
    </div>
    <div class="clear">
    </div>
    <div class="bestSellerBox">
        <div class="titleBox">
            <h3>
                This Products are Feature Products</h3>
            <img src="images/arrow02.jpg" width="18" height="10" /></div>
        <div class="data">
            <div id="div_Specials" runat="server">
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
                        <asp:Button ID="btnSaveFeat" runat="server" CssClass="class_btnSave" OnClientClick="return confirm('Add New Product as Special?')"
                            OnClick="btnSaveFeat_Click" Visible="False" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </anonymoustemplate>
</asp:Content>
