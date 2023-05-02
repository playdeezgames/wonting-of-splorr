Public Interface IMessage
    ReadOnly Property Lines As IEnumerable(Of IMessageLine)
    ReadOnly Property Sfx As Sfx?
End Interface
