Friend Class Message
    Implements IMessage

    Private ReadOnly _data As WorldData
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer
    Private ReadOnly _index As Integer
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


    Public Sub New(data As WorldData, mapName As String, column As Integer, row As Integer, index As Integer)
        _data = data
        _mapName = mapName
        _column = column
        _row = row
        _index = index
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of IMessageLine) Implements IMessage.Lines
        Get
            Dim result As New List(Of IMessageLine)
            For lineIndex = 0 To MessageData.MessageLines.Count - 1
                result.Add(New MessageLine(_data, _mapName, _column, _row, _index, lineIndex))
            Next
            Return result
        End Get
    End Property
End Class
