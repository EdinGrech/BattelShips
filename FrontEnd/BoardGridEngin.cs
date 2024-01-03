using static DataAccess.Structures.Positioners;
using static DataAccess.Structures.StructHelpers;

namespace FrontEnd
{
    public class BoardGridEngin
    {
        public struct Grid
    {
        public string[] rowLabels { get; }
        public string[] colLabels { get; }
        public string[,] gridData { get; set; }
        public Grid()
        {
            rowLabels = new string[] { "A", "B", "C", "D", "E", "F", "G" };
            colLabels = new string[] { "1", "2", "3", "4", "5", "6", "7", "8" };
            gridData = GridConfig();
        }

        public int GetRowIndex(string label)
        {
            return Array.IndexOf(rowLabels, label);
        }

        public int GetColIndex(string label)
        {
            return Array.IndexOf(colLabels, label);
        }

        string[,] GridConfig()
        {
            // Define grid size
            int rows = rowLabels.Length;
            int cols = colLabels.Length;

            // Initial grid
            string[,] grid = new string[rows, cols];


            // Populate initial grid
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    grid[i, j] = " ";
                }
            }

            return grid;
        }

    }

        public class ShipPlacementValidator
        {
            public static bool ValidateShipPlacement(BoardData board, ShipInfo newShip, Grid grid)
            {
                // Create a matrix to represent ship locations on the grid
                var shipMatrix = CreateShipMatrix(board, grid);

                // Check if the new ship collides with existing ships
                return CheckCollision(shipMatrix, newShip, grid);
            }

            private static bool CheckCollision(bool[,] shipMatrix, ShipInfo newShip, Grid grid)
            {
                int rows = shipMatrix.GetLength(0);
                int cols = shipMatrix.GetLength(1);

                foreach (var shipCoord in GetShipCoordinates(newShip, grid))
                {
                    int row = shipCoord.Item1;
                    int col = shipCoord.Item2;

                    if (row < 0 || row >= rows || col < 0 || col >= cols || shipMatrix[row, col])
                    {
                        // Collision detected
                        return true;
                    }
                }

                // No collision
                return false;
            }

            private static bool[,] CreateShipMatrix(BoardData board, Grid grid)
            {
                int rows = grid.rowLabels.Length;
                int cols = grid.colLabels.Length;

                bool[,] shipMatrix = new bool[rows, cols];

                if(board.Ships.Count > 0)
                {
                    foreach (var ship in board.Ships)
                    {
                        foreach (var shipCoord in GetShipCoordinates(ship, grid))
                        {
                            int row = shipCoord.Item1;
                            int col = shipCoord.Item2;

                            // Mark ship locations in the matrix
                            shipMatrix[row, col] = true;
                        }
                    }
                }


                return shipMatrix;
            }

            private static IEnumerable<(int, int)> GetShipCoordinates(ShipInfo ship, Grid grid)
            {
                List<(int, int)> coordinates = new List<(int, int)>();

                int row = grid.GetRowIndex(ship.OrientedCoordinate.Coordinate.Row);
                int col = grid.GetColIndex(ship.OrientedCoordinate.Coordinate.Col);

                for (int i = 0; i < ship.Size; i++)
                {
                    if (ship.OrientedCoordinate.Orientation == Orientation.Horizontal)
                    {
                        coordinates.Add((row, col + i));
                    }
                    else
                    {
                        coordinates.Add((row + i, col));
                    }
                }

                return coordinates;
            }
        }

        public static class AttackValidator
        {
            public static bool DuplicatAttack(BoardData boardData, Attack attack)
            {
                foreach (Attack attack_ in boardData.Attacks) { 
                    if (attack.coordinate.Col == attack_.coordinate.Col && attack.coordinate.Row == attack_.coordinate.Row)
                    {
                        return true;
                    }
                }
                return false;
            }

            public static bool ShipHitDetaction(BoardData boardData, Grid grid, Attack attack)
            {
                //bool[,] shipMeshGrrid = new bool[grid.rowLabels.Length,grid.colLabels.Length];
                for (int i = 0; i < boardData.Ships.Count; i++)
                {
                    int attackColIndex = grid.GetColIndex(attack.coordinate.Col);
                    int attackRowIndex = grid.GetRowIndex(attack.coordinate.Row);

                    bool attackColHit = false;
                    bool attackRowHit = false;
                    
                    int colIndex = grid.GetColIndex(boardData.Ships[i].OrientedCoordinate.Coordinate.Col);
                    int rowIndex = grid.GetRowIndex(boardData.Ships[i].OrientedCoordinate.Coordinate.Row);
                    
                    if (boardData.Ships[i].OrientedCoordinate.Orientation == Orientation.Horizontal)
                    {
                        for (int k = 0; k < boardData.Ships[i].Size; k++)
                        {
                            int tempColIndex = colIndex + k;
                            if (tempColIndex == attackColIndex)
                            {
                                attackColHit = true;
                            }
                            //shipMeshGrrid[rowIndex, tempColIndex] = true;
                        }
                    }
                    if (boardData.Ships[i].OrientedCoordinate.Orientation == Orientation.Vertical)
                    {
                        for (int k = 0; k < boardData.Ships[i].Size; k++)
                        {
                            int tempRowIndex = rowIndex + k;
                            if (tempRowIndex == attackRowIndex)
                            {
                                attackRowHit = true;
                            }
                            //shipMeshGrrid[tempRowIndex, colIndex] = true;
                        }
                    }
                    if( attackColHit &&  attackRowHit)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static void DisplayGrid(string[,] grid, string[] rowLabels, string[] colLabels)
        {
            // Display column labels
            Console.Write("   ");
            for (int j = 0; j < colLabels.Length; j++)
            {
                Console.Write(colLabels[j] + " ");
            }
            Console.WriteLine();

            // Display grid with row labels
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                Console.Write(rowLabels[i] + " |");
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j] + "|");
                }
                Console.WriteLine();
            }
        }

        public static void GridRenderShips(BoardData boardData, Grid grid)
        {
            if (boardData.Ships.Count > 0)
            {
                for (int i = 0; i < boardData.Ships.Count; i++)
            {
                int colIndex = grid.GetColIndex(boardData.Ships[i].OrientedCoordinate.Coordinate.Col);
                int rowIndex = grid.GetRowIndex(boardData.Ships[i].OrientedCoordinate.Coordinate.Row);
                if (boardData.Ships[i].OrientedCoordinate.Orientation == Orientation.Horizontal)
                {
                    for (int k = 0; k < boardData.Ships[i].Size; k++)
                    {
                        int tempColIndex = colIndex + k;
                        if (k == 0)
                        {
                            grid.gridData[rowIndex, tempColIndex] = "<";
                        }
                        else if (k == (boardData.Ships[i].Size - 1))
                        {
                            grid.gridData[rowIndex, tempColIndex] = ">";
                        }
                        else
                        {
                            grid.gridData[rowIndex, tempColIndex] = "=";
                        }
                    }
                }
                if (boardData.Ships[i].OrientedCoordinate.Orientation == Orientation.Vertical)
                {
                    for (int k = 0; k < boardData.Ships[i].Size; k++)
                    {
                        int tempRowIndex = rowIndex + k;
                        if (k == 0)
                        {
                            grid.gridData[tempRowIndex, colIndex] = "^";
                        }
                        else if (k == (boardData.Ships[i].Size - 1))
                        {
                            grid.gridData[tempRowIndex, colIndex] = "V";
                        }
                        else
                        {
                            grid.gridData[tempRowIndex, colIndex] = "|";
                        }
                    }
                }
            }
            }
            Console.Clear();
            Console.WriteLine($"Player {boardData.PlayerUsername}'s Ship Config Board");
            DisplayGrid(grid.gridData, grid.rowLabels, grid.colLabels);
        }

        public static void GridRenderAttacks(BoardData boardData, Grid grid)
        {
            for (int i = 0; i < boardData.Attacks.Count; i++)
            {
                string drawChar = boardData.Attacks[i].hit ? "X" : "O"; 
                grid.gridData[grid.GetRowIndex(boardData.Attacks[i].coordinate.Row), grid.GetColIndex(boardData.Attacks[i].coordinate.Col)] = drawChar;
            }
            Console.Clear();
            Console.WriteLine($"Player {boardData.PlayerUsername}'s Oppeninet Attack Board");
            DisplayGrid(grid.gridData, grid.rowLabels, grid.colLabels);
        }
    }
}
