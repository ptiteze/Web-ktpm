using Kitchen_MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace Kitchen_MVC.Singleton
{
	public class SingletonDataBridge
	{
		private static DataContext uniqueInstance;
		private static readonly object _lock = new object();
		private SingletonDataBridge() { 
			
		}
		public static DataContext GetInstance()
		{

					if (uniqueInstance == null)
					{
						uniqueInstance = new DataContext();
					}	
			return uniqueInstance;
		}
	}
}
