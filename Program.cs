namespace Threatotron
{
    /// <summary>
    /// The main entry point for the Threat-o-tron 9000 Obstacle Avoidance System.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            ObstacleSystem obstacleSystem = new ObstacleSystem();
            CommandHandler commandHandler = new CommandHandler(obstacleSystem);
            Menu menu = new Menu(commandHandler);

            Console.WriteLine(Menu.message);
            menu.Menuchoice();
        }
    }
}
