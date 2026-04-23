namespace Threatotron
{
    /// <summary>
    /// Represents a node in the grid.
    /// </summary>
    public class Node
    {

        public int X { get; set; }

        public int Y { get; set; }

        public bool IsObstacle { get; set; }

        public string ObstacleType { get; set; }


        public float GCost { get; set; }

        public float HCost { get; set; }

        /// <summary>
        /// Gets the F cost of the node.
        /// </summary>
        public float FCost => GCost + HCost;

        /// <summary>
        /// Gets or sets the parent node from which this node was reached.
        /// </summary>
        public Node? CameFrom { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="x">The X coordinate of the node.</param>
        /// <param name="y">The Y coordinate of the node.</param>
        /// <param name="isObstacle">If set to <c>true</c>, the node is an obstacle.</param>
        /// <param name="obstacleType">The type of obstacle.</param>
        public Node(int x, int y, bool isObstacle = false, string obstacleType = "")
        {
            X = x;
            Y = y;
            IsObstacle = isObstacle;
            ObstacleType = obstacleType;
        }
    }
}
