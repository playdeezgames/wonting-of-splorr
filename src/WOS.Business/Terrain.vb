Friend Class Terrain
    Implements ITerrain

    Private ReadOnly _data As WorldData
    Private ReadOnly _terrainName As String
    Private ReadOnly Property TerrainData As TerrainData
        Get
            Return _data.Terrains(_terrainName)
        End Get
    End Property

    Public ReadOnly Property Name As String Implements ITerrain.Name
        Get
            Return _terrainName
        End Get
    End Property

    Public Sub New(data As WorldData, terrainName As String)
        _data = data
        _terrainName = terrainName
    End Sub
End Class
