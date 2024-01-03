using DataAccess.Structures.DBStructures;
using DataAccess.Structures.DBStructures.Relations;
using static DataAccess.Structures.Positioners;
using static DataAccess.Structures.StructHelpers;
namespace LogicController
{
    public static class SaveTranslationController
    {
        public static int ShipOnBoardPlacementSaver(ShipInfo shipinfo, BoardData boardData, int shipTypeId)
        {
            int savedShipId;
            int savedBoardId;

            Ship createShipObject(ShipInfo shipinfo)
            {
                Ship ship = new Ship { 
                    col = shipinfo.OrientedCoordinate.Coordinate.Col,
                    row = shipinfo.OrientedCoordinate.Coordinate.Row,
                    orientation = shipinfo.OrientedCoordinate.Orientation.ToString(),
                    ShipTypeID = shipTypeId
                };
                return ship;
            }

            Board createBoardObject(BoardData boarddata)
            {
                Board board = new Board
                {
                    PlayerFK = boarddata.PlayerID,
                    GameFK = boarddata.GameID,
                };
                return board;
            }

            Board2Ship createBoard2ShipObject(int boardId, int savedShipId) {
                Board2Ship board2Ship = new Board2Ship
                {
                    BoardFK = boardId,
                    ShipFK = savedShipId
                };
                return board2Ship;
            }

            void saveBoard2Ship2DB() { 
                //call only once ship has been saved and ship id is in hand
                Board2ShipAccess.AddBoard2Ship(createBoard2ShipObject(savedBoardId, savedShipId));
            }

            void saveShip2DB(ShipInfo shipinfo)
            {
                savedShipId = (int)(boardData.id != null ? boardData.id : ShipAccess.AddShip(createShipObject(shipinfo)));
                savedBoardId = BoardAccess.AddBoard(createBoardObject(boardData));
                saveBoard2Ship2DB();
            }

            saveShip2DB(shipinfo);
            return savedBoardId;
        }

        public static void Attack2BoardSaver(DataAccess.Structures.StructHelpers.Attack attack, BoardData boardData)
        {
            DataAccess.Structures.DBStructures.Attack createAttackObject(DataAccess.Structures.StructHelpers.Attack attack)
            {
                Board boardLink = BoardAccess.GetBoardById((int)(boardData.id));

                DataAccess.Structures.DBStructures.Attack Attack = new DataAccess.Structures.DBStructures.Attack
                {
                    col = attack.coordinate.Col,
                    row = attack.coordinate.Row,
                    hit = attack.hit,
                    GameFK = boardLink.GameFK //useless
                };

                return Attack;
            }

            Board2Attack createBoard2AttackObject(int attackId)
            {
                Board2Attack board2Attack = new Board2Attack
                {
                    BoardFK = (int)(boardData.id!),
                    AttackFK = attackId,
                };
                return board2Attack;
            }

            void saveAttack2DB(){
                int attackId = AttackAccess.AddAttack(createAttackObject(attack));
                Board2AttackAccess.AddBoard2Attack(createBoard2AttackObject((int)attackId));
            }

            saveAttack2DB();
        }
    }

}
