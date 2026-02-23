
using MathGame.Brogment;
using System.Text;

/*
 
Make option for timed gamemode where each question is timed, and they fail question if they go over time?

below isn't working, should make checkmark and big X work?
Console.OutputEncoding = Encoding.UTF8;

 */

/*use a mix of enum and struct to represent difficulties*/

Console.WriteLine("Please enter your name");
string playerName = Console.ReadLine() ?? "Player1";

Player player = new Player(playerName);

string difficulty = "1";
string gameTypeCode = "1";

while (true)
{
    Console.WriteLine(@"(1) Start Game
(2) Change Difficulty
(3) Change Game Type
(4) Show Game History and Total Score
(5) Exit Program");

    string input = ProcessKey("1234");

    if (input == "1")
        SingleGame(player, difficulty, gameTypeCode);
    else if (input == "2")
    {
        Console.WriteLine(@"Choose a difficulty:
(1) Easy   <  5  Rounds >
(2) Medium <  8  Rounds > 
(3) Hard   < 10  Rounds >");

        difficulty = ProcessKey("123");
    }
    else if (input == "3")
    {
        Console.WriteLine(@"Choose a gametype:
(1) Standard
(2) Random Operations");

        gameTypeCode = ProcessKey("12");
    }
    else if (input == "4")
    {
        player.PrintGames();
        player.PrintScore();
    }
    else
        break;
}

static void SingleGame(Player player, string difficulty, string gameTypeCode)
{

    int roundCount = difficulty == "1" ? 5 : difficulty == "2" ? 8 : 10;
    int maxOperandRange = difficulty == "1" ? 100 : difficulty == "2" ? 250 : 1000;

    int roundsCorrect = 0;

    for (int i = 0; i < roundCount; i++)
    {
        SingleRound(player, gameTypeCode, maxOperandRange, out bool wasCorrect);
        if (wasCorrect)
            roundsCorrect++;
    }

    Console.WriteLine($"You answered {roundsCorrect} out of {roundCount} questions correctly.");
}

static void SingleRound(Player player, string gameTypeCode, int maxOperandRange, out bool wasCorrect)
{
    Random random = new Random();

    string currOperator;
    var operations = new[] { "+", "-", "*", "/" };

    if (gameTypeCode == "1")
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

    bool isNumeric;
    int userAnswer;
    do
    {
        isNumeric = int.TryParse(Console.ReadLine(), out userAnswer);
        if (!isNumeric)
            Console.WriteLine("Please enter an integer.");
    }
    while (!isNumeric);

    player.UpdateGameHistory($"{firstOperand} {currOperator} {secondOperand} = {correctAnswer} | Your Answer: {userAnswer}");

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
}

static int PerformOperation(string operationSelection, int firstOperand, int secondOperand)
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

static string ProcessKey(string validInputs)
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