using System;
using Microsoft.Owin.Hosting;

namespace VideoWebAPI
{
	class Program
	{
		static void Main(string[] args)
		{
			var port = 9012; 

			var baseAddress = $"http://+:{port}";

			// Start OWIN host
			WebApp.Start<Startup>(url: baseAddress);
			Console.WriteLine("Web API started...");
			Console.ReadLine();
		}
	}
}
