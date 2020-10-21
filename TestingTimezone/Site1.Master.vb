Imports TestingTimezone.TestingTimezone

Public Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ltAppDefTime.Text = Commonfunction.CurrentDateTimeZone(Application("DefTimeZone").ToString)

        ' ltLocalTimeZone.Text = Convert.ToString(Session("localTime"))
    End Sub

    Protected Sub LnkLogout_Click(sender As Object, e As EventArgs)
        Session.Abandon()
        Session.RemoveAll()
        Session.Clear()
        Response.Redirect("Default.aspx")
    End Sub
End Class