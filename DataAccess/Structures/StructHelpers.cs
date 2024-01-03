using static DataAccess.Structures.Positioners;

namespace DataAccess.Structures
{
    public class StructHelpers
    {

        public struct Attack
        {
            public Coordinate coordinate { get; set; }
            public bool hit { get; set; }
        }

        public class BoardData
        {
            public int? id { get; set; } = null;
            public int PlayerID;
            public string PlayerUsername { get; set; }
            public int GameID;
            public List<Attack> Attacks { get; set; }
            public List<ShipInfo> Ships { get; set; }

            public int hitPoints { get; set; } = 0;

            public BoardData(int playerID, string playerUsername, int gameID)
            {
                PlayerID = playerID;
                PlayerUsername = playerUsername;
                GameID = gameID;

                Attacks = new List<Attack>();
                Ships = new List<ShipInfo>();
            }
        }
    }
}
