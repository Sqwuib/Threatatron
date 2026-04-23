namespace Threatotron
{
    /// <summary>
    /// Represents the obstacle system that manages all obstacles.
    /// </summary>
    public class ObstacleSystem
    {
        /// <summary>
        /// Gets the list of obstacles in the system.
        /// </summary>
        public List<IObstacle> IObstacleList { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObstacleSystem"/> class.
        /// </summary>
        public ObstacleSystem()
        {
            IObstacleList = new List<IObstacle>();
        }

        /// <summary>
        /// Adds an obstacle to the obstacle system.
        /// </summary>
        /// <param name="obstacle">The obstacle to add.</param>
        public void AddObstacle(IObstacle obstacle)
        {
            IObstacleList.Add(obstacle);
        }

        /// <summary>
        /// Determines whether there is an obstacle at the specified coordinates.
        /// </summary>
        /// <param name="x">The X coordinate to check.</param>
        /// <param name="y">The Y coordinate to check.</param>
        /// <returns><c>true</c> if there is an obstacle at the specified coordinates; otherwise, <c>false</c>.</returns>
        public bool ContainsObstacleAt(int x, int y)
        {
            return IObstacleList.Any(obstacle => obstacle.obstacleX == x && obstacle.obstacleY == y);
        }

        /// <summary>
        /// Gets the obstacles within a specified region.
        /// </summary>
        /// <param name="startX">The starting X coordinate of the region.</param>
        /// <param name="startY">The starting Y coordinate of the region.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <returns>A list of obstacles within the specified region.</returns>
        public List<IObstacle> GetObstaclesInRegion(int startX, int startY, int width, int height)
        {
            List<IObstacle> obstaclesInRegion = new List<IObstacle>();

            foreach (var obstacle in IObstacleList)
            {
                if (obstacle is Camera || obstacle is Sensor)
                {
                    obstaclesInRegion.Add(obstacle); // Always add cameras and sensors, they have range calculations.
                }
                else if (obstacle.obstacleX >= startX && obstacle.obstacleX < startX + width &&
                         obstacle.obstacleY >= startY && obstacle.obstacleY < startY + height)
                {
                    obstaclesInRegion.Add(obstacle); // Obstacles whose origin is within the region
                }
                else if (obstacle is Fence fence)
                {
                    if (fence.orientation == "north")
                    {
                        for (int i = 0; i < fence.length; i++)
                        {
                            int y = fence.obstacleY + i;
                            if (y >= startY && y < startY + height && fence.obstacleX >= startX && fence.obstacleX < startX + width)
                            {
                                obstaclesInRegion.Add(fence);
                                break; // Only add once
                            }
                        }
                    }
                    else if (fence.orientation == "east")
                    {
                        for (int i = 0; i < fence.length; i++)
                        {
                            int x = fence.obstacleX + i;
                            if (x >= startX && x < startX + width && fence.obstacleY >= startY && fence.obstacleY < startY + height)
                            {
                                obstaclesInRegion.Add(fence);
                                break; // Only add once
                            }
                        }
                    }
                }
            }

            return obstaclesInRegion;
        }
    }
}
