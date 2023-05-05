Public Interface ICharacter
    ReadOnly Property Name As String
    ReadOnly Property IsMessageSink As Boolean
    ReadOnly Property DeathSfx As Sfx?
    Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer)
    Function GetStatistic(statisticType As StatisticType) As Integer
    Function GenerateItemDrop() As (IItem, Integer)
    ReadOnly Property HitSfx As Sfx?
    ReadOnly Property DisplayName As String
End Interface
