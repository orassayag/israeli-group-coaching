<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Articles.aspx.cs" Inherits="Admin_Articles" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="stabs2" runat="server">
        <div class="articles">
            <table class="articlesTable" border="1">
                <tr>
                    <td>
                        <div id="articleSelectorDiv" class="unVisi" runat="server" visible="false">
                            <asp:DropDownList ID="articleSelector" CssClass="textEn" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="articleSelector_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="addArticle" class="unVisi" runat="server" visible="false">
                            <table class="textHeb">
                                <tr>
                                    <td>
                                        עדכון אחרון:
                                        <asp:Label ID="articleUpdateTime" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        קישור לעמוד:
                                        <asp:TextBox ID="articleLink" runat="server" ReadOnly="true" Width="700px" CssClass="textEn"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        קישור לעמוד מלא:
                                        <asp:TextBox ID="articleFullPageLink" runat="server" ReadOnly="true" Width="700px"
                                            CssClass="textEn"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        שפה:
                                        <select id="articleLanguageSelector" runat="server" onchange="switchLanguage('article', this)">
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        כותרת הדף:
                                        <asp:TextBox ID="articleTitle" runat="server" Width="700px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        מילות מפתח:
                                        <asp:TextBox ID="articleKeyWords" runat="server" TextMode="MultiLine" Height="80px"
                                            Width="700px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        תיאור הדף ב-Google:
                                        <asp:TextBox ID="articleDescription" runat="server" TextMode="MultiLine" Height="80px"
                                            Width="700px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        תוכן העמוד:<FCKeditorV2:FCKeditor Height="400px" Width="700px" AutoDetectLanguage="true"
                                            EnableSourceXHTML="true" BasePath="/fckeditor/" ID="articleEditor" SkinPath="skins/office2003/"
                                            runat="server">
                                        </FCKeditorV2:FCKeditor>
                                        <br />
                                        <div class="tableEdit">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="articleEditorCancelButton" runat="server" OnClick="articleEditorCancelButton_Click"
                                                            Text="Cancel" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="articleEditorSaveButton" runat="server" OnClick="articleEditorSaveButton_Click"
                                                            Text="Save" Height="26px" Width="42px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="removeUpdateArticle" class="unVisi" runat="server" visible="false">
                            <input id="articleHiddenRe" type="hidden" runat="server" value="" />
                            <input id="articleHiddenUp" type="hidden" runat="server" value="" />
                            <div class="cen">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="articleRemoveUpdateSelector" runat="server" CssClass="textHeb">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="articleRemoveButton" runat="server" Text="Delete" OnClick="articleRemoveButton_Click" />
                                <asp:Button ID="articleUpdateButton" runat="server" Text="Update" OnClick="articleUpdateButton_Click" />
                                <asp:Button ID="articleCancelButton" runat="server" Text="Cancel" OnClick="articleCancelButton_Click" />
                            </div>
                        </div>
                        <div id="articlesNotify" runat="server" class="unVisi" visible="false">
                            <div class="cen">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="articlesNotifyLabel" runat="server" Text=""></asp:Label>
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
