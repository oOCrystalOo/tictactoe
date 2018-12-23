using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    class Player
    {
        public string mark;
        public string name;

        public Player(string c, string n)
        {
            mark = c;
            name = n;
        }
    }    
}
