Public Class CharacterInstanceData
    Public Property CharacterName As String
    Public Property Messages As New List(Of IEnumerable(Of (Hue, String)))
    Public Property Target As CharacterInstanceTargetData
End Class
