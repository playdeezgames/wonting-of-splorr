Friend Module FontInitializer
    Friend Const CharacterFontName = "characters"
    Friend Const TerrainFontName = "terrains"
    Friend Const ItemFontName = "items"
    Friend Const CharacterFontFilename = "Content/character-font.json"
    Friend Const TerrainFontFilename = "Content/terrain-font.json"
    Friend Const ItemFontFilename = "Content/item-font.json"

    Private Sub InitializeFont(_data As WorldData, fontName As String, fontFilename As String)
        _data.Fonts.Add(fontName, JsonSerializer.Deserialize(Of FontData)(File.ReadAllText(fontFilename)))
        SetCachedFont(fontName, New Font(_data.Fonts(fontName)))
    End Sub

    Friend Sub InitializeFonts(_data As WorldData)
        _data.Fonts.Clear()
        InitializeFont(_data, CharacterFontName, CharacterFontFilename)
        InitializeFont(_data, TerrainFontName, TerrainFontFilename)
        InitializeFont(_data, ItemFontName, ItemFontFilename)
    End Sub

End Module
