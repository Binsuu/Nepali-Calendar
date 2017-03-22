Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Text
Imports System.Math
Imports System.Windows.Media.Animation
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes
Imports System.Globalization
Imports System.Threading
Imports System.Windows.Threading
Imports System.Windows.Forms

Namespace WindowsCalender
    Class MainWindow
        Dim j As Integer
        Dim dayz As Integer
        Dim ndayz, x, y As Integer
        Dim Dayofweek As String
        Dim convert2nepali As New Convert2NepaliDate
        Dim convert2English As New convert2English
        Dim curNMonth As Byte
        Dim curNyear As Integer
        Dim curEMonth As Byte
        Dim curEYear As Integer
        Dim eStartDate As Integer
        Dim nStartDate As Integer
        Public Shared NepaliYear As Integer
        Public Shared NepaliMonth As Integer
        Public Shared NepaliDay As Integer
        Public Shared boolForward As Boolean
        Public SAngle As Double
        Public EAngle As Double

        Dim Count As Integer
        Dim expand As Boolean
        Dim timer As DispatcherTimer
        Dim startMarque As Integer
        Dim txtContent As String
        Dim txtEContent As String
        Dim intStop As Integer
        Dim dt As DispatcherTimer = New DispatcherTimer()

#Region "LoadCalender"
        Public Sub New()
       
            ' This call is required by the designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.
            If getFont() = "Preeti" Then
                'If checkFont("Preeti") Then
                For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                    If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                        ctlr.FontSize = 18
                        ctlr.FontFamily = New FontFamily("Preeti")
                    End If
                Next
                btnSunday.Content = "cfO{t"
                btnSunday.FontFamily = New FontFamily("Preeti")
                btnSunday.FontSize = 15

                btnMonday.Content = ";f]d"
                btnMonday.FontFamily = New FontFamily("Preeti")
                btnMonday.FontSize = 15

                btnTue.Content = "d+un"
                btnTue.FontFamily = New FontFamily("Preeti")
                btnTue.FontSize = 15

                btnWed.Content = "a'w"
                btnWed.FontFamily = New FontFamily("Preeti")
                btnWed.FontSize = 15

                btnThus.Content = "laxL"
                btnThus.FontFamily = New FontFamily("Preeti")
                btnThus.FontSize = 15

                btnFri.Content = "z" & """qm"
                btnFri.FontFamily = New FontFamily("Preeti")
                btnFri.FontSize = 15

                btnSaturday.Content = "zlg"
                btnSaturday.FontFamily = New FontFamily("Preeti")
                btnSaturday.FontSize = 15

                flgPFont = True
            Else
                For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                    If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                        ctlr.FontSize = 12
                        ctlr.FontFamily = New FontFamily("Arial")
                    End If
                Next

                btnSunday.Content = "Sun"
                btnSunday.FontFamily = New FontFamily("Arial")
                btnSunday.FontSize = 11

                btnMonday.Content = "Mon"
                btnMonday.FontFamily = New FontFamily("Arial")
                btnMonday.FontSize = 11

                btnTue.Content = "Tues"
                btnTue.FontFamily = New FontFamily("Arial")
                btnTue.FontSize = 11

                btnWed.Content = "Wed"
                btnWed.FontFamily = New FontFamily("Arial")
                btnWed.FontSize = 11

                btnThus.Content = "Thus"
                btnThus.FontFamily = New FontFamily("Arial")
                btnThus.FontSize = 11

                btnFri.Content = "Fri"
                btnFri.FontFamily = New FontFamily("Arial")
                btnFri.FontSize = 11

                btnSaturday.Content = "Sat"
                btnSaturday.FontFamily = New FontFamily("Arial")
                btnSaturday.FontSize = 11

                flgPFont = False
            End If
            m_Minimize = Minimize()
        End Sub

        Private Sub MainWindow_Activated(sender As Object, e As EventArgs) Handles Me.Activated
            'If getFont() = "Preeti" Then
            '    NepaliFontSetting()
            'Else
            '    EnglishFontSetting()
            'End If
        End Sub


        Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
            Dim CurrentDate As Date
            Dim WorkingHeight As Double
            Dim WorkingRight As Double
            Dim workingBottom As Double
            Register_Soft(True)
            WorkingHeight = SystemParameters.WorkArea.Height
            WorkingRight = SystemParameters.WorkArea.Right
            workingBottom = SystemParameters.WorkArea.Bottom
            If m_Minimize = False Then
                Me.Top = workingBottom - Me.Height
            Else
                Me.Top = workingBottom - 35
            End If
            Me.Left = WorkingRight - Me.Width

            Me.Title = "Nepali Calendar"

            EnDate.EngMonth = Now.Month
            EnDate.EngDay = Now.Day
            EnDate.EngYear = Now.Year
            CurrentDate = New Date(EnDate.EngYear, EnDate.EngMonth, EnDate.EngDay)
            npdate = convert2nepali.GetNepaliDate(DateTime.Parse(CurrentDate))
            j = (npdate.DayOfWeek - npdate.npDay Mod 7) + 1
            If j < 0 Then
                j = 7 + j
            End If
            nStartDate = j

            NepaliMonth = npdate.npMonth
            NepaliYear = npdate.npYear
            NepaliDay = npdate.npDay
            curNMonth = npdate.npMonth
            curNyear = npdate.npYear
            curEMonth = EnDate.EngMonth
            curEYear = EnDate.EngYear

            curNepaliDate = npdate
            curEnglishDate = EnDate

            If setMode() = "E" Then
                If flgPFont = True Then
                    NepaliFontSetting()
                Else
                    EnglishFontSetting()
                End If

                Dim _Date As Date
                _Date = New Date(Now.Year, Now.Month, 1)

                loadEnglishdate(Now.Date, _Date.DayOfWeek)
                ' loadEnglishdate(Now.Date, CDate(Now.Month & "/01/" & Now.Year).DayOfWeek)
                EngDateContent.Content = GetNepaliMonth(EnDate)
                If flgPFont = True Then
                    DateContent.Content = MonthName(EnDate.EngMonth, False) ' Format(CDate(EnDate.EngMonth & "/01/2015"), "MMMM").ToString
                    DateofToday.Content = "Today: " & DateContent.Content & " " & EnDate.EngDay & ", " & Now.DayOfWeek.ToString & ", " & DateContentYear.Content
                    lM.Content = "Today: " & DateContent.Content & " " & EnDate.EngDay & ", " & Now.DayOfWeek.ToString & ", " & DateContentYear.Content
                Else
                    DateContent.Content = MonthName(EnDate.EngMonth, False) 'Format(CDate(EnDate.EngMonth & "/01/2015"), "MMMM").ToString
                    DateContentYear.Content = EnDate.EngYear
                    DateofToday.Content = "Today: " & DateContent.Content & " " & EnDate.EngDay & ", " & Now.DayOfWeek.ToString & ", " & DateContentYear.Content
                    lM.Content = "Today: " & DateContent.Content & " " & EnDate.EngDay & ", " & Now.DayOfWeek.ToString & ", " & DateContentYear.Content
                End If
                NDate.Content = DateContent.Content & " " & DateContentYear.Content

            Else
                If flgPFont = True Then
                    NepaliFontSetting()
                Else
                    EnglishFontSetting()
                End If
                loaddate(npdate, j)
                DateContent.Content = nepMonth(npdate.npMonth)
                If flgPFont = True Then
                    DateContent.Content = nepMonth(npdate.npMonth)
                    DateContentYear.Content = covstr(npdate.npYear)
                Else
                    DateContent.Content = GetnepMonth(npdate.npMonth)
                    DateContentYear.Content = npdate.npYear
                End If

                EngDateContent.Content = GetEnglishMonth(npdate)
                If flgPFont = True Then
                    DateofToday.Content = "cfh M " & DateContent.Content & " " & covstr(npdate.npDay) & " ut]" & ", " & getNepaliDay(CInt(Now.DayOfWeek)) & ", " & DateContentYear.Content
                    lblMenu.Content = "cfh M " & DateContent.Content & " " & covstr(npdate.npDay) & " ut]" & ", " & getNepaliDay(CInt(Now.DayOfWeek)) & ", " & DateContentYear.Content
                Else
                    DateofToday.Content = "Today: " & DateContent.Content & " " & npdate.npDay & ", " & Now.DayOfWeek.ToString & ", " & DateContentYear.Content
                    lblMenu.Content = "Today: " & DateContent.Content & " " & npdate.npDay & ", " & Now.DayOfWeek.ToString & ", " & DateContentYear.Content
                End If
                NDate.Content = DateContent.Content & " " & DateContentYear.Content
            End If
            If m_Minimize = True Then
                expand = False
                Hide_Detail(Nothing, Nothing)
                lblMenu_LostFocus(Nothing, Nothing)
            End If
            txtContent = Mid(lblMenu.Content, 7, Len(lblMenu.Content)) ' lblMenu.Content
            lblMenu.Content = txtContent
            txtEContent = Mid(lM.Content, 7, Len(lM.Content))
            lM.Content = txtEContent
        End Sub

        Private Sub FontSetting()
            If flgPFont = True Then
                If setMode() = "E" Then
                    DateContent.FontFamily = New FontFamily("Arial")
                    DateContent.FontSize = 15

                    DateofToday.FontSize = 12
                    DateofToday.FontFamily = New FontFamily("Arial")

                    lM.FontSize = 12
                    lM.FontFamily = New FontFamily("Calibri")

                    'lblMenu.FontSize = 12
                    'lblMenu.FontFamily = New FontFamily("Calibri")

                    NDate.FontFamily = New FontFamily("Arial")
                    NDate.FontSize = 15

                    DateContentYear.FontSize = 15
                    DateContentYear.FontFamily = New FontFamily("Arial")

                    EngDateContent.FontSize = 15
                    EngDateContent.FontFamily = New FontFamily("Preeti")
                Else
                    DateofToday.FontSize = 16
                    DateofToday.FontFamily = New FontFamily("Preeti")

                    lblMenu.FontSize = 16
                    lblMenu.FontFamily = New FontFamily("Preeti")

                    DateContent.FontFamily = New FontFamily("Preeti")
                    DateContent.FontSize = 24

                    NDate.FontFamily = New FontFamily("Preeti")
                    NDate.FontSize = 24

                    DateContentYear.FontSize = 24
                    DateContentYear.FontFamily = New FontFamily("Preeti")
                    DateContentYear.Content = covstr(npdate.npYear)

                    EngDateContent.FontSize = 12
                    EngDateContent.FontFamily = New FontFamily("Arial")
                End If

            Else
                If setMode() = "E" Then
                    EngDateContent.FontSize = 12
                    EngDateContent.FontFamily = New FontFamily("Arial")
                End If
                DateContent.FontFamily = New FontFamily("Arial")
                DateContent.FontSize = 15

                DateofToday.FontSize = 12
                DateofToday.FontFamily = New FontFamily("Arial")

                lblMenu.FontSize = 12
                lblMenu.FontFamily = New FontFamily("Calibri")

                lM.FontSize = 12
                lM.FontFamily = New FontFamily("Calibri")

                NDate.FontFamily = New FontFamily("Arial")
                NDate.FontSize = 15

                DateContentYear.FontSize = 15
                DateContentYear.FontFamily = New FontFamily("Arial")

            End If
        End Sub
        Private Function GetEnglishMonth(ByVal _NepaliDate As NepaliDate) As String
            Dim NepDate As New NepaliDate
            Dim En_Date As New EnglishDate
            Dim engYear As String
            Dim _Date As Date
            Dim strMonth As String
            NepDate.npYear = _NepaliDate.npYear
            NepDate.npMonth = _NepaliDate.npMonth
            NepDate.npDay = 1
            En_Date = convert2English.GetEnglishDate(NepDate)
            _Date = New Date(En_Date.EngYear, En_Date.EngMonth, En_Date.EngDay)
            strMonth = GetEngMonth(_Date.Month)
            engYear = _Date.Year
            NepDate.npDay = _NepaliDate.npDaysInMonth
            En_Date = convert2English.GetEnglishDate(NepDate)
            _Date = New Date(En_Date.EngYear, En_Date.EngMonth, En_Date.EngDay)
            If engYear <> _Date.Year Then
                strMonth = strMonth & "  " & Mid(engYear, 2, 3)
                strMonth = strMonth & " " & " / " & GetEngMonth(_Date.Month) & "  " & Mid(_Date.Year, 2, 3)
            Else
                strMonth = strMonth & " / " & GetEngMonth(_Date.Month)
                strMonth = strMonth & " " & engYear
            End If

            Return strMonth
        End Function

        Private Function GetNepaliMonth(ByVal _EngDate As EnglishDate) As String
            Dim NepDate As NepaliDate
            Dim En_Date As New EnglishDate
            Dim NepYear As String
            Dim _Date As Date
            Dim strMonth As String
            En_Date.EngYear = _EngDate.EngYear
            En_Date.EngMonth = _EngDate.EngMonth
            En_Date.EngDay = 1
            _Date = New Date(En_Date.EngYear, En_Date.EngMonth, En_Date.EngDay)
            NepDate = convert2nepali.GetNepaliDate(_Date)

            If flgPFont = True Then
                strMonth = nepMonth(NepDate.npMonth)
            Else
                strMonth = GetnepMonth(NepDate.npMonth)
            End If
            NepYear = NepDate.npYear
            En_Date.EngDay = Date.DaysInMonth(_Date.Year, _Date.Month)
            _Date = New Date(En_Date.EngYear, En_Date.EngMonth, En_Date.EngDay)
            NepDate = convert2nepali.GetNepaliDate(_Date)
            If NepYear <> NepDate.npYear Then
                If setMode() = "E" Then
                    strMonth = strMonth & "  " & covstr(Mid(NepYear, 2, 3))
                    If flgPFont = True Then
                        strMonth = strMonth & " " & " . " & nepMonth(NepDate.npMonth) & "  " & covstr(Mid(NepDate.npYear, 2, 3))
                    Else
                        strMonth = strMonth & " " & " / " & GetnepMonth(NepDate.npMonth) & "  " & covstr(Mid(NepDate.npYear, 2, 3))
                    End If
                Else
                    strMonth = strMonth & "  " & Mid(NepYear, 2, 3)
                    strMonth = strMonth & " " & " / " & GetnepMonth(NepDate.npMonth) & "  " & Mid(NepDate.npYear, 2, 3)
                End If
            Else
                If flgPFont = False Then
                    strMonth = strMonth & " / " & GetnepMonth(NepDate.npMonth)
                    strMonth = strMonth & " " & NepYear
                Else
                    strMonth = strMonth & " . " & nepMonth(NepDate.npMonth)
                    strMonth = strMonth & " " & covstr(NepYear)
                End If

            End If

            Return strMonth
        End Function
        Private Function GetEngMonth(ByVal mth As Integer) As String
            GetEngMonth = ""
            Select Case mth
                Case 1
                    GetEngMonth = "Jan"
                Case 2
                    GetEngMonth = "Feb"
                Case 3
                    GetEngMonth = "Mar"
                Case 4
                    GetEngMonth = "Apr"
                Case 5
                    GetEngMonth = "May"
                Case 6
                    GetEngMonth = "Jun"
                Case 7
                    GetEngMonth = "Jul"
                Case 8
                    GetEngMonth = "Aug"
                Case 9
                    GetEngMonth = "Sep"
                Case 10
                    GetEngMonth = "Oct"
                Case 11
                    GetEngMonth = "Nov"
                Case 12
                    GetEngMonth = "Dec"
                Case Else
                    MsgBox("Invalid Date!", vbCritical)
            End Select
            Return GetEngMonth
        End Function
        ''' <summary>
        ''' Load on Grid
        ''' </summary>
        ''' <param name="Nepalidate">Nepali Date</param>
        ''' <param name="sdate">Starting Day of the Month</param>
        ''' <remarks></remarks>
        Public Sub loaddate(ByVal Nepalidate As NepaliDate, ByVal sdate As Integer)
            If sdate = 7 Then sdate = 0
            Dim btn As String, ebtn As String
            Dim En_Date As New EnglishDate
            En_Date = convert2English.GetEnglishDate(Nepalidate)
            clear()
            For i As Int32 = 1 To Nepalidate.npDaysInMonth
                btn = "btn" & Val(i + sdate)
                ebtn = "Ebtn" & Val(i + sdate)
                For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                    If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                        Dim DateButton As System.Windows.Controls.Button
                        DateButton = ctlr

                        If DateButton.Name = btn Then
                            If flgPFont = True Then
                                DateButton.Content = covstr(i)
                            Else
                                DateButton.Content = i
                            End If
                            If DateButton.IsEnabled = False Then DateButton.IsEnabled = True
                        ElseIf Val(i + sdate) > 35 Then
                            btn = "btn" & Val(i + sdate - 35)
                            If DateButton.Name = btn Then
                                If flgPFont = True Then
                                    DateButton.Content = covstr(i)
                                Else
                                    DateButton.Content = i
                                End If
                                If DateButton.IsEnabled = False Then DateButton.IsEnabled = True
                            End If
                        End If
                        If DateButton.Name = ebtn Then
                            DateButton.Content = loadEngdate(i, Nepalidate.npMonth, Nepalidate.npYear)
                            If DateButton.IsEnabled = False Then DateButton.IsEnabled = True
                        ElseIf Val(i + sdate) > 35 Then
                            ebtn = "Ebtn" & Val(i + sdate - 35)
                            If DateButton.Name = ebtn Then
                                DateButton.Content = loadEngdate(i, Nepalidate.npMonth, Nepalidate.npYear)
                                If DateButton.IsEnabled = False Then DateButton.IsEnabled = True
                            End If
                        End If
                        Dim NDate As New NepaliDate
                        NDate.npMonth = Nepalidate.npMonth
                        NDate.npYear = Nepalidate.npYear
                        NDate.npDay = i
                        NDate.npDate = Format(NDate.npDay, "00") & "/" & Format(Val(NDate.npMonth), "00") & "/" & NDate.npYear
                        En_Date = convert2English.GetEnglishDate(NDate)
                        If DateButton.Name = btn Then
                            If convert2English.GetEnglishDate(NDate).EngDate = Now.Date Then
                                DateButton.SetResourceReference(StyleProperty, "ButtonCurDay")
                            Else
                                If Mid(btn, 4, 2) Mod 7 = 0 Then
                                    DateButton.SetResourceReference(StyleProperty, "ButtonSaturday")
                                Else
                                    DateButton.SetResourceReference(StyleProperty, "StealthButton1")
                                End If

                            End If
                        End If
                        If DateButton.Name = ebtn Then
                            If En_Date.EngDay = loadEngdate(i, NDate.npMonth, NDate.npYear) And En_Date.EngDate = Now.Date Then
                                DateButton.SetResourceReference(StyleProperty, "ButtonCurEngDay")
                            Else
                                If Mid(ebtn, 5, 2) Mod 7 = 0 Then
                                    DateButton.SetResourceReference(StyleProperty, "EHoliday")
                                Else
                                    DateButton.SetResourceReference(StyleProperty, "EngBtn")
                                End If

                            End If
                        End If
                    End If
                Next
            Next

            If flgPFont = True Then
                DateContent.Content = nepMonth(Nepalidate.npMonth)
                DateContentYear.Content = covstr(Nepalidate.npYear)
            Else
                DateContent.Content = GetnepMonth(Nepalidate.npMonth)
                DateContentYear.Content = Nepalidate.npYear
            End If
            If flgPFont = True Then
                DateofToday.Content = "cfh M " & nepMonth(npdate.npMonth) & " " & covstr(npdate.npDay) & " ut]" & ", " & getNepaliDay(CInt(Now.DayOfWeek)) & ", " & covstr(npdate.npYear)
                lblMenu.Content = "cfh M " & nepMonth(npdate.npMonth) & " " & covstr(npdate.npDay) & " ut]" & ", " & getNepaliDay(CInt(Now.DayOfWeek)) & ", " & covstr(npdate.npYear)
            Else
                DateofToday.Content = "Today: " & GetnepMonth(npdate.npMonth) & " " & npdate.npDay & ", " & Now.DayOfWeek.ToString & ", " & npdate.npYear
                lblMenu.Content = "Today: " & GetnepMonth(npdate.npMonth) & " " & npdate.npDay & ", " & Now.DayOfWeek.ToString & ", " & npdate.npYear
            End If

            NDate.Content = DateContent.Content & " " & DateContentYear.Content
            EngDateContent.Content = GetEnglishMonth(Nepalidate)
            txtContent = Mid(lblMenu.Content, 7, Len(lblMenu.Content)) ' lblMenu.Content
            lblMenu.Content = txtContent
        End Sub

        Public Sub loadEnglishdate(ByVal EngDate As Date, ByVal sdate As Integer)
            Dim btn As String, ebtn As String
            Dim enDate1 As New EnglishDate
            Dim en_Date As New EnglishDate
            Dim np_Date As NepaliDate
            np_Date = New NepaliDate
            np_Date = convert2nepali.GetNepaliDate(EngDate)
            enDate1 = convert2English.GetEnglishDate(np_Date)
            clear()
            For i As Int32 = 1 To Date.DaysInMonth(EngDate.Year, EngDate.Month)
                btn = "btn" & Val(i + sdate)
                ebtn = "Ebtn" & Val(i + sdate)
                For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                    If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                        Dim DateButton As System.Windows.Controls.Button
                        DateButton = ctlr

                        If DateButton.Name = btn Then
                            'If flgPFont = True Then
                            '    DateButton.Content = covstr(i)
                            'Else
                            DateButton.Content = i
                            'End If
                            If DateButton.IsEnabled = False Then DateButton.IsEnabled = True
                        ElseIf Val(i + sdate) > 35 Then
                            btn = "btn" & Val(i + sdate - 35)
                            If DateButton.Name = btn Then
                                'If flgPFont = True Then
                                '    DateButton.Content = covstr(i)
                                'Else
                                DateButton.Content = i
                                'End If
                                If DateButton.IsEnabled = False Then DateButton.IsEnabled = True
                            End If
                        End If
                        If DateButton.Name = ebtn Then
                            If flgPFont = True Then
                                DateButton.Content = covstr(GetNDay(i, EngDate.Month, EngDate.Year))
                            Else
                                DateButton.Content = GetNDay(i, EngDate.Month, EngDate.Year)
                            End If
                            If DateButton.IsEnabled = False Then DateButton.IsEnabled = True
                        ElseIf Val(i + sdate) > 35 Then
                            ebtn = "Ebtn" & Val(i + sdate - 35)
                            If DateButton.Name = ebtn Then
                                If flgPFont = True Then
                                    DateButton.Content = covstr(GetNDay(i, EngDate.Month, EngDate.Year))
                                Else
                                    DateButton.Content = GetNDay(i, EngDate.Month, EngDate.Year)
                                End If
                                If DateButton.IsEnabled = False Then DateButton.IsEnabled = True
                            End If
                        End If
                        If DateButton.Name = btn Then
                            en_Date.EngMonth = enDate1.EngMonth
                            en_Date.EngDay = i
                            en_Date.EngYear = enDate1.EngYear
                            Dim _date As Date
                            _date = New Date(en_Date.EngYear, en_Date.EngMonth, en_Date.EngDay)
                            en_Date.EngDate = _date 'Format(_date, "MM/dd/yyyy")
                            ' en_Date.EngDate = Format(CDate(en_Date.EngMonth & "/" & en_Date.EngDay & "/" & en_Date.EngYear), "MM/dd/yyyy")
                            If i = EngDate.Day And convert2English.GetEnglishDate(np_Date).EngDate = Now.Date Then
                                DateButton.SetResourceReference(StyleProperty, "ButtonCurDay")
                            Else
                                If Mid(btn, 4, 2) Mod 7 = 0 Then
                                    DateButton.SetResourceReference(StyleProperty, "ButtonSaturday")
                                Else
                                    DateButton.SetResourceReference(StyleProperty, "StealthButton1")
                                End If

                            End If
                        End If
                        If DateButton.Name = ebtn Then
                            If np_Date.npDay = GetNDay(i, EngDate.Month, EngDate.Year) And enDate1.EngDate = Now.Date Then
                                DateButton.SetResourceReference(StyleProperty, "ButtonCurEngDay")
                            Else
                                If Mid(ebtn, 5, 2) Mod 7 = 0 Then
                                    DateButton.SetResourceReference(StyleProperty, "EHoliday")
                                Else
                                    DateButton.SetResourceReference(StyleProperty, "EngBtn")
                                End If

                            End If
                        End If
                    End If
                Next
            Next
            EngDateContent.Content = GetNepaliMonth(enDate1)
            If flgPFont = True Then
                DateContent.Content = MonthName(enDate1.EngMonth, False) 'Format(CDate(enDate.EngMonth & "/01/2015"), "MMMM").ToString
                DateContentYear.Content = enDate1.EngYear
            Else
                DateContent.Content = MonthName(enDate1.EngMonth, False)
                DateContentYear.Content = enDate1.EngYear
            End If
            DateofToday.Content = "Today: " & Format(Now.Date, "MMMM").ToString & " " & Now.Day & ", " & Now.DayOfWeek.ToString & ", " & Now.Year
            lM.Content = "Today: " & Format(Now.Date, "MMMM").ToString & " " & Now.Day & ", " & Now.DayOfWeek.ToString & ", " & Now.Year
            NDate.Content = DateContent.Content & " " & DateContentYear.Content
            txtEContent = Mid(lM.Content, 7, Len(lM.Content))
            lM.Content = txtEContent
        End Sub

        Private Function loadEngdate(ByVal npday As Integer, ByVal NepMonth As Integer, ByVal nepYwear As Integer) As Integer
            Dim np_date As New NepaliDate
            np_date.npMonth = NepMonth
            np_date.npYear = nepYwear
            np_date.npDay = npday
            Dim En_Date As EnglishDate = convert2English.GetEnglishDate(np_date)

            Dim EngDate1 As Date
            EngDate1 = New Date(En_Date.EngYear, En_Date.EngMonth, En_Date.EngDay)
            Return EngDate1.Day
        End Function

        Private Function GetNDay(ByVal eday As Integer, ByVal eMonth As Integer, ByVal eYear As Integer) As Integer
            Dim ED As Date
            ED = New Date(eYear, eMonth, eday)
            Dim NDate As NepaliDate = convert2nepali.GetNepaliDate(ED)
            Return NDate.npDay
        End Function

        Private Function covstr(ByVal str1 As String) As String
            Dim str As Integer
            Dim charStr As String
            Dim strlen As Integer
            strlen = Len(str1)
            charStr = ""
            For i = 1 To strlen
                str = conASCII(Asc(Mid(str1, i, 1)))
                charStr = charStr & Chr(str).ToString
            Next
            Return charStr
        End Function

        Private Function conASCII(ByVal Asci As Integer) As Integer
            Select Case Asci
                Case 48
                    Return 41
                Case 49
                    Return 33
                Case 50
                    Return 64
                Case 51
                    Return 35
                Case 52
                    Return 36
                Case 53
                    Return 37
                Case 54
                    Return 94
                Case 55
                    Return 38
                Case 56
                    Return 42
                Case 57
                    Return 40

                Case 41
                    Return 48
                Case 33
                    Return 49
                Case 64
                    Return 50
                Case 35
                    Return 51
                Case 36
                    Return 52
                Case 37
                    Return 53
                Case 94
                    Return 54
                Case 38
                    Return 55
                Case 42
                    Return 56
                Case 40
                    Return 57

                Case Else
                    Return 0
            End Select
        End Function



        Private Function nepMonth(ByVal npMonth As Integer) As String
            Select Case npMonth
                Case 1
                    Return "a}zfv"
                Case 2
                    Return "h]7"
                Case 3
                    Return "c;f/"
                Case 4
                    Return ";fpg"
                Case 5
                    Return "eb}"
                Case 6
                    Return "c;f]h"
                Case 7
                    Return "sflQ{s"
                Case 8
                    Return "d+l;/"
                Case 9
                    Return "kf}if"
                Case 10
                    Return "df3"
                Case 11
                    Return "kmfu'g"
                Case 12
                    Return "r}t"
                Case Else
                    Throw New ArgumentOutOfRangeException(npMonth.ToString, "Invalid Month!")
            End Select
        End Function
        Private Sub clear()
            For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                    Dim DateButton As System.Windows.Controls.Button
                    DateButton = ctlr
                    DateButton.Content = String.Empty
                    DateButton.IsEnabled = False
                    If Mid(DateButton.Name, 1, 3) = "btn" Then
                        DateButton.SetResourceReference(StyleProperty, "StealthButton1")
                    Else
                        DateButton.SetResourceReference(StyleProperty, "EngBtn")
                    End If
                End If
            Next
        End Sub



        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnBackWard.Click
            Try
                Dim EngDate As New EnglishDate
                Dim NepaliDate As New NepaliDate
                NepaliDate.npYear = NepaliYear
                NepaliMonth = NepaliMonth - 1

                If setMode() = "E" Then
                    If Val(NepaliMonth) < 1 Then
                        NepaliMonth = 12
                        NepaliYear = NepaliYear - 1
                        NepaliDate.npYear = NepaliYear - 1
                    End If
                End If
                If Val(NepaliMonth) < 1 Then
                    NepaliMonth = 12
                    NepaliYear = NepaliYear - 1
                    NepaliDate.npYear = NepaliYear
                End If
                NepaliDate.npYear = NepaliYear
                NepaliDate.npMonth = NepaliMonth
                NepaliDate.npDay = NepaliDay
                NepaliDate.npDate = NepaliMonth & "/" & NepaliDay & "/" & NepaliYear

                If MINNDate > Val(NepaliDate.npYear & Format(NepaliDate.npMonth, "00") & Format(NepaliDate.npDay, "00")) Then
                    MsgBox("Date of out of Range", vbInformation)
                    If NepaliMonth = 12 Then
                        NepaliMonth = 1
                        NepaliYear = NepaliYear + 1
                    Else
                        NepaliMonth = NepaliMonth + 1
                    End If
                    Exit Sub
                End If

                EngDate = convert2English.GetEnglishDate(NepaliDate)

                If EngDate Is Nothing Then
                    NepaliDate.npYear = NepaliDate.npYear + 1
                    EngDate = convert2English.GetEnglishDate(NepaliDate)
                End If


                curNMonth = NepaliDate.npMonth
                curNyear = NepaliDate.npYear
                curEMonth = EngDate.EngMonth
                curEYear = EngDate.EngYear

                If setMode() = "N" Then
                    NepaliDate = convert2nepali.GetNepaliDate(EngDate.EngDate)
                    j = (NepaliDate.DayOfWeek - NepaliDate.npDay Mod 7) + 1
                    If j < 0 Then
                        j = 7 + j
                    End If
                    If MINNDate > Val(NepaliDate.npYear & Format(NepaliDate.npMonth, "00") & Format(NepaliDate.npDay, "00")) Then
                        MsgBox("Date of out of Range", vbInformation)
                        If NepaliMonth = 12 Then
                            NepaliMonth = 1
                            NepaliYear = NepaliYear + 1
                        Else
                            NepaliMonth = NepaliMonth + 1
                        End If
                        Exit Sub
                    End If
                    loaddate(NepaliDate, j)
                    If flgPFont = True Then
                        DateContent.Content = nepMonth(NepaliDate.npMonth)
                        DateContentYear.Content = covstr(NepaliDate.npYear)
                    Else
                        DateContent.Content = GetnepMonth(NepaliDate.npMonth)
                        DateContentYear.Content = NepaliDate.npYear
                    End If
                    NDate.Content = DateContent.Content & " " & DateContentYear.Content
                    EngDateContent.Content = GetEnglishMonth(NepaliDate)
                Else
                    If flgPFont = True Then
                        NepaliFontSetting()
                    Else
                        EnglishFontSetting()
                    End If
                    Dim _date As Date
                    _date = New Date(EngDate.EngYear, EngDate.EngMonth, 1)
                    If MINEDate > Val(_date.Year & Format(_date.Month, "00") & Format(_date.Day, "00")) Then
                        MsgBox("Date of out of Range", vbInformation)
                        If NepaliMonth = 12 Then
                            NepaliMonth = 1
                            NepaliYear = NepaliYear + 1
                        Else
                            NepaliMonth = NepaliMonth + 1
                        End If
                        Exit Sub
                    End If
                    loadEnglishdate(EngDate.EngDate, _date.DayOfWeek)
                    EngDateContent.Content = GetNepaliMonth(EngDate)
                    DateContent.Content = MonthName(EngDate.EngMonth, False) 'Format(CDate(EngDate.EngMonth & "/01/2015"), "MMMM").ToString
                    NDate.Content = DateContent.Content & " " & DateContentYear.Content
                End If
                curNepaliDate = NepaliDate
                curEnglishDate = EngDate
            Catch ex As Exception
            End Try
        End Sub

        Private Sub BtnForward_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles BtnForward.Click
            Dim EngDate As New EnglishDate
            Dim NepaliDate As New NepaliDate
            Try
                NepaliDate.npYear = NepaliYear
                NepaliMonth = NepaliMonth + 1
                If setMode() = "E" Then
                    If Val(NepaliMonth) > 12 Then
                        NepaliMonth = 1
                        NepaliYear = NepaliYear + 1
                        NepaliDate.npYear = NepaliYear + 1
                    End If
                End If
                If Val(NepaliMonth) > 12 Then
                    NepaliMonth = 1
                    NepaliYear = NepaliYear + 1
                    NepaliDate.npYear = NepaliYear
                End If
                NepaliDate.npYear = NepaliYear
                NepaliDate.npMonth = NepaliMonth
                NepaliDate.npDay = NepaliDay
                NepaliDate.npDate = NepaliMonth & "/" & NepaliDay & "/" & NepaliYear
                If MaxNDate < Val(NepaliDate.npYear & Format(NepaliDate.npMonth, "00") & Format(NepaliDate.npDay, "00")) Then
                    MsgBox("Date of out of Range", vbInformation)
                    If NepaliMonth = 1 Then
                        NepaliMonth = 12
                        NepaliYear = NepaliYear - 1
                    Else
                        NepaliMonth = NepaliMonth - 1
                    End If
                    Exit Sub
                End If
                EngDate = convert2English.GetEnglishDate(NepaliDate)


                If EngDate Is Nothing Then
                    Exit Sub
                End If
                NepaliDate = convert2nepali.GetNepaliDate(EngDate.EngDate)
                j = (NepaliDate.DayOfWeek - NepaliDate.npDay Mod 7) + 1
                If j < 0 Then
                    j = 7 + j
                End If
                FontSetting()
                curNMonth = NepaliDate.npMonth
                curNyear = NepaliDate.npYear
                curEMonth = EngDate.EngMonth
                curEYear = EngDate.EngYear
                If setMode() = "N" Then
                    If MaxNDate < Val(NepaliDate.npYear & Format(NepaliDate.npMonth, "00") & Format(NepaliDate.npDay, "00")) Then
                        MsgBox("Date of out of Range", vbInformation)
                        If NepaliMonth = 1 Then
                            NepaliMonth = 12
                            NepaliYear = NepaliYear - 1
                        Else
                            NepaliMonth = NepaliMonth - 1
                        End If
                        Exit Sub
                    End If
                    loaddate(NepaliDate, j)
                    If flgPFont = True Then
                        DateContent.Content = nepMonth(NepaliDate.npMonth)
                        DateContentYear.Content = covstr(NepaliDate.npYear)
                    Else
                        DateContent.Content = GetnepMonth(NepaliDate.npMonth)
                        DateContentYear.Content = NepaliDate.npYear
                    End If
                    NDate.Content = DateContent.Content & " " & DateContentYear.Content
                    EngDateContent.Content = GetEnglishMonth(NepaliDate)
                Else
                    Dim _date As Date
                    _date = New Date(EngDate.EngYear, EngDate.EngMonth, 1)
                    If MaxEDate < Val(_date.Year & Format(_date.Month, "00") & Format(_date.Day, "00")) Then
                        MsgBox("Date of out of Range", vbInformation)
                        If NepaliMonth = 1 Then
                            NepaliMonth = 12
                            NepaliYear = NepaliYear - 1
                        Else
                            NepaliMonth = NepaliMonth - 1
                        End If
                        Exit Sub
                    End If
                    loadEnglishdate(EngDate.EngDate, _date.DayOfWeek)
                    ' loadEnglishdate(EngDate.EngDate, CDate(EngDate.EngMonth & "/01/" & EngDate.EngYear).DayOfWeek)
                    EngDateContent.Content = GetNepaliMonth(EngDate)
                    DateContent.Content = MonthName(EngDate.EngMonth, False) ' Format(CDate(EngDate.EngMonth & "/01/2015"), "MMMM").ToString
                    NDate.Content = DateContent.Content & " " & DateContentYear.Content
                End If
                curNepaliDate = NepaliDate
                curEnglishDate = EngDate
            Catch ex As Exception
            End Try
                 
        End Sub

        Private Sub BtnDoubleForward_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles BtnDoubleForward.Click
            Dim EngDate As New EnglishDate
            Dim NepaliDate As New NepaliDate
            Try
                boolForward = True
                NepaliYear = NepaliYear + 1
                NepaliDate.npYear = NepaliYear
                NepaliDate.npMonth = NepaliMonth
                NepaliDate.npDay = NepaliDay
                NepaliDate.npDate = NepaliMonth & "/" & NepaliDay & "/" & NepaliYear
                If MaxNDate < Val(NepaliDate.npYear & Format(NepaliDate.npMonth, "00") & Format(NepaliDate.npDay, "00")) Then
                    MsgBox("Date of out of Range", vbInformation)
                    NepaliYear = NepaliYear - 1
                    Exit Sub
                End If
                EngDate = convert2English.GetEnglishDate(NepaliDate)
                If EngDate Is Nothing Then Exit Sub
                NepaliDate = convert2nepali.GetNepaliDate(EngDate.EngDate)
                j = (NepaliDate.DayOfWeek - NepaliDate.npDay Mod 7) + 1
                If j < 0 Then
                    j = 7 + j
                End If
                curNMonth = NepaliDate.npMonth
                curNyear = NepaliDate.npYear
                curEMonth = EngDate.EngMonth
                curEYear = EngDate.EngYear


                If setMode() = "N" Then
                    If MaxNDate < Val(NepaliDate.npYear & Format(NepaliDate.npMonth, "00") & Format(NepaliDate.npDay, "00")) Then
                        MsgBox("Date of out of Range", vbInformation)
                        NepaliYear = NepaliYear - 1
                        Exit Sub
                    End If
                    loaddate(NepaliDate, j)
                    FontSetting()
                    If flgPFont = True Then
                        DateContentYear.Content = covstr(NepaliDate.npYear)
                    Else
                        DateContentYear.Content = NepaliDate.npYear
                    End If
                    EngDateContent.Content = GetEnglishMonth(NepaliDate)
                    NDate.Content = DateContent.Content & " " & DateContentYear.Content
                Else
                    Dim _date As Date
                    _date = New Date(EngDate.EngYear, EngDate.EngMonth, 1)
                    If MaxEDate < Val(_date.Year & Format(_date.Month, "00") & Format(_date.Day, "00")) Then
                        MsgBox("Date of out of Range", vbInformation)
                        NepaliYear = NepaliYear - 1
                        Exit Sub
                    End If
                    loadEnglishdate(EngDate.EngDate, _date.DayOfWeek)
                    'loadEnglishdate(EngDate.EngDate, CDate(EngDate.EngMonth & "/01/" & EngDate.EngYear).DayOfWeek)
                    EngDateContent.Content = GetNepaliMonth(EngDate)
                    If flgPFont = True Then
                        DateContentYear.Content = EngDate.EngYear
                    Else
                        DateContentYear.Content = EngDate.EngYear
                    End If
                    NDate.Content = DateContent.Content & " " & DateContentYear.Content
                End If
                curNepaliDate = NepaliDate
                curEnglishDate = EngDate
            Catch ex As Exception
            End Try
        End Sub

        Private Sub btnDoubleBackWard_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnDoubleBackWard.Click
            Dim EngDate As New EnglishDate
            Dim NepaliDate As New NepaliDate
            Try
                boolForward = False
                NepaliYear = Val(NepaliYear) - 1
                NepaliDate.npYear = NepaliYear
                NepaliDate.npMonth = NepaliMonth
                NepaliDate.npDay = NepaliDay

                NepaliDate.npDate = NepaliMonth & "/" & NepaliDay & "/" & NepaliYear
                If MINNDate > Val(NepaliDate.npYear & Format(NepaliDate.npMonth, "00") & Format(NepaliDate.npDay, "00")) Then
                    MsgBox("Date of out of Range", vbInformation)
                    NepaliYear = NepaliYear + 1
                    Exit Sub
                End If
                EngDate = convert2English.GetEnglishDate(NepaliDate)
                NepaliDate = convert2nepali.GetNepaliDate(EngDate.EngDate)
                j = (NepaliDate.DayOfWeek - NepaliDate.npDay Mod 7) + 1
                If j < 0 Then
                    j = 7 + j
                End If
                curNMonth = NepaliDate.npMonth
                curNyear = NepaliDate.npYear
                curEMonth = EngDate.EngMonth
                curEYear = EngDate.EngYear


                If setMode() = "N" Then
                    If MINNDate > Val(NepaliDate.npYear & Format(NepaliDate.npMonth, "00") & Format(NepaliDate.npDay, "00")) Then
                        MsgBox("Date of out of Range", vbInformation)
                        NepaliYear = NepaliYear + 1
                        Exit Sub
                    End If
                    loaddate(NepaliDate, j)
                    FontSetting()
                    If flgPFont = True Then
                        DateContentYear.Content = covstr(NepaliDate.npYear)
                    Else
                        DateContentYear.Content = NepaliDate.npYear
                    End If
                    NDate.Content = DateContent.Content & " " & DateContentYear.Content
                    EngDateContent.Content = GetEnglishMonth(NepaliDate)
                Else
                    Dim _date As Date
                    _date = New Date(EngDate.EngYear, EngDate.EngMonth, 1)
                    If MINEDate > Val(_date.Year & Format(_date.Month, "00") & Format(_date.Day, "00")) Then
                        MsgBox("Date of out of Range", vbInformation)
                        NepaliYear = NepaliYear + 1
                        Exit Sub
                    End If
                    loadEnglishdate(EngDate.EngDate, _date.DayOfWeek)
                    'loadEnglishdate(EngDate.EngDate, CDate(EngDate.EngMonth & "/01/" & EngDate.EngYear).DayOfWeek)
                    EngDateContent.Content = GetNepaliMonth(EngDate)
                    If flgPFont = True Then
                        DateContentYear.Content = EngDate.EngYear
                    Else
                        DateContentYear.Content = EngDate.EngYear
                    End If
                    NDate.Content = DateContent.Content & " " & DateContentYear.Content
                End If
                curNepaliDate = NepaliDate
                curEnglishDate = EngDate
            Catch ex As Exception
            End Try
        End Sub

        Private Sub Show_CurDate()
            If English.IsChecked = True Then
                j = (Now.DayOfWeek - Now.Day Mod 7) + 1
                If j < 0 Then
                    j = 7 + j
                End If
                nStartDate = j
                loadEnglishdate(Now.Date, j)
                ' DateofToday.Content = "Today: " & Format(Now.Date, "MMMM").ToString & " " & Now.Day & ", " & Now.DayOfWeek.ToString & ", " & DateContentYear.Content
            Else
                j = (npdate.DayOfWeek - npdate.npDay Mod 7) + 1
                If j < 0 Then
                    j = 7 + j
                End If
                nStartDate = j
                loaddate(npdate, j)
                'If FontEnable.IsChecked = True Then
                '    DateofToday.Content = "cfh M " & nepMonth(npdate.npMonth) & " " & covstr(npdate.npDay) & " ut]" & ", " & getNepaliDay(CInt(Now.DayOfWeek)) & ", " & DateContentYear.Content
                'Else
                '    DateofToday.Content = "Today: " & GetnepMonth(npdate.npMonth) & " " & npdate.npDay & ", " & Now.DayOfWeek.ToString & ", " & DateContentYear.Content
                'End If
            End If
            curNepaliDate = npdate
            curEnglishDate = EnDate
            curNMonth = npdate.npMonth
            curNyear = npdate.npYear
            curEMonth = Now.Month
            curEYear = Now.Year
            NepaliYear = npdate.npYear
            NepaliMonth = npdate.npMonth
            NepaliDay = npdate.npDay
        End Sub

        Private Sub SetBtnLabel()
            If getFont() = "Preeti" Then
            Else

            End If

        End Sub


        Public Sub NepaliFontSetting()
            Dim btn As String
            Dim ebtn As String
            If setMode() = "E" Then
                For i As Int32 = 1 To 35
                    ebtn = "ebtn" & Val(i)
                    For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                        If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                            Dim DateButton As System.Windows.Controls.Button
                            DateButton = ctlr
                            If UCase(DateButton.Name) = UCase(ebtn) Then
                                ctlr.FontSize = 12
                                ctlr.FontFamily = New FontFamily("Preeti")
                            End If
                        End If
                    Next
                Next
                For i As Int32 = 1 To 35
                    btn = "btn" & Val(i)
                    For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                        If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                            Dim DateButton As System.Windows.Controls.Button
                            DateButton = ctlr
                            If UCase(DateButton.Name) = UCase(btn) Then
                                ctlr.FontSize = 12
                                ctlr.FontFamily = New FontFamily("Arial")
                                DateButton.Content = covstr(DateButton.Content)
                            End If
                        End If
                    Next
                Next
            Else
                For i As Int32 = 1 To 35
                    btn = "btn" & Val(i)
                    For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                        If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                            Dim DateButton As System.Windows.Controls.Button
                            DateButton = ctlr
                            If DateButton.Name = btn Then
                                ctlr.FontSize = 18
                                ctlr.FontFamily = New FontFamily("Preeti")
                            End If
                        End If
                    Next
                Next
                For i As Int32 = 1 To 35
                    ebtn = "ebtn" & Val(i)
                    For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                        If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                            Dim DateButton As System.Windows.Controls.Button
                            DateButton = ctlr
                            If UCase(DateButton.Name) = UCase(ebtn) Then
                                ctlr.FontSize = 8
                                ctlr.FontFamily = New FontFamily("Arial")
                            End If
                        End If
                    Next
                Next
            End If
            btnSunday.Content = "cfO{t"
            btnSunday.FontFamily = New FontFamily("Preeti")
            btnSunday.FontSize = 15

            btnMonday.Content = ";f]d"
            btnMonday.FontFamily = New FontFamily("Preeti")
            btnMonday.FontSize = 15

            btnTue.Content = "d+un"
            btnTue.FontFamily = New FontFamily("Preeti")
            btnTue.FontSize = 15

            btnWed.Content = "a'w"
            btnWed.FontFamily = New FontFamily("Preeti")
            btnWed.FontSize = 15

            btnThus.Content = "laxL"
            btnThus.FontFamily = New FontFamily("Preeti")
            btnThus.FontSize = 15

            btnFri.Content = "z" & """qm"
            btnFri.FontFamily = New FontFamily("Preeti")
            btnFri.FontSize = 15

            btnSaturday.Content = "zlg"
            btnSaturday.FontFamily = New FontFamily("Preeti")
            btnSaturday.FontSize = 15

            If setMode() = "E" Then
                For i As Int32 = 1 To 35
                    ebtn = "ebtn" & Val(i)
                    For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                        If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                            Dim DateButton As System.Windows.Controls.Button
                            DateButton = ctlr
                            If UCase(DateButton.Name) = UCase(ebtn) Then
                                If IsNumeric(DateButton.Content) Then
                                    DateButton.Content = covstr(DateButton.Content)
                                End If
                            End If
                        End If
                    Next
                Next
            Else
                For i As Int32 = 1 To 35
                    btn = "btn" & Val(i)
                    For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                        If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                            Dim DateButton As System.Windows.Controls.Button
                            DateButton = ctlr
                            If DateButton.Name = btn Then
                                If IsNumeric(DateButton.Content) Then
                                    DateButton.Content = covstr(DateButton.Content)
                                End If
                            End If
                        End If
                    Next
                Next
            End If
            FontSetting()
            If setMode() = "E" Then
                DateContentYear.Content = curEYear
                Dim _date As Date
                _date = New Date(Now.Year, curNMonth, 1)
                DateContent.Content = Format(_date, "MMMM").ToString
                'DateContent.Content = Format(CDate(curNMonth & "/01/2015"), "MMMM").ToString
                NDate.Content = DateContent.Content & " " & DateContentYear.Content
                DateofToday.Content = "Today: " & Format(Now.Date, "MMMM").ToString & " " & Now.Day & ", " & Now.DayOfWeek.ToString & ", " & DateContentYear.Content
                lM.Content = "Today: " & Format(Now.Date, "MMMM").ToString & " " & Now.Day & ", " & Now.DayOfWeek.ToString & ", " & DateContentYear.Content
                EngDateContent.Content = GetNepaliMonth(EnDate)
            Else
                NDate.FontFamily = New FontFamily("Preeti")
                NDate.FontSize = 24
                DateContentYear.Content = covstr(curNyear)
                DateContent.Content = nepMonth(curNMonth)
                NDate.Content = DateContent.Content & " " & DateContentYear.Content
                DateofToday.Content = "cfh M " & nepMonth(npdate.npMonth) & " " & covstr(npdate.npDay) & " ut]" & ", " & getNepaliDay(CInt(Now.DayOfWeek)) & ", " & DateContentYear.Content
                lblMenu.Content = "cfh M " & nepMonth(npdate.npMonth) & " " & covstr(npdate.npDay) & " ut]" & ", " & getNepaliDay(CInt(Now.DayOfWeek)) & ", " & DateContentYear.Content
            End If
            txtContent = Mid(lblMenu.Content, 7, Len(lblMenu.Content)) ' lblMenu.Content
            lblMenu.Content = txtContent
            txtEContent = Mid(lM.Content, 7, Len(lM.Content)) ' lblMenu.Content
            lM.Content = txtEContent
        End Sub

        Public Sub EnglishFontSetting()
            Dim ebtn As String
            Dim btn As String
            If setMode() = "E" Then
                For i As Int32 = 1 To 35
                    btn = "btn" & Val(i)
                    For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                        If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                            Dim DateButton As System.Windows.Controls.Button
                            DateButton = ctlr
                            If UCase(DateButton.Name) = UCase(btn) Then
                                ctlr.FontSize = 12
                                ctlr.FontFamily = New FontFamily("Arial")
                                DateButton.Content = covstr(DateButton.Content)
                            End If
                        End If
                    Next
                Next
            Else
                For i As Int32 = 1 To 35
                    btn = "btn" & Val(i)
                    For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                        If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                            Dim DateButton As System.Windows.Controls.Button
                            DateButton = ctlr
                            If UCase(DateButton.Name) = UCase(btn) Then
                                If IsNumeric(DateButton.Content) = False Then
                                    ctlr.FontSize = 12
                                    ctlr.FontFamily = New FontFamily("Arial")
                                    DateButton.Content = covstr(DateButton.Content)
                                End If
                            End If
                        End If
                    Next
                Next
            End If
            For i As Int32 = 1 To 35
                ebtn = "ebtn" & Val(i)
                For Each ctlr As System.Windows.Controls.Control In ButtonGrid.Children
                    If TypeOf (ctlr) Is System.Windows.Controls.Button Then
                        Dim DateButton As System.Windows.Controls.Button
                        DateButton = ctlr
                        If UCase(DateButton.Name) = UCase(ebtn) Then
                            ctlr.FontSize = 8
                            ctlr.FontFamily = New FontFamily("Arial")
                        End If
                    End If
                Next
            Next
            FontSetting()

            btnSunday.Content = "Sun"
            btnSunday.FontFamily = New FontFamily("Arial")
            btnSunday.FontSize = 11

            btnMonday.Content = "Mon"
            btnMonday.FontFamily = New FontFamily("Arial")
            btnMonday.FontSize = 11

            btnTue.Content = "Tues"
            btnTue.FontFamily = New FontFamily("Arial")
            btnTue.FontSize = 11

            btnWed.Content = "Wed"
            btnWed.FontFamily = New FontFamily("Arial")
            btnWed.FontSize = 11

            btnThus.Content = "Thus"
            btnThus.FontFamily = New FontFamily("Arial")
            btnThus.FontSize = 11

            btnFri.Content = "Fri"
            btnFri.FontFamily = New FontFamily("Arial")
            btnFri.FontSize = 11

            btnSaturday.Content = "Sat"
            btnSaturday.FontFamily = New FontFamily("Arial")
            btnSaturday.FontSize = 11

            If setMode() = "E" Then
                DateContentYear.Content = curEYear
                Dim _Date As Date
                _Date = New Date(Now.Year, curNMonth, 1)
                DateContent.Content = Format(_Date, "MMMM").ToString
                'DateContent.Content = Format(CDate(curEMonth & "/01/2015"), "MMMM").ToString
                EngDateContent.Content = GetNepaliMonth(EnDate)
            Else
                DateContentYear.Content = curNyear
                DateContent.Content = GetnepMonth(curNMonth)
                EngDateContent.Content = GetNepaliMonth(EnDate)
            End If


            NDate.FontFamily = New FontFamily("Arial")
            NDate.FontSize = 15
            NDate.Content = DateContent.Content & " " & DateContentYear.Content

            DateofToday.Content = "Today: " & Format(Now.Date, "MMMM").ToString & " " & Now.Day & ", " & Now.DayOfWeek.ToString & ", " & Now.Year
            lM.Content = "Today: " & Format(Now.Date, "MMMM").ToString & " " & Now.Day & ", " & Now.DayOfWeek.ToString & ", " & Now.Year
            txtEContent = Mid(lM.Content, 7, Len(lM.Content))
            lM.Content = txtEContent
        End Sub

        Private Sub PopUp(ByVal sender As Object, ByVal e As RoutedEventArgs)

            myContextMenu.IsOpen = True

        End Sub

        Private Sub CloseButton_Click(sender As Object, e As RoutedEventArgs)
            End
        End Sub
        Private Sub Font_Enable(sender As Object, e As RoutedEventArgs)
            If FontEnable.IsChecked = True Then
                FontEnable.IsChecked = False
                m_FontName = "Arial"
                getFont()
                flgPFont = False
                EnglishFontSetting()
            Else
                FontEnable.IsChecked = True
                m_FontName = "Preeti"
                getFont()
                flgPFont = True
                NepaliFontSetting()
            End If
            If setMode() = "E" Then
                Dim _date As Date
                _date = New Date(curEnglishDate.EngYear, curEnglishDate.EngMonth, curEnglishDate.EngDay)
                Dim j = (_date.DayOfWeek - _date.Day Mod 7) + 1
                If j < 0 Then
                    j = 7 + j
                End If
                loadEnglishdate(_date, j)

                '                loadEnglishdate(CDate(curEnglishDate.EngMonth & "/" & curEnglishDate.EngDay & "/" & curEnglishDate.EngYear), CDate(curEnglishDate.EngMonth & "/" & 1 & "/" & curEnglishDate.EngYear).DayOfWeek)
            Else
                loaddate(curNepaliDate, j)
            End If

        End Sub

        Private Sub Menubar_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
            If checkFont("Preeti") = True Then
                If getFont() = "Preeti" Then
                    FontEnable.IsChecked = True
                Else
                    FontEnable.IsChecked = False
                End If
            Else
                If flgPFont = False Then
                    FontEnable.IsEnabled = False
                End If
                FontEnable.IsChecked = False
            End If

            If setMode() = "E" Then
                Nepali.IsChecked = False
                English.IsChecked = True
            Else
                If setMode() = "" Then
                    C_Mode = "N"
                End If
                Nepali.IsChecked = True
                English.IsChecked = False
            End If
        End Sub
