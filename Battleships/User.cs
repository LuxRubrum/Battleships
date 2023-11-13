using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class User
    {
        public static char[,] Check(string place, char[,] Field, out bool hit, out bool kill)
        {
            char psiNumVal = '0';
            char litVal = 'А';
            bool error;
            kill = false;
            hit = true;
            if (place.Length != 2)
                error = true;
            else
            {
                char[] PseudoNums = GameLauncher.GenPseudoNumArray();
                psiNumVal = place.Last();
                int diff1;
                bool flag1 = false;
                for (int i = 0; i < PseudoNums.Length; i++)
                {
                    diff1 = psiNumVal.CompareTo(PseudoNums[i]);
                    if (diff1 == 0)
                        flag1 = true;
                }
                if (flag1 == false)
                    error = true;
                else
                {
                    char[] Lits = GameLauncher.GenLitArray();
                    litVal = place.First();
                    int diff2;
                    bool flag2 = false;
                    for (int i = 0; i < Lits.Length; i++)
                    {
                        diff2 = litVal.CompareTo(Lits[i]);
                        if (diff2 == 0)
                            flag2 = true;
                    }
                    if (flag2 == false)
                        error = true;
                    else
                        error = false;
                }
            }
            return MoveInit(Convertor(psiNumVal, litVal), Field, out hit, out kill, error);
        }
        public static int[] Convertor(char num, char lit)
        {
            int litReal, numReal;
            switch (num)
            {
                case '0':
                    numReal = 0;
                    break;
                case '1':
                    numReal = 1;
                    break;
                case '2':
                    numReal = 2;
                    break;
                case '3':
                    numReal = 3;
                    break;
                case '4':
                    numReal = 4;
                    break;
                case '5':
                    numReal = 5;
                    break;
                case '6':
                    numReal = 6;
                    break;
                case '7':
                    numReal = 7;
                    break;
                case '8':
                    numReal = 8;
                    break;
                case '9':
                    numReal = 9;
                    break;
                default:
                    numReal = 10;
                    break;
            }
            switch (lit)
            {
                case 'А':
                    litReal = 0;
                    break;
                case 'Б':
                    litReal = 1;
                    break;
                case 'В':
                    litReal = 2;
                    break;
                case 'Г':
                    litReal = 3;
                    break;
                case 'Д':
                    litReal = 4;
                    break;
                case 'Е':
                    litReal = 5;
                    break;
                case 'Ж':
                    litReal = 6;
                    break;
                case 'З':
                    litReal = 7;
                    break;
                case 'И':
                    litReal = 8;
                    break;
                case 'К':
                    litReal = 9;
                    break;
                default:
                    litReal = 10;
                    break;
            }
            int[] Reals = new int[2];
            Reals[0] = litReal;
            Reals[1] = numReal;
            return Reals;
        }
        public static char[,] MoveInit(int[] Reals, char[,] Field, out bool hit, out bool kill, bool error)
        {
            kill = false;
            hit = false;
            if (error == true)
            {
                hit = true;
                Console.WriteLine("Некорректный ввод. Попробуйте снова. (Нажмите любую клавишу)");
                Console.ReadKey();
                return Field;
            }
            else if ((Field[Reals[0], Reals[1]] == 'х') || (Field[Reals[0], Reals[1]] == '#'))
            {
                hit = true;
                Console.WriteLine("Вы уже стреляли в данную клетку. Попробуйте снова. (Нажмите любую клавишу)");
                Console.ReadKey();
                return Field;
            }
            else if (Field[Reals[0], Reals[1]] == '0')
            {
                hit = true;
                if ((Reals[0] == 10) || (Reals[1] == 10))
                    Tools.Clear();
                else
                {
                    int saveX = Reals[1];
                    int saveY = Reals[0];
                    Field[Reals[0], Reals[1]] = '#';
                    CheckForKill(Reals, Field, out kill, "NotChecked");
                    Reals[0] = saveY;
                    Reals[1] = saveX;
                    if (kill == true)
                        Field = DeathMarker(Reals, Field, "NotChecked");
                }
                return Field;
            }
            else
            {
                if ((Reals[0] == 10) || (Reals[1] == 10))
                    Tools.Clear();
                else
                    Field[Reals[0], Reals[1]] = 'х';
                return Field;
            }
        }
        public static void CheckForKill(int[] Reals, char[,] Field, out bool kill, string flagOfCheck)
        {
            kill = true;
            bool cont = false;
            string state = "Center";
            if ((Reals[0] == 0) && (Reals[1] == 0))
                state = "UpLeft";
            else if ((Reals[0] == 0) && (Reals[1] > 0) && (Reals[1] < 9))
                state = "Up";
            else if ((Reals[0] == 0) && (Reals[1] == 9))
                state = "UpRight";
            else if ((Reals[0] > 0) && (Reals[0] < 9) && (Reals[1] == 9))
                state = "Right";
            else if ((Reals[0] == 9) && (Reals[1] == 9))
                state = "DownRight";
            else if ((Reals[0] == 9) && (Reals[1] > 0) && (Reals[1] < 9))
                state = "Down";
            else if ((Reals[0] == 9) && (Reals[1] == 0))
                state = "DownLeft";
            else if ((Reals[0] > 0) && (Reals[0] < 9) && (Reals[1] == 0))
                state = "Left";
            switch (state)
            {
                case "Center":
                    if ((Field[Reals[0], Reals[1] - 1] == '0') || (Field[Reals[0] - 1, Reals[1]] == '0') || (Field[Reals[0] + 1, Reals[1]] == '0') || (Field[Reals[0], Reals[1] + 1] == '0'))
                    {
                        if (Field[Reals[0], Reals[1] - 1] == '0')
                            kill = false;
                        if (Field[Reals[0] - 1, Reals[1]] == '0')
                            kill = false;
                        if (Field[Reals[0] + 1, Reals[1]] == '0')
                            kill = false;
                        if (Field[Reals[0], Reals[1] + 1] == '0')
                            kill = false;
                    }
                    else if ((Field[Reals[0], Reals[1] - 1] == '#') && (Field[Reals[0], Reals[1] + 1] == '#'))
                    {
                        bool kill1 = true;
                        bool kill2 = true;
                        int x = Reals[1];
                        if (flagOfCheck != "CameFromLeft")
                        {
                            Reals[1]--;
                            flagOfCheck = "CameFromRight";
                            CheckForKill(Reals, Field, out kill1, flagOfCheck);
                            flagOfCheck = "NotChecked";
                        }
                        Reals[1] = x;
                        if (flagOfCheck != "CameFromRight")
                        {
                            Reals[1]++;
                            flagOfCheck = "CameFromLeft";
                            CheckForKill(Reals, Field, out kill2, flagOfCheck);
                            flagOfCheck = "NotChecked";
                        }
                        if ((kill1 == true) && (kill2 == true))
                            kill = true;
                        else
                            kill = false;
                    }
                    else if ((Field[Reals[0] - 1, Reals[1]] == '#') && (Field[Reals[0] + 1, Reals[1]] == '#'))
                    {
                        bool kill1 = true;
                        bool kill2 = true;
                        int y = Reals[0];
                        if (flagOfCheck != "CameFromUp")
                        {
                            Reals[0]--;
                            flagOfCheck = "CameFromDown";
                            CheckForKill(Reals, Field, out kill1, flagOfCheck);
                            flagOfCheck = "NotChecked";
                        }
                        Reals[0] = y;
                        if (flagOfCheck != "CameFromDown")
                        {
                            Reals[0]++;
                            flagOfCheck = "CameFromUp";
                            CheckForKill(Reals, Field, out kill2, flagOfCheck);
                            flagOfCheck = "NotChecked";
                        }
                        if ((kill1 == true) && (kill2 == true))
                            kill = true;
                        else
                            kill = false;
                    }
                    else
                    {
                        if (Field[Reals[0] - 1, Reals[1]] == '#')
                        {
                            if (flagOfCheck != "CameFromUp")
                            {
                                Reals[0]--;
                                flagOfCheck = "CameFromDown";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0], Reals[1] + 1] == '#')
                        {
                            if (flagOfCheck != "CameFromRight")
                            {
                                Reals[1]++;
                                flagOfCheck = "CameFromLeft";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0] + 1, Reals[1]] == '#')
                        {
                            if (flagOfCheck != "CameFromDown")
                            {
                                Reals[0]++;
                                flagOfCheck = "CameFromUp";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0], Reals[1] - 1] == '#')
                        {
                            if (flagOfCheck != "CameFromLeft")
                            {
                                Reals[1]--;
                                flagOfCheck = "CameFromRight";
                                cont = true;
                            }
                        }
                    }
                    break;
                    
                case "UpLeft":
                    if ((Field[Reals[0] + 1, Reals[1]] == '0') || (Field[Reals[0], Reals[1] + 1] == '0'))
                    {
                        if (Field[Reals[0] + 1, Reals[1]] == '0')
                            kill = false;
                        if (Field[Reals[0], Reals[1] + 1] == '0')
                            kill = false;
                    }
                    else
                    {
                        if (Field[Reals[0], Reals[1] + 1] == '#')
                        {
                            if (flagOfCheck != "CameFromRight")
                            {
                                Reals[1]++;
                                flagOfCheck = "CameFromLeft";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0] + 1, Reals[1]] == '#')
                        {
                            if (flagOfCheck != "CameFromDown")
                            {
                                Reals[0]++;
                                flagOfCheck = "CameFromUp";
                                cont = true;
                            }
                        }
                    }
                    break;
                case "Up":
                    if ((Field[Reals[0], Reals[1] - 1] == '0') || (Field[Reals[0] + 1, Reals[1]] == '0') || (Field[Reals[0], Reals[1] + 1] == '0'))
                    {
                        if (Field[Reals[0], Reals[1] - 1] == '0')
                            kill = false;
                        if (Field[Reals[0] + 1, Reals[1]] == '0')
                            kill = false;
                        if (Field[Reals[0], Reals[1] + 1] == '0')
                            kill = false;
                    }
                    else if ((Field[Reals[0], Reals[1] - 1] == '#') && (Field[Reals[0], Reals[1] + 1] == '#'))
                    {
                        bool kill1 = true;
                        bool kill2 = true;
                        int x = Reals[1];
                        if (flagOfCheck != "CameFromLeft")
                        {
                            Reals[1]--;
                            flagOfCheck = "CameFromRight";
                            CheckForKill(Reals, Field, out kill1, flagOfCheck);
                            flagOfCheck = "NotChecked";
                        }
                        Reals[1] = x;
                        if (flagOfCheck != "CameFromRight")
                        {
                            Reals[1]++;
                            flagOfCheck = "CameFromLeft";
                            CheckForKill(Reals, Field, out kill2, flagOfCheck);
                            flagOfCheck = "NotChecked";
                        }
                        if ((kill1 == true) && (kill2 == true))
                            kill = true;
                        else
                            kill = false;
                    }
                    else
                    {
                        if (Field[Reals[0], Reals[1] + 1] == '#')
                        {
                            if (flagOfCheck != "CameFromRight")
                            {
                                Reals[1]++;
                                flagOfCheck = "CameFromLeft";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0] + 1, Reals[1]] == '#')
                        {
                            if (flagOfCheck != "CameFromDown")
                            {
                                Reals[0]++;
                                flagOfCheck = "CameFromUp";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0], Reals[1] - 1] == '#')
                        {
                            if (flagOfCheck != "CameFromLeft")
                            {
                                Reals[1]--;
                                flagOfCheck = "CameFromRight";
                                cont = true;
                            }
                        }
                    }
                    break;
                case "UpRight":
                    if ((Field[Reals[0], Reals[1] - 1] == '0') || (Field[Reals[0] + 1, Reals[1]] == '0'))
                    {
                        if (Field[Reals[0], Reals[1] - 1] == '0')
                            kill = false;
                        if (Field[Reals[0] + 1, Reals[1]] == '0')
                            kill = false;
                    }
                    else
                    {
                        if (Field[Reals[0] + 1, Reals[1]] == '#')
                        {
                            if (flagOfCheck != "CameFromDown")
                            {
                                Reals[0]++;
                                flagOfCheck = "CameFromUp";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0], Reals[1] - 1] == '#')
                        {
                            if (flagOfCheck != "CameFromLeft")
                            {
                                Reals[1]--;
                                flagOfCheck = "CameFromRight";
                                cont = true;
                            }
                        }
                    }
                    break;
                case "Right":
                    if ((Field[Reals[0], Reals[1] - 1] == '0') || (Field[Reals[0] - 1, Reals[1]] == '0') || (Field[Reals[0] + 1, Reals[1]] == '0'))
                    {
                        if (Field[Reals[0], Reals[1] - 1] == '0')
                            kill = false;
                        if (Field[Reals[0] - 1, Reals[1]] == '0')
                            kill = false;
                        if (Field[Reals[0] + 1, Reals[1]] == '0')
                            kill = false;
                    }
                    else if ((Field[Reals[0] - 1, Reals[1]] == '#') && (Field[Reals[0] + 1, Reals[1]] == '#'))
                    {
                        bool kill1 = true;
                        bool kill2 = true;
                        int y = Reals[0];
                        if (flagOfCheck != "CameFromUp")
                        {
                            Reals[0]--;
                            flagOfCheck = "CameFromDown";
                            CheckForKill(Reals, Field, out kill1, flagOfCheck);
                            flagOfCheck = "NotChecked";
                        }
                        Reals[0] = y;
                        if (flagOfCheck != "CameFromDown")
                        {
                            Reals[0]++;
                            flagOfCheck = "CameFromUp";
                            CheckForKill(Reals, Field, out kill2, flagOfCheck);
                            flagOfCheck = "NotChecked";
                        }
                        if ((kill1 == true) && (kill2 == true))
                            kill = true;
                        else
                            kill = false;
                    }
                    else
                    {
                        if (Field[Reals[0] - 1, Reals[1]] == '#')
                        {
                            if (flagOfCheck != "CameFromUp")
                            {
                                Reals[0]--;
                                flagOfCheck = "CameFromDown";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0] + 1, Reals[1]] == '#')
                        {
                            if (flagOfCheck != "CameFromDown")
                            {
                                Reals[0]++;
                                flagOfCheck = "CameFromUp";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0], Reals[1] - 1] == '#')
                        {
                            if (flagOfCheck != "CameFromLeft")
                            {
                                Reals[1]--;
                                flagOfCheck = "CameFromRight";
                                cont = true;
                            }
                        }
                    }
                    break;
                case "DownRight":
                    if ((Field[Reals[0], Reals[1] - 1] == '0') || (Field[Reals[0] - 1, Reals[1]] == '0'))
                    {
                        if (Field[Reals[0], Reals[1] - 1] == '0')
                            kill = false;
                        if (Field[Reals[0] - 1, Reals[1]] == '0')
                            kill = false;
                    }
                    else
                    {
                        if (Field[Reals[0] - 1, Reals[1]] == '#')
                        {
                            if (flagOfCheck != "CameFromUp")
                            {
                                Reals[0]--;
                                flagOfCheck = "CameFromDown";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0], Reals[1] - 1] == '#')
                        {
                            if (flagOfCheck != "CameFromLeft")
                            {
                                Reals[1]--;
                                flagOfCheck = "CameFromRight";
                                cont = true;
                            }
                        }
                    }
                    break;
                case "Down":
                    if ((Field[Reals[0], Reals[1] - 1] == '0') || (Field[Reals[0] - 1, Reals[1]] == '0') || (Field[Reals[0], Reals[1] + 1] == '0'))
                    {
                        if (Field[Reals[0], Reals[1] - 1] == '0')
                            kill = false;
                        if (Field[Reals[0] - 1, Reals[1]] == '0')
                            kill = false;
                        if (Field[Reals[0], Reals[1] + 1] == '0')
                            kill = false;
                    }
                    else if ((Field[Reals[0], Reals[1] - 1] == '#') && (Field[Reals[0], Reals[1] + 1] == '#'))
                    {
                        bool kill1 = true;
                        bool kill2 = true;
                        int x = Reals[1];
                        if (flagOfCheck != "CameFromLeft")
                        {
                            Reals[1]--;
                            flagOfCheck = "CameFromRight";
                            CheckForKill(Reals, Field, out kill1, flagOfCheck);
                            flagOfCheck = "NotChecked";
                        }
                        Reals[1] = x;
                        if (flagOfCheck != "CameFromRight")
                        {
                            Reals[1]++;
                            flagOfCheck = "CameFromLeft";
                            CheckForKill(Reals, Field, out kill2, flagOfCheck);
                            flagOfCheck = "NotChecked";
                        }
                        if ((kill1 == true) && (kill2 == true))
                            kill = true;
                        else
                            kill = false;
                    }
                    else
                    {
                        if (Field[Reals[0] - 1, Reals[1]] == '#')
                        {
                            if (flagOfCheck != "CameFromUp")
                            {
                                Reals[0]--;
                                flagOfCheck = "CameFromDown";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0], Reals[1] + 1] == '#')
                        {
                            if (flagOfCheck != "CameFromRight")
                            {
                                Reals[1]++;
                                flagOfCheck = "CameFromLeft";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0], Reals[1] - 1] == '#')
                        {
                            if (flagOfCheck != "CameFromLeft")
                            {
                                Reals[1]--;
                                flagOfCheck = "CameFromRight";
                                cont = true;
                            }
                        }
                    }
                    break;
                case "DownLeft":
                    if ((Field[Reals[0], Reals[1] - 1] == '0') || (Field[Reals[0] - 1, Reals[1]] == '0') || (Field[Reals[0], Reals[1] + 1] == '0'))
                    {
                        if (Field[Reals[0] - 1, Reals[1]] == '0')
                            kill = false;
                        if (Field[Reals[0], Reals[1] + 1] == '0')
                            kill = false;
                    }
                    else
                    {
                        if (Field[Reals[0] - 1, Reals[1]] == '#')
                        {
                            if (flagOfCheck != "CameFromUp")
                            {
                                Reals[0]--;
                                flagOfCheck = "CameFromDown";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0], Reals[1] + 1] == '#')
                        {
                            if (flagOfCheck != "CameFromRight")
                            {
                                Reals[1]++;
                                flagOfCheck = "CameFromLeft";
                                cont = true;
                            }
                        }
                    }
                    break;
                case "Left":
                    if ((Field[Reals[0] - 1, Reals[1]] == '0') || (Field[Reals[0] + 1, Reals[1]] == '0') || (Field[Reals[0], Reals[1] + 1] == '0'))
                    {
                        if (Field[Reals[0] - 1, Reals[1]] == '0')
                            kill = false;
                        if (Field[Reals[0] + 1, Reals[1]] == '0')
                            kill = false;
                        if (Field[Reals[0], Reals[1] + 1] == '0')
                            kill = false;
                    }
                    else if ((Field[Reals[0] - 1, Reals[1]] == '#') && (Field[Reals[0] + 1, Reals[1]] == '#'))
                    {
                        bool kill1 = true;
                        bool kill2 = true;
                        int y = Reals[0];
                        if (flagOfCheck != "CameFromUp")
                        {
                            Reals[0]--;
                            flagOfCheck = "CameFromDown";
                            CheckForKill(Reals, Field, out kill1, flagOfCheck);
                            flagOfCheck = "NotChecked";
                        }
                        Reals[0] = y;
                        if (flagOfCheck != "CameFromDown")
                        {
                            Reals[0]++;
                            flagOfCheck = "CameFromUp";
                            CheckForKill(Reals, Field, out kill2, flagOfCheck);
                            flagOfCheck = "NotChecked";
                        }
                        if ((kill1 == true) && (kill2 == true))
                            kill = true;
                        else
                            kill = false;
                    }
                    else
                    {
                        if (Field[Reals[0] - 1, Reals[1]] == '#')
                        {
                            if (flagOfCheck != "CameFromUp")
                            {
                                Reals[0]--;
                                flagOfCheck = "CameFromDown";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0], Reals[1] + 1] == '#')
                        {
                            if (flagOfCheck != "CameFromRight")
                            {
                                Reals[1]++;
                                flagOfCheck = "CameFromLeft";
                                cont = true;
                            }
                        }
                        if (Field[Reals[0] + 1, Reals[1]] == '#')
                        {
                            if (flagOfCheck != "CameFromDown")
                            {
                                Reals[0]++;
                                flagOfCheck = "CameFromUp";
                                cont = true;
                            }
                        }
                    }
                    break;
            }
            if (cont == true)
                CheckForKill(Reals, Field, out kill, flagOfCheck);
        }
        public static char[,] DeathMarker(int[] Reals, char[,] Field, string flagOfDir)
        {
            bool cont = false;
            string state = "Center";
            if ((Reals[0] == 0) && (Reals[1] == 0))
                state = "UpLeft";
            else if ((Reals[0] == 0) && (Reals[1] > 0) && (Reals[1] < 9))
                state = "Up";
            else if ((Reals[0] == 0) && (Reals[1] == 9))
                state = "UpRight";
            else if ((Reals[0] > 0) && (Reals[0] < 9) && (Reals[1] == 9))
                state = "Right";
            else if ((Reals[0] == 9) && (Reals[1] == 9))
                state = "DownRight";
            else if ((Reals[0] == 9) && (Reals[1] > 0) && (Reals[1] < 9))
                state = "Down";
            else if ((Reals[0] == 9) && (Reals[1] == 0))
                state = "DownLeft";
            else if ((Reals[0] > 0) && (Reals[0] < 9) && (Reals[1] == 0))
                state = "Left";
            switch (state)
            {
                case "Center":
                    if (Field[Reals[0] - 1, Reals[1] - 1] == '~')
                        Field[Reals[0] - 1, Reals[1] - 1] = 'х';
                    if (Field[Reals[0], Reals[1] - 1] == '~')
                        Field[Reals[0], Reals[1] - 1] = 'х';
                    if (Field[Reals[0] + 1, Reals[1] - 1] == '~')
                        Field[Reals[0] + 1, Reals[1] - 1] = 'х';
                    if (Field[Reals[0] - 1, Reals[1]] == '~')
                        Field[Reals[0] - 1, Reals[1]] = 'х';
                    if (Field[Reals[0] + 1, Reals[1]] == '~')
                        Field[Reals[0] + 1, Reals[1]] = 'х';
                    if (Field[Reals[0] - 1, Reals[1] + 1] == '~')
                        Field[Reals[0] - 1, Reals[1] + 1] = 'х';
                    if (Field[Reals[0], Reals[1] + 1] == '~')
                        Field[Reals[0], Reals[1] + 1] = 'х';
                    if (Field[Reals[0] + 1, Reals[1] + 1] == '~')
                        Field[Reals[0] + 1, Reals[1] + 1] = 'х';
                    if (Field[Reals[0] - 1, Reals[1]] == '#')
                    {
                        if (flagOfDir != "CameFromUp")
                        {
                            Reals[0]--;
                            flagOfDir = "CameFromDown";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0], Reals[1] + 1] == '#')
                    {
                        if (flagOfDir != "CameFromRight")
                        {
                            Reals[1]++;
                            flagOfDir = "CameFromLeft";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0] + 1, Reals[1]] == '#')
                    {
                        if (flagOfDir != "CameFromDown")
                        {
                            Reals[0]++;
                            flagOfDir = "CameFromUp";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0], Reals[1] - 1] == '#')
                    {
                        if (flagOfDir != "CameFromLeft")
                        {
                            Reals[1]--;
                            flagOfDir = "CameFromRight";
                            cont = true;
                        }
                    }
                    break;
                case "UpLeft":
                    if (Field[Reals[0] + 1, Reals[1]] == '~')
                        Field[Reals[0] + 1, Reals[1]] = 'х';
                    if (Field[Reals[0], Reals[1] + 1] == '~')
                        Field[Reals[0], Reals[1] + 1] = 'х';
                    if (Field[Reals[0] + 1, Reals[1] + 1] == '~')
                        Field[Reals[0] + 1, Reals[1] + 1] = 'х';
                    if (Field[Reals[0], Reals[1] + 1] == '#')
                    {
                        if (flagOfDir != "CameFromRight")
                        {
                            Reals[1]++;
                            flagOfDir = "CameFromLeft";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0] + 1, Reals[1]] == '#')
                    {
                        if (flagOfDir != "CameFromDown")
                        {
                            Reals[0]++;
                            flagOfDir = "CameFromUp";
                            cont = true;
                        }
                    }
                    break;
                case "Up":
                    if (Field[Reals[0], Reals[1] - 1] == '~')
                        Field[Reals[0], Reals[1] - 1] = 'х';
                    if (Field[Reals[0] + 1, Reals[1] - 1] == '~')
                        Field[Reals[0] + 1, Reals[1] - 1] = 'х';
                    if (Field[Reals[0] + 1, Reals[1]] == '~')
                        Field[Reals[0] + 1, Reals[1]] = 'х';
                    if (Field[Reals[0], Reals[1] + 1] == '~')
                        Field[Reals[0], Reals[1] + 1] = 'х';
                    if (Field[Reals[0] + 1, Reals[1] + 1] == '~')
                        Field[Reals[0] + 1, Reals[1] + 1] = 'х';
                    if (Field[Reals[0], Reals[1] + 1] == '#')
                    {
                        if (flagOfDir != "CameFromRight")
                        {
                            Reals[1]++;
                            flagOfDir = "CameFromLeft";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0] + 1, Reals[1]] == '#')
                    {
                        if (flagOfDir != "CameFromDown")
                        {
                            Reals[0]++;
                            flagOfDir = "CameFromUp";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0], Reals[1] - 1] == '#')
                    {
                        if (flagOfDir != "CameFromLeft")
                        {
                            Reals[1]--;
                            flagOfDir = "CameFromRight";
                            cont = true;
                        }
                    }
                    break;
                case "UpRight":
                    if (Field[Reals[0], Reals[1] - 1] == '~')
                        Field[Reals[0], Reals[1] - 1] = 'х';
                    if (Field[Reals[0] + 1, Reals[1] - 1] == '~')
                        Field[Reals[0] + 1, Reals[1] - 1] = 'х';
                    if (Field[Reals[0] + 1, Reals[1]] == '~')
                        Field[Reals[0] + 1, Reals[1]] = 'х';
                    if (Field[Reals[0] + 1, Reals[1]] == '#')
                    {
                        if (flagOfDir != "CameFromDown")
                        {
                            Reals[0]++;
                            flagOfDir = "CameFromUp";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0], Reals[1] - 1] == '#')
                    {
                        if (flagOfDir != "CameFromLeft")
                        {
                            Reals[1]--;
                            flagOfDir = "CameFromRight";
                            cont = true;
                        }
                    }
                    break;
                case "Right":
                    if (Field[Reals[0] - 1, Reals[1] - 1] == '~')
                        Field[Reals[0] - 1, Reals[1] - 1] = 'х';
                    if (Field[Reals[0], Reals[1] - 1] == '~')
                        Field[Reals[0], Reals[1] - 1] = 'х';
                    if (Field[Reals[0] + 1, Reals[1] - 1] == '~')
                        Field[Reals[0] + 1, Reals[1] - 1] = 'х';
                    if (Field[Reals[0] - 1, Reals[1]] == '~')
                        Field[Reals[0] - 1, Reals[1]] = 'х';
                    if (Field[Reals[0] + 1, Reals[1]] == '~')
                        Field[Reals[0] + 1, Reals[1]] = 'х';
                    if (Field[Reals[0] - 1, Reals[1]] == '#')
                    {
                        if (flagOfDir != "CameFromUp")
                        {
                            Reals[0]--;
                            flagOfDir = "CameFromDown";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0] + 1, Reals[1]] == '#')
                    {
                        if (flagOfDir != "CameFromDown")
                        {
                            Reals[0]++;
                            flagOfDir = "CameFromUp";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0], Reals[1] - 1] == '#')
                    {
                        if (flagOfDir != "CameFromLeft")
                        {
                            Reals[1]--;
                            flagOfDir = "CameFromRight";
                            cont = true;
                        }
                    }
                    break;
                case "DownRight":
                    if (Field[Reals[0] - 1, Reals[1] - 1] == '~')
                        Field[Reals[0] - 1, Reals[1] - 1] = 'х';
                    if (Field[Reals[0], Reals[1] - 1] == '~')
                        Field[Reals[0], Reals[1] - 1] = 'х';
                    if (Field[Reals[0] - 1, Reals[1]] == '~')
                        Field[Reals[0] - 1, Reals[1]] = 'х';
                    if (Field[Reals[0] - 1, Reals[1]] == '#')
                    {
                        if (flagOfDir != "CameFromUp")
                        {
                            Reals[0]--;
                            flagOfDir = "CameFromDown";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0], Reals[1] - 1] == '#')
                    {
                        if (flagOfDir != "CameFromLeft")
                        {
                            Reals[1]--;
                            flagOfDir = "CameFromRight";
                            cont = true;
                        }
                    }
                    break;
                case "Down":
                    if (Field[Reals[0] - 1, Reals[1] - 1] == '~')
                        Field[Reals[0] - 1, Reals[1] - 1] = 'х';
                    if (Field[Reals[0], Reals[1] - 1] == '~')
                        Field[Reals[0], Reals[1] - 1] = 'х';
                    if (Field[Reals[0] - 1, Reals[1]] == '~')
                        Field[Reals[0] - 1, Reals[1]] = 'х';
                    if (Field[Reals[0] - 1, Reals[1] + 1] == '~')
                        Field[Reals[0] - 1, Reals[1] + 1] = 'х';
                    if (Field[Reals[0], Reals[1] + 1] == '~')
                        Field[Reals[0], Reals[1] + 1] = 'х';
                    if (Field[Reals[0] - 1, Reals[1]] == '#')
                    {
                        if (flagOfDir != "CameFromUp")
                        {
                            Reals[0]--;
                            flagOfDir = "CameFromDown";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0], Reals[1] + 1] == '#')
                    {
                        if (flagOfDir != "CameFromRight")
                        {
                            Reals[1]++;
                            flagOfDir = "CameFromLeft";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0], Reals[1] - 1] == '#')
                    {
                        if (flagOfDir != "CameFromLeft")
                        {
                            Reals[1]--;
                            flagOfDir = "CameFromRight";
                            cont = true;
                        }
                    }
                    break;
                case "DownLeft":
                    if (Field[Reals[0] - 1, Reals[1]] == '~')
                        Field[Reals[0] - 1, Reals[1]] = 'х';
                    if (Field[Reals[0] - 1, Reals[1] + 1] == '~')
                        Field[Reals[0] - 1, Reals[1] + 1] = 'х';
                    if (Field[Reals[0], Reals[1] + 1] == '~')
                        Field[Reals[0], Reals[1] + 1] = 'х';
                    if (Field[Reals[0] - 1, Reals[1]] == '#')
                    {
                        if (flagOfDir != "CameFromUp")
                        {
                            Reals[0]--;
                            flagOfDir = "CameFromDown";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0], Reals[1] + 1] == '#')
                    {
                        if (flagOfDir != "CameFromRight")
                        {
                            Reals[1]++;
                            flagOfDir = "CameFromLeft";
                            cont = true;
                        }
                    }
                    break;
                case "Left":
                    if (Field[Reals[0] - 1, Reals[1]] == '~')
                        Field[Reals[0] - 1, Reals[1]] = 'х';
                    if (Field[Reals[0] + 1, Reals[1]] == '~')
                        Field[Reals[0] + 1, Reals[1]] = 'х';
                    if (Field[Reals[0] - 1, Reals[1] + 1] == '~')
                        Field[Reals[0] - 1, Reals[1] + 1] = 'х';
                    if (Field[Reals[0], Reals[1] + 1] == '~')
                        Field[Reals[0], Reals[1] + 1] = 'х';
                    if (Field[Reals[0] + 1, Reals[1] + 1] == '~')
                        Field[Reals[0] + 1, Reals[1] + 1] = 'х';
                    if (Field[Reals[0] - 1, Reals[1]] == '#')
                    {
                        if (flagOfDir != "CameFromUp")
                        {
                            Reals[0]--;
                            flagOfDir = "CameFromDown";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0], Reals[1] + 1] == '#')
                    {
                        if (flagOfDir != "CameFromRight")
                        {
                            Reals[1]++;
                            flagOfDir = "CameFromLeft";
                            cont = true;
                        }
                    }
                    if (Field[Reals[0] + 1, Reals[1]] == '#')
                    {
                        if (flagOfDir != "CameFromDown")
                        {
                            Reals[0]++;
                            flagOfDir = "CameFromUp";
                            cont = true;
                        }
                    }
                    break;
            }
            if (cont == true)
            {
                Field = DeathMarker(Reals, Field, flagOfDir);
                return Field;
            }
            else
                return Field;
        }
    }
}