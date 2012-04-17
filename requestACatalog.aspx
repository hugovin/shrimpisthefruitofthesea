<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="requestACatalog.aspx.cs"
    Inherits="requestACatalog" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" runat="Server">

    <script language="javascript" type="text/javascript" src="js/catrequest.js"></script>

    
        <div id="cont">
            <div id="print">
            <!-- Queda comentado por si luego se tiene que volver a usar-->
            <!-- <% Response.Write(formHeader); %>
            <div class="mainAccount" style="margin-top:5px;">
                <h1>
                    Request a Catalog</h1><br />
                <div id="boxRequest">
                    <div>
                        <p>
                            <input type="checkbox" checked />
                            Yes, I would like an Educational Resources Catalog.</p>
                        <p>
                            <input type="checkbox" checked />
                            Yes, I would like to receive email promotions periodically from Educational Resources.</p>
                        <div class="posRequir reqInf">
                            <p>
                                * Required Information</p>
                        </div>
                        <div class="errorRequir alert" id="errorGlobal" name="errorGlobal" style="display: none;">
                            <p>
                                <strong>ERROR:</strong> CORRECT ITEMS IN RED</p>
                        </div>
                    </div>
                    <div id="boxFormRequest">
                        <div class="formCell">
                            <div class="formLabel">
                                <p>
                                    Salutation:</p>
                            </div>
                            <div class="formInput">
                                <select id="Select1" tabindex="-1" id="_ctl0__ctl1__ctl0__Salutation" name="_ctl0:_ctl1:_ctl0:_Salutation">
                                    <option selected="selected" value=""></option>
                                    <option value="Dr">Dr</option>
                                    <option value="Miss">Miss</option>
                                    <option value="Mr">Mr</option>
                                    <option value="Mrs">Mrs</option>
                                    <option value="Ms">Ms</option>
                                </select></div>
                            <div class="formAlert">
                            </div>
                        </div>
                        <div class="formCell">
                            <div class="formLabel" id="fieldName1">
                                <p>
                                    *Full Name:</p>
                            </div>
                            <div class="formInput">
                                <%  Response.Write("<input type=\"text\" name=\"_ctl0:_ctl1:_ctl0:_FirstName\" id=\"_ctl0__ctl1__ctl0__FirstName\" value=\"" + _user_fullname + "\"/>");%>
                               </div>
                            <div class="formAlert alert" id="alert1" style="display: none;">
                                <p>
                                    &lt; required</p>
                            </div>
                        </div>
                        <div class="formCell">
                            <div class="formLabel" id="fieldName2">
                                <p>
                                    *Job Title:</p>
                            </div>
                            <div class="formInput">
                                <select tabindex="-1" id="_ctl0__ctl1__ctl0_ddlTitle" name="_ctl0:_ctl1:_ctl0:ddlTitle">
                                    <option value="" selected="selected">Select your title</option>
                                    <option value="Computer/Tech Coordinator">Computer/Tech Coordinator</option>
                                    <option value="Teacher">Teacher</option>
                                    <option value="Administrator">Administrator</option>
                                    <option value="Librarian/Media Specialist">Librarian/Media Specialist</option>
                                    <option value="Curriculum Specialist">Curriculum Specialist</option>
                                    <option value="Title 1 Coordinator">Title 1 Coordinator</option>
                                    <option value="Parent/Home Purchaser">Parent/Home Purchaser</option>
                                    <option value="Home School Teacher">Home School Teacher</option>
                                    <option value="Assessment Coordinator">Assessment Coordinator</option>
                                    <option value="Youth/Ed Director">Youth/Ed Director</option>
                                    <option value="Other">Other</option>
                                </select></div>
                            <div class="formAlert alert" id="alert2" style="display: none;">
                                <p>
                                    &lt; required</p>
                            </div>
                        </div>
                        <div class="formCell">
                            <div class="formLabel" id="fieldName3">
                                <p>
                                    *School / District:</p>
                            </div>
                            <div class="formInput">
                                <%   Response.Write("<input type=\"text\" id=\"_ctl0__ctl1__ctl0__CompanyName\" name=\"_ctl0:_ctl1:_ctl0:_CompanyName\" value=\""+_user_schoolname+"\" />");%>
                            </div>
                            <div class="formAlert alert" id="alert3" style="display: none;">
                                <p>
                                    &lt; required</p>
                            </div>
                        </div>
                        <div class="formCell">
                            <div class="formLabel" id="fieldName4">
                                <p>
                                    *Considering Purchase For:</p>
                            </div>
                            <div class="formInput">
                                <select id="_ctl0__ctl1__ctl0_ddlPurchaseFor" name="_ctl0:_ctl1:_ctl0:ddlPurchaseFor"
                                    tabindex="-1">
                                    <option value="" selected="selected">Select...</option>
                                    <option value="District">District</option>
                                    <option value="Classroom">Classroom</option>
                                    <option value="Home">Home</option>
                                    <option value="Lab">Lab</option>
                                    <option value="School">School</option>
                                    <option value="Other">Other</option>
                                </select></div>
                            <div class="formAlert alert" id="alert4" style="display: none;">
                                <p>
                                    &lt; required</p>
                            </div>
                        </div>
                        <div class="titleSend">
                            <p>
                                Send Items To:</p>
                        </div>
                        <div class="formCell">
                            <div class="formLabel">
                                <p>
                                    Please Indicate:</p>
                            </div>
                            <div class="formInput">
                                <select id="" tabindex="-1" id="_ctl0__ctl1__ctl0__AddressType" name="_ctl0:_ctl1:_ctl0:_AddressType">
                                    <option value="Business">Business</option>
                                    <option value="Home">Home</option>
                                    <option value="School">School</option>
                                </select></div>
                            <div class="formAlert">
                            </div>
                        </div>
                        <div class="formCell">
                            <div class="formLabel" id="fieldName5">
                                <p>
                                    *Street Address:</p>
                            </div>
                            <div class="formInput">
                                <%  Response.Write("<input type=\"text\" id=\"_ctl0__ctl1__ctl0__Address1\" name=\"_ctl0:_ctl1:_ctl0:_Address1\" value=\""+_user_add1+"\"/>"); %>
                            </div>
                            <div class="formAlert alert" id="alert5" style="display: none;">
                                <p>
                                    &lt; required</p>
                            </div>
                        </div>
                        <div class="formCell">
                            <div class="formLabel" id="fieldName6">
                                <p>
                                    *City:</p>
                            </div>
                            <div class="formInput">
                                <% Response.Write("<input type=\"text\" size=\"35\" id=\"_ctl0__ctl1__ctl0__City\" name=\"_ctl0:_ctl1:_ctl0:_City\" value=\""+_user_city+"\" />"); %>
                            </div>
                            <div class="formAlert alert" id="alert6" style="display: none;">
                                <p>
                                    &lt; required</p>
                            </div>
                        </div>
                        <div class="formCell">
                            <div class="formLabel" id="fieldName7">
                                <p>
                                    *State:</p>
                            </div>
                            <div class="formInput">
                                <% Response.Write("<input type=\"text\" id=\"_ctl0__ctl1__ctl0__State\" name=\"_ctl0:_ctl1:_ctl0:_State\" value=\""+_user_state+"\" />"); %>
                            </div>
                            <div class="formAlert alert" id="alert7" style="display: none;">
                                <p>
                                    &lt; required</p>
                            </div>
                        </div>
                        <div class="formCell">
                            <div class="formLabel" id="fieldName8">
                                <p>
                                    *Zip:</p>
                            </div>
                            <div class="formInput">
                                <% Response.Write("<input type=\"text\" id=\"_ctl0__ctl1__ctl0__Zip\" name=\"_ctl0:_ctl1:_ctl0:_Zip\" value=\""+_user_zip+"\"/>"); %>
                                </div>
                            <div class="formAlert alert" id="alert8" style="display: none;">
                                <p>
                                    &lt; required</p>
                            </div>
                        </div>
                        <div class="formCell">
                            <div class="formLabel">
                                <p>
                                    Country:</p>
                            </div>
                            <div class="formInput">
                                <select id="" tabindex="-1" id="_ctl0__ctl1__ctl0__Country" name="_ctl0:_ctl1:_ctl0:_Country">
                                    <option value="United States" selected="selected">United States</option>
                                    <option value="Canada">Canada</option>
                                    <option value="Virgin Islands">Virgin Islands</option>
                                    <option value="U.S. Minor Outlying Islands">U.S. Minor Outlying Islands</option>
                                </select></div>
                            <div class="formAlert">
                            </div>
                        </div>
                        <div class="formCell">
                            <div class="formLabel">
                                <p>
                                    Telephone:</p>
                            </div>
                            <div class="formInput">
                                <% Response.Write("<input type=\"text\" id=\"_ctl0__ctl1__ctl0__Phone\" name=\"_ctl0:_ctl1:_ctl0:_Phone\" value=\""+_user_phone+"\" />"); %>
                            </div>
                            <div class="formAlert">
                            </div>
                        </div>
                        <div class="formCell">
                            <div class="formLabel">
                                <p>
                                    Fax:</p>
                            </div>
                            <div class="formInput">
                                <input type="text" id="_ctl0__ctl1__ctl0__Fax" name="_ctl0:_ctl1:_ctl0:_Fax" /></div>
                            <div class="formAlert">
                            </div>
                        </div>
                        <div class="formCell">
                            <div class="formLabel">
                                <p>
                                    Email:</p>
                            </div>
                            <div class="formInput">
                                <% Response.Write("<input type=\"text\" id=\"_ctl0__ctl1__ctl0__Email\" name=\"_ctl0:_ctl1:_ctl0:_Email\" value=\""+_user_email+"\" />"); %>
                            </div>
                            <div class="formAlert">
                            </div>
                        </div>
                    </div>
                    <div class="formBotton">
                        <input type="image" src="<% = Global.globalSiteImagesPath %>/buttonRequest.jpg" alt="" />
                    </div>
                </div>
            </div>
            </form> -->
            <asp:Literal ID="catalogIframe" runat="server" Text=""></asp:Literal>
            </div>
        </div>
</asp:Content>
