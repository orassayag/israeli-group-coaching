<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="Graduates.aspx.cs" Inherits="Graduates" %>

<%@ MasterType VirtualPath="~/Master.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div id="intro" runat="server" class="">
        </div>
        <div class="space1"></div>
        <div class="insideTD">
            <asp:Repeater ID="graduatesRepeater" runat="server">
                <ItemTemplate>
                    <a href="javascript:load(<%#Eval("GraduateSessionID")%>)">
                        <%#Eval("GraduateYearHebrew") %>,
                        <%#Eval("GraduateYearNumber")%>
                    </a>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
