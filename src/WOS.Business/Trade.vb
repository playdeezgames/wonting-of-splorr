Friend Class Trade
    Implements ITrade

    Private ReadOnly _data As WorldData
    Private ReadOnly _shoppeName As String
    Private ReadOnly _index As Integer
    Private ReadOnly Property TradeData As TradeData
        Get
            Return _data.Shoppes(_shoppeName).Trades(_index)
        End Get
    End Property


    Public Sub New(data As WorldData, shoppeName As String, index As Integer)
        _data = data
        _shoppeName = shoppeName
        _index = index
    End Sub

    Public ReadOnly Property FromItem As IItemInstance Implements ITrade.FromItem
        Get
            Return New TradeFromItemInstance(_data, _shoppeName, _index)
        End Get
    End Property

    Public ReadOnly Property ToItem As IItemInstance Implements ITrade.ToItem
        Get
            Return New TradeToItemInstance(_data, _shoppeName, _index)
        End Get
    End Property
End Class
