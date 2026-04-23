namespace Threatotron
{
    /// <summary>
    /// Represents a fence obstacle.
    /// </summary>
    public class Fence : IObstacle
    {
  
        public string name { get; set; } = "Fence";

  
        public int obstacleX { get; set; }

        public int obstacleY { get; set; }

        public string orientation { get; set; }

        public int length { get; set; }


        public int fenceCount { get; set; }

        private static List<Fence> fences = new List<Fence>();

        /// <summary>
        /// Adds a fence obstacle to the system.
        /// </summary>
        /// <param name="obstacleX">The X coordinate of the fence.</param>
        /// <param name="obstacleY">The Y coordinate of the fence.</param>
        /// <param name="length">The length of the fence.</param>
        /// <param name="orientation">The orientation of the fence.</param>
        /// <param name="fenceCount">The count of fences.</param>
        /// <param name="obstacleSystem">The obstacle system to add the fence to.</param>
        public static void addFence(int obstacleX, int obstacleY, int length, string orientation, int fenceCount, ObstacleSystem obstacleSystem)
        {
            Fence fence = new Fence(obstacleX, obstacleY, orientation, length, fenceCount);
            fence.name = "Fence" + fenceCount;
            fences.Add(fence);
            obstacleSystem.AddObstacle(fence);

            Console.WriteLine("Successfully added fence obstacle.");
        }

        /// <summary>
        /// Gets the list of fences.
        /// </summary>
        /// <returns>A list of <see cref="Fence"/> objects.</returns>
        public static List<Fence> GetFences()
        {
            return fences;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fence"/> class.
        /// </summary>
        /// <param name="X">The X coordinate of the fence.</param>
        /// <param name="Y">The Y coordinate of the fence.</param>
        /// <param name="Orientation">The orientation of the fence.</param>
        /// <param name="Length">The length of the fence.</param>
        /// <param name="FenceCount">The count of fences.</param>
        public Fence(int X, int Y, string Orientation, int Length, int FenceCount)
        {
            obstacleX = X;
            obstacleY = Y;
            orientation = Orientation;
            length = Length;
            fenceCount = FenceCount;
        }
    }
}
