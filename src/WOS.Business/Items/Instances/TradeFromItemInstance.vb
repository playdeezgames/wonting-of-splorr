Friend Class TradeFromItemInstance
    Inherits BaseItemInstance

    Private ReadOnly _shoppeName As String
    Private ReadOnly _index As Integer

    Public Sub New(data As WorldData, shoppeName As String, index As Integer)
        MyBase.New(data)
        _shoppeName = shoppeName
        _index = index
    End Sub

    Protected Overrides ReadOnly Property ItemInstanceData As ItemInstanceData
        Get
            Return _data.Shoppes(_shoppeName).Trades(_index).FromItem
        End Get
    End Property
End Class
