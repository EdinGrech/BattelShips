using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Structures.DBStructures
{
    public class GameShipConfiguration
    {
        //As you can see it's kinda pointless, I just used Ships to keep all the relevant data there and had a Relationship made to link it to player boards
        //I am curious as to why the assignment posed it in such a way as otherwise the program would not have been functional.
        //Would have been a more fun and better learning experance if we were just told to make a Battel ships game as I spent most of my time understanding what you expected us to do.
        //The assignment was terribly unclear and had a number of mistakes.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int PlayerFK { get; set; }
        public int GameFK { get; set; }
    }
}
