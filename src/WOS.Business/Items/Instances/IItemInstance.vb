Public Interface IItemInstance
    ReadOnly Property Item As IItem
    Property Quantity As Integer
    ReadOnly Property CanUse As Boolean
    Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer)
    ReadOnly Property UseTrigger As ITrigger
    ReadOnly Property CanEquip As Boolean
    ReadOnly Property MaximumDefend As Integer
    ReadOnly Property MaximumAttack As Integer
    ReadOnly Property DefendDice As Integer
    ReadOnly Property AttackDice As Integer
    Function GetStatistic(statisticType As StatisticType) As Integer
    Sub SetStatistic(statisticType As StatisticType, value As Integer)
    Property WeaponDurability As Integer
    Property ArmorDurability As Integer
    ReadOnly Property MaximumWeaponDurability As Integer
    ReadOnly Property MaximumArmorDurability As Integer
    ReadOnly Property IsWeapon As Boolean
    ReadOnly Property IsArmor As Boolean
End Interface
