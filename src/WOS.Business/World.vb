Public Class World
    Implements IWorld
    Private ReadOnly _data As WorldData
    Sub New(data As WorldData)
        _data = data
    End Sub
    Public Sub Initialize() Implements IWorld.Initialize
        InitializeFonts(_data)
        InitializeTerrains(_data)
        InitializeCharacters(_data)
        InitializeMaps(_data)
    End Sub

    Public ReadOnly Property Avatar As IAvatar Implements IWorld.Avatar
        Get
            If _data.Avatar Is Nothing Then
                Return Nothing
            End If
            Return New Avatar(_data)
        End Get
    End Property

    Public Function GetMap(mapName As String) As IMap Implements IWorld.GetMap
        Return New Map(_data, mapName)
    End Function

    Public Function GetTerrain(terrainName As String) As ITerrain Implements IWorld.GetTerrain
        Return New Terrain(_data, terrainName)
    End Function
End Class
