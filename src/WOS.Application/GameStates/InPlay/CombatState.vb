Friend Class CombatState
    Inherits BaseMenuState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Combat!",
            New List(Of String) From {
                FightText,
                FleeText},
            Sub(menuItem)
                Select Case menuItem
                    Case FightText
                        HandleFight()
                        setState(GameState.Neutral, False)
                    Case FleeText
                        HandleFlee()
                        setState(GameState.Neutral, False)
                End Select
            End Sub,
            Sub()
                'no cancel!
            End Sub)
    End Sub

    Private Shared Sub HandleFlee()
        World.Avatar.Character.Target = Nothing
    End Sub

    Private Shared Sub HandleFight()
        Dim mainCharacter = World.Avatar.Character
        mainCharacter.Attack(mainCharacter.Target)
        If Not mainCharacter.Target.IsDead Then
            mainCharacter.Target.Attack(mainCharacter)
        Else
            'TODO: clean up corpse
            mainCharacter.Target = Nothing
        End If
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        Dim mainCharacter = World.Avatar.Character
        font.WriteText(displayBuffer, (0, font.Height * 5), $"{mainCharacter.Name}: {mainCharacter.Health}/{mainCharacter.MaximumHealth}", Hue.Gray)
        Dim enemy = mainCharacter.Target
        font.WriteText(displayBuffer, (0, font.Height * 6), $"{enemy.Name}: {enemy.Health}/{enemy.MaximumHealth}", Hue.Gray)
    End Sub
End Class
