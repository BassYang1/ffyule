using System;
using System.Collections.Generic;
using System.Text;

namespace THPayHelper
{
	public class KeyValues
	{
		public List<KeyValue> items()
		{
			return this.keyValues;
		}

		public void add(KeyValue kv)
		{
			if (kv != null && !string.IsNullOrEmpty(kv.getVal()))
			{
				this.keyValues.Add(kv);
			}
		}

		public string sign(string key, string inputCharset)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.keyValues.Sort(new KeyValues.KeyValueComparer());
			foreach (KeyValue current in this.keyValues)
			{
				URLUtils.appendParam(stringBuilder, current.getKey(), current.getVal());
			}
			URLUtils.appendParam(stringBuilder, AppConstants.KEY, key);
			string text = stringBuilder.ToString();
			text = text.Substring(1, text.Length - 1);
			return MD5Encoder.encode(text, inputCharset);
		}

		private List<KeyValue> keyValues = new List<KeyValue>();

		private class KeyValueComparer : IComparer<KeyValue>
		{
			public int Compare(KeyValue l, KeyValue r)
			{
				return l.compare(r);
			}
		}
	}
}
