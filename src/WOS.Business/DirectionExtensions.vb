Imports System.Globalization
Imports System.Runtime.CompilerServices

Friend Module DirectionExtensions
    <Extension>
    Function GetNextX(direction As Direction, x As Integer) As Integer
        Select Case direction
            Case Direction.North, Direction.South
                Return x
            Case Direction.East
                Return x + 1
            Case Direction.West
                Return x - 1
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Function GetNextY(direction As Direction, y As Integer) As Integer
        Select Case direction
            Case Direction.East, direction, Direction.West
                Return y
            Case Direction.North
                Return y - 1
            Case Direction.South
                Return y + 1
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
