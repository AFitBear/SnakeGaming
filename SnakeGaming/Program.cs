using System.Xml.Linq;

namespace SnakeConsole
{
    public enum Direction : int
    {
        Up, Down, Left, Right, None
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int rows = Console.WindowHeight;
            int cols = Console.WindowWidth;
            // typ: 30 x 120 (rows x columns)
            Board board = new Board(rows, cols);
            Snake snake = new Snake(rows, cols);
            // Render Board og Snake:
            board.Render();
            snake.Render();
            const float TARGET_FPS = 4f; // Frame rate (ønsket)
            const float TARGET_FRAME_TIME = 1f / TARGET_FPS; // Tid per frame
            DateTime previousFrameTime;
            float elapsedTime = 0f;
            bool gameIsRunning = true;
            while (gameIsRunning)
            {
                // Gem nuværende tidspunkt:
                previousFrameTime = DateTime.UtcNow;
                snake.Update();
                snake.Render();
                snake.ReadInput();
                // Beregn faktisk løkketid (cirka):
                elapsedTime = (float)(DateTime.UtcNow - previousFrameTime).TotalSeconds;
                if (elapsedTime < TARGET_FRAME_TIME)
                {
                    // Hvis en frame tid ikke er forløbet endnu, sov lidt:
                    int sleepTime = (int)((TARGET_FRAME_TIME - elapsedTime) * 1000);
                    System.Threading.Thread.Sleep(sleepTime);
                }
                //if (collision = true){break;}
            }
            Console.ReadLine();
        }
    }
}

