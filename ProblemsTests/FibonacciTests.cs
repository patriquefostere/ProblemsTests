using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Problems.Problems;
using Problems.UserInteraction;


namespace Tests.ProblemsTests
{
    //https://itnext.io/how-to-fully-cover-net-c-console-application-with-unit-tests-446927a4a793
    [TestClass]
    public class FibonacciTests
    {
        [TestMethod]
        public void TestRun_Should_Display_Zero_For_Input_Of_Zero()
        {
            //SETUP
            var mocker = new AutoMocker();

            //BEHAVIOUR
            mocker.Setup<IFibonacciUserInteraction, int>(x => x.GetOriginalInput())
                .Returns(0);

            mocker.Setup<IFibonacciUserInteraction>(x => x.DisplayResult(0));

            //TEST
            var subject = mocker.CreateInstance<Fibonacci>();
            subject.Run();

            mocker.VerifyAll();

            //breaks for some reason
            //var orchestration = new TestOrchestration
            //{
            //    Input = 0,
            //};
            //DoTest(orchestration);
        }

        [TestMethod]
        public void TestRun_Should_Display_Zero_For_Input_Of_One()
        {
            //SETUP
            var mocker = new AutoMocker();

            //BEHAVIOUR
            mocker.Setup<IFibonacciUserInteraction, int>(x => x.GetOriginalInput())
                .Returns(1);

            mocker.Setup<IFibonacciUserInteraction>(x => x.DisplayResult(0));

            //TEST
            var subject = mocker.CreateInstance<Fibonacci>();
            subject.Run();

            mocker.VerifyAll();

            //breaks for some reason
            //var orchestration = new TestOrchestration
            //{
            //    Input = 1,
            //};
            //DoTest(orchestration);
        }

        [TestMethod]
        public void TestRun_Should_Calculate_And_Display_Sum_Of_Fibonacci()
        {
           
            var orchestration = new TestOrchestration
            {
                Input = 6,
                Result = 12,
            };
            DoTest(orchestration);
        }

        [TestMethod]
        public void TestRun_Should_Throw_OverflowException_For_Large_Input()
        {
            var orchestration = new TestOrchestration
            {
                ShouldThrowException = true,
                Input = 100,
                MockDisplayResult = false,
            };
            DoTest(orchestration);
        }

        private static void DoTest(TestOrchestration orchestration)
        {
            //SETUP
            var mocker = new AutoMocker();

            //BEHAVIOUR
            mocker.Setup<IFibonacciUserInteraction, int>(x => x.GetOriginalInput())
                .Returns(orchestration.Input);

            if (orchestration.MockDisplayResult)
                mocker.Setup<IFibonacciUserInteraction>(x => x.DisplayResult(orchestration.Result));

            if (orchestration.ShouldThrowException)
                mocker.Setup<IFibonacciUserInteraction>(
                    x => x.HandleOverFlowException(It.IsAny<OverflowException>()));

            //TEST
            var subject = mocker.CreateInstance<Fibonacci>();

            if (orchestration.ShouldThrowException)
            {
                Action act = () => subject.Run();
                act.Should().Throw<OverflowException>();
            }
            else subject.Run();

            mocker.VerifyAll();
        }

        private class TestOrchestration
        {
            public int Input { get; set; }

            public bool MockDisplayResult { get; set; } = true;
            public long Result { get; set; } = 0;

            public bool ShouldThrowException { get; set; }

        }
    }
}