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
        InitializeHealingItem(data, PotionItemName, "Potion", " "c, Hue.Red, 5, stacks:=True)
        InitializeItem(data, SwordItemName, "Sword", "!"c, Hue.DarkGray)
        InitializeItem(data, CrownsItemName, "Crowns", """"c, Hue.Yellow, stacks:=True)
        InitializeItem(data, JoolsItemName, "Jools", "#"c, Hue.LightCyan, stacks:=True)
        InitializeItem(data, ClubItemName, "Club", "$"c, Hue.Brown, stacks:=True)
        InitializeItem(data, DaggerItemName, "Dagger", "%"c, Hue.DarkGray, stacks:=True)
        InitializeItem(data, SpearItemName, "Spear", "&"c, Hue.Brown, stacks:=True)
        InitializeItem(data, AxeItemName, "Axe", "'"c, Hue.DarkGray, stacks:=True)
        InitializeItem(data, WoodenShieldItemName, "Wooden Shield", "("c, Hue.Brown, stacks:=True)
        InitializeItem(data, HelmetItemName, "Helmet", ")"c, Hue.DarkGray, stacks:=True)
        InitializeItem(data, LeatherArmorItemName, "Leather Armor", "*"c, Hue.Brown, stacks:=True)
        InitializeItem(data, ShieldItemName, "Shield", "+"c, Hue.DarkGray, stacks:=True)
        InitializeItem(data, ChainMailItemName, "Chain Mail", ","c, Hue.DarkGray, stacks:=True)
        InitializeItem(data, PlateMailItemName, "Plate Mail", "-"c, Hue.Gray, stacks:=True)
        InitializeHealingItem(data, FoodItemName, "Chikkin", "."c, Hue.Brown, 1, stacks:=True)
    End Sub

    Private Sub InitializeHealingItem(data As WorldData, itemName As String, displayName As String, glyph As Char, hue As Hue, healing As Integer, Optional stacks As Boolean = False)
        InitializeItem(data, itemName, displayName, glyph, hue, stacks:=stacks)
        data.Items(itemName).UseTrigger = New TriggerData With
            {
                .TriggerType = TriggerType.Healing,
                .Healing = healing
            }
    End Sub

    Private Sub InitializeItem(data As WorldData, itemName As String, displayName As String, glyph As Char, hue As Hue, Optional stacks As Boolean = False)
        data.Items(itemName) = New ItemData With {
            .DisplayName = displayName,
            .FontName = ItemFontName,
            .Glyph = glyph,
            .Hue = hue,
            .Stacks = stacks
            }
    End Sub
End Module
