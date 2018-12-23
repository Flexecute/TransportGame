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
        //List<Tile> tiles = new List<Tile>();
        Tile[,] tiles;
        int xMax;
        int yMax;

        public City(int xMax, int yMax)
        {
            tiles = new Tile[xMax, yMax];
            this.xMax = xMax;
            this.yMax = yMax;
            // Create blank tiles at each location
            for (int x = 0; x < xMax; x++)
            {
                for (int y = 0; y < yMax; y++)
                {
                    Tile tile = new Tile(x, y);
                    tiles[x, y] = tile;
                    //tiles.Add(tile);
                }

            }
            
        }

        public Tile[,] getTiles()
        {
            return tiles;
        }

        public void changeTile(int x, int y, Tile newTileType) {
            Tile tile = new RoadTile(x, y);
            // Destroy the old road tile, Replace with new one
            tiles[x, y] = tile;
        }

        public Tile getTileAtPos(int x, int y) {
            if (x < 0 || y < 0 || x > xMax || y > yMax) {
                // Attempt to find a tile outside of the bounds
                return null;
            } else {
                return tiles[x, y];
            }
        }

    }
}
