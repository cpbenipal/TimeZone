<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="StudentZone.aspx.vb" Inherits="TestingTimezone.StudentZone" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- column -->
    <div class="col-12">
        <div class="card">
            <div class="card-body">

                <div class="form-group">

                    <div class="row">
                        <div class="col-xs-12 col-sm-2">
                            <h4 class="card-title">Upcoming Tests</h4>
                        </div>
                        <%--<div class="col-xs-8 col-sm-2">
                            <asp:DropDownList ID="DdlTz" runat="server" OnSelectedIndexChanged="DdlTz_SelectedIndexChanged" Visible="false" AutoPostBack="true" CssClass="form-control form-control-sm" Width="300">
                            </asp:DropDownList>
                        </div>--%>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="GrdTests" runat="server" OnPageIndexChanging="GrdTests_PageIndexChanging" OnRowCommand="GrdTests_RowCommand" PagerSettings-Mode="Numeric" CssClass="table" AllowPaging="true"
                        PageSize="10" EmptyDataText="No test scheduled yet" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="GrdTests_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="SR No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Topic">
                                <ItemTemplate>
                                    <asp:Literal ID="ltTopic" runat="server" Text='<%# Eval("Topic")%>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Time">
                                <ItemTemplate>
                                    <asp:Literal ID="ltStart" runat="server" Text='<%# Eval("StartDateTime")%>'></asp:Literal>                                      
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Time">
                                <ItemTemplate>
                                    <asp:Literal ID="ltEnd" runat="server" Text='<%# Eval("EndDateTime")%>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duration (HH:MM)">
                                <ItemTemplate>
                                    <asp:Literal ID="ltDuration" runat="server" Text='<%# Eval("Duration")%>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button ID="BtnStart" runat="server" CommandArgument='<%#  Eval("TopicId")%>' Text="Start Test"></asp:Button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                                
            </div>
        </div>
    </div>   
     
</asp:Content>
