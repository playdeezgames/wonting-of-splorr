Public Interface IMapCell
    Property Terrain As ITerrain
    Property Character As ICharacterInstance
    ReadOnly Property Bump As ITrigger
    Sub TriggerBump(character As ICharacterInstance)
    Sub SetBump(triggerType As TriggerType)
    Function CreateCharacterInstance(characterName As String) As ICharacterInstance
End Interface
