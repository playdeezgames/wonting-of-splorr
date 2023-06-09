﻿Friend MustInherit Class BaseItemInstance
    Implements IItemInstance
    Protected ReadOnly _data As WorldData
    Sub New(data As WorldData)
        _data = data
    End Sub

    Protected MustOverride ReadOnly Property ItemInstanceData As ItemInstanceData

    Public ReadOnly Property Item As IItem Implements IItemInstance.Item
        Get
            Return New Item(_data, ItemInstanceData.ItemName)
        End Get
    End Property

    Public Property Quantity As Integer Implements IItemInstance.Quantity
        Get
            Return ItemInstanceData.Quantity
        End Get
        Set(value As Integer)
            ItemInstanceData.Quantity = Math.Max(value, 0)
        End Set
    End Property

    Public ReadOnly Property CanUse As Boolean Implements IItemInstance.CanUse
        Get
            Return Item.CanUse
        End Get
    End Property

    Public ReadOnly Property UseTrigger As ITrigger Implements IItemInstance.UseTrigger
        Get
            Return Item.UseTrigger
        End Get
    End Property

    Public ReadOnly Property CanEquip As Boolean Implements IItemInstance.CanEquip
        Get
            Return Item.CanEquip
        End Get
    End Property

    Public ReadOnly Property MaximumDefend As Integer Implements IItemInstance.MaximumDefend
        Get
            Return Item.MaximumDefend
        End Get
    End Property

    Public ReadOnly Property MaximumAttack As Integer Implements IItemInstance.MaximumAttack
        Get
            Return Item.MaximumAttack
        End Get
    End Property

    Public ReadOnly Property DefendDice As Integer Implements IItemInstance.DefendDice
        Get
            Return Item.DefendDice
        End Get
    End Property

    Public ReadOnly Property AttackDice As Integer Implements IItemInstance.AttackDice
        Get
            Return Item.AttackDice
        End Get
    End Property

    Public Property WeaponDurability As Integer Implements IItemInstance.WeaponDurability
        Get
            Dim statistics = ItemInstanceData.Statistics
            Return If(statistics.ContainsKey(StatisticType.WeaponWear), MaximumWeaponDurability - statistics(StatisticType.WeaponWear), MaximumWeaponDurability)
        End Get
        Set(value As Integer)
            SetStatistic(StatisticType.WeaponWear, Math.Clamp(MaximumWeaponDurability - value, 0, MaximumWeaponDurability))
        End Set
    End Property

    Public Property ArmorDurability As Integer Implements IItemInstance.ArmorDurability
        Get
            Dim statistics = ItemInstanceData.Statistics
            Return If(statistics.ContainsKey(StatisticType.ArmorWear), MaximumArmorDurability - statistics(StatisticType.ArmorWear), MaximumArmorDurability)
        End Get
        Set(value As Integer)
            SetStatistic(StatisticType.ArmorWear, Math.Clamp(MaximumArmorDurability - value, 0, MaximumArmorDurability))
        End Set
    End Property

    Public ReadOnly Property MaximumWeaponDurability As Integer Implements IItemInstance.MaximumWeaponDurability
        Get
            Return Item.MaximumWeaponDurability
        End Get
    End Property

    Public ReadOnly Property MaximumArmorDurability As Integer Implements IItemInstance.MaximumArmorDurability
        Get
            Return Item.MaximumArmorDurability
        End Get
    End Property

    Public ReadOnly Property IsWeapon As Boolean Implements IItemInstance.IsWeapon
        Get
            Return Item.IsWeapon
        End Get
    End Property

    Public ReadOnly Property IsArmor As Boolean Implements IItemInstance.IsArmor
        Get
            Return Item.IsArmor
        End Get
    End Property

    Public Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer) Implements IItemInstance.Render
        Item.Render(displayBuffer, x, y)
    End Sub

    Public Function GetStatistic(statisticType As StatisticType) As Integer Implements IItemInstance.GetStatistic
        Return ItemInstanceData.Statistics(statisticType)
    End Function

    Public Sub SetStatistic(statisticType As StatisticType, value As Integer) Implements IItemInstance.SetStatistic
        ItemInstanceData.Statistics(statisticType) = value
    End Sub
End Class
