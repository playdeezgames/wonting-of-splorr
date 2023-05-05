Friend Class EquipmentState
    Inherits BasePickState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Equipment",
            Sub(index)
                InventoryIndex = index
            End Sub,
            Sub(menuItem)
                setState(GameState.EquipmentDetails, False)
            End Sub,
            Sub()
                setState(GameState.SelectMode, False)
            End Sub,
            Function(x) x,
            Function()
                Dim equipment = World.Avatar.Character.Equipment
                Return equipment.Select(Function(entry) $"{entry.Key}: {entry.Value.Item.DisplayName}")
            End Function)
    End Sub


    Public Overrides Sub OnStart()
        MyBase.OnStart()
        Dim character = World.Avatar.Character
        If Not character.HasAnyEquipment Then
            character.AddMessage(Nothing, New List(Of (Hue, String)) From {
                    (Hue.Red, $"{character.DisplayName} has no equipment.")
                })
            SetState(GameState.Neutral)
        End If
        InventoryIndex = 0
    End Sub

End Class
