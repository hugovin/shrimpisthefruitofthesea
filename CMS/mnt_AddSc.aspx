<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/CMS/CMS_mntMasterPage.master" AutoEventWireup="true" CodeFile="mnt_AddSc.aspx.cs" Inherits="CMS_mnt_AddSc"  %>
<%@ Reference Control="~/CMS/mnt_SiteMap.ascx" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- TinyMCE -->

    <script type="text/javascript" src="tinymce/jscripts/tiny_mce/tiny_mce.js"></script>

    <% Response.Write(str_TinyMCE); %>
    <!-- /TinyMCE -->

    <script src="js/Links2.js" type="text/javascript"></script>

    <div id="master_main" class="master_main">
        <div class="newOpciones master_menu">
            <div id="opciones" runat="server">
                <a href="mnt_AddSc.aspx?pages=true">
                    <div class="newItem">
                        <img src="images/point.png" border="0" />&nbsp;&nbsp;Adds</div>
                </a><a href="mnt_FeaturedSpace.aspx">
                    <div class="newItem">
                        <img src="images/point.png" border="0" />&nbsp;&nbsp;Theater</div>
                </a><a href="mnt_HighLights.aspx">
                    <div class="newItem">
                        <img src="images/point.png" border="0" />&nbsp;&nbsp;Highlights</div>
                </a><a href="mnt_FeaturedBrands.aspx">
                    <div class="newItem">
                        <img src="images/point.png" border="0" />&nbsp;&nbsp;Featured Brands</div>
                </a>
            </div>
        </div>
        <div class="master_container">
            <div id="div_Adds" runat="server">
                <h3>
                    Adds</h3>
                <div id="contCajas5">
                    <div class="cdro_caja2">
                        <div id="div_displayadds" runat="server">
                        </div>
                        
                    </div>
                    <div class="clear">
                    </div>
                    <span>Limit: Two Adds</span>
                    <asp:Button ID="btn_NewAdd" CssClass="class_addNew" runat="server" OnClick="btn_NewAdd_Click" />
                </div>
                
            </div>
            <div class="clear">
            </div>
            <div id="div_editNewadd" runat="server" visible="false">
            </div>
            <div id="div_adduploadBTn" runat="server" visible="false">
            </div>
            <div id="htmlElement3" style="visibility:hidden;">
                <div>
                    <div id="div_addButtons" runat="server">
                        <div class="cdro_caja0">
                            <div class="cdro_contenido">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            Image:&nbsp;
                                        </td>
                                        <td>
                                            <input type="radio" id="rb_select_image_upload" name="rb_image_type" value="upload"
                                                checked="checked" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />Upload
                                            image
                                            <input type="radio" id="rb_select_image_url" name="rb_image_type" value="url" onclick="enable_reference_image(this.value, 'container_file_uploader_image', 'container_url_image');" />External
                                            Url
                                            <div id="container_file_uploader_image">
                                                <asp:FileUpload ID="UploadNew" CssClass="class_file" runat="server" />
                                                <asp:Button ID="btn_Uploadsnew" runat="server" Text="" CssClass="class_btnUpload"
                                                    OnClick="btn_Uploadsnew_Click" UseSubmitBehavior="False" />
                                            </div>
                                            <div id="container_url_image" style="display: none">
                                                <input type="text" id="txt_image_url" name="txt_image_url" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbAdd" runat="server" Text="" Visible="false"></asp:Label>
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
                                            Alt:&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <input type="text" id="txtGeneDlinkTitle" class="class_file" name="txtGeneDlinkTitle"
                                                value="<%Response.Write(GenedLink);%>" />
                                        </td>
                                        <td>
                                            <%if (lbAdd.Text != "")
                                      {%>
                                            <a href="../images/<%Response.Write(lbAdd.Text);%>" target="_blank" id="mb2" class="mb"
                                                title="Left Add"><span class="class_btnActualImage"></span></a>
                                            <%} %>
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
                                    <% if  (linktopage1 != ""){ %>
                                    <tr>
                                        <td>
                                            Current Link:
                                        </td>
                                        <td>
                                            <%Response.Write(linktopage1);%>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <% } %>
                                    <tr>
                                        <td>
                                            Link:
                                        </td>
                                        <td>
                                            <input id="txt_link" class="class_file" name="txt_link" type="text" value="<%Response.Write(linktopage1);%>" />
                                        </td>
                                        <td>
                                            <p id="p_link_format_warning">i.e.: http://www.mywebsite.com</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <input id="radio1" checked="checked" name="rb_option_new_page" type="radio" value="external" onclick="radioExternal_onclick(this)" />External&nbsp;&nbsp;
                                            <input id="radio2" name="rb_option_new_page" type="radio" value="internal" onclick="radioInternal_onclick(this)" />Internal&nbsp;&nbsp;
                                            <input id="radio4" name="rb_option_new_page" type="radio" value="new_landing"  onclick="radioNewLanding_onclick(this)" /><p id="p_new_landing" class="p_landing_page">New</p><p id="p_edit_landing" class="p_landing_page">Edit</p>
                                            <input id="radiotipo" name="radiotipo" value="<%Response.Write(radioType); %>" type="hidden" />
                                            Landing Page
                                        </td>
                                        <td>
                                            <input id="LinkID" name="LinkID" type="hidden" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="htmlElement" name="htmlElement" style="display: none;">
                                <div id="div_landingPage" class="add_new_landing spaceInternal" name="div_landingPage">
                                    <span> <!-- Used to add specific style for this section, formerly it was a modal, now it is included inside the main frame. -->
                                        <hr />
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                        <br />
                                        Title:<br />
                                        <input type="text" class="class_texto2" value="<%Response.Write(TitleGen);%>" name="txtlandTitle" /><br /><br />
                                        Content:<br />
                                        <textarea id="elmlandpage" name="elmlandpage" style="width:100%"><%Response.Write(ContGen); %></textarea><br /><br />
                                        <% if(Gen_Image != ""){ %>
                                        <p>Actual Image: <%Response.Write(Gen_Image); %></p>
                                        <% } %>
                                        <input type="radio" id="rb_upload_landing" name="rb_image_type_landing" value="upload"
                                            checked="checked" onclick="enable_reference_image(this.value, 'container_file_uploader_image_landing', 'container_url_image_landing');" />Upload
                                        image
                                        <input type="radio" id="rb_url_landing" name="rb_image_type_landing" value="url"
                                            onclick="enable_reference_image(this.value, 'container_file_uploader_image_landing', 'container_url_image_landing');" />External
                                        Url
                                        <div id="container_file_uploader_image_landing">
                                            <asp:FileUpload ID="UploadLanding" runat="server" CssClass="class_file" /></asp:Button>
                                        </div>
                                        <div id="container_url_image_landing" style="display: none">
                                            <input type="text" id="txt_image_url_landing" name="txt_image_url_landing" />
                                        </div>
                                        <asp:Label ID="lbimageinfo" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="LbgenID" runat="server" Text=""></asp:Label>
                                        <br />
                                        <!--<table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btn_CancelLand" runat="server" OnClick="btn_CancelLand_Click" CssClass="class_Cancel" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_SaveLand" runat="server" OnClick="btn_SaveLand_Click" CssClass="class_btnSave" />
                                                </td>
                                            </tr>
                                        </table>-->
                                    </span>
                                </div>
                            </div>
                            
                            <div id="div_internalLink" class="internal_link_adds spaceInternal" style="display:none;">
                                <hr />
                                <asp:PlaceHolder ID="PlaceHolderMinisitemap" runat="server"></asp:PlaceHolder>
                                <hr />
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btn_cancelAdd" runat="server" Text="" CssClass="class_Cancel" OnClick="btn_cancelAdd_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_Saveadd" runat="server" CssClass="class_btnUpdate" Text="" OnClick="btn_Saveadd_Click"
                                            UseSubmitBehavior="False" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%if (Details == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement2').style.visibility = 'visible';</script>"); } %>
        <%if (AddEdit == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement3').style.visibility = 'visible';is_landing_page(\""+linktopage1+"\");</script>"); } %>
        <%if (Information == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('htmlElement').style.visibility = 'visible';</script>"); } %>

        <input id="LinkIDmore" name="LinkIDmore" type="hidden" />
        <input id="txtLinkmore2" name="txtLinkmore2" type="hidden" />
    </div>
    <script type="text/javascript">
        $('#div_internalLink').children().children().children('img').css('display','none');
    </script>
</asp:Content>
