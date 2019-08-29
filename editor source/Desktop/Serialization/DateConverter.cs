using Newtonsoft.Json;
using System;

namespace Desktop
{
	public class DateConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			if (reader.Value == null)
				return dt;
			double timestamp;
			double.TryParse(reader.Value.ToString(), out timestamp);
			dt = dt.AddMilliseconds((long)timestamp);
			return dt;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			DateTime dt = (DateTime)value;
			writer.WriteValue((long)(dt.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds));
		}
	}
}
