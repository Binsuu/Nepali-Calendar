Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace WindowsCalender
    Public Class convert2English
        Dim NepaliDateDataArray As New NepaliDateDataArray
        Public Function GetEnglishDate(ByVal npDate As NepaliDate) As EnglishDate
            Try
                Dim nYear As Integer
                Dim nMonth As Integer
                Dim nDay As Integer
                nDay = npDate.npDay '(Mid(npDate, 1, 2))
                nMonth = npDate.npMonth 'Val(Mid(npDate, 4, 2))
                nYear = npDate.npYear 'Val(Mid(npDate, 7, 4))
                If nMonth > 9 Then
                    nYear = nYear + 1
                End If
                Dim npmonthdate() As Integer = getNepaliMonth(nDay, nMonth, nYear - 57)
                If npmonthdate Is Nothing Then Return Nothing
                nDay = npmonthdate(0)
                nMonth = npmonthdate(1)
                nYear = npmonthdate(2)
                Dim EngDate As New EnglishDate
                'EngDate.npDate = String.Format("{0}/{1}/{2}", npYear, npMonth, npDay)
                EngDate.EngDay = npmonthdate(0)
                EngDate.EngMonth = npmonthdate(1)
                EngDate.EngYear = npmonthdate(2)
                EngDate.EngDaysInMonth = npmonthdate(3)
                EngDate.EngDate = New Date(EngDate.EngYear, EngDate.EngMonth, EngDate.EngDay)
                Return (EngDate)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Function
        Public Function getNepaliMonth(ByVal ndays As Integer, ByVal nMonth As Integer, ByVal npyear As Integer) As Integer()
            Dim npDateData() As Integer = NepaliDateDataArray.GetNepaliDateDataArray(Val(npyear))
            Dim i, Nepdays, engDays, emth As Integer
            While npDateData Is Nothing
                If MainWindow.boolForward = True Then
                    MainWindow.NepaliYear = MainWindow.NepaliYear - 1
                    npyear = npyear - 1
                Else
                    MainWindow.NepaliYear = MainWindow.NepaliYear + 1
                    npyear = npyear + 1
                End If
                npDateData = NepaliDateDataArray.GetNepaliDateDataArray(Val(npyear))
            End While
            engDays = 0
            Select Case nMonth
                Case 1, 2, 3, 4, 5, 6, 7, 8
                    For i = 2 To nMonth + 4
                        Nepdays = Nepdays + npDateData(i)
                    Next
                    Nepdays = Nepdays + ndays - npDateData(1) + 1
                    For i = 1 To nMonth + 3
                        engDays = engDays + Date.DaysInMonth(npyear, i)
                    Next
                    emth = engDays - Nepdays
                    If emth >= 0 Then
                        Return New Integer(3) {(Date.DaysInMonth(npyear, i - 1) - emth), i - 1, npyear, npDateData(nMonth + 5)}
                    Else
                        Return New Integer(3) {Math.Abs(emth), i, npyear, npDateData(nMonth + 5)}
                    End If

                Case 9
                    For i = 2 To nMonth + 4
                        Nepdays = Nepdays + npDateData(i)
                    Next
                    Nepdays = Nepdays + ndays - npDateData(1) + 1
                    For i = 1 To nMonth + 3
                        engDays = engDays + Date.DaysInMonth(npyear, i)
                    Next
                    emth = engDays - Nepdays
                    If emth >= 0 Then
                        Return New Integer(3) {(Date.DaysInMonth(npyear, i - 1) - emth), 12, npyear, npDateData(14)}
                    Else
                        Return New Integer(3) {Math.Abs(emth), 1, npyear + 1, npDateData(14)}
                    End If
                Case 10, 11, 12
                    For i = 2 To nMonth - 8
                        Nepdays = Nepdays + npDateData(i)
                    Next
                    Nepdays = Nepdays + ndays - npDateData(1) + 1
                    For i = 1 To nMonth - 9
                        engDays = engDays + Date.DaysInMonth(npyear, i)
                    Next
                    emth = engDays - Nepdays
                    If emth >= 0 Then
                        Return New Integer(3) {(Date.DaysInMonth(npyear, i - 1) - emth), i - 1, npyear, npDateData(nMonth - 7)}
                    Else
                        Return New Integer(3) {Math.Abs(emth), i, npyear, npDateData(nMonth - 7)}
                    End If
                Case Else
                    MsgBox(nMonth.ToString & "Invalid date range!")
                    Return Nothing
            End Select
        End Function
    End Class

End Namespace

