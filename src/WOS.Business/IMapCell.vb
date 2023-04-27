Public Interface IMapCell
    Property Terrain As ITerrain
    Property Character As ICharacterInstance
    Function CreateCharacterInstance(characterName As String) As ICharacterInstance
End Interface
