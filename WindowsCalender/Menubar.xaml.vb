Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports WindowsCalender.WindowsCalender

Namespace WindowsCalender
    Partial Public Class Menubar
        Inherits UserControl
        Dim FontName As String
        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
        End Sub

        Private Sub PopUp(ByVal sender As Object, ByVal e As RoutedEventArgs)

            myContextMenu.IsOpen = True

        End Sub

        Private Sub CloseButton_Click(sender As Object, e As RoutedEventArgs)
            End
        End Sub
        Private Sub Font_Enable(sender As Object, e As RoutedEventArgs)
            If FontEnable.IsEnabled = True Then
                If FontEnable.IsChecked = True Then
                    FontEnable.IsChecked = False
                    m_FontName = "Arial"
                    getFont()
                Else
                    FontEnable.IsChecked = True
                    m_FontName = "Preeti"
                    getFont()
                End If
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



        Private Sub English_Click(sender As Object, e As RoutedEventArgs) Handles English.Click
            Nepali.IsChecked = False
            English.IsChecked = True
            C_Mode = "E"
            setMode()
        End Sub

        Private Sub Nepali_Click(sender As Object, e As RoutedEventArgs) Handles Nepali.Click
            Nepali.IsChecked = True
            English.IsChecked = False
            C_Mode = "N"
            setMode()
        End Sub

        Private Sub About_Click(sender As Object, e As RoutedEventArgs) Handles About.Click
            Dim frmAbout As New About
            frmAbout.Show()
        End Sub
    End Class
End Namespace
