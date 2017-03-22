Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace WindowsCalender

    '/// <summary>
    '/// Main class to convert English date to Nepali date
    '/// </summary>
    Public Class Convert2NepaliDate

        Dim NepaliDateDataArray As New NepaliDateDataArray
        '/// <summary>
        '/// Main Algorithm to Convert English date to Nepali date
        '/// </summary>
        '/// <param name="enDate">English date to get converted</param>
        '/// <returns>NepaliDate object with actual Nepali date info</returns>
        Public Function GetNepaliDate(ByVal enDate As Date) As NepaliDate
            '#region Core Algorithm for Nepali date conversion
            '//Getting Nepali date data for Nepali date calculation
            On Error Resume Next

            Dim npDateData() As Integer = NepaliDateDataArray.GetNepaliDateDataArray(enDate.Year)

            '//Getting English day of the year
            Dim enDayOfYear As Integer = enDate.DayOfYear

            '//Initializing Nepali Year from the data
            Dim npYear As Integer = npDateData(0)

            '//Initializing Nepali month to Poush (9)
            '//This is because English date Jan 1 always fall in Poush month of Nepali Calendar, which is 9th month of Nepali calendar
            Dim npMonth As Integer = 9

            '//Initializing Nepali DaysInMonth with total days in the month of Poush
            Dim npDaysInMonth As Integer = npDateData(2)

            '//Initializing temp nepali days
            '//This is sum of total days in each Nepali month starting Jan 1 in Nepali month Poush
            '//Note: for the month Poush, only counting days after Jan 1
            '//***** This is the key field to calculate Nepali date *****
            If npDateData(2) = Nothing Then Return Nothing
            Dim npTempDays As Integer = npDateData(2) - npDateData(1) + 1

            '//Looping through Nepali date data array to get exact Nepali month, Nepali year & Nepali daysInMonth information
            Dim i As Integer = 3
            While Val(enDayOfYear) > Val(npTempDays)
                npTempDays += npDateData(i)
                npDaysInMonth = npDateData(i)
                npMonth = npMonth + 1

                If npMonth > 12 Then

                    npMonth -= 12
                    npYear = npYear + 1
                End If
                i = i + 1
            End While

            '//Calculating Nepali day
            Dim npDay As Integer = npDaysInMonth - (Val(npTempDays) - Val(enDayOfYear))

            '#End Region

            '#region Constructing and returning NepaliDate object
            '//Returning back NepaliDate object with all the date details
            Dim npDate As New NepaliDate()
            npDate.npYear = npYear
            npDate.npMonth = npMonth
            npDate.npDay = npDay
            npDate.npDaysInMonth = npDaysInMonth
            npDate.npDate = String.Format("{0}/{1}/{2}", npYear, npMonth, npDay)
            npDate.DayOfWeek = enDate.DayOfWeek
            Return npDate
            '#End Region
        End Function

    End Class
End Namespace