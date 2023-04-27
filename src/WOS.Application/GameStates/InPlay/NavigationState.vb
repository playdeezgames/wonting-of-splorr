Friend Class NavigationState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        Dim character = World.Avatar.Character
        Dim offsetX = ViewWidth \ 2 - MapCellWidth \ 2 - character.Column * MapCellWidth
        Dim offsetY = ViewHeight \ 2 - MapCellHeight \ 2 - character.Row * MapCellHeight
    End Sub
End Class
