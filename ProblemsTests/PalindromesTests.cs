using Moq.AutoMock;
using Problems.UserInteraction;
using Problems.Problems;

namespace Tests.ProblemsTests
{
    //https://itnext.io/how-to-fully-cover-net-c-console-application-with-unit-tests-446927a4a793
    [TestClass]
    public class PalindromesTests
    {
        [TestMethod]
        public void TestRun_Should_Detect_Palindrome()
        {
            var mocker = new AutoMocker();

            mocker.Setup<IPalindromesUserInteraction, string>(x => x.GetOriginalInput())
                .Returns("palindromemordnilap");

            mocker.Setup<IPalindromesUserInteraction>(x => x.DisplayResult(true));

            var subject = mocker.CreateInstance<Palindromes>();
            subject.Run();

            mocker.VerifyAll();
        }

        [TestMethod]
        public void TestRun_Should_Detect_Palindrome_Ignoring_Capitalisation_And_Non_Alphanumeric_Characters()
        {
            var mocker = new AutoMocker();

            mocker.Setup<IPalindromesUserInteraction, string>(x => x.GetOriginalInput())
                .Returns("pali n#d@rOMemorDnilap");

            mocker.Setup<IPalindromesUserInteraction>(x => x.DisplayResult(true));

            var subject = mocker.CreateInstance<Palindromes>();
            subject.Run();

            mocker.VerifyAll();
        }

        [TestMethod]
        public void TestRun_Should_Not_Detect_Palindrome()
        {
            var mocker = new AutoMocker();

            mocker.Setup<IPalindromesUserInteraction, string>(x => x.GetOriginalInput())
                .Returns("not a palindrome");

            mocker.Setup<IPalindromesUserInteraction>(x => x.DisplayResult(false));

            var subject = mocker.CreateInstance<Palindromes>();
            subject.Run();

            mocker.VerifyAll();
        }
    }
}