namespace Threatotron
{
    /// <summary>
    /// Provides functionality to build and update a grid with obstacles.
    /// </summary>
    public static class GridBuilder
    {
        /// <summary>
        /// Initializes a grid with the specified dimensions.
        /// </summary>
        /// <param name="width">The width of the grid.</param>
        /// <param name="height">The height of the grid.</param>
        /// <returns>A two-dimensional array of <see cref="Node"/> objects representing the grid.</returns>
        public static Node[,] InitializeGrid(int width, int height)
        {
            Node[,] grid = new Node[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    grid[y, x] = new Node(x, y, false, ""); // Initialize all nodes as non-obstacles
                }
            }
            return grid;
        }

        /// <summary>
        /// Updates the grid with obstacles from the obstacle system.
        /// </summary>
        /// <param name="grid">The grid to update.</param>
        /// <param name="obstacleSystem">The obstacle system containing the obstacles.</param>
        /// <param name="xOffset">The X offset to adjust obstacle coordinates.</param>
        /// <param name="yOffset">The Y offset to adjust obstacle coordinates.</param>
        public static void UpdateGridWithObstacles(Node[,] grid, ObstacleSystem obstacleSystem, int xOffset, int yOffset)
        {
            int gridWidth = grid.GetLength(1);
            int gridHeight = grid.GetLength(0);

            foreach (var obstacle in obstacleSystem.IObstacleList)
            {
                int adjustedX = obstacle.obstacleX - xOffset;
                int adjustedY = obstacle.obstacleY - yOffset;

                if (adjustedX >= 0 && adjustedX < gridWidth && adjustedY >= 0 && adjustedY < gridHeight)
                {
                    if (obstacle is Guard)
                    {
                        grid[adjustedY, adjustedX].IsObstacle = true;
                        grid[adjustedY, adjustedX].ObstacleType = "Guard";
                    }
                    else if (obstacle is Fence fence)
                    {
                        for (int i = 0; i < fence.length; i++)
                        {
                            if (fence.orientation == "north")
                            {
                                grid[adjustedY + i, adjustedX].IsObstacle = true;
                                grid[adjustedY + i, adjustedX].ObstacleType = "Fence";
                            }
                            else if (fence.orientation == "east")
                            {
                                grid[adjustedY, adjustedX + i].IsObstacle = true;
                                grid[adjustedY, adjustedX + i].ObstacleType = "Fence";
                            }
                        }
                    }
                    else if (obstacle is Sensor sensor)
                    {
                        for (int i = adjustedY - (int)sensor.range; i <= adjustedY + (int)sensor.range; i++)
                        {
                            for (int j = adjustedX - (int)sensor.range; j <= adjustedX + (int)sensor.range; j++)
                            {
                                if (i >= 0 && i < grid.GetLength(0) && j >= 0 && j < grid.GetLength(1))
                                {
                                    if (Math.Pow(i - adjustedY, 2) + Math.Pow(j - adjustedX, 2) <= Math.Pow(sensor.range, 2))
                                    {
                                        grid[i, j].IsObstacle = true;
                                        grid[i, j].ObstacleType = "Sensor";
                                    }
                                }
                            }
                        }
                    }
                    else if (obstacle is Camera camera)
                    {
                        int coneWidth = 1;
                        for (int deltaX = 0; deltaX < 100; deltaX++)
                        {
                            int maxDeltaY = (coneWidth - 1) / 2;
                            for (int deltaY = -maxDeltaY; deltaY <= maxDeltaY; deltaY++)
                            {
                                int x = adjustedX;
                                int y = adjustedY;

                                switch (camera.direction)
                                {
                                    case "east":
                                        x = adjustedX + deltaX;
                                        y = adjustedY + deltaY;
                                        break;
                                    case "north":
                                        x = adjustedX - deltaY;
                                        y = adjustedY + deltaX;
                                        break;
                                    case "west":
                                        x = adjustedX - deltaX;
                                        y = adjustedY - deltaY;
                                        break;
                                    case "south":
                                        x = adjustedX + deltaY;
                                        y = adjustedY - deltaX;
                                        break;
                                }

                                if (x >= 0 && x < grid.GetLength(1) && y >= 0 && y < grid.GetLength(0))
                                {
                                    grid[y, x].IsObstacle = true;
                                    grid[y, x].ObstacleType = "Camera";
                                }
                            }
                            coneWidth += 2;
                        }
                    }
                }
            }
        }
    }
}
