Public Class FileSrcOut
    Implements ISrcOut

    Dim str As New System.Text.StringBuilder()

    Public Sub WriteLn(line As String, tab As Integer) Implements ISrcOut.WriteLn

        str.Append(vbTab, tab)

        str.Append(line)
        str.Append(vbCrLf)



    End Sub

    Public Sub Save(file As String)
		System.IO.File.WriteAllText(file, str.ToString())
	End Sub

End Class