#End Region
        Private Sub English_Click(sender As Object, e As RoutedEventArgs) Handles English.Click
            Dim animflg As Boolean
            Dim anim As DoubleAnimation
            Dim anim1 As DoubleAnimation
            If Nepali.IsChecked = True AndAlso expand = True Then
                animflg = True
                dt.Stop()
            End If
            Nepali.IsChecked = False
            English.IsChecked = True
            C_Mode = "E"
            setMode()
            If flgPFont = True Then
                NepaliFontSetting()
            Else
                EnglishFontSetting()
            End If
            Dim _date As Date
            _date = New Date(curEnglishDate.EngYear, curEnglishDate.EngMonth, curEnglishDate.EngDay)
            Dim j = (_date.DayOfWeek - _date.Day Mod 7) + 1
            If j < 0 Then
                j = 7 + j
            End If
            loadEnglishdate(_date, j)
            If animflg = True Then
                dt.Start()
                anim = New Animation.DoubleAnimation(30, TimeSpan.FromSeconds(1))
                lM.BeginAnimation(Canvas.HeightProperty, anim)

                anim1 = New Animation.DoubleAnimation(100, TimeSpan.FromSeconds(0.4))
                lblMenu.BeginAnimation(Canvas.HeightProperty, anim1)
            End If
            lblMenu.Height = 0
            'loadEnglishdate(CDate(curEnglishDate.EngMonth & "/" & curEnglishDate.EngDay & "/" & curEnglishDate.EngYear), CDate(curEnglishDate.EngMonth & "/" & 1 & "/" & curEnglishDate.EngYear).DayOfWeek)
        End Sub

        Private Sub Nepali_Click(sender As Object, e As RoutedEventArgs) Handles Nepali.Click
            Dim anim As DoubleAnimation
            Dim anim1 As DoubleAnimation
            Dim animflg As Boolean
            If English.IsChecked = True AndAlso expand = True Then
                animflg = True
                dt.Stop()
            End If
            Nepali.IsChecked = True
            English.IsChecked = False

            C_Mode = "N"
            setMode()
            FontSetting()

            If flgPFont = True Then
                NepaliFontSetting()
            Else
                EnglishFontSetting()
            End If
            If curNepaliDate.npDaysInMonth = 0 Then
                Dim tempDate As EnglishDate
                tempDate = convert2English.GetEnglishDate(curNepaliDate)
                Dim _date As Date
                _date = New Date(tempDate.EngYear, tempDate.EngMonth, tempDate.EngDay)
                curNepaliDate = convert2nepali.GetNepaliDate(_date)
                '    curNepaliDate = convert2nepali.GetNepaliDate(CDate(tempDate.EngDate))
            End If
            j = (curNepaliDate.DayOfWeek - curNepaliDate.npDay Mod 7) + 1
            If j < 0 Then
                j = 7 + j
            End If
            loaddate(curNepaliDate, j)
            If animflg = True Then
                lM.Content = txtEContent & ", " & DateTime.Now.ToString("HH:mm:ss")
                dt.Start()
                anim = New Animation.DoubleAnimation(0, TimeSpan.FromSeconds(0.4))
                lM.BeginAnimation(Canvas.HeightProperty, anim)

                anim1 = New Animation.DoubleAnimation(30, TimeSpan.FromSeconds(1))
                lblMenu.BeginAnimation(Canvas.HeightProperty, anim1)
            End If
            lM.Height = 0
        End Sub

        Private Sub About_Click(sender As Object, e As RoutedEventArgs) Handles About.Click
            Try
                _frmAbout.Show()
                _frmAbout.Focus()
            Catch ex As Exception
                _frmAbout = New About
                _frmAbout.Show()
            End Try
        End Sub

        Private Sub Border_MouseDown(sender As Object, e As MouseButtonEventArgs)
            On Error Resume Next
            Me.DragMove()
        End Sub

        Private Sub lblMenu_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles lblMenu.MouseDown
            On Error Resume Next
            Me.DragMove()
        End Sub

        Private Sub lM_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles lM.MouseDown
            On Error Resume Next
            Me.DragMove()
        End Sub

        Private Sub Go_To(sender As Object, e As RoutedEventArgs)
            Try
                _frmGoto.Show()
                _frmGoto.Focus()
            Catch ex As Exception
                _frmGoto = New frmGoto
                _frmGoto.Show()
            End Try
        End Sub

        Private Sub ShowDateCov(sender As Object, e As RoutedEventArgs)
            Try
                _frmDateConveter.Show()
                _frmDateConveter.Focus()
            Catch ex As Exception
                _frmDateConveter = New frmDateConverter
                _frmDateConveter.Show()
            End Try
        End Sub

        Private Sub minButton_Show(sender As Object, e As RoutedEventArgs)
            Dim anim1 As Animation.DoubleAnimation
            anim1 = New Animation.DoubleAnimation(100, TimeSpan.FromSeconds(0.1))
            MinButton.BeginAnimation(Canvas.OpacityProperty, anim1)
        End Sub
        Private Sub lblMenu_LostFocus(sender As Object, e As RoutedEventArgs)
            Dim anim1 As Animation.DoubleAnimation
            If expand = True Then
                anim1 = New Animation.DoubleAnimation(0, TimeSpan.FromSeconds(0.1))
            Else
                anim1 = New Animation.DoubleAnimation(100, TimeSpan.FromSeconds(0.1))
            End If
            MinButton.BeginAnimation(Canvas.OpacityProperty, anim1)
        End Sub

        Private Sub menuButton_Show(sender As Object, e As RoutedEventArgs)
            Dim anim1 As Animation.DoubleAnimation
            anim1 = New Animation.DoubleAnimation(100, TimeSpan.FromSeconds(0.1))
            menuButton.BeginAnimation(Canvas.OpacityProperty, anim1)
        End Sub
        Private Sub menuButton_Hide(sender As Object, e As RoutedEventArgs)
            Dim anim1 As Animation.DoubleAnimation
            If expand = True Then
                anim1 = New Animation.DoubleAnimation(0, TimeSpan.FromSeconds(0.1))
            Else
                anim1 = New Animation.DoubleAnimation(100, TimeSpan.FromSeconds(0.1))
            End If
            menuButton.BeginAnimation(Canvas.OpacityProperty, anim1)
        End Sub

        Public Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
            'Dim anim1 As Animation.DoubleAnimation

            'If expand = True Then
            '    Count = Count + 1
            '    If Count > 60 Then
            '        anim1.From = -tbmarquee.ActualWidth
            '        anim1.To = Me.ActualWidth
            '        anim1.RepeatBehavior = RepeatBehavior.Forever
            '        anim1.Duration = New Duration(TimeSpan.Parse("0:0:20"))
            '        tbmarquee.BeginAnimation(Canvas.RightProperty, anim1)
            '        Count = 0
            '    End If
            'End If
        End Sub

        Private Sub MarqueTimer_Tick(sender As Object, e As EventArgs)
            lM.Content = txtEContent & ", " & DateTime.Now.ToString("HH:mm:ss")

            If flgPFont Then
                lblMenu.Content = txtContent & ", " & covstr(Format(Now.Hour, "00")) & "M" & covstr(Format(Now.Minute, "00")) & "M" & covstr(Format(Now.Second, "00"))
            Else
                lblMenu.Content = txtContent & ", " & DateTime.Now.ToString("HH:mm:ss")
            End If


            'If Len(lblMenu.Content) = Len(txtContent) Then
            '    intStop = intStop + 1
            'End If

            'startMarque = startMarque + 1
            'lblMenu.Content = Mid(txtContent, 1, startMarque)
            'If Len(lblMenu.Content) = Len(txtContent) Then
            '    startMarque = 0
            '    intStop = 0
            'End If
        End Sub

        Private Sub Hide_Detail(sender As Object, e As RoutedEventArgs)
            dt.Stop()
            Dim anim1 As Animation.DoubleAnimation
            Dim anim2 As Animation.DoubleAnimation
            Dim anim3 As Animation.DoubleAnimation
            Dim anim4 As Animation.DoubleAnimation
            Dim anim5 As Animation.DoubleAnimation
            Dim anim6 As Animation.DoubleAnimation
            If expand = False Then
                anim1 = New Animation.DoubleAnimation(0, TimeSpan.FromSeconds(0.2))
                anim2 = New Animation.DoubleAnimation(0, TimeSpan.FromSeconds(0.2))
                anim3 = New Animation.DoubleAnimation(35, TimeSpan.FromSeconds(0.2))
                anim4 = New Animation.DoubleAnimation(0, TimeSpan.FromSeconds(0.2))
                anim5 = New Animation.DoubleAnimation(0, TimeSpan.FromSeconds(0.1))
                anim6 = New Animation.DoubleAnimation(30, TimeSpan.FromSeconds(1.0))
                FontEnable.IsEnabled = False
                [GOTO].IsEnabled = False
                'English.IsEnabled = False
                'Nepali.IsEnabled = False
                expand = True
                m_Minimize = "True"
                Minimize()
                AddHandler dt.Tick, AddressOf MarqueTimer_Tick
                dt.Interval = New TimeSpan(0, 0, 0, 0, 200)
                dt.Start()
            Else
                anim1 = New Animation.DoubleAnimation(256, TimeSpan.FromSeconds(0.2))
                anim2 = New Animation.DoubleAnimation(256, TimeSpan.FromSeconds(0.1))
                anim3 = New Animation.DoubleAnimation(326, TimeSpan.FromSeconds(0.2))
                anim4 = New Animation.DoubleAnimation(35, TimeSpan.FromSeconds(0.7))
                anim5 = New Animation.DoubleAnimation(100, TimeSpan.FromSeconds(0.1))
                anim6 = New Animation.DoubleAnimation(0, TimeSpan.FromSeconds(0.1))
                FontEnable.IsEnabled = True
                [GOTO].IsEnabled = True
                'English.IsEnabled = True
                'Nepali.IsEnabled = True
                expand = False
                m_Minimize = "False"
                Minimize()
            End If
            GridToday.BeginAnimation(Canvas.HeightProperty, anim4)
            GridWeek.BeginAnimation(Canvas.HeightProperty, anim2)
            Bd.BeginAnimation(Canvas.HeightProperty, anim1)
            Me.BeginAnimation(Canvas.HeightProperty, anim3)
            menuButton.BeginAnimation(Canvas.OpacityProperty, anim5)
            If setMode() = "E" Then
                lM.BeginAnimation(Canvas.HeightProperty, anim6)
            Else
                lblMenu.BeginAnimation(Canvas.HeightProperty, anim6)
            End If
        End Sub

        Private Sub minButton_Show(sender As Object, e As System.Windows.Input.MouseEventArgs)
            Dim anim1 As Animation.DoubleAnimation
            anim1 = New Animation.DoubleAnimation(100, TimeSpan.FromSeconds(0.1))
            MinButton.BeginAnimation(Canvas.OpacityProperty, anim1)
        End Sub

        Private Sub lblMenu_LostFocus(sender As Object, e As System.Windows.Input.MouseEventArgs)
            Dim anim1 As Animation.DoubleAnimation
            If expand = True Then
                anim1 = New Animation.DoubleAnimation(0, TimeSpan.FromSeconds(0.1))
            Else
                anim1 = New Animation.DoubleAnimation(100, TimeSpan.FromSeconds(0.1))
            End If
            MinButton.BeginAnimation(Canvas.OpacityProperty, anim1)
        End Sub

        Private Sub MainWindow_PreviewKeyDown(sender As Object, e As Input.KeyEventArgs) Handles Me.PreviewKeyDown
            If Keyboard.IsKeyDown(Key.U) AndAlso Keyboard.IsKeyDown(Key.LeftCtrl) AndAlso Keyboard.IsKeyDown(Key.LeftShift) Then
                Register_Soft(False)
            End If
        End Sub


        Private Sub Mouse_Enter(sender As Object, e As Input.MouseEventArgs) Handles DateofToday.MouseEnter

        End Sub
    End Class
End Namespace