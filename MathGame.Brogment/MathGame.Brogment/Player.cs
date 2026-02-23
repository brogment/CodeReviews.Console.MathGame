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

        public void IncreaseScore()
        {
            score += 1;
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
        public void PrintScore()
        {
            Console.WriteLine($"Score: {score}");
        }
        public void SetScore(int newScore)
        {
            score = newScore;
        }
    }
}
