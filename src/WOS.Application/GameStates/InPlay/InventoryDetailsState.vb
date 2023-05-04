Friend Class InventoryDetailsState
    Inherits BaseMenuState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From
            {
                UseText,
                EquipText
            },
            Sub(menuItem)
                Select Case menuItem
                    Case UseText
                        HandleUse()
                        setState(GameState.Neutral, False)
                End Select
            End Sub,
            Sub()
                setState(GameState.Inventory, False)
            End Sub)
    End Sub

    Private Shared Sub HandleUse()
        Dim character = World.Avatar.Character
        Dim item = character.Items.ToList()(InventoryIndex)
        character.Use(item)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim character = World.Avatar.Character
        Dim item = character.Items.ToList()(InventoryIndex)
        Dim font = Fonts(GameFont.Font5x7)
        font.WriteText(displayBuffer, (0, ViewHeight - font.Height), $"{item.Item.DisplayName}(x{item.Quantity})", Hue.Gray)
    End Sub
End Class
