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

    Public Sub AddMessage(lines As IEnumerable(Of (Hue, String))) Implements ICharacterInstance.AddMessage
        If Character.IsMessageSink Then
            CharacterInstanceData.Messages.Add(lines)
        End If
    End Sub

    Public Sub NextMessage() Implements ICharacterInstance.NextMessage
        CharacterInstanceData.Messages.RemoveAt(0)
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
                .MapName = _mapName,
                .Column = _column,
                .Row = _row}
        End Set
    End Property
End Class
