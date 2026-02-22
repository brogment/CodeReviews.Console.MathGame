using System;
using System.Collections.Generic;
using System.Text;

namespace MathGame.Brogment
{
    internal class Player
    {
        private string name;
        private int score = 0;
        private List<string> gameHistory = new List<string>();

        public Player(string name)
        {
            this.name = name;
        }

    }
}
