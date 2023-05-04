Friend Module GameItems
    Friend Const PotionItemName = "potion"
    Friend Const SwordItemName = "sword"
    Friend Const CrownsItemName = "crowns"
    Friend Const JoolsItemName = "jools"
    Friend Const FoodItemName = "chikkin"
    Friend Const ClubItemName = "club"
    Friend Const DaggerItemName = "dagger"
    Friend Const SpearItemName = "spear"
    Friend Const AxeItemName = "axe"
    Friend Const WoodenShieldItemName = "woodenshield"
    Friend Const HelmetItemName = "helmet"
    Friend Const ShieldItemName = "shield"
    Friend Const ChainMailItemName = "chainmail"
    Friend Const PlateMailItemName = "platemail"
    Friend Const LeatherArmorItemName = "leatherarmor"
    Friend Sub InitializeItems(data As WorldData)
        InitializeItem(data, CrownsItemName, "Crowns", """"c, Hue.Yellow, stacks:=True)
        InitializeItem(data, JoolsItemName, "Jools", "#"c, Hue.LightCyan, stacks:=True)

        InitializeHealingItem(data, FoodItemName, "Chikkin", "."c, Hue.Brown, 1, stacks:=True)
        InitializeHealingItem(data, PotionItemName, "Potion", " "c, Hue.Red, 5, stacks:=True)

        InitializeWeaponItem(data, SwordItemName, "Sword", "!"c, Hue.DarkGray, 6, 2, 100, equipSlot:=EquipSlot.Weapon)
        InitializeWeaponItem(data, ClubItemName, "Club", "$"c, Hue.Brown, 3, 1, 250, equipSlot:=EquipSlot.Weapon)
        InitializeWeaponItem(data, DaggerItemName, "Dagger", "%"c, Hue.DarkGray, 3, 1, 150, equipSlot:=EquipSlot.Weapon)
        InitializeWeaponItem(data, SpearItemName, "Spear", "&"c, Hue.Brown, 3, 2, 50, equipSlot:=EquipSlot.Weapon)
        InitializeWeaponItem(data, AxeItemName, "Axe", "'"c, Hue.DarkGray, 9, 3, 75, equipSlot:=EquipSlot.Weapon)

        InitializeArmorItem(data, WoodenShieldItemName, "Wooden Shield", "("c, Hue.Brown, 2, 1, 25, equipSlot:=EquipSlot.Shield)
        InitializeArmorItem(data, HelmetItemName, "Helmet", ")"c, Hue.DarkGray, 2, 0, 50, equipSlot:=EquipSlot.Head)
        InitializeArmorItem(data, LeatherArmorItemName, "Leather Armor", "*"c, Hue.Brown, 2, 1, 25, equipSlot:=EquipSlot.Torso)
        InitializeArmorItem(data, ShieldItemName, "Shield", "+"c, Hue.DarkGray, 2, 1, 75, equipSlot:=EquipSlot.Shield)
        InitializeArmorItem(data, ChainMailItemName, "Chain Mail", ","c, Hue.DarkGray, 2, 1, 50, equipSlot:=EquipSlot.Torso)
        InitializeArmorItem(data, PlateMailItemName, "Plate Mail", "-"c, Hue.Gray, 4, 2, 100, equipSlot:=EquipSlot.Torso)
    End Sub

    Private Sub InitializeHealingItem(data As WorldData, itemName As String, displayName As String, glyph As Char, hue As Hue, healing As Integer, Optional stacks As Boolean = False)
        InitializeItem(data, itemName, displayName, glyph, hue, stacks:=stacks)
        data.Items(itemName).UseTrigger = New TriggerData With
            {
                .TriggerType = TriggerType.Healing,
                .Healing = healing
            }
    End Sub
    Private Sub InitializeWeaponItem(data As WorldData, itemName As String, displayName As String, glyph As Char, hue As Hue, attack As Integer, maximumAttack As Integer, durability As Integer, Optional stacks As Boolean = False, Optional equipSlot As EquipSlot? = Nothing)
        InitializeItem(data, itemName, displayName, glyph, hue, stacks, equipSlot)
        data.Items(itemName).Statistics(StatisticType.BaseAttack) = attack
        data.Items(itemName).Statistics(StatisticType.MaximumAttack) = maximumAttack
        data.Items(itemName).Statistics(StatisticType.Durability) = durability
    End Sub
    Private Sub InitializeArmorItem(data As WorldData, itemName As String, displayName As String, glyph As Char, hue As Hue, defend As Integer, maximumDefend As Integer, durability As Integer, Optional stacks As Boolean = False, Optional equipSlot As EquipSlot? = Nothing)
        InitializeItem(data, itemName, displayName, glyph, hue, stacks, equipSlot)
        data.Items(itemName).Statistics(StatisticType.BaseDefend) = defend
        data.Items(itemName).Statistics(StatisticType.MaximumDefend) = maximumDefend
        data.Items(itemName).Statistics(StatisticType.Durability) = durability
    End Sub
    Private Sub InitializeItem(data As WorldData, itemName As String, displayName As String, glyph As Char, hue As Hue, Optional stacks As Boolean = False, Optional equipSlot As EquipSlot? = Nothing)
        data.Items(itemName) = New ItemData With {
            .DisplayName = displayName,
            .FontName = ItemFontName,
            .Glyph = glyph,
            .Hue = hue,
            .Stacks = stacks,
            .EquipSlot = equipSlot
            }
    End Sub
End Module
