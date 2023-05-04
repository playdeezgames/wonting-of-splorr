﻿Friend Class CharacterInstance
    Implements ICharacterInstance

    Private ReadOnly _data As WorldData
    Private _mapName As String
    Private _column As Integer
    Private _row As Integer
    Private ReadOnly Property MapCellData As MapCellData
        Get
            Dim mapData = _data.Maps(_mapName)
            Return mapData.Cells(_column + _row * mapData.Columns)
        End Get
    End Property
    Private ReadOnly Property CharacterInstanceData As CharacterInstanceData
        Get
            Return MapCellData.Character
        End Get
    End Property

    Public Sub New(data As WorldData, mapName As String, column As Integer, row As Integer)
        _data = data
        _mapName = mapName
        _column = column
        _row = row
    End Sub

    Public Sub Render(displayBuffer As IPixelSink(Of Hue), x As Integer, y As Integer) Implements ICharacterInstance.Render
        Character.Render(displayBuffer, x, y)
    End Sub

    Public Sub Move(direction As Direction) Implements ICharacterInstance.Move
        Dim nextColumn = direction.GetNextX(_column)
        Dim nextRow = direction.GetNextY(_row)
        If nextColumn < 0 OrElse nextRow < 0 OrElse nextColumn >= Map.Columns OrElse nextRow >= Map.Rows Then
            'no moving
            Return
        End If
        Dim nextCell = Map.GetCell(nextColumn, nextRow)
        If nextCell.Character IsNot Nothing Then
            Target = nextCell.Character
            Return
        End If
        Dim terrain = nextCell.Terrain
        If Not terrain.Tenantable Then
            nextCell.DoTrigger(TriggerKind.Bump, Me)
            Return
        End If
        Teleport(_mapName, nextColumn, nextRow)
    End Sub

    Public Sub Teleport(mapName As String, column As Integer, row As Integer) Implements ICharacterInstance.Teleport
        Dim instanceData = CharacterInstanceData
        MapCellData.Character = Nothing
        _mapName = mapName
        _column = column
        _row = row
        MapCellData.Character = instanceData
    End Sub

    Public Sub AddMessage(sfx As Sfx?, lines As IEnumerable(Of (Hue, String))) Implements ICharacterInstance.AddMessage
        If Character.IsMessageSink Then
            CharacterInstanceData.Messages.Add(New MessageData With
                                               {
                                                    .Sfx = sfx,
                                                    .MessageLines = lines.Select(Function(x) New MessageLineData With
                                               {
                                                    .Hue = x.Item1,
                                                    .Text = x.Item2
                                               }).ToList})
        End If
    End Sub

    Public Sub NextMessage() Implements ICharacterInstance.NextMessage
        CharacterInstanceData.Messages.RemoveAt(0)
    End Sub

    Public Function GetStatistic(statisticType As StatisticType) As Integer Implements ICharacterInstance.GetStatistic
        Return CharacterInstanceData.Statistics(statisticType)
    End Function

    Public Sub Attack(target As ICharacterInstance) Implements ICharacterInstance.Attack
        Dim msgSfx As Sfx? = Nothing
        Dim defend As Integer = target.RollDefend()
        Dim attack As Integer = RollAttack()
        Dim msg As New List(Of (Hue, String)) From {
            (Hue.Gray, $"{Name} attacks {target.Name}!"),
            (Hue.Gray, $"{Name} rolls attack of {attack}!"),
            (Hue.Gray, $"{target.Name} rolls defend of {defend}!")
        }
        If attack > defend Then
            Dim damage = attack - defend
            msg.Add((Hue.Gray, $"{target.Name} takes {damage} damage!"))
            target.Health -= damage
            If target.IsDead Then
                msg.Add((Hue.Gray, $"{Name} kills {target.Name}!"))
                msgSfx = target.DeathSfx
            Else
                msg.Add((Hue.Gray, $"{target.Name} has {target.Health} health"))
                msgSfx = target.HitSfx
            End If
        Else
            msgSfx = Sfx.Miss
            msg.Add((Hue.Gray, $"{Name} misses!"))
        End If
        AddMessage(msgSfx, msg)
        target.AddMessage(msgSfx, msg)
    End Sub

    Private Function RollDice(dice As Integer, maximumRoll As Integer) As Integer
        Dim roll = 0
        While dice > 0
            If RNG.FromRange(1, 6) = 6 Then
                roll += 1
            End If
            dice -= 1
        End While
        Return Math.Min(roll, maximumRoll)
    End Function

    Private ReadOnly Property DefendDice As Integer
        Get
            Return Character.GetStatistic(StatisticType.BaseDefend)
        End Get
    End Property

    Private ReadOnly Property AttackDice As Integer
        Get
            Return Character.GetStatistic(StatisticType.BaseAttack)
        End Get
    End Property

    Public Function RollDefend() As Integer Implements ICharacterInstance.RollDefend
        Return RollDice(DefendDice, MaximumDefend)
    End Function

    Public Function RollAttack() As Integer Implements ICharacterInstance.RollAttack
        Return RollDice(AttackDice, MaximumAttack)
    End Function

    Public Sub SetStatistic(statisticType As StatisticType, value As Integer) Implements ICharacterInstance.SetStatistic
        CharacterInstanceData.Statistics(statisticType) = value
    End Sub

    Public Sub Die() Implements ICharacterInstance.Die
        DropItem()
        MapCellData.Character = Nothing
    End Sub

    Private Sub DropItem()
        If MapCellData.Item IsNot Nothing Then
            Return
        End If
        Dim itemDrop As (IItem, Integer) = Character.GenerateItemDrop()
        If itemDrop.Item1 Is Nothing OrElse itemDrop.Item2 = 0 Then
            Return
        End If
        MapCellData.Item = New ItemInstanceData With
            {
                .ItemName = itemDrop.Item1.Name,
                .Quantity = itemDrop.Item2
            }
    End Sub

    Public Sub PickUpGroundItem() Implements ICharacterInstance.PickUpGroundItem
        If MapCellData.Item Is Nothing Then
            Return
        End If
        Dim itemName = MapCellData.Item.ItemName
        Dim quantity = MapCellData.Item.Quantity
        Dim item As IItem = New Item(_data, itemName)
        AddItemsToInventory(item, quantity)
        Dim msg As New List(Of (Hue, String)) From
            {
                (Hue.Green, $"{Name} takes {MapCellData.Item.Quantity} {itemName}"),
                (Hue.Gray, $"{Name} has {GetItemCount(item)} {itemName}")
            }
        MapCellData.Item = Nothing
        AddMessage(Nothing, msg)
    End Sub

    Private Sub AddItemsToInventory(item As IItem, quantity As Integer)
        If item.Stacks Then
            Dim itemInstance = CharacterInstanceData.Items.FirstOrDefault(Function(x) x.ItemName = item.Name)
            If itemInstance IsNot Nothing Then
                itemInstance.Quantity += quantity
            Else
                CharacterInstanceData.Items.Add(New ItemInstanceData With {.ItemName = item.Name, .Quantity = quantity})
            End If
        Else
            While quantity > 0
                CharacterInstanceData.Items.Add(New ItemInstanceData With {.ItemName = item.Name, .Quantity = 1})
                quantity -= 1
            End While
        End If
    End Sub

    Public Function CanTrade(trade As ITrade) As Boolean Implements ICharacterInstance.CanTrade
        Return GetItemCount(trade.FromItem.Item) >= trade.FromItem.Quantity
    End Function

    Private Function GetItemCount(item As IItem) As Integer
        Return CharacterInstanceData.Items.Where(Function(x) x.ItemName = item.Name).Sum(Function(x) x.Quantity)
    End Function

    Public Sub MakeTrade(trade As ITrade) Implements ICharacterInstance.MakeTrade
        If Not CanTrade(trade) Then
            AddMessage(Nothing, New List(Of (Hue, String)) From
                       {
                        (Hue.Red, "Cannot make trade!")
                       })
            Return
        End If
        Dim quantity = trade.FromItem.Quantity
        For Each item In Items.Where(Function(x) x.Item.Name = trade.FromItem.Item.Name)
            If quantity = 0 Then
                Exit For
            ElseIf quantity > item.Quantity Then
                quantity -= item.Quantity
                item.Quantity = 0
            Else
                item.Quantity -= quantity
                quantity = 0
            End If
        Next
        AddItemsToInventory(trade.ToItem.Item, trade.ToItem.Quantity)
        CleanUpInventory()
        AddMessage(Nothing, New List(Of (Hue, String)) From
                               {
                                (Hue.Green, "Its a deal!"),
                                (Hue.Red, $"{-trade.FromItem.Quantity} {trade.FromItem.Item.DisplayName}"),
                                (Hue.Gray, $"({GetItemCount(trade.FromItem.Item)} {trade.FromItem.Item.DisplayName} remaining)"),
                                (Hue.Green, $"+{trade.ToItem.Quantity} {trade.ToItem.Item.DisplayName}"),
                                (Hue.Gray, $"{Name} has {GetItemCount(trade.ToItem.Item)} {trade.ToItem.Item.DisplayName}")
                               })
    End Sub

    Private Sub CleanUpInventory()
        CharacterInstanceData.Items = CharacterInstanceData.Items.Where(Function(x) x.Quantity > 0).ToList
    End Sub

    Public Sub Use(itemInstance As IItemInstance) Implements ICharacterInstance.Use
        If Not itemInstance.CanUse Then
            AddMessage(Nothing, New List(Of (Hue, String)) From
                       {
                        (Hue.Red, $"{Name} cannot use {itemInstance.Item.Name}.")
                       })
            Return
        End If
        Dim trigger = itemInstance.UseTrigger
        Dim lines = New List(Of (Hue, String))
        Dim sfx As Sfx? = Nothing
        Select Case trigger.TriggerType
            Case TriggerType.Healing
                Health += trigger.Healing
                lines.Add((Hue.Green, $"{Name} now has {Health}/{MaximumHealth} health."))
        End Select
        itemInstance.Quantity -= 1
        lines.Add((Hue.Gray, $"{Name} has {GetItemCount(itemInstance.Item)} {itemInstance.Item.DisplayName} remaining."))
        AddMessage(sfx, lines)
        CleanUpInventory()
    End Sub

    Public Sub Equip(item As IItemInstance) Implements ICharacterInstance.Equip
        If Not item.CanEquip Then
            AddMessage(Nothing, New List(Of (Hue, String)) From
                       {
                        (Hue.Red, $"{Name} cannot equip {item.Item.Name}.")
                       })
            Return
        End If
        Dim equipSlot = item.Item.EquipSlot.Value
        If HasEquipment(equipSlot) Then
            Unequip(equipSlot)
        End If
        Dim sfx As Sfx? = Nothing
        Dim messageLines As New List(Of (Hue, String)) From {
            (Hue.Green, $"{Name} equips {item.Item.DisplayName} to {item.Item.EquipSlot}")
        }
        AddMessage(sfx, messageLines)
        CharacterInstanceData.Equipment(equipSlot) = New ItemInstanceData With
            {
                .ItemName = item.Item.Name,
                .Quantity = item.Quantity
            }
        item.Quantity = 0
        CleanUpInventory()
    End Sub

    Public Function HasEquipment(equipSlot As EquipSlot) As Boolean Implements ICharacterInstance.HasEquipment
        Return CharacterInstanceData.Equipment.ContainsKey(equipSlot)
    End Function

    Public Sub Unequip(equipSlot As EquipSlot) Implements ICharacterInstance.Unequip
        If HasEquipment(equipSlot) Then
            Dim item = CharacterInstanceData.Equipment(equipSlot)
            CharacterInstanceData.Equipment.Remove(equipSlot)
            CharacterInstanceData.Items.Add(item)
        End If
    End Sub

    Public ReadOnly Property Character As ICharacter Implements ICharacterInstance.Character
        Get
            Return New Character(_data, CharacterInstanceData.CharacterName)
        End Get
    End Property

    Public ReadOnly Property Column As Integer Implements ICharacterInstance.Column
        Get
            Return _column
        End Get
    End Property

    Public ReadOnly Property Row As Integer Implements ICharacterInstance.Row
        Get
            Return _row
        End Get
    End Property

    Public ReadOnly Property Map As IMap Implements ICharacterInstance.Map
        Get
            Return New Map(_data, _mapName)
        End Get
    End Property

    Public ReadOnly Property HasMessage As Boolean Implements ICharacterInstance.HasMessage
        Get
            Return CharacterInstanceData.Messages.Any
        End Get
    End Property

    Public ReadOnly Property Message As IMessage Implements ICharacterInstance.Message
        Get
            If Not CharacterInstanceData.Messages.Any Then
                Return Nothing
            End If
            Return New Message(_data, _mapName, _column, _row, 0)
        End Get
    End Property

    Public Property Target As ICharacterInstance Implements ICharacterInstance.Target
        Get
            Dim currentTarget = CharacterInstanceData.Target
            If currentTarget Is Nothing Then
                Return Nothing
            End If
            Return New CharacterInstance(_data, currentTarget.MapName, currentTarget.Column, currentTarget.Row)
        End Get
        Set(value As ICharacterInstance)
            If value Is Nothing Then
                CharacterInstanceData.Target = Nothing
                Return
            End If
            CharacterInstanceData.Target = New CharacterInstanceTargetData With {
                .MapName = value.Map.Name,
                .Column = value.Column,
                .Row = value.Row}
        End Set
    End Property

    Public ReadOnly Property Name As String Implements ICharacterInstance.Name
        Get
            Return Character.Name
        End Get
    End Property

    Public Property Health As Integer Implements ICharacterInstance.Health
        Get
            Dim maxHealth = MaximumHealth
            Return Math.Clamp(maxHealth - GetStatistic(StatisticType.Wounds), 0, maxHealth)
        End Get
        Set(value As Integer)
            SetStatistic(StatisticType.Wounds, MaximumHealth - Math.Clamp(value, 0, MaximumHealth))
        End Set
    End Property

    Public ReadOnly Property MaximumHealth As Integer Implements ICharacterInstance.MaximumHealth
        Get
            Return Character.GetStatistic(StatisticType.MaximumHealth)
        End Get
    End Property

    Public ReadOnly Property IsDead As Boolean Implements ICharacterInstance.IsDead
        Get
            Return Health <= 0
        End Get
    End Property

    Public ReadOnly Property MaximumDefend As Integer Implements ICharacterInstance.MaximumDefend
        Get
            Return Character.GetStatistic(StatisticType.MaximumDefend)
        End Get
    End Property

    Public ReadOnly Property MaximumAttack As Integer Implements ICharacterInstance.MaximumAttack
        Get
            Return Character.GetStatistic(StatisticType.MaximumAttack)
        End Get
    End Property

    Public ReadOnly Property Items As IEnumerable(Of IItemInstance) Implements ICharacterInstance.Items
        Get
            Dim result As New List(Of IItemInstance)
            For index = 0 To CharacterInstanceData.Items.Count - 1
                result.Add(New CharacterItemInstance(_data, _mapName, _column, _row, index))
            Next
            Return result
        End Get
    End Property

    Public Property Shoppe As IShoppe Implements ICharacterInstance.Shoppe
        Get
            If CharacterInstanceData.ShoppeName Is Nothing Then
                Return Nothing
            End If
            Return New Shoppe(_data, CharacterInstanceData.ShoppeName)
        End Get
        Set(value As IShoppe)
            If value Is Nothing Then
                CharacterInstanceData.ShoppeName = Nothing
                Return
            End If
            CharacterInstanceData.ShoppeName = value.Name
        End Set
    End Property
    Public ReadOnly Property DeathSfx As Sfx? Implements ICharacterInstance.DeathSfx
        Get
            Return Character.DeathSfx
        End Get
    End Property

    Public ReadOnly Property HitSfx As Sfx? Implements ICharacterInstance.HitSfx
        Get
            Return Character.HitSfx
        End Get
    End Property

    Public ReadOnly Property CanUseItem As Boolean Implements ICharacterInstance.CanUseItem
        Get
            Return Items.Any(Function(x) x.CanUse)
        End Get
    End Property

    Public ReadOnly Property UsableItems As IEnumerable(Of IItemInstance) Implements ICharacterInstance.UsableItems
        Get
            Return Items.Where(Function(x) x.CanUse)
        End Get
    End Property

    Public ReadOnly Property HasItems As Boolean Implements ICharacterInstance.HasItems
        Get
            Return CharacterInstanceData.Items.Any
        End Get
    End Property

    Public ReadOnly Property Equipment As IReadOnlyDictionary(Of EquipSlot, IItemInstance) Implements ICharacterInstance.Equipment
        Get
            Return CharacterInstanceData.Equipment.ToDictionary(Of EquipSlot, IItemInstance)(
                Function(x) x.Key,
                Function(x) New CharacterEquipmentItemInstance(_data, _mapName, _column, _row, x.Key))
        End Get
    End Property
End Class
