Friend Class BaseMenuState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private ReadOnly _caption As String
    Private ReadOnly _menuItems As IReadOnlyList(Of String)
    Private _currentItem As Integer = 0
    Private ReadOnly _onMenuItem As Action(Of String)
    Private ReadOnly _onCancel As Action
    Protected ReadOnly Property CurrentItemText As String
        Get
            Return _menuItems(_currentItem)
        End Get
    End Property
    Sub New(
                     parent As IGameController(Of Hue, Command, Sfx),
                     setState As Action(Of GameState?, Boolean),
                     caption As String,
                     menuItems As IReadOnlyList(Of String),
                     onMenuItem As Action(Of String),
                     onCancel As Action)
        MyBase.New(parent, setState)
        _menuItems = menuItems
        _caption = caption
        _onMenuItem = onMenuItem
        _onCancel = onCancel
    End Sub
    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased
                _currentItem = (_currentItem + _menuItems.Count - 1) Mod _menuItems.Count
            Case Command.DownReleased
                _currentItem = (_currentItem + 1) Mod _menuItems.Count
            Case Command.FireReleased, Command.RightReleased
                _onMenuItem(_menuItems(_currentItem))
            Case Command.LeftReleased
                _onCancel()
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font5x7)
        Dim y = 0
        If Not String.IsNullOrWhiteSpace(_caption) Then
            font.WriteText(displayBuffer, (0, y), _caption, Hue.Gray)
            y += font.Height
        End If
        Dim row = 0
        For Each menuItem In _menuItems
            Dim h As Hue = If(row = _currentItem, Hue.LightBlue, Hue.Blue)
            font.WriteText(displayBuffer, (0, y), menuItem, h)
            row += 1
            y += font.Height
        Next
    End Sub
End Class
