using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Tools
    {
        public static int GetN()
        {
            int x = Convert.ToInt32(Console.ReadLine());
            return x;
        }
        public static void Clear()
        {
            Console.ReadKey();
            Console.Clear();
            GameLauncher.Intro(false);
        }
    }
}
