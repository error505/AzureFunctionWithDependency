namespace FunctionWithDependency
{
	public class GetPageService : IGetPageService
	{
		public string GetPage()
		{
			return "https://github.com";
		}
	}
}
