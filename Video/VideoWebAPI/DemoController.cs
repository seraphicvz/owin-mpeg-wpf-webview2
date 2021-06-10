using System.Collections.Generic;
using System.Web.Http;

namespace VideoWebAPI
{
	public class DemoController : ApiController
	{
		// GET api/demo
		public IEnumerable<string> Get()
		{
			return new[] { "hi", "there" };
		}
	}
}