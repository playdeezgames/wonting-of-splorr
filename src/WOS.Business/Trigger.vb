Friend Class Trigger
    Implements ITrigger

    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer
    Private ReadOnly _kind As TriggerKind
    Private ReadOnly Property TriggerData As TriggerData
        Get
            Dim mapData = _data.Maps(_mapName)
            Return mapData.Cells(_row * mapData.Columns + _column).Triggers(_kind)
        End Get
    End Property

    Public Sub New(data As WorldData, mapName As String, column As Integer, row As Integer, kind As TriggerKind)
        Me._data = data
        Me._mapName = mapName
        Me._column = column
        Me._row = row
        Me._kind = kind
    End Sub

    Public Sub SetTeleport(toMapName As String, toColumn As Integer, toRow As Integer) Implements ITrigger.SetTeleport
        TriggerData.Teleport = New TeleportData With {.MapName = toMapName, .Column = toColumn, .Row = toRow}
    End Sub
End Class
