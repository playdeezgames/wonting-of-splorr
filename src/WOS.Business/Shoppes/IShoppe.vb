Public Interface IShoppe
    ReadOnly Property Name As String
    ReadOnly Property TradeCount As Integer
    ReadOnly Property Trades As IEnumerable(Of ITrade)
    ReadOnly Property DisplayName As String
End Interface
