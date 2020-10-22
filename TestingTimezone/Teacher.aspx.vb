Imports System.Globalization
Imports TestingTimezone.TestingTimezone

Public Class Teacher
    Inherits System.Web.UI.Page
    Private ReadOnly ConnectionString As String = ConfigurationManager.ConnectionStrings("connection").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Convert.ToString(Session("localTime"))) Then
            Response.Redirect("Default.aspx")
        Else
            If Not IsPostBack Then
                hdnlocaltimezone.Value = Convert.ToString(Session("UTCTimeZone"))
                GetAllTimeZones()
                GetAllTopics()
                PopulateHours()
                PopulateMinutes()
            End If
        End If
    End Sub
    Private Sub GetAllTopics()
        BindDropDownList(ddlTopics, "[dbo].[spGetAllTopics]", "Topic", "TopicId", "-- Choose Topic--")
    End Sub

    Private Sub BindDropDownList(ByVal ddlcontrol As DropDownList, ByVal spname As String, ByVal DataTextField As String, ByVal DataValueField As String, ByVal Defaulttext As String)
        AdoHelper.ConnectionString = ConnectionString
        ddlcontrol.DataSource = New AdoHelper().ExecDataSetProc(spname)
        ddlcontrol.DataTextField = DataTextField
        ddlcontrol.DataValueField = DataValueField
        ddlcontrol.DataBind()
        ddlcontrol.Items.Insert(0, New ListItem(Defaulttext, "0"))
    End Sub

    Private Sub PopulateHours()
        For i As Integer = 0 To 23
            ddlHours.Items.Add(i.ToString("D2"))
        Next
    End Sub

    Private Sub PopulateMinutes()
        For i As Integer = 0 To 59
            ddlMinutes.Items.Add(i.ToString("D2"))
        Next
    End Sub

    Private Sub GetAllTimeZones()
        ddlTimeZone.DataSource = TimeZoneInfo.GetSystemTimeZones()
        ddlTimeZone.DataTextField = "DisplayName"
        ddlTimeZone.DataValueField = "Id"
        ddlTimeZone.DataBind()
        ddlTimeZone.Items.Insert(0, New ListItem("-- Choose TimeZone--", "0"))
        ddlTimeZone.SelectedIndex = -1
        ' Timezone not found
        ddlTimeZone.Items.FindByValue(Convert.ToString(Application("DefTimeZone"))).Selected = True
    End Sub

    Protected Sub BtnCreateTest_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Page.IsValid Then

                Dim StartDateTime As DateTime = Commonfunction.ConvertToUTC(txtStartDateTime.Text, ddlTimeZone.SelectedValue, Application("DefTimeZone").ToString)

                Dim objArray As Object() = New Object() {"TopicId", ddlTopics.SelectedValue, "StartDateTime", StartDateTime, "Duration", ddlHours.SelectedValue & ":" + ddlMinutes.SelectedValue, "TeacherTZ", ddlTimeZone.SelectedValue}
                AdoHelper.ConnectionString = ConnectionString
                Dim ret As String = Convert.ToString(New AdoHelper().ExecScalarProc("[dbo].[spCreateTest]", objArray))

                If ret = "1" Then
                    ' Response.Write(Date.Parse(txtStartDateTime.Text))
                    ' Response.Write(Convert.ToDateTime(txtStartDateTime.Text).ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture))
                    ' Response.Write(Commonfunction.ConvertToUTC(txtStartDateTime.Text))

                    dvmessage.InnerHtml = "Test Scheduled Successfully"
                    dvmessage.Attributes.Add("class", "alert alert-success")
                ElseIf ret = "0" Then
                    dvmessage.InnerHtml = "Test already Scheduled for this time"
                    dvmessage.Attributes.Add("class", "alert alert-warning")
                Else
                    dvmessage.InnerHtml = "DB transaction error. Contact Support..."
                    dvmessage.Attributes.Add("class", "alert alert-danger")
                End If

            End If
        Catch ex As Exception
            dvmessage.InnerHtml = "System Exception! Contact Support..."
            dvmessage.Attributes.Add("class", "alert alert-danger")
        End Try
    End Sub

    Protected Sub CusStartDateTime_ServerValidate(source As Object, args As ServerValidateEventArgs)
        args.IsValid = ValidateStartDate(args.Value)
    End Sub

    Private Function ValidateStartDate(value As String) As Boolean
        Dim IsValidStartDateTime As Boolean
        Try
            Dim StartDateTime As Date = Commonfunction.ConvertToUTC(value, ddlTimeZone.SelectedValue, Application("DefTimeZone").ToString)

            If Date.Compare(StartDateTime, DateTime.UtcNow) < 0 Then
                IsValidStartDateTime = False
            Else
                IsValidStartDateTime = True
            End If
        Catch ex As Exception
            IsValidStartDateTime = False
        End Try
        Return IsValidStartDateTime
    End Function

    Protected Sub CusValDuration_ServerValidate(source As Object, args As ServerValidateEventArgs)
        Dim Duration As String = ddlHours.SelectedValue & ddlMinutes.SelectedValue
        If Duration = "0000" Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub
End Class