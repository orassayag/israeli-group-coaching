<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Contents.aspx.cs" Inherits="Admin_Contents" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="stabs1" runat="server">
        <div class="contents">
            <table class="contentsTable" border="1">
                <tr>
                    <td>
                        <div id="contentSelectorDiv" class="unVisi" runat="server" visible="false">
                            <asp:DropDownList ID="contentSelector" CssClass="textEn" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="contentSelector_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="addContent" class="unVisi" runat="server" visible="false">
                            <table class="textHeb">
                                                            <tr>
                                    <td>
                                        עדכון אחרון:
                                        <asp:Label ID="contentUpdateTime" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        קישור לעמוד:
                                        <asp:TextBox ID="contentLink" runat="server" ReadOnly="true" Width="700px" CssClass="textEn"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        קישור לעמוד מלא:
                                        <asp:TextBox ID="contentFullPageLink" runat="server" ReadOnly="true" Width="700px" CssClass="textEn"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        שפה:
                                        <select id="contentLanguageSelector" runat="server" onchange="switchLanguage('content', this)">
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        סוג דף:
                                        <asp:DropDownList ID="contentPageTypeSelector" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        מיקום הדף בתפריט:
                                        <asp:TextBox ID="contentPlace" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        כותרת הכפתור לדף:
                                        <asp:TextBox ID="contentButtonTitle" runat="server" Width="700px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        כותרת הדף:
                                        <asp:TextBox ID="contentTitle" runat="server" Width="700px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        מילות מפתח:
                                        <asp:TextBox ID="contentKeyWords" runat="server" TextMode="MultiLine" Height="80px"
                                            Width="700px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        תיאור הדף ב-Google:
                                        <asp:TextBox ID="contentDescription" runat="server" TextMode="MultiLine" Height="80px"
                                            Width="700px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        תוכן העמוד:<FCKeditorV2:FCKeditor Height="400px" Width="700px" AutoDetectLanguage="true"
                                            EnableSourceXHTML="true" BasePath="/fckeditor/" ID="contentEditor" SkinPath="skins/office2003/"
                                            runat="server">
                                        </FCKeditorV2:FCKeditor>
                                        <br />
                                        <div class="tableEdit">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="contentEditorCancelButton" runat="server" OnClick="contentEditorCancelButton_Click"
                                                            Text="Cancel" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="contentEditorSaveButton" runat="server" OnClick="contentEditorSaveButton_Click"
                                                            Text="Save" Height="26px" Width="42px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="removeUpdateContent" class="unVisi" runat="server" visible="false">
                            <input id="contentHiddenRe" type="hidden" runat="server" value="" />
                            <input id="contentHiddenUp" type="hidden" runat="server" value="" />
                            <div class="cen">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="contentRemoveUpdateSelector" runat="server" CssClass="textHeb">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="contentRemoveButton" runat="server" Text="Delete" OnClick="contentRemoveButton_Click" />
                                <asp:Button ID="contentUpdateButton" runat="server" Text="Update" OnClick="contentUpdateButton_Click" />
                                <asp:Button ID="contentCancelButton" runat="server" Text="Cancel" OnClick="contentCancelButton_Click" />
                            </div>
                        </div>
                        <div id="addLinkContent" runat="server" class="unVisi" visible="false">
                            <input id="contentLinkHiddenRe" type="hidden" runat="server" value="" />
                            <input id="contentLinkHiddenUp" type="hidden" runat="server" value="" />
                            <table class="textHeb">
                                <tr>
                                    <td>
                                        עדכון אחרון:
                                        <asp:Label ID="contentLinkUpdateTime" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        מיקום הדף בתפריט:
                                        <asp:TextBox ID="contentLinkPlace" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        כותרת הכפתור לדף:
                                        <asp:TextBox ID="contentLinkButtonTitle" runat="server" Width="700px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        כתובת העמוד:
                                        <asp:TextBox ID="contentLinkUrl" CssClass="textEn" runat="server" Width="700px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="tableEdit">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="contentLinkAddButton" runat="server" OnClick="contentLinkAddButton_Click"
                                                Text="Add" OnClientClick="return validateFieldLink()" />
                                        </td>
                                        <td>
                                            <asp:Button ID="contentLinkCancelButton" runat="server" OnClick="contentLinkCancelButton_Click"
                                                Text="Cancel" Height="26px" Width="42px" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="contentLinkPageLabel" runat="server" CssClass="red"></asp:Label>
                            </div>
                        </div>
                        <div id="contentsNotify" runat="server" class="unVisi" visible="false">
                            <div class="cen">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="contentsNotifyLabel" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="okBut" Text="Ok" runat="server" OnClick="okBut_Click" />
                                <asp:Button ID="cancelBut" Text="Cancel" Visible="false" runat="server" OnClick="cancelBut_Click" />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
