Friend Module ForestInitializer
    Const ForestGridColumns = 15
    Const ForestGridRows = 15
    Const ForestGridSizeX = 3
    Const ForestGridSizeY = 3
    Friend Const ForestMapName = "forest"
    Friend Const ForestMapColumns = (ForestGridSizeX * ForestGridColumns) + ForestGridColumns + 1
    Friend Const ForestMapRows = (ForestGridSizeY * ForestGridRows) + ForestGridRows + 1
    Const ForestCellShrubCount = 2
    Private ReadOnly mazeDirections As IReadOnlyDictionary(Of Direction, MazeDirection(Of Direction)) =
        New Dictionary(Of Direction, MazeDirection(Of Direction)) From
        {
            {Direction.North, New MazeDirection(Of Direction)(Direction.South, 0, -1)},
            {Direction.East, New MazeDirection(Of Direction)(Direction.West, 1, 0)},
            {Direction.South, New MazeDirection(Of Direction)(Direction.North, 0, 1)},
            {Direction.West, New MazeDirection(Of Direction)(Direction.East, -1, 0)}
        }
    Friend Sub InitializeForest(_data As WorldData)
        Const mapName = ForestMapName
        Const mapColumns = ForestMapColumns
        Const mapRows = ForestMapRows
        Dim maze = New Maze(Of Direction)(ForestGridColumns, ForestGridRows, mazeDirections)
        maze.Generate()
        Dim map = CreateMap(_data, mapName, mapColumns, mapRows, ForestTerrainName)
        For mazeColumn = 0 To ForestGridColumns - 1
            Dim column = mazeColumn * (ForestGridSizeX + 1)
            For mazeRow = 0 To ForestGridRows - 1
                Dim cell = maze.GetCell(mazeColumn, mazeRow)
                Dim row = mazeRow * (ForestGridSizeY + 1)
                FillMap(_data, map, column + 1, row + 1, ForestGridSizeX, ForestGridSizeY, EmptySpawnTerrainName)
                Dim door = cell.GetDoor(Direction.North)
                If door IsNot Nothing AndAlso door.Open Then
                    FillMap(_data, map, column + 1, row, ForestGridSizeX, 1, EmptySpawnTerrainName)
                End If
                door = cell.GetDoor(Direction.South)
                If door IsNot Nothing AndAlso door.Open Then
                    FillMap(_data, map, column + 1, row + ForestGridSizeY + 1, ForestGridSizeX, 1, EmptySpawnTerrainName)
                End If
                door = cell.GetDoor(Direction.West)
                If door IsNot Nothing AndAlso door.Open Then
                    FillMap(_data, map, column, row + 1, 1, ForestGridSizeY, EmptySpawnTerrainName)
                End If
                door = cell.GetDoor(Direction.East)
                If door IsNot Nothing AndAlso door.Open Then
                    FillMap(_data, map, column + ForestGridSizeX + 1, row + 1, 1, ForestGridSizeY, EmptySpawnTerrainName)
                End If
                Dim shrubs = ForestCellShrubCount
                While shrubs > 0
                    Dim x = RNG.FromRange(column + 1, column + ForestGridSizeX)
                    Dim y = RNG.FromRange(row + 1, row + ForestGridSizeY)
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
        CreateTeleportTrigger(map, mapColumns \ 2, mapRows \ 2, TownMapName, TownColumns \ 2, TownRows - 2)
    End Sub
    Private ReadOnly forestSpawns As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {BlobCharacterName, 100}
        }
End Module
