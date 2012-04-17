<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntNewPage.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="mnt_generic_d.aspx.cs" Inherits="mnt_generic_d" %>

<%@ Reference Control="~/CMS/mnt_SiteMap.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="GenericTemplate">
    <!-- TinyMCE -->

    <script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>

    <% Response.Write(str_TinyMCE); %>
    <!-- /TinyMCE -->

    <script src="js/templateD.js" type="text/javascript"></script>

    <div class="maincontainer">
        <div id="contCajas10">
            <!-- esta es la caja que aparecera en el template D -->
            <div id="div_Generid_information" runat="server" visible="false">
                <div class="cdro_caja2">
                    <div id="div_Generic_D" runat="server">
                    </div>
                </div>
                <div class="clear">
                </div>
                <asp:Button ID="btn_new" CssClass="class_addNew" runat="server" OnClick="btn_save_Click" /><br />
            </div>
            <div class="clear">
            </div>
            <div class="maincontainer">
                <div id="div_editNewgenD" runat="server" visible="false">
                    <div id="div_editFields" runat="server">
                        <p>
                            <label>
                                Title:</label>
                            <input type="text" class="class_texto2" id="txtGeneDTitle" value="<%Response.Write(PageTitle);%>"
                                name="txtGeneDTitle" />
                            <input type="hidden" id="txtGeneDPos" readonly value="<%Response.Write(Position);%>"
                                name="txtGeneDPos" />
                        </p>
                        <p>
                            <label>
                                Content:</label>
                            <textarea class="class_texto2" id="elm1" name="elm1" rows="20" cols="20"><%Response.Write(PageContent);%></textarea>
                        </p>
                    </div>
                    <input id="radiotipo" name="radiotipo" value="<%Response.Write(radioType); %>" type="hidden" />
                    <div id="div_Links" runat="server">
                        <% if(PageImage!=""){ %>
                        <p><label>Current Image: <% Response.Write(PageImage); %></label></p>
                        <% } %>
                        <p>
                            <label>
                                Image to display:</label>
                            <input type="radio" id="rb_select_image_upload" name="rb_image_type" value="upload"
                                checked onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />Upload
                            image
                            <input type="radio" id="rb_select_image_url" name="rb_image_type" value="url" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />External
                            Url
                            <div id="container_file_uploader_image">
                                <asp:FileUpload ID="Upload" CssClass="class_file2" runat="server"></asp:FileUpload>
                            </div>
                            <div id="container_url_image" style="display: none">
                                <input type="text" id="txt_image_url" name="txt_image_url" />
                            </div>
                            <asp:Label ID="lbimage" runat="server" Text="" Visible="false"></asp:Label>
                        </p>
                        <p>
                            <label>
                                Title:</label>
                            <input type="text" class="class_texto2" id="txtGeneDlinkTitle" name="txtGeneDlinkTitle"
                                value="<%Response.Write(GenedLink);%>" />
                            <asp:Label ID="lbGenx" runat="server" Text=""></asp:Label>
                        </p>
                        <p>
                            <label>
                                Link:</label>
                            <input id="txt_link" class="class_texto2" name="txt_link" type="text" value="<%Response.Write(linktopage1);%>" />
                            <div>
                                <input id="radioExternal" name="radioLinkType" value="external"  checked type="radio" onclick="return radioExternal_onclick('txt_link')" />
                                <label>External</label>
                                <input id="radioInternal" name="radioLinkType" type="radio" value="internal" onclick="return radioInternal_onclick('div_internalLink')" />
                                <label>Internal</label>
                                <input id="radioNewLanding" name="radioLinkType" type="radio" value="landingPage" onclick="return radioNewLanding_onclick()" />
                                <label><% Response.Write(add_edit_landing_page1); %>Landing Page</label>
                            </div>
                            <input id="LinkID" name="LinkID" type="hidden" />
                        </p>
                        <div id="htmlElement" name="htmlElement" style="display: none">
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
                                    <textarea id="elm2" name="elm2" cols="20" rows="15"><% Response.Write(landingPageContent); %></textarea>
                                </p>
                                <% if(landingPageImage!=""){ %>
                                <p><label>Current Image: <% Response.Write(landingPageImage); %></label></p>
                                <% } %>
                                <p>
                                    <label>
                                        Image:</label>
                                    <input type="radio" id="rbImageLandingPageUpload" name="rbImageLandingPage" value="upload"
                                        checked onclick="enable_reference_image(this.value, 'container_file_uploader_imageLP', 'container_url_imageLP');" />Upload
                                    image
                                    <input type="radio" id="rbImageLandingPageUrl" name="rbImageLandingPage" value="url" onclick="enable_reference_image(this.value, 'container_file_uploader_imageLP', 'container_url_imageLP');" />External
                                    Url
                                    <div id="container_file_uploader_imageLP">
                                        <asp:FileUpload ID="UploadLanding" runat="server" CssClass="class_file" />
                                    </div>
                                    <div id="container_url_imageLP" style="display: none">
                                        <input type="text" id="txt_image_urlLandingPage" name="txt_image_urlLandingPage" />
                                        
                                    </div>
                                    <asp:Label ID="lbimageinfo" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div id="div_internalLink" name="div_internalLink" style="display: none">
                            <asp:PlaceHolder ID="PlaceHolderMinisitemap" runat="server"></asp:PlaceHolder>
                        </div>
                        <asp:Button ID="btn_Cancel_edit" CssClass="class_Cancel" runat="server" OnClick="btn_Cancel_edit_Click" />
                        <asp:Button ID="btn_Save_new" runat="server" CssClass="class_btnSave" OnClick="btn_Save_new_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
<asp:Label runat="server" ID="mjtest"></asp:Label>
</asp:Content>
