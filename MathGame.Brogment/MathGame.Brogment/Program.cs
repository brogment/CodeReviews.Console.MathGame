

string? readResult;
Random random = new Random();
int firstOperand;
int secondOperand;

do
{
    Console.WriteLine("Enter the number key next to the operation you wish to solve: ");
    Console.WriteLine(@"(1) Addition
(2) Subtraction
(3) Multiplication
(4) Division

(Q) Quit Program");


    readResult = Console.ReadLine();
    readResult = readResult?.ToLower();

    switch (readResult)
    {
        case "1":
        case "2":
        case "3":
            firstOperand = random.Next(100);
            secondOperand = random.Next(100);
            break;


        case "4":
            firstOperand = random.Next(100);
            do
            {
                secondOperand = random.Next(firstOperand);
            }
            while (firstOperand % secondOperand != 0);
            break;
        case "q":
            Console.WriteLine("Exiting Program...");
            break;
        default:
            Console.WriteLine("Please select an operation.");
            break;
    }


}
while (readResult != "q");


static string PerformOperation(string operater, int firstOperand, int SecondOperand)
{
    return "";
}