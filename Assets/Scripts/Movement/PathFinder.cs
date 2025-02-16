using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding {

    public static class PathFinder {

        public static Path FindPath(BoardSpace[,] board, BoardSpace start, BoardSpace end, int maxDistance = Path.NO_MAX) {

            PathNode[,] pathGrid = InitPathGrid(board);

            PathNode startNode = pathGrid[start.Cell.x, start.Cell.y];
            PathNode endNode = pathGrid[end.Cell.x, end.Cell.y];

            List<PathNode> openList = new List<PathNode>() { startNode };
            List<PathNode> closedList = new List<PathNode>();

            // Initialize Path
            for(int x = 0; x < pathGrid.GetLength(0); x++) {
                for(int y = 0; y < pathGrid.GetLength(1); y++) {
                    PathNode pathNode = pathGrid[x, y];
                    pathNode.GCost = int.MaxValue;
                    pathNode.CalculateFCost();
                    pathNode.CameFrom = null;
                }
            }

            startNode.GCost = 0;
            startNode.HCost = startNode.Space.DistanceTo(endNode.Space); 
            startNode.CalculateFCost();

            // Cycle to calculate costs
            while(openList.Count > 0) {

                PathNode currentNode = GetLowestFCostNode(openList);
                
                // We reached our destination
                if(currentNode == endNode) {
                    return GetPath(endNode, maxDistance);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach(PathNode neighborNode in GetNeighborList(pathGrid, currentNode)) {
                    
                    if(closedList.Contains(neighborNode)) {
                        continue;
                    }

                    if(!neighborNode.Accessible) {
                        closedList.Add(neighborNode);
                        continue;
                    }

                    int tentativeGCost = currentNode.GCost + currentNode.Space.DistanceTo(neighborNode.Space);

                    if(tentativeGCost < neighborNode.GCost) {
                        neighborNode.CameFrom = currentNode;
                        neighborNode.GCost = tentativeGCost;
                        neighborNode.HCost = neighborNode.Space.DistanceTo(endNode.Space);
                        neighborNode.CalculateFCost();
                    }

                    if(!openList.Contains(neighborNode)) {
                        openList.Add(neighborNode);
                    }
                }
            }

            // Inaccessible endpoint
            return Path.Invalid();
        }

        private static PathNode[,] InitPathGrid(BoardSpace[,] board)
        {
            PathNode[,] pathGrid = new PathNode[board.GetLength(0), board.GetLength(1)];

            for(int i = 0; i < board.GetLength(0); i++) {
                for(int j = 0; j < board.GetLength(1); j++) {
                    pathGrid[i, j] = new PathNode(board[i, j]);
                }   
            }

            return pathGrid;
        }

        private static List<PathNode> GetNeighborList(PathNode[,] pathGrid, PathNode currentNode) {
            
            List<PathNode> neighborList = new List<PathNode>();

            int width = pathGrid.GetLength(0);
            int height = pathGrid.GetLength(1);

            if(currentNode.Space.Cell.x > 0) {
                neighborList.Add(pathGrid[currentNode.Space.Cell.x - 1, currentNode.Space.Cell.y]);
            }

            if(currentNode.Space.Cell.x < width - 1) {
                neighborList.Add(pathGrid[currentNode.Space.Cell.x + 1, currentNode.Space.Cell.y]);
            }

            if(currentNode.Space.Cell.y > 0) {
                neighborList.Add(pathGrid[currentNode.Space.Cell.x, currentNode.Space.Cell.y - 1]);
            }

            if(currentNode.Space.Cell.y < height - 1) {
                neighborList.Add(pathGrid[currentNode.Space.Cell.x, currentNode.Space.Cell.y + 1]);
            }

            return neighborList;
        }

        private static Path GetPath(PathNode endNode, int maxDistance = Path.NO_MAX) {
            
            List<PathNode> path = new List<PathNode> { endNode };
            PathNode currentNode = endNode;
            while(currentNode.CameFrom != null) {
                path.Add(currentNode.CameFrom);
                currentNode = currentNode.CameFrom;
            }
            path.Reverse();
            
            return new Path(path, maxDistance);
        }

        private static int CalculateDistance(PathNode a, PathNode b) {
            int xDistance = Mathf.Abs(a.Space.Cell.x - b.Space.Cell.x);
            int yDistance = Mathf.Abs(a.Space.Cell.y - b.Space.Cell.y);
            return Mathf.Abs(xDistance - yDistance);
        }

        // TODO: Look into Priortiy Queue
        private static PathNode GetLowestFCostNode(List<PathNode> pathNodeList) {

            PathNode lowestFCostPathNode = pathNodeList[0];

            for(int i = 1; i < pathNodeList.Count; i++) {
                if(pathNodeList[i].FCost < lowestFCostPathNode.FCost) {
                    lowestFCostPathNode = pathNodeList[i];
                }
            }

            return lowestFCostPathNode;
        }
    }

}
