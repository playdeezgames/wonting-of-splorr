Friend Class MapCellItemInstance
    Implements IItemInstance

    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer
    Private ReadOnly Property MapData As MapData
        Get
            Return _data.Maps(_mapName)
        End Get
    End Property
    Private ReadOnly Property MapCellData As MapCellData
        Get
            Return MapData.Cells(_column + _row * MapData.Columns)
        End Get
    End Property
    Private ReadOnly Property ItemInstanceData As ItemInstanceData
        Get
            Return MapCellData.Item
        End Get
    End Property

    Public Sub New(data As WorldData, mapName As String, column As Integer, row As Integer)
        _data = data
        _mapName = mapName
        _column = column
        _row = row
    End Sub

    Public Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer) Implements IItemInstance.Render
        Item.Render(displayBuffer, x, y)
    End Sub

    Public ReadOnly Property Item As IItem Implements IItemInstance.Item
        Get
            Return New Item(_data, ItemInstanceData.ItemName)
        End Get
    End Property

    Public ReadOnly Property Quantity As Integer Implements IItemInstance.Quantity
        Get
            Return ItemInstanceData.Quantity
        End Get
    End Property
End Class
