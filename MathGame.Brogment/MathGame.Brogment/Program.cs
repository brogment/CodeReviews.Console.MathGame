
Random random = new Random();

string? readResult;

int firstOperand;
int secondOperand;
int maxOperandRange = 100;
int correctAnswer;

do
{
    Console.WriteLine("Enter the number key next to the operation you wish to solve: ");
    Console.WriteLine(@"(1) Addition
(2) Subtraction
(3) Multiplication
(4) Division

(Q) Quit Program");
    
    readResult = Console.ReadLine();
    if (readResult != null)
    {
        if ("1234".Contains(readResult) && readResult.Length == 1)
        {

            firstOperand = random.Next(maxOperandRange);
            if (readResult == "4")
            {
                do
                {
                    secondOperand = random.Next(firstOperand/2 + 1); // optimizing finding second operand
                }
                while (firstOperand % secondOperand != 0);
            } else
            {
                secondOperand = random.Next(maxOperandRange);
            }

            correctAnswer = PerformOperation(readResult, firstOperand, secondOperand);
            Console.WriteLine($"What is the result of "); // need a dict that maps number choice selection to operation symbol

        } else if (readResult.ToLower() == "q")
        {
            Console.WriteLine("Exiting program...");
            break;
        }
    }

    Console.WriteLine("Please select an option.");

}
while (true);


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