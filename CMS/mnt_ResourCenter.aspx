<%@ Page Language="C#" MasterPageFile="~/CMS/CMS_mntMasterPage.master" ValidateRequest="false"
    AutoEventWireup="true" CodeFile="mnt_ResourCenter.aspx.cs" Inherits="mnt_ResourCenter" %>

<%@ Reference Control="~/CMS/mnt_SiteMap.ascx" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="content1" runat="server">
    <!-- TinyMCE -->
    <!-- TinyMCE -->

    <script type="text/javascript" src="tinyMCE3_3_9/tiny_mce/tiny_mce.js"></script>
    <% Response.Write(str_TinyMCE); %>
    <!-- /TinyMCE -->

    <script src="js/Links.js" type="text/javascript"></script>

    <div class="MainContainer">
        <div class="newOpciones2"></div>
        <div class="master_container">
            <div class="titulos2">
                <h3>Main Image:</h3>
                    <div class="containerSection">
                        <p><input type="radio" id="rb_select_image_upload" name="rb_image_type" value="upload" checked="checked" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />Upload image</p>
                        <p><input type="radio" id="rb_select_image_url" name="rb_image_type" value="url" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />External Url</p>
                        <div id="container_file_uploader_image">
                            <asp:FileUpload ID="UploadMain" CssClass="class_file" runat="server"></asp:FileUpload>
                            <asp:Button ID="Btn_UploadMain" CssClass="class_btnUpload" runat="server" Text=""
                                OnClick="Btn_UploadMain_Click" UseSubmitBehavior="False" />
                        </div>
                        <div id="container_url_image" style="display: none">
                            <input type="text" id="txt_image_url" name="txt_image_url" />
                        </div>
                    </div>
                <input id="imagemain" name="imagemain" style="background-color: transparent; border: 0px; width: auto" value="<%Response.Write(imagemain);%>" type="text" />
                <input id="lbimagemainhide" name="lbimagemainhide" value="<%Response.Write(imagemain); %>" type="hidden" />
                <div class="clear"></div>
            </div>
            <div class="clear">
            </div>
            <div id="contCajas7">
                <div id="contCajas4">
                    <div id="contCajas14">
                        <div class="cdro_titulo1">
                            Left Side&nbsp;&nbsp;<img src="images/arrow1.png" /></div>
                        Title:<br />
                        <input id="txt_title1" name="txt_title1" class="class_texto2" value="<%Response.Write(title1);%>"
                            class="" type="text" /><br />
                        <br />
                        Content:<br />
                        <br />
                        <textarea id="elm1" rows="15" cols="80" name="elm1"><%Response.Write(content1);%></textarea><br />
                        <br />
                        <br />
                        <table>
                            <tr>
                                <td width="87">
                                </td>
                                <td width="78">
                                    <strong>Email:</strong>
                                </td>
                                <td width="300">
                                    <input id="txt_email1" size="50" name="txt_email1" value="<%Response.Write(email1);%>"
                                        type="text" /><br />
                                </td>
                                <td width="181">
                                </td>
                            </tr>
                            <tr>
                                <td width="87" colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="87">
                                </td>
                                <td width="78">
                                    <strong>Contact:</strong>
                                </td>
                                <td width="300">
                                    <input id="txt_contact1" size="50" name="txt_contact1" value="<%Response.Write(phone1);%>"
                                        type="text" />
                                </td>
                                <td width="181">
                                </td>
                            </tr>
                            <tr>
                                <td width="87" colspan="3">
                                </td>
                            </tr>
                        </table><br />
                        <div class="cdro_caja0">
                            <div class="cdro_contenido">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <h4>
                                                +More Link</h4>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Link
                                        </td>
                                        <td>
                                            <input id="txtLinkmore" class="class_file" name="txtLinkmore" type="text" value="<%Response.Write(txtmoreLink);%>" /><br />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <br />
                                            <input id="radioMore1" checked="checked" name="radiomore" type="radio" onclick="return radioExternalmore_onclick('txt_link')" />External&nbsp;&nbsp;
                                            <input id="radioMore2" name="radiomore" type="radio" onclick="return radioInternalmore_onclick('div_internalLink')" />Internal&nbsp;&nbsp;
                                            <input id="radioMore3" name="radiomore" type="radio" onclick="return radioNewLandingmore_onclick()" />New
                                            <input id="radiotipomore" value="<%Response.Write(RadioTipoMore1);%>" name="radiotipomore"
                                                type="hidden" />
                                            Landing Page
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <br />
                        <div class="cdro_caja0">
                            <div class="cdro_contenido">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="27">
                                            Image
                                        </td>
                                        <td width="302">
                                            <input type="radio" id="rb_image_type_left_upload" name="rb_image_type_left" value="upload"
                                                checked="checked" onclick="enable_reference_image(this.value, 'container_file_uploader_image_left', 'container_url_image_left');" />Upload
                                            image
                                            <input type="radio" id="rb_image_type_left_url" name="rb_image_type_left" value="url"
                                                onclick="enable_reference_image(this.value, 'container_file_uploader_image_left', 'container_url_image_left');" />External
                                            Url
                                            <div id="container_file_uploader_image_left"><br />
                                                <asp:FileUpload ID="UploadImage1" CssClass="class_file" runat="server"></asp:FileUpload>
                                            </div>
                                            <div id="container_url_image_left" style="display: none">
                                                <input type="text" id="txt_image_url_left" name="txt_image_url_left" />
                                            </div>
                                        </td>
                                        <td width="121">
                                            <asp:Button ID="btnUploadleft" runat="server" CssClass="class_btnUpload" OnClick="btnUploadleft_Click"
                                                UseSubmitBehavior="False" />
                                            <input id="imageleft" name="imageleft" style="background-color: transparent; border: 0px;"
                                                value="<%Response.Write(imageleft);%>" type="text" />
                                            <input id="lbimagelefthide" name="lbimagelefthide" value="<%Response.Write(imageleft); %>"
                                                type="hidden" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Link
                                        </td>
                                        <td>
                                            <input id="txt_link" class="class_file" name="txt_link" type="text" value="<%Response.Write(linktopage1);%>" /><br />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <br />
                                            <input id="radioExternal" checked="checked" name="radio" type="radio" onclick="return radioExternal_onclick('txt_link')" />External&nbsp;&nbsp;
                                            <input id="radioInternal" name="radio" type="radio" onclick="return radioInternal_onclick('div_internalLink')" />Internal&nbsp;&nbsp;
                                            <input id="radioNewLanding" name="radio" type="radio" onclick="return radioNewLanding_onclick()" />New
                                            <input id="radiotipo" value="<%Response.Write(RadioTipo1);%>" name="radiotipo" type="hidden" />
                                            Landing Page
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <!-- fin caja Template A -->
                </div>
            </div>
            <div class="clear">
            </div>
            <div id="contCajas4">
                <!-- este div permanecera oculto -->
                <div id="contCajas7">
                    <div class="cdro_titulo1">
                        Right Side&nbsp;&nbsp;<img src="images/arrow1.png" /></div>
                    Title:<br />
                    <input id="txt_title2" class="class_texto2" name="txt_title2" value="<%Response.Write(title2); %>"
                        type="text" /><br />
                    <br />
                    <br />
                    Content:<br />
                    <br />
                    <textarea id="elm2" rows="15" cols="80" style="width: 100%" name="elm2"><%Response.Write(content2); %></textarea>
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td width="87">
                            </td>
                            <td width="78">
                                <strong>Email:</strong>
                            </td>
                            <td width="300">
                                <input id="txt_email2" size="50" name="txt_email2" value="<%Response.Write(email2);%>"
                                    type="text" />
                            </td>
                            <td width="181">
                            </td>
                        </tr>
                        <tr>
                            <td width="87" colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="87">
                            </td>
                            <td width="78">
                                <strong>Contact:</strong>
                            </td>
                            <td width="300">
                                <input id="txt_contact2" size="50" name="txt_contact2" value="<%Response.Write(phone2);%>"
                                    type="text" />
                            </td>
                            <td width="181">
                            </td>
                        </tr>
                        <tr>
                            <td width="87" colspan="3">
                            </td>
                        </tr>
                    </table><br />
                    <div class="cdro_caja0">
                        <div class="cdro_contenido">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <h4>
                                            +More Link</h4>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Link
                                    </td>
                                    <td>
                                        <input id="txtLinkmore2" class="class_file" name="txtLinkmore2" type="text" value="<%Response.Write(txtmoreLink2);%>" /><br />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <br />
                                        <input id="radiomore12" checked="checked" name="radiomore2" type="radio" onclick="return radioExternalmore2_onclick('txt_link')" />External&nbsp;&nbsp;
                                        <input id="radiomore22" name="radiomore2" type="radio" onclick="return radioInternalmore2_onclick('div_internalLink')" />Internal&nbsp;&nbsp;
                                        <input id="radiomore32" name="radiomore2" type="radio" onclick="return radioNewLandingmore2_onclick()" />New
                                        <input id="radiotipomore2" value="<%Response.Write(RadioTipoMore2);%>" name="radiotipomore2"
                                            type="hidden" />
                                        Landing Page
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <br />
                    <div class="cdro_caja0">
                        <div class="cdro_contenido">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="27">
                                        Image&nbsp;&nbsp;
                                    </td>
                                    <td width="302">
                                    
                                        <input type="radio" id="rb_image_type_right_upload" name="rb_image_type_right" value="upload"
                                            checked="checked" onclick="enable_reference_image(this.value, 'container_file_uploader_image_right', 'container_url_image_right');" />Upload
                                        image
                                        <input type="radio" id="rb_image_type_right_url" name="rb_image_type_right" value="url"
                                            onclick="enable_reference_image(this.value, 'container_file_uploader_image_right', 'container_url_image_right');" />External
                                        Url
                                        <div id="container_file_uploader_image_right"><br />
                                            <asp:FileUpload ID="UploadImage2" CssClass="class_file" runat="server"></asp:FileUpload>
                                        </div>
                                        <div id="container_url_image_right" style="display: none">
                                            <input type="text" id="txt_image_url_right" name="txt_image_url_right" />
                                        </div><br />
                                    </td>
                                    <td width="121">
                                        <asp:Button ID="btnUploadRight" runat="server" CssClass="class_btnUpload" Text=""
                                            OnClick="btnUploadRight_Click" UseSubmitBehavior="False" />
                                        <input id="imageright" name="imageright" style="background-color: transparent; border: 0px;"
                                            value="<%Response.Write(imageright);%>" type="text" />
                                        <input id="lbimagerighthide" name="lbimagerighthide" value="<%Response.Write(imageright); %>"
                                            type="hidden" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Link
                                    </td>
                                    <td>
                                        <input id="txt_link2" class="class_file" name="txt_link2" type="text" value="<%Response.Write(linktopage2);%>" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td><br />
                                        <input id="radioExternal2" checked name="radio2" type="radio" onclick="return radioExternal2_onclick('txt_link2')" />External&nbsp;
                                        <input id="radioInternal2" name="radio2" type="radio" onclick="return radioInternal2_onclick('div_internalLink')" />Internal&nbsp;&nbsp;
                                        <input id="radioNewLanding2" name="radio2" type="radio" onclick="return radioNewLanding2_onclick()" />New
                                        Landing Page&nbsp;&nbsp;
                                        <input id="radiotipo2" value="<%Response.Write(RadioTipo2);%>" name="radiotipo2"
                                            type="hidden" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="clear">
                <hr />
            </div>
            <!-- fin caja Template A -->
            <div id="contCajas8">
                <br />
                <a href="#" style="float: left">
                    <asp:Button ID="btnCancelRC" CssClass="class_Cancel" runat="server" Text="" /></a><a
                        href="#" style="float: right">
                        <asp:Button ID="btnSaveRC" runat="server" CssClass="class_btnSave" Text="" OnClick="btnSaveRC_Click" /></a><div
                            class="clear">
                        </div>
            </div>
        </div>
    </div>
    <!-- fin cuerpo -->
    </div>
    <!-- /TinyMCE -->
    <div id="none2" style="clear: both">
        <input id="LinkID" name="LinkID" type="hidden" />
        <input id="LinkIDmore" name="LinkIDmore" type="hidden" />
        <input id="LinkTypeT" name="LinkTypeT" type="hidden" />
    </div>
    <div id="div_internalLink" name="div_internalLink" style="width: 888px; position: absolute;
        margin: -300px 0 0 45%; background-color: #FFFFFF; border: solid 5px #999999;
        visibility: hidden; top: 762px; left: -368px;">
        <asp:PlaceHolder ID="PlaceHolderMinisitemap" runat="server"></asp:PlaceHolder>
    </div>
    <div id="none3" style="clear: both">
    </div>
    <div id="htmlElement" name="htmlElement" style="width: 888px; position: absolute;
        margin: -300px 0 0 45%; background-color: #FFFFFF; border: solid 5px #999999;
        visibility: hidden; top: 1175px; left: -282px;">
        <div style="padding: 5px;">
            <div id="div_landingPage" name="div_landingPage">
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <br />
                Title:<br />
                <input type="text" class="class_texto2" name="txtlandTitle" id="txtlandTitle" /><br />
                Content:<br />
                <textarea id="elmlandpage" name="elmlandpage" cols="20" rows="15"></textarea><br />
                <input type="radio" id="rb_select_image_uploadLanding" name="rb_image_typeLanding"
                    value="upload" checked="checked" onclick="enable_reference_image(this.value, 'container_file_uploader_image_Landing', 'container_url_image_Landing');" />Upload
                image
                <input type="radio" id="rb_select_image_urlLanding" name="rb_image_typeLanding" value="url"
                    onclick="enable_reference_image(this.value, 'container_file_uploader_image_Landing', 'container_url_image_Landing');" />External
                Url
                <div id="container_file_uploader_image_Landing">
                    <asp:FileUpload ID="UploadLanding" runat="server" CssClass="class_file" />
                    <asp:Label ID="lbimageinfo" name="lbimageinfo" runat="server"></asp:Label>
                </div>
                <div id="container_url_image_Landing" style="display: none">
                    <input type="text" id="imageUrlLanding" name="imageUrlLanding" />
                </div>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btn_CancelLand" runat="server" OnClick="btn_CancelLand_Click" CssClass="class_Cancel" />
                        </td>
                        <td>
                            <asp:Button ID="btn_SaveLand" runat="server" OnClick="btn_SaveLand_Click" CssClass="class_btnSave" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <%if (RadioTipo1 == 1) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioExternal').checked = 'checked';</script>"); } %>
    <%if (RadioTipo1 == 2) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioInternal').checked = 'checked';</script>"); } %>
    <%if (RadioTipo1 == 3) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioNewLanding').checked = 'checked';</script>"); } %>
    <%if (RadioTipo2 == 1) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioExternal2').checked = 'checked';</script>"); } %>
    <%if (RadioTipo2 == 2) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioInternal2').checked = 'checked';</script>"); } %>
    <%if (RadioTipo2 == 3) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioNewLanding2').checked = 'checked';</script>"); } %>
    <%if (RadioTipoMore1 == 1) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioMore1').checked = 'checked';</script>"); } %>
    <%if (RadioTipoMore1 == 2) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioMore2').checked = 'checked';</script>"); } %>
    <%if (RadioTipoMore1 == 3) { Response.Write("<script type=\"text/javascript\">document.getElementById('radioMore3').checked = 'checked';</script>"); } %>
    <%if (RadioTipoMore2 == 1) { Response.Write("<script type=\"text/javascript\">document.getElementById('radiomore12').checked = 'checked';</script>"); } %>
    <%if (RadioTipoMore2 == 2) { Response.Write("<script type=\"text/javascript\">document.getElementById('radiomore22').checked = 'checked';</script>"); } %>
    <%if (RadioTipoMore2 == 3) { Response.Write("<script type=\"text/javascript\">document.getElementById('radiomore32').checked = 'checked';</script>"); } %>
    <asp:Label runat="server" ID="mjtest"></asp:Label>
    <input type="hidden" id="landingPageTitle1" value="<% Response.Write(landingPageTitle1); %>" />
    <input type="hidden" id="landingPageTitle2" value="<% Response.Write(landingPageTitle2); %>" />
    <input type="hidden" id="landingPageTitle3" value="<% Response.Write(landingPageTitle3); %>" />
    <input type="hidden" id="landingPageTitle4" value="<% Response.Write(landingPageTitle4); %>" />
    <input type="hidden" id="landingPageContent1" value="<% Response.Write(landingPageContent1); %>" />
    <input type="hidden" id="landingPageContent2" value="<% Response.Write(landingPageContent2); %>" />
    <input type="hidden" id="landingPageContent3" value="<% Response.Write(landingPageContent3); %>" />
    <input type="hidden" id="landingPageContent4" value="<% Response.Write(landingPageContent4); %>" />
    <input type="hidden" id="landingPageImage1" value="<% Response.Write(landingPageImage1); %>" />
    <input type="hidden" id="landingPageImage2" value="<% Response.Write(landingPageImage2); %>" />
    <input type="hidden" id="landingPageImage3" value="<% Response.Write(landingPageImage3); %>" />
    <input type="hidden" id="landingPageImage4" value="<% Response.Write(landingPageImage4); %>" />
</asp:Content>
