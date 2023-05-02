Public Interface IMap
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    ReadOnly Property Name As String
    Function GetCell(column As Integer, row As Integer) As IMapCell
End Interface
