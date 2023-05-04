﻿Friend Class Item
    Implements IItem

    Private _data As WorldData
    Private _itemName As String
    Private ReadOnly Property ItemData As ItemData
        Get
            Return _data.Items(_itemName)
        End Get
    End Property

    Public Sub New(data As WorldData, itemName As String)
        Me._data = data
        Me._itemName = itemName
    End Sub

    Public ReadOnly Property Name As String Implements IItem.Name
        Get
            Return _itemName
        End Get
    End Property
    Private ReadOnly Property Font As Font
        Get
            Return GetCachedFont(ItemData.FontName)
        End Get
    End Property

    Public ReadOnly Property Stacks As Boolean Implements IItem.Stacks
        Get
            Return ItemData.Stacks
        End Get
    End Property

    Public ReadOnly Property CanUse As Boolean Implements IItem.CanUse
        Get
            Return ItemData.UseTrigger IsNot Nothing
        End Get
    End Property

    Public ReadOnly Property UseTrigger As ITrigger Implements IItem.UseTrigger
        Get
            Return New ItemUseTrigger(_data, _itemName)
        End Get
    End Property

    Public ReadOnly Property DisplayName As String Implements IItem.DisplayName
        Get
            Return ItemData.DisplayName
        End Get
    End Property

    Public ReadOnly Property CanEquip As Boolean Implements IItem.CanEquip
        Get
            Return ItemData.EquipSlot.HasValue
        End Get
    End Property

    Public ReadOnly Property EquipSlot As EquipSlot? Implements IItem.EquipSlot
        Get
            Return ItemData.EquipSlot
        End Get
    End Property

    Public ReadOnly Property MaximumDefend As Integer Implements IItem.MaximumDefend
        Get
            Return If(HasStatistic(StatisticType.MaximumDefend), GetStatistic(StatisticType.MaximumDefend), 0)
        End Get
    End Property

    Private Function HasStatistic(statisticType As StatisticType) As Boolean
        Return ItemData.Statistics.ContainsKey(statisticType)
    End Function

    Public ReadOnly Property MaximumAttack As Integer Implements IItem.MaximumAttack
        Get
            Return If(HasStatistic(StatisticType.MaximumAttack), GetStatistic(StatisticType.MaximumAttack), 0)
        End Get
    End Property

    Public ReadOnly Property AttackDice As Integer Implements IItem.AttackDice
        Get
            Return If(HasStatistic(StatisticType.BaseAttack), GetStatistic(StatisticType.BaseAttack), 0)
        End Get
    End Property

    Public ReadOnly Property DefendDice As Integer Implements IItem.DefendDice
        Get
            Return If(HasStatistic(StatisticType.BaseDefend), GetStatistic(StatisticType.BaseDefend), 0)
        End Get
    End Property

    Public Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer) Implements IItem.Render
        Font.WriteText(displayBuffer, (x, y), $"{ItemData.Glyph}", ItemData.Hue)
    End Sub

    Public Function GetStatistic(statisticType As StatisticType) As Integer Implements IItem.GetStatistic
        Return ItemData.Statistics(statisticType)
    End Function
End Class
