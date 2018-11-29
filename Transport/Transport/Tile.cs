using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport
{
    class Tile
    {
        private int x;
        private int y;

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int Y { get => y; set => y = value; }
        public int X { get => x; set => x = value; }
    }

    class RoadTile : Tile
    {
        public RoadTile(int x, int y) : base(x, y)
        {
        }
    }

    class ResidentialTile : Tile
    {
        public ResidentialTile(int x, int y) : base(x, y)
        {
        }
    }

    class WorkTile : Tile
    {
        public WorkTile(int x, int y) : base(x, y)
        {
        }
    }

    class PlayTile : Tile
    {
        public PlayTile(int x, int y) : base(x, y)
        {
        }
    }

    class ShopTile : Tile
    {
        public ShopTile(int x, int y) : base(x, y)
        {
        }
    }
}
