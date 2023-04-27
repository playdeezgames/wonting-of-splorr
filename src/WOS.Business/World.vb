Imports AOS.UI

Public Class World
    Implements IWorld
    Private ReadOnly _data As WorldData
    Const CharacterFontName = "characters"
    Const TerrainFontName = "terrains"
    Const ItemFontName = "items"
    Const CharacterFontFilename = "character-font.json"
    Const TerrainFontFilename = "terrain-font.json"
    Const ItemFontFilename = "item-font.json"
    Sub New(data As WorldData)
        _data = data
    End Sub
    Sub Initialize()
        InitializeFonts()
        InitializeTerrains()
        InitializeMaps()
    End Sub

    Private Sub InitializeFonts()
        _data.Fonts.Add(CharacterFontName, JsonSerializer.Deserialize(Of FontData)(CharacterFontFilename))
        _data.Fonts.Add(TerrainFontName, JsonSerializer.Deserialize(Of FontData)(TerrainFontFilename))
        _data.Fonts.Add(ItemFontName, JsonSerializer.Deserialize(Of FontData)(ItemFontFilename))
    End Sub

    Private Sub InitializeMaps()
        Throw New NotImplementedException()
    End Sub

    Private Sub InitializeTerrains()
        Throw New NotImplementedException()
    End Sub
End Class
