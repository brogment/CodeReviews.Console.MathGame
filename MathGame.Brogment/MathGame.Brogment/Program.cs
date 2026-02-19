
/*
 
Make option for timed gamemode where each question is timed, and they fail question if they go over time?
 
 */
using System.Text;

// not working Console.OutputEncoding = Encoding.UTF8;


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
    int playerScore = 0;



    var operatorMap = new Dictionary<string, string>()
{
    {"1", "+" },
    {"2", "-"},
    {"3", "*"},
    {"4", "/" }
};

    bool validInput = false;

    do
    {
        Console.WriteLine("Enter the number key next to the operation you wish to solve: ");
        Console.WriteLine(@"(1) Addition
(2) Subtraction
(3) Multiplication
(4) Division");

        readResult = Console.ReadLine();

        if (readResult != null)
        {
            if ("1234".Contains(readResult) && readResult.Length == 1)
                validInput = true;
        }
        else
            Console.WriteLine("Please select an option.");

    }
    while (!validInput);

    firstOperand = random.Next(maxOperandRange);

    if (readResult == "4")
    {
        do
        {
            secondOperand = random.Next(1, firstOperand / 2 + 1); // optimizing finding second operand
        }
        while (firstOperand % secondOperand != 0);
    }
    else
        secondOperand = random.Next(maxOperandRange);

    currOperator = operatorMap[readResult];

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
    {
        playerScore++;
        Console.WriteLine("Correct!");
    }
    else
        Console.WriteLine("Incorrect!");

    gameHistory.Add($"{firstOperand} {currOperator} {secondOperand} = {correctAnswer} | Your Answer: {userAnswer} {(userAnswer == correctAnswer ? "U+2713" : "U+FF38")}");


}




static int PerformOperation(string operatorSymbol, int firstOperand, int secondOperand)
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

static void PrintGames(List<string> gameHistory)
{
    foreach (var game in gameHistory)
    {
        Console.WriteLine(game);
    }
}