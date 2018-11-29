using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport
{
    class City
    {
        List<Tile> tiles = new List<Tile>();

        public City(int xMax, int yMax)
        {
            // Create blank tiles at each location
            for (int i = 0; i < xMax; i++)
            {
                for (int j = 0; j < yMax; j++)
                {
                    Tile tile = new Tile(i, j);
                    tiles.Add(tile);
                }

            }
        }

        public List<Tile> getTiles()
        {
            return tiles;
        }

    }
}
