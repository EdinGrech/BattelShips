using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DataAccess.AppAccessContext;

namespace DataAccess.Structures.DBStructures.Relations
{
    public class Board2Attack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int BoardFK { get; set; }
        public int AttackFK { get; set; }
    }

    public static class Board2AttackAccess
    {
        private static readonly AppDBContext AppContext = new AppDBContext();

        public static void AddBoard2Attack(Board2Attack board2Attack)
        {
            AppContext.Boards2Attacks.Add(board2Attack);
            AppContext.SaveChanges();
        }
    }
}
