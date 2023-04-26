Public Class GameController
    Inherits BaseGameController(Of Hue, Command, Sfx, GameState)
    Private ReadOnly _configSink As Action(Of (Integer, Integer), Single)

    Public Sub New(windowSizeSource As Func(Of (Integer, Integer)), volumeSource As Func(Of Single), configSink As Action(Of (Integer, Integer), Single))
        MyBase.New(windowSizeSource(), volumeSource())
        _configSink = configSink
        _configSink(Size, Volume)
        Initialize()
        SetBoilerplateStates()
        SetCurrentState(GameState.Title, True)
    End Sub

    Private Sub SetBoilerplateStates()
        SetState(GameState.Title, New TitleState(Me, AddressOf SetCurrentState))
        SetState(GameState.MainMenu, New BaseMenuState(
                 Me,
                 AddressOf SetCurrentState,
                 MainMenuCaptionText,
                 New List(Of String) From {
                    OptionsText,
                    QuitText
                 },
                 Sub(menuItem)
                     Select Case menuItem
                         Case OptionsText
                             SetCurrentState(GameState.OptionsMenu, False)
                         Case QuitText
                             SetCurrentState(GameState.ConfirmQuit, False)
                     End Select
                 End Sub,
                 Sub()
                     SetCurrentState(GameState.ConfirmQuit, False)
                 End Sub))
        SetState(GameState.OptionsMenu, New BaseMenuState(
                 Me,
                 AddressOf SetCurrentState,
                 OptionsMenuCaptionText,
                 New List(Of String) From
                 {
                    WindowSizeText,
                    SfxVolumeText
                 },
                 Sub(menuItem)
                     Select Case menuItem
                         Case WindowSizeText
                             SetCurrentState(GameState.PickWindowSize, False)
                         Case SfxVolumeText
                             SetCurrentState(GameState.PickSfxVolume, False)
                     End Select
                 End Sub,
                 Sub()
                     SetCurrentState(GameState.MainMenu, False)
                 End Sub))
        SetState(GameState.PickWindowSize, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "Window Size",
                 AddressOf OnSelectWindowSize,
                 Sub(picked)
                     SetCurrentState(GameState.OptionsMenu, False)
                 End Sub,
                 Sub()
                 End Sub,
                 AddressOf WindowSizeIndexSource,
                 AddressOf WindowSizeListSource))
        SetState(GameState.ConfirmQuit, New BaseConfirmState(
                 Me,
                 AddressOf SetCurrentState,
                 ConfirmQuitPromptText,
                 Hue.Red,
                 Sub(confirmed)
                     If confirmed Then
                         SetCurrentState(Nothing, False)
                     Else
                         SetCurrentState(GameState.MainMenu, False)
                     End If
                 End Sub,
                 Sub()
                     SetCurrentState(GameState.MainMenu, False)
                 End Sub))
    End Sub
    Private Shared ReadOnly _indexedWindowSizes As IReadOnlyList(Of (Integer, Integer)) = New List(Of (Integer, Integer)) From
        {
            (0, 4), (1, 6), (2, 8), (3, 10), (4, 12), (5, 14), (6, 16), (7, 18), (8, 20)
        }
    Private Shared ReadOnly _windowSizePickTexts As IReadOnlyList(Of String) = _indexedWindowSizes.Select(Function(x) $"{x.Item2 * ViewWidth}x{x.Item2 * ViewHeight}").ToList
    Private Shared ReadOnly _windowScaleToIndex As IReadOnlyDictionary(Of Integer, Integer) = _indexedWindowSizes.ToDictionary(Function(x) x.Item2, Function(x) x.Item1)
    Private Shared ReadOnly _windowIndexToScale As IReadOnlyDictionary(Of Integer, Integer) = _indexedWindowSizes.ToDictionary(Function(x) x.Item1, Function(x) x.Item2)

    Private Function WindowSizeListSource() As IEnumerable(Of String)
        Return _windowSizePickTexts
    End Function

    Private Function WindowSizeIndexSource(dummy As Integer) As Integer
        Return _windowScaleToIndex(Size.Item1 \ ViewWidth)
    End Function

    Private Sub OnSelectWindowSize(index As Integer)
        Dim scale = _windowIndexToScale(index)
        Size = (ViewWidth * scale, ViewHeight * scale)
        _configSink(Size, Volume)
    End Sub
End Class
