<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="TestingTimezone._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="icon" type="image/png" sizes="16x16" href="assets/favicon.png" />
    <title>Welcome to Timezone project</title>
    <link href="assets/css/style.css" rel="stylesheet" />
</head>
<body class="skin-default-dark fixed-layout">
    <form id="form1" runat="server">
        <br />
        <br />
        <br />
        <br />
        <br />
        <section id="wrapper" class="error-page">
            <div class="error-box">
                <div class="error-body text-center">
                    <h1>Login</h1>
                    <asp:DropDownList ID="ddlLogin" runat="server" CssClass="form-control form-control-sm" Width="300">
                        <asp:ListItem Text="--Select User--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Teacher" Value="Teacher.aspx"></asp:ListItem>
                        <asp:ListItem Text="Student" Value="StudentZone.aspx"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="rqdfddlLogin" ValidationGroup="A" runat="server" Text="Please select a User" Display="Dynamic" ForeColor="Red" ControlToValidate="ddlLogin" InitialValue="0"></asp:RequiredFieldValidator>

                    <br />
                    <asp:Button ID="BtnLogin" CssClass="btn btn-info btn-rounded waves-effect waves-light m-b-40" ValidationGroup="A" Text="Login" runat="server" OnClick="BtnLogin_Click"></asp:Button>
                    <asp:HiddenField ID="hdnOffset" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnclientdatetime" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdndeftz" runat="server"></asp:HiddenField>
                    <script type="text/javascript" defer="defer">                                                  

                        var curDate = new Date();
                        var tz = /\((.*)\)/.exec(curDate.toString());
                        document.getElementById("hdndeftz").value = tz[1];
                        document.getElementById("hdnOffset").value = curDate.getTimezoneOffset();
                        document.getElementById("hdnclientdatetime").value = curDate.toISOString();
                        console.log(curDate.toString() + "    " + curDate.getTimezoneOffset());
                    </script>   
            </div>
        </section>                                                                                  
    </form>
</body>
</html>
