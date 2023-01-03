using Moq.AutoMock;
using Problems.Problems;
using Problems.UserInteraction;

namespace Tests.ProblemsTests
{
    [TestClass]
    public class CoinExchangeTests
    {
        [TestMethod]
        public void TestRun_Should_Display_NecessaryCoins()
        {
            var result = new int[] { 50, 20, 20, 5, 2, 2 };

            var orchestration = new TestOrchestration
            {
                Input = 99,
                Output = result
            };

            DoTest(orchestration);
        }

        [TestMethod]// problem with this test for some reason
        public void TestRun_Should_Display_Nothing_For_Input_Zero()
        {
            var orchestration = new TestOrchestration
            {
                Input = 0,
                Output = Array.Empty<int>()
            };

            DoTest(orchestration);
        }

        private static void DoTest(TestOrchestration orchestration)
        {
            //SETUP
            var mocker = new AutoMocker();

            //BEHAVIOUR
            mocker.Setup<ICoinExchangeUserInteraction, int>(x => x.GetOriginalInput())
                .Returns(orchestration.Input);

            mocker.Setup<ICoinExchangeUserInteraction>(x => x.DisplayResult(orchestration.Output));

            //TEST
            var subject = mocker.CreateInstance<CoinExchange>();
            subject.Run();

            mocker.VerifyAll();


        }

        private class TestOrchestration
        {
            public int Input { get; set; }
            public int[] Output { get; set; }   
        }
    }
}
