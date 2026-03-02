
using MathGame.Brogment;


Console.WriteLine("Please enter your name");
string playerName = Console.ReadLine() ?? "Player1";

Player player = new Player(playerName);

var difficultyMap = new Dictionary<Difficulty, DifficultySettings>
{
    [Difficulty.Easy] = new DifficultySettings { keyboardKey = "1", difficultyName = "Easy", maxOperandRange = 100, roundcount = 5},
    [Difficulty.Medium] = new DifficultySettings { keyboardKey = "2", difficultyName = "Medium", maxOperandRange = 250, roundcount = 8},
    [Difficulty.Hard] = new DifficultySettings { keyboardKey = "3", difficultyName = "Hard", maxOperandRange = 1000, roundcount = 10}
};

var gameTypeMap = new Dictionary<GameType, GameTypeSettings>
{
    [GameType.Standard] = new GameTypeSettings { keyboardKey = "1", gameTypeName = "Standard" },
    [GameType.Random] = new GameTypeSettings { keyboardKey = "2", gameTypeName = "Random Operations" }
};

GameTypeSettings currentGameType = gameTypeMap[GameType.Standard];
DifficultySettings currentDifficulty = difficultyMap[Difficulty.Easy];

while (true)
{
    Console.Clear();
    Console.WriteLine($@"(1) Start Game
(2) Change Difficulty [ Currently: {currentDifficulty.difficultyName} ]
(3) Change Game Type [ Currently: {currentGameType.gameTypeName} ]
(4) Show Game History and Total Score
(5) Exit Program");

    string input = ProcessKey("12345");

    Console.Clear();
    if (input == "1")
        SingleGame(player, currentDifficulty, currentGameType);
    else if (input == "2")
    {
        Console.WriteLine("Choose a difficulty:"); 
        foreach (var setting in difficultyMap.Values)
        {
            Console.WriteLine($"({setting.keyboardKey}) {setting.difficultyName} < {setting.roundcount} Rounds >");
        }

        string difficultySelection = ProcessKey("123");
        foreach (var setting in difficultyMap.Values)
        {
            if (setting.keyboardKey == difficultySelection)
                currentDifficulty = setting;
        }
    }
    else if (input == "3")
    {
        Console.WriteLine("Choose a gametype:");
        foreach (var setting in gameTypeMap.Values)
        {
            Console.WriteLine($"({setting.keyboardKey}) {setting.gameTypeName}");
        }

        string gameTypeSelection = ProcessKey("123");
        foreach (var setting in gameTypeMap.Values)
        {
            if (setting.keyboardKey == gameTypeSelection)
                currentGameType = setting;

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

 void SingleGame(Player player, DifficultySettings currentDifficulty, GameTypeSettings gameTypeCode)
{
    GameRecord gameRecord = new GameRecord(player.GamesPlayed, currentDifficulty.roundcount);
    
    DateTime gameStartTime = DateTime.Now;
    int roundsCorrect = 0;

    for (int i = 0; i < currentDifficulty.roundcount; i++)
    {
        
        gameRecord.AddRound(SingleRound(player, gameTypeCode.gameTypeName, currentDifficulty.maxOperandRange, out bool wasCorrect));
        if (wasCorrect)
            roundsCorrect++;
    }

    DateTime gameEndTime = DateTime.Now;
    TimeSpan gameTime = gameEndTime - gameStartTime;
    gameRecord.TimeLength = gameTime.TotalSeconds;
    gameRecord.CorrectAnswers = roundsCorrect;
    
    player.UpdateGameHistory(gameRecord);
    player.GamesPlayed++;


    Console.WriteLine($"You answered {roundsCorrect} out of {currentDifficulty.roundcount} questions correctly.");
    Console.WriteLine($"Game Time: {gameTime.TotalSeconds.ToString("F2")} seconds");
    Console.WriteLine("Press any key to continue");
    Console.ReadKey();
}

 string SingleRound(Player player, string gameTypeName, int maxOperandRange, out bool wasCorrect)
{
    Random random = new Random();

    string currOperator;
    var operations = new[] { "+", "-", "*", "/" };

    if (gameTypeName == "Standard")
    {
        Console.WriteLine("Enter the number key next to the operation you wish to solve: ");
        Console.WriteLine(@"(1) Addition
(2) Subtraction
(3) Multiplication
(4) Division");

        string input = ProcessKey("1234");
        currOperator = operations[int.Parse(input) - 1];
    }
    else
    {
        currOperator = operations[random.Next(0, operations.Length)];
    }

    int firstOperand = random.Next(maxOperandRange);
    int secondOperand;

    if (currOperator == "/")
    {
        do
        {
            secondOperand = random.Next(1, firstOperand / 2 + 1);
        }
        while (firstOperand % secondOperand != 0);
    }
    else
        secondOperand = random.Next(maxOperandRange);

    int correctAnswer = PerformOperation(currOperator, firstOperand, secondOperand);

    Console.WriteLine($"What is the result of {firstOperand} {currOperator} {secondOperand} ? ");

    int userAnswer = getNumericInput();

    if (userAnswer == correctAnswer)
    {
        Console.WriteLine("Correct!");
        player.IncreaseScore();
        wasCorrect = true;
    }
    else
    {
        Console.WriteLine("Incorrect!");
        wasCorrect = false;
    }

    return $"{firstOperand} {currOperator} {secondOperand} = {correctAnswer} | Your Answer: {userAnswer}";
}

int getNumericInput()
{
    bool isNumeric;
    int userAnswer;
    do
    {
        isNumeric = int.TryParse(Console.ReadLine(), out userAnswer);
        if (!isNumeric)
            Console.WriteLine("Please enter an integer.");
    }
    while (!isNumeric);

    return userAnswer;
}

int PerformOperation(string operationSelection, int firstOperand, int secondOperand)
{
    if (operationSelection == "+")
        return firstOperand + secondOperand;
    else if (operationSelection == "-")
        return firstOperand - secondOperand;
    else if (operationSelection == "*")
        return firstOperand * secondOperand;
    else if (operationSelection == "/")
        return firstOperand / secondOperand;
    else
        return 0;
}

 string ProcessKey(string validInputs)
{
    string? keyValue;
    while (true)
    {
        ConsoleKeyInfo keyPressed = Console.ReadKey();
        Console.WriteLine();
        keyValue = keyPressed.KeyChar.ToString();

        if (validInputs.Contains(keyValue))
        {
            return keyValue;
        }
        
    }
}

 enum Difficulty { Easy, Medium, Hard};

 enum GameType { Standard, Random, Timed};

 struct GameTypeSettings
{
    public string keyboardKey;
    public string gameTypeName;
}

 struct DifficultySettings
{
    public string keyboardKey;
    public string difficultyName;
    public int roundcount;
    public int maxOperandRange;
};