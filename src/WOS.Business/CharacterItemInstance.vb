Friend Class CharacterItemInstance
    Implements IItemInstance

    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer
    Private ReadOnly _index As Integer
    Private ReadOnly Property MapData As MapData
        Get
            Return _data.Maps(_mapName)
        End Get
    End Property
    Private ReadOnly Property ItemInstanceData As ItemInstanceData
        Get
            Return MapData.Cells(_column + _row * MapData.Columns).Character.Items(_index)
        End Get
    End Property

    Public ReadOnly Property CanUse As Boolean Implements IItemInstance.CanUse
        Get
            Return Item.CanUse
        End Get
    End Property

    Public Sub New(data As WorldData, mapName As String, column As Integer, row As Integer, index As Integer)
        _data = data
        _mapName = mapName
        _column = column
        _row = row
        _index = index
    End Sub

    Public ReadOnly Property Item As IItem Implements IItemInstance.Item
        Get
            Return New Item(_data, ItemInstanceData.ItemName)
        End Get
    End Property

    Public Property Quantity As Integer Implements IItemInstance.Quantity
        Get
            Return ItemInstanceData.Quantity
        End Get
        Set(value As Integer)
            ItemInstanceData.Quantity = Math.Max(value, 0)
        End Set
    End Property

    Public Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer) Implements IItemInstance.Render
        Item.Render(displayBuffer, x, y)
    End Sub
End Class
