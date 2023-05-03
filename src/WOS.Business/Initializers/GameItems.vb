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
        InitializeHealingItem(data, PotionItemName, " "c, Hue.Red, 5, stacks:=True)
        InitializeItem(data, SwordItemName, "!"c, Hue.DarkGray)
        InitializeItem(data, CrownsItemName, """"c, Hue.Yellow, stacks:=True)
        InitializeItem(data, JoolsItemName, "#"c, Hue.LightCyan, stacks:=True)
        InitializeItem(data, ClubItemName, "$"c, Hue.Brown, stacks:=True)
        InitializeItem(data, DaggerItemName, "%"c, Hue.DarkGray, stacks:=True)
        InitializeItem(data, SpearItemName, "&"c, Hue.Brown, stacks:=True)
        InitializeItem(data, AxeItemName, "'"c, Hue.DarkGray, stacks:=True)
        InitializeItem(data, WoodenShieldItemName, "("c, Hue.Brown, stacks:=True)
        InitializeItem(data, HelmetItemName, ")"c, Hue.DarkGray, stacks:=True)
        InitializeItem(data, LeatherArmorItemName, "*"c, Hue.Brown, stacks:=True)
        InitializeItem(data, ShieldItemName, "+"c, Hue.DarkGray, stacks:=True)
        InitializeItem(data, ChainMailItemName, ","c, Hue.DarkGray, stacks:=True)
        InitializeItem(data, PlateMailItemName, "-"c, Hue.Gray, stacks:=True)
        InitializeHealingItem(data, FoodItemName, "."c, Hue.Brown, 1, stacks:=True)
    End Sub

    Private Sub InitializeHealingItem(data As WorldData, itemName As String, glyph As Char, hue As Hue, healing As Integer, Optional stacks As Boolean = False)
        InitializeItem(data, itemName, glyph, hue, stacks:=stacks)
        data.Items(itemName).UseTrigger = New TriggerData With
            {
                .TriggerType = TriggerType.Healing,
                .Healing = healing
            }
    End Sub

    Private Sub InitializeItem(data As WorldData, itemName As String, glyph As Char, hue As Hue, Optional stacks As Boolean = False)
        data.Items(itemName) = New ItemData With {
            .FontName = ItemFontName,
            .Glyph = glyph,
            .Hue = hue,
            .Stacks = stacks
            }
    End Sub
End Module
