Friend Class MessageState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.FireReleased
                World.Avatar.Character.NextMessage()
                SetState(GameState.Neutral)
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        ShowMessage(displayBuffer)
        Dim font = Fonts(GameFont.Font3x5)
        Const text = "Press Fire"
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, ViewHeight - font.Height), text, Hue.DarkGray)
    End Sub

    Private Shared Sub ShowMessage(displayBuffer As IPixelSink(Of Hue))
        Dim message As IEnumerable(Of (Hue, String)) = World.Avatar.Character.Message
        Dim font = Fonts(GameFont.Font5x7)
        Dim y = 0
        For Each line In message
            font.WriteText(displayBuffer, (0, y), line.Item2, line.Item1)
            y += font.Height
        Next
    End Sub
End Class
