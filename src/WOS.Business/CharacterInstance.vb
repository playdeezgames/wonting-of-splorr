Imports System.ComponentModel

Friend Class CharacterInstance
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

    Public Sub AddMessage(lines As IEnumerable(Of (Hue, String))) Implements ICharacterInstance.AddMessage
        If Character.IsMessageSink Then
            CharacterInstanceData.Messages.Add(lines)
        End If
    End Sub

    Public Sub NextMessage() Implements ICharacterInstance.NextMessage
        CharacterInstanceData.Messages.RemoveAt(0)
    End Sub

    Public Function GetStatistic(statisticType As StatisticType) As Integer Implements ICharacterInstance.GetStatistic
        Return CharacterInstanceData.Statistics(statisticType)
    End Function

    Public Sub Attack(target As ICharacterInstance) Implements ICharacterInstance.Attack
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
            Else
                msg.Add((Hue.Gray, $"{target.Name} has {target.Health} health"))
            End If
        Else
            msg.Add((Hue.Gray, $"{Name} misses!"))
        End If
        AddMessage(msg)
        target.AddMessage(msg)
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
        Dim item As IItem = New Item(_data, MapCellData.Item.ItemName)
        Dim msg As New List(Of (Hue, String)) From
            {
                (Hue.Green, $"{Name} takes {MapCellData.Item.Quantity} {MapCellData.Item.ItemName}")
            }
        If item.Stacks Then
            Dim itemInstance = CharacterInstanceData.Items.FirstOrDefault(Function(x) x.ItemName = MapCellData.Item.ItemName)
            If itemInstance IsNot Nothing Then
                itemInstance.Quantity += MapCellData.Item.Quantity
            Else
                CharacterInstanceData.Items.Add(New ItemInstanceData With {.ItemName = MapCellData.Item.ItemName, .Quantity = MapCellData.Item.Quantity})
            End If
        Else
            CharacterInstanceData.Items.Add(New ItemInstanceData With {.ItemName = MapCellData.Item.ItemName, .Quantity = MapCellData.Item.Quantity})
        End If
        MapCellData.Item = Nothing
        AddMessage(msg)
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

    Public ReadOnly Property Message As IEnumerable(Of (Hue, String)) Implements ICharacterInstance.Message
        Get
            Return CharacterInstanceData.Messages.FirstOrDefault
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
End Class
