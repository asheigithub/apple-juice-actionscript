Public Class ConSrcOut
    Implements ISrcOut

    Public Sub WriteLn(line As String, tab As Integer) Implements ISrcOut.WriteLn
        Dim str As New System.Text.StringBuilder()
        str.Append(vbTab, tab)

        str.Append(line)
        str.Append(vbCrLf)

        Console.Write(str.ToString())

    End Sub
End Class
