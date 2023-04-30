﻿Public Interface IItemInstance
    ReadOnly Property Item As IItem
    ReadOnly Property Quantity As Integer
    Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer)
End Interface
