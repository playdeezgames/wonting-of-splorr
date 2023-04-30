Friend Class StatisticsState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.FireReleased, Command.LeftReleased
                SetState(GameState.SelectMode)
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font5x7)
        Dim character = World.Avatar.Character
        font.WriteText(displayBuffer, (0, 0), "Statistics:", Hue.White)
        font.WriteText(displayBuffer, (0, 1 * font.Height), $"Health: {character.Health}/{character.MaximumHealth}", Hue.Gray)
        font.WriteText(displayBuffer, (0, 2 * font.Height), $"Attack: {character.MaximumAttack}", Hue.Gray)
        font.WriteText(displayBuffer, (0, 3 * font.Height), $"Defend: {character.MaximumDefend}", Hue.Gray)
    End Sub
End Class
