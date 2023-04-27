Public Class MapData
    Public Sub New()
        'nothing
    End Sub
    Public Sub New(columns As Integer, rows As Integer, terrainName As String)
        Me.Columns = columns
        Me.Rows = rows
        While Cells.Count < columns * rows
            Cells.Add(New MapCellData With {.TerrainName = terrainName})
        End While
    End Sub
    Public Property Columns As Integer
    Public Property Rows As Integer
    Public Property Cells As New List(Of MapCellData)
End Class
