using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace VideoWebAPI
{
	public class CameraController : ApiController
	{
		[HttpGet]
		public HttpResponseMessage FromImages()
		{
			var imageStream = new ImageStream();
			var response = Request.CreateResponse();
			response.Content = new PushStreamContent(imageStream.WriteToStream);

			response.Content.Headers.Remove("Content-Type");
			response.Content.Headers.TryAddWithoutValidation("Content-Type", "multipart/x-mixed-replace;boundary=" + imageStream.Boundary);

			return response;
		}
	}

	internal class ImageStream
	{
		public object Boundary { get; private set; } = "HintDesk";

		public async Task WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
		{
			byte[] newLine = Encoding.UTF8.GetBytes("\r\n");

			for (int i = 0; i < 15; i++)
			{
				foreach (var file in Directory.GetFiles(@"TestData\Images", "*.jpg"))
				{
					var fileInfo = new FileInfo(file);
					var header = $"--{Boundary}\r\nContent-Type: image/jpeg\r\nContent-Length: {fileInfo.Length}\r\n\r\n";
					var headerData = Encoding.UTF8.GetBytes(header);
					await outputStream.WriteAsync(headerData, 0, headerData.Length);
					await fileInfo.OpenRead().CopyToAsync(outputStream);
					await outputStream.WriteAsync(newLine, 0, newLine.Length);
					await Task.Delay(1000/3);
				}
			}
		}
	}
}