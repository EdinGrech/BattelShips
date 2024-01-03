using DataAccess.Structures.DBStructures;
using static DataAccess.Structures.Positioners;
using static DataAccess.Structures.StructHelpers;
using static FrontEnd.BoardGridEngin;


namespace FrontEnd
{
    static public class FrontEndController
    {
        //public FrontEndController()
        //{
        //    Grid grid = new();
        //    BoardData board = new(1, 1, "john", 1); //board id, player id, game id

        //    PlaceShips(board, grid);
        //    Console.ReadKey();
        //}

        public struct ExtendedShipInfo
        {
            public ShipInfo ShipInfo { get; set; }
            public int Id { get; set; }
        }

        public static (List<(ShipInfo, int)>, BoardData) PlaceShips(BoardData boardData, Grid grid)
        {
            List<ShipTypes> shipTypes = ShipTypesAccessor.GetAllShipTypes();
            List<ExtendedShipInfo> ShipList = new List<ExtendedShipInfo>();

            List<(ShipInfo, int)> shipInfoReturnList = new List<(ShipInfo, int)>();

            for (int i = 0; i < shipTypes.Count; i++)
            {
                ShipTypes shipType = shipTypes[i];
                ShipList.Add(new ExtendedShipInfo { ShipInfo = new ShipInfo { Size = shipType.size }, Id = i + 1 });
            }

            for (int i = 0; i < ShipList.Count; i++)
            {
                bool shipPlacementValid = false;
                GridRenderShips(boardData, grid);
                do
                {
                    string? userInputRow = "";
                    string? userInputCol = "";
                    string? userInputOrientation = "";

                    Console.WriteLine($"Placement of ship size: {ShipList[i].ShipInfo.Size}, Id: {ShipList[i].Id}");
                    bool isValidInput = false;
                    do
                    {
                        Console.WriteLine("Enter a Row (1-8): ");
                        userInputCol = Console.ReadLine();
                        if (grid.colLabels.Contains(userInputCol))
                        {
                            isValidInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Not a known column");
                        }
                    }
                    while (!isValidInput);
                    isValidInput = false;
                    do
                    {
                        Console.WriteLine("Enter a column (A-G): ");
                        userInputRow = Console.ReadLine();
                        if (grid.rowLabels.Contains(userInputRow))
                        {
                            isValidInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Not a known row");
                        }
                    }
                    while (!isValidInput);
                    isValidInput = false;
                    do
                    {
                        Console.WriteLine("Enter an orientation (Horizontal / Vertical): ");
                        userInputOrientation = Console.ReadLine();
                        if (Enum.IsDefined(typeof(Orientation), userInputOrientation))
                        {
                            isValidInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Not a known orientation");
                        }
                    }
                    while (!isValidInput);

                    ShipInfo newShip = new ShipInfo
                    {
                        OrientedCoordinate = new OrientedCoordinate
                        {
                            Coordinate = new Coordinate { Row = userInputRow, Col = userInputCol },
                            Orientation = (Orientation)Enum.Parse(typeof(Orientation), userInputOrientation)
                        },
                        Size = ShipList[i].ShipInfo.Size
                    };

                    if (!ShipPlacementValidator.ValidateShipPlacement(boardData, newShip, grid))
                    {
                        shipPlacementValid = true;
                        boardData.Ships.Add(newShip);
                        shipInfoReturnList.Add((newShip, ShipList[i].Id));
                    }
                } while (!shipPlacementValid);
            }
            return (shipInfoReturnList, boardData);
        }

        public static (BoardData, DataAccess.Structures.StructHelpers.Attack) AttackShip(BoardData boardData, Player attacker, Grid grid)
        {
            DataAccess.Structures.StructHelpers.Attack attack;
            Console.WriteLine($"Player {attacker.username} Enter your Attack Cordiantes");
            bool isValidAttack = false;
            
            GridRenderAttacks(boardData, grid);
            do
            {
                string? userInputRow = "";
                string? userInputCol = "";

                bool isValidInput = false;
                do
                {
                    Console.WriteLine("Enter a Row (1-8): ");
                    userInputCol = Console.ReadLine();
                    if (grid.colLabels.Contains(userInputCol))
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Not a known column");
                    }
                }
                while (!isValidInput);
                isValidInput = false;
                do
                {
                    Console.WriteLine("Enter a column (A-G): ");
                    userInputRow = Console.ReadLine();
                    if (grid.rowLabels.Contains(userInputRow))
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Not a known row");
                    }
                }while (!isValidInput);

                attack = new DataAccess.Structures.StructHelpers.Attack { coordinate = new Coordinate { Row = userInputRow, Col = userInputCol } };
                if (AttackValidator.DuplicatAttack(boardData, attack))
                {
                    Console.WriteLine("Attack Already Exists");
                    isValidAttack = false;
                }
                else
                {
                    attack.hit = AttackValidator.ShipHitDetaction(boardData,grid, attack);
                    isValidAttack = true;
                }
            } while (!isValidAttack);

            boardData.Attacks.Add(attack);
            boardData.hitPoints = boardData.hitPoints += -1;
            return (boardData, attack);
        }
    }
}
