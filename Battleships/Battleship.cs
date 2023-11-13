using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battleships
{
    class Battleship
    {
        public static char[,] Placer(int pl)
        {
            char[,] Field = GameLauncher.GenField();
            Random rnd;
            if (pl == 1)
                rnd = new Random();
            else
                rnd = new Random(1000);
            //генерация координат "носа" корабля-четвёрки
            int rndX4 = rnd.Next(0, 9);
            int rndY4 = rnd.Next(0, 9);
            Field = Fourths(Field, rndX4, rndY4, rnd);
            Field = Thirds(2, Field, rndX4, rndY4);
            Field = Seconds(3, Field);
            Field = Firsts(4, Field);
            return Field;
        }
        public static char[,] Fourths(char[,] Field, int rndX, int rndY, Random rnd)
        {
            string state = "";
            Field = Turner(rndX, rndY, 4, Field, state, out bool success);
            return Field;
        }
        public static char[,] FourthsBuild(int rndX, int rndY, int rndA, char[,] Field)
        {
            if (rndA == 0)
            {
                Field[rndX, rndY] = '0';
                Field[rndX, rndY - 1] = '0';
                Field[rndX, rndY - 2] = '0';
                Field[rndX, rndY - 3] = '0';
            }
            else if (rndA == 1)
            {
                Field[rndX, rndY] = '0';
                Field[rndX + 1, rndY] = '0';
                Field[rndX + 2, rndY] = '0';
                Field[rndX + 3, rndY] = '0';
            }
            else if (rndA == 2)
            {
                Field[rndX, rndY] = '0';
                Field[rndX, rndY + 1] = '0';
                Field[rndX, rndY + 2] = '0';
                Field[rndX, rndY + 3] = '0';
            }
            else if (rndA == 3)
            {
                Field[rndX, rndY] = '0';
                Field[rndX - 1, rndY] = '0';
                Field[rndX - 2, rndY] = '0';
                Field[rndX - 3, rndY] = '0';
            }
            return Field;
        }
        public static char[,] Thirds(int amount, char[,] Field, int rndX4, int rndY4)
        {
            Random rnd = new Random();
            int rndX, rndY;
            string state;
            for (int i = 0; i != amount; i++)
            {
                rndX = 0;
                rndY = 0;
                if ((rndX4 < 5) & (rndY4 < 5))
                {
                    rndX = 0;
                    rndY = 0;
                    for (; (rndX < 5) || (rndY < 5);)
                    {
                        rndY = rnd.Next(0, 9);
                        rndX = rnd.Next(0, 9);
                    }
                }
                else if ((rndX4 < 5) & (rndY4 >= 5))
                {
                    rndX = 0;
                    rndY = 9;
                    for (; (rndX < 5) || (rndY >= 5);)
                    {
                        rndY = rnd.Next(0, 9);
                        rndX = rnd.Next(0, 9);
                    }
                }
                else if ((rndX4 >= 5) & (rndY4 < 5))
                {
                    rndX = 9;
                    rndY = 0;
                    for (; (rndX >= 5) || (rndY < 5);)
                    {
                        rndY = rnd.Next(0, 9);
                        rndX = rnd.Next(0, 9);
                    }
                }
                else if ((rndX4 >= 5) & (rndY4 >= 5))
                {
                    rndX = 9;
                    rndY = 9;
                    for (; (rndX >= 5) || (rndY >= 5);)
                    {
                        rndY = rnd.Next(0, 9);
                        rndX = rnd.Next(0, 9);
                    }
                }
                rndY = rnd.Next(0, 9);
                rndX = rnd.Next(0, 9);
                for (; Field[rndX, rndY] == '0';)
                {
                    rndY = rnd.Next(0, 9);
                    rndX = rnd.Next(0, 9);
                }
                //проверка положения носа для правильной проверки коллизии
                state = Stater(rndX, rndY, 3);
                //проверка положения носа для поворота и генерации корабля
                Field = Turner(rndX, rndY, 3, Field, state, out bool success);
                if (success == false)
                    i--;
            }
            return Field;
        }
        public static char[,] ThirdsBuild(int rndX, int rndY, int rndA, char[,] Field, string state, out bool success)
        {
            success = false;
            //проверка коллизии
            switch (state)
            {
                case "Norm":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') || (Field[rndX + 1, rndY - 3] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') || (Field[rndX, rndY - 3] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') || (Field[rndX - 1, rndY - 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        {
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') || (Field[rndX + 3, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') || (Field[rndX + 3, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') || (Field[rndX + 3, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') || (Field[rndX + 1, rndY + 3] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') || (Field[rndX, rndY + 3] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') || (Field[rndX - 1, rndY + 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        {
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') || (Field[rndX - 3, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') || (Field[rndX - 3, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') || (Field[rndX - 3, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "UpLeft":
                    if (rndA == 1)
                    {
                        if ((Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') || (Field[rndX + 3, rndY] == '0') ||
                            (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') || (Field[rndX + 3, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') || (Field[rndX + 1, rndY + 3] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') || (Field[rndX, rndY + 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "Up":
                    if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') || (Field[rndX + 3, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') || (Field[rndX + 3, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') || (Field[rndX + 1, rndY + 3] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') || (Field[rndX, rndY + 3] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') || (Field[rndX - 1, rndY + 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') || (Field[rndX - 3, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') || (Field[rndX - 3, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0'))
                            break;
                        else 
                        { 
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "UpRight":
                    if (rndA == 2)
                    {
                        if ((Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') || (Field[rndX, rndY + 3] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') || (Field[rndX - 1, rndY + 3] == '0') ||
                            (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') || (Field[rndX - 3, rndY] == '0') ||
                           (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') || (Field[rndX - 3, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "Right":
                    if (rndA == 0)
                    {
                        if ((Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') || (Field[rndX, rndY - 3] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') || (Field[rndX - 1, rndY - 3] == '0') ||
                            (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') || (Field[rndX, rndY + 3] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') || (Field[rndX - 1, rndY + 3] == '0') ||
                            (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') || (Field[rndX - 3, rndY - 1] == '0') ||
                           (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') || (Field[rndX - 3, rndY] == '0') ||
                           (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') || (Field[rndX - 3, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "DownRight":
                    if (rndA == 0)
                    {
                        if ((Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') || (Field[rndX, rndY - 3] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') || (Field[rndX - 1, rndY - 3] == '0') ||
                            (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') || (Field[rndX - 3, rndY - 1] == '0') ||
                           (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') || (Field[rndX - 3, rndY] == '0') ||
                           (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "Down":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') || (Field[rndX + 1, rndY - 3] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') || (Field[rndX, rndY - 3] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') || (Field[rndX - 1, rndY - 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') || (Field[rndX + 3, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') || (Field[rndX + 3, rndY] == '0') ||
                            (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') || (Field[rndX - 3, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') || (Field[rndX - 3, rndY] == '0') ||
                           (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "DownLeft":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') || (Field[rndX + 1, rndY - 3] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') || (Field[rndX, rndY - 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') || (Field[rndX + 3, rndY - 1] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') || (Field[rndX + 3, rndY] == '0') ||
                            (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "Left":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') || (Field[rndX + 1, rndY - 3] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') || (Field[rndX, rndY - 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') || (Field[rndX + 3, rndY - 1] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') || (Field[rndX + 3, rndY] == '0') ||
                            (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') || (Field[rndX + 3, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') || (Field[rndX + 1, rndY + 3] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') || (Field[rndX, rndY + 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerUpLeft":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') || (Field[rndX + 3, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') || (Field[rndX + 3, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') || (Field[rndX + 3, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        {
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') || (Field[rndX + 1, rndY + 3] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') || (Field[rndX, rndY + 3] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') || (Field[rndX - 1, rndY + 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        {
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerUp":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        {
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') || (Field[rndX + 3, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') || (Field[rndX + 3, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') || (Field[rndX + 3, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') || (Field[rndX + 1, rndY + 3] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') || (Field[rndX, rndY + 3] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') || (Field[rndX - 1, rndY + 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') || (Field[rndX - 3, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') || (Field[rndX - 3, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') || (Field[rndX - 3, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerUpRight":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') || (Field[rndX + 1, rndY + 3] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') || (Field[rndX, rndY + 3] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') || (Field[rndX - 1, rndY + 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') || (Field[rndX - 3, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') || (Field[rndX - 3, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') || (Field[rndX - 3, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerRight":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') || (Field[rndX + 1, rndY - 3] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') || (Field[rndX, rndY - 3] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') || (Field[rndX - 1, rndY - 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') || (Field[rndX + 1, rndY + 3] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') || (Field[rndX, rndY + 3] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') || (Field[rndX - 1, rndY + 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') || (Field[rndX - 3, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') || (Field[rndX - 3, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') || (Field[rndX - 3, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerDownRight":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') || (Field[rndX + 1, rndY - 3] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') || (Field[rndX, rndY - 3] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') || (Field[rndX - 1, rndY - 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') || (Field[rndX - 3, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') || (Field[rndX - 3, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') || (Field[rndX - 3, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerDown":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') || (Field[rndX + 1, rndY - 3] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') || (Field[rndX, rndY - 3] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') || (Field[rndX - 1, rndY - 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        {
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') || (Field[rndX + 3, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') || (Field[rndX + 3, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') || (Field[rndX + 3, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') || (Field[rndX - 3, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') || (Field[rndX - 3, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') || (Field[rndX - 3, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerDownLeft":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') || (Field[rndX + 1, rndY - 3] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') || (Field[rndX, rndY - 3] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') || (Field[rndX - 1, rndY - 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') || (Field[rndX + 3, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') || (Field[rndX + 3, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') || (Field[rndX + 3, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerLeft":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') || (Field[rndX + 1, rndY - 3] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') || (Field[rndX, rndY - 3] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') || (Field[rndX - 1, rndY - 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') || (Field[rndX + 3, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') || (Field[rndX + 3, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') || (Field[rndX + 3, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') || (Field[rndX + 1, rndY + 3] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') || (Field[rndX, rndY + 3] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') || (Field[rndX - 1, rndY + 3] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw3s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
            }
            return Field;
        }
        public static char[,] Seconds(int amount, char[,] Field)
        {
            Random rnd = new Random();
            int rndX, rndY;
            string state;
            for (int i = 0; i != amount; i++)
            {
                bool flag = false;
                for (int k = 0; k != 10; k++)
                    for (int j = 0; j != 10; j++)
                    {
                        if (Field[k, j] == '0')
                            flag = true;
                    }
                if (flag == false)
                    break;
                rndY = rnd.Next(0, 9);
                rndX = rnd.Next(0, 9);
                for (; Field[rndX, rndY] == '0';)
                {
                    rndY = rnd.Next(0, 9);
                    rndX = rnd.Next(0, 9);
                }
                state = Stater(rndX, rndY, 2);
                Field = Turner(rndX, rndY, 2, Field, state, out bool success);
                if (success == false)
                    i--;
            }
            return Field;
        }
        public static char[,] SecondsBuild(int rndX, int rndY, int rndA, char[,] Field, string state, out bool success)
        {
            success = false;
            switch (state)
            {
                case "Norm":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        {
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else 
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else 
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "UpLeft":
                    if (rndA == 1)
                    {
                        if ((Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "Up":
                    if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "UpRight":
                    if (rndA == 2)
                    {
                        if ((Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "Right":
                    if (rndA == 0)
                    {
                        if ((Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') ||
                           (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "DownRight":
                    if (rndA == 0)
                    {
                        if ((Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') ||
                           (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "Down":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "DownLeft":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "Left":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerUpLeft":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerUp":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerUpRight":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') || 
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerRight":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                            break;
                        else 
                        { 
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0')) break;
                        else
                        { 
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0')) break;
                        else
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerDownRight":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0')) break;
                        else
                        { 
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0')) break;
                        else
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0')) break;
                        else
                        { 
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') || 
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0')) break;
                        else
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerDown":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') || 
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0')) break;
                        else
                        { 
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0')) break;
                        else
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0')) break;
                        else
                        {
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 2, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') || (Field[rndX - 2, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 2, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0')) break;
                        else
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerDownLeft":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0')) break;
                        else
                        { 
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0')) break;
                        else
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0')) break;
                        else
                        { 
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0')) break;
                        else
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
                case "InnerLeft":
                    if (rndA == 0)
                    {
                        if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 2] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY - 2] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0')) break;
                        else
                        { 
                            Field = Draw2s0Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 1)
                    {
                        if ((Field[rndX - 1, rndY - 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 2, rndY - 1] == '0') ||
                            (Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') || (Field[rndX + 2, rndY] == '0') ||
                            (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 2, rndY + 1] == '0') ||
                            (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0')) break;
                        else
                        { 
                            Field = Draw2s90Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 2)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 2] == '0') ||
                            (Field[rndX, rndY - 1] == '0') || (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY + 2] == '0') ||
                            (Field[rndX - 1, rndY - 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 2] == '0') ||
                            (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0')) break;
                        else
                        { 
                            Field = Draw2s180Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    else if (rndA == 3)
                    {
                        if ((Field[rndX + 1, rndY - 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') ||
                           (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0') ||
                           (Field[rndX + 1, rndY + 1] == '0') || (Field[rndX - 1, rndY + 1] == '0') ||
                           (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0'))
                            break;
                        else
                        { 
                            Field = Draw2s270Deg(rndX, rndY, Field);
                            success = true;
                        }
                    }
                    break;
            }
            return Field;
        }
        public static char[,] Firsts(int amount, char[,] Field)
        {
            Random rnd = new Random();
            int rndX, rndY;
            string state;
            for (int i = 0; i != amount; i++)
            {
                bool flag = false;
                for (int k = 0; k != 10; k++)
                    for (int j = 0; j != 10; j++)
                    {
                        if (Field[k, j] == '0')
                            flag = true;
                    }
                if (flag == false)
                    break;
                rndY = rnd.Next(0, 9);
                rndX = rnd.Next(0, 9);
                for (; Field[rndX, rndY] == '0';)
                {
                    rndY = rnd.Next(0, 9);
                    rndX = rnd.Next(0, 9);
                }
                state = Stater(rndX, rndY, 1);
                Field = FirstsBuild(rndX, rndY, Field, state, out bool success);
                if (success == false)
                    i--;
            }
            return Field;
        }
        public static char[,] FirstsBuild(int rndX, int rndY, char[,] Field, string state, out bool success)
        {
            success = false;
            switch (state)
            {
                case "Norm":
                    if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') ||
                        (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') ||
                        (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') ||
                        (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                        break;
                    else
                    {
                        Field[rndX, rndY] = '0';
                        success = true;
                    }
                    break;
                case "UpLeft":
                    if ((Field[rndX + 1, rndY] == '0') ||
                        (Field[rndX + 1, rndY + 1] == '0') ||
                        (Field[rndX, rndY + 1] == '0'))
                        break;
                    else
                    {
                        Field[rndX, rndY] = '0';
                        success = true;
                    }
                    break;
                case "Up":
                    if ((Field[rndX - 1, rndY] == '0') || (Field[rndX + 1, rndY] == '0') ||
                        (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX + 1, rndY + 1] == '0') ||
                        (Field[rndX, rndY + 1] == '0'))
                        break;
                    else
                    {
                        Field[rndX, rndY] = '0';
                        success = true;
                    }
                    break;
                case "UpRight":
                    if ((Field[rndX, rndY + 1] == '0') ||
                        (Field[rndX - 1, rndY + 1] == '0') ||
                        (Field[rndX - 1, rndY] == '0'))
                        break;
                    else
                    {
                        Field[rndX, rndY] = '0';
                        success = true;
                    }
                    break;
                case "Right":
                    if ((Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') ||
                        (Field[rndX - 1, rndY + 1] == '0') || (Field[rndX - 1, rndY - 1] == '0') ||
                        (Field[rndX - 1, rndY] == '0'))
                        break;
                    else
                    {
                        Field[rndX, rndY] = '0';
                        success = true;
                    }
                    break;
                case "DownRight":
                    if ((Field[rndX, rndY - 1] == '0') ||
                        (Field[rndX - 1, rndY - 1] == '0') ||
                        (Field[rndX - 1, rndY] == '0'))
                        break;
                    else
                    {
                        Field[rndX, rndY] = '0';
                        success = true;
                    }
                    break;
                case "Down":
                    if ((Field[rndX + 1, rndY - 1] == '0') || 
                        (Field[rndX, rndY - 1] == '0') ||
                        (Field[rndX - 1, rndY - 1] == '0') || 
                        (Field[rndX + 1, rndY] == '0') || (Field[rndX - 1, rndY] == '0'))
                        break;
                    else
                    {
                        Field[rndX, rndY] = '0';
                        success = true;
                    }
                    break;
                case "DownLeft":
                    if ((Field[rndX + 1, rndY - 1] == '0') ||
                        (Field[rndX, rndY - 1] == '0') ||
                        (Field[rndX + 1, rndY] == '0'))
                        break;
                    else
                    {
                        Field[rndX, rndY] = '0';
                        success = true;
                    }
                    break;
                case "Left":
                    if ((Field[rndX + 1, rndY + 1] == '0') || (Field[rndX + 1, rndY - 1] == '0') ||
                        (Field[rndX, rndY + 1] == '0') || (Field[rndX, rndY - 1] == '0') ||
                        (Field[rndX + 1, rndY] == '0'))
                        break;
                    else
                    {
                        Field[rndX, rndY] = '0';
                        success = true;
                    }
                    break;
            }
              return Field;
        }
        public static char[,] Draw3s0Deg(int rndX, int rndY, char[,] Field)
        {
            Field[rndX, rndY] = '0';
            Field[rndX, rndY - 1] = '0';
            Field[rndX, rndY - 2] = '0';
            return Field;
        }
        public static char[,] Draw3s90Deg(int rndX, int rndY, char[,] Field)
        {
            Field[rndX, rndY] = '0';
            Field[rndX + 1, rndY] = '0';
            Field[rndX + 2, rndY] = '0';
            return Field;
        }
        public static char[,] Draw3s180Deg(int rndX, int rndY, char[,] Field)
        {
            Field[rndX, rndY] = '0';
            Field[rndX, rndY + 1] = '0';
            Field[rndX, rndY + 2] = '0';
            return Field;
        }
        public static char[,] Draw3s270Deg(int rndX, int rndY, char[,] Field)
        {
            Field[rndX, rndY] = '0';
            Field[rndX - 1, rndY] = '0';
            Field[rndX - 2, rndY] = '0';
            return Field;
        }
        public static char[,] Draw2s0Deg(int rndX, int rndY, char[,] Field)
        {
            Field[rndX, rndY] = '0';
            Field[rndX, rndY - 1] = '0';
            return Field;
        }
        public static char[,] Draw2s90Deg(int rndX, int rndY, char[,] Field)
        {
            Field[rndX, rndY] = '0';
            Field[rndX + 1, rndY] = '0';
            return Field;
        }
        public static char[,] Draw2s180Deg(int rndX, int rndY, char[,] Field)
        {
            Field[rndX, rndY] = '0';
            Field[rndX, rndY + 1] = '0';
            return Field;
        }
        public static char[,] Draw2s270Deg(int rndX, int rndY, char[,] Field)
        {
            Field[rndX, rndY] = '0';
            Field[rndX - 1, rndY] = '0';
            return Field;
        }
        public static string Stater(int rndX, int rndY, byte type)
        {
            string state;
            int smaller = 0, higher = 0;
            if (type == 3)
            {
                smaller = 2;
                higher = 7;
            }
            else if (type == 2)
            {
                smaller = 1;
                higher = 8;
            }
            if (type != 1)
            {
                if ((rndX == 0) & (rndY == 0))
                    state = "UpLeft";
                else if ((rndX == 0) & (rndY > 0) & (rndY < 9))
                    state = "Left";
                else if ((rndX == 0) & (rndY == 9))
                    state = "DownLeft";
                else if ((rndX > 0) & (rndX < 9) & (rndY == 9))
                    state = "Down";
                else if ((rndX == 9) & (rndY == 9))
                    state = "DownRight";
                else if ((rndX == 9) & (rndY > 0) & (rndY < 9))
                    state = "Right";
                else if ((rndX == 9) & (rndY == 0))
                    state = "UpRight";
                else if ((rndX > 0) & (rndX < 9) & (rndY == 0))
                    state = "Up";
                else if ((rndX == smaller) & (rndY == smaller))
                    state = "InnerUpLeft";
                else if ((rndX == smaller) & (rndY > smaller) & (rndY < higher))
                    state = "InnerLeft";
                else if ((rndX == smaller) & (rndY == higher))
                    state = "InnerDownLeft";
                else if ((rndX > smaller) & (rndX < higher) & (rndY == higher))
                    state = "InnerDown";
                else if ((rndX == higher) & (rndY == higher))
                    state = "InnerDownRight";
                else if ((rndX == higher) & (rndY > smaller) & (rndY < higher))
                    state = "InnerRight";
                else if ((rndX == higher) & (rndY == smaller))
                    state = "InnerUpRight";
                else if ((rndX > smaller) & (rndX < higher) & (rndY == smaller))
                    state = "InnerUp";
                else
                    state = "Norm";
            }
            else
            {
                if ((rndX == 0) & (rndY == 0))
                    state = "UpLeft";
                else if ((rndX == 0) & (rndY > 0) & (rndY < 9))
                    state = "Left";
                else if ((rndX == 0) & (rndY == 9))
                    state = "DownLeft";
                else if ((rndX > 0) & (rndX < 9) & (rndY == 9))
                    state = "Down";
                else if ((rndX == 9) & (rndY == 9))
                    state = "DownRight";
                else if ((rndX == 9) & (rndY > 0) & (rndY < 9))
                    state = "Right";
                else if ((rndX == 9) & (rndY == 0))
                    state = "UpRight";
                else if ((rndX > 0) & (rndX < 9) & (rndY == 0))
                    state = "Up";
                else
                    state = "Norm";
            }
            return state;
        }
        public static char[,] Turner(int rndX, int rndY, byte type, char[,] Field, string state, out bool success)
        {
            Random rnd = new Random();
            success = false;
            int rndA, smaller = 0, higher = 0;
            if (type == 4)
            {
                smaller = 3;
                higher = 6;
            }
            else if(type == 3)
            {
                smaller = 2;
                higher = 7;
            }
            else if (type == 2)
            {
                smaller = 1;
                higher = 8;
            }
            if ((rndX >= smaller) & (rndX <= higher) & (rndY >= smaller) & (rndY <= higher))
            {
                rndA = rnd.Next(0, 3);
                if (type == 4)
                    Field = FourthsBuild(rndX, rndY, rndA, Field);
                else if(type == 3)
                    Field = ThirdsBuild(rndX, rndY, rndA, Field, state, out success);
                else
                    Field = SecondsBuild(rndX, rndY, rndA, Field, state, out success);
            }
            else if ((rndX <= smaller) & (rndY <= smaller))
            {
                rndA = rnd.Next(1, 2);
                if (type == 4)
                    Field = FourthsBuild(rndX, rndY, rndA, Field);
                else if (type == 3)
                    Field = ThirdsBuild(rndX, rndY, rndA, Field, state, out success);
                else
                    Field = SecondsBuild(rndX, rndY, rndA, Field, state, out success);
            }
            else if ((rndX >= higher) & (rndY <= smaller))
            {
                rndA = rnd.Next(2, 3);
                if (type == 4)
                    Field = FourthsBuild(rndX, rndY, rndA, Field);
                else if (type == 3)
                    Field = ThirdsBuild(rndX, rndY, rndA, Field, state, out success);
                else
                    Field = SecondsBuild(rndX, rndY, rndA, Field, state, out success);
            }
            else if ((rndX <= smaller) & (rndY >= higher))
            {
                rndA = rnd.Next(0, 1);
                if (type == 4)
                    Field = FourthsBuild(rndX, rndY, rndA, Field);
                else if (type == 3)
                    Field = ThirdsBuild(rndX, rndY, rndA, Field, state, out success);
                else
                    Field = SecondsBuild(rndX, rndY, rndA, Field, state, out success);
            }
            else if ((rndX >= higher) & (rndY >= higher))
            {
                rndA = rnd.Next(3, 4);
                if (rndA == 4)
                    rndA = 0;
                if (type == 4)
                    Field = FourthsBuild(rndX, rndY, rndA, Field);
                else if (type == 3)
                    Field = ThirdsBuild(rndX, rndY, rndA, Field, state, out success);
                else
                    Field = SecondsBuild(rndX, rndY, rndA, Field, state, out success);
            }
            else if ((rndX >= smaller) & (rndX <= higher) & (rndY <= smaller))
            {
                rndA = rnd.Next(1, 3);
                if (type == 4)
                    Field = FourthsBuild(rndX, rndY, rndA, Field);
                else if (type == 3)
                    Field = ThirdsBuild(rndX, rndY, rndA, Field, state, out success);
                else
                    Field = SecondsBuild(rndX, rndY, rndA, Field, state, out success);
            }
            else if ((rndX >= smaller) & (rndX <= higher) & (rndY >= higher))
            {
                rndA = rnd.Next(3, 5);
                if (rndA == 4)
                    rndA = 0;
                if (rndA == 5)
                    rndA = 1;
                if (type == 4)
                    Field = FourthsBuild(rndX, rndY, rndA, Field);
                else if (type == 3)
                    Field = ThirdsBuild(rndX, rndY, rndA, Field, state, out success);
                else
                    Field = SecondsBuild(rndX, rndY, rndA, Field, state, out success);
            }
            else if ((rndX <= smaller) & (rndY <= higher) & (rndY >= smaller))
            {
                rndA = rnd.Next(0, 2);
                if (type == 4)
                    Field = FourthsBuild(rndX, rndY, rndA, Field);
                else if (type == 3)
                    Field = ThirdsBuild(rndX, rndY, rndA, Field, state, out success);
                else
                    Field = SecondsBuild(rndX, rndY, rndA, Field, state, out success);
            }
            else if ((rndX >= higher) & (rndY <= higher) & (rndY >= smaller))
            {
                rndA = rnd.Next(2, 4);
                if (rndA == 4)
                    rndA = 0;
                if (type == 4)
                    Field = FourthsBuild(rndX, rndY, rndA, Field);
                else if (type == 3)
                    Field = ThirdsBuild(rndX, rndY, rndA, Field, state, out success);
                else
                    Field = SecondsBuild(rndX, rndY, rndA, Field, state, out success);
            }
            return Field;
        }
    }
}
