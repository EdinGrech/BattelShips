using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DataAccess.AppAccessContext;

namespace DataAccess.Structures.DBStructures
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string title { get; set; } //This is never set why did you ask for it ... *2 times
        public bool complete { get; set; }

        //useless keeping for marks
        public int creatorFK { get; set; }
        public int opponentFK { get; set; }
    }

    public class GameAccess
    {
        private static readonly AppDBContext AppContext = new AppDBContext();

        static public int AddGame(Game newGame)
        {
            AppContext.Games.Add(newGame);
            AppContext.SaveChanges();
            return newGame.id;
        }

        static public List<Game> GetAllGames()
        {
            return AppContext.Games.ToList();
        }

        static public Game GetGameById(int gameId)
        {
            return AppContext.Games.FirstOrDefault(g => g.id == gameId)!;
        }

        static public void UpdateGame(int gameId, bool newCompleteStatus)
        {
            var existingGame = AppContext.Games.Find(gameId);

            if (existingGame != null)
            {
                existingGame.complete = newCompleteStatus;

                AppContext.SaveChanges();
            }
        }

    }
}
