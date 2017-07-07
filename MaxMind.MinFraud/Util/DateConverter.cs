#region
using Newtonsoft.Json.Converters;
#endregion

namespace MaxMind.MinFraud.Util
{
    internal class DateConverter : IsoDateTimeConverter
    {
        public DateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}