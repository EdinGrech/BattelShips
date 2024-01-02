using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DataAccess.AppAccessContext;

namespace DataAccess.Structures.DBStructures.Relations
{
    public class Board2Ship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int BoardFK { get; set; }
        public int ShipFK { get; set; }
    }

    public class Board2ShipAccess
    {
        private static readonly AppDBContext AppContext = new AppDBContext();

        public static int AddBoard2Ship(Board2Ship board2Ship)
        {
            AppContext.Boards2Ships.Add(board2Ship);
            AppContext.SaveChanges();
            return board2Ship.id;
        }

        public static IEnumerable<int> GetShipIdsByBoardFK(int boardFK)
        {
            return AppContext.Boards2Ships
                .Where(b2s => b2s.BoardFK == boardFK)
                .Select(b2s => b2s.ShipFK)
                .ToList();
        }
    }
}
