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
    End Sub
    Private Shared Sub HandleIntelligenceIncrease()
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim character = World.Avatar.Character
        Dim font = Fonts(GameFont.Font5x7)
        Dim y = ViewHeight - font.Height * 3
        font.WriteText(displayBuffer, (0, y), $"Intelligence Cost: {character.Intelligence * character.GetStatistic(StatisticType.IntelligenceIncreaseMultiplier)}", Hue.Gray)
        y += font.Height
        font.WriteText(displayBuffer, (0, y), $"Health Cost: {character.MaximumHealth * character.GetStatistic(StatisticType.HealthIncreaseMultiplier)}", Hue.Gray)
        y += font.Height
        font.WriteText(displayBuffer, (0, y), $"Current XP: {character.XP}", Hue.Gray)
    End Sub
End Class
