Imports System.Globalization
Imports TestingTimezone.TestingTimezone

Public Class StudentZone
    Inherits System.Web.UI.Page
    Private ReadOnly ConnectionString As String = ConfigurationManager.ConnectionStrings("connection").ConnectionString
    Private localtimezone As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Convert.ToString(Session("localTime"))) Then
            Response.Redirect("Default.aspx")
        Else
            ' Fetch local timezone
            localtimezone = Convert.ToString(Session("localTime"))
            If Not IsPostBack Then
                '   GetCurrentTimezones()
                GetAllTests()
            End If
        End If
    End Sub
    'Private Sub GetCurrentTimezones()
    '    DdlTz.Items.Add(New ListItem(Convert.ToString(Application("DefTimeZone")), "1"))
    '    DdlTz.Items.Add(New ListItem(localtimezone, "2"))
    '    DdlTz.SelectedIndex = -1
    '    DdlTz.Items.FindByValue("2").Selected = True

    '    'ddlTz.DataSource = TimeZoneInfo.GetSystemTimeZones();
    '    'ddlTz.DataValueField = "DisplayName";
    '    'ddlTz.DataTextField = "Id";
    '    'ddlTz.DataBind();
    '    'ddlTz.Items.Insert(0, new ListItem("-- Choose TimeZone--", "")) ;
    'End Sub

    ''' <summary>
    ''' Bind Upcoming Gridview 
    ''' </summary>
    Private Sub GetAllTests()
        AdoHelper.ConnectionString = ConnectionString
        Dim dataSet As DataSet = New AdoHelper().ExecDataSetProc("[dbo].[spGetUpcomingTest]")
        GrdTests.DataSource = dataSet.Tables(0).DefaultView
        GrdTests.DataBind()
    End Sub

    ''' <summary>
    ''' Row level operation : 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub GrdTests_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then

            ' Set Application Default Time when browser time not available
            If String.IsNullOrEmpty(Convert.ToString(Session("localTime"))) Then
                localtimezone = Convert.ToString(Application("DefTimeZone"))
            Else
                localtimezone = Convert.ToString(Session("localTime"))
            End If

            Dim ltStart As Literal = CType(e.Row.FindControl("ltStart"), Literal)
            Dim dtStartTime As DateTime = Commonfunction.UTCtoOther(Convert.ToDateTime(ltStart.Text), localtimezone, Convert.ToString(Application("DefTimeZone")))
            ' Date time display formatting
            ltStart.Text = dtStartTime.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture)

            Dim ltEnd As Literal = CType(e.Row.FindControl("ltEnd"), Literal)
            Dim dtEndTime As DateTime = Commonfunction.UTCtoOther(Convert.ToDateTime(ltEnd.Text), localtimezone, Convert.ToString(Application("DefTimeZone")))
            ltEnd.Text = dtEndTime.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture)

            ' Start Time is equal to Current time
            Dim curTime As DateTime = Commonfunction.UTCtoOther(Date.UtcNow, localtimezone, Convert.ToString(Application("DefTimeZone")))

            ' START BUTTON Action : Compare Two UTC times
            Dim compdtStartTodtCur As Integer = Date.Compare(curTime, dtStartTime)
            Dim compdtEndTodtCur As Integer = Date.Compare(curTime, dtEndTime)
            Console.WriteLine(compdtStartTodtCur)
            Console.WriteLine(compdtEndTodtCur)

            '((Literal)e.Row.FindControl("ltDuration1")).Text = compdtStartTodtCur.ToString() + "," + compdtEndTodtCur.ToString();

            Dim BtnStart As Button = CType(e.Row.FindControl("BtnStart"), Button)

            If compdtStartTodtCur >= 0 AndAlso compdtEndTodtCur <= 0 Then
                ' Start button appears
                BtnStart.Attributes.Add("class", "btn m-t-10 m-r-5 btn-rounded btn-outline-success")
                BtnStart.CommandName = "s"
            ElseIf compdtStartTodtCur < 0 Then
                ' Test yet to start
                BtnStart.Attributes.Add("class", "btn m-t-10 m-r-5 btn-rounded btn-outline-danger")
                BtnStart.Text = "Upcoming"
                BtnStart.CommandName = "u"
                'BtnStart.Attributes.Add("onclick", "confirm('Test is yet to start');return false;")
            Else
                ' very very Rare case
                BtnStart.Attributes.Add("class", "btn m-t-10 m-r-5 btn-rounded btn-outline-warning")
                BtnStart.Enabled = False
                BtnStart.Text = "Past"
            End If
        End If
    End Sub

    'Protected Sub DdlTz_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    localtimezone = Convert.ToString(DdlTz.SelectedItem.Text)
    '    GetAllTests()
    'End Sub

    Protected Sub GrdTests_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GrdTests.PageIndex = e.NewPageIndex
        GetAllTests()
    End Sub

    Protected Sub GrdTests_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Session.Add("action", e.CommandName)
        Session.Add("topicId", e.CommandArgument.ToString)
        Response.Redirect("StudentTest.aspx")
    End Sub
End Class