using System;
using System.Collections.Generic;
using System.Text;

namespace MathGame.Brogment
{
    internal class Player
    {
        // need to make these fields properties with getters / setters
        private string name;
        private int score = 0;
        private List<string> gameHistory = new List<string>();

        public Player(string name)
        {
            Name = name;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }      

        public int Score { get; set; }

        public void IncreaseScore()
        {
            Score += 1;
        }
        public void UpdateGameHistory(string game)
        {
            gameHistory.Add(game);
        }

        public void PrintGames()
        {
            foreach (var game in gameHistory)
            {
                Console.WriteLine(game);
            }
        }
    }
}
