Friend Class Map
    Implements IMap
    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly Property MapData As MapData
        Get
            Return _data.Maps(_mapName)
        End Get
    End Property

    Public Sub New(data As WorldData, mapName As String)
        _data = data
        _mapName = mapName
    End Sub

    Public ReadOnly Property Columns As Integer Implements IMap.Columns
        Get
            Return MapData.Columns
        End Get
    End Property

    Public ReadOnly Property Rows As Integer Implements IMap.Rows
        Get
            Return MapData.Rows
        End Get
    End Property

    Public Function GetCell(column As Integer, row As Integer) As IMapCell Implements IMap.GetCell
        Return New MapCell(_data, _mapName, column, row)
    End Function
End Class
