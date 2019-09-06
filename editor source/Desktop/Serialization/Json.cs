using Newtonsoft.Json;

namespace Desktop
{
	/// <summary>
	/// Wrapper around Newtonsoft JSON
	/// </summary>
	public static class Json
	{
		private static JsonSerializerSettings _settings;

		static Json()
		{
			_settings = new JsonSerializerSettings();
			_settings.TypeNameHandling = TypeNameHandling.Auto;
			_settings.Formatting = Formatting.Indented;
			_settings.NullValueHandling = NullValueHandling.Ignore;
			_settings.Converters.Add(new DateConverter());
		}

		public static string Serialize(object value)
		{
			string result = JsonConvert.SerializeObject(value, _settings);
			return result;
		}

		public static T Deserialize<T>(string json)
		{
			T obj = JsonConvert.DeserializeObject<T>(json, _settings);
			return obj;
		}
	}
}
