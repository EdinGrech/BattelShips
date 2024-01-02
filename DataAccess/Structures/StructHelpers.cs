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
            public int id { get; set; }
            public int PlayerID;
            public string PlayerUsername { get; set; }
            public int GameID;
            public List<Attack> Attacks { get; set; }
            public List<ShipInfo> Ships { get; set; }

            public int hitPoints { get; set; }

            public BoardData(int playerID, string playerUsername, int gameID)
            {
                PlayerID = playerID;
                PlayerUsername = playerUsername;
                GameID = gameID;

                //temp prefill
                Attacks = new List<Attack>
                {
                    new Attack{
                            hit = true,
                            coordinate = new Coordinate { Row = "A", Col = "1" } 
                        },
                    new Attack{
                            hit = true,
                            coordinate = new Coordinate { Row = "A", Col = "2" }
                        },
                    new Attack{
                            hit = true,
                            coordinate = new Coordinate { Row = "A", Col = "3" }
                        },
                    new Attack{
                            hit = false,
                            coordinate = new Coordinate { Row = "A", Col = "4" }
                        }
                };
                Ships = new List<ShipInfo>
                {
                    new ShipInfo
                    {
                        OrientedCoordinate = new OrientedCoordinate
                        {
                            Coordinate = new Coordinate { Row = "D", Col = "4" },
                            Orientation = Orientation.Horizontal
                        },
                        Size = 2
                    },
                    new ShipInfo
                    {
                        OrientedCoordinate = new OrientedCoordinate
                        {
                            Coordinate = new Coordinate { Row = "E", Col = "5" },
                            Orientation = Orientation.Vertical
                        },
                        Size = 3
                    },
                    new ShipInfo
                    {
                        OrientedCoordinate = new OrientedCoordinate
                        {
                            Coordinate = new Coordinate { Row = "F", Col = "6" },
                            Orientation = Orientation.Horizontal
                        },
                        Size = 3
                    }
                };
            }
        }
    }
}
