Public Class frmMsgBox
    Public Function MsgBox(ByRef Promt As String, Optional ByRef Button As Microsoft.VisualBasic.MsgBoxStyle = MsgBoxStyle.OkOnly, Optional ByRef Title As String = Nothing) As Microsoft.VisualBasic.MsgBoxResult
        strMsg.Text = Promt
        msgTitle.Content = Title
    End Function
    Private Sub show_msg()
        MsgBox("Bonod", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Information, "new msg")
    End Sub

    Private Sub Me_Drag(sender As Object, e As MouseButtonEventArgs)
        On Error Resume Next
        Me.DragMove()
    End Sub
End Class
