<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="MailLists.aspx.cs" Inherits="Admin_MailLists" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="stabs6" runat="server">
        <div class="mailList">
            <table class="mailListTable" border="1">
                <tr>
                    <td>
                        <asp:DropDownList ID="mailListSelector" CssClass="textEn" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="mailListSelector_SelectedIndexChanged">
                        </asp:DropDownList>
                        <input id="mailHiddenSelection" type="hidden" runat="server" value="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="mailData" runat="server" class="unVisi" visible="false">
                            <div class="mailTable">
                                <table>
                                    <tr>
                                        <td>
                                            <div class="list">
                                                <asp:Repeater ID="mailGrid" runat="server">
                                                    <HeaderTemplate>
                                                        <table border="1" cellspacing="1" cellpadding="4">
                                                            <tr>
                                                                <th>
                                                                    ID
                                                                </th>
                                                                <th>
                                                                    Name
                                                                </th>
                                                                <th>
                                                                    Mail
                                                                </th>
                                                                <th>
                                                                    Status
                                                                </th>
                                                                <th>
                                                                    Join Date
                                                                </th>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%#Eval("MailListID") %>
                                                            </td>
                                                            <td>
                                                                <%#Eval("MailListName")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("MailListMail")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("spActvie")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("spMailListJoinTime")%>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="mailEnabletBut" Text="Enable" runat="server" OnClick="mailEnableBut_Click"
                                                                    CommandName='<%# DataBinder.Eval(Container.DataItem,"MailListID") %>'></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="mailDisableBut" Text="Disable" runat="server" OnClick="mailDisableBut_Click"
                                                                    CommandName='<%# DataBinder.Eval(Container.DataItem,"MailListID") %>'></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="sendMail" runat="server" class="unVisi" visible="false">
                            <table class="textHeb">
                                <tr>
                                    <td>
                                        נמענים:
                                        <table class="tableEdit">
                                            <tr>
                                                <td>
                                                    <input type="button" id="DeselectAllButoon" value="Deselect All" onclick="selectNone()" />
                                                </td>
                                                <td>
                                                    <input type="button" id="selectAllButoon" value="Select All" onclick="selectAll()" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="mailListSenderLabel" runat="server" CssClass="red"></asp:Label>
                                        <div class="checklist">
                                            <asp:CheckBoxList ID="mailsSelector" runat="server" TextAlign="Right" RepeatDirection="Horizontal"
                                                CellPadding="18" RepeatColumns="6">
                                            </asp:CheckBoxList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        שפה:
                                        <select id="mailLanguageSelector" runat="server" onchange="switchLanguage('mail', this)">
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        כותרת המייל:
                                        <asp:TextBox ID="mailTitle" runat="server" Width="700px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        תוכן המייל:<FCKeditorV2:FCKeditor Height="400px" Width="700px" AutoDetectLanguage="true"
                                            EnableSourceXHTML="true" BasePath="/fckeditor/" ID="sendMailEditor" SkinPath="skins/office2003/"
                                            runat="server">
                                        </FCKeditorV2:FCKeditor>
                                        <br />
                                        <div class="tableEdit">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="sendMailCancelEditorButton" runat="server" OnClick="sendMailCancelEditorButton_Click"
                                                            Text="Cancel" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="sendMailSendEditorButton" runat="server" OnClick="sendMailSendEditorButton_Click"
                                                            OnClientClick="return checkOne()" Text="Send" Height="26px" Width="42px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="mailActions" runat="server" class="unVisi" visible="false">
                            <input id="mailActionHiddenRe1" type="hidden" runat="server" value="" />
                            <input id="mailActionHiddenRe2" type="hidden" runat="server" value="" />
                            <input id="mailActionHiddenUp1" type="hidden" runat="server" value="" />
                            <input id="mailActionHiddenUp2" type="hidden" runat="server" value="" />
                            <asp:DropDownList ID="mailActionSelector" CssClass="mailSelect" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="mailActionSelector_SelectedIndexChanged">
                            </asp:DropDownList>
                            <div id="addMail" runat="server" class="visi" visible="true">
                                <div class="cen">
                                    <table>
                                        <tr>
                                            <td>
                                                Name:
                                                <asp:TextBox ID="mailName" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                Mail Address
                                                <asp:TextBox ID="mailAddress" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="mailAdditionalInfo" visible="false" runat="server">
                                            <td>
                                                Status:
                                                <asp:Label ID="mailStatus" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                Joined At:
                                                <asp:Label ID="mailJoinDate" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="cen">
                                        <asp:Label ID="mailAddLabel" runat="server" CssClass="red"></asp:Label>
                                        <br />
                                        <asp:Button ID="mailSaveActionButton" runat="server" Text="Save" OnClientClick="return validateAddMail()"
                                            OnClick="mailSaveActionButton_Click" />
                                        <asp:Button ID="mailCancelActionAddButton" runat="server" Text="Cancel" OnClick="mailCancelActionAddButton_Click" />
                                        <asp:Button ID="mailDisableActionButton" runat="server" Text="Disable" OnClick="mailDisableActionButton_Click"
                                            Visible="false" />
                                        <asp:Button ID="mailEnableActionButton" runat="server" Text="Enable" OnClick="mailEnableActionButton_Click"
                                            Visible="false" />
                                    </div>
                                </div>
                            </div>
                            <div id="removeUpdateMail" runat="server" class="unVisi" visible="false">
                                <table>
                                    <tr>
                                        <td>
                                            <input id="mailSearchList" type="checkbox" onclick="selectMethod(this)" />
                                            <span id="mailSearchListSpan">Search By Mail Address From List</span>
                                        </td>
                                        <td>
                                            <input id="searchManually" type="checkbox" onclick="selectMethod(this)" />
                                            <span id="searchManuallySpan">Search By From List</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="mailSearchByManually" runat="server" class="unVisiPro">
                                                <table>
                                                    <tr>
                                                        <td align="center">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        Mail Address
                                                                        <br />
                                                                        <asp:TextBox ID="mailSearchText" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:Button ID="mailSearchSearchButton" runat="server" Text="Search" OnClientClick="return validateMail()"
                                                                OnClick="mailSearchSearchButton_Click" />
                                                            <asp:Button ID="mailSearchCancelButton" runat="server" Text="Cancel" OnClick="mailSearchCancelButton_Click" />
                                                            <br />
                                                            <asp:Label ID="mailError" runat="server" CssClass="red"></asp:Label>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="mailSearchByList" runat="server" class="unVisiPro">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ListBox ID="mailListMailSelector" runat="server" Height=" 300px"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Label ID="mailSelectLabel" runat="server" CssClass="red"></asp:Label>
                                                <br />
                                                <asp:Button ID="mailSelectActionButton" runat="server" Text="Select" OnClick="mailSelectActionButton_Click"
                                                    OnClientClick="return validateSelectMail()" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div id="getMails" class="unVisi" runat="server" visible="false">
                            <div class="cen">
                                <input id="geMailsHidden" type="hidden" runat="server" value="" />
                                <table>
                                    <tr>
                                        <td align="center">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <input id="getAllMailsCheck" type="checkbox" onclick="mailSelectMethod(this)" />
                                                        <span id="getAllSpan">Get All</span>
                                                    </td>
                                                    <td>
                                                        <input id="getFromDateToDateMailsCheck" runat="server" type="checkbox" onclick="mailSelectMethod(this)" />
                                                        <span id="getFromDateToDateMailsSpan">Get All Joined From Date Till Date</span>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="getMailsLabel" runat="server" CssClass="red"></asp:Label>
                                            <div id="getFromDateToDateMailsDiv" runat="server" class="unVisiPro">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            From Join Date:
                                                            <asp:TextBox ID="fromDateBox" runat="server" TextMode="SingleLine" CssClass="textEn"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Till Join Date:
                                                            <asp:TextBox ID="toDateBox" runat="server" TextMode="SingleLine" CssClass="textEn"></asp:TextBox>
                                                        </td>
                                                        <td align="center" class="red">
                                                            * Note: Please Enter Date as dd/mm/yyyy
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="getMailsButton" runat="server" Text="Get Mail Address" OnClick="getMailsButton_Click"
                                                OnClientClick="return validateGetMail()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="textHeb">
                                                רשימת הכתובות:</div>
                                            <asp:TextBox ID="mailsResult" runat="server" ReadOnly="true" CssClass="textEn" Height="400px"
                                                Width="700px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="okGetMails" runat="server" Text="Ok" OnClick="okGetMails_Click" />
                                <input id="getMailsClear" type="button" value="Clear" onclick="clearC()" />
                            </div>
                        </div>
                        <div id="mailStatistics" class="unVisi" runat="server" visible="false">
                            <table>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="mailStatisticsSelector" runat="server" CssClass="textEn" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <div id="mailStatisticsGeneral" runat="server" class="unVisi" visible="false">
                            </div>
                            <div id="mailStatisticsFromDateToDate" runat="server" class="unVisi" visible="false">
                            </div>
                        </div>
                        <div id="mailListsNotify" runat="server" class="unVisi" visible="false">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="mailListsNotifyLabel" runat="server" Text=""></asp:Label>
                                        <span id="test" runat="server"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="okBut" Text="Ok" runat="server" OnClick="okBut_Click" />
                                        <asp:Button ID="cancelBut" Text="Cancel" Visible="false" runat="server" OnClick="cancelBut_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
