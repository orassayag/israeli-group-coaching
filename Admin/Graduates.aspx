<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Graduates.aspx.cs" Inherits="Admin_Graduates" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="stabs3" runat="server">
        <div class="graduates">
            <table class="graduatesTable" border="1">
                <tr>
                    <td>
                        <asp:DropDownList ID="graduatesSelector" runat="server" CssClass="textEn" AutoPostBack="true"
                            OnSelectedIndexChanged="graduatesSelector_SelectedIndexChanged">
                        </asp:DropDownList>
                        <input id="graduatesHiddenSelection" type="hidden" runat="server" value="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="graduatesAddNewSession" runat="server" class="unVisi" visible="false">
                            <div class="cen">
                                <table>
                                    <tr class="textHeb">
                                        <td>
                                            מיקום:
                                            <asp:TextBox ID="graduatesPlaceSelector" CssClass="textHeb" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            שנת מחזור בעברית:
                                            <asp:TextBox ID="graduatesYearHebrew" CssClass="textHeb" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            שנת מחזור:
                                            <asp:DropDownList ID="graduatesYearNumberSelector" CssClass="textEn" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="addSessionLabel" runat="server" Text="" CssClass="red"></asp:Label>
                                <br />
                                <asp:Button ID="graduatesSaveSession" Text="Save" runat="server" OnClick="graduatesSaveSession_Click"
                                    OnClientClick="return graduateCheck('session')" />
                                <asp:Button ID="graduatesCancelSession" Text="Cancel" runat="server" OnClick="graduatesCancelSession_Click" />
                            </div>
                        </div>
                        <div id="graduatesUpdateRemoveSession" runat="server" class="unVisi" visible="false">
                            <input id="removeSessionHidden" type="hidden" runat="server" value="" />
                            <input id="sessionHiddenUp" type="hidden" runat="server" value="" />
                            <input id="sessionHiddenRe" type="hidden" runat="server" value="" />
                            <div class="cen">
                                <table>
                                    <tr>
                                        <td class="textHeb">
                                            בחר מחזור:
                                            <asp:DropDownList ID="graduatesRemoveUpdateSessionSelector" runat="server">
                                            </asp:DropDownList>
                                            <asp:Button ID="graduatesRemoveSessionButton" Text="Remove" runat="server" OnClick="graduatesRemoveSessionButton_Click" />
                                            <asp:Button ID="graduatesUpdateSessionButton" Text="Update" runat="server" OnClick="graduatesUpdateSessionButton_Click" />
                                            <asp:Button ID="graduatesRemoveUpdateCancelButton" Text="Cancel" runat="server" OnClick="graduatesRemoveUpdateCancelButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div id="graduatesAddNewGraduate" runat="server" class="unVisi" visible="false">
                            <div class="cen">
                                <table>
                                    <tr class="textHeb">
                                        <td>
                                            שם הבוגר:
                                            <asp:TextBox ID="addGraduateName" CssClass="textHeb" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            שייך למחזור:
                                            <asp:DropDownList ID="addGraduateSelector" CssClass="textHeb" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="addGraduateLabel" runat="server" Text="" CssClass="red"></asp:Label>
                                <br />
                                <asp:Button ID="graduatesSaveGraduate" Text="Save" runat="server" OnClick="graduatesSaveGraduate_Click"
                                    OnClientClick="return graduateCheck('graduate')" />
                                <asp:Button ID="graduatesCancelGraduate" Text="Cancel" runat="server" OnClick="graduatesCancelGraduate_Click" />
                            </div>
                        </div>
                        <div id="graduatesRemoveGraduate" runat="server" class="unVisi" visible="false">
                            <div class="cen">
                                <input id="graduateHiddenUp" type="hidden" runat="server" value="" />
                                <input id="graduateHiddenRe" type="hidden" runat="server" value="" />
                                <table>
                                    <tr>
                                        <td class="textHeb">
                                            <table>
                                                <tr>
                                                    <td>
                                                        בחר מחזור:
                                                        <asp:DropDownList ID="selectSessionGraduate" CssClass="textHeb" runat="server" AutoPostBack="true"
                                                            OnSelectedIndexChanged="selectSessionGraduate_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td id="selectGraduate" runat="server" visible="false" class="unVisi">
                                                        <div>
                                                            בחר בוגר:
                                                            <asp:DropDownList ID="selectGraduatesGraduate" CssClass="textHeb" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Button ID="graduatesRemoveGraduateButton" Text="Remove" runat="server" OnClick="graduatesRemoveGraduateButton_Click" />
                                            <asp:Button ID="graduatesUpdateGraduateButton" Text="Update" runat="server" OnClick="graduatesUpdateGraduateButton_Click" />
                                            <asp:Button ID="graduatesRemoveCancelButton" Text="Cancel" runat="server" OnClick="graduatesRemoveCancelButton_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div id="graduatesNotify" runat="server" class="unVisi" visible="false">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="graduatesNotifyLabel" runat="server" Text=""></asp:Label>
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
