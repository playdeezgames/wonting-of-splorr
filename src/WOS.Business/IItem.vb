Public Interface IItem
    ReadOnly Property Name As String
    ReadOnly Property Stacks As Boolean
    Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer)
End Interface
