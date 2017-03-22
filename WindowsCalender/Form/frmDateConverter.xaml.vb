
Namespace WindowsCalender
    Public Class frmDateConverter
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
            Dim i As Integer
            Dim _date As Date
            CboMonth.Items.Clear()
            CboYear.Items.Clear()
            cboDateFormat.Items.Clear()
            If RBEng.IsChecked = True Then
                For i = 1 To 12
                    _date = New Date(Now.Year, i, 1)
                    CboMonth.Items.Add(Format(_date, "MMMM").ToString)
                Next
                For i = 1943 To 2023
                    CboYear.Items.Add(i)
                Next
            Else
                For i = 1 To 12
                    CboMonth.Items.Add(GetnepMonth(i))
                Next

                For i = 2005 To 2079
                    CboYear.Items.Add(i)
                Next
            End If
            cboDateFormat.Items.Add("MM/dd/yyyy")
            cboDateFormat.Items.Add("dd/MM/yyyy")
            cboDateFormat.Items.Add("yyyy-MM-dd")
            cboDateFormat.Items.Add("yyyy-dd-MM")
            cboDateFormat.Items.Add("dddd, MMMM d, yyyy")
            cboDateFormat.Items.Add("MMMM d, yyyy")
            cboDateFormat.Items.Add("dddd, d MMMM, yyyy")
            cboDateFormat.Items.Add("d MMMM, yyyy")
            If RBNEp.IsChecked = True Then
                CboMonth.Text = GetnepMonth(npdate.npMonth)
                CboYear.Text = npdate.npYear
            Else
                CboMonth.Text = Format(Now.Date, "MMMM").ToString
                CboYear.Text = EnDate.EngYear
            End If
            cboDateFormat.SelectedIndex = 0
            txtBlockDay.Text = "01"
            txtConvert.Text = ""
        End Sub
        Private Sub BtnLoad_Click(sender As Object, e As RoutedEventArgs) Handles BtnLoad.Click
            Try
                Dim _date As Date
                Dim nDate As New NepaliDate
                Dim eDate As New EnglishDate
                Dim MWindow As MainWindow = Application.Current.Windows(0)
                If CboYear.Text <> "" And CboYear.Text <> "" And Val(txtBlockDay.Text) <> 0 Then
                    If RBEng.IsChecked = True Then
                        If Val(Date.DaysInMonth(CboYear.Text, CboMonth.SelectedIndex + 1)) < Val(txtBlockDay.Text) Then
                            MsgBox("Invalid Date!!", vbInformation)
                            txtBlockDay.Focus()
                            Exit Sub
                        End If
                        eDate.EngMonth = CboMonth.SelectedIndex + 1
                        eDate.EngYear = CboYear.Text
                        eDate.EngDay = Val(txtBlockDay.Text)
                        eDate.EngDate = New Date(Val(eDate.EngYear), Val(eDate.EngMonth), Val(txtBlockDay.Text))
                        _date = New Date(eDate.EngYear, eDate.EngMonth, eDate.EngDay)
                        nDate = convert2nepali.GetNepaliDate(_date)
                        txtConvert.Text = NFormat(nDate, cboDateFormat.Text)
                    Else
                        nDate.npMonth = CboMonth.SelectedIndex + 1
                        nDate.npYear = CboYear.Text
                        nDate.npDay = Val(txtBlockDay.Text)
                        nDate.npDate = nDate.npDay & "/" & Format(Val(nDate.npMonth), "00") & "/" & nDate.npYear
                        eDate = convert2English.GetEnglishDate(nDate)
                        _date = New Date(eDate.EngYear, eDate.EngMonth, eDate.EngDay)
                        If IsDate(_date) = False Then
                            MsgBox("Not Valid Date!!", vbInformation)
                            txtBlockDay.Focus()
                            txtConvert.Text = ""
                            Exit Sub
                        End If
                        nDate = convert2nepali.GetNepaliDate(_date)
                        If nDate.npYear = CboYear.Text And nDate.npMonth = CboMonth.SelectedIndex + 1 Then
                            If Val(nDate.npDaysInMonth) < Val(txtBlockDay.Text) Then
                                MsgBox("Invalid Date!!", vbInformation)
                                txtBlockDay.Focus()
                                Exit Sub
                            End If
                        Else
                            MsgBox("Invalid Date!!", vbInformation)
                            txtBlockDay.Focus()
                            Exit Sub
                        End If
                        txtConvert.Text = DateFormat(New Date(eDate.EngYear, eDate.EngMonth, eDate.EngDay))
                    End If
                Else
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub

        Private Function DateFormat(ByVal _Date As Date) As String
            Dim eFlag As Boolean
            Dim tempDate As String
            If RBEng.IsChecked = True Then
                eFlag = True
            Else
                eFlag = False
            End If
            Select Case cboDateFormat.SelectedIndex
                Case 0
                    tempDate = Format(_Date, "MM/dd/yyyy")
                    Return Replace(tempDate, "-", "/")
                    'Return Format(_Date, "MM/dd/yyyy")
                Case 1
                    tempDate = Format(_Date, "dd/MM/yyyy")
                    Return Replace(tempDate, "-", "/")
                    'Return Format(_Date, "dd/MM/yyyy")
                Case 2
                    tempDate = Format(_Date, "yyyy-MM-dd")
                    Return Replace(tempDate, "/", "-")
                    'Return Format(_Date, "yyyy-MM-dd")
                Case 3
                    tempDate = Format(_Date, "yyyy-dd-MM")
                    Return Replace(tempDate, "/", "-")
                    'Return Format(_Date, "yyyy-dd-MM")
                Case 4
                    Return Format(_Date, "dddd, MMMM d, yyyy")
                Case 5
                    Return Format(_Date, "MMMM d, yyyy")
                Case 6
                    Return Format(_Date, "dddd, d MMMM, yyyy")
                Case 7
                    Return Format(_Date, "d MMMM, yyyy")
                Case Else
                    Return Format(_Date, "MM/dd/yyyy")
            End Select
        End Function

        Private Function NFormat(ByVal nepDate As NepaliDate, ByVal strFormat As String) As String
            Dim e_Date As EnglishDate
            Dim _Date As Date
            e_Date = convert2English.GetEnglishDate(nepDate)
            _Date = New Date(e_Date.EngYear, e_Date.EngMonth, e_Date.EngDay)
            Select Case strFormat
                Case "MM/dd/yyyy"
                    Return nepDate.npMonth & "/" & nepDate.npDay & "/" & nepDate.npYear
                Case "dd/MM/yyyy"
                    Return nepDate.npDay & "/" & nepDate.npMonth & "/" & nepDate.npYear
                Case "yyyy-MM-dd"
                    Return nepDate.npYear & "-" & nepDate.npMonth & "-" & nepDate.npDay
                Case "yyyy-dd-MM"
                    Return nepDate.npYear & "-" & nepDate.npDay & "-" & nepDate.npMonth
                Case "dddd, MMMM d, yyyy"
                    Return _Date.DayOfWeek.ToString & ", " & GetnepMonth(nepDate.npMonth) & " " & nepDate.npDay & ", " & nepDate.npYear
                Case "MMMM d, yyyy"
                    Return GetnepMonth(nepDate.npMonth) & " " & nepDate.npDay & ", " & nepDate.npYear
                Case "dddd, d MMMM, yyyy"
                    Return _Date.DayOfWeek.ToString & ", " & nepDate.npDay & " " & GetnepMonth(nepDate.npMonth) & ", " & nepDate.npYear
                Case "d MMMM, yyyy"
                    Return nepDate.npDay & " " & GetnepMonth(nepDate.npMonth) & ", " & nepDate.npYear
                Case Else
                    Return nepDate.npMonth & "/" & nepDate.npDay & "/" & nepDate.npYear
            End Select
        End Function

        Private Function Day(ByVal i As Integer) As String
            Select Case i
                Case 0
                    day = "Sunday"
                Case 1
                    day = "Monday"
                Case 2
                    day = "Tuesday"
                Case 3
                    day = "Wednesday"
                Case 4
                    day = "Thursday"
                Case 5
                    day = "Friday"
                Case 6
                    day = "Saturday"
                Case Else
                    day = "Apocalypse: we're all boned."
            End Select
        End Function
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

        Private Sub txtBlockDay_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtBlockDay.PreviewTextInput
            Try
                Convert.ToInt32(e.Text)
            Catch ex As Exception
                e.Handled = True
            End Try
        End Sub
    End Class

End Namespace