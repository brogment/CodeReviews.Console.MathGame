
namespace MathGame.Brogment
{
    public class GameRecord(int id, GameTypeSettings gameType, DifficultySettings difficulty)
    {
        private readonly int id = id;

        private List<String> rounds = new List<String>();

        public int CorrectAnswers { get; set; }
        public double TimeLength { get; set; }
        public GameTypeSettings GameType { get; set; } = gameType;

        public DifficultySettings Difficulty { get; set; } = difficulty;

        public void AddRound(string round)
        {
            rounds.Add(round);
        }

        public void PrintGame()
        {
            Console.WriteLine("Game {0}:", id);
            Console.WriteLine($"\tDifficulty: {Difficulty.difficultyName} | Game Mode: {GameType.gameTypeName}");
            Console.WriteLine("\tTime: {0}", TimeLength.ToString("F2"));
            Console.WriteLine("\tYou answered {0} / {1} questions correctly", CorrectAnswers, Difficulty.roundcount);
            foreach (var round in rounds)
            {
                Console.WriteLine($"\t{round}");
            }
            Console.WriteLine();
        }

    }
}
