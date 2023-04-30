Friend Class MapCell
    Implements IMapCell

    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer
    Private ReadOnly Property MapCellData As MapCellData
        Get
            Dim mapData = _data.Maps(_mapName)
            Return mapData.Cells(_column + _row * mapData.Columns)
        End Get
    End Property

    Public Sub New(data As WorldData, mapName As String, column As Integer, row As Integer)
        Me._data = data
        Me._mapName = mapName
        Me._column = column
        Me._row = row
    End Sub

    Public Function CreateCharacterInstance(characterName As String) As ICharacterInstance Implements IMapCell.CreateCharacterInstance
        MapCellData.Character = New CharacterInstanceData With {
            .CharacterName = characterName,
            .Statistics = New Dictionary(Of StatisticType, Integer) From
            {
                {StatisticType.Wounds, 0}
            }}
        Return Character
    End Function

    Public Sub DoTrigger(triggerKind As TriggerKind, character As ICharacterInstance) Implements IMapCell.DoTrigger
        If Not MapCellData.Triggers.ContainsKey(triggerKind) Then
            Return
        End If
        Dim trigger = MapCellData.Triggers(triggerKind)
        If trigger Is Nothing Then
            Return
        End If
        Select Case trigger.TriggerType
            Case TriggerType.Teleport
                character.Teleport(trigger.Teleport.MapName, trigger.Teleport.Column, trigger.Teleport.Row)
            Case Else
                character.AddMessage(trigger.Message.MessageLines.Select(Function(line) (line.Hue, line.Text)))
        End Select
    End Sub

    Public Sub SetTrigger(triggerKind As TriggerKind, triggerType As TriggerType) Implements IMapCell.SetTrigger
        MapCellData.Triggers(triggerKind) = New TriggerData With {.TriggerType = triggerType}
    End Sub

    Public Function GetTrigger(triggerKind As TriggerKind) As ITrigger Implements IMapCell.GetTrigger
        Return New Trigger(_data, _mapName, _column, _row, triggerKind)
    End Function

    Public Property Terrain As ITerrain Implements IMapCell.Terrain
        Get
            Return New Terrain(_data, MapCellData.TerrainName)
        End Get
        Set(value As ITerrain)
            MapCellData.TerrainName = value.Name
        End Set
    End Property

    Public Property Character As ICharacterInstance Implements IMapCell.Character
        Get
            If MapCellData.Character Is Nothing Then
                Return Nothing
            End If
            Return New CharacterInstance(_data, _mapName, _column, _row)
        End Get
        Set(value As ICharacterInstance)
            If value Is Nothing Then
                MapCellData.Character = Nothing
                Return
            End If
            CreateCharacterInstance(value.Character.Name)
        End Set
    End Property

    Public Property Item As IItemInstance Implements IMapCell.Item
        Get
            If MapCellData.Item Is Nothing Then
                Return Nothing
            End If
            Return New ItemInstance(_data, _mapName, _column, _row)
        End Get
        Set(value As IItemInstance)
            If value Is Nothing Then
                MapCellData.Item = Nothing
                Return
            End If
            MapCellData.Item = New ItemInstanceData With
                {
                    .ItemName = value.Item.Name,
                    .Quantity = value.Quantity
                }
        End Set
    End Property
End Class
