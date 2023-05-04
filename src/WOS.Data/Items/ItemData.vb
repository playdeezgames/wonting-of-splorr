Public Class ItemData
    Inherits SpriteData
    Public Property Stacks As Boolean
    Public Property UseTrigger As TriggerData
    Public Property DisplayName As String
    Public Property EquipSlot As EquipSlot?
    Public Property Statistics As New Dictionary(Of StatisticType, Integer)
End Class
