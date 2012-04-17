<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntNewPage.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="mnt_generic_e.aspx.cs" Inherits="mnt_generic_e" %>

<%@ Reference Control="~/CMS/mnt_SiteMap.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="GenericTemplate">
    <!-- TinyMCE -->

    <script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>

    <% Response.Write(str_TinyMCE); %>
    <!-- /TinyMCE -->

    <script src="js/templateE.js" type="text/javascript"></script>

    <div class="maincontainer">
        <div id="contCajas11">
            <!-- esta es la caja que aparecera en el template E -->
            <div class="div_centro">
                <p>
                    <label>
                        Title:</label><input id="txt_GenATitle" class="class_texto2" name="txt_GenATitle"
                            value="<%Response.Write(TitleE); %>" type="text" /></p>
                <p style="display:none;">
                    <label>
                        Place: This content will appear bellow the title in the rendered page.</label><input
                            id="txt_Place" class="class_file2" name="txt_Place" value="<%Response.Write(Place); %>"
                            type="text" /></p>
                <p>
                    <label>
                        Content:</label><textarea id="elm1" class="class_texto2" name="elm1" rows="15" cols="80"
                            style="width: 80%"><%Response.Write(contentLoad); %></textarea></p>
                <p>
                    <label>
                        Title:</label><input type="text" class="class_file2" id="txt_linkTitleE" name="txt_linkTitleE"
                            value="<%Response.Write(GenedLink);%>" /></p>
                <p>
                    <label>
                        Link:</label>
                    <input id="txt_link" class="class_file2" name="txt_link" type="text" value="<%Response.Write(linktopage1);%>" />
                    <div>
                        <input id="radioExternal" name="radioLinkType" value="external" checked type="radio" onclick="return radioExternal_onclick('txt_link')" /><label>External</label>
                        <input id="radioInternal" name="radioLinkType" value="internal" type="radio" onclick="return radioInternal_onclick('div_internalLink')" /><label>Internal</label>
                        <input id="radioNewLanding" name="radioLinkType" value="landing" type="radio" onclick="return radioNewLanding_onclick()" /><label><% Response.Write(add_edit_landing_page1); %>
                            Landing Page</label>
                    </div>
                    <input id="hideid" runat="server" type="hidden" />
                </p>
                <div id="htmlElement" style="display: none">
                    <div id="div_landingPage" name="div_landingPage">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        <p>
                            <label>
                                Title:</label>
                            <input type="text" class="class_texto2" name="txtlandTitle" value="<% Response.Write(landingPageTitle); %>" />
                        </p>
                        <p>
                            <label>
                                Content:</label>
                            <textarea id="elmlandpage" name="elmlandpage" cols="20" rows="15"><% Response.Write(landingPageContent); %></textarea>
                        </p>
                        <% if(landingPageImage!=""){ %>
                        <p><label>Current Image: <% Response.Write(landingPageImage); %></label></p>
                        <% } %>
                        <p>
                            <label>
                                Image:</label>
                            <input type="radio" id="rb_select_image_upload" name="rb_image_type" value="upload"
                                checked onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" /><label>Upload
                                    image</label>
                            <input type="radio" id="rb_select_image_url" name="rb_image_type" value="url" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" /><label>External
                                Url</label>
                            <div id="container_file_uploader_image">
                                <asp:FileUpload ID="UploadLanding" runat="server" CssClass="class_file" />
                            </div>
                            <div id="container_url_image" style="display: none">
                                <input type="text" id="txt_image_url" name="txt_image_url" />
                            </div>
                            <asp:Label ID="lbimageinfo" runat="server" Text=""></asp:Label>
                        </p>
                    </div>
                </div>
                <div id="div_internalLink" name="div_internalLink" style="display: none">
                    <asp:PlaceHolder ID="PlaceHolderMinisitemap" runat="server"></asp:PlaceHolder>
                </div>
                <p>
                    <label>
                        Creation Date:</label>
                    <asp:Label ID="lbCreationDate" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lbGenx" runat="server" Text=""></asp:Label>
                </p>
                <asp:Button ID="btn_Cancel" CssClass="class_Cancel" runat="server"></asp:Button>
                <asp:Button ID="btn_Save" runat="server" CssClass="class_btnSave" OnClick="btn_Save_Click"
                    PostBackUrl="~/CMS/mnt_generic_e.aspx" UseSubmitBehavior="False" />
                <input id="LinkID" name="LinkID" type="hidden" />
            </div>
        </div>
        <!-- fin caja Template E -->
    </div>
    <input id="hidePath" runat="server" type="hidden" />
    <input id="GenaIDHide" runat="server" type="hidden" />
    <input id="radiotipo" name="radiotipo" value="<%Response.Write(tipo); %>" type="hidden" />

    <script type="text/javascript">
        $('#div_internalLink').children().children().children('img').css('display','none');
    </script>
    <input type="hidden" id="landingPageFound" value="<% Response.Write(has_landing_page1); %>"
    <asp:Label runat="server" ID="test"></asp:Label>
</asp:Content>
