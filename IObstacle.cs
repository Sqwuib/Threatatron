namespace Threatotron
{
    /// <summary>
    /// Represents a generic obstacle.
    /// </summary>
    public interface IObstacle
    {
        /// <summary>
        /// Gets the name of the obstacle.
        /// </summary>
        string name { get; }

        /// <summary>
        /// Gets the X coordinate of the obstacle.
        /// </summary>
        int obstacleX { get; }

        /// <summary>
        /// Gets the Y coordinate of the obstacle.
        /// </summary>
        int obstacleY { get; }
    }
}