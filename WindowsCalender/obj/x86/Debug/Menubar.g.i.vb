﻿#ExternalChecksum("..\..\..\Menubar.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","F9D0466D0AD11565975BF35BB265CF3C")
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports System.Windows.Media.TextFormatting
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace WindowsCalender
    
    '''<summary>
    '''Menubar
    '''</summary>
    <Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
    Partial Public Class Menubar
        Inherits System.Windows.Controls.UserControl
        Implements System.Windows.Markup.IComponentConnector
        
        
        #ExternalSource("..\..\..\Menubar.xaml",38)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents menuButton As System.Windows.Controls.Button
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\..\Menubar.xaml",40)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents myContextMenu As System.Windows.Controls.ContextMenu
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\..\Menubar.xaml",48)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents FontEnable As System.Windows.Controls.MenuItem
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\..\Menubar.xaml",50)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents Nepali As System.Windows.Controls.MenuItem
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\..\Menubar.xaml",51)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents English As System.Windows.Controls.MenuItem
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\..\Menubar.xaml",53)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents About As System.Windows.Controls.MenuItem
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\..\Menubar.xaml",57)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents MinButton As System.Windows.Controls.Button
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\..\Menubar.xaml",58)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents CloseButton As System.Windows.Controls.Button
        
        #End ExternalSource
        
        Private _contentLoaded As Boolean
        
        '''<summary>
        '''InitializeComponent
        '''</summary>
        <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")>  _
        Public Sub InitializeComponent() Implements System.Windows.Markup.IComponentConnector.InitializeComponent
            If _contentLoaded Then
                Return
            End If
            _contentLoaded = true
            Dim resourceLocater As System.Uri = New System.Uri("/NepaliCalender;component/menubar.xaml", System.UriKind.Relative)
            
            #ExternalSource("..\..\..\Menubar.xaml",1)
            System.Windows.Application.LoadComponent(Me, resourceLocater)
            
            #End ExternalSource
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0"),  _
         System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
         System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes"),  _
         System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"),  _
         System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")>  _
        Sub System_Windows_Markup_IComponentConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IComponentConnector.Connect
            If (connectionId = 1) Then
                Me.menuButton = CType(target,System.Windows.Controls.Button)
                
                #ExternalSource("..\..\..\Menubar.xaml",38)
                AddHandler Me.menuButton.Click, New System.Windows.RoutedEventHandler(AddressOf Me.PopUp)
                
                #End ExternalSource
                Return
            End If
            If (connectionId = 2) Then
                Me.myContextMenu = CType(target,System.Windows.Controls.ContextMenu)
                Return
            End If
            If (connectionId = 3) Then
                Me.FontEnable = CType(target,System.Windows.Controls.MenuItem)
                
                #ExternalSource("..\..\..\Menubar.xaml",48)
                AddHandler Me.FontEnable.Click, New System.Windows.RoutedEventHandler(AddressOf Me.Font_Enable)
                
                #End ExternalSource
                Return
            End If
            If (connectionId = 4) Then
                Me.Nepali = CType(target,System.Windows.Controls.MenuItem)
                Return
            End If
            If (connectionId = 5) Then
                Me.English = CType(target,System.Windows.Controls.MenuItem)
                Return
            End If
            If (connectionId = 6) Then
                Me.About = CType(target,System.Windows.Controls.MenuItem)
                Return
            End If
            If (connectionId = 7) Then
                Me.MinButton = CType(target,System.Windows.Controls.Button)
                Return
            End If
            If (connectionId = 8) Then
                Me.CloseButton = CType(target,System.Windows.Controls.Button)
                
                #ExternalSource("..\..\..\Menubar.xaml",58)
                AddHandler Me.CloseButton.Click, New System.Windows.RoutedEventHandler(AddressOf Me.CloseButton_Click)
                
                #End ExternalSource
                Return
            End If
            Me._contentLoaded = true
        End Sub
    End Class
End Namespace

