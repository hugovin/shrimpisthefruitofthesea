<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/CMS/CMS_mntMasterPage.master"
    CodeFile="mnt_Site.aspx.cs" Inherits="CMS_mnt_Site" %>

<%@ Reference Control="~/CMS/mnt_SiteMap.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <!-- TinyMCE -->

    <script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>

    <% Response.Write(str_TinyMCE); %>
    <!-- /TinyMCE -->

    <script src="js/Links.js" type="text/javascript"></script>

    <div class="wrapper">
        <div class="MainContainer2">
            <div>
                <div id="div_Information" runat="server">
                    <div id="div_SiteInfo" runat="server">
                    </div>
                    <asp:Button ID="btn_Update" runat="server" CssClass="class_btnUpdate" OnClientClick="return confirm('Do You want to modify The information to display in the main site');"
                        OnClick="btn_Update_Click" PostBackUrl="~/CMS/mnt_Site.aspx?Update=true" />
                    <div class="clear">
                    </div>
                    <!-- fin caja Template A -->
                    <hr />
                    <h2>
                        Site Contact Information</h2>
                    <div class="clear">
                    </div>
                    <div class="cdro_caja2">
                        <div id="div_ContactInfo" runat="server">
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                    <asp:Button ID="btn_New" runat="server" CssClass="class_addNew" OnClick="btn_New_Click"
                        Style="height: 26px" />
                    <div class="clear">
                    </div>
                    <!-- fin caja Template A -->
                    <div class="clear">
                    </div>
                </div>
                <div id="htmlElement2" name="htmlElement2" style="display: none;">
                    <hr />
                    <div style="padding: 5px;">
                        <h2>
                            Edit</h2>
                        <div id="div_ContactDetail" runat="server">
                            <div id="div_Detail" runat="server">
                                <input type="hidden" value="<%Response.Write(sitecontid);%>" name="sitecontid" id="sitecontid" />
                                <p>
                                    <label>
                                        Title:
                                    </label>
                                    <input type="text" class="class_texto2" value="<%Response.Write(SiteContTitle); %>"
                                        name="txtContTitle" id="txtContTitle" /><br />
                                </p>
                                <p>
                                    <label>
                                        Address:</label>
                                    <textarea id="elm1" name="elm1" rows="8" cols="15"><%Response.Write(SiteContAddress);%></textarea>
                                </p>
                                <p class="emailService">
                                    <label>
                                        Email Customer Service:
                                    </label>
                                    <input type="text" size="30" value="<%Response.Write(SiteContEmailCus); %>" name="txtEmailCus"
                                        id="Text1" />
                                </p>
                                <p class="emailService">
                                    <label>
                                        Email Sales Service:
                                    </label>
                                    <input type="text" value="<%Response.Write(SiteContEmailSal); %>" name="txtEmailSal"
                                        id="Text2" />
                                    <input type="hidden" value="<%Response.Write(SiteContOrdPos); %>" name="txtPosition"
                                        id="Hidden1" />
                                </p>
                            </div>
                        </div>
                        <div id="divbuttons" runat="server">
                            <div class="clear">
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        <input type="radio" id="rb_select_image_upload" name="rb_image_type" value="upload"
                                            checked="checked" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />Upload
                                        image
                                        <input type="radio" id="rb_select_image_url" name="rb_image_type" value="url" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />External
                                        Url
                                        <div id="container_file_uploader_image">
                                            <asp:FileUpload ID="Upload" CssClass="class_file" runat="server"></asp:FileUpload>
                                            <asp:Button ID="btn_Upload" runat="server" Text="" CssClass="class_btnUpload" OnClick="btn_Upload_Click"
                                                PostBackUrl="~/CMS/mnt_Site.aspx?New=true" UseSubmitBehavior="False" />
                                        </div>
                                        <div id="container_url_image" style="display: none">
                                            <input type="text" id="txt_image_url" name="txt_image_url" />
                                        </div>
                                    </td>
                                    <div id="div_actualimage" visible="false" runat="server">
                                        <td>
                                            <a href="../images/<%Response.Write(ActualImage);%>" target="_blank" id="mb2" class="mb"
                                                title="Left Add"><span class="class_btnActualImage"></span></a>
                                            <asp:Label ID="lbimage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </div>
                                </tr>
                            </table>
                            <div class="clear">
                            </div>
                            <div style="float: left">
                                <asp:Button ID="btn_CancelCont" runat="server" Text="" CssClass="class_Cancel" OnClick="btn_CancelCont_Click" /></div>
                            <div style="float: left">
                                <asp:Button ID="btn_UpdateCont" runat="server" Text="" CssClass="class_btnSave" OnClick="btn_UpdateCont_Click"
                                    PostBackUrl="~/CMS/mnt_Site.aspx?UpdateInfo=true" /></div>
                            <div class="clear">
                            </div>
                            <input id="oldimage" runat="server" value="" type="hidden" />
                            <input id="neimage" runat="server" value="" type="hidden" />
                        </div>
                    </div>
                    <hr />
                </div>
                <div class="clear">
                </div>
                <h2>
                    Additional Information</h2>
                <div id="div_AdditionalInfo" runat="server">
                </div>
                <br />
                <div id="htmlElement3" name="htmlElement3" style="background-color: #FFFFFF; border: solid 5px #999999;
                    display: none;">
                    <h1>
                        Title</h1>
                    <div style="padding: 5px;">
                        <div id="div_addButtons" runat="server" visible="false">
                            <div class="cdro_caja0">
                                <div class="cdro_contenido">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="27">
                                                Image:&nbsp;
                                            </td>
                                            <td width="302">
                                                <asp:FileUpload ID="UploadNew" CssClass="class_file" runat="server" />
                                            </td>
                                            <td width="121">
                                                <asp:Button ID="btn_Uploadsnew" runat="server" Text="" CssClass="class_btnUpload"
                                                    OnClick="btn_Uploadsnew_Click" />
                                                <asp:Label ID="lbAdd" runat="server" Text=""></asp:Label>
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
                                            <td width="27">
                                                Alt:&nbsp;&nbsp;
                                            </td>
                                            <td width="302">
                                                <input type="text" id="txtGeneDlinkTitle" class="class_file" name="txtGeneDlinkTitle"
                                                    value="<%Response.Write(GenedLink);%>" />
                                            </td>
                                            <td width="121">
                                                <asp:Label ID="lbGenx" runat="server" Visible="false" Text=""></asp:Label>
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
                                                <input id="txt_link" class="class_file" name="txt_link" type="text" value="<%Response.Write(linktopage1);%>" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td align="center">
                                                <label>
                                                    <input id="radio1" checked name="radio" type="radio" onclick="return radioExternal_onclick('txt_link')" />External&nbsp;&nbsp;
                                                    <input id="radio2" name="radio" type="radio" onclick="return radioInternal_onclick('div_internalLink')" />Internal&nbsp;&nbsp;
                                                    <input id="radio3" name="radio" type="radio" onclick="return radioNewLanding_onclick()" />New
                                                    <input id="radiotipo" name="radiotipo" value="<%Response.Write(radioType); %>" type="hidden" />
                                                    Landing Page
                                                </label>
                                            </td>
                                            <td>
                                                <input id="LinkID" name="LinkID" type="hidden" />
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btn_cancelAdd" runat="server" Text="" CssClass="class_Cancel" OnClick="btn_cancelAdd_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btn_Saveadd" runat="server" CssClass="class_btnUpdate" Text="" OnClick="btn_Saveadd_Click"
                                                PostBackUrl="~/CMS/mnt_Site.aspx?UpdateInfoAdd=true" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="htmlElement" name="htmlElement" style="display: none;">
                    <hr />
                    <h1>
                        Additional Information</h1>
                    <div style="padding: 5px;">
                        <div id="div_landingPage" name="div_landingPage">
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            <p>
                                <label>
                                    Title:
                                </label>
                                <input type="text" class="class_texto2" value="<%Response.Write(TitleGen);%>" name="txtlandTitle" /></p>
                            <p>
                                <label>
                                    Content:
                                </label>
                                <textarea id="elmlandpage" name="elmlandpage" cols="20" rows="25"><%Response.Write(ContGen);%></textarea></p>
                            <hr />
                            <input type="radio" id="rb_select_image_upload_privacy" name="rb_image_type_privacy"
                                value="upload" checked="checked" onclick="enable_reference_image(this.value, 'container_file_uploader_image_privacy', 'container_url_image_privacy');" />Upload
                            image
                            <input type="radio" id="rb_select_image_url_privacy" name="rb_image_type_privacy"
                                value="url" onclick="enable_reference_image(this.value, 'container_file_uploader_image_privacy', 'container_url_image_privacy');" />External
                            Url
                            <div id="container_file_uploader_image_privacy">
                                <asp:FileUpload ID="UploadLanding" runat="server" CssClass="class_file" />
                            </div>
                            <div id="container_url_image_privacy" style="display: none">
                                <input type="text" id="txt_image_url_privacy" name="txt_image_url_privacy" />
                            </div>
                            <asp:Label ID="lbimageinfo" runat="server" Text=""></asp:Label>
                            <asp:Label ID="LbgenID" runat="server" Text=""></asp:Label>
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
                    <hr />
                </div>
            </div>
        </div>
    </div>
    <div id="div_internalLink" class="internal_link_adds" style="display: none;">
        <asp:PlaceHolder ID="PlaceHolderMinisitemap" runat="server"></asp:PlaceHolder>
    </div>
    <%if (Details == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement2').style.display = 'block';</script>"); } %>
    <%if (AddEdit == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement3').style.display = 'block';</script>"); } %>
    <%if (Information == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement').style.display = 'block';</script>"); } %>
</asp:Content>
