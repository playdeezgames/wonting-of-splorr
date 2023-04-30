Public Class World
    Implements IWorld
    Private ReadOnly _data As WorldData
    Sub New(data As WorldData)
        _data = data
    End Sub
    Public Sub Initialize() Implements IWorld.Initialize
        InitializeFonts(_data)
        InitializeTerrains(_data)
        InitializeCharacters(_data)
        InitializeMaps()
    End Sub

    Private Sub InitializeMaps()
        _data.Maps.Clear()
        InitializeTown()
        InitializeForest()
    End Sub

    Const ForestGridColumns = 15
    Const ForestGridRows = 15
    Const ForestGridSizeX = 3
    Const ForestGridSizeY = 3
    Const ForestMapName = "forest"
    Const ForestMapColumns = (ForestGridSizeX * ForestGridColumns) + ForestGridColumns + 1
    Const ForestMapRows = (ForestGridSizeY * ForestGridRows) + ForestGridRows + 1
    Const ForestCellShrubCount = 2
    Private Shared ReadOnly mazeDirections As IReadOnlyDictionary(Of Direction, MazeDirection(Of Direction)) =
        New Dictionary(Of Direction, MazeDirection(Of Direction)) From
        {
            {Direction.North, New MazeDirection(Of Direction)(Direction.South, 0, -1)},
            {Direction.East, New MazeDirection(Of Direction)(Direction.West, 1, 0)},
            {Direction.South, New MazeDirection(Of Direction)(Direction.North, 0, 1)},
            {Direction.West, New MazeDirection(Of Direction)(Direction.East, -1, 0)}
        }
    Private Sub InitializeForest()
        Const mapName = ForestMapName
        Const mapColumns = ForestMapColumns
        Const mapRows = ForestMapRows
        Dim maze = New Maze(Of Direction)(ForestGridColumns, ForestGridRows, mazeDirections)
        maze.Generate()
        Dim map = CreateMap(mapName, mapColumns, mapRows, ForestTerrainName)
        For mazeColumn = 0 To ForestGridColumns - 1
            Dim column = mazeColumn * (ForestGridSizeX + 1)
            For mazeRow = 0 To ForestGridRows - 1
                Dim cell = maze.GetCell(mazeColumn, mazeRow)
                Dim row = mazeRow * (ForestGridSizeY + 1)
                FillMap(map, column + 1, row + 1, ForestGridSizeX, ForestGridSizeY, EmptySpawnTerrainName)
                Dim door = cell.GetDoor(Direction.North)
                If door IsNot Nothing AndAlso door.Open Then
                    FillMap(map, column + 1, row, ForestGridSizeX, 1, EmptySpawnTerrainName)
                End If
                door = cell.GetDoor(Direction.South)
                If door IsNot Nothing AndAlso door.Open Then
                    FillMap(map, column + 1, row + ForestGridSizeY + 1, ForestGridSizeX, 1, EmptySpawnTerrainName)
                End If
                door = cell.GetDoor(Direction.West)
                If door IsNot Nothing AndAlso door.Open Then
                    FillMap(map, column, row + 1, 1, ForestGridSizeY, EmptySpawnTerrainName)
                End If
                door = cell.GetDoor(Direction.East)
                If door IsNot Nothing AndAlso door.Open Then
                    FillMap(map, column + ForestGridSizeX + 1, row + 1, 1, ForestGridSizeY, EmptySpawnTerrainName)
                End If
                Dim shrubs = ForestCellShrubCount
                While shrubs > 0
                    Dim x = RNG.FromRange(column + 1, column + ForestGridSizeX)
                    Dim y = RNG.FromRange(row + 1, row + ForestGridSizeY)
                    FillMap(map, x, y, 1, 1, ForestTerrainName)
                    shrubs -= 1
                End While
            Next
        Next
        FillMap(map, mapColumns \ 2 - 1, mapRows \ 2 - 1, 3, 3, EmptyTerrainName)
        FillMap(map, mapColumns \ 2, mapRows \ 2, 1, 1, TownTerrainName)
        FillMap(map, 1, 1, 3, 3, EmptyTerrainName)
        FillMap(map, mapColumns - 4, 1, 3, 3, EmptyTerrainName)
        FillMap(map, 1, mapRows - 4, 3, 3, EmptyTerrainName)
        FillMap(map, mapColumns - 4, mapRows - 4, 3, 3, EmptyTerrainName)
        PopulateMap(map, forestSpawns)
        CreateTeleportTrigger(map, mapColumns \ 2, mapRows \ 2, TownMapName, TownColumns \ 2, TownRows - 2)
    End Sub
    Private ReadOnly forestSpawns As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {BlobCharacterName, 100}
        }

    Private Sub PopulateMap(map As IMap, spawnCounts As IReadOnlyDictionary(Of String, Integer))
        For Each entry In spawnCounts
            SpawnCharacters(map, entry.Key, entry.Value)
        Next
    End Sub

    Private Sub SpawnCharacters(map As IMap, characterName As String, spawnCount As Integer)
        While spawnCount > 0
            SpawnCharacter(map, characterName)
            spawnCount -= 1
        End While
    End Sub

    Private Sub SpawnCharacter(map As IMap, characterName As String)
        Dim x As Integer
        Dim y As Integer
        Do
            x = RNG.FromRange(0, map.Columns - 1)
            y = RNG.FromRange(0, map.Rows - 1)
        Loop Until map.GetCell(x, y).Character Is Nothing AndAlso map.GetCell(x, y).Terrain.CanSpawn
        map.GetCell(x, y).CreateCharacterInstance(characterName)
    End Sub

    Const TownMapName = "town"
    Const TownColumns = 25
    Const TownRows = 25
    Private Sub InitializeTown()
        Dim map As IMap = CreateMap(TownMapName, TownColumns, TownRows, EmptySpawnTerrainName)
        For column = 0 To map.Columns - 1
            map.GetCell(column, 0).Terrain = GetTerrain(FenceTerrainName)
            map.GetCell(column, map.Rows - 1).Terrain = GetTerrain(FenceTerrainName)
        Next
        For row = 1 To map.Rows - 2
            map.GetCell(0, row).Terrain = GetTerrain(FenceTerrainName)
            map.GetCell(map.Columns - 1, row).Terrain = GetTerrain(FenceTerrainName)
        Next
        map.GetCell(TownColumns \ 2, TownRows - 1).Terrain = GetTerrain(GateTerrainName)
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
        FillMap(map, 7, 5, 1, 1, SignTerrainName)
        FillMap(map, 18, 5, 1, 1, HouseTerrainName)
        FillMap(map, 19, 5, 1, 1, SignTerrainName)
        FillMap(map, 5, 18, 1, 1, HouseTerrainName)
        FillMap(map, 5, 17, 1, 1, SignTerrainName)
        FillMap(map, 19, 18, 1, 1, HouseTerrainName)
        FillMap(map, 19, 19, 1, 1, SignTerrainName)
        CreateTeleportTrigger(map, TownColumns \ 2, TownRows - 1, ForestMapName, ForestMapColumns \ 2, ForestMapRows \ 2) 'TODO: go outside of town
        CreateTeleportTrigger(map, 6, 5, InnMapName, InnMapColumns \ 2, InnMapRows - 2)
        CreateTeleportTrigger(map, 18, 5, ExchangeMapName, ExchangeMapColumns \ 2, ExchangeMapRows - 2)
        CreateTeleportTrigger(map, 19, 18, SmokeShoppeMapName, 1, SmokeShoppeMapRows \ 2)
        CreateTeleportTrigger(map, 5, 18, ArmoryMapName, ArmoryMapColumns - 2, ArmoryMapRows \ 2)
        CreateMessageTrigger(map, 7, 5, New List(Of (Hue, String)) From {
                                (Hue.Gray, "The Dog's Face Inn"),
                                (Hue.Gray, "Proprietor:"),
                                (Hue.Gray, "Graham W.")
                             })
        CreateMessageTrigger(map, 19, 5, New List(Of (Hue, String)) From {
                                (Hue.Gray, """Honest"" Dan's Currency Exchange")
                             })
        CreateMessageTrigger(map, 19, 19, New List(Of (Hue, String)) From {
                                (Hue.Gray, "Marcus's Magick and Smoke Shoppe")
                             })
        CreateMessageTrigger(map, 5, 17, New List(Of (Hue, String)) From {
                                (Hue.Gray, "Samuli's Armory")
                             })
        CreateCharacterInstance(TownMapName, TownColumns \ 2, TownRows \ 2, N00bCharacterName)
        CreateAvatar(TownMapName, TownColumns \ 2, TownRows \ 2)
        InitializeInn()
        InitializeExchange()
        InitializeArmory()
        InitializeSmokeShoppe()
    End Sub

    Const SmokeShoppeMapName = "smoke-shoppe"
    Const SmokeShoppeMapColumns = 7
    Const SmokeShoppeMapRows = 7
    Private Sub InitializeSmokeShoppe()
        Dim map As IMap = CreateMap(SmokeShoppeMapName, SmokeShoppeMapColumns, SmokeShoppeMapRows, EmptySpawnTerrainName)
        For column = 0 To map.Columns - 1
            map.GetCell(column, 0).Terrain = GetTerrain(WallTerrainName)
            map.GetCell(column, map.Rows - 1).Terrain = GetTerrain(WallTerrainName)
        Next
        For row = 1 To map.Rows - 2
            map.GetCell(0, row).Terrain = GetTerrain(WallTerrainName)
            map.GetCell(map.Columns - 1, row).Terrain = GetTerrain(WallTerrainName)
        Next
        FillMap(map, 2, 2, 3, 1, CounterTerrainName)
        FillMap(map, 1, 2, 1, 1, LeftCounterTerrainName)
        FillMap(map, 5, 2, 1, 1, RightCounterTerrainName)
        FillMap(map, 0, SmokeShoppeMapRows \ 2, 1, 1, ClosedDoorTerrainName)
        CreateTeleportTrigger(map, 0, SmokeShoppeMapRows \ 2, TownMapName, 18, 18)
        CreateCharacterInstance(SmokeShoppeMapName, SmokeShoppeMapColumns \ 2, 1, MarcusCharacterName)
    End Sub
    Const ArmoryMapName = "armory"
    Const ArmoryMapColumns = 7
    Const ArmoryMapRows = 7
    Private Sub InitializeArmory()
        Dim map As IMap = CreateMap(ArmoryMapName, ArmoryMapColumns, ArmoryMapRows, EmptySpawnTerrainName)
        For column = 0 To map.Columns - 1
            map.GetCell(column, 0).Terrain = GetTerrain(WallTerrainName)
            map.GetCell(column, map.Rows - 1).Terrain = GetTerrain(WallTerrainName)
        Next
        For row = 1 To map.Rows - 2
            map.GetCell(0, row).Terrain = GetTerrain(WallTerrainName)
            map.GetCell(map.Columns - 1, row).Terrain = GetTerrain(WallTerrainName)
        Next
        FillMap(map, 2, 2, 3, 1, CounterTerrainName)
        FillMap(map, 1, 2, 1, 1, LeftCounterTerrainName)
        FillMap(map, 5, 2, 1, 1, RightCounterTerrainName)
        FillMap(map, ArmoryMapColumns - 1, ArmoryMapRows \ 2, 1, 1, ClosedDoorTerrainName)
        CreateTeleportTrigger(map, ArmoryMapColumns - 1, ArmoryMapRows \ 2, TownMapName, 6, 18)
        CreateCharacterInstance(ArmoryMapName, ArmoryMapColumns \ 2, 1, SamuliCharacterName)
    End Sub

    Private Sub CreateMessageTrigger(map As IMap, column As Integer, row As Integer, lines As List(Of (Hue, String)))
        Dim mapCell = map.GetCell(column, row)
        mapCell.SetTrigger(TriggerKind.Bump, TriggerType.Message)
        mapCell.GetTrigger(TriggerKind.Bump).SetMessage(lines)
    End Sub

    Const InnMapName = "inn"
    Const InnMapColumns = 7
    Const InnMapRows = 7
    Private Sub InitializeInn()
        Dim map As IMap = CreateMap(InnMapName, InnMapColumns, InnMapRows, EmptySpawnTerrainName)
        For column = 0 To map.Columns - 1
            map.GetCell(column, 0).Terrain = GetTerrain(WallTerrainName)
            map.GetCell(column, map.Rows - 1).Terrain = GetTerrain(WallTerrainName)
        Next
        For row = 1 To map.Rows - 2
            map.GetCell(0, row).Terrain = GetTerrain(WallTerrainName)
            map.GetCell(map.Columns - 1, row).Terrain = GetTerrain(WallTerrainName)
        Next
        FillMap(map, 2, 2, 3, 1, CounterTerrainName)
        FillMap(map, 1, 2, 1, 1, LeftCounterTerrainName)
        FillMap(map, 5, 2, 1, 1, RightCounterTerrainName)
        FillMap(map, InnMapColumns \ 2, InnMapRows - 1, 1, 1, ClosedDoorTerrainName)
        CreateTeleportTrigger(map, InnMapColumns \ 2, InnMapRows - 1, TownMapName, 6, 6)
        CreateCharacterInstance(InnMapName, InnMapColumns \ 2, 1, GrahamCharacterName)
    End Sub
    Const ExchangeMapName = "exchange"
    Const ExchangeMapColumns = 7
    Const ExchangeMapRows = 7
    Private Sub InitializeExchange()
        Dim map As IMap = CreateMap(ExchangeMapName, ExchangeMapColumns, ExchangeMapRows, EmptySpawnTerrainName)
        For column = 0 To map.Columns - 1
            map.GetCell(column, 0).Terrain = GetTerrain(WallTerrainName)
            map.GetCell(column, map.Rows - 1).Terrain = GetTerrain(WallTerrainName)
        Next
        For row = 1 To map.Rows - 2
            map.GetCell(0, row).Terrain = GetTerrain(WallTerrainName)
            map.GetCell(map.Columns - 1, row).Terrain = GetTerrain(WallTerrainName)
        Next
        FillMap(map, 2, 2, 3, 1, CounterTerrainName)
        FillMap(map, 1, 2, 1, 1, LeftCounterTerrainName)
        FillMap(map, 5, 2, 1, 1, RightCounterTerrainName)
        FillMap(map, ExchangeMapColumns \ 2, ExchangeMapRows - 1, 1, 1, ClosedDoorTerrainName)
        CreateTeleportTrigger(map, ExchangeMapColumns \ 2, ExchangeMapRows - 1, TownMapName, 18, 6)
        CreateCharacterInstance(ExchangeMapName, ExchangeMapColumns \ 2, 1, DanCharacterName)
    End Sub

    Private Sub CreateTeleportTrigger(map As IMap, fromColumn As Integer, fromRow As Integer, toMapName As String, toColumn As Integer, toRow As Integer)
        Dim mapCell = map.GetCell(fromColumn, fromRow)
        mapCell.SetTrigger(TriggerKind.Bump, TriggerType.Teleport)
        mapCell.GetTrigger(TriggerKind.Bump).SetTeleport(toMapName, toColumn, toRow)
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



    Public ReadOnly Property Avatar As IAvatar Implements IWorld.Avatar
        Get
            If _data.Avatar Is Nothing Then
                Return Nothing
            End If
            Return New Avatar(_data)
        End Get
    End Property

    Public Function GetMap(mapName As String) As IMap Implements IWorld.GetMap
        Return New Map(_data, mapName)
    End Function

    Public Function GetTerrain(terrainName As String) As ITerrain Implements IWorld.GetTerrain
        Return New Terrain(_data, terrainName)
    End Function
End Class
