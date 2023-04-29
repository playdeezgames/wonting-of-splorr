Public Interface ITrigger
    Sub SetTeleport(toMapName As String, toColumn As Integer, toRow As Integer)
    Sub SetMessage(lines As IEnumerable(Of (Hue, String)))
End Interface
