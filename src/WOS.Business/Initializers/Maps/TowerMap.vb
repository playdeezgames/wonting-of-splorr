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
        PopulateTower(_data)
    End Sub

    Private Sub PopulateTower(_data As WorldData)
        For level = 0 To TowerLevels - 1
            PopulateLevel(_data, level)
        Next
    End Sub

    Private Sub PopulateLevel(_data As WorldData, level As Integer)
        Dim map = New Map(_data, $"{MapName}{level}")
        PopulateMap(map, levelSpawns(level))
    End Sub

    Private ReadOnly levelSpawns As New List(Of IReadOnlyDictionary(Of String, Integer)) From
        {
            New Dictionary(Of String, Integer) From
            {
                {GoblinCharacterName, 48},
                {OrcCharacterName, 4}
            },
            New Dictionary(Of String, Integer) From
            {
                {GoblinCharacterName, 40},
                {OrcCharacterName, 8},
                {CyclopsCharacterName, 2}
            },
            New Dictionary(Of String, Integer) From
            {
                {GoblinCharacterName, 32},
                {OrcCharacterName, 12},
                {CyclopsCharacterName, 4}
            },
            New Dictionary(Of String, Integer) From
            {
                {GoblinCharacterName, 24},
                {OrcCharacterName, 16},
                {CyclopsCharacterName, 6}
            },
            New Dictionary(Of String, Integer) From
            {
                {GoblinCharacterName, 16},
                {OrcCharacterName, 20},
                {CyclopsCharacterName, 8}
            },
            New Dictionary(Of String, Integer) From
            {
                {GoblinCharacterName, 8},
                {OrcCharacterName, 24},
                {CyclopsCharacterName, 10}
            },
            New Dictionary(Of String, Integer) From
            {
                {OrcCharacterName, 28},
                {CyclopsCharacterName, 12}
            },
            New Dictionary(Of String, Integer) From
            {
                {OrcCharacterName, 32},
                {CyclopsCharacterName, 14}
            },
            New Dictionary(Of String, Integer) From
            {
                {OrcCharacterName, 36},
                {CyclopsCharacterName, 16}
            },
            New Dictionary(Of String, Integer) From
            {
                {OrcCharacterName, 40},
                {CyclopsCharacterName, 18}
            },
            New Dictionary(Of String, Integer) From
            {
                {OrcCharacterName, 44},
                {CyclopsCharacterName, 20}
            },
            New Dictionary(Of String, Integer) From
            {
                {OrcCharacterName, 48},
                {CyclopsCharacterName, 22}
            },
            New Dictionary(Of String, Integer) From
            {
                {OrcCharacterName, 40},
                {CyclopsCharacterName, 24}
            },
            New Dictionary(Of String, Integer) From
            {
                {OrcCharacterName, 32},
                {CyclopsCharacterName, 26}
            },
            New Dictionary(Of String, Integer) From
            {
                {OrcCharacterName, 24},
                {CyclopsCharacterName, 28}
            },
            New Dictionary(Of String, Integer) From
            {
                {OrcCharacterName, 16},
                {CyclopsCharacterName, 30}
            }
        }

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
