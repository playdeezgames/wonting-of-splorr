Public Interface ICharacterInstance
    ReadOnly Property Character As ICharacter
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    ReadOnly Property Map As IMap
    Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer)
End Interface
