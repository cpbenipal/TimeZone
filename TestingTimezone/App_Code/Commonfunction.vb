﻿Imports System
Imports System.Globalization

Namespace TestingTimezone
    Public Module Commonfunction
        ''' <summary>
        ''' Convert UTC to mentioned timezone, pass Application Default Timezone if no timezone found
        ''' </summary>
        ''' <param name="timeUtc"></param>
        ''' <param name="ToTimeZone"></param>
        ''' <param name="DefaultTimeZone">TimeZone not found</param>
        ''' <returns>DateTime in Specified Timezone</returns>
        Public Function UTCtoOther(ByVal timeUtc As DateTime, ByVal ToTimeZone As String, ByVal DefaultTimeZone As String) As Date
            Dim cstZone As TimeZoneInfo
            Dim cstTime As Date
            Try
                cstZone = TimeZoneInfo.FindSystemTimeZoneById(ToTimeZone)
                cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone)
            Catch ex As TimeZoneNotFoundException
                cstZone = TimeZoneInfo.FindSystemTimeZoneById(DefaultTimeZone)
                cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone)
            Catch ex As InvalidTimeZoneException
                cstZone = TimeZoneInfo.FindSystemTimeZoneById(DefaultTimeZone)
                cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone)
            End Try
            Return cstTime
        End Function
        ''' <summary>
        ''' Get Date Time as per Application Timezone
        ''' </summary>
        ''' <param name="ToTimeZone"></param>
        ''' <returns>String:DateTime</returns>
        Public Function CurrentDateTimeZone(ByVal ToTimeZone As String) As String
            Dim cstZone As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(ToTimeZone)
            Dim cstTime As Date = TimeZoneInfo.ConvertTimeFromUtc(Date.UtcNow, cstZone)
            Return cstTime.ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture) & " " & cstZone.StandardName
        End Function
        ''' <summary>
        ''' Convert source Date Time to target UTC, source will be Application Time if not found on system.
        ''' </summary>
        ''' <param name="Fromtime"></param>
        ''' <param name="FromTimeZone"></param>
        ''' <param name="DefaultTimeZone"></param>
        ''' <returns>Date in UTC</returns>
        Public Function ConvertToUTC(ByVal Fromtime As String, ByVal FromTimeZone As String, ByVal DefaultTimeZone As String) As Date

            Dim ustZone As TimeZoneInfo
            Dim ustTime As Date
            Dim universalZone As TimeZoneInfo = TimeZoneInfo.Utc
            Try

                ustZone = TimeZoneInfo.FindSystemTimeZoneById(FromTimeZone)
                ustTime = TimeZoneInfo.ConvertTime(Convert.ToDateTime(Fromtime), ustZone, universalZone)
            Catch ex As Exception
                ustZone = TimeZoneInfo.FindSystemTimeZoneById(DefaultTimeZone)
                ustTime = TimeZoneInfo.ConvertTime(Convert.ToDateTime(Fromtime), ustZone, universalZone)
            End Try
            Return ustTime

        End Function

        ''' <summary>
        ''' Get User Browser Time Zone matched with Available Timezone on Machine.
        ''' </summary>
        ''' <param name="Offset">Client Browser TimeOffset</param>
        ''' <param name="localdatetime">Client Browser Time</param>
        ''' <returns></returns>
        Public Function GetLocalTimeZone(ByVal Offset As String, ByVal localdatetime As String) As String
            Dim localtimezoneName As String = String.Empty
            Dim jsNumberOfMinutesOffset As String = Convert.ToString(Offset)
            Dim clientDateTime As Date = Convert.ToDateTime(localdatetime)
            Dim numberOfMinutes As Integer = (Int32.Parse(jsNumberOfMinutesOffset) * -1)
            Dim timeSpan As TimeSpan = TimeSpan.FromMinutes(numberOfMinutes)

            For Each info As TimeZoneInfo In TimeZoneInfo.GetSystemTimeZones
                ' Check if Daylighter Saving
                Dim extra As Integer = If(info.IsDaylightSavingTime(clientDateTime), 60, 0)
                If info.BaseUtcOffset = timeSpan Then
                    localtimezoneName = Convert.ToString(info.Id)
                    Exit For
                End If
            Next
            Return localtimezoneName
        End Function

    End Module
End Namespace
