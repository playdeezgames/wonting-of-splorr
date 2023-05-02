Public Interface IShoppe
    ReadOnly Property Name As String
    ReadOnly Property TradeCount As Integer
    ReadOnly Property Trades As IEnumerable(Of ITrade)
End Interface
