namespace MathGame.Brogment
{

    public class GameEngine(Player _player)
    {
        readonly Random random = new Random();
        protected Player player = _player;

        public static readonly Dictionary<Difficulty, DifficultySettings> difficultyMap = new Dictionary<Difficulty, DifficultySettings>
        {
            [Difficulty.Easy] = new DifficultySettings { keyboardKey = "1", difficultyName = "Easy", maxOperandRange = 100, minOperandRange = 0, roundcount = 5 },
            [Difficulty.Medium] = new DifficultySettings { keyboardKey = "2", difficultyName = "Medium", maxOperandRange = 250, minOperandRange = 100, roundcount = 8 },
            [Difficulty.Hard] = new DifficultySettings { keyboardKey = "3", difficultyName = "Hard", maxOperandRange = 1000, minOperandRange = 250, roundcount = 10 }
        };

        public static readonly Dictionary<GameType, GameTypeSettings> gameTypeMap = new Dictionary<GameType, GameTypeSettings>
        {
            [GameType.Standard] = new GameTypeSettings { keyboardKey = "1", gameTypeName = "Standard" },
            [GameType.Random] = new GameTypeSettings { keyboardKey = "2", gameTypeName = "Random Operations" }
        };

        public GameTypeSettings CurrentGameType { get; set; } = gameTypeMap[GameType.Standard];
        public DifficultySettings CurrentDifficulty { get; set; } = difficultyMap[Difficulty.Easy];
       

        public void SingleGame()
        {
            GameRecord gameRecord = new GameRecord(player.GamesPlayed, CurrentGameType, CurrentDifficulty);
            
            DateTime gameStartTime = DateTime.Now;

            for (int i = 0; i < CurrentDifficulty.roundcount; i++)
            {
                gameRecord.AddRound(SingleRound(gameRecord));
            }

            DateTime gameEndTime = DateTime.Now;
            TimeSpan gameTime = gameEndTime - gameStartTime;
            gameRecord.TimeLength = gameTime.TotalSeconds;

            player.UpdateGameHistory(gameRecord);
            player.IncreaseScoreAfterGame(gameRecord.CorrectAnswers);
            player.GamesPlayed++;

            Console.WriteLine($"You answered {gameRecord.CorrectAnswers} out of {CurrentDifficulty.roundcount} questions correctly.");
            Console.WriteLine($"Game Time: {gameTime.TotalSeconds.ToString("F2")} seconds");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public string SingleRound(GameRecord gameRecord)
        {
            string currOperator = ChooseOperation();

            (int firstOperand, int secondOperand) = GenerateOperands(currOperator);

            int correctAnswer = PerformOperation(currOperator, firstOperand, secondOperand);

            Console.WriteLine($"What is the result of {firstOperand} {currOperator} {secondOperand} ? ");

            int userAnswer = getNumericInput();

            if (userAnswer == correctAnswer)
            {
                Console.WriteLine("Correct!");
                gameRecord.CorrectAnswers += 1;
            }
            else
                Console.WriteLine("Incorrect!");

            return $"{firstOperand} {currOperator} {secondOperand} = {correctAnswer} | Your Answer: {userAnswer}";
        }

        private string ChooseOperation()
        {
            string currOperator;
            string[] operations = ["+", "-", "*", "/"];

            if (CurrentGameType.gameTypeName == "Standard")
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

            return currOperator;
        }

        public (int firstOperand, int secondOperand) GenerateOperands(string operatorSymbol)
        {
            int lowerLimit = CurrentDifficulty.minOperandRange;
            int upperLimmit = CurrentDifficulty.maxOperandRange;

            int firstOperand;
            int secondOperand;

            if (operatorSymbol == "/")
            {
                do
                {
                    //avoiding divide by zero and easy divide by 1 or 2 questions
                    lowerLimit = lowerLimit < 3 ? 3 : lowerLimit;
                    firstOperand = random.Next(lowerLimit, upperLimmit);
                    secondOperand = random.Next(3, upperLimmit);
                } while (firstOperand % secondOperand != 0 || firstOperand == secondOperand);
            }
            else
            {
                firstOperand = random.Next(lowerLimit, upperLimmit);
                secondOperand = random.Next(lowerLimit, upperLimmit);
            }

            return (firstOperand, secondOperand);
        }


        public int PerformOperation(string operatorSymbol, int firstOperand, int secondOperand)
        {
            if (operatorSymbol == "+")
                return firstOperand + secondOperand;
            else if (operatorSymbol == "-")
                return firstOperand - secondOperand;
            else if (operatorSymbol == "*")
                return firstOperand * secondOperand;
            else if (operatorSymbol == "/")
                return firstOperand / secondOperand;
            else
                return 0;
        }
        public static int getNumericInput()
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

        public static string ProcessKey(string validInputs)
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

        public static void PrintDifficultySettings()
        {
            foreach (var setting in difficultyMap.Values)
            {
                Console.WriteLine($"({setting.keyboardKey}) {setting.difficultyName} < {setting.roundcount} Rounds >");
            }
        }

        public static void PrintGameTypeSettings()
        {
            foreach (var setting in gameTypeMap.Values)
            {
                Console.WriteLine($"({setting.keyboardKey}) {setting.gameTypeName}");
            }
        }
    }
}
