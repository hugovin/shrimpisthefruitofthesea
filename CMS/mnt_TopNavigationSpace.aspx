<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntMasterPage.master" AutoEventWireup="true" CodeFile="mnt_TopNavigationSpace.aspx.cs" Inherits="CMS_mnt_TopNavigationSpace" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <anonymoustemplate>
    <div id="div_GroupName" runat ="server">
    </div>
    <asp:Button ID="btnNew" runat="server" Text="New" onclick="btnNew_Click" 
        Height="26px" Width="39px" />
    <asp:Label ID="lbError1" runat="server" Text=""></asp:Label>
    <div id="div_New" runat="server">
    </div>
    <div id="div_btnSave" runat= "server" visible ="false">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                onclick="btnCancel_Click" OnClientClick="return confirm('Do you want to leave without save')" />
            <asp:Button ID="btnSaveGroup" runat="server" Text="Save" 
                OnClientClick="return confirm('Do you want to save this Group?')" 
                onclick="btnSaveGroup_Click"/>
    </div>
    <p>        
        <asp:Label ID="lbError" runat="server" Text="*Check Your Information " 
            Visible="False"></asp:Label>
    </anonymoustemplate>
</asp:Content>
