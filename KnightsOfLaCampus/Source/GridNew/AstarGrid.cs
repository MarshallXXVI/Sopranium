using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace KnightsOfLaCampus.Source.GridNew
{
    /// <summary>
    /// The Grid nodes, that the A Star is going to work with
    /// </summary>
    internal sealed class GridNode : GridBox
    {
        public GridNode Parent { get; set; }

        public float DistanceToTarget { get; set; }
        public float Cost { get; set; }

        // The position in the Grid of GridNodes later
        // This is not the Rectangle position of the Box itself
        public Vector2 Position { get; }
        public float Weight { get; }
        public float F
        {
            get
            {
                if ((int)DistanceToTarget != -1 && (int)Cost != -1)
                {
                    return DistanceToTarget + Cost;
                }

                return -1;
            }
        }

        /// <summary>
        /// Constructor for GridNode
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public GridNode(int x, int y)
        {
            Position = new Vector2(x, y);
            Parent = null;
            DistanceToTarget = -1;

            // For us the Costs and weight are always init with 1
            Cost = 1;
            Weight = 1;
        }

    }

    /// <summary>
    /// The Class to calculate the A Star path. Consider it as a black box
    /// </summary>
    internal sealed class AstarGrid
    {
        // The Grid the A Star has to deal with
        private readonly Grid mGrid;

        /// <summary>
        /// Constructor for the A Star class
        /// </summary>
        /// <param name="grid"></param>
        public AstarGrid(Grid grid)
        {
            mGrid = grid;
            for (var i = 0; i < grid.GridDimensions.X; i++)
            {
                for (var j = 0; j < grid.GridDimensions.Y; j++)
                {
                    // copy from gridbox to grid node and sets collisions
                    var newNode = new GridNode(i, j);
                    newNode.CollisionOn = grid.GetBoxAt(new Vector2(i, j)).CollisionOn;
                    mGrid.SetBoxAt(new Vector2(i, j), newNode);
                }
            }
        }

        /// <summary>
        /// Finds the shortest path (stack of GridNodes)
        /// GridNode contains a Position.X and Position.Y property 
        /// </summary>
        public Stack<GridNode> FindPath(Vector2 pStart, Vector2 pEnd)
        {
            var start = new GridNode((int)pStart.X, (int)pStart.Y);
            var end = new GridNode((int)pEnd.X, (int)pEnd.Y);
            end.CollisionOn = false;

            var path = new Stack<GridNode>();

            // If the start Node should be already included in the path
            // use path.Push(start); here

            var openList = new PriorityQueue<GridNode, float>();
            var closedList = new List<GridNode>();

            var current = start;

            openList.Enqueue(start, start.F);


            // Calculates the shortest path
            while (openList.Count != 0 && !closedList.Exists(x => x.Position == end.Position))
            {
                current = openList.Dequeue();
                closedList.Add(current);

                var adjacentNodes = GetAdjacentNodes(current);

                foreach (var n in adjacentNodes)
                {
                    if (!closedList.Contains(n) && !n.CollisionOn)
                    {
                        var isFound = false;
                        foreach (var oLNode in openList.UnorderedItems)
                        {
                            if (oLNode.Element == n)
                            {
                                isFound = true;
                            }
                        }
                        if (!isFound)
                        {
                            n.Parent = current;
                            n.DistanceToTarget = Math.Abs(n.Position.X - end.Position.X) + Math.Abs(n.Position.Y - end.Position.Y);
                            n.Cost = n.Weight + n.Parent.Cost;
                            openList.Enqueue(n, n.F);
                        }
                    }
                }
            }

            // construct path, if end was not closed return en empty stack
            if (!closedList.Exists(x => x.Position == end.Position))
            {
                return new Stack<GridNode>();
            }

            // If there is a path, return it. Otherwise just an empty stack
            var temp = closedList[closedList.IndexOf(current)];
            if (temp == null)
            {
                return new Stack<GridNode>();
            }
            do
            {
                path.Push(temp);
                temp = temp.Parent;
            } while (temp != start && temp != null);
            return path;
        }


        /// <summary>
        /// Finds the AdjacentNodes of one GridNode 
        /// </summary>
        private List<GridNode> GetAdjacentNodes(GridNode node)
        {
            var adjacentNodes = new List<GridNode>();

            var row = (int)node.Position.Y;
            var col = (int)node.Position.X;

            // Adds each adjacent node to the GridNode node
            if (row + 1 < mGrid.GridDimensions.Y)
            {
                adjacentNodes.Add((GridNode)mGrid.GetBoxAt(new Vector2(col, row + 1)));
            }
            if (row - 1 >= 0)
            {
                adjacentNodes.Add((GridNode)mGrid.GetBoxAt(new Vector2(col, row - 1)));
            }
            if (col - 1 >= 0)
            {
                adjacentNodes.Add((GridNode)mGrid.GetBoxAt(new Vector2(col - 1, row)));
            }
            if (col + 1 < mGrid.GridDimensions.X)
            {
                adjacentNodes.Add((GridNode)mGrid.GetBoxAt(new Vector2(col + 1, row)));
            }

            return adjacentNodes;
        }
    }
}
