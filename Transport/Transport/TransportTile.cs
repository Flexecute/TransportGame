using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport
{
    class Passage {
        private List<TransportTile> tiles;
        private List<Passage> connectingPassages;
        private List<DestinationTile> connectingDestinations;

        internal List<DestinationTile> ConnectingDestinations { get => connectingDestinations; set => connectingDestinations = value; }
        internal List<Passage> ConnectingPassages { get => connectingPassages; set => connectingPassages = value; }

        public Passage(List<TransportTile> newTiles) {
            this.tiles = new List<TransportTile>(newTiles);
            this.ConnectingPassages = new List<Passage>();
            this.ConnectingDestinations = new List<DestinationTile>();
        }

        /// <summary>
        /// Adds a new tile to the passage
        /// </summary>
        /// <param name="newTile"></param>
        public void addTile(TransportTile newTile) {
            tiles.Add(newTile);
        }

        /// <summary>
        /// Removes a tile from the passage
        /// </summary>
        /// <param name="newTile"></param>
        public void removeTile(TransportTile newTile) {
            tiles.Remove(newTile);
        }

        /// <summary>
        /// Adds a new connecting passage
        /// </summary>
        /// <param name="newPassage"></param>
        public void addPassage(Passage newPassage) {
            ConnectingPassages.Add(newPassage);
        }

        /// <summary>
        /// Removes a passage from the connecting passage
        /// </summary>
        /// <param name="newPassage"></param>
        public void removePassage(Passage newPassage) {
            ConnectingPassages.Remove(newPassage);
        }

        /// <summary>
        /// Adds a new connecting destination
        /// </summary>
        /// <param name="newTile"></param>
        public void addTile(DestinationTile newTile) {
            ConnectingDestinations.Add(newTile);
        }

        /// <summary>
        /// Removes a connected destination
        /// </summary>
        /// <param name="newTile"></param>
        public void removeTile(DestinationTile newTile) {
            ConnectingDestinations.Remove(newTile);
        }
    }
    class TransportTile : Tile
    {
        public TransportTile(int x, int y) : base(x, y) {
        }

    }

    class RoadTile : Tile
    {
        public RoadTile(int x, int y) : base(x, y)
        {
        }
    }

}
