<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="StudentTest.aspx.vb" Inherits="TestingTimezone.StudentTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- column -->
    <div class="col-lg-8 col-xlg-9 col-md-7">
        <div class="card">
            <!-- Tab panes -->
            <div class="card-body">
                <h4 class="card-title">Test Information</h4>
                <div class="col-12" id="dvmessage" runat="server">
                   <%-- <div class="alert alert-warning alert-rounded"><i class="ti-user"></i>This is an example top alert. You can edit what u wish.</div>--%>
                    <%=htmlmessage %>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
