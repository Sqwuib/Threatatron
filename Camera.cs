namespace Threatotron
{
    /// <summary>
    /// Represents a camera obstacle.
    /// </summary>
    public class Camera : IObstacle
    {

        public int obstacleX { get; set; }

        public int obstacleY { get; set; }

        public string direction { get; set; }

        public int cameraCount { get; set; }

        public string name { get; set; } = "Camera";

        private static List<Camera> cameras = new List<Camera>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <param name="obstacleX">The X coordinate of the camera.</param>
        /// <param name="obstacleY">The Y coordinate of the camera.</param>
        /// <param name="direction">The direction the camera is facing.</param>
        /// <param name="cameraCount">The count of cameras.</param>
        public Camera(int obstacleX, int obstacleY, string direction, int cameraCount)
        {
            this.obstacleX = obstacleX;
            this.obstacleY = obstacleY;
            this.direction = direction;
            this.cameraCount = cameraCount;
        }

        /// <summary>
        /// Adds a camera obstacle to the system.
        /// </summary>
        /// <param name="obstacleX">The X coordinate of the camera.</param>
        /// <param name="obstacleY">The Y coordinate of the camera.</param>
        /// <param name="direction">The direction the camera is facing.</param>
        /// <param name="cameraCount">The count of cameras.</param>
        /// <param name="obstacleSystem">The obstacle system to add the camera to.</param>
        public static void addCamera(int obstacleX, int obstacleY, string direction, int cameraCount, ObstacleSystem obstacleSystem)
        {
            Camera camera = new Camera(obstacleX, obstacleY, direction, cameraCount);
            camera.name = "camera" + cameraCount;
            cameras.Add(camera);
            obstacleSystem.AddObstacle(camera);

            Console.WriteLine("Successfully added camera obstacle.");
        }

        /// <summary>
        /// Gets the list of cameras.
        /// </summary>
        /// <returns>A list of <see cref="Camera"/> objects.</returns>
        public static List<Camera> GetCameras()
        {
            return cameras;
        }
    }
}
