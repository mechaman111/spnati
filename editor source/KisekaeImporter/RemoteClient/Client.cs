using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KisekaeImporter.RemoteClient
{
	public class Client : IDisposable
	{
		private struct MessageHeader
		{
			public MessageType Type;
			public int Length;
		}

		private TcpClient _clientSocket;

		private static readonly byte[] PROTOCOL_HEADER = Encoding.UTF8.GetBytes("KKL ");

		private const int MaxRequestId = 2048;
		private static int _requestId = 1;

		private Dictionary<int, PendingRequest> _pendingRequests = new Dictionary<int, PendingRequest>();

		private DateTime LastHeartbeat;

		~Client()
		{
			Dispose();
		}

		public bool Connect()
		{
			_clientSocket = new TcpClient();
			_clientSocket.Connect("127.0.0.1", 8008);
			if (_clientSocket.Connected)
			{
				return true;
			}
			return false;
		}

		public bool Connected
		{
			get { return _clientSocket != null && _clientSocket.Connected; }
		}

		public void Disconnect()
		{
			if (Connected)
			{
				_clientSocket.Dispose();
				_clientSocket = null;
			}
		}

		public void Dispose()
		{
			Disconnect();
		}

		public async void Run()
		{
			while (Connected)
			{
				NetworkStream stream = _clientSocket.GetStream();
				ServerResponse response = await ReadMessage(stream);
				if (response.Type == MessageType.Heartbeat)
				{
					LastHeartbeat = DateTime.Now;
					continue;
				}

				int id = response.RequestId;
				PendingRequest request;
				if (_pendingRequests.TryGetValue(id, out request))
				{
					if (response.IsComplete)
					{
						request.SetResponse(response);
						_pendingRequests.Remove(id);
					}
				}
			}
		}

		private async Task<ServerResponse> ReadMessage(NetworkStream stream)
		{
			MessageHeader header = await ReadMessageHeader(stream);
			byte[] bytes = new byte[header.Length];
			await stream.ReadAsync(bytes, 0, header.Length);
			switch (header.Type)
			{
				case MessageType.Heartbeat:
					return new ServerResponse() { Type = MessageType.Heartbeat };
				case MessageType.CommandResponse:
					string payload = Encoding.UTF8.GetString(bytes);
					return new JSONResponse(payload);
				case MessageType.ImageData:
					int identifier = GetInt(bytes, 0);
					byte[] data = new byte[bytes.Length - 4];
					Array.Copy(bytes, 4, data, 0, data.Length);
					return new ImageResponse(identifier, data);
				default:
					return new ServerResponse();
			}
		}

		private async Task<MessageHeader> ReadMessageHeader(NetworkStream stream)
		{
			int i = 0;
			byte[] buffer = new byte[5];
			while (true)
			{
				await stream.ReadAsync(buffer, 0, 1);
				if (buffer[0] != PROTOCOL_HEADER[i])
				{
					i = 0;
					continue;
				}
				else
				{
					i++;
					if (i >= PROTOCOL_HEADER.Length)
					{
						break;
					}
				}
			}

			await stream.ReadAsync(buffer, 0, 5);
			int type;
			int length;
			type = BitConverter.ToChar(buffer, 0);
			length = GetInt(buffer, 1);
			return new MessageHeader()
			{
				Type = (MessageType)type,
				Length = length
			};
		}

		/// <summary>
		/// Reads 4 bytes into an int
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="start"></param>
		/// <returns></returns>
		private int GetInt(byte[] buffer, int start)
		{
			int value = 0;
			for (int i = 0; i < 4; i++)
			{
				int shift = (4 - 1 - i) * 8;
				value += (buffer[i + start] & 0x000000FF) << shift;
			}
			return value;
		}

		public async Task<ServerResponse> SendCommand(ServerRequest request)
		{
			int id = _requestId++;
			if (_requestId > MaxRequestId)
			{
				_requestId = 1;
			}
			NetworkStream stream = _clientSocket.GetStream();
			stream.ReadTimeout = 10000;

			string payload = request.Encode(id);
			byte[] lengthB = new byte[4] {
					(byte)(payload.Length >> 24),
					(byte)(payload.Length >> 16),
					(byte)(payload.Length >> 8),
					(byte)(payload.Length)
				};
			string length = Encoding.UTF8.GetString(lengthB);
			byte[] header = Encoding.UTF8.GetBytes("KKL \x01");
			byte[] bytes = new byte[header.Length + lengthB.Length + payload.Length];
			byte[] payloadB = Encoding.UTF8.GetBytes(payload);
			header.CopyTo(bytes, 0);
			lengthB.CopyTo(bytes, header.Length);
			payloadB.CopyTo(bytes, header.Length + lengthB.Length);

			PendingRequest pendedRequest = new PendingRequest(id);
			_pendingRequests[id] = pendedRequest;

			stream.Write(bytes, 0, bytes.Length);
			await stream.FlushAsync();

			ServerResponse response = await pendedRequest;

			return response;
		}
	}

	public class PendingRequest
	{
		public int RequestId;
		public TaskCompletionSource<ServerResponse> Response;

		public PendingRequest(int id)
		{
			RequestId = id;
			Response = new TaskCompletionSource<ServerResponse>();
		}

		public TaskAwaiter<ServerResponse> GetAwaiter()
		{
			return Response.Task.GetAwaiter();
		}

		public void SetResponse(ServerResponse response)
		{
			Response.SetResult(response);
		}
	}
}
