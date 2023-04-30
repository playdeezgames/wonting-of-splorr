Public Interface IMapCell
    Property Terrain As ITerrain
    Property Character As ICharacterInstance
    Property Item As IItemInstance
    Function GetTrigger(triggerKind As TriggerKind) As ITrigger
    Sub DoTrigger(triggerKind As TriggerKind, character As ICharacterInstance)
    Sub SetTrigger(triggerKind As TriggerKind, triggerType As TriggerType)
    Function CreateCharacterInstance(characterName As String) As ICharacterInstance
End Interface
