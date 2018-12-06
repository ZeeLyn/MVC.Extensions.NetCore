using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVC.Extensions.Converters
{
	public class LongToStringConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value.ToString());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jt = JToken.ReadFrom(reader);
			return jt.Value<long>();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(long) == objectType;
		}
	}
}
