namespace Threatotron
{
    /// <summary>
    /// Provides functionality to find a path using the A* algorithm.
    /// </summary>
    public class FindPath
    {
        private Node[,]? grid;
        private readonly ObstacleSystem obstacleSystem;
        private int gridWidth;
        private int gridHeight;
        private int xOffset;
        private int yOffset;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindPath"/> class.
        /// </summary>
        /// <param name="obstacleSystem">The obstacle system to use.</param>
        public FindPath(ObstacleSystem obstacleSystem)
        {
            this.obstacleSystem = obstacleSystem ?? throw new ArgumentNullException(nameof(obstacleSystem));
        }

        /// <summary>
        /// Handles the pathfinding command.
        /// </summary>
        /// <param name="agentX">The X coordinate of the agent.</param>
        /// <param name="agentY">The Y coordinate of the agent.</param>
        /// <param name="objectiveX">The X coordinate of the objective.</param>
        /// <param name="objectiveY">The Y coordinate of the objective.</param>
        public void HandlePathCommand(int agentX, int agentY, int objectiveX, int objectiveY)
        {
            int minX = Math.Min(agentX, objectiveX);
            int minY = Math.Min(agentY, objectiveY);
            int maxX = Math.Max(agentX, objectiveX);
            int maxY = Math.Max(agentY, objectiveY);

            foreach (var obstacle in obstacleSystem.IObstacleList)
            {
                if (obstacle is Sensor sensor)
                {
                    minX = Math.Min(minX, obstacle.obstacleX - (int)sensor.range);
                    minY = Math.Min(minY, obstacle.obstacleY - (int)sensor.range);
                    maxX = Math.Max(maxX, obstacle.obstacleX + (int)sensor.range);
                    maxY = Math.Max(maxY, obstacle.obstacleY + (int)sensor.range);
                }
            }

            int padding = 10;

            gridWidth = (maxX - minX) + 1 + padding * 2;
            gridHeight = (maxY - minY) + 1 + padding * 2;

            xOffset = minX - padding;
            yOffset = minY - padding;

            grid = GridBuilder.InitializeGrid(gridWidth, gridHeight);
            GridBuilder.UpdateGridWithObstacles(grid, obstacleSystem, xOffset, yOffset);

            int adjustedAgentX = agentX - xOffset;
            int adjustedAgentY = agentY - yOffset;
            int adjustedObjectiveX = objectiveX - xOffset;
            int adjustedObjectiveY = objectiveY - yOffset;

            if (adjustedAgentX == adjustedObjectiveX && adjustedAgentY == adjustedObjectiveY)
            {
                Console.WriteLine("Error: Start and destination coordinates are the same.");
                return;
            }

            if (grid[adjustedAgentY, adjustedAgentX].IsObstacle || grid[adjustedObjectiveY, adjustedObjectiveX].IsObstacle)
            {
                Console.WriteLine("Error: Start or destination is blocked by an obstacle.");
                return;
            }

            Node startNode = grid[adjustedAgentY, adjustedAgentX];
            Node targetNode = grid[adjustedObjectiveY, adjustedObjectiveX];

            List<Node> path = ExecuteAStar(startNode, targetNode);

            if (path.Count > 0)
            {
                List<string> directions = GenerateDirections(path);
                Console.WriteLine("The following path will take you to the objective:");
                foreach (string direction in directions)
                {
                    Console.WriteLine(direction);
                }
            }
            else
            {
                Console.WriteLine("There is no safe path to the objective.");
            }
        }

        private List<Node> ExecuteAStar(Node startNode, Node targetNode)
        {
            List<Node> openSet = new List<Node> { startNode };
            List<Node> closedSet = new List<Node>();

            startNode.GCost = 0;
            startNode.HCost = CalculateHeuristic(startNode, targetNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.OrderBy(node => node.FCost).ThenBy(node => node.HCost).First();
                if (currentNode == targetNode)
                {
                    return ReconstructPath(targetNode);
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (Node neighbor in GetNeighbors(currentNode))
                {
                    if (closedSet.Contains(neighbor) || neighbor.IsObstacle)
                        continue;

                    float tentativeGCost = currentNode.GCost + CalculateDistance(currentNode, neighbor);
                    if (tentativeGCost < neighbor.GCost || !openSet.Contains(neighbor))
                    {
                        neighbor.CameFrom = currentNode;
                        neighbor.GCost = tentativeGCost;
                        neighbor.HCost = CalculateHeuristic(neighbor, targetNode);

                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                    }
                }
            }

            return new List<Node>(); // No path found
        }

        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();
            int x = node.X;
            int y = node.Y;

            if (x > 0) neighbors.Add(grid![y, x - 1]);
            if (x < gridWidth - 1) neighbors.Add(grid![y, x + 1]);
            if (y > 0) neighbors.Add(grid![y - 1, x]);
            if (y < gridHeight - 1) neighbors.Add(grid![y + 1, x]);

            return neighbors;
        }

        private float CalculateDistance(Node nodeA, Node nodeB)
        {
            return Math.Abs(nodeA.X - nodeB.X) + Math.Abs(nodeA.Y - nodeB.Y);
        }

        private float CalculateHeuristic(Node nodeA, Node nodeB)
        {
            return Math.Abs(nodeA.X - nodeB.X) + Math.Abs(nodeA.Y - nodeB.Y);
        }

        private List<Node> ReconstructPath(Node endNode)
        {
            List<Node> path = new List<Node>();
            Node? currentNode = endNode;
            while (currentNode != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.CameFrom;
            }
            path.Reverse();
            return path;
        }

        private List<string> GenerateDirections(List<Node> path)
        {
            List<string> directions = new List<string>();
            if (path.Count < 2) return directions;

            int startX = path[0].X;
            int startY = path[0].Y;
            int distance = 0;
            string? currentDirection = null;

            for (int i = 1; i < path.Count; i++)
            {
                int deltaX = path[i].X - path[i - 1].X;
                int deltaY = path[i].Y - path[i - 1].Y;

                string direction = "";
                if (deltaX > 0) direction = "east";
                else if (deltaX < 0) direction = "west";
                else if (deltaY > 0) direction = "north";
                else if (deltaY < 0) direction = "south";

                if (direction == currentDirection)
                {
                    distance++;
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentDirection))
                    {
                        directions.Add($"Head {currentDirection} for {distance} klick{(distance > 1 ? "s" : "")}.");
                    }
                    currentDirection = direction;
                    distance = 1;
                }

                startX = path[i].X;
                startY = path[i].Y;
            }

            if (!string.IsNullOrEmpty(currentDirection))
            {
                directions.Add($"Head {currentDirection} for {distance} klick{(distance > 1 ? "s" : "")}.");
            }

            return directions;
        }
    }
}
