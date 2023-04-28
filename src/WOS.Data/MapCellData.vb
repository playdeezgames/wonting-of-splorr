Public Class MapCellData
    Public Property TerrainName As String
    Public Property Item As ItemInstanceData
    Public Property Character As CharacterInstanceData
    Public Property Triggers As New Dictionary(Of TriggerKind, TriggerData)
End Class
