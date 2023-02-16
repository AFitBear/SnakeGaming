namespace SnakeConsole
{
    internal class Position
    {
        public int row;
        public int col;
        public Position(int r, int c)
        {
            row = r;
            col = c;
        }
    }
    internal class Board
    {
        int rows;
        int cols;
        public Board(int r, int c)
        {
            rows = r;
            cols = c;
            Console.CursorVisible = false;
        }
        public void Render()
        {
            /*for (int row = 0; row < rows - 1; row++)
            {
                for (int col = 0; col < cols - 1; col++)
                {
                    if (row == 0 || col == 0 || row == rows - 2 || col == cols - 2)
                    {
                        Console.SetCursorPosition(col, row);
                        Console.Write('-');

                    }
                }
            }*/
        } //useless
    }
    internal class Snake
    {
        private Queue<Position> body;
        private Dictionary<Position, bool> occupiedPositions;
        Position clearPosition = null;
        public int rows, cols;
        Direction direction = Direction.None;
        private Random random;
        public bool collision;

        public Snake(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            random = new Random();
            body = new Queue<Position>();
            occupiedPositions = new Dictionary<Position, bool>();
            //Laver en border
            for (int row = 0; row < rows - 1; row++)
            {
                for (int col = 0; col < cols - 1; col++)
                {
                    if (row == 0 || col == 0 || row == rows - 2 || col == cols - 2)
                    {
                        Console.SetCursorPosition(col, row);
                        Console.Write('-');
                        Position vegg = new Position(col, row);
                        occupiedPositions.Add(vegg, true);          //Makes the wall position an occupied position
                    }
                }
            }
            int trow, tcol;
            trow = rows / 2 + random.Next(-3, 3);
            tcol = cols / 2 + random.Next(-7, 7);
            Position newHead = new Position(trow, tcol);
            body.Enqueue(newHead);
            occupiedPositions.Add(newHead, true);
            body.Enqueue(newHead);
            body.Enqueue(newHead);
            body.Enqueue(newHead);
            direction = Direction.None; // Don't move until .. .
            collision = false;
        }
        public void Update()
        {
            Position head = body.Last();
            Position tail = null;
            Position newHead;
            // Debugging:
            bool testNu = true;
            if (testNu)
            {
                Console.SetCursorPosition(cols / 2, rows / 2);
                Console.Write(" ");
                Console.SetCursorPosition(cols / 2, rows / 2);
                Console.Write("r = " + head.row + ", c = " + head.col);
                Console.SetCursorPosition(cols / 2, rows / 2 + 1);
                Console.Write(" ");
                Console.SetCursorPosition(cols / 2, rows / 2 + 1);
                Console.Write("collsion = " + collision);
            }
            // Hvilken retning?
            switch (direction)
            {
                case Direction.Up:                                  //Up
                                                                    // If at top row, collision with wall:
                    /* if (IsCollision(head))
                     {
                         Console.WriteLine("Hello");
                         collision = true;
                         return;
                     }*/
                    if (head.row < 2)
                    {
                        collision = true;
                        return;
                    }
                    // Heads new position is added to body:
                    newHead = new Position(
                    head.row - 1,
                    head.col
                    );
                    body.Enqueue(newHead);
                    occupiedPositions.Add(newHead, true);
                    // Remove tail:
                    tail = body.Dequeue();
                    clearPosition = tail;
                    occupiedPositions.Remove(tail);
                    break;
                case Direction.Down:                                //Ned
                    // If at top row, collision with wall:
                    if (head.row > rows - 4)
                    {
                        collision = true;
                        return;
                    }
                    // Heads new position is added to body:
                    newHead = new Position(
                    head.row + 1,
                    head.col
                    );
                    body.Enqueue(newHead);
                    occupiedPositions.Add(newHead, true);
                    // Remove tail:
                    tail = body.Dequeue();
                    clearPosition = tail;
                    occupiedPositions.Remove(tail);
                    break;
                case Direction.Left:                            //Venstre
                    // If at top row, collision with wall:
                    if (head.col < 2)
                    {
                        collision = true;
                        return;
                    }
                    // Heads new position is added to body:
                    newHead = new Position(
                    head.row,
                    head.col - 1
                    );
                    body.Enqueue(newHead);
                    occupiedPositions.Add(newHead, true);
                    // Remove tail:
                    tail = body.Dequeue();
                    clearPosition = tail;
                    occupiedPositions.Remove(tail);
                    break;
                case Direction.Right:                           //højre
                    // If at right row, collision with wall:
                    if (head.col > cols - 4)
                    {
                        collision = true;
                        return;
                    }
                    // Heads new position is added to body:
                    newHead = new Position(
                    head.row,
                    head.col + 1
                    );
                    body.Enqueue(newHead);
                    occupiedPositions.Add(newHead, true);
                    /*if (IsCollision(Position position))
                    {
                        collision= true;
                    }*/
                    // Remove tail:
                    tail = body.Dequeue();
                    clearPosition = tail;
                    occupiedPositions.Remove(tail);
                    break;
                default:
                    break;
            }
        }
        public void Render()
        {
            foreach (var item in body)
            {
                Console.SetCursorPosition(item.col, item.row);
                Console.Write('*');
            }
            // Overwrite old position:
            if (clearPosition != null)
            {
                Console.SetCursorPosition(clearPosition.col, clearPosition.row);
                Console.Write(' ');
            }
        }
        public void ReadInput()
        {
            if (!Console.KeyAvailable)
                return;
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    direction = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    direction = Direction.Right;
                    break;
                case ConsoleKey.UpArrow:
                    direction = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    direction = Direction.Down;
                    break;
            }
        }
        public bool IsCollision(Position posiss)
        {
            // Undersøger om position allerede er optaget:
            return occupiedPositions.ContainsKey(posiss); // lærens kode, gider ikke virke. jeg laver min egen

            /*if (occupiedPositions.ContainsKey(newHead))
            {
                return true;
            }
            return false;*/
        }
    }
}