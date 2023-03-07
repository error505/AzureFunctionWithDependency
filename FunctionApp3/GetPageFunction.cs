using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace FunctionWithDependency
{
	public class GetPageFunction
	{
		private readonly HttpClient _httpClient;
		private readonly IGetPageService _getPageservice;

		public GetPageFunction(IHttpClientFactory httpClientFactory, IGetPageService getPageservice)
		{
			_httpClient = httpClientFactory.CreateClient();
			_getPageservice = getPageservice;
		}

		[FunctionName("GetPageFunction")]
		public async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
			ILogger log)
		{
			var url = _getPageservice.GetPage();
			var response = await _httpClient.GetAsync(url);

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return new RedirectResult(url);
			}
			else
			{
				return new NotFoundResult();
			}
		}
	}
}
