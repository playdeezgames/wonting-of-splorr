Friend Module GameItems
    Friend Const PotionItemName = "potion"
    Friend Const SwordItemName = "sword"
    Friend Const CrownsItemName = "crowns"
    Friend Const JoolsItemName = "jools"
    Friend Const FoodItemName = "chikkin"
    Friend Sub InitializeItems(data As WorldData)
        InitializeItem(data, PotionItemName, " "c, Hue.Red, stacks:=True)
        InitializeItem(data, SwordItemName, "!"c, Hue.DarkGray)
        InitializeItem(data, CrownsItemName, """"c, Hue.Yellow, stacks:=True)
        InitializeItem(data, JoolsItemName, "#"c, Hue.LightCyan, stacks:=True)
        InitializeItem(data, FoodItemName, "$"c, Hue.Brown, stacks:=True)
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
