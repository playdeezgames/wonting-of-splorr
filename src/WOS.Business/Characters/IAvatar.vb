Public Interface IAvatar
    ReadOnly Property Character As ICharacterInstance
    Sub Move(direction As Direction)
End Interface
