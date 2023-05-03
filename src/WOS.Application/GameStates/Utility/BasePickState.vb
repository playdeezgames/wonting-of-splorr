Imports System.Runtime.InteropServices

Friend Class BasePickState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private _index As Integer = 0
    Private ReadOnly _caption As String = ""
    Private ReadOnly _indexSource As Func(Of Integer, Integer)
    Private ReadOnly _onSelect As Action(Of Integer)
    Private ReadOnly _onPick As Action(Of String)
    Private ReadOnly _onCancel As Action
    Private ReadOnly _listSource As Func(Of IEnumerable(Of String))
    Public Sub New(
                  parent As IGameController(Of Hue, Command, Sfx),
                  setState As Action(Of GameState?, Boolean),
                  caption As String,
                  onSelect As Action(Of Integer),
                  onPick As Action(Of String),
                  onCancel As Action,
                  indexSource As Func(Of Integer, Integer),
                  listSource As Func(Of IEnumerable(Of String)))
        MyBase.New(parent, setState)
        _caption = caption
        _onCancel = onCancel
        _onPick = onPick
        _onSelect = onSelect
        _listSource = listSource
        _indexSource = indexSource
    End Sub
    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased
                _index = (_index + _listSource().Count - 1) Mod _listSource().Count
                _onSelect(_index)
            Case Command.DownReleased
                _index = (_index + 1) Mod _listSource().Count
                _onSelect(_index)
            Case Command.LeftReleased
                _onCancel()
            Case Command.FireReleased
                _onPick(_listSource().ToList(_index))
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font5x7)
        Dim listItems = _listSource().ToList
        If _index > listItems.Count - 1 Then
            _index = 0
        End If
        Dim y = ViewHeight \ 2 - font.Height \ 2 - _index * font.Height
        For index = 0 To listItems.Count - 1
            Dim h As Hue = If(index = _index, Hue.LightBlue, Hue.Blue)
            Dim text = listItems(index)
            font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(text) \ 2, y), text, h)
            y += font.Height
        Next
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(_caption) \ 2, 0), _caption, Hue.White)
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
        MyBase.Update(elapsedTime)
        _index = _indexSource(_index)
    End Sub

    Public Overrides Sub OnStart()
        MyBase.OnStart()
        _index = 0
    End Sub
End Class
