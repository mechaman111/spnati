using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace KisekaeImporter.RemoteClient
{
	public enum MessageType
	{
		Command = 0x01,
		CommandResponse = 0x02,
		ImageData = 0x03,
		Heartbeat = 0x04,
	}

	public class ServerRequest
	{
		private Dictionary<string, string> _payload;

		public ServerRequest(string type, params string[] fields)
		{
			_payload = new Dictionary<string, string>();
			_payload["type"] = type;
			for (int i = 0; i < fields.Length; i+= 2)
			{
				if (i < fields.Length - 1)
				{
					string key = fields[i];
					string value = fields[i + 1];
					_payload[key] = value;
				}
			}
		}

		/// <summary>
		/// Encodes the payload as JSON
		/// </summary>
		/// <returns></returns>
		public string Encode(int id)
		{
			_payload["id"] = id.ToString();
			List<string> fields = new List<string>();
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			foreach (KeyValuePair<string, string> kvp in _payload)
			{
				int numValue;
				if (int.TryParse(kvp.Value, out numValue))
				{
					fields.Add($"\"{kvp.Key}\": {kvp.Value}");
				}
				else
				{
					fields.Add($"\"{kvp.Key}\": \"{kvp.Value}\"");
				}
			}
			sb.Append(string.Join(", ", fields));
			sb.Append("}");
			return sb.ToString();
			//return Encoding.UTF8.GetBytes(sb.ToString());
		}
	}

	public class ServerResponse
	{
		public MessageType Type;
		public int RequestId;
		public bool IsSuccess;
		public bool IsComplete;
	}

	public class JSONResponse : ServerResponse
	{
		public Dictionary<string, object> Data;

		public JSONResponse(string payload)
		{
			Type = MessageType.CommandResponse;
			Data = JsonConvert.DeserializeObject<Dictionary<string, object>>(payload);
			if (Data.ContainsKey("id"))
			{
				RequestId = int.Parse(Data["id"].ToString());
			}

			object status;
			if (Data.TryGetValue("status", out status))
			{
				string statusValue = status.ToString();
				if (statusValue != "in_progress")
				{
					IsComplete = true;
				}
				IsSuccess = statusValue == "done";
			}
		}
	}

	public class ImageResponse : ServerResponse
	{
		public Bitmap Image { get; private set; }

		public ImageResponse(int id, byte[] data)
		{
			Type = MessageType.ImageData;
			RequestId = id;
			MemoryStream stream = new MemoryStream(data, false);
			Image = new Bitmap(stream);
			IsSuccess = true;
			IsComplete = true;
		}
	}
}
