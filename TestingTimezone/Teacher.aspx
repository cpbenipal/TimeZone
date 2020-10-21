<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Teacher.aspx.vb" Inherits="TestingTimezone.Teacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Column -->
    <div class="col-lg-8 col-xlg-9 col-md-7">
        <div class="card">
            <!-- Tab panes -->
            <div class="card-body">
                <h4 class="card-title">Schedule Test</h4>
                <div class="form-group">
                    <label class="col-md-12">Topic</label>
                    <div class="col-md-12">
                        <asp:DropDownList ID="ddlTopics" runat="server" ValidationGroup="A" CssClass="form-control" required></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rqdfddltopic" ValidationGroup="A" runat="server" Text="Please select a topic" Display="Dynamic" ForeColor="Red" ControlToValidate="ddlTopics" InitialValue="0"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="example-email" class="col-md-12">Start Date&Time</label>
                    <div class="col-md-12">
                        <asp:TextBox ID="txtStartDateTime" ValidationGroup="A" CssClass="form-control" runat="server" TextMode="datetimelocal"></asp:TextBox>
                        <asp:CustomValidator runat="server" ID="cusStartDateTime" ControlToValidate="txtStartDateTime" OnServerValidate="CusStartDateTime_ServerValidate"
                            Text="Please enter a valid and future Start Date&Time. (dd-mm-yyyy hh:mm)" ValidationGroup="A" EnableClientScript="true" SetFocusOnError="false" ForeColor="Red" Display="Dynamic" ValidateEmptyText="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-12">Duration (HH:MM)</label>
                    <div class="row">
                        <div class="col-xs-12 col-sm-2">
                            <asp:DropDownList ID="ddlHours" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-xs-8 col-sm-2">
                            <asp:DropDownList ID="ddlMinutes" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>

                    </div>
                    <div class="col-md-12">
                        <asp:CustomValidator runat="server" ID="CusValDuration" ControlToValidate="ddlMinutes" OnServerValidate="CusValDuration_ServerValidate"
                            Text="Duration must be greater than 00:00 (HH:MM)" ValidationGroup="A" ForeColor="Red" EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ValidateEmptyText="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-12">TimeZone</label>
                    <div class="col-sm-12">
                        <asp:DropDownList ID="ddlTimeZone" ValidationGroup="A" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rqfvddlTimeZone" runat="server" ValidationGroup="A" Text="Please select a timezone" ToolTip="Required" ForeColor="Red" ControlToValidate="ddlTimeZone" InitialValue="0"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <asp:Button ID="BtnCreateTest" runat="server" Text="Create Test" ValidationGroup="A" CssClass="btn btn-success" OnClick="BtnCreateTest_Click" />

                    </div>
                </div>

            </div>
            <div class="card-body" runat="server" id="dvmessage" style="font-size: medium">
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnlocaltimezone" runat="server"></asp:HiddenField>

    <!-- Column -->
    <script type="text/javascript">       
        var s = document.getElementById('<%=ddlTimeZone.ClientID %>');
        var curDate = new Date();
        var tz = /\((.*)\)/.exec(curDate.toString())[1]; 
        for (var i = 0; i < s.options.length; i++) {
            if (s.options[i].value == tz) {
                s.options[i].selected = true;
            }
        }
    </script>
</asp:Content>
