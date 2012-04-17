<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntNewPage.master" AutoEventWireup="true"
    CodeFile="mnt_generic_b.aspx.cs" Inherits="mnt_generic_b" ValidateRequest="false" %>

<asp:Content ID="ChildHolder" runat="server" ContentPlaceHolderID="GenericTemplate">
    <!-- TinyMCE -->

    <script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>

    <script src="js/Links2.js" type="text/javascript"></script>

    <% Response.Write(str_TinyMCE); %>
    <!-- /TinyMCE -->
    <!-- esta es la caja que aparecera en el template B -->
    
    <h2>Edit Section</h2>
    <hr />
    <div id="cdro_caja2" class="cdro_caja2" <% Response.Write(printCdroCaja2); %>>
        <div id="divNews" runat="server">
            <div id="div_NewInfo" runat="server">
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <asp:Button ID="btn_NewNI" CssClass="class_addNew" runat="server" Text="" OnClick="btn_NewNI_Click">
    </asp:Button>
    <br />
    <div class="div_centro">
        <div id="div_NewEdit" runat="server" visible="false">
            <div id="div_Information" runat="server">
                <p>
                <label>Title:</label>
                <input id="txt_Title" name="txt_Title" type="text" class="class_texto2" value="<%Response.Write(Title); %>" />
                </p>
                <p>
                <label>Content:</label>
                <textarea id="elm1" name="elm1" class="class_texto2" rows="15" cols="80" style="width: 80%"><%Response.Write(contentLoad);%> </textarea>
                </p>
                <p>
                <label>Share This:</label>
                <asp:CheckBox ID="cb_Sharethis" runat="server" Text="Share This">
                </asp:CheckBox>
                <asp:Label ID="lbDate" runat="server" Text=""></asp:Label>
                </p>
                <p>
                <label>PDF File:</label>
                <input type="radio" id="rb_select_image_upload" name="rb_image_type" value="upload" checked onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />Upload pdf
                <input type="radio" id="rb_select_image_url" name="rb_image_type" value="url" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />External Url
                <div id="container_file_uploader_image">
                    <asp:FileUpload ID="FUnewPDF" runat="server"></asp:FileUpload>
                </div>
                <div id="container_url_image" style="display:none">
                    <input type="text" id="txt_image_url" name="txt_image_url"/>
                </div>
                <p><asp:Label ID="lbl_error" runat="server" Text=""></asp:Label></p>
                <input id="hidePath" type="hidden" runat="server" />
                </p>

                <asp:Button ID="btn_CancelNI" runat="server" CssClass="class_Cancel" OnClientClick="return confirm('Do you want to leave without save?');"
                    OnClick="btn_CancelNI_Click"></asp:Button>
           
                <asp:Button ID="btn_SaveIN" runat="server" OnClientClick="return confirm('Do you want to save?');"
                    OnClick="btn_SaveIN_Click" CssClass="class_btnSave"></asp:Button>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <div>
    <div id="contCajas6" style="display:none;"></div>
    <asp:Label ID="mjtest" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>