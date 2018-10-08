using System.Text.RegularExpressions;

namespace ResuppliesPerMglt.Helpers
{
    public class DurationParser : IDurationParser
    {
        readonly Regex durationRegex = new Regex(@"^(?<value>\d+)\s(?:(?<unit>second|minute|hour|day|week|month|year)s?)$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        public (long? Value, DurationUnits Units) Parse(string duration)
        {
            var result = durationRegex.Match(duration ?? string.Empty);

            if (!result.Success) return default;

            var valueStr = result.Groups["value"].Value;
            var unitsStr = result.Groups["unit"].Value;

            var value = TypeParseHelper.LongParseOrNull(valueStr);
            var units = TypeParseHelper.DurationUnitsParseOrDefault(unitsStr);
            if (!value.HasValue || units == default) return default;

            return (value, units);
        }

        public decimal? ConvertToHours(decimal? value, DurationUnits units)
        {
            // for increased precision the average days in a year is used, accounting for leap years
            const decimal averageDaysInYear = 365.25M;
            const decimal averageDaysInMonth = averageDaysInYear / 12;
            const decimal hoursInDay = 24;
            const decimal hoursInWeek = 7 * hoursInDay;
            const decimal hoursInMonth = hoursInDay * averageDaysInMonth;
            const decimal hoursInYear = hoursInDay * averageDaysInYear;

            switch (units)
            {
                case DurationUnits.Second:
                    return value / 3600M;

                case DurationUnits.Minute:
                    return value / 60M;

                case DurationUnits.Hour:
                    return value;

                case DurationUnits.Day:
                    return value * hoursInDay;

                case DurationUnits.Week:
                    return value * hoursInWeek;

                case DurationUnits.Month:
                    return value * hoursInMonth;
                
                case DurationUnits.Year:
                    return value * hoursInYear;
            }

            return null;
        }
    }
}
