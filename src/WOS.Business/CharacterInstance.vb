Friend Class CharacterInstance
    Implements ICharacterInstance

    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
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
            'no moving
            Return
        End If
        'TODO: check terrain
        Dim instanceData = CharacterInstanceData
        MapCellData.Character = Nothing
        _column = nextColumn
        _row = nextRow
        MapCellData.Character = instanceData
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
End Class
