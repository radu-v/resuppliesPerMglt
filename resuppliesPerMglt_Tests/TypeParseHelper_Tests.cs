using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResuppliesPerMglt.Helpers;

namespace ResuppliesPerMglt_Tests
{
    [TestClass]
    public class TypeParseHelper_Tests
    {
        [TestMethod]
        public void IntParseOrNull_Should_ReturnNull_OnNullInput()
        {
            Assert.IsNull(TypeParseHelper.LongParseOrNull(null));
        }

        [TestMethod]
        public void IntParseOrNull_Should_ReturnNull_OnEmptyInput()
        {
            Assert.IsNull(TypeParseHelper.LongParseOrNull(string.Empty));
        }

        [TestMethod]
        public void IntParseOrNull_Should_ReturnNull_OnNonNumericInput()
        {
            Assert.IsNull(TypeParseHelper.LongParseOrNull("alskdjfh"));
        }

        [TestMethod]
        public void IntParseOrNull_Should_ReturnNull_OnMixedInput()
        {
            Assert.IsNull(TypeParseHelper.LongParseOrNull("1 ab ;13"));
        }

        [TestMethod]
        public void IntParseOrNull_Should_ReturnNull_OnTooBigNumberInput()
        {
            const string input = "18446744073709551616";

            Assert.IsNull(TypeParseHelper.LongParseOrNull(input));
        }

        [TestMethod]
        public void IntParseOrNull_Should_ReturnCorrectNumber_OnValidInput()
        {
            const string input = "1234";
            const long expected = 1234;

            Assert.AreEqual(expected, TypeParseHelper.LongParseOrNull(input));
        }

        [TestMethod]
        public void IntParseOrNull_Should_ReturnCorrectNumber_OnInt64Input()
        {
            const string input = "140737488355328";
            const long expected = 140737488355328;

            Assert.AreEqual(expected, TypeParseHelper.LongParseOrNull(input));
        }

        [TestMethod]
        public void DurationUnitsParseOrDefault_Should_ReturnDefault_OnNullInput()
        {
            Assert.AreEqual(default, TypeParseHelper.DurationUnitsParseOrDefault(null));
        }
        
        [TestMethod]
        public void DurationUnitsParseOrDefault_Should_ReturnDefault_OnEmptyInput()
        {
            Assert.AreEqual(default, TypeParseHelper.DurationUnitsParseOrDefault(string.Empty));
        }
        
        [TestMethod]
        public void DurationUnitsParseOrDefault_Should_ReturnDefault_OnInvalidInput()
        {
            Assert.AreEqual(default, TypeParseHelper.DurationUnitsParseOrDefault("2 years"));
        }
        
        [TestMethod]
        public void DurationUnitsParseOrDefault_Should_ReturnCorrectValue_OnValidInput()
        {
            foreach (DurationUnits val in Enum.GetValues(typeof(DurationUnits)))
            {
                Assert.AreEqual(val, TypeParseHelper.DurationUnitsParseOrDefault(val.ToString()));
            }
        }
    }
}
