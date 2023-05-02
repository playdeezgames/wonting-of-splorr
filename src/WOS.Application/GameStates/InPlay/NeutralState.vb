﻿Friend Class NeutralState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
        MyBase.Update(elapsedTime)
        Dim mainCharacter = World.Avatar.Character
        mainCharacter.PickUpGroundItem()
        If mainCharacter.HasMessage Then
            Dim msgSfx = mainCharacter.Message.Sfx
            If msgSfx.HasValue Then
                PlaySfx(msgSfx.Value)
            End If
            SetState(GameState.Message)
            Return
        End If
        If mainCharacter.IsDead Then
            SetState(GameState.GameOver)
            Return
        End If
        If mainCharacter.Target IsNot Nothing Then
            SetState(GameState.Combat)
            Return
        End If
        If mainCharacter.Shoppe IsNot Nothing Then
            SetState(GameState.Shoppe)
            Return
        End If
        SetState(GameState.Navigation)
    End Sub
End Class
