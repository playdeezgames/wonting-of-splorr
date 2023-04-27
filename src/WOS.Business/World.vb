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
        InitializeCharacters()
        InitializeMaps()
    End Sub
    Const N00bCharacterName = "n00b"
    Private Sub InitializeCharacters()
        _data.Characters.Clear()
        InitializeCharacter(N00bCharacterName, " "c, Hue.Brown)
    End Sub

    Private Sub InitializeCharacter(characterName As String, glyph As Char, hue As Hue)
        _data.Characters.Add(characterName, New CharacterData With {.FontName = CharacterFontName, .Glyph = glyph, .Hue = hue})
    End Sub

    Private Sub InitializeFont(fontName As String, fontFilename As String)
        _data.Fonts.Add(fontName, JsonSerializer.Deserialize(Of FontData)(File.ReadAllText(fontFilename)))
    End Sub

    Private Sub InitializeFonts()
        _data.Fonts.Clear()
        InitializeFont(CharacterFontName, CharacterFontFilename)
        InitializeFont(TerrainFontName, ItemFontFilename)
        InitializeFont(ItemFontName, TerrainFontFilename)
    End Sub

    Private Sub InitializeMaps()
        _data.Maps.Clear()
        InitializeTown()
    End Sub

    Const TownMapName = "town"
    Const TownColumns = 16
    Const TownRows = 9
    Private Sub InitializeTown()
        Dim map As IMap = CreateMap(TownMapName, TownColumns, TownRows, EmptyTerrainName)
        For column = 0 To map.Columns - 1
            map.GetCell(column, 0).Terrain = GetTerrain(FenceTerrainName)
            map.GetCell(column, map.Rows - 1).Terrain = GetTerrain(FenceTerrainName)
        Next
        For row = 1 To map.Rows - 2
            map.GetCell(0, row).Terrain = GetTerrain(FenceTerrainName)
            map.GetCell(map.Columns - 1, row).Terrain = GetTerrain(FenceTerrainName)
        Next
        CreateCharacterInstance(TownMapName, TownColumns \ 2, TownRows \ 2, N00bCharacterName)
        CreateAvatar(TownMapName, TownColumns \ 2, TownRows \ 2)
    End Sub

    Private Sub CreateAvatar(mapName As String, column As Integer, row As Integer)
        _data.Avatar = New AvatarData With {.MapName = mapName, .Column = column, .Row = row}
    End Sub

    Private Function CreateCharacterInstance(mapName As String, column As Integer, row As Integer, characterName As String) As ICharacterInstance
        Return GetMap(mapName).GetCell(column, row).CreateCharacterInstance(characterName)
    End Function

    Private Function CreateMap(mapName As String, columns As Integer, rows As Integer, terrainName As String) As IMap
        _data.Maps(mapName) = New MapData(columns, rows, terrainName)
        Return GetMap(mapName)
    End Function

    Const EmptyTerrainName = "empty"
    Const WallTerrainName = "wall"
    Const OpenDoorTerrainName = "open-door"
    Const LadderTerrainName = "ladder"
    Const ClosedDoorTerrainName = "closed-door"
    Const DownStairsTerrainName = "down-stairs"
    Const UpStairsTerrainName = "up-stairs"
    Const TreeTerrainName = "tree"
    Const GateTerrainName = "gate"
    Const FenceTerrainName = "fence"
    Const HouseTerrainName = "house"
    Const TownTerrainName = "town"
    Const ForestTerrainName = "forest"
    Const LeftCounterTerrainName = "left-counter"
    Const CounterTerrainName = "counter"
    Const RightCounterTerrainName = "right-counter"

    Public ReadOnly Property Avatar As IAvatar Implements IWorld.Avatar
        Get
            If _data.Avatar Is Nothing Then
                Return Nothing
            End If
            Return New Avatar(_data)
        End Get
    End Property

    Private Sub InitializeTerrain(terrainName As String, glyph As Char, hue As Hue)
        _data.Terrains.Add(terrainName, New TerrainData With {.FontName = TerrainFontName, .Glyph = glyph, .Hue = hue})
    End Sub
    Private Sub InitializeTerrains()
        _data.Terrains.Clear()
        InitializeTerrain(EmptyTerrainName, " "c, Hue.Black)
        InitializeTerrain(WallTerrainName, "!"c, Hue.Red)
        InitializeTerrain(OpenDoorTerrainName, """"c, Hue.Brown)
        InitializeTerrain(LadderTerrainName, "#"c, Hue.Brown)
        InitializeTerrain(ClosedDoorTerrainName, "$"c, Hue.Brown)
        InitializeTerrain(DownStairsTerrainName, "%"c, Hue.Brown)
        InitializeTerrain(UpStairsTerrainName, "&"c, Hue.Brown)
        InitializeTerrain(TreeTerrainName, "'"c, Hue.Green)
        InitializeTerrain(GateTerrainName, "("c, Hue.Brown)
        InitializeTerrain(FenceTerrainName, ")"c, Hue.Brown)
        InitializeTerrain(HouseTerrainName, "*"c, Hue.Red)
        InitializeTerrain(TownTerrainName, "+"c, Hue.Red)
        InitializeTerrain(ForestTerrainName, ","c, Hue.Green)
        InitializeTerrain(LeftCounterTerrainName, "-"c, Hue.Brown)
        InitializeTerrain(CounterTerrainName, "."c, Hue.Brown)
        InitializeTerrain(RightCounterTerrainName, "/"c, Hue.Brown)
    End Sub

    Public Function GetMap(mapName As String) As IMap Implements IWorld.GetMap
        Return New Map(_data, mapName)
    End Function

    Public Function GetTerrain(terrainName As String) As ITerrain Implements IWorld.GetTerrain
        Return New Terrain(_data, terrainName)
    End Function
End Class
