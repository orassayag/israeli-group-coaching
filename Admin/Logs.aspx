<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="Logs.aspx.cs" Inherits="Admin_Logs" %>

<%@ MasterType VirtualPath="~/Admin/AdminMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tab-content2" id="stabs5" runat="server">
        <div class="logs">
            <input id="logsHidden" type="hidden" runat="server" value="" />
            <table class="logsTable" border="1">
                <tr>
                    <td>
                        <div id="getLogs" runat="server" class="unVisi" visible="false">
                            <div class="cenLo">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="logsDateSelector" runat="server" CssClass="textEn" AutoPostBack="true"
                                                OnSelectedIndexChanged="logsDateSelector_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="logsBody" CssClass="textEn" Height="400px" Width="600px" runat="server"
                                                ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="okLogsButton" runat="server" Text="Ok" OnClick="okLogsButton_Click" />
                                <input type="button" id="clearLogButton" value="Clear" onclick="clearLog()" />
                                <asp:Button ID="deleteLogButton" runat="server" Text="Delete Log" OnClick="deleteLogButton_Click" />
                                <asp:Button ID="deleteAllLogsButton" runat="server" Text="Delete All Logs" OnClick="deleteAllLogsButton_Click" />
                            </div>
                        </div>
                        <div id="logsNotify" runat="server" class="unVisi" visible="false">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="logsNotifyLabel" runat="server" Text=""></asp:Label>
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
