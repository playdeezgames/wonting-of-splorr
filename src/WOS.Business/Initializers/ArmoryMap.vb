Friend Module ArmoryMap
    Friend Const MapName = "armory"
    Friend Const MapColumns = 7
    Friend Const MapRows = 7
    Friend Sub InitializeArmory(_data As WorldData)
        Dim map As IMap = CreateMap(_data, MapName, MapColumns, MapRows, EmptySpawnTerrainName)
        FillMap(_data, map, 0, 0, map.Columns, 1, WallTerrainName)
        FillMap(_data, map, 0, map.Rows - 1, map.Columns, 1, WallTerrainName)
        FillMap(_data, map, 0, 1, 1, map.Rows - 2, WallTerrainName)
        FillMap(_data, map, map.Columns - 1, 1, 1, map.Rows - 2, WallTerrainName)
        FillMap(_data, map, 2, 2, 3, 1, CounterTerrainName)
        FillMap(_data, map, 1, 2, 1, 1, LeftCounterTerrainName)
        FillMap(_data, map, 5, 2, 1, 1, RightCounterTerrainName)
        FillMap(_data, map, MapColumns - 1, MapRows \ 2, 1, 1, ClosedDoorTerrainName)
        CreateTeleportTrigger(map, MapColumns - 1, MapRows \ 2, TownMap.MapName, 6, 18)
        CreateCharacterInstance(_data, MapName, MapColumns \ 2, 1, SamuliCharacterName)
    End Sub
End Module
