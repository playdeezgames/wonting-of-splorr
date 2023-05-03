Friend Class Trigger
    Inherits BaseTrigger

    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer
    Private ReadOnly _kind As TriggerKind

    Public Sub New(data As WorldData, mapName As String, column As Integer, row As Integer, kind As TriggerKind)
        _data = data
        _mapName = mapName
        _column = column
        _row = row
        _kind = kind
    End Sub

    Protected Overrides ReadOnly Property TriggerData As TriggerData
        Get
            Dim mapData = _data.Maps(_mapName)
            Return mapData.Cells(_row * mapData.Columns + _column).Triggers(_kind)
        End Get
    End Property
End Class
