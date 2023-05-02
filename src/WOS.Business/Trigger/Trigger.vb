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
        _data = data
        _mapName = mapName
        _column = column
        _row = row
        _kind = kind
    End Sub

    Public Sub SetTeleport(toMapName As String, toColumn As Integer, toRow As Integer) Implements ITrigger.SetTeleport
        TriggerData.Teleport = New TeleportData With {.MapName = toMapName, .Column = toColumn, .Row = toRow}
    End Sub

    Public Sub SetMessage(lines As IEnumerable(Of (Hue, String))) Implements ITrigger.SetMessage
        TriggerData.Message = New MessageData With {
            .MessageLines = lines.Select(Function(line) New MessageLineData With
            {
                .Hue = line.Item1,
                .Text = line.Item2
            }).ToList}
    End Sub

    Public Sub SetShoppe(shoppeName As String) Implements ITrigger.SetShoppe
        TriggerData.ShoppeName = shoppeName
    End Sub
End Class
