Public Class CharacterInstanceData
    Public Property CharacterName As String
    Public Property Messages As New List(Of MessageData)
    Public Property Target As CharacterInstanceTargetData
    Public Property Statistics As New Dictionary(Of StatisticType, Integer)
    Public Property Items As New List(Of ItemInstanceData)
    Public Property ShoppeName As String
    Public Property Equipment As New Dictionary(Of EquipSlot, ItemInstanceData)
End Class
