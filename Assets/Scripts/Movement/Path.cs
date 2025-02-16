using System.Collections.Generic;
using System.Linq;

namespace Pathfinding {
    public class Path {
        public const int NO_MAX = -1;

        public bool IsValid {
            get { return valid; }
        }

        public Queue<BoardSpace> Spaces { get; private set; }

        private bool valid = true;

        public Path(List<PathNode> pathNodes, int maxDistance = NO_MAX) {
            
            if(pathNodes == null) {
                valid = false;
                return;
            }
            
            Spaces = new Queue<BoardSpace>(pathNodes.Select(x => x.Space).ToList());
            valid = maxDistance == NO_MAX || Spaces.Count <= maxDistance + 1;

        }

        public static Path Invalid() {
            return new Path(null);
        }
    }
}

