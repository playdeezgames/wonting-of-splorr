Public Interface IMapCell
    Property Terrain As ITerrain
    Property Character As ICharacterInstance
    ReadOnly Property Bump As ITrigger
    Sub DoTrigger(triggerKind As TriggerKind, character As ICharacterInstance)
    Sub SetTrigger(triggerKind As TriggerKind, triggerType As TriggerType)
    Function CreateCharacterInstance(characterName As String) As ICharacterInstance
End Interface
