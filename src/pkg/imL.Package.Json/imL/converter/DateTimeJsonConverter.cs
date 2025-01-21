#if (NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)

using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace imL.Package.Json
{
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        readonly string _FORMAT;

        public DateTimeJsonConverter(string _format)
        {
            _FORMAT = _format;
        }

        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                DateTime.ParseExact(reader.GetString(),
                    _FORMAT, CultureInfo.InvariantCulture);

        public override void Write(
            Utf8JsonWriter writer,
            DateTime dateTimeValue,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(dateTimeValue.ToString(
                    _FORMAT, CultureInfo.InvariantCulture));
    }
}

#endif
