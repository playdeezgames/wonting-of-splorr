Friend Class InventoryState
    Inherits BasePickState
    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "Inventory",
            Sub(index)
                GameContext.InventoryIndex = index
            End Sub,
            Sub(itemName)
                setState(GameState.InventoryDetails, False)
            End Sub,
            Sub()
                setState(GameState.SelectMode, False)
            End Sub,
            Function(x) x,
            Function() World.Avatar.Character.Items.Select(Function(x) $"{x.Item.DisplayName}(x{x.Quantity})"))
    End Sub
    Public Overrides Sub OnStart()
        Dim character = World.Avatar.Character
        If Not character.HasItems Then
            character.AddMessage(Nothing, New List(Of (Hue, String)) From {
                (Hue.Red, $"{character.Name} has no inventory.")
                                              })
            SetState(GameState.Neutral)
        End If
    End Sub
End Class
