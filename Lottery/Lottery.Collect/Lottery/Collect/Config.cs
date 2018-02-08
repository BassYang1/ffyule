using System;

namespace Lottery.Collect
{
	public class Config
	{
		public static string DefaultUrl
		{
			get
			{
				return Config._DefaultUrl;
			}
			set
			{
				Config._DefaultUrl = value;
			}
		}

		public static string DefaultUrlYoule
		{
			get
			{
				return Config._DefaultUrlYoule;
			}
			set
			{
				Config._DefaultUrlYoule = value;
			}
		}

		private static string _DefaultUrl = "http://cloud.bmob.cn/5c86c74041efdeb5/hisStory-xml";

		private static string _DefaultUrlYoule = "http://cloud.bmob.cn/5c86c74041efdeb5/lottery{0}-xml";
	}
}
