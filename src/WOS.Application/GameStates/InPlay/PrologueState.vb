﻿Friend Class PrologueState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.FireReleased
                SetState(GameState.Neutral)
        End Select
    End Sub

    Private Shared ReadOnly prologueLines As IReadOnlyList(Of (String, Hue)) = New List(Of (String, Hue)) From
        {
            ("Prologue", Hue.LightBlue),
            ("In the world of SPLORR!!, every ", Hue.Gray),
            ("coin has two sides - just like  ", Hue.Gray),
            ("every world with coins! Because ", Hue.Gray),
            ("that's how coins work!          ", Hue.Gray),
            ("But in world of SPLORR!!, there ", Hue.Gray),
            ("exist two TYPES of coin, CROWNS ", Hue.Gray),
            ("and JOOLS, but for some arbi-   ", Hue.Gray),
            ("trary reason, people can only   ", Hue.Gray),
            ("have one type at a time!        ", Hue.Gray),
            ("Some items are bought with JOOLS", Hue.Gray),
            ("and some with CROWNS.", Hue.Gray),
            ("Good luck playing.", Hue.Green)
        }

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        Dim font = Fonts(GameFont.Font5x7)
        Dim y = 0
        For Each line In prologueLines
            font.WriteText(displayBuffer, (0, y), line.Item1, line.Item2)
            y += font.Height
        Next
    End Sub
End Class
