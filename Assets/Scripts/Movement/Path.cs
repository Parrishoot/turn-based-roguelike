using System.Collections.Generic;
using System.Linq;

namespace Pathfinding {
    public class Path {
        public const int NO_MAX = -1;

        public bool IsValid {
            get { return valid; }
        }

        public Queue<BoardSpace> Spaces { get; private set; }

        private BoardSpace destination;

        public BoardSpace Destination {
            get {
                return destination;
            }
        }

        private bool valid = true;

        public Path(List<PathNode> pathNodes, int maxDistance = NO_MAX) {
            
            if(pathNodes == null) {
                valid = false;
                return;
            }
            
            destination = pathNodes.Count == 0 ? null : pathNodes.Select(x => x.Space).Last();

            Spaces = new Queue<BoardSpace>(pathNodes.Select(x => x.Space).ToList());
            valid = maxDistance == NO_MAX || Spaces.Count <= maxDistance + 1;

        }

        public static Path Invalid() {
            return new Path(null);
        }

        public static Path Make(List<BoardSpace> spaces) {
            return new Path(spaces.Select(x => new PathNode(x)).ToList());
        }
    }
}

