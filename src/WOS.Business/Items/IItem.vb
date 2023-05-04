﻿Public Interface IItem
    ReadOnly Property Name As String
    ReadOnly Property DisplayName As String
    ReadOnly Property Stacks As Boolean
    ReadOnly Property CanUse As Boolean
    Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer)
    ReadOnly Property CanEquip As Boolean
    ReadOnly Property UseTrigger As ITrigger
    ReadOnly Property EquipSlot As EquipSlot?
End Interface
