using DNG.Library.Models;
using NUnit;

namespace DrawingNumberGenerator.Tests
{
    [TestFixture]
    public class BeltSeriesTests
    {
        private BeltSeries _series;

        [SetUp]
        public void Setup()
        {
            _series = BeltSeries.Test();
        }

        [Test]
        public void Create_isNotNull()
        {
            Assert.IsNotNull(_series);
        }

        [Test]
        public void Options_Count_Equals_105()
        {
            var result = BeltSeries.Options.Count;
            Assert.That(result, Is.EqualTo(105));
        }

        [Test]
        public void Code_Expected_Value()
        {
            var result = _series.Code;
            Assert.That(_series.Code, Is.EqualTo(result));
        }

        [Test]
        public void Code_Expected_MaxCharacter()
        {
            var result = _series.MaxCharacters;
            Assert.That(_series.Code.Length, Is.EqualTo(result));
        }

        [Test]
        public void ToString_Returns_ExpectedCode()
        {
            Assert.That(_series.Code, Is.EqualTo(_series.ToString()));
        }

        [TestCase("SM605", "KVP")]
        [TestCase("870", "HabasitLink")]
        public void GetBeltSeriesType_Returns_ExpectedType(string code, string expectedType)
        {
            var result = BeltSeries.GetBeltSeriesType(code);
            Assert.That(expectedType, Is.EqualTo(result));
        }
    }
}