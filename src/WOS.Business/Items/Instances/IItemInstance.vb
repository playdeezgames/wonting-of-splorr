﻿Public Interface IItemInstance
    ReadOnly Property Item As IItem
    Property Quantity As Integer
    ReadOnly Property CanUse As Boolean
    Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer)
    ReadOnly Property UseTrigger As ITrigger
    ReadOnly Property CanEquip As Boolean
End Interface
