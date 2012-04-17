<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntNewPage.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="mnt_generic_c.aspx.cs" Inherits="mnt_generic_c" %>

<asp:Content ID="ChildHolder" runat="server" ContentPlaceHolderID="GenericTemplate">
    <!-- TinyMCE -->

    <script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>

    <% Response.Write(str_TinyMCE); %>
    <!-- /TinyMCE -->
    <div id="contCajas9">
        <!-- esta es la caja que aparecera en el template C -->
        <div id="div_FAQS" runat="server">
            <div class="cdro_caja2">
                <div id="div_FaqsList" runat="server">
                </div>
                <input id="hidepos" name="hidepos" value="<%Response.Write(position); %>" type="hidden" />
            </div>
            <div class="clear">
            </div>
            <asp:Button ID="btn_New" CssClass="class_addNew" runat="server" OnClick="btn_New_Click">
            </asp:Button>
        </div>
        <div id="div_newEdit" runat="server" visible="false">
            <div id="div_Information" runat="server">
                <p>
                    <label>
                        Question</label>
                    <input id="txt_TitleC" class="class_texto2" name="txt_TitleC" type="text" value="<%Response.Write(Question); %>" />
                </p>
                <p>
                    <label>
                        Answer:</label>
                    <textarea id="elm1" name="elm1" rows="15" cols="80" style="width: 80%"><%Response.Write(Answer);%> </textarea>
                </p>
                <asp:Label ID="lbDate" runat="server" Text=""></asp:Label>
            </div>
            <asp:Button ID="btn_Cancel" runat="server" CssClass="class_Cancel" OnClientClick="return confirm('Do you want to leave without save?')"
                OnClick="btn_Cancel_Click"></asp:Button>
            <asp:Button ID="btn_Save" runat="server" CssClass="class_btnSave" OnClientClick="return confirm('Do you want to save?')"
                OnClick="btn_Save_Click"></asp:Button>
        </div>
    </div>
    <div style="display:none;">
        <div id="contCajas6"></div>
    </div>
</asp:Content>
