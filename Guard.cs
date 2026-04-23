namespace Threatotron
{
    /// <summary>
    /// Represents a guard obstacle.
    /// </summary>
    internal class Guard : IObstacle
    {
        /// <summary>
        /// Gets or sets the name of the guard.
        /// </summary>
        public string name { get; set; } = "Guard";

        /// <summary>
        /// Gets or sets the X coordinate of the guard.
        /// </summary>
        public int obstacleX { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the guard.
        /// </summary>
        public int obstacleY { get; set; }

        /// <summary>
        /// Gets the count of guards.
        /// </summary>
        public int guardCount { get; }

        private static List<Guard> guards = new List<Guard>();

        /// <summary>
        /// Adds a guard obstacle to the system.
        /// </summary>
        /// <param name="obstacleX">The X coordinate of the guard.</param>
        /// <param name="obstacleY">The Y coordinate of the guard.</param>
        /// <param name="guardCount">The count of guards.</param>
        /// <param name="obstacleSystem">The obstacle system to add the guard to.</param>
        public static void addGuard(int obstacleX, int obstacleY, int guardCount, ObstacleSystem obstacleSystem)
        {
            Guard guard = new Guard(obstacleX, obstacleY);
            guard.name = "Guard" + guardCount;
            guard.obstacleX = obstacleX;
            guard.obstacleY = obstacleY;
            guards.Add(guard);
            obstacleSystem.AddObstacle(guard);

            Console.WriteLine("Successfully added guard obstacle.");
        }

        /// <summary>
        /// Gets the list of guards.
        /// </summary>
        /// <returns>A list of <see cref="Guard"/> objects.</returns>
        public static List<Guard> GetGuards()
        {
            return guards;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Guard"/> class.
        /// </summary>
        /// <param name="x">The X coordinate of the guard.</param>
        /// <param name="y">The Y coordinate of the guard.</param>
        public Guard(int x, int y)
        {
            obstacleX = x;
            obstacleY = y;
        }
    }
}
