using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleSnakeGame.AdditionalDLL;
using ConsoleSnakeGame.Enums;
namespace ConsoleSnakeGame.ClassesOfObjects
{

    class Snake
    {
        /// <summary>
        /// Создание свойств и полей для змеи
        /// </summary>

        //Направление пути головы змеи(куда ползет змея)
        public static HeadWay HeadWaySnake { get; set; } = HeadWay.Right;

        //Направление пути хвоста змеи(чтобы хвост следовал по следам змеи при движении зигзагом)
        public static TailWay TailWaySnake { get; set; } = TailWay.Right;

        //Запоминает предыдущее направление змеи(для того чтобы змея двигаясь направо, не могла сразу начать двигаться налево)
        public static PreviousWay PreviousWaySnake { get; set; } = PreviousWay.Right;

        //Из каких символов состоит змея
        public static char head = '*';

        //Изначальный размер змеи
        public List<char> snake = new List<char> { '*', '*', '*','*','*' };

        //Расположение курсора(Хвост змеи)
        private static int PosYTail = 0;
        private static int PosXTail = 0;

        //Расположение курсора(Голова змеи)
        private static int PosYHead = Console.CursorLeft;
        private static int PosXHead = Console.CursorTop;

        //Скорость змеи
        private static readonly int delay = 50;

        //Метод движение головы змеи
        public void Head()
        {
            while (true)
            {
                switch (HeadWaySnake)
                {
                    case HeadWay.Right:

                        if (PreviousWaySnake != PreviousWay.Left)
                        {
                            while (HeadWaySnake == HeadWay.Right)
                            {

                                Console.SetCursorPosition(PosYHead++, PosXHead);

                                BodyBite(PosYHead, PosXHead);

                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(head);
                                Console.ResetColor();

                                Eating();
                                Tail();
                                Thread.Sleep(delay);
                            }
                            PreviousWaySnake = PreviousWay.Right;
                        }
                        else
                        {
                            HeadWaySnake = HeadWay.Left;
                        }

                        break;
                    case HeadWay.Down:

                        if (PreviousWaySnake != PreviousWay.Up)
                        {
                            while (HeadWaySnake == HeadWay.Down)
                            {

                                Console.SetCursorPosition(PosYHead, PosXHead++);

                                BodyBite(PosYHead, PosXHead);

                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(head);
                                Console.ResetColor();

                                Eating();
                                Tail();
                                Thread.Sleep(delay);
                                if (PosXHead==Console.WindowHeight)
                                    Program.GameOver();
                            }
                            PreviousWaySnake = PreviousWay.Down;
                        }
                        else
                        {
                            HeadWaySnake = HeadWay.Up;
                        }

                        break;
                    case HeadWay.Up:

                        if (PreviousWaySnake != PreviousWay.Down)
                        {
                            while (HeadWaySnake == HeadWay.Up)
                            {
                                Console.SetCursorPosition(PosYHead, PosXHead--);

                                BodyBite(PosYHead, PosXHead);

                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(head);
                                Console.ResetColor();

                                Eating();
                                Tail();
                                Thread.Sleep(delay);
                            }
                            PreviousWaySnake = PreviousWay.Up;
                        }
                        else
                        {
                            HeadWaySnake = HeadWay.Down;
                        }

                        break;
                    case HeadWay.Left:

                        if (PreviousWaySnake != PreviousWay.Right)
                        {
                            while (HeadWaySnake == HeadWay.Left)
                            {
                                Console.SetCursorPosition(PosYHead--, PosXHead);

                                BodyBite(PosYHead, PosXHead);

                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(head);
                                Console.ResetColor();

                                Eating();
                                Tail();
                                Thread.Sleep(delay);
                            }
                            PreviousWaySnake = PreviousWay.Left;
                        }
                        else
                        {
                            HeadWaySnake = HeadWay.Right;
                        }

                        break;
                }
            }
        }

