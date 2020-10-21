Public Class StudentTest
    Inherits System.Web.UI.Page

    Public htmlmessage As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Convert.ToString(Session("localTime"))) Then
            Response.Redirect("Default.aspx")
        Else
            If Not String.IsNullOrEmpty(Convert.ToString(Session("action"))) Then
                If Convert.ToString(Session("action")).Equals("s") Then
                    htmlmessage = HttpUtility.HtmlDecode("<div class='alert alert-success alert-rounded'><i class='ti-user'></i>Test Detail: You can start test</div>")
                Else
                    htmlmessage = HttpUtility.HtmlDecode("<div class='alert alert-warning alert-rounded'><i class='ti-user'></i>Test yet to start , Kindly check Start Date&Time.</div>")
                End If
            End If
        End If
    End Sub
End Class