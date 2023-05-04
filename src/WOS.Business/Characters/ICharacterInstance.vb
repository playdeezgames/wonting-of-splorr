Public Interface ICharacterInstance
    ReadOnly Property Character As ICharacter
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    ReadOnly Property Map As IMap
    ReadOnly Property HasMessage As Boolean
    ReadOnly Property Message As IMessage
    Property Target As ICharacterInstance
    Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer)
    Sub Move(direction As Direction)
    Sub Teleport(mapName As String, column As Integer, row As Integer)
    Sub AddMessage(sfx As Sfx?, lines As IEnumerable(Of (Hue, String)))
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
    Function CanTrade(trade As ITrade) As Boolean
    Sub MakeTrade(trade As ITrade)
    Sub Use(itemInstance As IItemInstance)
    Sub Equip(item As IItemInstance)
    ReadOnly Property MaximumDefend As Integer
    ReadOnly Property MaximumAttack As Integer
    ReadOnly Property Items As IEnumerable(Of IItemInstance)
    Property Shoppe As IShoppe
    ReadOnly Property DeathSfx As Sfx?
    ReadOnly Property HitSfx As Sfx?
    ReadOnly Property CanUseItem As Boolean
    ReadOnly Property UsableItems As IEnumerable(Of IItemInstance)
    Function HasEquipment(equipSlot As EquipSlot) As Boolean
    Sub Unequip(equipSlot As EquipSlot)
    ReadOnly Property HasItems As Boolean
    ReadOnly Property Equipment As IReadOnlyDictionary(Of EquipSlot, IItemInstance)
End Interface