        //Метод если змея укусит себя
        public void BodyBite(int y, int  x)
        {
            var outChar = '*';
            var readBuffer = new char[1];
            int readCount;

            CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11), readBuffer, 1, new CheckSymbol.COORD() { X = (short)y, Y = (short)x }, out readCount);
            if (readBuffer[0] == outChar)
                Program.GameOver();
        }

        //Метод в случае поедания яблока, змея увеличивается
        public void Eating()
        {
            var outChar = 'o';
            var readBuffer = new char[1];
            int readCount;

            switch (HeadWaySnake)
            {
                case HeadWay.Right:

                    Console.SetCursorPosition(PosYHead, PosXHead);

                    CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11), readBuffer, 1, new CheckSymbol.COORD() { X = (short)PosYHead, Y = (short)PosXHead }, out readCount);
                    if (readBuffer[0] == outChar)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(head);

                        Console.ResetColor();

                        PosYHead++;
                        Apple.AppleSpawn();
                    }

                    break;
                case HeadWay.Down:

                    Console.SetCursorPosition(PosYHead, PosXHead);

                    CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11), readBuffer, 1, new CheckSymbol.COORD() { X = (short)PosYHead, Y = (short)PosXHead }, out readCount);
                    if (readBuffer[0] == outChar)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(head);

                        Console.ResetColor();

                        PosXHead++;
                        Apple.AppleSpawn();
                    }

                    break;
                case HeadWay.Up:

                    Console.SetCursorPosition(PosYHead, PosXHead);

                    CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11), readBuffer, 1, new CheckSymbol.COORD() { X = (short)PosYHead, Y = (short)PosXHead }, out readCount);
                    if (readBuffer[0] == outChar)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(head);

                        Console.ResetColor();

                        PosXHead--;
                        Apple.AppleSpawn();
                    }

                    break;
                case HeadWay.Left:

                    Console.SetCursorPosition(PosYHead, PosXHead);

                    CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11), readBuffer, 1, new CheckSymbol.COORD() { X = (short)(PosYHead - 1), Y = (short)PosXHead }, out readCount);
                    if (readBuffer[0] == outChar)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(head);

                        Console.ResetColor();

                        PosYHead--;
                        Apple.AppleSpawn();
                    }

                    break;
            }
        }

        //Метод движения хвоста змеи
        public void Tail()
        {
            var outChar = head;
            var readBuffer = new char[1];
            int readCount;

            switch (TailWaySnake)
            {
                case TailWay.Right:

                    Console.SetCursorPosition(PosYTail++, PosXTail);

                    CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11), readBuffer, 1, new CheckSymbol.COORD() { X = (short)PosYTail, Y = (short)PosXTail }, out readCount);
                    if (readBuffer[0] == outChar)
                        Console.Write(" ");
                    else
                        Direction(PosYTail, PosXTail);

                    break;
                case TailWay.Down:

                    Console.SetCursorPosition(PosYTail-1, ++PosXTail);

                    CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11), readBuffer, 1, new CheckSymbol.COORD() { X = (short)(PosYTail - 1), Y = (short)(PosXTail + 1) }, out readCount);
                    if (readBuffer[0] == outChar)
                        Console.Write(" ");
                    else
                        Direction(PosYTail, PosXTail);

                    break;
                case TailWay.Up:

                    Console.SetCursorPosition(PosYTail-1, --PosXTail);

                    CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11), readBuffer, 1, new CheckSymbol.COORD() { X = (short)(PosYTail - 1), Y = (short)(PosXTail - 1) }, out readCount);
                    if (readBuffer[0] == outChar)
                        Console.Write(" ");
                    else
                        Direction(PosYTail, PosXTail);

                    break;
                case TailWay.Left:

                    Console.SetCursorPosition(--PosYTail-1, PosXTail);

                    CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11), readBuffer, 1, new CheckSymbol.COORD() { X = (short)(PosYTail - 2), Y = (short)PosXTail }, out readCount);
                    if (readBuffer[0] == outChar)
                        Console.Write(" ");
                    else
                        Direction(PosYTail, PosXTail);

                    break;
            }
        }

        //Метод определяет в какую сторону дальше  движется хвост змеи при повороте
        public void Direction(int y, int x)
        {
            var outChar = '*';
            var readBuffer = new char[1];
            int readCount;

            CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11), readBuffer, 1, new CheckSymbol.COORD() { X = (short)y, Y = (short)x }, out readCount);
            if (readBuffer[0] == outChar)
            {
                Console.Write(" ");
                TailWaySnake = TailWay.Right;
            }

            CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11), readBuffer, 1, new CheckSymbol.COORD() { X = (short)(y - 1), Y = (short)(x + 1) }, out readCount);
            if (readBuffer[0] == outChar)
            {
                Console.Write(" ");
                TailWaySnake = TailWay.Down;
            }

            CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11), readBuffer, 1, new CheckSymbol.COORD() { X = (short)(y - 1), Y = (short)(x - 1) }, out readCount);
            if (readBuffer[0] == outChar)
            {
                Console.Write(" ");
                TailWaySnake = TailWay.Up;
            }

            CheckSymbol.ReadConsoleOutputCharacter(CheckSymbol.GetStdHandle(-11), readBuffer, 1, new CheckSymbol.COORD() { X = (short)(y - 2), Y = (short)x }, out readCount);
            if (readBuffer[0] == outChar)
            {
                Console.Write(" ");
                TailWaySnake = TailWay.Left;
            }
        }
    }
}
