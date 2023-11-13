using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class GameLauncher
    {
        public static void Intro(bool debug)
        {
            if (debug == false)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Морской Бой\n\nЧтобы включить дебаг-режим введите 1.\n\nДля старта нажмите Enter");
                Console.Write("\n\nДебаг-режим:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  ВЫКЛ");
                Console.ForegroundColor = ConsoleColor.White;
            } 
            else if (debug == true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Морской Бой\n\nЧтобы выключить дебаг-режим введите 1.\n\nДля старта нажмите Enter");
                Console.Write("\n\nДебаг-режим:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  ВКЛ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            string n;
            try
            {
                n = Console.ReadLine();
            }
            catch
            {
                n = "0";
            }
            if (n == "1") 
            {
                if (debug == true)
                    debug = false;
                else
                    debug = true;
                Console.Clear();
                Intro(debug);
            }
            else
            {
                Console.Clear();
                Start(debug);
            }
        }
        public static void Start(bool debug)
        {
            char[,] EnField = Battleship.Placer(2);
            char[,] PlField = Battleship.Placer(1);
            DrawBoard(PlField);
            Console.WriteLine("\n(#)=========<|*|>=========(#)\n");
            DrawEnemyBoard(EnField, debug);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nВаш ход! Введите поле (формат \"А5\" или \"Е3\"): ");
            Console.ForegroundColor = ConsoleColor.White;
            string move = Console.ReadLine();
            EnField = User.Check(move, EnField, out bool hit, out bool kill);
            bool hitBot;
            if (hit == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nСоперник ходит. (Нажмите любую клавишу, чтобы продолжить)");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                PlField = Bot.Check(PlField, out hitBot, out bool killBot);
            }
            else
                hitBot = false;
            Game(PlField, EnField, hit, hitBot, kill, debug);
        }
        public static void Game(char[,] PlField, char[,] EnField, bool hit, bool hitBot, bool kill, bool debug)
        {
            Console.Clear();
            DrawBoard(PlField);
            Console.WriteLine("\n(#)=========<|*|>=========(#) \n");
            DrawEnemyBoard(EnField, debug);
            if (hitBot == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (kill == true)
                    Console.WriteLine("\nВы полностью разгромили корабль! Ваш ход. Введите поле: ");
                else
                    Console.WriteLine("\nВаш ход! Введите поле: ");
                Console.ForegroundColor = ConsoleColor.White;
                string move = Console.ReadLine();
                EnField = User.Check(move, EnField, out hit, out kill);
            }
            else
                hit = false;
            if (hit == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nСоперник ходит. (Нажмите любую клавишу, чтобы продолжить)");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                PlField = Bot.Check(PlField, out hitBot, out bool killBot);
            }
            else
                hitBot = false;
            bool flagEn = false;
            for (int i = 0; i != 10; i++)
                for (int j = 0; j != 10; j++)
                {
                    if (EnField[i, j] == '0')
                        flagEn = true;
                }
            bool flagPl = false;
            for (int i = 0; i != 10; i++)
                for (int j = 0; j != 10; j++)
                {
                    if (PlField[i, j] == '0')
                        flagPl = true;
                }
            if ((flagEn == true) && (flagPl == true))
                Game(PlField, EnField, hit, hitBot, kill, debug);
            else if (flagEn == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nПоздравляем, Вы победили! Нажмите любую кнопку для выхода в меню.");
                Console.ForegroundColor = ConsoleColor.White;
                Tools.Clear();
            }
            else if (flagPl == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nВы проиграли! Нажмите любую кнопку для выхода в меню.");
                Console.ForegroundColor = ConsoleColor.White;
                Tools.Clear();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nНа полях больше нет кораблей? Этого не должно было случиться. Никто не победил. Нажмите любую кнопку для выхода в меню.");
                Console.ForegroundColor = ConsoleColor.White;
                Tools.Clear();
            }
        }
        public static char[,] GenField()
        {
            char[,] Field = new char[10, 10];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    Field[i, j] = '~';
            return Field;
        }
        public static int[] GenNumArray()
        {
            int[] Nums = new int[10];
            for (int i = 0; i < Nums.Length; i++)
            {
                Nums[i] = i;
                Console.Write($"{Nums[i]} ");
            }
            return Nums;
        }
        public static char[] GenPseudoNumArray()
        {
            char[] PseudoNums = new char[10];
            int counter = 0;
            string nums = "0123456789";
            foreach (char ch in nums)
            {
                PseudoNums[counter] = ch;
                counter++;
            }
            return PseudoNums;
        }
        public static char[] GenLitArray()
        {
            char[] Lits = new char[10];
            int counter = 0;
            string lits = "АБВГДЕЖЗИК";
            foreach (char ch in lits)
            {
                Lits[counter] = ch;
                counter++;
            }
            return Lits;
        }
        public static void DrawBoard(char [,] Field)
        {
            char[] Lits = GenLitArray();
            Console.Write("     ");
            int[] Nums = GenNumArray();
            Console.WriteLine("\n");
            for (int i = 0; i < Lits.Length; i++)
            {
                Console.Write($"{Lits[i]}    ");
                for (int j = 0; j < Nums.Length; j++)
                {
                    if (Field[i, j] == '0')
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("0 ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (Field[i, j] == '~')
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("~ ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (Field[i, j] == 'х')
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("X ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (Field[i, j] == '#')
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("# ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.Write($"{Field[i, j]} ");
                }
                Console.WriteLine("");
            }
        }
        public static void DrawEnemyBoard(char[,] Field, bool debug)
        {
            char[] Lits = GenLitArray();
            Console.Write("     ");
            int[] Nums = GenNumArray();
            Console.WriteLine("\n");
            for (int i = 0; i < Lits.Length; i++)
            {
                Console.Write($"{Lits[i]}    ");
                for (int j = 0; j < Nums.Length; j++)
                {
                    if (Field[i, j] == '0')
                    {
                        if (debug == false)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("~ ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("0 ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else if (Field[i, j] == '~')
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("~ ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (Field[i, j] == 'х')
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("X ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (Field[i, j] == '#')
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("# ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.Write($"{Field[i, j]} ");
                }
                Console.WriteLine("");
            }
        }
    }
}
