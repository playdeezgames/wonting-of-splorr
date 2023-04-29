Imports System
Imports SPLORR.Game

Module Program
    Const MazeColumns = 15
    Const MazeRows = 15
    Const MazeCount = 500
    Private ReadOnly mazeDirections As IReadOnlyDictionary(Of Direction, MazeDirection(Of Direction)) =
        New Dictionary(Of Direction, MazeDirection(Of Direction)) From
        {
            {Direction.North, New MazeDirection(Of Direction)(Direction.South, 0, -1)},
            {Direction.East, New MazeDirection(Of Direction)(Direction.West, 1, 0)},
            {Direction.South, New MazeDirection(Of Direction)(Direction.North, 0, 1)},
            {Direction.West, New MazeDirection(Of Direction)(Direction.East, -1, 0)}
        }
    Sub Main(args As String())
        Dim mazes = MazeCount
        While mazes > 0
            If GenerateAndValidateMaze() Then
                mazes -= 1
            Else
                Console.ReadLine()
                mazes = MazeCount
            End If
        End While
    End Sub

    Private Function GenerateAndValidateMaze() As Boolean
        Dim maze = New Maze(Of Direction)(MazeColumns, MazeRows, mazeDirections)
        maze.Generate()
        For row = 0 To MazeRows - 1
            For column = 0 To MazeColumns - 1
                Console.Write("#")
                If row > 0 AndAlso maze.GetCell(column, row).GetDoor(Direction.North).Open Then
                    Console.Write(" ")
                Else
                    Console.Write("#")
                End If
            Next
            Console.WriteLine("#")
            For column = 0 To MazeColumns - 1
                If column > 0 AndAlso maze.GetCell(column, row).GetDoor(Direction.West).Open Then
                    Console.Write(" ")
                Else
                    Console.Write("#")
                End If
                Console.Write(" ")
            Next
            Console.WriteLine("#")
        Next
        For column = 0 To MazeColumns - 1
            Console.Write("#")
            Console.Write("#")
        Next
        Console.WriteLine("#")
        Dim inside As New HashSet(Of (Integer, Integer))
        WalkMaze(maze, (0, 0), inside)
        Return inside.Count = MazeColumns * MazeRows
    End Function

    Private Sub WalkMaze(maze As Maze(Of Direction), value As (Integer, Integer), inside As HashSet(Of (Integer, Integer)))
        If inside.Contains(value) Then
            Return
        End If
        inside.Add(value)
        For Each direction In mazeDirections.Keys
            Dim cell = maze.GetCell(value.Item1, value.Item2)
            Dim door = cell.GetDoor(direction)
            If door IsNot Nothing Then
                WalkMaze(maze, (value.Item1 + mazeDirections(direction).DeltaX, value.Item2 + mazeDirections(direction).DeltaY), inside)
            End If
        Next
    End Sub
End Module
