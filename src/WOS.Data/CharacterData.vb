Public Class CharacterData
    Inherits SpriteData
    Public Property IsMessageSink As Boolean
    Public Property Statistics As New Dictionary(Of StatisticType, Integer)
    Public Property ItemDrops As New List(Of CharacterItemDropData)
    Public Property DeathSfx As Sfx?
    Public Property HitSfx As Sfx?
End Class
