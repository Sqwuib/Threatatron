namespace Threatotron
{
    /// <summary>
    /// Provides the menu functionality for the Threat-o-tron 9000 Obstacle Avoidance System.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// The welcome message and list of valid commands.
        /// </summary>
        public static string message = "Welcome to the Threat-o-tron 9000 Obstacle Avoidance System.\r\n\r\n" +
                "Valid commands are:\r\nadd guard <x> <y>: registers a guard obstacle\r\n" +
                "add fence <x> <y> <orientation> <length>: registers a fence obstacle. Orientation must be 'east' or 'north'.\r\n" +
                "add sensor <x> <y> <radius>: registers a sensor obstacle\r\n" +
                "add camera <x> <y> <direction>: registers a camera obstacle. Direction must be 'north', 'south', 'east' or 'west'.\r\n" +
                "check <x> <y>: checks whether a location and its surroundings are safe\r\n" +
                "map <x> <y> <width> <height>: draws a text-based map of registered obstacles\r\n" +
                "path <agent x> <agent y> <objective x> <objective y>: finds a path free of obstacles\r\n" +
                "help: displays this help message\r\n" +
                "exit: closes this program";

        private static string notimplemented = "Apologies, this feature is not implemented yet.";

 
        public static string? obstacletype;


        public static ObstacleSystem obstacleSystem = new ObstacleSystem();


        public static List<string> validObstacleTypes = new List<string> { "guard", "fence", "sensor", "camera" };

        public static List<string> validCameraOrientation = new List<string> { "north", "south", "east", "west" };

        public static List<string> validFenceOrientation = new List<string> { "north", "east" };

        public static List<string> validCommands = new List<string> { "add", "check", "map", "path", "help", "exit" };

        public static int obstacleX;
        public static int obstacleY;

        public static int agentX;
        public static int agentY;

        public static int objectiveX;
        public static int objectiveY;

        public static string? orientation;
        public static int length;
        public static int width;
        public static int height;
        public static double range;
        public static string? direction;

        public static int guardCount = 0;
        public static int fenceCount = 0;
        public static int sensorCount = 0;
        public static int cameraCount = 0;

        private static bool exit = false;

        private readonly CommandHandler commandHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <param name="commandHandler">The command handler to use.</param>
        public Menu(CommandHandler commandHandler)
        {
            this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
        }

        /// <summary>
        /// Starts the menu and processes user commands.
        /// </summary>
        public void Menuchoice()
        {
            while (!exit)
            {
                Console.WriteLine("Enter command:");
                string? input = Console.ReadLine()?.ToLower();

                if (input != null)
                {
                    string[] parts = input.Split(' ');
                    string command = parts[0];
                    string[] parameters = parts.Skip(1).ToArray();

                    commandHandler.ExecuteCommand(command, parameters);
                }
                else
                {
                    Console.WriteLine("Invalid input received.");
                }
            }
        }
    }
}
