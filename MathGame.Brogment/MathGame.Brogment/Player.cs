using System;
using System.Collections.Generic;
using System.Text;

namespace MathGame.Brogment
{
    internal class Player
    {
        private string name;
        private int gamesPlayed = 0;
        private List<GameRecord> gameHistory = new List<GameRecord>();

        public Player(string name)
        {
            Name = name;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }      

        public int GamesPlayed
        {
            get { return gamesPlayed; }
            set { gamesPlayed = value; }
        }
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
