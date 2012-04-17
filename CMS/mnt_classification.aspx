<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntNewPage.master" AutoEventWireup="true" CodeFile="mnt_classification.aspx.cs" Inherits="CMS_mnt_classification" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="GenericTemplate" Runat="Server">
    <asp:PlaceHolder ID="PlaceHolder_Classification" runat="server"></asp:PlaceHolder>
    <asp:Button ID="btn_New" CssClass="class_addNew" runat="server" 
                                    Text="" OnClick="btn_New_Click" 
        Visible="false" Height="30px" Width="98px" >
    </asp:Button>
    
    
     <div id="contenido" runat="server" visible="false">  
        <input id="txt_ClassId" name="txt_ClassId" class="class_texto2" value="<%Response.Write(_ClassId); %>" type="hidden" />  
        Title:<br />
        <input id="txt_Title" name="txt_Title" class="class_texto2" value="<%Response.Write(_Description); %>"
            type="text" />
        <br />
        Comments:<br />
        <br />
        <textarea id="elm1" name="Comments" rows="15" cols="80" style="width: 80%"><%Response.Write(_Content); %></textarea>
        <br />
        <br />               
    </div>
    

    <div id="div_insert" runat="server" visible="false">
        <table border="0" cellspacing="3" cellpadding="0">
            <tr>
                <td>
                     <asp:Button ID="btn_Cancel" runat="server" CssClass="class_Cancel" onclick="btn_Cancel_Click"></asp:Button>
                </td>
                <td width="80px">
                    &nbsp;
                </td>
                <td>
                     <asp:Button ID="btn_Save" runat="server" CssClass="class_btnSave" OnClick="btn_Save_Click" PostBackUrl="~/CMS/mnt_classification.aspx"   UseSubmitBehavior="False" />
                </td>
            </tr>
        </table> 
    </div>
    
    <div id="div_update" runat="server" visible="false">
        <table border="0" cellspacing="3" cellpadding="0">
            <tr>
                <td>
                     <asp:Button ID="btn_Cancel_Upd" runat="server" CssClass="class_Cancel" onclick="btn_Cancel_Upd_Click"></asp:Button>
                </td>
                <td width="80px">
                    &nbsp;
                </td>
                <td>
                     <asp:Button ID="btn_Update" runat="server" CssClass="class_btnSave" OnClick="btn_Update_Click" PostBackUrl="~/CMS/mnt_classification.aspx"   UseSubmitBehavior="False" />
                </td>
            </tr>
        </table> 
    </div>
</asp:Content>

