using DrawingNumberGenerator.Library.Models;
using NUnit;

namespace DrawingNumberGenerator.Tests
{
    [TestFixture]
    public class BeltTypeTests
    {
        private BeltType _beltType = null;

        [SetUp]
        public void Setup()
        {
            _beltType = BeltType.Test();
        }

        [Test]
        public void Create_isNotNull()
        {
            Assert.IsNotNull(_beltType);
        }

        [Test]
        public void Options_Count_Equals_105()
        {
            var result = _beltType.Options.Count();
            Assert.AreEqual(result, 105);
        }

        [Test]
        public void Code_Expected_Value()
        {
            var result = _beltType.Code;
            Assert.AreEqual(result, _beltType.Code);
        }

        [Test]
        public void Code_Expected_MaxCharacter()
        {
            var result = _beltType.MaxCharacters;
            Assert.AreEqual(result, _beltType.Code.Count());
        }

        [Test]
        public void ToString_Returns_ExpectedCode()
        {
            var result = _beltType.MaxCharacters;
            Assert.AreEqual(_beltType.ToString(), _beltType.Code);
        }

        [TestCase("SM605", "KVP")]
        [TestCase("870", "HabasitLink")]
        public void GetBeltSeriesType_Returns_ExpectedType(string code, string expectedType)
        {
            var result = _beltType.GetBeltSeriesType(code);
            Assert.AreEqual(result, expectedType);
        }
    }
}