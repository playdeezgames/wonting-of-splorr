Friend Class Avatar
    Implements IAvatar

    Private _data As WorldData
    Private ReadOnly Property AvatarData As AvatarData
        Get
            Return _data.Avatar
        End Get
    End Property


    Public Sub New(data As WorldData)
        Me._data = data
    End Sub

    Public ReadOnly Property Character As ICharacterInstance Implements IAvatar.Character
        Get
            If AvatarData Is Nothing Then
                Return Nothing
            End If
            Return New CharacterInstance(_data, AvatarData.MapName, AvatarData.Column, AvatarData.Row)
        End Get
    End Property
End Class
