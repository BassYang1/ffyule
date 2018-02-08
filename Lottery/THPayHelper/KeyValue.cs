using System;

namespace THPayHelper
{
	public class KeyValue
	{
		public KeyValue(string k, string v)
		{
			this.key = k;
			this.val = v;
		}

		public int compare(KeyValue other)
		{
			return this.key.CompareTo(other.key);
		}

		public string getKey()
		{
			return this.key;
		}

		public string getVal()
		{
			return this.val;
		}

		private string key;

		private string val;
	}
}
