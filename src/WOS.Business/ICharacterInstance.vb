Public Interface ICharacterInstance
    ReadOnly Property Character As ICharacter
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    ReadOnly Property Map As IMap
    ReadOnly Property HasMessage As Boolean
    ReadOnly Property Message As IEnumerable(Of (Hue, String))
    Property Target As ICharacterInstance
    Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer)
    Sub Move(direction As Direction)
    Sub Teleport(mapName As String, column As Integer, row As Integer)
    Sub AddMessage(lines As IEnumerable(Of (Hue, String)))
    Sub NextMessage()
    ReadOnly Property Name As String
    Property Health As Integer
    ReadOnly Property MaximumHealth As Integer
    ReadOnly Property IsDead As Boolean
    Function GetStatistic(statisticType As StatisticType) As Integer
    Sub Attack(target As ICharacterInstance)
    Function RollDefend() As Integer
    Function RollAttack() As Integer
    Sub SetStatistic(statisticType As StatisticType, value As Integer)
    Sub Die()
    Sub PickUpGroundItem()
    ReadOnly Property MaximumDefend As Integer
    ReadOnly Property MaximumAttack As Integer
End Interface
