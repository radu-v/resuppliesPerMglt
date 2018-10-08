using System;

namespace ResuppliesPerMglt.Helpers
{
    /// <summary>
    /// Contains static methods that aid in parsing a string to a specific type
    /// </summary>
    public static class TypeParseHelper
    {
        /// <summary>
        /// Tries to parse a string into a long value
        /// </summary>
        /// <param name="valueStr"></param>
        /// <returns>The parsed value or null on invalid input</returns>
        public static long? LongParseOrNull(string valueStr)
        {
            long? value = default;
            if (long.TryParse(valueStr, out var tmp)) value = tmp;

            return value;
        }

        /// <summary>
        /// Tries to parse a string into the DurationUnits enum
        /// </summary>
        /// <param name="unitsStr"></param>
        /// <returns>THe parsed value or the default value for DurationUnits enum (DurationUnits.Unknown)</returns>
        public static DurationUnits DurationUnitsParseOrDefault(string unitsStr)
        {
            DurationUnits units = default;
            if (Enum.TryParse<DurationUnits>(unitsStr, true, out var tmp)) units = tmp;

            return units;
        }
    }
}
