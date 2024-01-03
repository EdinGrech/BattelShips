using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DataAccess.AppAccessContext;

namespace DataAccess.Structures.DBStructures
{
    public class Board
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int GameFK { get; set; }
        public int PlayerFK { get; set; }
    }

    public static class BoardAccess
    {
        private static readonly AppDBContext AppContext = new AppDBContext();

        public static Board GetBoardById(int boardId)
        {
            return AppContext.Boards.FirstOrDefault(board => board.id == boardId);
        }

        public static int AddBoard(Board newBoard)
        {
            AppContext.Boards.Add(newBoard);
            AppContext.SaveChanges();
            return newBoard.id;
        }
    }
}
