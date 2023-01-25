using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleSnakeGame.ClassOfMenu
{
    class Menu
    {
        //Токен для прекращения параллельной задачи
        private static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        private static CancellationToken token = cancelTokenSource.Token;
        public static int CursorOfMenu = 1;

        public void GreetingMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 1);
            Console.WriteLine("Hello my frend!");

            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.SetCursorPosition(Console.WindowWidth / 2 - 13, Console.WindowHeight / 2);
            Console.WriteLine("Press Enter to be continue!");

            Console.ReadKey();

            Console.ResetColor();
            Console.Clear();
        }

        public void MainMenu()
        {
            Console.CursorVisible = false;
            MenuKeyPressAsync();

            while (true)
            {
                switch (CursorOfMenu)
                {
                    case 1:
                        Console.BackgroundColor = ConsoleColor.Green;

                        Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 5);
                        Console.WriteLine("START  GAME");

                        Console.ResetColor();

                        Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 4);
                        Console.WriteLine("EXIT");
                        break;
                    case 2:

                        Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 5);
                        Console.WriteLine("START  GAME");

                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 4);
                        Console.WriteLine("EXIT");

                        Console.ResetColor();
                        break;
                    default:
                        return;
                }
            }
        }

        private static async void MenuKeyPressAsync()
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
                            if (CursorOfMenu > 1)
                                --CursorOfMenu;

                            break;
                        case ConsoleKey.DownArrow:
                            if (CursorOfMenu < 2)
                                ++CursorOfMenu;

                            break;
                        case ConsoleKey.Enter:
                                CursorOfMenu = CursorOfMenu + 10;
                            if (CursorOfMenu != 12)
                            {
                                cancelTokenSource.Cancel();
                                //Если была команда прервать задачу то она прерывается
                                if (token.IsCancellationRequested)
                                {
                                    //Создается новый токен чтобы обнулить старую команду на прерывание задачи
                                    cancelTokenSource = new CancellationTokenSource();
                                    token = cancelTokenSource.Token;

                                    //Задача прерывается
                                    return;
                                }
                            }
                            break;
                    }
                }
            });
        }

        /*public void OptionsMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 7, (Console.WindowHeight / 2) - 6);
            Console.WriteLine("Select speed of the Snake");
            Console.ResetColor();

            while (true)
            {
                switch (CursorOfMenu)
                {
                    case 11:
                        Console.BackgroundColor = ConsoleColor.Green;

                        Console.SetCursorPosition((Console.WindowWidth / 2) - 5, (Console.WindowHeight / 2) - 5);
                        Console.WriteLine("100");

                        Console.ResetColor();

                        Console.SetCursorPosition((Console.WindowWidth / 2) - 5, (Console.WindowHeight / 2) - 4);
                        Console.WriteLine("500");

                        Console.SetCursorPosition((Console.WindowWidth / 2) - 5, (Console.WindowHeight / 2) - 3);
                        Console.WriteLine("1000");
                        break;
                    case 12:

                        Console.SetCursorPosition((Console.WindowWidth / 2) - 5, (Console.WindowHeight / 2) - 5);
                        Console.WriteLine("100");

                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition((Console.WindowWidth / 2) - 5, (Console.WindowHeight / 2) - 4);
                        Console.WriteLine("500");

                        Console.ResetColor();

                        Console.SetCursorPosition((Console.WindowWidth / 2) - 5, (Console.WindowHeight / 2) - 3);
                        Console.WriteLine("1000");
                        break;
                    case 13:

                        Console.SetCursorPosition((Console.WindowWidth / 2) - 5, (Console.WindowHeight / 2) - 5);
                        Console.WriteLine("100");

                        Console.SetCursorPosition((Console.WindowWidth / 2) - 5, (Console.WindowHeight / 2) - 4);
                        Console.WriteLine("500");

                        Console.BackgroundColor = ConsoleColor.Green;

                        Console.SetCursorPosition((Console.WindowWidth / 2) - 5, (Console.WindowHeight / 2) - 3);
                        Console.WriteLine("1000");
                        Console.ResetColor();
                        break;
                    default:
                        return;
                }
            }
        }*/
    }
}
