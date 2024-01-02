using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DataAccess.AppAccessContext;

namespace DataAccess.Structures.DBStructures
{
    public class ShipTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public int size { get; set; }
    }

    public class ShipTypesAccessor
    {
        static AppDBContext appContext = new AppDBContext();

        static public List<ShipTypes> GetAllShipTypes()
        {
            List<ShipTypes> allShipTypes = appContext.ShipTypes.ToList();
            return allShipTypes;
        }
    }
}
