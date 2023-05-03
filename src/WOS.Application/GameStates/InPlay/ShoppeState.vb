Friend Class ShoppeState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private _tradeIndex As Integer = 0

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Dim character = World.Avatar.Character
        Dim shoppe = character.Shoppe
        Select Case command
            Case Command.LeftReleased
                character.Shoppe = Nothing
                SetState(GameState.Neutral)
            Case Command.DownReleased
                _tradeIndex = (_tradeIndex + 1) Mod shoppe.TradeCount
            Case Command.UpReleased
                _tradeIndex = (_tradeIndex + shoppe.TradeCount - 1) Mod shoppe.TradeCount
            Case Command.FireReleased
                character.MakeTrade(shoppe.Trades(_tradeIndex))
                SetState(GameState.Neutral)
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim character = World.Avatar.Character
        Dim shoppe = character.Shoppe
        If _tradeIndex >= shoppe.TradeCount Then
            _tradeIndex = 0
        End If
        Dim font = Fonts(GameFont.Font4x6)
        Dim y = ViewHeight \ 2 - font.Height \ 2 - _tradeIndex * font.Height
        Dim index = 0
        For Each trade In shoppe.Trades
            Dim fromItem = trade.FromItem
            Dim toItem = trade.ToItem
            Dim fromQuantity = If(fromItem.Quantity > 1, $"{fromItem.Quantity} ", "")
            Dim toQuantity = If(toItem.Quantity > 1, $"{toItem.Quantity} ", "")
            Dim text = $"{fromQuantity}{fromItem.Item.Name} -> {toQuantity}{toItem.Item.Name}"
            Dim h = If(_tradeIndex = index, Hue.LightRed, Hue.Red)
            If character.CanTrade(trade) Then
                h = If(_tradeIndex = index, Hue.LightGreen, Hue.Green)
            End If
            font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, y), text, h)
            y += font.Height
            index += 1
        Next
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(shoppe.DisplayName) \ 2, 0), shoppe.DisplayName, Hue.White)
    End Sub
End Class
