Imports System.Reflection

Friend Class CharacterEquipmentItemInstance
    Inherits BaseItemInstance
    Private ReadOnly _mapName As String
    Private ReadOnly _column As Integer
    Private ReadOnly _row As Integer
    Private ReadOnly _equipSlot As EquipSlot

    Public Sub New(data As WorldData, mapName As String, column As Integer, row As Integer, equipSlot As EquipSlot)
        MyBase.New(data)
        _mapName = mapName
        _column = column
        _row = row
        _equipSlot = equipSlot
    End Sub
    Private ReadOnly Property MapData As MapData
        Get
            Return _data.Maps(_mapName)
        End Get
    End Property

    Protected Overrides ReadOnly Property ItemInstanceData As ItemInstanceData
        Get
            Return MapData.Cells(_column + _row * MapData.Columns).Character.Equipment(_equipSlot)
        End Get
    End Property
End Class
