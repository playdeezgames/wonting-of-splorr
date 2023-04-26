Module Program
    Const ConfigFileName = "config.json"
    Sub Main(args As String())
        Dim config = LoadConfig()
        Dim gameController As New GameController(
            Function() (config.WindowWidth, config.WindowHeight),
            Function() config.SfxVolume,
            AddressOf SaveConfig)
        Using host As New Host(Of Hue, Command, Sfx)(
            "Wonting of SPLORR!!",
            gameController,
            (GameContext.ViewWidth, GameContext.ViewHeight),
            AddressOf BufferCreatorator,
            AddressOf CommandTransformerator,
            AddressOf GamePadTransformer,
            New Dictionary(Of Sfx, String) From
            {
                {Sfx.PlayerHit, "Content/PlayerHit.wav"}
            })
            host.Run()
        End Using
    End Sub

    Private Function GamePadTransformer(newState As GamePadState, oldState As GamePadState) As Command()
        Dim results As New HashSet(Of Command)
        If newState.DPad.Up = ButtonState.Released AndAlso oldState.DPad.Up = ButtonState.Pressed Then
            results.Add(Command.UpReleased)
        End If
        If newState.DPad.Down = ButtonState.Released AndAlso oldState.DPad.Down = ButtonState.Pressed Then
            results.Add(Command.DownReleased)
        End If
        If newState.DPad.Left = ButtonState.Released AndAlso oldState.DPad.Left = ButtonState.Pressed Then
            results.Add(Command.LeftReleased)
        End If
        If newState.DPad.Right = ButtonState.Released AndAlso oldState.DPad.Right = ButtonState.Pressed Then
            results.Add(Command.RightReleased)
        End If
        If newState.Buttons.X = ButtonState.Released AndAlso oldState.Buttons.X = ButtonState.Pressed Then
            results.Add(Command.FireReleased)
        End If
        If newState.Buttons.Y = ButtonState.Released AndAlso oldState.Buttons.Y = ButtonState.Pressed Then
            results.Add(Command.FireReleased)
        End If
        If newState.Buttons.A = ButtonState.Released AndAlso oldState.Buttons.A = ButtonState.Pressed Then
            results.Add(Command.FireReleased)
        End If
        If newState.Buttons.B = ButtonState.Released AndAlso oldState.Buttons.B = ButtonState.Pressed Then
            results.Add(Command.FireReleased)
        End If
        Return results.ToArray
    End Function

    Private Sub SaveConfig(windowSize As (Integer, Integer), volume As Single)
        File.WriteAllText(ConfigFileName, JsonSerializer.Serialize(New WOSConfig With {.SfxVolume = volume, .WindowHeight = windowSize.Item2, .WindowWidth = windowSize.Item1}))
    End Sub
    Const DefaultWindowScale = 6
    Const DefaultWindowWidth = ViewWidth * DefaultWindowScale
    Const DefaultWindowHeight = ViewHeight * DefaultWindowScale
    Const DefaultSfxVolume = 1.0F
    Private Function LoadConfig() As WOSConfig
        Try
            Return JsonSerializer.Deserialize(Of WOSConfig)(File.ReadAllText(ConfigFileName))
        Catch ex As Exception
            Return New WOSConfig With
            {
                .WindowWidth = DefaultWindowWidth,
                .WindowHeight = DefaultWindowHeight,
                .SfxVolume = DefaultSfxVolume
            }
        End Try
    End Function

    Private ReadOnly keyTable As IReadOnlyDictionary(Of Keys, Command) =
        New Dictionary(Of Keys, Command) From
        {
            {Keys.Up, Command.UpReleased},
            {Keys.Right, Command.RightReleased},
            {Keys.Left, Command.LeftReleased},
            {Keys.Down, Command.DownReleased},
            {Keys.Space, Command.FireReleased}
        }
    Private Function CommandTransformerator(key As Keys) As Command?
        If keyTable.ContainsKey(key) Then
            Return keyTable(key)
        End If
        Return Nothing
    End Function
    Private Function BufferCreatorator(texture As Texture2D) As IDisplayBuffer(Of Hue)
        Return New DisplayBuffer(Of Hue)(texture, AddressOf TransformHue)
    End Function
    Private ReadOnly hueTable As IReadOnlyDictionary(Of Hue, Color) =
        New Dictionary(Of Hue, Color) From
{
            {Hue.Black, New Color(0, 0, 0, 255)},
            {Hue.Blue, New Color(0, 0, 170, 255)},
            {Hue.Green, New Color(0, 170, 0, 255)},
            {Hue.Cyan, New Color(0, 170, 170, 255)},
            {Hue.Red, New Color(170, 0, 0, 255)},
            {Hue.Magenta, New Color(170, 0, 170, 255)},
            {Hue.Brown, New Color(170, 85, 0, 255)},
            {Hue.Gray, New Color(170, 170, 170, 255)},
            {Hue.DarkGray, New Color(85, 85, 85, 255)},
            {Hue.LightBlue, New Color(85, 85, 255, 255)},
            {Hue.LightGreen, New Color(85, 255, 85, 255)},
            {Hue.LightCyan, New Color(85, 255, 255, 255)},
            {Hue.LightRed, New Color(255, 85, 85, 255)},
            {Hue.LightMagenta, New Color(255, 85, 255, 255)},
            {Hue.Yellow, New Color(255, 255, 85, 255)},
            {Hue.White, New Color(255, 255, 255, 255)}
        }
    Private Function TransformHue(hue As Hue) As Color
        Return hueTable(hue)
    End Function
End Module