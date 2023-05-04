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
