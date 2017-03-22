
Namespace WindowsCalender
    Public Class frmGoto
        Dim convert2nepali As New Convert2NepaliDate
        Dim convert2English As New convert2English

        Private m_notifyIcon As System.Windows.Forms.NotifyIcon

        Public Sub New()
            InitializeComponent()

            ' Initialise code here
            'm_notifyIcon = New System.Windows.Forms.NotifyIcon()
            'm_notifyIcon.BalloonTipText = "The app has been minimised. Click the tray icon to show."
            'm_notifyIcon.BalloonTipTitle = "The App"
            'm_notifyIcon.Text = "The App"
            'm_notifyIcon.Icon = New System.Drawing.Icon("System-settings-icon.png")
            'AddHandler m_notifyIcon.Click, AddressOf Me.m_notifyIcon_Click
        End Sub

        Private m_storedWindowState As WindowState = Windows.WindowState.Normal

        Private Sub Window_StateChanged(sender As Object, e As EventArgs)
            If WindowState = WindowState.Minimized Then
                Me.Hide()
                If m_notifyIcon IsNot Nothing Then
                    m_notifyIcon.ShowBalloonTip(2000)
                End If
            Else
                m_storedWindowState = WindowState
            End If
        End Sub

        Private Sub m_notifyIcon_Click(sender As Object, e As EventArgs)
            Me.Show()
            Me.WindowState = m_storedWindowState
        End Sub

        Private Sub Window_IsVisibleChanged(sender As Object, e As DependencyPropertyChangedEventArgs)
            ShowTrayIcon(Not IsVisible)
        End Sub

        Private Sub ShowTrayIcon(show As Boolean)
            If m_notifyIcon IsNot Nothing And Me.WindowState = Windows.WindowState.Minimized Then
                m_notifyIcon.Visible = show
            End If
        End Sub
        Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
            Me.Close()
        End Sub

        Private Sub frmGoto_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
            If setMode() = "E" Then
                RBEng.IsChecked = True
            Else
                RBNEp.IsChecked = True
            End If
            loadCombo()
        End Sub

        Private Sub loadCombo()
            Dim _Date As Date
            Dim i As Integer
            CboMonth.Items.Clear()
            CboYear.Items.Clear()
            If RBEng.IsChecked = True Then
                For i = 1 To 12
                    _Date = New Date(Now.Year, i, 1)
                    CboMonth.Items.Add(Format(_Date, "MMMM").ToString)
                Next
                For i = 1950 To 2054
                    CboYear.Items.Add(i)
                Next
            Else
                For i = 1 To 12
                    CboMonth.Items.Add(GetnepMonth(i))
                Next

                For i = 2005 To 2110
                    CboYear.Items.Add(i)
                Next
            End If
            If RBNEp.IsChecked = True Then
                CboMonth.Text = GetnepMonth(npdate.npMonth)
                CboYear.Text = npdate.npYear
            Else
                CboMonth.Text = Format(Now.Date, "MMMM").ToString
                CboYear.Text = EnDate.EngYear
            End If
        End Sub
        Private Sub BtnLoad_Click(sender As Object, e As RoutedEventArgs) Handles BtnLoad.Click
            Dim sDay As Integer
            Dim _Date As Date
            Dim nDate As New NepaliDate
            Dim eDate As New EnglishDate
            Dim MWindow As MainWindow = Application.Current.Windows(0)
            If CboYear.Text <> "" And CboYear.Text <> "" Then

                If RBEng.IsChecked = True Then
                    eDate.EngMonth = CboMonth.SelectedIndex + 1
                    eDate.EngYear = CboYear.Text
                    If eDate.EngMonth = Now.Month And eDate.EngYear Then
                        eDate.EngDay = Now.Day
                    Else
                        eDate.EngDay = 1
                    End If
                    _Date = New Date(eDate.EngYear, eDate.EngMonth, eDate.EngDay)
                    If MaxEDate < Val(_Date.Year & Format(_Date.Month, "00") & Format(_Date.Day, "00")) Then
                        MsgBox("Date is out of range.", vbInformation)
                        Exit Sub
                    End If
                    If MINEDate > Val(_Date.Year & Format(_Date.Month, "00") & Format(_Date.Day, "00")) Then
                        MsgBox("Date is out of range.", vbInformation)
                        Exit Sub
                    End If
                    eDate.EngDate = _Date
                    curNepaliDate = convert2nepali.GetNepaliDate(_Date)
                    curEnglishDate = eDate
                    MainWindow.NepaliYear = curNepaliDate.npYear
                    MainWindow.NepaliMonth = curNepaliDate.npMonth
                    MainWindow.NepaliDay = curNepaliDate.npDay
                Else
                    nDate.npMonth = CboMonth.SelectedIndex + 1
                    nDate.npYear = CboYear.Text
                    nDate.npDay = npdate.npDay
                    If MaxNDate < Val(nDate.npYear & Format(Val(nDate.npMonth), "00") & Format(nDate.npDay, "00")) Then
                        MsgBox("Date is out of range.", vbInformation)
                        Exit Sub
                    End If
                    If MINNDate > Val(nDate.npYear & Format(Val(nDate.npMonth), "00") & Format(nDate.npDay, "00")) Then
                        MsgBox("Date is out of range.", vbInformation)
                        Exit Sub
                    End If
                    nDate.npDate = "01/" & Format(Val(nDate.npMonth), "00") & "/" & nDate.npYear
                    eDate = convert2English.GetEnglishDate(nDate)
                    _Date = New Date(eDate.EngYear, eDate.EngMonth, eDate.EngDay)
                    nDate = convert2nepali.GetNepaliDate(_Date)
                    sDay = (nDate.DayOfWeek - nDate.npDay Mod 7) + 1
                    curEnglishDate = convert2English.GetEnglishDate(nDate)
                    curNepaliDate = nDate
                    MainWindow.NepaliYear = curNepaliDate.npYear
                    MainWindow.NepaliMonth = curNepaliDate.npMonth
                    MainWindow.NepaliDay = curNepaliDate.npDay
                End If

                If setMode() = "E" Then
                    sDay = (_Date.DayOfWeek - _Date.Day Mod 7) + 1
                    If sDay < 0 Then
                        sDay = 7 + sDay
                    End If

                    MWindow.loadEnglishdate(eDate.EngDate, sDay)
                Else
                    nDate = convert2nepali.GetNepaliDate(_Date)
                    sDay = (nDate.DayOfWeek - nDate.npDay Mod 7) + 1
                    If sDay < 0 Then
                        sDay = 7 + sDay
                    End If
                    MWindow.loaddate(nDate, sDay)
                End If

                Me.Close()
            Else
                If CboYear.Text = "" Then
                    CboYear.Focus()
                    Exit Sub
                End If

                If CboMonth.Text = "" Then
                    CboMonth.Focus()
                    Exit Sub
                End If
            End If
        End Sub

        Private Sub CboYear_KeyDown(sender As Object, e As KeyEventArgs) Handles CboYear.KeyDown
            If e.Key = Key.Space Then
                sender.Equals({"Key.F4"})
            End If
        End Sub

        Private Sub chkAD(sender As Object, e As RoutedEventArgs)
            loadCombo()
        End Sub

        Private Sub chkBS(sender As Object, e As RoutedEventArgs)
            loadCombo()
        End Sub

        Private Sub Me_Drag(sender As Object, e As MouseButtonEventArgs)
            On Error Resume Next
            Me.DragMove()
        End Sub

        Private Sub CboYear_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles CboYear.SelectionChanged

        End Sub
    End Class

End Namespace