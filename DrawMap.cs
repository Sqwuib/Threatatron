namespace Threatotron
{
    /// <summary>
    /// Provides functionality to draw a map of the obstacles.
    /// </summary>
    public class DrawMap
    {
        /// <summary>
        /// Gets or sets the width of the map.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the map.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the starting X coordinate of the map.
        /// </summary>
        public int StartX { get; set; }

        /// <summary>
        /// Gets or sets the starting Y coordinate of the map.
        /// </summary>
        public int StartY { get; set; }

        private ObstacleSystem obstacleSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawMap"/> class.
        /// </summary>
        /// <param name="startX">The starting X coordinate of the map.</param>
        /// <param name="startY">The starting Y coordinate of the map.</param>
        /// <param name="width">The width of the map.</param>
        /// <param name="height">The height of the map.</param>
        /// <param name="obstacleSystem">The obstacle system containing the obstacles.</param>
        public DrawMap(int startX, int startY, int width, int height, ObstacleSystem obstacleSystem)
        {
            Width = width;
            Height = height;
            StartX = startX;
            StartY = startY;
            this.obstacleSystem = obstacleSystem;
        }

        /// <summary>
        /// Draws the map of obstacles in the console.
        /// </summary>
        public void Draw()
        {
            char[,] visibleMap = new char[Height, Width];

            // Initialize the map with empty spaces
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    visibleMap[i, j] = '.';
                }
            }

            // Adjust coordinates for grid
            int xOffset = StartX - 10;
            int yOffset = StartY - 10;
            int gridWidth = Width + 20;
            int gridHeight = Height + 20;

            // Initialize and update the grid using GridBuilder
            Node[,] grid = GridBuilder.InitializeGrid(gridWidth, gridHeight);
            GridBuilder.UpdateGridWithObstacles(grid, obstacleSystem, xOffset, yOffset);

            // Populate visibleMap with obstacles from the grid
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    if (grid[y, x].IsObstacle)
                    {
                        int adjustedX = x + xOffset;
                        int adjustedY = y + yOffset;
                        if (IsWithinBounds(adjustedX, adjustedY))
                        {
                            char obstacleChar = GetObstacleChar(grid[y, x].ObstacleType);
                            visibleMap[adjustedY - StartY, adjustedX - StartX] = obstacleChar;
                        }
                    }
                }
            }

            // Print the grid to the console
            Console.WriteLine("Here is a map of obstacles in the selected region:");
            for (int i = Height - 1; i >= 0; i--)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(visibleMap[i, j]);
                }
                Console.WriteLine();
            }
        }

        private bool IsWithinBounds(int x, int y)
        {
            return x >= StartX && x < StartX + Width && y >= StartY && y < StartY + Height;
        }

        private char GetObstacleChar(string obstacleType)
        {
            return obstacleType switch
            {
                "Guard" => 'G',
                "Fence" => 'F',
                "Sensor" => 'S',
                "Camera" => 'C',
                _ => 'O'
            };
        }
    }
}
