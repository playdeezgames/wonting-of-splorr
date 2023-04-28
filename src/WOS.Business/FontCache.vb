Friend Module FontCache
    Private ReadOnly _cache As New Dictionary(Of String, Font)
    Friend Sub SetCachedFont(fontName As String, font As Font)
        _cache(fontName) = font
    End Sub
    Friend Function GetCachedFont(fontName As String) As Font
        Return _cache(fontName)
    End Function
End Module
