Imports System.Text
Imports System.Drawing.Text
Imports System.Drawing
Imports WindowsCalender.WindowsCalender

Module MainModule
    Public m_FontName As String
    Public flgPFont As Boolean
    Public C_Mode As String
    Public curNepaliDate As NepaliDate
    Public curEnglishDate As EnglishDate
    Public EnDate As New EnglishDate
    Public npdate As New NepaliDate
    Public m_Minimize As String
    Public _frmAbout As About
    Public _frmDateConveter As frmDateConverter
    Public _frmGoto As frmGoto

    Public MaxEDate As Integer = "20541231"
    Public MaxNDate As Integer = "21101230"
    Public MINEDate As Integer = "19490101"
    Public MINNDate As Integer = "20050101"
    Public Sub Register_Soft(ByVal bool As Boolean)

        ' The following code is a rendition of one provided by
        ' Firestarter_75, so he gets the credit here:

        Dim applicationName As String = Windows.Application.ResourceAssembly.GetName().Name
        Dim applicationPath As String = Windows.Application.ResourceAssembly().Location

        If bool = True Then
            Dim regKey As Microsoft.Win32.RegistryKey
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
            regKey.SetValue(applicationName, """" & applicationPath & """")
            regKey.Close()
        Else
            Dim regKey As Microsoft.Win32.RegistryKey
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
            regKey.DeleteValue(applicationName, False)
            regKey.Close()
        End If
    End Sub
    Public Function GetnepMonth(ByVal npMonth As Integer) As String
        Select Case npMonth
            Case 1
                Return "Baishak"
            Case 2
                Return "Jesth"
            Case 3
                Return "Ashad"
            Case 4
                Return "Shrawan"
            Case 5
                Return "Bhadra"
            Case 6
                Return "Ashwin"
            Case 7
                Return "Kartik"
            Case 8
                Return "Mangsir"
            Case 9
                Return "Poush"
            Case 10
                Return "Magh"
            Case 11
                Return "Falgun"
            Case 12
                Return "Chaitra"
            Case Else
                Return "0"
                Throw New ArgumentOutOfRangeException(npMonth.ToString, "Invalid Month!")
        End Select
    End Function
    Public Function checkFont(ByVal FontName As String) As Boolean
        Try
            Dim ff As New InstalledFontCollection
            Dim fontfamilies() As FontFamily = ff.Families()
            For Each fontfamily As FontFamily In fontfamilies
                If UCase(FontName.ToString) = UCase(fontfamily.Name.ToString) Then
                    Return True
                End If
            Next
            Return False
        Catch
            Return False
        End Try

    End Function

    Public Function getNepaliDay(ByVal nepDay As Integer) As String
        Select Case nepDay
            Case 0
                Return "cfO{taf/"
            Case 1
                Return ";f]daf/"
            Case 2
                Return "d+unaf/"
            Case 3
                Return "a'waf/"
            Case 4
                Return "laxLaf/"
            Case 5
                Return "z'qmaf/"
            Case 6
                Return "zlgaf/"
            Case Else
                Return Nothing
        End Select
    End Function

    'Sub main()
    '    FontSetting()
    'End Sub
    Public Function getFont() As String
        On Error Resume Next
        Dim FileName As String
        Dim FontName As String
        FontName = ""
        FileName = System.AppDomain.CurrentDomain.BaseDirectory & "fsysFile.ini"
        If checkFont("Preeti") = False Then GoTo NXT
        Dim fs As IO.FileStream
        If IO.File.Exists(FileName) Then
            Dim readFile As New IO.StreamReader(FileName, True)
            FontName = readFile.ReadLine
            readFile.Close()
            If m_FontName <> FontName Then
                IO.File.Delete(FileName)
                GoTo NXT
            End If
            FileName = System.AppDomain.CurrentDomain.BaseDirectory & "fsysFile.ini"
            IO.File.SetAttributes(FileName, IO.FileAttributes.Hidden)
            Return FontName
        Else
NXT:
            fs = IO.File.Create(FileName)
            fs.Close()
            Dim File As New IO.StreamWriter(FileName, True)
            If m_FontName = "" Then
                m_FontName = FontName
            End If
            File.WriteLine(m_FontName)
            File.Close()
            IO.File.SetAttributes(FileName, IO.FileAttributes.Hidden)
            Return m_FontName
        End If
    End Function

    Public Function Minimize() As Boolean
        On Error Resume Next
        Dim FileName As String
        Dim minForm As String
        minForm = ""
        FileName = System.AppDomain.CurrentDomain.BaseDirectory & "fsysMin.ini"
        Dim fs As IO.FileStream
        If IO.File.Exists(FileName) Then
            Dim readFile As New IO.StreamReader(FileName, True)
            minForm = readFile.ReadLine
            readFile.Close()
            If m_Minimize <> minForm Or m_Minimize <> Nothing Then
                IO.File.Delete(FileName)
                GoTo NXT
            End If
            IO.File.SetAttributes(FileName, IO.FileAttributes.Hidden)
            If minForm = "" Then
                Return False
            Else
                Return minForm
            End If
        Else
NXT:
            fs = IO.File.Create(FileName)
            fs.Close()
            Dim File As New IO.StreamWriter(FileName, True)
            'If m_Minimize <> minForm Then
            '    m_Minimize = minForm
            'End If
            File.WriteLine(m_Minimize)
            File.Close()
            IO.File.SetAttributes(FileName, IO.FileAttributes.Hidden)
            If minForm = "" Then
                Return False
            Else
                Return minForm
            End If
        End If
    End Function
    Public Function setMode() As String
        On Error Resume Next
        Dim FileName As String
        Dim CMode As String
        CMode = ""
        FileName = System.AppDomain.CurrentDomain.BaseDirectory & "msysFile.ini"
        Dim fs As IO.FileStream
        If IO.File.Exists(FileName) Then
            Dim readFile As New IO.StreamReader(FileName, True)
            CMode = readFile.ReadLine
            readFile.Close()
            If C_Mode <> CMode And C_Mode <> "" Then
                IO.File.Delete(FileName)
                GoTo NXT
            End If
            IO.File.SetAttributes(FileName, IO.FileAttributes.Hidden)
            Return CMode
        Else
NXT:
            fs = IO.File.Create(FileName)
            fs.Close()
            Dim File As New IO.StreamWriter(FileName, True)
            If C_Mode = "" Then
                C_Mode = CMode
            End If
            File.WriteLine(C_Mode)
            File.Close()
            IO.File.SetAttributes(FileName, IO.FileAttributes.Hidden)
            Return C_Mode
        End If
    End Function
End Module

