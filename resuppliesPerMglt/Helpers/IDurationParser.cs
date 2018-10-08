namespace ResuppliesPerMglt.Helpers
{
    /// <summary>
    /// Defines a duration parser
    /// </summary>
    public interface IDurationParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="duration">Value to be parsed. Must be in a format of: "x unit", where "x" is a numeric amount of "unit" units</param>
        /// <returns>A tuple of the amount and duration unit. The Value will be null and Units will be the default value of the DurationUnits enum in case of parsing failure</returns>
        (long? Value, DurationUnits Units) Parse(string duration);

        /// <summary>
        /// Helper method for converting a duration value to hours
        /// </summary>
        /// <param name="value"></param>
        /// <param name="units"></param>
        /// <returns>Amount of hours or null in case of invalid input</returns>
        decimal? ConvertToHours(decimal? value, DurationUnits units);
    }
}