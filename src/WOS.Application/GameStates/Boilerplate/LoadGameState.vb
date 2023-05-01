Friend Class LoadGameState
    Inherits BaseMenuState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Load from Slot:",
            New List(Of String) From
            {
                Slot1Text,
                Slot2Text,
                Slot3Text,
                Slot4Text,
                Slot5Text
            },
            Sub(menuItem)
                If GameContext.Load(menuItem) Then
                    setState(GameState.Neutral, False)
                Else
                    setState(GameState.MainMenu, False)
                End If
            End Sub,
            Sub()
                setState(GameState.MainMenu, False)
            End Sub)
    End Sub
End Class
