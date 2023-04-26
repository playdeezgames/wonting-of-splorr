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
    End Sub
End Class