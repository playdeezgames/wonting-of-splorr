Friend Class BaseConfirmState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private _confirmation As Boolean = False
    Private ReadOnly _caption As String
    Private ReadOnly _hue As Hue
    Private ReadOnly _onDone As Action(Of Boolean)
    Private ReadOnly _onCancel As Action
    Public Sub New(
                  parent As IGameController(Of Hue, Command, Sfx),
                  setState As Action(Of GameState?, Boolean),
                  caption As String,
                  hue As Hue,
                  onDone As Action(Of Boolean),
                  onCancel As Action)
        MyBase.New(parent, setState)
        _onDone = onDone
        _onCancel = onCancel
        _caption = caption
        _hue = hue
    End Sub
    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased, Command.DownReleased
                _confirmation = Not _confirmation
            Case Command.LeftReleased
                _onCancel()
            Case Command.FireReleased
                _onDone(_confirmation)
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, 0), _caption, _hue)
        font.WriteText(displayBuffer, (0, font.Height), "No", If(_confirmation, Hue.Blue, Hue.LightBlue))
        font.WriteText(displayBuffer, (0, font.Height * 2), "Yes", If(_confirmation, Hue.LightBlue, Hue.Blue))
    End Sub
End Class
