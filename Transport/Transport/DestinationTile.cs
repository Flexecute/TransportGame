using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport
{
    /// <summary>
    /// A class of tiles which represent destinations which can be reached via transport tiles
    /// </summary>
    class DestinationTile : Tile
    {
        private List<Passage> connectedPassages;
        
        /// <summary>
        ///  A dictionary of destination tiles with the corresponding 
        /// </summary>
        Dictionary<DestinationTile, List<TransportTile>> connections;

        public DestinationTile(int x, int y) : base(x, y) {
            connectedPassages = new List<Passage>();
        }

    }

    class ResidentialTile : DestinationTile
    {
        public ResidentialTile(int x, int y) : base(x, y)
        {
        }
    }

    class WorkTile : DestinationTile {
        public WorkTile(int x, int y) : base(x, y)
        {
        }
    }

    class PlayTile : DestinationTile 
    {
        public PlayTile(int x, int y) : base(x, y)
        {
        }
    }

    class ShopTile : DestinationTile 
    {
        public ShopTile(int x, int y) : base(x, y)
        {
        }
    }
}
