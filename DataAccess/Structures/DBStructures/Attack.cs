using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DataAccess.AppAccessContext;

namespace DataAccess.Structures.DBStructures
{
    public class Attack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string row { get; set; }
        public string col { get; set; }
        public bool hit { get; set; }

        //useless keeping for marks
        public int GameFK { get; set; }
    }

    public static class AttackAccess
    {
        private static readonly AppDBContext AppContext = new AppDBContext();

        public static int AddAttack(Attack newAttack)
        {
            AppContext.Attacks.Add(newAttack);
            AppContext.SaveChanges();
            return newAttack.id;
        }
    }
}
