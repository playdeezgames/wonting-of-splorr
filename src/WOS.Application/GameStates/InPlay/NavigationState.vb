Friend Class NavigationState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased
                World.Avatar.Move(Direction.North)
            Case Command.RightReleased
                World.Avatar.Move(Direction.East)
            Case Command.DownReleased
                World.Avatar.Move(Direction.South)
            Case Command.LeftReleased
                World.Avatar.Move(Direction.West)
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim avatarCharacter = World.Avatar.Character
        Dim offsetX = ViewWidth \ 2 - MapCellWidth \ 2 - avatarCharacter.Column * MapCellWidth
        Dim offsetY = ViewHeight \ 2 - MapCellHeight \ 2 - avatarCharacter.Row * MapCellHeight
        Dim map As IMap = avatarCharacter.Map
        For column = 0 To map.Columns - 1
            Dim x = offsetX + column * MapCellWidth
            For row = 0 To map.Rows - 1
                Dim y = offsetY + row * MapCellHeight
                Dim cell = map.GetCell(column, row)
                Dim terrain = cell.Terrain
                terrain.Render(displayBuffer, x, y)
                Dim character = cell.Character
                If character IsNot Nothing Then
                    character.Render(displayBuffer, x, y)
                End If
            Next
        Next
    End Sub
End Class
