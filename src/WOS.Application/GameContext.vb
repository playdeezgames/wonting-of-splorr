Public Module GameContext
    Public Const ViewWidth = 192
    Public Const ViewHeight = 108
    Public ReadOnly AllHues As IReadOnlyList(Of Hue) = New List(Of Hue) From {
            Hue.Black,
            Hue.Blue,
            Hue.Green,
            Hue.Cyan,
            Hue.Red,
            Hue.Magenta,
            Hue.Brown,
            Hue.Gray,
            Hue.DarkGray,
            Hue.LightBlue,
            Hue.LightGreen,
            Hue.LightCyan,
            Hue.LightRed,
            Hue.LightMagenta,
            Hue.Yellow,
            Hue.White
            }
    Friend Sub Initialize()
        InitializeFonts()
    End Sub
    Friend ReadOnly Fonts As New Dictionary(Of GameFont, Font)
    Private Sub InitializeFonts()
        Fonts.Clear()
        Fonts.Add(GameFont.Font3x5, New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText("Content/CyFont3x5.json"))))
        Fonts.Add(GameFont.Font4x6, New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText("Content/CyFont4x6.json"))))
        Fonts.Add(GameFont.Font5x7, New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText("Content/CyFont5x7.json"))))
        Fonts.Add(GameFont.Font8x8, New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText("Content/CyFont8x8.json"))))
    End Sub
End Module
