namespace Threatotron
{
    /// <summary>
    /// Handles the execution of commands for the Threat-o-tron 9000 Obstacle Avoidance System.
    /// </summary>
    public class CommandHandler
    {
        private readonly ObstacleSystem obstacleSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandler"/> class.
        /// </summary>
        /// <param name="obstacleSystem">The obstacle system to use.</param>
        public CommandHandler(ObstacleSystem obstacleSystem)
        {
            this.obstacleSystem = obstacleSystem ?? throw new ArgumentNullException(nameof(obstacleSystem));
        }

        /// <summary>
        /// Executes the specified command with the provided parameters.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="parameters">The parameters for the command.</param>
        public void ExecuteCommand(string command, string[] parameters)
        {
            switch (command)
            {
                case "add":
                    HandleAddCommand(parameters);
                    break;
                case "check":
                    HandleCheckCommand(parameters);
                    break;
                case "map":
                    HandleMapCommand(parameters);
                    break;
                case "path":
                    HandlePathCommand(parameters);
                    break;
                case "help":
                    Console.WriteLine(Menu.message);
                    break;
                case "exit":
                    Console.WriteLine("Thank you for using the Threat-o-tron 9000.");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine($"Invalid option: {command}");
                    Console.WriteLine("Type 'help' to see a list of commands.");
                    break;
            }
        }

        private void HandleAddCommand(string[] parameters)
        {
            if (parameters.Length < 3)
            {
                Console.WriteLine("Insufficient arguments. Please provide both X and Y coordinates.");
                return;
            }

            if (!int.TryParse(parameters[1], out int obstacleX) || !int.TryParse(parameters[2], out int obstacleY))
            {
                Console.WriteLine("Coordinates are not valid integers.");
                return;
            }

            switch (parameters[0])
            {
                case "guard":
                    if (parameters.Length != 3)
                    {
                        Console.WriteLine("Incorrect number of arguments.");
                        return;
                    }
                    Guard.addGuard(obstacleX, obstacleY, ++Menu.guardCount, obstacleSystem);
                    break;
                case "fence":
                    if (parameters.Length != 5)
                    {
                        Console.WriteLine("Incorrect number of arguments.");
                        return;
                    }

                    if (!Menu.validFenceOrientation.Contains(parameters[3]))
                    {
                        Console.WriteLine("Orientation must be 'east' or 'north'.");
                        return;
                    }

                    if (!int.TryParse(parameters[4], out int length) || length <= 0)
                    {
                        Console.WriteLine("Length must be a valid integer greater than 0.");
                        return;
                    }

                    Fence.addFence(obstacleX, obstacleY, length, parameters[3], ++Menu.fenceCount, obstacleSystem);
                    break;
                case "sensor":
                    if (parameters.Length != 4)
                    {
                        Console.WriteLine("Incorrect number of arguments.");
                        return;
                    }

                    if (!double.TryParse(parameters[3], out double range) || range <= 0)
                    {
                        Console.WriteLine("Invalid sensor range. Range must be a positive number.");
                        return;
                    }

                    Sensor.addSensor(obstacleX, obstacleY, range, ++Menu.sensorCount, obstacleSystem);
                    break;
                case "camera":
                    if (parameters.Length != 4)
                    {
                        Console.WriteLine("Incorrect number of arguments.");
                        return;
                    }

                    if (!Menu.validCameraOrientation.Contains(parameters[3]))
                    {
                        Console.WriteLine("Direction must be 'north', 'south', 'east' or 'west'.");
                        return;
                    }

                    Camera.addCamera(obstacleX, obstacleY, parameters[3], ++Menu.cameraCount, obstacleSystem);
                    break;
                default:
                    Console.WriteLine("Unknown obstacle type. Valid types are: guard, fence, sensor, camera.");
                    break;
            }
        }

        private void HandleCheckCommand(string[] parameters)
        {
            if (parameters.Length != 2)
            {
                Console.WriteLine("Incorrect number of arguments.");
                return;
            }

            if (!int.TryParse(parameters[0], out int x) || !int.TryParse(parameters[1], out int y))
            {
                Console.WriteLine("Coordinates are not valid integers.");
                return;
            }

            CheckLocation checker = new CheckLocation(x, y, obstacleSystem);
            bool isSafe = checker.IsLocationSafe();
            List<string> safeDirections = checker.GetSafeDirections();

            if (safeDirections.Count > 0)
            {
                Console.WriteLine("You can safely take any of the following directions:");
                foreach (string direction in safeDirections)
                {
                    Console.WriteLine($"{direction}");
                }
            }
            else
            {
                Console.WriteLine("You cannot safely move in any direction. Abort mission.");
            }
        }

        private void HandleMapCommand(string[] parameters)
        {
            if (parameters.Length != 4)
            {
                Console.WriteLine("Incorrect number of arguments.");
                return;
            }

            if (!int.TryParse(parameters[0], out int x) || !int.TryParse(parameters[1], out int y) ||
                !int.TryParse(parameters[2], out int width) || !int.TryParse(parameters[3], out int height))
            {
                Console.WriteLine("Coordinates are not valid integers.");
                return;
            }

            if (width < 1 || height < 1)
            {
                Console.WriteLine("Width and height must be valid positive integers.");
                return;
            }

            DrawMap map = new DrawMap(x, y, width, height, obstacleSystem);
            map.Draw();
        }

        private void HandlePathCommand(string[] parameters)
        {
            if (parameters.Length != 4)
            {
                Console.WriteLine("Incorrect number of arguments.");
                return;
            }

            if (!int.TryParse(parameters[0], out int agentX) || !int.TryParse(parameters[1], out int agentY))
            {
                Console.WriteLine("Agent coordinates are not valid integers.");
                return;
            }

            if (!int.TryParse(parameters[2], out int objectiveX) || !int.TryParse(parameters[3], out int objectiveY))
            {
                Console.WriteLine("Objective coordinates are not valid integers.");
                return;
            }

            if (agentX == objectiveX && agentY == objectiveY)
            {
                Console.WriteLine("Agent, you are already at the objective.");
                return;
            }

            if (obstacleSystem.ContainsObstacleAt(objectiveX, objectiveY))
            {
                Console.WriteLine("The objective is blocked by an obstacle and cannot be reached.");
                return;
            }

            FindPath findPath = new FindPath(obstacleSystem);
            findPath.HandlePathCommand(agentX, agentY, objectiveX, objectiveY);
        }
    }
}
