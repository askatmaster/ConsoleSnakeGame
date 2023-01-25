using System;
using ConsoleSnakeGame.AdditionalDLL;
namespace ConsoleSnakeGame.ClassesOfObjects
{
    class Apple
    {
        //Символ яблока
        private static readonly  char apple = 'o';

        //Для рандомного появления яблока
        private static readonly Random random = new Random();

        //Координаты для яблока
        private static int appleFieldWidth;
        private static int appleFieldHeight;

        //Счет
        public static int score = -1;

        //Метка чтобы яблоко не появилось на теле змеи
        private static bool freeZone = true;

        //Метод для рандомного появления яблока
        public static void AppleSpawn()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Green;

            appleFieldWidth = random.Next(0, Console.WindowWidth);
            appleFieldHeight = random.Next(0, Console.WindowHeight);

            Console.SetCursorPosition(appleFieldWidth, appleFieldHeight);
            CheckSnakePosition(appleFieldWidth, appleFieldHeight);

            if (freeZone)
            {
                Console.Write(apple);
                score++;
            }

            Console.ResetColor();
        }

        //Если позиция будушего яблока окажется на координатах змеи то Мето появления яблока будет вызван по новой!
        private static void CheckSnakePosition(int y, int x )
        {
            var outChar = '*';
            var readBuffer = new char[1];

            CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11),
                                                   readBuffer,
                                                   1,
                                                   new CheckSymbol.COORD()
                                                   {
                                                       X = (short)y,
                                                       Y = (short)x
                                                   },
                                                   out var _);

            if (readBuffer[0] == outChar)
            {
                AppleSpawn();
                freeZone = false;
            }
            else
            {
                freeZone = true;
            }
        }
    }
}