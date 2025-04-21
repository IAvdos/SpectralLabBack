using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
/*
public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
	private const string DateFormat = "dd.MM.yyyy";

	public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		// Пытаемся распарсить дату в формате "dd.MM.yyyy"
		if (DateOnly.TryParseExact(reader.GetString(), DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
		{
			return date;
		}
		throw new JsonException($"Неверный формат даты. Ожидается: {DateFormat}.");
	}

	public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString());
	}
}
*/