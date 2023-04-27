Friend Class CharacterInstance
    Implements ICharacterInstance

    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer
    Private ReadOnly Property CharacterInstanceData As CharacterInstanceData
        Get
            Dim mapData = _data.Maps(_mapName)
            Return mapData.Cells(_column + _row * mapData.Columns).Character
        End Get
    End Property

    Public Sub New(data As WorldData, mapName As String, column As Integer, row As Integer)
        _data = data
        _mapName = mapName
        _column = column
        _row = row
    End Sub

    Public ReadOnly Property Character As ICharacter Implements ICharacterInstance.Character
        Get
            Return New Character(_data, CharacterInstanceData.CharacterName)
        End Get
    End Property

    Public ReadOnly Property Column As Integer Implements ICharacterInstance.Column
        Get
            Return _column
        End Get
    End Property

    Public ReadOnly Property Row As Integer Implements ICharacterInstance.Row
        Get
            Return _row
        End Get
    End Property
End Class
