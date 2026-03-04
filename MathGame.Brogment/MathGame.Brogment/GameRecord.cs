using System;
using System.Collections.Generic;
using System.Text;

namespace MathGame.Brogment
{
    internal class GameRecord(int id, int totalRounds)
    {
        private readonly int id = id;
        private readonly int totalRounds = totalRounds;

        private List<String> rounds = new List<String>();

        public int CorrectAnswers { get; set; }
        public double TimeLength { get; set; }

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
