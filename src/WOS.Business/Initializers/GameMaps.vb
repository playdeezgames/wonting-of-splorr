Friend Module GameMaps

    Friend Sub InitializeMaps(_data As WorldData)
        _data.Maps.Clear()
        InitializeTown(_data)
        InitializeForest(_data)
    End Sub


    Friend Sub PopulateMap(map As IMap, spawnCounts As IReadOnlyDictionary(Of String, Integer))
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


    Friend Sub CreateMessageTrigger(map As IMap, column As Integer, row As Integer, lines As List(Of (Hue, String)))
        Dim mapCell = map.GetCell(column, row)
        mapCell.SetTrigger(TriggerKind.Bump, TriggerType.Message)
        mapCell.GetTrigger(TriggerKind.Bump).SetMessage(lines)
    End Sub


    Friend Sub CreateTeleportTrigger(map As IMap, fromColumn As Integer, fromRow As Integer, toMapName As String, toColumn As Integer, toRow As Integer)
        Dim mapCell = map.GetCell(fromColumn, fromRow)
        mapCell.SetTrigger(TriggerKind.Bump, TriggerType.Teleport)
        mapCell.GetTrigger(TriggerKind.Bump).SetTeleport(toMapName, toColumn, toRow)
    End Sub

    Friend Sub FillMap(_data As WorldData, map As IMap, x As Integer, y As Integer, w As Integer, h As Integer, terrainName As String)
        For dx = 0 To w - 1
            For dy = 0 To h - 1
                map.GetCell(x + dx, y + dy).Terrain = New Terrain(_data, terrainName)
            Next
        Next
    End Sub

    Friend Sub CreateAvatar(_data As WorldData, mapName As String, column As Integer, row As Integer)
        _data.Avatar = New AvatarData With {.MapName = mapName, .Column = column, .Row = row}
    End Sub

    Friend Function CreateCharacterInstance(_data As WorldData, mapName As String, column As Integer, row As Integer, characterName As String) As ICharacterInstance
        Return New Map(_data, mapName).GetCell(column, row).CreateCharacterInstance(characterName)
    End Function

    Friend Function CreateMap(_data As WorldData, mapName As String, columns As Integer, rows As Integer, terrainName As String) As IMap
        _data.Maps(mapName) = New MapData(columns, rows, terrainName)
        Return New Map(_data, mapName)
    End Function

End Module
