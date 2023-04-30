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
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
    End Sub
End Class
