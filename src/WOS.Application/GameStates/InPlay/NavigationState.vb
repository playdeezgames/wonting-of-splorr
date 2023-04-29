Friend Class NavigationState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.UpReleased
                World.Avatar.Move(Direction.North)
                SetState(GameState.Neutral)
            Case Command.RightReleased
                World.Avatar.Move(Direction.East)
                SetState(GameState.Neutral)
            Case Command.DownReleased
                World.Avatar.Move(Direction.South)
                SetState(GameState.Neutral)
            Case Command.LeftReleased
                World.Avatar.Move(Direction.West)
                SetState(GameState.Neutral)
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
            If x < -MapCellWidth OrElse x >= ViewWidth Then
                Continue For
            End If
            For row = 0 To map.Rows - 1
                Dim y = offsetY + row * MapCellHeight
                If y < -MapCellHeight OrElse y >= ViewHeight Then
                    Continue For
                End If
                Dim cell = map.GetCell(column, row)
                Dim terrain = cell.Terrain
                terrain.Render(displayBuffer, x, y)
                Dim character = cell.Character
                If character IsNot Nothing Then
                    character.Render(displayBuffer, x, y)
                End If
            Next
        Next
        For column = 0 To map.Columns - 1
            For row = 0 To map.Rows - 1
                Dim cell = map.GetCell(column, row)
                Dim terrain = cell.Terrain
                displayBuffer.SetPixel(column, row, terrain.Hue)
            Next
        Next
    End Sub
End Class
