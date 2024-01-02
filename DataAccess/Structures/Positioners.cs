using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Structures
{
    public class Positioners
    {
        public struct ShipInfo
        {
            public OrientedCoordinate OrientedCoordinate { get; set; }
            public int Size { get; set; }
        }

        public struct Coordinate
        {
            public string Row { get; set; }
            public string Col { get; set; }
        }

        public enum Orientation
        {
            Horizontal,
            Vertical
        }

        public struct OrientedCoordinate
        {
            public Coordinate Coordinate { get; set; }
            public Orientation Orientation { get; set; }
        }
    }
}
