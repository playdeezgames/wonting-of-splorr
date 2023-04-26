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
                    QuitText
                 },
                 Sub(menuItem)
                     Select Case menuItem
                         Case QuitText
                             SetCurrentState(GameState.ConfirmQuit, False)
                     End Select
                 End Sub,
                 Sub()
                     SetCurrentState(GameState.ConfirmQuit, False)
                 End Sub))
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
End Class
