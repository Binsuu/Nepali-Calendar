Imports System
Imports System.Windows
Imports System.Windows.Media.Animation
Imports System.Windows.Controls
Imports System.Windows.Threading

Namespace WindowsCalender
    Public Class About
        Dim expand As Boolean = False
        Dim count As Integer
        Dim dt As DispatcherTimer = New DispatcherTimer()

        Private Sub About_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
            'Dim WorkingHeight As Double
            'Dim WorkingRight As Double
            'Dim workingBottom As Double
            'WorkingHeight = SystemParameters.WorkArea.Height
            'WorkingRight = SystemParameters.WorkArea.Right
            'workingBottom = SystemParameters.WorkArea.Bottom
            'Me.Top = workingBottom - Me.Height
            'Me.Left = WorkingRight - Me.Width
            AddHandler dt.Tick, AddressOf dispatcherTimer_Tick
            dt.Interval = New TimeSpan(0, 0, 1)
            If expand = False Then
                ContentPresenterLabel.Content = 6
            Else
                ContentPresenterLabel.Content = 5
            End If
        End Sub

        Public Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
            If expand = True Then
                count = count + 1
                If count > 20 Then
                    Button_Click_1(Nothing, Nothing)
                    count = 0
                    dt.Stop()
                End If
            End If
        End Sub
        Private Sub Animate(ByVal column As ColumnDefinition)

            Dim storyboard As Storyboard
            storyboard = New Storyboard()

            Dim duration As Duration
            duration = New Duration(TimeSpan.FromMilliseconds(500))

            Dim animation As DoubleAnimation
            animation = New DoubleAnimation()

            animation.Duration = duration
            storyboard.Children.Add(animation)
            animation.From = 1000
            animation.To = 0
            'animation.EnableDependentAnimation = True
            storyboard.SetTarget(animation, column)
            'storyboard.SetTarget()
            'storyboard.SetTargetProperty(animation, ColumnDefinition.MaxWidth)

            storyboard.Begin()
        End Sub
        Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
            Me.Close()
        End Sub

        Private Sub Border_MouseDown(sender As Object, e As MouseButtonEventArgs)
            Me.DragMove()
        End Sub

        Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
            Dim anim1 As Animation.DoubleAnimation
            Dim anim2 As Animation.DoubleAnimation
            Dim anim3 As Animation.DoubleAnimation
            If expand = False Then
                anim1 = New Animation.DoubleAnimation(100, TimeSpan.FromSeconds(0.2))
                anim2 = New Animation.DoubleAnimation(340, TimeSpan.FromSeconds(0.2))
                anim3 = New Animation.DoubleAnimation(0, 90, TimeSpan.FromSeconds(0.2))
                ContentPresenterLabel.Content = 5
                dt.Start()
                expand = True
            Else
                anim1 = New Animation.DoubleAnimation(0, TimeSpan.FromSeconds(0.2))
                anim2 = New Animation.DoubleAnimation(240, TimeSpan.FromSeconds(0.2))
                anim3 = New Animation.DoubleAnimation(90, 0, TimeSpan.FromSeconds(0.2))
                ContentPresenterLabel.Content = 6
                dt.Stop()
                expand = False
            End If
            ContentPresenterLabel.BeginAnimation(RotateTransform.AngleProperty, anim3)
            Me.BeginAnimation(Canvas.HeightProperty, anim2)
            myDetial.BeginAnimation(Canvas.HeightProperty, anim1)
        End Sub

        Public Sub AnimateGridRowExpandCollapse(ByVal gridRow As RowDefinition, expand As Boolean, expandedHeight As Double, collapsedHeight As Double, minHeight As Double, seconds As Integer, milliseconds As Integer)
            If expand And gridRow.ActualHeight >= expandedHeight Then Return
            If Not expand And gridRow.ActualHeight = collapsedHeight Then Return

            Dim storyBoard As New Storyboard

            Dim animation As New Animation.DoubleAnimation
            animation.From = gridRow.ActualHeight
            animation.To = IIf(expand, expandedHeight, collapsedHeight)
            animation.Duration = New TimeSpan(0, 0, 0, seconds, milliseconds)

            ' // Set delegate that will fire on completioon.
            gridRow.BeginAnimation(RowDefinition.HeightProperty, Nothing)

            '  // Set the final height.
            gridRow.Height = New GridLength(IIf(expand, expandedHeight, collapsedHeight))

            ' // Set the minimum height.
            gridRow.MinHeight = minHeight

            storyBoard.Children.Add(animation)

            storyBoard.SetTarget(animation, gridRow)
            storyBoard.SetTargetProperty(animation, New PropertyPath(RowDefinition.HeightProperty))
            storyBoard.Children.Add(animation)

            '// Begin the animation.
            storyBoard.Begin()
        End Sub

    End Class
End Namespace