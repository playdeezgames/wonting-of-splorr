Imports System.Security.Cryptography

Friend Class TitleState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub
    Public Overrides Sub HandleCommand(command As Command)
    End Sub
    Const TitleText = "Wonting of SPLORR!!"
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font8x8)
        Dim h = RNG.FromEnumerable(AllHues)
        font.WriteText(displayBuffer, (ViewWidth \ 2 - font.TextWidth(TitleText) \ 2, ViewHeight \ 2 - font.Height \ 2), TitleText, h)
    End Sub
End Class
