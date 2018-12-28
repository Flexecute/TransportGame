using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport {
    /// <summary>
    /// Class to help find fastest routes between tiles
    /// </summary>
    class RouteFinder {

        public RouteFinder() {
        }

        /// <summary>
        /// Finds the closest tile of a given type to a given source
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destType"></param>
        /// <returns></returns>
        public List<Passage> closestDestination(DestinationTile source, Type destType) {
            List<Passage> route = new List<Passage>();

            return route;
        }

        public List<Passage> fastestRoute(DestinationTile source, DestinationTile dest) {
            List<Passage> route = new List<Passage>();

            return route;
        }
    }
}
