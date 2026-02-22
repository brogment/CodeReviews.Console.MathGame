
using MathGame.Brogment;
using System.Text;

/*
 
Make option for timed gamemode where each question is timed, and they fail question if they go over time?

below isn't working, should make checkmark and big X work?
Console.OutputEncoding = Encoding.UTF8;

 */


Console.WriteLine("Please enter your name");
string playerName = Console.ReadLine() ?? "Player1";

Console.WriteLine(@"Choose a gametype:
(1) Standard
(2) Random Operations");

string gameTypeCode = ProcessKey("12");

//Player player = new Player(playerName);

var gameHistory = new List<string>();

SingleGame(5, gameHistory);


static void SingleGame(int roundCount, List<string>? gameHistory)
{
    for (int i = 0; i < roundCount; i++)
    {
        SingleRound(gameHistory);
    }

    PrintGames(gameHistory);
}

static void SingleRound(List<string>? gameHistory)
{
    Random random = new Random();

    string? readResult;

    int firstOperand;
    int secondOperand;
    int maxOperandRange = 100;
    int correctAnswer;
    string currOperator;

    Console.WriteLine("Enter the number key next to the operation you wish to solve: ");
    Console.WriteLine(@"(1) Addition
(2) Subtraction
(3) Multiplication
(4) Division");

    readResult = ProcessKey("1234");

    if (readResult == "1")
        currOperator = "+";
    else if (readResult == "2")
        currOperator = "-";
    else if (readResult == "3")
        currOperator = "*";
    else
        currOperator = "/";


    firstOperand = random.Next(maxOperandRange);

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

    correctAnswer = PerformOperation(currOperator, firstOperand, secondOperand);

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


    if (userAnswer == correctAnswer)
        Console.WriteLine("Correct!");
    else
        Console.WriteLine("Incorrect!");

    gameHistory.Add($"{firstOperand} {currOperator} {secondOperand} = {correctAnswer} | Your Answer: {userAnswer} {(userAnswer == correctAnswer ? "U+2713" : "U+FF38")}");
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

static void PrintGames(List<string> gameHistory)
{
    foreach (var game in gameHistory)
    {
        Console.WriteLine(game);
    }
}

static string ProcessKey(string validInputs)
{
    string? keyValue;
    while(true)
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