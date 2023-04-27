Friend Class Character
    Implements ICharacter
    Private ReadOnly _data As WorldData
    Private ReadOnly _characterName As String
    Private ReadOnly Property CharacterData As CharacterData
        Get
            Return _data.Characters(_characterName)
        End Get
    End Property
    Public Sub New(data As WorldData, characterName As String)
        _data = data
        _characterName = characterName
    End Sub
    Public ReadOnly Property Name As String Implements ICharacter.Name
        Get
            Return _characterName
        End Get
    End Property
End Class
