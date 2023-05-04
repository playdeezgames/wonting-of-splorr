Friend Class PickCombatItemState
    Inherits BasePickState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Choose Item...",
            Sub(index)
                InventoryIndex = index
            End Sub,
            Sub(menuItem)
                Dim itemInstance = World.Avatar.Character.UsableItems.ToList()(InventoryIndex)
                World.Avatar.Character.Use(itemInstance)
                setState(GameState.Neutral, False)
            End Sub,
            Sub()
                setState(GameState.Neutral, False)
            End Sub,
            Function(index) index,
            Function() GetCombatItems())
    End Sub
    Private Shared Function GetCombatItems() As IEnumerable(Of String)
        Return World.Avatar.Character.UsableItems.Select(Function(x) $"{x.Item.DisplayName}(x{x.Quantity})")
    End Function

    Public Overrides Sub OnStart()
        MyBase.OnStart()
        InventoryIndex = 0
    End Sub
End Class
