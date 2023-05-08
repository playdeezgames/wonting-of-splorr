Imports System.Reflection.Emit

Friend Module TowerMap
    Friend Const MapName = "tower"
    Friend Const GridColumns = 3
    Friend Const GridRows = 3
    Friend Const GridCellWidth = 5
    Friend Const GridCellHeight = 5
    Friend Const MapColumns = GridColumns * (GridCellWidth + 1) + 1
    Friend Const MapRows = GridRows * (GridCellHeight + 1) + 1
    Friend Const TowerLevels = 16
    Friend Sub InitializeTower(_data As WorldData)
        For level = 0 To TowerLevels - 1
            InitializeLevel(_data, level)
        Next
        InitializeStairs(_data)
        InitializeBottomLevel(_data)
    End Sub

    Private Sub InitializeStairs(_data As WorldData)
        For level = 0 To TowerLevels - 2
            Dim row = GridCellHeight \ 2 + 1
            Dim column = If(level Mod 2 = 0, GridCellWidth \ 2 + 1, MapColumns - GridCellWidth \ 2 - 1)
            Dim fromMapName = $"{MapName}{level}"
            Dim toMapName = $"{MapName}{level + 1}"
            Dim fromMap = New Map(_data, fromMapName)
            Dim toMap = New Map(_data, toMapName)
            FillMap(_data, fromMap, column, row, 1, 1, UpStairsTerrainName)
            CreateTeleportTrigger(fromMap, column, row, toMapName, column, row)
            FillMap(_data, toMap, column, row, 1, 1, DownStairsTerrainName)
            CreateTeleportTrigger(toMap, column, row, fromMapName, column, row)
        Next
    End Sub

    Private Sub InitializeBottomLevel(_data As WorldData)
        Dim mapName = $"{TowerMap.MapName}0"
        Const mapColumns = TowerMap.MapColumns
        Const mapRows = TowerMap.MapRows
        Dim map = New Map(_data, mapName)
        FillMap(_data, map, mapColumns \ 2, mapRows - 1, 1, 1, ClosedDoorTerrainName)
        CreateTeleportTrigger(map, mapColumns \ 2, mapRows - 1, ForestMap.MapName, ForestMap.MapColumns - 3, 2)
        FillMap(_data, map, mapColumns \ 2 - 1, mapRows - 4, 3, 3, EmptyTerrainName)
    End Sub

    Private Sub InitializeLevel(_data As WorldData, level As Integer)
        Dim mapName = $"{TowerMap.MapName}{level}"
        Const mapColumns = TowerMap.MapColumns
        Const mapRows = TowerMap.MapRows
        Dim map = CreateMap(_data, mapName, mapColumns, mapRows, StoneTerrainName)
        FillMap(_data, map, 1, 1, mapColumns - 2, mapRows - 2, EmptySpawnTerrainName)
        Dim maze = New Maze(Of Direction)(GridColumns, GridRows, mazeDirections)
        maze.Generate()
        For mazeColumn = 0 To GridColumns - 1
            Dim column = mazeColumn * (GridCellWidth + 1)
            For mazeRow = 0 To GridRows - 1
                Dim cell = maze.GetCell(mazeColumn, mazeRow)
                Dim row = mazeRow * (GridCellHeight + 1)
                FillMap(_data, map, column, row, 1, 1, StoneTerrainName)
                Dim door = cell.GetDoor(Direction.South)
                If door IsNot Nothing AndAlso Not door.Open Then
                    FillMap(_data, map, column + 1, row + GridCellHeight + 1, GridCellWidth, 1, StoneTerrainName)
                End If
                door = cell.GetDoor(Direction.East)
                If door IsNot Nothing AndAlso Not door.Open Then
                    FillMap(_data, map, column + GridCellWidth + 1, row + 1, 1, GridCellHeight, StoneTerrainName)
                End If
            Next
        Next
    End Sub
End Module
