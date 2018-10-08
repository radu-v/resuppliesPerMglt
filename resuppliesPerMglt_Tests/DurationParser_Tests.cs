using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResuppliesPerMglt.Helpers;

namespace ResuppliesPerMglt_Tests
{
    [TestClass]
    public class DurationParser_Tests
    {
        const decimal AverageDaysInYear = 365.25M;
        const decimal AverageDaysInMonth = AverageDaysInYear / 12;

        DurationParser durationParser;

        [TestInitialize]
        public void TestInitialize()
        {
            durationParser = new DurationParser();
        }

        [TestMethod]
        public void Parse_Should_ReturnDefault_OnNullInput()
        {
            Assert.AreEqual(default, durationParser.Parse(null));
        }

        [TestMethod]
        public void Parse_Should_ReturnDefault_OnEmptyInput()
        {
            Assert.AreEqual(default, durationParser.Parse(string.Empty));
        }

        [TestMethod]
        public void Parse_Should_ReturnDefault_OnInvalidInput()
        {
            Assert.AreEqual(default, durationParser.Parse("Mmm. Lost a planet, Master Obi-Wan has. How embarrassing."));
        }

        [TestMethod]
        public void Parse_Should_ReturnDefault_OnInvalidValue()
        {
            Assert.AreEqual(default, durationParser.Parse("x months"));
        }

        [TestMethod]
        public void Parse_Should_ReturnDefault_OnInvalidUnit()
        {
            Assert.AreEqual(default, durationParser.Parse("2 Jarjars"));
        }

        [TestMethod]
        public void Parse_Should_Pass_OnValidInput()
        {
            var random = new Random(DateTime.UtcNow.Millisecond);

            foreach (DurationUnits val in Enum.GetValues(typeof(DurationUnits)))
            {
                long? duration = random.Next();

                var expected = val == DurationUnits.Unknown ? default : (duration, val);
                var actual = durationParser.Parse($"{duration} {val}");

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void ConvertToHours_Should_ReturnNull_OnNullValue()
        {
            Assert.IsNull(durationParser.ConvertToHours(null, DurationUnits.Day));
        }

        [TestMethod]
        public void ConvertToHours_Should_ReturnNull_OnUnknownUnit()
        {
            Assert.IsNull(durationParser.ConvertToHours(new Random().Next(), DurationUnits.Unknown));
        }

        [TestMethod]
        public void ConvertToHours_Should_ReturnCorrectValue_Second()
        {
            decimal unitValue = new Random().Next();
            var expected = unitValue / 3600M;
            var actual = durationParser.ConvertToHours(unitValue, DurationUnits.Second);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertToHours_Should_ReturnCorrectValue_Minute()
        {
            decimal unitValue = new Random().Next();
            var expected = unitValue / 60M;
            var actual = durationParser.ConvertToHours(unitValue, DurationUnits.Minute);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertToHours_Should_ReturnCorrectValue_Hour()
        {
            var expected = (decimal)new Random().Next();
            var actual = durationParser.ConvertToHours(expected, DurationUnits.Hour);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertToHours_Should_ReturnCorrectValue_Day()
        {
            decimal unitValue = new Random().Next();
            var expected = unitValue * 24M;
            var actual = durationParser.ConvertToHours(unitValue, DurationUnits.Day);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertToHours_Should_ReturnCorrectValue_Week()
        {
            decimal unitValue = new Random().Next();
            var expected = unitValue * 24 * 7;
            var actual = durationParser.ConvertToHours(unitValue, DurationUnits.Week);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertToHours_Should_ReturnCorrectValue_Month()
        {
            decimal unitValue = new Random().Next();
            var expected = unitValue * 24 * AverageDaysInMonth;
            var actual = durationParser.ConvertToHours(unitValue, DurationUnits.Month);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertToHours_Should_ReturnCorrectValue_Year()
        {
            decimal unitValue = new Random().Next();
            var expected = unitValue * 24 * AverageDaysInYear;
            var actual = durationParser.ConvertToHours(unitValue, DurationUnits.Year);

            Assert.AreEqual(expected, actual);
        }
    }
}
