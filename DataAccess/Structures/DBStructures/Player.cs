using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DataAccess.AppAccessContext;

namespace DataAccess.Structures.DBStructures
{
    public class BasePlayer
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }

    public class Player : BasePlayer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
    }

    static public class PlayerAccess
    {
        static AppDBContext appContext = new AppDBContext();

        static public Player? getPlayerByUsername(string username)
        {
            return appContext.Players.FirstOrDefault(player => player.username == username);
        }

        static public bool DoesPlayerExist(string username)
        {
            return appContext.Players.Any(player => player.username == username);
        }

        static public bool IsPasswordMatch(string username, string password)
        {
            var player = appContext.Players.FirstOrDefault(p => p.username == username);
            return player != null && player.password == password;
        }
        
        static public void AddNewPlayer(Player newPlayer)
        {
            if (!DoesPlayerExist(newPlayer.username))
            {
                appContext.Players.Add(newPlayer);
                appContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Player with the given username already exists.");
            }
        }
    }
}
