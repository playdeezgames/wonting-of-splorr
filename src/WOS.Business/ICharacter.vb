Public Interface ICharacter
    ReadOnly Property Name As String
    Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer)
End Interface
