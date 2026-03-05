using MathGame.Brogment;

namespace MathGameTests
{
    [TestClass]
    public sealed class MathGameUnitTests
    {


        private TextReader _originalIn;

        [TestInitialize]
        public void Setup()
        {
            _originalIn = Console.In;
        }


        [TestCleanup]
        public void Cleanup()
        {
            Console.SetIn(_originalIn);
        }


        [TestMethod]
        public void PerformOperation_Division_ReturnsQuotient()
        {
            //arrange
            Player player = new Player("TestUser");
            GameEngine gameEngine = new GameEngine(player);
            int firstOperand = 32;
            int secondOperand = 8;
            int expected = 4;
            int actual;

            //act
            actual = gameEngine.PerformOperation("/", firstOperand, secondOperand);

            //assert
            Assert.AreEqual(expected, actual, "Division not peformed correctly");
        }
    }
}
