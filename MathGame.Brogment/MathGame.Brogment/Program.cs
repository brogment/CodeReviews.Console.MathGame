

string? readResult;
Random random = new Random();

int firstOperand = random.Next();
int secondOperand = random.Next();

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
            break;
        case "2":
            break;
        case "3":
            break;
        case "4":
            break;

    }


}
while (readResult != "q");


static string PerformOperation()
{
    return "";
}