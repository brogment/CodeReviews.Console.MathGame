
namespace MathGame.Brogment
{
    public class Player(string name)
    {
        private List<GameRecord> gameHistory = new List<GameRecord>();
        public string Name { get; set; } = name;

        public int GamesPlayed { get; set; }
      
        public int Score { get; set; }

        public void IncreaseScoreAfterGame(int correctAnswers)
        {
            Score += correctAnswers;
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
