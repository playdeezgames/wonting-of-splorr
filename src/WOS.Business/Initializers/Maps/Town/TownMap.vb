Friend Module TownMap
    Friend Const MapName = "town"
    Friend Const MapColumns = 25
    Friend Const MapRows = 25
    Friend Sub InitializeTown(_data As WorldData)
        Dim map As IMap = CreateMap(_data, MapName, MapColumns, MapRows, EmptySpawnTerrainName)
        FillMap(_data, map, 0, 0, map.Columns, 1, FenceTerrainName)
        FillMap(_data, map, 0, map.Rows - 1, map.Columns, 1, FenceTerrainName)
        FillMap(_data, map, 0, 1, 1, map.Rows - 2, FenceTerrainName)
        FillMap(_data, map, map.Columns - 1, 1, 1, map.Rows - 2, FenceTerrainName)
        FillMap(_data, map, MapColumns \ 2, MapRows - 1, 1, 1, GateTerrainName)
        FillMap(_data, map, 6, 7, 1, 5, Path5TerrainName)
        FillMap(_data, map, 12, 7, 1, 5, Path5TerrainName)
        FillMap(_data, map, 18, 7, 1, 5, Path5TerrainName)
        FillMap(_data, map, 12, 13, 1, 5, Path5TerrainName)
        FillMap(_data, map, 12, 19, 1, 5, Path5TerrainName)
        FillMap(_data, map, 7, 12, 5, 1, PathATerrainName)
        FillMap(_data, map, 13, 12, 5, 1, PathATerrainName)
        FillMap(_data, map, 7, 18, 5, 1, PathATerrainName)
        FillMap(_data, map, 13, 18, 5, 1, PathATerrainName)
        FillMap(_data, map, 6, 12, 1, 1, Path3TerrainName)
        FillMap(_data, map, 18, 12, 1, 1, Path9TerrainName)
        FillMap(_data, map, 12, 12, 1, 1, PathFTerrainName)
        FillMap(_data, map, 12, 18, 1, 1, PathFTerrainName)
        FillMap(_data, map, 6, 6, 1, 1, Path4TerrainName)
        FillMap(_data, map, 12, 6, 1, 1, Path4TerrainName)
        FillMap(_data, map, 18, 6, 1, 1, Path4TerrainName)
        FillMap(_data, map, 6, 18, 1, 1, Path2TerrainName)
        FillMap(_data, map, 18, 18, 1, 1, Path8TerrainName)
        FillMap(_data, map, 6, 5, 1, 1, HouseTerrainName)
        FillMap(_data, map, 7, 5, 1, 1, SignTerrainName)
        FillMap(_data, map, 12, 5, 1, 1, HouseTerrainName)
        FillMap(_data, map, 13, 5, 1, 1, SignTerrainName)
        FillMap(_data, map, 18, 5, 1, 1, HouseTerrainName)
        FillMap(_data, map, 19, 5, 1, 1, SignTerrainName)
        FillMap(_data, map, 5, 18, 1, 1, HouseTerrainName)
        FillMap(_data, map, 5, 17, 1, 1, SignTerrainName)
        FillMap(_data, map, 19, 18, 1, 1, HouseTerrainName)
        FillMap(_data, map, 19, 19, 1, 1, SignTerrainName)
        CreateTeleportTrigger(map, MapColumns \ 2, MapRows - 1, ForestMap.MapName, ForestMap.MapColumns \ 2, ForestMap.MapRows \ 2)
        CreateTeleportTrigger(map, 6, 5, InnMap.MapName, InnMap.MapColumns \ 2, InnMap.MapRows - 2)
        CreateTeleportTrigger(map, 12, 5, ExchangeMap.MapName, ExchangeMap.MapColumns \ 2, ExchangeMap.MapRows - 2)
        CreateTeleportTrigger(map, 18, 5, ArmoryMap.MapName, ArmoryMap.MapColumns \ 2, ArmoryMap.MapRows - 2)
        CreateTeleportTrigger(map, 19, 18, SmokeShoppeMap.MapName, 1, SmokeShoppeMap.MapRows \ 2)
        CreateTeleportTrigger(map, 5, 18, BlacksmithMap.MapName, BlacksmithMap.MapColumns - 2, BlacksmithMap.MapRows \ 2)
        CreateMessageTrigger(map, 7, 5, New List(Of (Hue, String)) From {
                                (Hue.Gray, "The Dog's Face Inn"),
                                (Hue.Gray, "Proprietor:"),
                                (Hue.Gray, "Graham W.")
                             })
        CreateMessageTrigger(map, 13, 5, New List(Of (Hue, String)) From {
                                (Hue.Gray, """Honest"" Dan's Currency Exchange")
                             })
        CreateMessageTrigger(map, 19, 5, New List(Of (Hue, String)) From {
                                (Hue.Gray, "David's Armory")
                             })
        CreateMessageTrigger(map, 19, 19, New List(Of (Hue, String)) From {
                                (Hue.Gray, "Marcus's Magick and Smoke Shoppe")
                             })
        CreateMessageTrigger(map, 5, 17, New List(Of (Hue, String)) From {
                                (Hue.Gray, "Samuli's Blacksmith Shoppe")
                             })
        CreateCharacterInstance(_data, MapName, MapColumns \ 2, MapRows \ 2, N00bCharacterName)
        CreateAvatar(_data, MapName, MapColumns \ 2, MapRows \ 2)
        InitializeInn(_data)
        InitializeExchange(_data)
        InitializeBlacksmith(_data)
        InitializeSmokeShoppe(_data)
        InitializeArmory(_data)
        PopulateMap(map, townSpawns)
    End Sub
    Private ReadOnly townSpawns As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {ChickenCharacterName, 10}
        }
End Module
