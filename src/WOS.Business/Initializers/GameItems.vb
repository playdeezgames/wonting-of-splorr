Friend Module GameItems
    Friend Const PotionItemName = "potion"
    Friend Const SwordItemName = "sword"
    Friend Const CrownsItemName = "crowns"
    Friend Const JoolsItemName = "jools"
    Friend Sub InitializeItems(data As WorldData)
        InitializeItem(data, PotionItemName, " "c, Hue.Red)
        InitializeItem(data, SwordItemName, "!"c, Hue.DarkGray)
        InitializeItem(data, CrownsItemName, """"c, Hue.Yellow)
        InitializeItem(data, JoolsItemName, "#"c, Hue.LightCyan)
    End Sub

    Private Sub InitializeItem(data As WorldData, itemName As String, glyph As Char, hue As Hue)
        data.Items(itemName) = New ItemData With {
            .FontName = ItemFontName,
            .Glyph = glyph,
            .Hue = hue
            }
    End Sub
End Module
