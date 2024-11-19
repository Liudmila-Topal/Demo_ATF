using ConsoleApp2.Configuration;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Serilog;

namespace ConsoleApp2.StepDefinition
{
    [Binding]
    public class ExampleFeatureSteps
    {
        private int _firstNumber;
        private int _secondNumber;
        private int _result;
        
        // LoggerConfig.InitializeLogger(); // Инициализация Serilog

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int number)
        {
            if (_firstNumber == 0)
                _firstNumber = number;
            else
                _secondNumber = number;
            LoggerConfig.InitializeLogger(); // Инициализация Serilog
            Log.Information("First step");
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            _result = _firstNumber + _secondNumber;
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int expectedResult)
        {
            Assert.AreEqual(expectedResult, _result);
        }
    }
}