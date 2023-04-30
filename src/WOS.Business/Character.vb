﻿Friend Class Character
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

    Public Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer) Implements ICharacter.Render
        Dim font As Font = Me.Font
        font.WriteText(displayBuffer, (x, y), $"{CharacterData.Glyph}", CharacterData.Hue)
    End Sub

    Public Function GetStatistic(statisticType As StatisticType) As Integer Implements ICharacter.GetStatistic
        Return CharacterData.Statistics(statisticType)
    End Function

    Public Function GenerateItemDrop() As (IItem, Integer) Implements ICharacter.GenerateItemDrop
        If Not CharacterData.ItemDrops.Any Then
            Return (Nothing, 0)
        End If
        Dim table As New Dictionary(Of Integer, Integer)
        For index = 0 To CharacterData.ItemDrops.Count - 1
            table(index) = CharacterData.ItemDrops(index).Weight
        Next
        Dim chosen = CharacterData.ItemDrops(RNG.FromGenerator(table))
        Return (New Item(_data, chosen.ItemName), chosen.Quantity)
    End Function

    Public ReadOnly Property Name As String Implements ICharacter.Name
        Get
            Return _characterName
        End Get
    End Property

    Public ReadOnly Property Font As Font
        Get
            Return GetCachedFont(CharacterData.FontName)
        End Get
    End Property

    Public ReadOnly Property IsMessageSink As Boolean Implements ICharacter.IsMessageSink
        Get
            Return CharacterData.IsMessageSink
        End Get
    End Property
End Class
