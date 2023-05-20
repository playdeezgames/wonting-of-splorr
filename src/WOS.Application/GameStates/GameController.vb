Public Class GameController
    Inherits BaseGameController(Of Hue, Command, Sfx, GameState)
    Private ReadOnly _configSink As Action(Of (Integer, Integer), Single)

    Public Sub New(windowSizeSource As Func(Of (Integer, Integer)), fullScreenSource As Func(Of Boolean), volumeSource As Func(Of Single), configSink As Action(Of (Integer, Integer), Single))
        MyBase.New(windowSizeSource(), fullScreenSource(), volumeSource())
        _configSink = configSink
        _configSink(Size, Volume)
        Initialize()
        SetBoilerplateStates()
        SetInPlayStates()
        SetCurrentState(GameState.Title, True)
    End Sub

    Private Sub SetInPlayStates()
        SetState(GameState.Prologue, New PrologueState(Me, AddressOf SetCurrentState))
        SetState(GameState.Neutral, New NeutralState(Me, AddressOf SetCurrentState))
        SetState(GameState.Navigation, New NavigationState(Me, AddressOf SetCurrentState))
        SetState(GameState.Message, New MessageState(Me, AddressOf SetCurrentState))
        SetState(GameState.Combat, New CombatState(Me, AddressOf SetCurrentState))
        SetState(GameState.SelectMode, New BaseMenuState(
                 Me,
                 AddressOf SetCurrentState,
                 "Choose Mode:",
                 New List(Of String) From {
                    NavigationText,
                    StatisticsText,
                    InventoryText,
                    EquipmentText,
                    AdvancementText,
                    GameMenuText
                 },
                 Sub(menuItem)
                     Select Case menuItem
                         Case NavigationText
                             SetCurrentState(GameState.Neutral, False)
                         Case StatisticsText
                             SetCurrentState(GameState.Statistics, False)
                         Case GameMenuText
                             SetCurrentState(GameState.GameMenu, False)
                         Case InventoryText
                             SetCurrentState(GameState.Inventory, False)
                         Case EquipmentText
                             SetCurrentState(GameState.Equipment, False)
                         Case AdvancementText
                             SetCurrentState(GameState.Advancement, False)
                     End Select
                 End Sub,
                 Sub()
                     SetCurrentState(GameState.Neutral, False)
                 End Sub))
        SetState(GameState.Statistics, New StatisticsState(Me, AddressOf SetCurrentState))
        SetState(GameState.Inventory, New InventoryState(Me, AddressOf SetCurrentState))
        SetState(GameState.InventoryDetails, New InventoryDetailsState(Me, AddressOf SetCurrentState))
        SetState(GameState.Equipment, New EquipmentState(Me, AddressOf SetCurrentState))
        SetState(GameState.EquipmentDetails, New EquipmentDetailsState(Me, AddressOf SetCurrentState))
        SetState(GameState.GameOver, New GameOverState(Me, AddressOf SetCurrentState))
        SetState(GameState.Shoppe, New ShoppeState(Me, AddressOf SetCurrentState))
        SetState(GameState.PickCombatUseItem, New PickCombatItemState(Me, AddressOf SetCurrentState))
        SetState(GameState.Advancement, New AdvancementState(Me, AddressOf SetCurrentState))
        SetGameMenuStates()
    End Sub

    Private Sub SetGameMenuStates()
        SetState(GameState.GameMenu, New BaseMenuState(
                 Me,
                 AddressOf SetCurrentState,
                 "Game Menu:",
                 New List(Of String) From {
                    SaveGameText,
                    AbandonGameText
                 },
                 Sub(menuItem)
                     Select Case menuItem
                         Case AbandonGameText
                             SetCurrentState(GameState.ConfirmAbandon, False)
                         Case SaveGameText
                             SetCurrentState(GameState.SaveGame, False)
                     End Select
                 End Sub,
                 Sub()
                     SetCurrentState(GameState.SelectMode, False)
                 End Sub))
        SetState(GameState.ConfirmAbandon, New BaseConfirmState(
                 Me,
                 AddressOf SetCurrentState,
                 "Confirm Abandon?",
                 Hue.Red,
                 Sub(confirm)
                     If confirm Then
                         SetCurrentState(GameState.MainMenu, False)
                     Else
                         SetCurrentState(GameState.GameMenu, False)
                     End If
                 End Sub,
                 Sub()
                     SetCurrentState(GameState.GameMenu, False)
                 End Sub))
        SetState(GameState.SaveGame, New SaveGameState(Me, AddressOf SetCurrentState))
    End Sub

    Private Sub SetBoilerplateStates()
        SetState(GameState.Title, New TitleState(Me, AddressOf SetCurrentState))
        SetState(GameState.MainMenu, New BaseMenuState(
                 Me,
                 AddressOf SetCurrentState,
                 MainMenuCaptionText,
                 New List(Of String) From {
                    EmbarkText,
                    LoadGameText,
                    OptionsText,
                    QuitText
                 },
                 Sub(menuItem)
                     Select Case menuItem
                         Case EmbarkText
                             World.Initialize()
                             SetCurrentState(GameState.Prologue, False)
                         Case OptionsText
                             SetCurrentState(GameState.OptionsMenu, False)
                         Case QuitText
                             SetCurrentState(GameState.ConfirmQuit, False)
                         Case LoadGameText
                             SetCurrentState(GameState.LoadGame, False)
                     End Select
                 End Sub,
                 Sub()
                     SetCurrentState(GameState.ConfirmQuit, False)
                 End Sub))
        SetState(GameState.LoadGame, New LoadGameState(Me, AddressOf SetCurrentState))
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
        SetState(GameState.PickSfxVolume, New BasePickState(
                 Me,
                 AddressOf SetCurrentState,
                 "SFX Volume",
                 AddressOf OnSelectSfxVolume,
                 Sub(picked)
                     SetCurrentState(GameState.OptionsMenu, False)
                 End Sub,
                 Sub()
                 End Sub,
                 AddressOf SfxVolumeIndexSource,
                 AddressOf SfxVolumeListSource))
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

    Private Shared ReadOnly _indexedSfxVolumes As IReadOnlyList(Of (Integer, Single)) = New List(Of (Integer, Single)) From
        {
            (0, 0.0F), (1, 0.1F), (2, 0.2F), (3, 0.3F), (4, 0.4F), (5, 0.5F), (6, 0.6F), (7, 0.7F), (8, 0.8F), (9, 0.9F), (10, 1.0F)
        }
    Private Shared ReadOnly _sfxVolumePickTexts As IReadOnlyList(Of String) = _indexedSfxVolumes.Select(Function(x) $"{x.Item2 * 100}%").ToList
    Private Shared ReadOnly _sfxIndexToVolume As IReadOnlyDictionary(Of Integer, Single) = _indexedSfxVolumes.ToDictionary(Function(x) x.Item1, Function(x) x.Item2)
    Private Function SfxVolumeListSource() As IEnumerable(Of String)
        Return _sfxVolumePickTexts
    End Function

    Private Function SfxVolumeIndexSource(dummy As Integer) As Integer
        Return CInt(Volume * 10.0F)
    End Function

    Private Sub OnSelectSfxVolume(index As Integer)
        Volume = _sfxIndexToVolume(index)
        PlaySfx(Sfx.PlayerHit)
        _configSink(Size, Volume)
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
