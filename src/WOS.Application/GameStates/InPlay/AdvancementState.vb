Friend Class AdvancementState
    Inherits BaseMenuState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Advancement",
            New List(Of String) From {
                IncreaseIntelligenceText,
                IncreaseMaximumHealthText
            },
            Sub(menuItem)
                Select Case menuItem
                    Case IncreaseIntelligenceText
                        HandleIntelligenceIncrease()
                    Case IncreaseMaximumHealthText
                        HandleMaximumHealthIncrease()
                End Select
            End Sub,
            Sub()
                setState(GameState.SelectMode, False)
            End Sub)
    End Sub
    Private Shared Sub HandleMaximumHealthIncrease()
        Dim character = World.Avatar.Character
        Dim cost = character.MaximumHealth * character.GetStatistic(StatisticType.HealthIncreaseMultiplier)
        If cost <= character.XP Then
            character.XP -= cost
            character.MaximumHealth += 1
        End If
    End Sub
    Private Shared Sub HandleIntelligenceIncrease()
        Dim character = World.Avatar.Character
        Dim cost = character.Intelligence * character.GetStatistic(StatisticType.IntelligenceIncreaseMultiplier)
        If cost <= character.XP Then
            character.XP -= cost
            character.Intelligence += 1
        End If
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim character = World.Avatar.Character
        Dim font = Fonts(GameFont.Font5x7)
        Dim y = ViewHeight - font.Height * 5
        font.WriteText(displayBuffer, (0, y), $"Current Intelligence: {character.Intelligence}", Hue.Gray)
        y += font.Height
        Dim cost = character.Intelligence * character.GetStatistic(StatisticType.IntelligenceIncreaseMultiplier)
        font.WriteText(displayBuffer, (0, y), $"Intelligence Cost: {cost}", If(cost > character.XP, Hue.Red, Hue.Green))
        y += font.Height
        font.WriteText(displayBuffer, (0, y), $"Current Health: {character.MaximumHealth}", Hue.Gray)
        y += font.Height
        cost = character.MaximumHealth * character.GetStatistic(StatisticType.HealthIncreaseMultiplier)
        font.WriteText(displayBuffer, (0, y), $"Health Cost: {cost}", If(cost > character.XP, Hue.Red, Hue.Green))
        y += font.Height
        font.WriteText(displayBuffer, (0, y), $"Current XP: {character.XP}", Hue.Gray)
    End Sub
End Class
