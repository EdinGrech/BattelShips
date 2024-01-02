using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DataAccess.AppAccessContext;

namespace DataAccess.Structures.DBStructures
{
    public class BaseShip
    {
        public int ShipTypeID { get; set; }
        public string row { get; set; }
        public string col { get; set; }
        public string orientation { get; set; }
    }

    public class Ship: BaseShip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
    }

    static public class ShipAccess
    {
        private static readonly AppDBContext AppContext = new AppDBContext();

        public static int AddShip(Ship ship)
        {
            AppContext.Ships.Add(ship);
            AppContext.SaveChanges();
            return ship.id;
        }

        public static Ship GetShipById(int id)
        {
            return AppContext.Ships.FirstOrDefault(ship => ship.id == id);
        }
    }
}
