using ConsoleApp2.Configuration;
using ConsoleApp2.Hooks;
using ConsoleApp2.Providers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Serilog;

namespace ConsoleApp2.StepDefinition
{
    [Binding]
    public class CalculatorSteps
    {
        private int _firstNumber;
        private int _secondNumber;
        private int _result;

        private ILogger Logger => LoggerProvider.Logger;

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int number)
        {
            if (_firstNumber == 0)
                _firstNumber = number;
            else
                _secondNumber = number;
            Logger.Information("I have entered into the calculator the number " + number);
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            _result = _firstNumber + _secondNumber;
            Logger.Information("I pressed add");
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int expectedResult)
        {
            Assert.AreEqual(expectedResult, _result);
            Logger.Information("the result should be " + _result);
        }
    }
}