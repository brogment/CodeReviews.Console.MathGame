
using MathGame.Brogment;
using System.Text;

/*
 
Make option for timed gamemode where each question is timed, and they fail question if they go over time?
Should add a continue option so they don't have to reenter game options if they want to play the same game over and over
alternatively making it so you could return to menu at any time and change options

below isn't working, should make checkmark and big X work?
Console.OutputEncoding = Encoding.UTF8;

 */


Console.WriteLine("Please enter your name");
string playerName = Console.ReadLine() ?? "Player1";

Player player = new Player(playerName);
while (true)
{
    Console.WriteLine(@"(1) Continue
(2) Change Settings
(3) Exit Program");

    string input = ProcessKey("123");

    if (input == "1")
        SingleGame(player);
    else if (input == "2")
        ChangeSettings();
    else
        break;
}

SingleGame(player);


//putting chooseing options into own method, dict maybe I can pass to game in infinite loop?



static void SingleGame(Player player)
{

    Console.WriteLine(@"Choose a gametype:
(1) Standard
(2) Random Operations");

    string gameTypeCode = ProcessKey("12");

    Console.WriteLine(@"Choose a difficulty:
(1) Easy   <  5  Rounds >
(2) Medium <  8  Rounds > 
(3) Hard   < 10  Rounds >");

    string difficulty = ProcessKey("123");

    int roundCount = difficulty == "1" ? 5 : difficulty == "2" ? 8 : 10;
    int maxOperandRange = difficulty == "1" ? 100 : difficulty == "2" ? 250 : 1000;

    for (int i = 0; i < roundCount; i++)
    {
        SingleRound(player, gameTypeCode, maxOperandRange);

    }
    player.PrintGames();
    player.PrintScore();
    player.SetScore(0);


}

static void SingleRound(Player player, string gameTypeCode, int maxOperandRange)
{
    Random random = new Random();

    string currOperator;
    var operations = new[] {"+", "-", "*", "/"};

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


    if (userAnswer == correctAnswer)
    {
        Console.WriteLine("Correct!");
        player.IncreaseScore();
    }
    else
        Console.WriteLine("Incorrect!");

    player.UpdateGameHistory($"{firstOperand} {currOperator} {secondOperand} = {correctAnswer} | Your Answer: {userAnswer} {(userAnswer == correctAnswer ? "U+2713" : "U+FF38")}");
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