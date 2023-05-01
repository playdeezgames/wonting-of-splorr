Friend Class SaveGameState
    Inherits BaseMenuState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Save Slot:",
            New List(Of String) From {
                Slot1Text,
                Slot2Text,
                Slot3Text,
                Slot4Text,
                Slot5Text
            },
            Sub(menuItem)
                GameContext.Save(menuItem)
                setState(GameState.SelectMode, False)
            End Sub,
            Sub()
                setState(GameState.GameMenu, False)
            End Sub)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim font = Fonts(GameFont.Font5x7)
        If GameContext.HasSaveSlot(CurrentItemText) Then
            font.WriteText(displayBuffer, (0, ViewHeight - font.Height), "Will Overwrite", Hue.Red)
        End If
    End Sub
End Class
