Imports AOS.UI

Public Class World
    Implements IWorld
    Private ReadOnly _data As WorldData
    Const CharacterFontName = "characters"
    Const TerrainFontName = "terrains"
    Const ItemFontName = "items"
    Const CharacterFontFilename = "Content/character-font.json"
    Const TerrainFontFilename = "Content/terrain-font.json"
    Const ItemFontFilename = "Content/item-font.json"
    Sub New(data As WorldData)
        _data = data
    End Sub
    Public Sub Initialize() Implements IWorld.Initialize
        InitializeFonts()
        InitializeTerrains()
        InitializeMaps()
    End Sub

    Private Sub InitializeFont(fontName As String, fontFilename As String)
        _data.Fonts.Add(fontName, JsonSerializer.Deserialize(Of FontData)(File.ReadAllText(fontFilename)))
    End Sub

    Private Sub InitializeFonts()
        InitializeFont(CharacterFontName, CharacterFontFilename)
        InitializeFont(TerrainFontName, ItemFontFilename)
        InitializeFont(ItemFontName, TerrainFontFilename)
    End Sub

    Private Sub InitializeMaps()
        Throw New NotImplementedException()
    End Sub

    Private Sub InitializeTerrains()
        Throw New NotImplementedException()
    End Sub
End Class
