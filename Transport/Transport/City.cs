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
        SpriteBatch spriteBatch;
        Dictionary<Type, Texture2D> tileTextures = new Dictionary<Type, Texture2D>();

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

        public void enableGraphics(SpriteBatch sb, Texture2D baseTile, Texture2D roadTile, Texture2D residentialTile, Texture2D workTile, Texture2D playTile, Texture2D shopTile)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = sb;
            this.tileTextures[typeof(Tile)] = baseTile;
            this.tileTextures[typeof(RoadTile)] = roadTile;
            this.tileTextures[typeof(ResidentialTile)] = residentialTile;
            this.tileTextures[typeof(WorkTile)] = workTile;
            this.tileTextures[typeof(PlayTile)] = playTile;
            this.tileTextures[typeof(ShopTile)] = shopTile;
        }

        public void DrawTiles(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Tile tile in tiles)
            {
                spriteBatch.Draw(tileTextures[tile.GetType()], new Rectangle(tile.X * 100, tile.Y * 100, 100, 100), Color.White);
            }

            spriteBatch.End();
        }
    }
}
