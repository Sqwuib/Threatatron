namespace Threatotron
{
    /// <summary>
    /// Represents a sensor obstacle.
    /// </summary>
    public class Sensor : IObstacle
    {
        /// <summary>
        /// Gets or sets the name of the sensor.
        /// </summary>
        public string name { get; set; } = "Sensor";

        /// <summary>
        /// Gets or sets the X coordinate of the sensor.
        /// </summary>
        public int obstacleX { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the sensor.
        /// </summary>
        public int obstacleY { get; set; }

        /// <summary>
        /// Gets or sets the range of the sensor.
        /// </summary>
        public double range { get; set; }

        /// <summary>
        /// Gets the count of sensors.
        /// </summary>
        public int sensorCount { get; }

        private static List<Sensor> sensors = new List<Sensor>();

        /// <summary>
        /// Adds a sensor obstacle to the system.
        /// </summary>
        /// <param name="obstacleX">The X coordinate of the sensor.</param>
        /// <param name="obstacleY">The Y coordinate of the sensor.</param>
        /// <param name="range">The range of the sensor.</param>
        /// <param name="sensorCount">The count of sensors.</param>
        /// <param name="obstacleSystem">The obstacle system to add the sensor to.</param>
        public static void addSensor(int obstacleX, int obstacleY, double range, int sensorCount, ObstacleSystem obstacleSystem)
        {
            Sensor sensor = new Sensor(obstacleX, obstacleY, range, sensorCount);
            sensor.name = "Sensor" + sensorCount;
            sensors.Add(sensor);
            obstacleSystem.AddObstacle(sensor);

            Console.WriteLine("Successfully added sensor obstacle.");
        }

        /// <summary>
        /// Gets the list of sensors.
        /// </summary>
        /// <returns>A list of <see cref="Sensor"/> objects.</returns>
        public static List<Sensor> GetSensors()
        {
            return sensors;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sensor"/> class.
        /// </summary>
        /// <param name="X">The X coordinate of the sensor.</param>
        /// <param name="Y">The Y coordinate of the sensor.</param>
        /// <param name="Range">The range of the sensor.</param>
        /// <param name="SensorCount">The count of sensors.</param>
        public Sensor(int X, int Y, double Range, int SensorCount)
        {
            obstacleX = X;
            obstacleY = Y;
            range = Range;
            sensorCount = SensorCount;
        }
    }
}
