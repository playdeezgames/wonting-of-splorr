Friend Class EquipmentDetailsState
    Inherits BaseMenuState

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(
            parent,
            setState,
            "",
            New List(Of String) From
            {
                UnequipText
            },
            Sub(menuItem)
                Select Case menuItem
                    Case UnequipText
                        HandleUnequip()
                        setState(GameState.Equipment, False)
                End Select
            End Sub,
            Sub()
                setState(GameState.Equipment, False)
            End Sub)
    End Sub

    Private Shared Sub HandleUnequip()
        Dim character = World.Avatar.Character
        Dim equipSlot = character.Equipment.ToList()(InventoryIndex).Key
        character.Unequip(equipSlot)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        MyBase.Render(displayBuffer)
        Dim character = World.Avatar.Character
        Dim equipSlot = character.Equipment.ToList()(InventoryIndex)
        Dim item = equipSlot.Value
        Dim font = Fonts(GameFont.Font5x7)
        Dim y = ViewHeight - font.Height * 2
        If item.IsWeapon Then
            y -= font.Height
        End If
        If item.IsArmor Then
            y -= font.Height
        End If
        font.WriteText(displayBuffer, (0, y), $"{item.Item.DisplayName}", Hue.Gray)
        y += font.Height
        font.WriteText(displayBuffer, (0, y), $"Equipped: {equipSlot.Key}", Hue.Gray)
        y += font.Height
        If item.IsWeapon Then
            font.WriteText(displayBuffer, (0, y), $"Durability: {item.WeaponDurability}/{item.MaximumWeaponDurability}", Hue.Gray)
            y += font.Height
        End If
        If item.IsArmor Then
            font.WriteText(displayBuffer, (0, y), $"Durability: {item.ArmorDurability}/{item.MaximumArmorDurability}", Hue.Gray)
            y += font.Height
        End If
    End Sub
End Class
