using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjetoChat.Utils.Configurations
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (DateOnly.TryParseExact(value, Format, null, System.Globalization.DateTimeStyles.None, out var date))
            {
                return date;
            }

            throw new JsonException($"Formato de data inválido. Esperado: {Format}");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }

}
