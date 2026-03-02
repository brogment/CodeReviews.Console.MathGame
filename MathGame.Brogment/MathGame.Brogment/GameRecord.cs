using System;
using System.Collections.Generic;
using System.Text;

namespace MathGame.Brogment
{
    internal class GameRecord
    {
        private readonly int id;
        private readonly int totalRounds;

        private List<String> rounds = new List<String>();

        public int CorrectAnswers { get; set; }
        public double TimeLength { get; set; }
        public GameRecord(int id, int totalRounds)
        {
            this.id = id;
            this.totalRounds = totalRounds;
        }
        public void AddRound(string round)
        {
            rounds.Add(round);
        }

        public void PrintGame()
        {
            Console.WriteLine("Game {0}:", id);
            Console.WriteLine("\tTime: {0}", TimeLength.ToString("F2"));
            Console.WriteLine("\tYou answered {0} / {1} questions correctly", CorrectAnswers, totalRounds);
            foreach (var round in rounds)
            {
                Console.WriteLine($"\t{round}");
            }
            Console.WriteLine();
        }

    }
}
