Imports System.Globalization
Imports TestingTimezone.TestingTimezone

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub BtnLogin_Click(sender As Object, e As EventArgs)

        Dim localtimezone As String = Commonfunction.GetLocalTimeZone(hdnlocaltimezone.Value, hdnclientdatetime.Value)

        Session.Add("localTime", localtimezone)

        If ddlLogin.SelectedIndex > 0 Then
            Response.Redirect(ddlLogin.SelectedValue)
        End If
    End Sub

End Class