using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding {

    public class PathNode {
        public int GCost { get; set; } = 0;
        public int HCost { get; set; } = 0;
        public int FCost { get; set; } = 0;

        public BoardSpace Space { get; set; }

        public PathNode CameFrom { get; set; }

        public bool Accessible { get; set; }

        public PathNode(BoardSpace space) {
            Space = space;
            GCost = 0;
            HCost = 0;
            FCost = 0;
            CameFrom = null;
            Accessible = !space.IsOccupied;
        }

        public void CalculateFCost() {
            FCost = GCost + HCost;
        }
    }
    
}


