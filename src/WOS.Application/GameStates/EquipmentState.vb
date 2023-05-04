Friend Class EquipmentState
    Inherits BasePickState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Equipment",
            Sub(index)

            End Sub,
            Sub(menuItem)

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


End Class
