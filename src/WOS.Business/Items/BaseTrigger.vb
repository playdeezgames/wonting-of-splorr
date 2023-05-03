Friend MustInherit Class BaseTrigger
    Implements ITrigger
    Protected MustOverride ReadOnly Property TriggerData As TriggerData

    Public ReadOnly Property TriggerType As TriggerType Implements ITrigger.TriggerType
        Get
            Return TriggerData.TriggerType
        End Get
    End Property

    Public ReadOnly Property Healing As Integer Implements ITrigger.Healing
        Get
            Return TriggerData.Healing
        End Get
    End Property

    Public Sub SetTeleport(toMapName As String, toColumn As Integer, toRow As Integer) Implements ITrigger.SetTeleport
        TriggerData.Teleport = New TeleportData With {.MapName = toMapName, .Column = toColumn, .Row = toRow}
    End Sub

    Public Sub SetMessage(lines As IEnumerable(Of (Hue, String))) Implements ITrigger.SetMessage
        TriggerData.Message = New MessageData With {
            .MessageLines = lines.Select(Function(line) New MessageLineData With
            {
                .Hue = line.Item1,
                .Text = line.Item2
            }).ToList}
    End Sub

    Public Sub SetShoppe(shoppeName As String) Implements ITrigger.SetShoppe
        TriggerData.ShoppeName = shoppeName
    End Sub
End Class
