using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Notifications.Api.JsonConverters
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private readonly string _dateFormat;
        private readonly CultureInfo _culture;

        public DateOnlyJsonConverter(string dateFormat, CultureInfo culture)
        {
            _dateFormat = dateFormat;
            _culture = culture;
        }

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            return DateOnly.ParseExact(dateString, _dateFormat, _culture);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateFormat, _culture));
        }
    }
}
