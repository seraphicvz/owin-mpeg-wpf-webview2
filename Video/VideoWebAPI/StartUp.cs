using System.Web.Http;
using Owin;

namespace VideoWebAPI
{
	public class Startup
	{
		// This code configures Web API. The Startup class is specified as a type
		// parameter in the WebApp.Start method.
		public void Configuration(IAppBuilder appBuilder)
		{
			// Harden security 
			appBuilder.Use((context, next) =>
			{
				context.Response.Headers.Add("Server", new[] { string.Empty });
				return next();
			});

			// Configure Web API for self-host.
			var config = new HttpConfiguration();
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			appBuilder.UseWebApi(config);

			//config.Formatters.Remove(config.Formatters.XmlFormatter);
			//config.Formatters.Add(config.Formatters.JsonFormatter);

		}
	}
}