Friend Module TownInitializer
    Friend Const TownMapName = "town"
    Friend Const TownColumns = 25
    Friend Const TownRows = 25
    Friend Sub InitializeTown(_data As WorldData)
        Dim map As IMap = CreateMap(_data, TownMapName, TownColumns, TownRows, EmptySpawnTerrainName)
        FillMap(_data, map, 0, 0, map.Columns, 1, FenceTerrainName)
        FillMap(_data, map, 0, map.Rows - 1, map.Columns, 1, FenceTerrainName)
        FillMap(_data, map, 0, 1, 1, map.Rows - 2, FenceTerrainName)
        FillMap(_data, map, map.Columns - 1, 1, 1, map.Rows - 2, FenceTerrainName)
        FillMap(_data, map, TownColumns \ 2, TownRows - 1, 1, 1, GateTerrainName)
        FillMap(_data, map, 6, 7, 1, 5, Path5TerrainName)
        FillMap(_data, map, 18, 7, 1, 5, Path5TerrainName)
        FillMap(_data, map, 12, 13, 1, 5, Path5TerrainName)
        FillMap(_data, map, 12, 19, 1, 5, Path5TerrainName)
        FillMap(_data, map, 7, 12, 5, 1, PathATerrainName)
        FillMap(_data, map, 13, 12, 5, 1, PathATerrainName)
        FillMap(_data, map, 7, 18, 5, 1, PathATerrainName)
        FillMap(_data, map, 13, 18, 5, 1, PathATerrainName)
        FillMap(_data, map, 6, 12, 1, 1, Path3TerrainName)
        FillMap(_data, map, 18, 12, 1, 1, Path9TerrainName)
        FillMap(_data, map, 12, 12, 1, 1, PathETerrainName)
        FillMap(_data, map, 12, 18, 1, 1, PathFTerrainName)
        FillMap(_data, map, 6, 6, 1, 1, Path4TerrainName)
        FillMap(_data, map, 18, 6, 1, 1, Path4TerrainName)
        FillMap(_data, map, 6, 18, 1, 1, Path2TerrainName)
        FillMap(_data, map, 18, 18, 1, 1, Path8TerrainName)
        FillMap(_data, map, 6, 5, 1, 1, HouseTerrainName)
        FillMap(_data, map, 7, 5, 1, 1, SignTerrainName)
        FillMap(_data, map, 18, 5, 1, 1, HouseTerrainName)
        FillMap(_data, map, 19, 5, 1, 1, SignTerrainName)
        FillMap(_data, map, 5, 18, 1, 1, HouseTerrainName)
        FillMap(_data, map, 5, 17, 1, 1, SignTerrainName)
        FillMap(_data, map, 19, 18, 1, 1, HouseTerrainName)
        FillMap(_data, map, 19, 19, 1, 1, SignTerrainName)
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
        CreateCharacterInstance(_data, TownMapName, TownColumns \ 2, TownRows \ 2, N00bCharacterName)
        CreateAvatar(_data, TownMapName, TownColumns \ 2, TownRows \ 2)
        InitializeInn(_data)
        InitializeExchange(_data)
        InitializeArmory(_data)
        InitializeSmokeShoppe(_data)
    End Sub

    Const SmokeShoppeMapName = "smoke-shoppe"
    Const SmokeShoppeMapColumns = 7
    Const SmokeShoppeMapRows = 7
    Private Sub InitializeSmokeShoppe(_data As WorldData)
        Dim map As IMap = CreateMap(_data, SmokeShoppeMapName, SmokeShoppeMapColumns, SmokeShoppeMapRows, EmptySpawnTerrainName)
        FillMap(_data, map, 0, 0, map.Columns, 1, WallTerrainName)
        FillMap(_data, map, 0, map.Rows - 1, map.Columns, 1, WallTerrainName)
        FillMap(_data, map, 0, 1, 1, map.Rows - 2, WallTerrainName)
        FillMap(_data, map, map.Columns - 1, 1, 1, map.Rows - 2, WallTerrainName)
        FillMap(_data, map, 2, 2, 3, 1, CounterTerrainName)
        FillMap(_data, map, 1, 2, 1, 1, LeftCounterTerrainName)
        FillMap(_data, map, 5, 2, 1, 1, RightCounterTerrainName)
        FillMap(_data, map, 0, SmokeShoppeMapRows \ 2, 1, 1, ClosedDoorTerrainName)
        CreateTeleportTrigger(map, 0, SmokeShoppeMapRows \ 2, TownMapName, 18, 18)
        CreateCharacterInstance(_data, SmokeShoppeMapName, SmokeShoppeMapColumns \ 2, 1, MarcusCharacterName)
    End Sub
    Const ArmoryMapName = "armory"
    Const ArmoryMapColumns = 7
    Const ArmoryMapRows = 7
    Private Sub InitializeArmory(_data As WorldData)
        Dim map As IMap = CreateMap(_data, ArmoryMapName, ArmoryMapColumns, ArmoryMapRows, EmptySpawnTerrainName)
        FillMap(_data, map, 0, 0, map.Columns, 1, WallTerrainName)
        FillMap(_data, map, 0, map.Rows - 1, map.Columns, 1, WallTerrainName)
        FillMap(_data, map, 0, 1, 1, map.Rows - 2, WallTerrainName)
        FillMap(_data, map, map.Columns - 1, 1, 1, map.Rows - 2, WallTerrainName)
        FillMap(_data, map, 2, 2, 3, 1, CounterTerrainName)
        FillMap(_data, map, 1, 2, 1, 1, LeftCounterTerrainName)
        FillMap(_data, map, 5, 2, 1, 1, RightCounterTerrainName)
        FillMap(_data, map, ArmoryMapColumns - 1, ArmoryMapRows \ 2, 1, 1, ClosedDoorTerrainName)
        CreateTeleportTrigger(map, ArmoryMapColumns - 1, ArmoryMapRows \ 2, TownMapName, 6, 18)
        CreateCharacterInstance(_data, ArmoryMapName, ArmoryMapColumns \ 2, 1, SamuliCharacterName)
    End Sub
    Const InnMapName = "inn"
    Const InnMapColumns = 7
    Const InnMapRows = 7
    Private Sub InitializeInn(_data As WorldData)
        Dim map As IMap = CreateMap(_data, InnMapName, InnMapColumns, InnMapRows, EmptySpawnTerrainName)
        FillMap(_data, map, 0, 0, map.Columns, 1, WallTerrainName)
        FillMap(_data, map, 0, map.Rows - 1, map.Columns, 1, WallTerrainName)
        FillMap(_data, map, 0, 1, 1, map.Rows - 2, WallTerrainName)
        FillMap(_data, map, map.Columns - 1, 1, 1, map.Rows - 2, WallTerrainName)
        FillMap(_data, map, 2, 2, 3, 1, CounterTerrainName)
        FillMap(_data, map, 1, 2, 1, 1, LeftCounterTerrainName)
        FillMap(_data, map, 5, 2, 1, 1, RightCounterTerrainName)
        FillMap(_data, map, InnMapColumns \ 2, InnMapRows - 1, 1, 1, ClosedDoorTerrainName)
        CreateTeleportTrigger(map, InnMapColumns \ 2, InnMapRows - 1, TownMapName, 6, 6)
        CreateCharacterInstance(_data, InnMapName, InnMapColumns \ 2, 1, GrahamCharacterName)
    End Sub
    Const ExchangeMapName = "exchange"
    Const ExchangeMapColumns = 7
    Const ExchangeMapRows = 7
    Private Sub InitializeExchange(_data As WorldData)
        Dim map As IMap = CreateMap(_data, ExchangeMapName, ExchangeMapColumns, ExchangeMapRows, EmptySpawnTerrainName)
        FillMap(_data, map, 0, 0, map.Columns, 1, WallTerrainName)
        FillMap(_data, map, 0, map.Rows - 1, map.Columns, 1, WallTerrainName)
        FillMap(_data, map, 0, 1, 1, map.Rows - 2, WallTerrainName)
        FillMap(_data, map, map.Columns - 1, 1, 1, map.Rows - 2, WallTerrainName)
        FillMap(_data, map, 2, 2, 3, 1, CounterTerrainName)
        FillMap(_data, map, 1, 2, 1, 1, LeftCounterTerrainName)
        FillMap(_data, map, 5, 2, 1, 1, RightCounterTerrainName)
        FillMap(_data, map, ExchangeMapColumns \ 2, ExchangeMapRows - 1, 1, 1, ClosedDoorTerrainName)
        CreateTeleportTrigger(map, ExchangeMapColumns \ 2, ExchangeMapRows - 1, TownMapName, 18, 6)
        CreateCharacterInstance(_data, ExchangeMapName, ExchangeMapColumns \ 2, 1, DanCharacterName)
    End Sub

End Module
