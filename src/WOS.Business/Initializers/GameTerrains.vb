Friend Module GameTerrains
    Friend Const EmptySpawnTerrainName = "empty-spawn"
    Friend Const EmptyTerrainName = "empty"
    Friend Const WallTerrainName = "wall"
    Friend Const OpenDoorTerrainName = "open-door"
    Friend Const LadderTerrainName = "ladder"
    Friend Const ClosedDoorTerrainName = "closed-door"
    Friend Const DownStairsTerrainName = "down-stairs"
    Friend Const UpStairsTerrainName = "up-stairs"
    Friend Const TreeTerrainName = "tree"
    Friend Const GateTerrainName = "gate"
    Friend Const FenceTerrainName = "fence"
    Friend Const HouseTerrainName = "house"
    Friend Const TownTerrainName = "town"
    Friend Const ForestTerrainName = "forest"
    Friend Const LeftCounterTerrainName = "left-counter"
    Friend Const CounterTerrainName = "counter"
    Friend Const RightCounterTerrainName = "right-counter"
    Friend Const Path0TerrainName = "Path0"
    Friend Const Path1TerrainName = "Path1"
    Friend Const Path2TerrainName = "Path2"
    Friend Const Path3TerrainName = "Path3"
    Friend Const Path4TerrainName = "Path4"
    Friend Const Path5TerrainName = "Path5"
    Friend Const Path6TerrainName = "Path6"
    Friend Const Path7TerrainName = "Path7"
    Friend Const Path8TerrainName = "Path8"
    Friend Const Path9TerrainName = "Path9"
    Friend Const PathATerrainName = "PathA"
    Friend Const PathBTerrainName = "PathB"
    Friend Const PathCTerrainName = "PathC"
    Friend Const PathDTerrainName = "PathD"
    Friend Const PathETerrainName = "PathE"
    Friend Const PathFTerrainName = "PathF"
    Friend Const SignTerrainName = "Sign"

    Private Sub InitializeTerrain(_data As WorldData, terrainName As String, glyph As Char, hue As Hue, tenantability As Boolean, Optional canSpawn As Boolean = False)
        _data.Terrains.Add(terrainName, New TerrainData With {.FontName = TerrainFontName, .Glyph = glyph, .Hue = hue, .Tenantable = tenantability, .CanSpawn = canSpawn})
    End Sub
    Friend Sub InitializeTerrains(_data As WorldData)
        _data.Terrains.Clear()
        InitializeTerrain(_data, EmptySpawnTerrainName, " "c, Hue.Black, True, canSpawn:=True)
        InitializeTerrain(_data, EmptyTerrainName, " "c, Hue.Black, True)
        InitializeTerrain(_data, WallTerrainName, "!"c, Hue.Red, False)
        InitializeTerrain(_data, OpenDoorTerrainName, """"c, Hue.Brown, False)
        InitializeTerrain(_data, LadderTerrainName, "#"c, Hue.Brown, False)
        InitializeTerrain(_data, ClosedDoorTerrainName, "$"c, Hue.Brown, False)
        InitializeTerrain(_data, DownStairsTerrainName, "%"c, Hue.Brown, False)
        InitializeTerrain(_data, UpStairsTerrainName, "&"c, Hue.Brown, False)
        InitializeTerrain(_data, TreeTerrainName, "'"c, Hue.Green, False)
        InitializeTerrain(_data, GateTerrainName, "("c, Hue.Brown, False)
        InitializeTerrain(_data, FenceTerrainName, ")"c, Hue.Gray, False)
        InitializeTerrain(_data, HouseTerrainName, "*"c, Hue.Red, False)
        InitializeTerrain(_data, TownTerrainName, "+"c, Hue.Red, False)
        InitializeTerrain(_data, ForestTerrainName, ","c, Hue.Green, False)
        InitializeTerrain(_data, LeftCounterTerrainName, "-"c, Hue.Brown, False)
        InitializeTerrain(_data, CounterTerrainName, "."c, Hue.Brown, False)
        InitializeTerrain(_data, RightCounterTerrainName, "/"c, Hue.Brown, False)
        InitializeTerrain(_data, Path0TerrainName, "0"c, Hue.DarkGray, True)
        InitializeTerrain(_data, Path1TerrainName, "1"c, Hue.DarkGray, True)
        InitializeTerrain(_data, Path2TerrainName, "2"c, Hue.DarkGray, True)
        InitializeTerrain(_data, Path3TerrainName, "3"c, Hue.DarkGray, True)
        InitializeTerrain(_data, Path4TerrainName, "4"c, Hue.DarkGray, True)
        InitializeTerrain(_data, Path5TerrainName, "5"c, Hue.DarkGray, True)
        InitializeTerrain(_data, Path6TerrainName, "6"c, Hue.DarkGray, True)
        InitializeTerrain(_data, Path7TerrainName, "7"c, Hue.DarkGray, True)
        InitializeTerrain(_data, Path8TerrainName, "8"c, Hue.DarkGray, True)
        InitializeTerrain(_data, Path9TerrainName, "9"c, Hue.DarkGray, True)
        InitializeTerrain(_data, PathATerrainName, ":"c, Hue.DarkGray, True)
        InitializeTerrain(_data, PathBTerrainName, ";"c, Hue.DarkGray, True)
        InitializeTerrain(_data, PathCTerrainName, "<"c, Hue.DarkGray, True)
        InitializeTerrain(_data, PathDTerrainName, "="c, Hue.DarkGray, True)
        InitializeTerrain(_data, PathETerrainName, ">"c, Hue.DarkGray, True)
        InitializeTerrain(_data, PathFTerrainName, "?"c, Hue.DarkGray, True)
        InitializeTerrain(_data, SignTerrainName, "@"c, Hue.Brown, False)
    End Sub
End Module
