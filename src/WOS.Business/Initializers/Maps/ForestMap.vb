Friend Module ForestMap
    Const GridColumns = 15
    Const GridRows = 15
    Const CellWidth = 3
    Const CellHeight = 3
    Friend Const MapName = "forest"
    Friend Const MapColumns = (CellWidth * GridColumns) + GridColumns + 1
    Friend Const MapRows = (CellHeight * GridRows) + GridRows + 1
    Const ShrubCount = 2
    Friend ReadOnly mazeDirections As IReadOnlyDictionary(Of Direction, MazeDirection(Of Direction)) =
        New Dictionary(Of Direction, MazeDirection(Of Direction)) From
        {
            {Direction.North, New MazeDirection(Of Direction)(Direction.South, 0, -1)},
            {Direction.East, New MazeDirection(Of Direction)(Direction.West, 1, 0)},
            {Direction.South, New MazeDirection(Of Direction)(Direction.North, 0, 1)},
            {Direction.West, New MazeDirection(Of Direction)(Direction.East, -1, 0)}
        }
    Friend Sub InitializeForest(_data As WorldData)
        Const mapName = ForestMap.MapName
        Const mapColumns = ForestMap.MapColumns
        Const mapRows = ForestMap.MapRows
        Dim maze = New Maze(Of Direction)(GridColumns, GridRows, mazeDirections)
        maze.Generate()
        Dim map = CreateMap(_data, mapName, mapColumns, mapRows, ForestTerrainName)
        For mazeColumn = 0 To GridColumns - 1
            Dim column = mazeColumn * (CellWidth + 1)
            For mazeRow = 0 To GridRows - 1
                Dim cell = maze.GetCell(mazeColumn, mazeRow)
                Dim row = mazeRow * (CellHeight + 1)
                FillMap(_data, map, column + 1, row + 1, CellWidth, CellHeight, EmptySpawnTerrainName)
                Dim door = cell.GetDoor(Direction.North)
                If door IsNot Nothing AndAlso door.Open Then
                    FillMap(_data, map, column + 1, row, CellWidth, 1, EmptySpawnTerrainName)
                End If
                door = cell.GetDoor(Direction.South)
                If door IsNot Nothing AndAlso door.Open Then
                    FillMap(_data, map, column + 1, row + CellHeight + 1, CellWidth, 1, EmptySpawnTerrainName)
                End If
                door = cell.GetDoor(Direction.West)
                If door IsNot Nothing AndAlso door.Open Then
                    FillMap(_data, map, column, row + 1, 1, CellHeight, EmptySpawnTerrainName)
                End If
                door = cell.GetDoor(Direction.East)
                If door IsNot Nothing AndAlso door.Open Then
                    FillMap(_data, map, column + CellWidth + 1, row + 1, 1, CellHeight, EmptySpawnTerrainName)
                End If
                Dim shrubs = ShrubCount
                While shrubs > 0
                    Dim x = RNG.FromRange(column + 1, column + CellWidth)
                    Dim y = RNG.FromRange(row + 1, row + CellHeight)
                    FillMap(_data, map, x, y, 1, 1, ForestTerrainName)
                    shrubs -= 1
                End While
            Next
        Next
        FillMap(_data, map, mapColumns \ 2 - 1, mapRows \ 2 - 1, 3, 3, EmptyTerrainName)
        FillMap(_data, map, mapColumns \ 2, mapRows \ 2, 1, 1, TownTerrainName)
        FillMap(_data, map, 1, 1, 3, 3, EmptyTerrainName)
        FillMap(_data, map, mapColumns - 4, 1, 3, 3, EmptyTerrainName)
        FillMap(_data, map, 1, mapRows - 4, 3, 3, EmptyTerrainName)
        FillMap(_data, map, mapColumns - 4, mapRows - 4, 3, 3, EmptyTerrainName)
        PopulateMap(map, forestSpawns)
        CreateTeleportTrigger(map, mapColumns \ 2, mapRows \ 2, TownMap.MapName, TownMap.MapColumns \ 2, TownMap.MapRows - 2)
        FillMap(_data, map, 2, 2, 1, 1, DownStairsTerrainName)
        CreateTeleportTrigger(map, 2, 2, GraveyardMap.MapName, GraveyardMap.MapColumns \ 2, GraveyardMap.MapRows - 2)
        InitializeGraveyard(_data)
        FillMap(_data, map, mapColumns - 3, 2, 1, 1, TowerTerrainName)
        CreateTeleportTrigger(map, mapColumns - 3, 2, $"{TowerMap.MapName}0", TowerMap.MapColumns \ 2, TowerMap.MapRows - 2)
        InitializeTower(_data)
    End Sub
    Private ReadOnly forestSpawns As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {BlobCharacterName, 100}
        }
End Module
