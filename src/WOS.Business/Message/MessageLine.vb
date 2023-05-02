Friend Class MessageLine
    Implements IMessageLine

    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer
    Private ReadOnly _index As Integer
    Private ReadOnly _lineIndex As Integer
    Private ReadOnly Property MapData As MapData
        Get
            Return _data.Maps(_mapName)
        End Get
    End Property
    Private ReadOnly Property MapCellData As MapCellData
        Get
            Return MapData.Cells(_column + _row * MapData.Columns)
        End Get
    End Property
    Private ReadOnly Property MessageData As MessageData
        Get
            Return MapCellData.Character.Messages(_index)
        End Get
    End Property
    Private ReadOnly Property MessageLineData As MessageLineData
        Get
            Return MessageData.MessageLines(_lineIndex)
        End Get
    End Property

    Public Sub New(data As WorldData, mapName As String, column As Integer, row As Integer, index As Integer, lineIndex As Integer)
        _data = data
        _mapName = mapName
        _column = column
        _row = row
        _index = index
        _lineIndex = lineIndex
    End Sub

    Public ReadOnly Property Hue As Hue Implements IMessageLine.Hue
        Get
            Return MessageLineData.Hue
        End Get
    End Property

    Public ReadOnly Property Text As String Implements IMessageLine.Text
        Get
            Return MessageLineData.Text
        End Get
    End Property
End Class
