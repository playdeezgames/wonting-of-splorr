﻿Friend Class InventoryState
    Inherits BasePickState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Inventory",
            Sub(index)

            End Sub,
            Sub(itemName)

            End Sub,
            Sub()
                setState(GameState.SelectMode, False)
            End Sub,
            Function(x) x,
            Function() World.Avatar.Character.Items.Select(Function(x) $"{x.Item.Name}(x{x.Quantity})"))
    End Sub
End Class
