Friend Class ItemUseTrigger
    Inherits BaseTrigger

    Private _data As WorldData
    Private _itemName As String

    Public Sub New(data As WorldData, itemName As String)
        _data = data
        _itemName = itemName
    End Sub

    Protected Overrides ReadOnly Property TriggerData As TriggerData
        Get
            Return _data.Items(_itemName).UseTrigger
        End Get
    End Property
End Class
