Public Interface IWorld
    Sub Initialize()
    Function GetMap(mapName As String) As IMap
    Function GetTerrain(terrainName As String) As ITerrain
    ReadOnly Property Avatar As IAvatar
End Interface
