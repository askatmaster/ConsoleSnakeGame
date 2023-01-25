using ConsoleSnakeGame.ClassesOfObjects;
using ConsoleSnakeGame.ClassOfMenu;
using System;
using System.Threading.Tasks;
using ConsoleSnakeGame.Enums;

namespace ConsoleSnakeGame
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.CursorVisible = false;
            var menu = new Menu();
            menu.GreetingMenu();
            menu.MainMenu();

            switch (Menu.CursorOfMenu)
            {
                case 11:
                    Console.Clear();
                    StartGame();

                    break;
                case 12:
                    Environment.Exit(0);

                    break;
            }
        }

        //Метод начинает игру
        private static void StartGame()
        {
            AppleSpawn();

            try
            {
                Parallel.Invoke(SnakeGo, KeyPressAsync);
            }
            catch (AggregateException)
            {
                //Если змея достигнет границ консоли
                GameOver();
            }
        }

        //Метод запуска появления яблок
        private static void AppleSpawn()
        {
            Apple.AppleSpawn();
            Console.SetCursorPosition(0, 0);
        }

        //Метод запуска змеи
        private static void SnakeGo()
        {
            var snake = new Snake();

            Console.ForegroundColor = ConsoleColor.Red;

            for(var i = 0;
                i < snake.snake.Count;
                i++)
                Console.Write("*");

            Console.ResetColor();

            TimerOfGame.Timer();
            snake.Head();
        }

        //Асинхронный метод для управления змеей
        private static async void KeyPressAsync()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    var key = Console.ReadKey(true);
                    var x = key.Key;

                    switch (x)
                    {
                        case ConsoleKey.UpArrow:
                            Snake.HeadWaySnake = HeadWay.Up;

                            break;
                        case ConsoleKey.DownArrow:
                            Snake.HeadWaySnake = HeadWay.Down;

                            break;
                        case ConsoleKey.RightArrow:
                            Snake.HeadWaySnake = HeadWay.Right;

                            break;
                        case ConsoleKey.LeftArrow:
                            Snake.HeadWaySnake = HeadWay.Left;

                            break;
                        case ConsoleKey.Escape:
                            GameOver();

                            break;
                        case ConsoleKey.Enter:
                            return;
                    }
                }
            });
        }

        //Метод в случае проигрыша
        public static void GameOver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 1);
            Console.WriteLine("GAME  OVER!");

            Console.SetCursorPosition(Console.WindowWidth / 2 - 6, Console.WindowHeight / 2);
            Console.WriteLine("YOU DIED!");

            Console.SetCursorPosition(Console.WindowWidth / 2 - 6, Console.WindowHeight / 2 + 2);
            Console.WriteLine($"Score: {Apple.score}");

            Console.SetCursorPosition(Console.WindowWidth / 2 - 6, Console.WindowHeight / 2 + 3);
            Console.WriteLine($"Time: {TimerOfGame.sec} sec.");

            Console.ResetColor();
            Console.ReadKey();

            Environment.Exit(0);
        }
    }
}