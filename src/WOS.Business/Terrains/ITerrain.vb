Public Interface ITerrain
    ReadOnly Property Name As String
    ReadOnly Property Hue As Hue
    Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer)
    ReadOnly Property CanSpawn As Boolean
    ReadOnly Property Tenantable As Boolean
End Interface
