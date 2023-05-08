Friend Module GraveyardMap
    Friend Const MapName = "graveyard"
    Friend Const MapColumns = 99
    Friend Const MapRows = 99
    Friend Sub InitializeGraveyard(_data As WorldData)
        Const mapName = GraveyardMap.MapName
        Const mapColumns = GraveyardMap.MapColumns
        Const mapRows = GraveyardMap.MapRows
        Dim map = CreateMap(_data, mapName, mapColumns, mapRows, TombstoneTerrainName)
        FillMap(_data, map, 1, 1, mapColumns - 2, mapRows - 2, EmptySpawnTerrainName)

        FillMap(_data, map, mapColumns \ 2 - 1, mapRows - 4, 3, 3, EmptyTerrainName)
        FillMap(_data, map, mapColumns \ 2, mapRows - 1, 1, 1, UpStairsTerrainName)
        For column = 2 To mapColumns - 1 Step 2
            For row = 2 To mapRows - 1 Step 2
                FillMap(_data, map, column, row, 1, 1, TombstoneTerrainName)
            Next
        Next
        CreateTeleportTrigger(map, mapColumns \ 2, mapRows - 1, ForestMap.MapName, 2, 3)
        PopulateMap(map, graveyardSpawns)
    End Sub
    Private ReadOnly graveyardSpawns As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {
            {RatCharacterName, 250},
            {SnakeCharacterName, 250},
            {BatCharacterName, 255}
        }
End Module
