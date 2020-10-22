Imports System.Globalization
Imports TestingTimezone.TestingTimezone

Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    Protected Sub BtnLogin_Click(sender As Object, e As EventArgs)

        Dim TimeZoneParams As TimeZoneParams = Commonfunction.GetLocalTimeZone(hdnOffset.Value, hdnclientdatetime.Value, hdndeftz.Value)
        ' IF Timezone not found set Application Default Time
        TimeZoneParams.MappedTimeZone = If(TimeZoneParams.MappedTimeZone = Nothing, Convert.ToString(Application("DefTimeZone")), TimeZoneParams.MappedTimeZone)

        Session.Add("localTime", TimeZoneParams.MappedTimeZone)
        Session.Add("UTCTimeZone", TimeZoneParams.UTCTimeZone)
        If ddlLogin.SelectedIndex > 0 Then
            Response.Redirect(ddlLogin.SelectedValue)
        End If
    End Sub

End Class