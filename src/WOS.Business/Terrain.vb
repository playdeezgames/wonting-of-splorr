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

    Private ReadOnly Property Font As Font
        Get
            Return GetCachedFont(TerrainData.FontName)
        End Get
    End Property

    Public Sub New(data As WorldData, terrainName As String)
        _data = data
        _terrainName = terrainName
    End Sub

    Public Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer) Implements ITerrain.Render
        Dim font As Font = Me.Font
        font.WriteText(displayBuffer, (x, y), $"{TerrainData.Glyph}", TerrainData.Hue)
    End Sub
End Class
