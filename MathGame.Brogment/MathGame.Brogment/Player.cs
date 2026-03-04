using System;
using System.Collections.Generic;
using System.Text;

namespace MathGame.Brogment
{
    internal class Player(string name)
    {
        private List<GameRecord> gameHistory = new List<GameRecord>();
        public string Name { get; set; } = name;

        public int GamesPlayed { get; set; } = 0;
      
        public int Score { get; set; }

        public void IncreaseScore()
        {
            Score += 1;
        }
        public void UpdateGameHistory(GameRecord game)
        {
            gameHistory.Add(game);
        }

        public void PrintGames()
        {
            foreach (var game in gameHistory)
            {
                game.PrintGame();
            }
        }
    }
}
