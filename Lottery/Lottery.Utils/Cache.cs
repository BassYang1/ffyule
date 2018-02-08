using System;
using System.Web;
using System.Web.Caching;

namespace Lottery.Utils
{
	public static class Cache
	{
		public static void Insert(string strCacheName, string strValue, int iExpires, int priority)
		{
			TimeSpan value = new TimeSpan(0, 0, iExpires);
			CacheItemPriority priority2;
			switch (priority)
			{
			case 1:
				priority2 = CacheItemPriority.NotRemovable;
				break;
			case 2:
				priority2 = CacheItemPriority.High;
				break;
			case 3:
				priority2 = CacheItemPriority.AboveNormal;
				break;
			case 4:
				priority2 = CacheItemPriority.Normal;
				break;
			case 5:
				priority2 = CacheItemPriority.BelowNormal;
				break;
			case 6:
				priority2 = CacheItemPriority.Low;
				break;
			default:
				priority2 = CacheItemPriority.Normal;
				break;
			}
			HttpContext.Current.Cache.Insert(strCacheName, strValue, null, DateTime.Now.Add(value), System.Web.Caching.Cache.NoSlidingExpiration, priority2, null);
		}

		public static string Get(string strCacheName)
		{
			return HttpContext.Current.Cache[strCacheName].ToString();
		}

		public static void Del(string strCacheName)
		{
			HttpContext.Current.Cache.Remove(strCacheName);
		}
	}
}
