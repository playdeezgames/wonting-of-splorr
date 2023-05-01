Friend Class Shoppe
    Implements IShoppe
    Private ReadOnly _data As WorldData
    Private ReadOnly _shoppeName As String

    Public Sub New(data As WorldData, shoppeName As String)
        Me._data = data
        Me._shoppeName = shoppeName
    End Sub

    Public ReadOnly Property Name As String Implements IShoppe.Name
        Get
            Return _shoppeName
        End Get
    End Property
End Class
