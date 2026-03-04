
using MathGame.Brogment;


Console.WriteLine("Please enter your name");
string playerName = Console.ReadLine() ?? "Player1";

Player player = new Player(playerName);

GameEngine gameEngine = new GameEngine(player);

while (true)
{
    Console.Clear();
    Console.WriteLine($@"(1) Start Game
(2) Change Difficulty [ Currently: {gameEngine.CurrentDifficulty.difficultyName} ]
(3) Change Game Type [ Currently: {gameEngine.CurrentGameType.gameTypeName} ]
(4) Show Game History and Total Score
(5) Exit Program");

    string input = GameEngine.ProcessKey("12345");

    Console.Clear();
    if (input == "1")
        gameEngine.SingleGame();
    else if (input == "2")
    {
        Console.WriteLine("Choose a difficulty:");
        GameEngine.PrintDifficultySettings();

        string difficultySelection = GameEngine.ProcessKey("123");
        foreach (var setting in GameEngine.difficultyMap.Values)
        {
            if (setting.keyboardKey == difficultySelection)
                gameEngine.CurrentDifficulty = setting;
        }
    }
    else if (input == "3")
    {
        Console.WriteLine("Choose a gametype:");
        GameEngine.PrintGameTypeSettings();

        string gameTypeSelection = GameEngine.ProcessKey("123");
        foreach (var setting in GameEngine.gameTypeMap.Values)
        {
            if (setting.keyboardKey == gameTypeSelection)
                gameEngine.CurrentGameType = setting;

        }
    }
    else if (input == "4")
    {
        Console.WriteLine($"Player Name: {player.Name}");

        player.PrintGames();

        Console.WriteLine($"Total Score: {player.Score}");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    else
        break;
}
