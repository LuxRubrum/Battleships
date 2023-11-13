using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Bot
    {
        public static char[,] Check(char[,] Field, out bool hit, out bool kill)
        {
            Random rnd = new Random();
            int[] Reals = new int[2];
            Reals[0] = rnd.Next(0, 9);
            Reals[1] = rnd.Next(0, 9);
            for(; ((Field[Reals[0], Reals[1]] == 'х') && (Field[Reals[0], Reals[1]] == '#'));)
            {
                Reals[0] = rnd.Next(0, 9);
                Reals[1] = rnd.Next(0, 9);
            }
            return MoveInit(Reals, Field, out hit, out kill);
        }
        public static char[,] MoveInit(int[] Reals, char[,] Field, out bool hit, out bool kill)
        {
            kill = false;
            hit = false;
            if (Field[Reals[0], Reals[1]] == '0')
            {
                hit = true;
                int saveX = Reals[1];
                int saveY = Reals[0];
                Field[Reals[0], Reals[1]] = '#';
                User.CheckForKill(Reals, Field, out kill, "NotChecked");
                Reals[0] = saveY;
                Reals[1] = saveX;
                if (kill == true)
                    Field = User.DeathMarker(Reals, Field, "NotChecked");
                return Field;
            }
            else
            {
                Field[Reals[0], Reals[1]] = 'х';
                return Field;
            }
        }
    }
}
