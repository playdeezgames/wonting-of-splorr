Friend Class TradeFromItemInstance
    Implements IItemInstance

    Private ReadOnly _data As WorldData
    Private ReadOnly _shoppeName As String
    Private ReadOnly _index As Integer
    Private ReadOnly Property ItemInstanceData As ItemInstanceData
        Get
            Return _data.Shoppes(_shoppeName).Trades(_index).FromItem
        End Get
    End Property

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

    Public Sub New(data As WorldData, shoppeName As String, index As Integer)
        _data = data
        _shoppeName = shoppeName
        _index = index
    End Sub

    Public Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer) Implements IItemInstance.Render
        Item.Render(displayBuffer, x, y)
    End Sub
End Class
