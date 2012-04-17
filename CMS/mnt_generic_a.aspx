<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/CMS/CMS_mntNewPage.master"
    CodeFile="mnt_generic_a.aspx.cs" Inherits="mnt_generic_a" %>

<%@ Reference Control="~/CMS/mnt_SiteMap.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="GenericTemplate">

    <script src="js/templateA.js" type="text/javascript"></script>

    <!-- TinyMCE -->

    <script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>

    <% Response.Write(str_TinyMCE); %>
    <!-- /TinyMCE -->
    <div id="contCajas7">
        <!-- esta es la caja que aparecera en el template A -->
        <input id="LinkID" name="LinkID" type="hidden" />
        <h2>
            Main Page Content</h2>
        <p>
            <label>
                Title:</label>
            <input id="txt_GenATitle" name="txt_GenATitle" class="class_texto2" value="<%Response.Write(titleMainPage); %>"
                type="text" /></p>
        <p>
            <label>
                Comments:</label>
            <textarea id="elm1" name="elm1" rows="15" cols="80" style="width: 100%"><%Response.Write(contentLoad); %></textarea></p>
        <p>
            <label>
                Image to display:</label></p>
        <p>
            <input type="radio" id="rb_select_image_upload" name="rb_image_type" value="upload"
                checked onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />Upload
            image
            <input type="radio" id="rb_select_image_url" name="rb_image_type" value="url" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />External
            Url
            <div id="container_file_uploader_image">
                <asp:FileUpload ID="FUimage" CssClass="class_file2" runat="server"></asp:FileUpload>
            </div>
            <div id="container_url_image" style="display: none">
                <input type="text" id="txt_image_url" name="txt_image_url" value="<%Response.Write(imageA);%>" />
            </div>
            <asp:Label ID="Lberror" runat="server" Text=""></asp:Label>
            <%if (imageA != ""){%>
            <p>
                Current Image:
                <%Response.Write(imageA);%></p>
            <a href="../images/<%Response.Write(imageA);%>" target="_blank" id="mb2" class="mb"
                title="Current Image"><span class="class_btnActualImage"></span></a>
            <%} %>
            <input type="hidden" id="imageA" name="imageA" value="<%Response.Write(imageA);%>" />
        </p>
        <div id="optionalLinks">
        </div>
        <label>
            Optional Links</label>
        <p>
            <input type="radio" value="yes" name="optionalLinks" onclick="displayOptionalLinks(this.value)" />Yes
            <input type="radio" value="no" name="optionalLinks" checked="checked" onclick="displayOptionalLinks(this.value)" />No
        </p>
        <div id="tabs">
            <ul>
                <li><a href="#tabs-1">Link 1</a></li>
                <li><a href="#tabs-2">Link 2</a></li>
            </ul>
            <div class="clear">
            </div>
            <div id="tabs-1" class="container_segment">
                <p class="emailService">
                    <label>
                        <strong>Link Title 1:</strong>
                    </label>
                    <input id="txtLinkTitle1" class="class_file2" name="txtLinkTitle1" value="<%Response.Write(txtLinkTitle1);%>"
                        type="text" />
                </p>
                <p class="emailService">
                    <label>
                        <strong>Link 1:</strong>
                    </label>
                    <input id="txtLink1" class="class_file2" name="txtLink1" type="text" title="<%Response.Write(txtLink1);%>"
                        value="<%Response.Write(txtLink1);%>" />
                </p>
                <p>
                    <input id="radioExternal" checked name="rbLinkType1" value="external" type="radio" onclick="return radioExternal2_onclick(1)" />External
                    <input id="radioInternal" name="rbLinkType1" value="internal" type="radio" onclick="return radioInternal2_onclick(1)" />Internal
                    <input id="radioNewLanding" name="rbLinkType1" value="landing" type="radio" onclick="return radioNewLanding2_onclick(1)" />
                    <input id="radiotipo" value="<%Response.Write(rbLinkType1ID);%>" name="radiotipo" type="hidden" />
                    <%Response.Write(add_edit_landing_page1); %>Landing Page
                </p>
                <div id="htmlElement1" style="display: none;">
                    <hr />
                    <div id="div_landingPage1">
                        <div id="landing_page_title_1" class="landing_page_title">
                            Title:
                            <input type="text" class="class_texto2" name="txtlandTitle1" value="<% Response.Write(txt_gen_page_title_1); %>" />
                        </div>
                        <div id="landing_page_content_1" class="landing_page_content">
                            <p>
                                Content:
                                <textarea id="elmlandpage1" name="elmlandpage1" cols="20" rows="15"><% Response.Write(txt_gen_page_content_1); %></textarea><br />
                            </p>
                            <p>
                                Image:
                            </p>
                            <p>
                                <input type="radio" id="rb_image_landing_upload" name="rb_image_type_landing1" value="upload"
                                    checked onclick="enable_reference_image(this.value, 'container_uploader_landing1', 'container_url_landing1');" />Upload
                                image
                                <input type="radio" id="rb_image_landing_url" name="rb_image_type_landing1" value="url"
                                    onclick="enable_reference_image(this.value, 'container_uploader_landing1', 'container_url_landing1');" />External
                                Url
                            </p>
                            <p>
                                <% if(txt_gen_page_image_1!="" || txt_gen_page_image_1 != null){Response.Write("Current image: "+txt_gen_page_image_1);} %>
                            </p>
                            <asp:Label ID="lbErrorUploadImageLP1" runat="server" Text=""></asp:Label>
                            <div id="container_uploader_landing1">
                                <asp:FileUpload ID="UploadLanding1" runat="server" CssClass="class_file" />
                            </div>
                            <div id="container_url_landing1" style="display: none">
                                <input type="text" id="txt_url_image_landing1" name="txt_url_image_landing1" />
                            </div>
                            <asp:Label ID="lbimageinfo1" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lbCurrentImage" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
                <div id="div_internalLink1">
                </div>
            </div>
            <div id="tabs-2" class="container_segment">
                <p class="emailService">
                    <label>
                        <strong>Link Title 2:</strong>
                    </label>
                    <input id="txtLinkTitle2" class="class_file2" name="txtLinkTitle2" value="<%Response.Write(txtLinkTitle2);%>"
                        type="text" />
                </p>
                <p class="emailService">
                    <label>
                        <strong>Link 2:</strong>
                    </label>
                    <input id="txtLink2" class="class_file2" name="txtLink2" type="text" value="<%Response.Write(txtLink2);%>" />
                </p>
                <p>
                    <input id="radioExternal2" checked name="rbLinkType2" value="external" type="radio" onclick="return radioExternal2_onclick(2)" />External
                    <input id="radioInternal2" name="rbLinkType2" value="internal" type="radio" onclick="return radioInternal2_onclick(2)" />Internal
                    <input id="radioNewLanding2" name="rbLinkType2" value="landing" type="radio" onclick="return radioNewLanding2_onclick(2)" /><%Response.Write(add_edit_landing_page2); %>Landing Page
                    <input id="radioType2" value="<%Response.Write(rbLinkType2ID);%>" name="radioType2"
                        type="hidden" />
                </p>
                <div id="htmlElement2" style="display: none;">
                    <hr />
                    <div id="div_landingPage2">
                        <div id="landing_page_title_2" class="landing_page_title">
                            Title:
                            <input type="text" class="class_texto2" name="txtlandTitle2" value="<% Response.Write(txt_gen_page_title_2); %>" />
                        </div>
                        <div id="landing_page_content_2" class="landing_page_content">
                            <p>
                                Content:
                                <textarea id="elmlandpage2" name="elmlandpage2" cols="20" rows="15"><% Response.Write(txt_gen_page_content_2); %></textarea><br />
                            </p>
                            <p>
                                Image:
                            </p>
                            <p>
                                <input type="radio" id="rb_image_landing_upload2" name="rb_image_type_landing2" value="upload"
                                    checked onclick="enable_reference_image(this.value, 'container_uploader_landing2', 'container_url_landing2');" />Upload
                                image
                                <input type="radio" id="rb_image_landing_url2" name="rb_image_type_landing2" value="url"
                                    onclick="enable_reference_image(this.value, 'container_uploader_landing2', 'container_url_landing2');" />External
                                Url
                            </p>
                            <p>
                                <% if(txt_gen_page_image_2!="" || txt_gen_page_image_2 != null){Response.Write("Current image: "+txt_gen_page_image_2);} %>
                            </p>
                            <asp:Label ID="lbErrorUploadImageLP2" runat="server" Text=""></asp:Label>
                            <div id="container_uploader_landing2">
                                <asp:FileUpload ID="UploadLanding2" runat="server" CssClass="class_file" />
                            </div>
                            <div id="container_url_landing2" style="display: none">
                                <input type="text" id="txt_url_image_landing2" name="txt_url_image_landing2" />
                            </div>
                            <asp:Label ID="lbimageinfo2" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lbCurrentImage2" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
                <div id="div_internalLink2">
                </div>
            </div>
        </div>
        <p>
            <hr />
            <label>
                Creation Date:
            </label>
            <asp:Label ID="lbCreationDate" runat="server" Text=""></asp:Label></p>
        <div id="saveCancel">
            <asp:Button ID="btn_Cancel" runat="server" CssClass="class_Cancel" OnClick="btn_Cancel_Click">
            </asp:Button>
            <asp:Button ID="btn_Save" runat="server" CssClass="class_btnSave" OnClick="btn_Save_Click"
                UseSubmitBehavior="False" />
        </div>
    </div>
    <div id="internal_links_container" style="display: none;">
        <p>
            Click on the link to select an internal page.</p>
        <asp:PlaceHolder ID="PlaceHolderMinisitemap" runat="server"></asp:PlaceHolder>
        <div class="clear">
        </div>
    </div>
    <div class="clear">
    </div>
    <!-- fin caja Template A -->
    <input id="hidePath" runat="server" type="hidden" />
    <%if (rbLinkType1ID == 1) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioExternal').checked = 'checked';</script>"); } %>
    <%if (rbLinkType1ID == 2) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioInternal').checked = 'checked';</script>"); } %>
    <%if (rbLinkType1ID == 3) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioNewLanding').checked = 'checked';</script>"); } %>
    <%if (rbLinkType2ID == 1) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioExternal2').checked = 'checked';</script>"); } %>
    <%if (rbLinkType2ID == 2) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioInternal2').checked = 'checked';</script>"); } %>
    <%if (rbLinkType2ID == 3) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioNewLanding2').checked = 'checked';</script>"); } %>
    <div id="p_link_format_warning">
        <input id="LinkIDmore" name="LinkIDmore" type="hidden" />
        <input id="txtLinkmore2" name="txtLinkmore2" type="hidden" />
        <input id="contCajas6" name="contCajas6" type="hidden" />
    </div>
</asp:Content>
