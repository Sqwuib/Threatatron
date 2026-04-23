namespace Threatotron
{
    /// <summary>
    /// Provides functionality to check if a location is safe.
    /// </summary>
    public class CheckLocation
    {
        private int obstacleX;
        private int obstacleY;
        private ObstacleSystem obstacleSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckLocation"/> class.
        /// </summary>
        /// <param name="obstacleX">The X coordinate to check.</param>
        /// <param name="obstacleY">The Y coordinate to check.</param>
        /// <param name="obstacleSystem">The obstacle system to use for checks.</param>
        public CheckLocation(int obstacleX, int obstacleY, ObstacleSystem obstacleSystem)
        {
            this.obstacleX = obstacleX;
            this.obstacleY = obstacleY;
            this.obstacleSystem = obstacleSystem;
        }

        /// <summary>
        /// Determines whether the location is safe.
        /// </summary>
        /// <returns><c>true</c> if the location is safe; otherwise, <c>false</c>.</returns>
        public bool IsLocationSafe()
        {
            return CheckSafety(obstacleX, obstacleY);
        }

        /// <summary>
        /// Gets the safe directions from the current location.
        /// </summary>
        /// <returns>A list of safe directions.</returns>
        public List<string> GetSafeDirections()
        {
            List<string> safeDirections = new List<string>();

            if (CheckSafety(obstacleX, obstacleY + 1))
                safeDirections.Add("North");
            if (CheckSafety(obstacleX, obstacleY - 1))
                safeDirections.Add("South");
            if (CheckSafety(obstacleX + 1, obstacleY))
                safeDirections.Add("East");
            if (CheckSafety(obstacleX - 1, obstacleY))
                safeDirections.Add("West");

            return safeDirections;
        }

        private bool CheckSafety(int x, int y)
        {
            foreach (IObstacle obstacle in obstacleSystem.IObstacleList)
            {
                if (obstacle is Guard guard)
                {
                    if (guard.obstacleX == x && guard.obstacleY == y)
                    {
                        return false; // Location is not safe
                    }
                }
                else if (obstacle is Fence fence)
                {
                    if (fence.orientation == "north")
                    {
                        if (fence.obstacleX == x && y >= fence.obstacleY && y < fence.obstacleY + fence.length)
                        {
                            return false; // Location is not safe
                        }
                    }
                    else if (fence.orientation == "east")
                    {
                        if (fence.obstacleY == y && x >= fence.obstacleX && x < fence.obstacleX + fence.length)
                        {
                            return false; // Location is not safe
                        }
                    }
                }
                else if (obstacle is Sensor sensor)
                {
                    if (Math.Pow(x - sensor.obstacleX, 2) + Math.Pow(y - sensor.obstacleY, 2) <= Math.Pow(sensor.range, 2))
                    {
                        return false; // Location is not safe
                    }
                }
                else if (obstacle is Camera camera)
                {
                    int deltaX = x - camera.obstacleX;
                    int deltaY = y - camera.obstacleY;
                    switch (camera.direction)
                    {
                        case "north":
                            if (deltaY >= 0 && Math.Abs(deltaX) <= deltaY)
                                return false; // Location is not safe
                            break;
                        case "south":
                            if (deltaY <= 0 && Math.Abs(deltaX) <= -deltaY)
                                return false; // Location is not safe
                            break;
                        case "east":
                            if (deltaX >= 0 && Math.Abs(deltaY) <= deltaX)
                                return false; // Location is not safe
                            break;
                        case "west":
                            if (deltaX <= 0 && Math.Abs(deltaY) <= -deltaX)
                                return false; // Location is not safe
                            break;
                    }
                }
            }
            return true; // Location is safe
        }
    }
}
