Public NotInheritable Class AboutBox1


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) 
        Process.Start("https://www.facebook.com/xBlueDragon")
    End Sub

    Private Sub AboutBox1_LostFocus(sender As Object, e As System.EventArgs) Handles Me.LostFocus
        Me.Close()
    End Sub
End Class
