using System;
using System.Collections.Generic;

namespace Lottery.Utils
{
	public static class dicHelp
	{
		public static void Order(ref Dictionary<string, string> dic, int type)
		{
			if (dic == null)
			{
				return;
			}
			if (dic.Count < 1)
			{
				return;
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>(dic);
			switch (type)
			{
			case 0:
				list.Sort((KeyValuePair<string, string> s1, KeyValuePair<string, string> s2) => s1.Value.CompareTo(s2.Value));
				break;
			case 1:
				list.Sort((KeyValuePair<string, string> s1, KeyValuePair<string, string> s2) => s2.Value.CompareTo(s1.Value));
				break;
			case 2:
				list.Sort((KeyValuePair<string, string> s1, KeyValuePair<string, string> s2) => s1.Key.CompareTo(s2.Key));
				break;
			default:
				list.Sort((KeyValuePair<string, string> s1, KeyValuePair<string, string> s2) => s2.Key.CompareTo(s1.Key));
				break;
			}
			dic.Clear();
			foreach (KeyValuePair<string, string> current in list)
			{
				dic.Add(current.Key, current.Value);
			}
		}

		public static void Order(ref Dictionary<string, int> dic, int type)
		{
			if (dic == null)
			{
				return;
			}
			if (dic.Count < 1)
			{
				return;
			}
			List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(dic);
			switch (type)
			{
			case 0:
				list.Sort((KeyValuePair<string, int> s1, KeyValuePair<string, int> s2) => s1.Value.CompareTo(s2.Value));
				break;
			case 1:
				list.Sort((KeyValuePair<string, int> s1, KeyValuePair<string, int> s2) => s2.Value.CompareTo(s1.Value));
				break;
			case 2:
				list.Sort((KeyValuePair<string, int> s1, KeyValuePair<string, int> s2) => s1.Key.CompareTo(s2.Key));
				break;
			default:
				list.Sort((KeyValuePair<string, int> s1, KeyValuePair<string, int> s2) => s2.Key.CompareTo(s1.Key));
				break;
			}
			dic.Clear();
			foreach (KeyValuePair<string, int> current in list)
			{
				dic.Add(current.Key, current.Value);
			}
		}
	}
}
