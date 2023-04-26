Friend Class TitleState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub
    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.FireReleased
                SetState(GameState.MainMenu)
        End Select
    End Sub
    Const TitleText = "Wonting of SPLORR!!"
    Const ControlText = "Press Fire"
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        ShowTitleText(displayBuffer)
        Dim font = Fonts(GameFont.Font3x5)
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(ControlText) \ 2, ViewHeight - font.Height), ControlText, Hue.Gray)
    End Sub

    Private Shared Sub ShowTitleText(displayBuffer As IPixelSink(Of Hue))
        Dim font = Fonts(GameFont.Font8x8)
        Dim h = RNG.FromEnumerable(AllHues)
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(TitleText) \ 2, ViewHeight \ 2 - font.Height \ 2), TitleText, h)
    End Sub
End Class
