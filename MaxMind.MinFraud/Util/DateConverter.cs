#region
using Newtonsoft.Json.Converters;
#endregion

namespace MaxMind.MinFraud.Util
{
    internal class DateConverter : IsoDateTimeConverter
    {
        public DateConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}