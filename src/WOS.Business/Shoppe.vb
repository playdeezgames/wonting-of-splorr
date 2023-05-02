Friend Class Shoppe
    Implements IShoppe
    Private ReadOnly _data As WorldData
    Private ReadOnly _shoppeName As String
    Private ReadOnly Property ShoppeData As ShoppeData
        Get
            Return _data.Shoppes(_shoppeName)
        End Get
    End Property

    Public Sub New(data As WorldData, shoppeName As String)
        Me._data = data
        Me._shoppeName = shoppeName
    End Sub

    Public ReadOnly Property Name As String Implements IShoppe.Name
        Get
            Return _shoppeName
        End Get
    End Property

    Public ReadOnly Property TradeCount As Integer Implements IShoppe.TradeCount
        Get
            Return ShoppeData.Trades.Count
        End Get
    End Property

    Public ReadOnly Property Trades As IEnumerable(Of ITrade) Implements IShoppe.Trades
        Get
            Dim result As New List(Of ITrade)
            For index = 0 To TradeCount - 1
                result.Add(New Trade(_data, _shoppeName, index))
            Next
            Return result
        End Get
    End Property
End Class
