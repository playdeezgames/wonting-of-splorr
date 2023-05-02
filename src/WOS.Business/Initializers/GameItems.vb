Friend Module GameItems
    Friend Const PotionItemName = "potion"
    Friend Const SwordItemName = "sword"
    Friend Const CrownsItemName = "crowns"
    Friend Const JoolsItemName = "jools"
    Friend Const FoodItemName = "chikkin"
    Friend Sub InitializeItems(data As WorldData)
        InitializeHealingItem(data, PotionItemName, " "c, Hue.Red, 5, stacks:=True)
        InitializeItem(data, SwordItemName, "!"c, Hue.DarkGray)
        InitializeItem(data, CrownsItemName, """"c, Hue.Yellow, stacks:=True)
        InitializeItem(data, JoolsItemName, "#"c, Hue.LightCyan, stacks:=True)
        InitializeHealingItem(data, FoodItemName, "$"c, Hue.Brown, 1, stacks:=True)
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
