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
        SetCachedFont(fontName, New Font(_data.Fonts(fontName)))
    End Sub

    Private Sub InitializeFonts()
        _data.Fonts.Clear()
        InitializeFont(CharacterFontName, CharacterFontFilename)
        InitializeFont(TerrainFontName, TerrainFontFilename)
        InitializeFont(ItemFontName, ItemFontFilename)
    End Sub

    Private Sub InitializeMaps()
        _data.Maps.Clear()
        InitializeTown()
    End Sub

    Const TownMapName = "town"
    Const TownColumns = 25
    Const TownRows = 25
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
        map.GetCell(TownColumns \ 2, TownRows - 1).Terrain = GetTerrain(GateTerrainName)
        CreateCharacterInstance(TownMapName, TownColumns \ 2, TownRows \ 2, N00bCharacterName)
        FillMap(map, 6, 7, 1, 5, Path5TerrainName)
        FillMap(map, 18, 7, 1, 5, Path5TerrainName)
        FillMap(map, 12, 13, 1, 5, Path5TerrainName)
        FillMap(map, 12, 19, 1, 5, Path5TerrainName)
        FillMap(map, 7, 12, 5, 1, PathATerrainName)
        FillMap(map, 13, 12, 5, 1, PathATerrainName)
        FillMap(map, 7, 18, 5, 1, PathATerrainName)
        FillMap(map, 13, 18, 5, 1, PathATerrainName)
        FillMap(map, 6, 12, 1, 1, Path3TerrainName)
        FillMap(map, 18, 12, 1, 1, Path9TerrainName)
        FillMap(map, 12, 12, 1, 1, PathETerrainName)
        FillMap(map, 12, 18, 1, 1, PathFTerrainName)
        FillMap(map, 6, 6, 1, 1, Path4TerrainName)
        FillMap(map, 18, 6, 1, 1, Path4TerrainName)
        FillMap(map, 6, 18, 1, 1, Path2TerrainName)
        FillMap(map, 18, 18, 1, 1, Path8TerrainName)
        FillMap(map, 6, 5, 1, 1, HouseTerrainName)
        FillMap(map, 18, 5, 1, 1, HouseTerrainName)
        FillMap(map, 5, 18, 1, 1, HouseTerrainName)
        FillMap(map, 19, 18, 1, 1, HouseTerrainName)
        CreateAvatar(TownMapName, TownColumns \ 2, TownRows \ 2)
    End Sub

    Private Sub FillMap(map As IMap, x As Integer, y As Integer, w As Integer, h As Integer, terrainName As String)
        For dx = 0 To w - 1
            For dy = 0 To h - 1
                map.GetCell(x + dx, y + dy).Terrain = GetTerrain(terrainName)
            Next
        Next
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
    Const Path0TerrainName = "Path0"
    Const Path1TerrainName = "Path1"
    Const Path2TerrainName = "Path2"
    Const Path3TerrainName = "Path3"
    Const Path4TerrainName = "Path4"
    Const Path5TerrainName = "Path5"
    Const Path6TerrainName = "Path6"
    Const Path7TerrainName = "Path7"
    Const Path8TerrainName = "Path8"
    Const Path9TerrainName = "Path9"
    Const PathATerrainName = "PathA"
    Const PathBTerrainName = "PathB"
    Const PathCTerrainName = "PathC"
    Const PathDTerrainName = "PathD"
    Const PathETerrainName = "PathE"
    Const PathFTerrainName = "PathF"


    Public ReadOnly Property Avatar As IAvatar Implements IWorld.Avatar
        Get
            If _data.Avatar Is Nothing Then
                Return Nothing
            End If
            Return New Avatar(_data)
        End Get
    End Property

    Private Sub InitializeTerrain(terrainName As String, glyph As Char, hue As Hue, tenantability As Boolean)
        _data.Terrains.Add(terrainName, New TerrainData With {.FontName = TerrainFontName, .Glyph = glyph, .Hue = hue, .Tenantable = tenantability})
    End Sub
    Private Sub InitializeTerrains()
        _data.Terrains.Clear()
        InitializeTerrain(EmptyTerrainName, " "c, Hue.Black, True)
        InitializeTerrain(WallTerrainName, "!"c, Hue.Red, False)
        InitializeTerrain(OpenDoorTerrainName, """"c, Hue.Brown, False)
        InitializeTerrain(LadderTerrainName, "#"c, Hue.Brown, False)
        InitializeTerrain(ClosedDoorTerrainName, "$"c, Hue.Brown, False)
        InitializeTerrain(DownStairsTerrainName, "%"c, Hue.Brown, False)
        InitializeTerrain(UpStairsTerrainName, "&"c, Hue.Brown, False)
        InitializeTerrain(TreeTerrainName, "'"c, Hue.Green, False)
        InitializeTerrain(GateTerrainName, "("c, Hue.Brown, False)
        InitializeTerrain(FenceTerrainName, ")"c, Hue.Gray, False)
        InitializeTerrain(HouseTerrainName, "*"c, Hue.Red, False)
        InitializeTerrain(TownTerrainName, "+"c, Hue.Red, False)
        InitializeTerrain(ForestTerrainName, ","c, Hue.Green, False)
        InitializeTerrain(LeftCounterTerrainName, "-"c, Hue.Brown, False)
        InitializeTerrain(CounterTerrainName, "."c, Hue.Brown, False)
        InitializeTerrain(RightCounterTerrainName, "/"c, Hue.Brown, False)
        InitializeTerrain(Path0TerrainName, "0"c, Hue.DarkGray, True)
        InitializeTerrain(Path1TerrainName, "1"c, Hue.DarkGray, True)
        InitializeTerrain(Path2TerrainName, "2"c, Hue.DarkGray, True)
        InitializeTerrain(Path3TerrainName, "3"c, Hue.DarkGray, True)
        InitializeTerrain(Path4TerrainName, "4"c, Hue.DarkGray, True)
        InitializeTerrain(Path5TerrainName, "5"c, Hue.DarkGray, True)
        InitializeTerrain(Path6TerrainName, "6"c, Hue.DarkGray, True)
        InitializeTerrain(Path7TerrainName, "7"c, Hue.DarkGray, True)
        InitializeTerrain(Path8TerrainName, "8"c, Hue.DarkGray, True)
        InitializeTerrain(Path9TerrainName, "9"c, Hue.DarkGray, True)
        InitializeTerrain(PathATerrainName, ":"c, Hue.DarkGray, True)
        InitializeTerrain(PathBTerrainName, ";"c, Hue.DarkGray, True)
        InitializeTerrain(PathCTerrainName, "<"c, Hue.DarkGray, True)
        InitializeTerrain(PathDTerrainName, "="c, Hue.DarkGray, True)
        InitializeTerrain(PathETerrainName, ">"c, Hue.DarkGray, True)
        InitializeTerrain(PathFTerrainName, "?"c, Hue.DarkGray, True)
    End Sub

    Public Function GetMap(mapName As String) As IMap Implements IWorld.GetMap
        Return New Map(_data, mapName)
    End Function

    Public Function GetTerrain(terrainName As String) As ITerrain Implements IWorld.GetTerrain
        Return New Terrain(_data, terrainName)
    End Function
End Class
