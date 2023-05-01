Public Class CharacterInstanceData
    Public Property CharacterName As String
    Public Property Messages As New List(Of IEnumerable(Of (Hue, String)))
    Public Property Target As CharacterInstanceTargetData
    Public Property Statistics As New Dictionary(Of StatisticType, Integer)
    Public Property Items As New List(Of ItemInstanceData)
End Class
