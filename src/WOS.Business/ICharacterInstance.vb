Public Interface ICharacterInstance
    ReadOnly Property Character As ICharacter
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    ReadOnly Property Map As IMap
    ReadOnly Property HasMessage As Boolean
    ReadOnly Property Message As IEnumerable(Of (Hue, String))
    Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer)
    Sub Move(direction As Direction)
    Sub Teleport(mapName As String, column As Integer, row As Integer)
    Sub AddMessage(lines As IEnumerable(Of (Hue, String)))
    Sub NextMessage()
End Interface
