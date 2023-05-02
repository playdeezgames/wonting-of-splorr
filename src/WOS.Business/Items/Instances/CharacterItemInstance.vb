Friend Class CharacterItemInstance
    Inherits BaseItemInstance

    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer
    Private ReadOnly _index As Integer
    Private ReadOnly Property MapData As MapData
        Get
            Return _data.Maps(_mapName)
        End Get
    End Property

    Protected Overrides ReadOnly Property ItemInstanceData As ItemInstanceData
        Get
            Return MapData.Cells(_column + _row * MapData.Columns).Character.Items(_index)
        End Get
    End Property

    Public Sub New(data As WorldData, mapName As String, column As Integer, row As Integer, index As Integer)
        MyBase.New(data)
        _mapName = mapName
        _column = column
        _row = row
        _index = index
    End Sub
End Class
