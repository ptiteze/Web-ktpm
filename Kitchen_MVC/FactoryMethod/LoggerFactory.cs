namespace Kitchen_MVC.FactoryMethod
{
	public static class LoggerFactory
	{
		private static readonly ILoggerFactory _loggerFactory;

		static LoggerFactory()
		{
			_loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
			{
				builder.AddConsole();
				builder.AddDebug();
				// Thêm các provider khác nếu cần
			});
		}

		public static ILogger CreateLogger<T>()
		{
			return _loggerFactory.CreateLogger<T>();
		}	
	}
}
