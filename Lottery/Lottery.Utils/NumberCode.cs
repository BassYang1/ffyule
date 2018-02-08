using System;
using System.Collections;
using System.Collections.Generic;

namespace Lottery.Utils
{
	public class NumberCode
	{
		public static string[] CreateCode20()
		{
			string[] randomNumNoRepeat = NumberCode.getRandomNumNoRepeat(20, 1, 99);
			NumberCode.SortByCount(randomNumNoRepeat);
			return randomNumNoRepeat;
		}

		public static string CreateCodeNew(int codeLength)
		{
			string text = "";
			Random random = new Random((int)DateTime.Now.Ticks);
			for (int i = 0; i <= codeLength - 1; i++)
			{
				int num = random.Next(100, 199);
				text += num % 100;
				if (i != codeLength - 1)
				{
					text += ",";
				}
			}
			return text;
		}

		public static string CreateCode(int codeLength)
		{
			string text = "";
			Random random = new Random();
			for (int i = 0; i < codeLength + 5; i++)
			{
				text += random.Next().ToString();
				if (text.Length > 10)
				{
					break;
				}
			}
			string text2 = text.Substring(random.Next(0, 4), codeLength);
			for (int i = 0; i < text2.Length; i++)
			{
				if (i == 0)
				{
					text = text2[i].ToString();
				}
				else
				{
					text = text + "," + text2[i];
				}
			}
			return text;
		}

		public static string CreateCode11X5(int codeLength)
		{
			string text = "";
			Hashtable hashtable = new Hashtable();
			Random random = new Random();
			int num = 5;
			int num2 = 0;
			while (hashtable.Count < num)
			{
				int num3 = random.Next(12);
				if (!hashtable.ContainsValue(num3) && num3 != 0)
				{
					hashtable.Add(num3, num3);
				}
				num2++;
			}
			foreach (DictionaryEntry dictionaryEntry in hashtable)
			{
				if (dictionaryEntry.Value.ToString().Length < 2)
				{
					text = text + "0" + dictionaryEntry.Value.ToString() + ",";
				}
				else
				{
					text = text + dictionaryEntry.Value.ToString() + ",";
				}
			}
			text = text.Substring(0, text.Length - 1);
			return text;
		}

		public static string CreateCode20(int codeLength)
		{
			string text = "";
			Hashtable hashtable = new Hashtable();
			Random random = new Random();
			int num = 20;
			int num2 = 0;
			while (hashtable.Count < num)
			{
				int num3 = random.Next(99);
				if (!hashtable.ContainsValue(num3) && num3 != 0)
				{
					hashtable.Add(num3, num3);
				}
				num2++;
			}
			foreach (DictionaryEntry dictionaryEntry in hashtable)
			{
				if (dictionaryEntry.Value.ToString().Length < 2)
				{
					text = text + "0" + dictionaryEntry.Value.ToString() + ",";
				}
				else
				{
					text = text + dictionaryEntry.Value.ToString() + ",";
				}
			}
			text = text.Substring(0, text.Length - 1);
			return text;
		}

		public static string CreateCodePk10(int codeLength)
		{
			string[] array = new string[]
			{
				"01",
				"02",
				"03",
				"04",
				"05",
				"06",
				"07",
				"08",
				"09",
				"10"
			};
			List<string> list = new List<string>();
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(array[i]);
			}
			list = NumberCode.ListRandom(list);
			return string.Join(",", list.ToArray());
		}

		private static List<string> ListRandom(List<string> myList)
		{
			Random random = new Random();
			for (int i = 0; i < myList.Count; i++)
			{
				int num = random.Next(0, myList.Count - 1);
				if (num != i)
				{
					string value = myList[i];
					myList[i] = myList[num];
					myList[num] = value;
				}
			}
			return myList;
		}

		public static string CreateCodeDN(int codeLength)
		{
			string[] array = new string[]
			{
				"01",
				"02",
				"03",
				"04",
				"05",
				"06",
				"07",
				"08",
				"09",
				"10",
				"01",
				"02",
				"03",
				"04",
				"05",
				"06",
				"07",
				"08",
				"09",
				"10",
				"01",
				"02",
				"03",
				"04",
				"05",
				"06",
				"07",
				"08",
				"09",
				"10",
				"01",
				"02",
				"03",
				"04",
				"05",
				"06",
				"07",
				"08",
				"09",
				"10"
			};
			string text = "";
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			Random random = new Random();
			int num = 5;
			int num2 = 0;
			while (hashtable.Count < num)
			{
				int num3 = random.Next(0, 39);
				if (!hashtable2.ContainsValue(num3) && num3 != 0)
				{
					hashtable.Add(num3, array[num3]);
					hashtable2.Add(num3, num3);
				}
				num2++;
			}
			foreach (DictionaryEntry dictionaryEntry in hashtable)
			{
				if (dictionaryEntry.Value.ToString().Length < 2)
				{
					text = text + "0" + dictionaryEntry.Value.ToString() + ",";
				}
				else
				{
					text = text + dictionaryEntry.Value.ToString() + ",";
				}
			}
			text = text.Substring(0, text.Length - 1);
			return text;
		}

		public int[] getRandomNum(int num, int minValue, int maxValue)
		{
			Random random = new Random((int)DateTime.Now.Ticks);
			int[] array = new int[num];
			for (int i = 0; i <= num - 1; i++)
			{
				int num2 = random.Next(minValue, maxValue);
				array[i] = num2;
			}
			return array;
		}

		public static string[] getRandomNumNoRepeat(int num, int minValue, int maxValue)
		{
			Random random = new Random((int)DateTime.Now.Ticks);
			int[] array = new int[num];
			string[] array2 = new string[num];
			for (int i = 0; i <= num - 1; i++)
			{
				int tmp = random.Next(minValue, maxValue);
				int num2 = NumberCode.getNum(array, tmp, minValue, maxValue, random);
				array[i] = num2;
				if (num2 < 10)
				{
					array2[i] = "0" + num2;
				}
				else
				{
					array2[i] = string.Concat(num2);
				}
			}
			return array2;
		}

		public static int getNum(int[] arrNum, int tmp, int minValue, int maxValue, Random ra)
		{
			for (int i = 0; i <= arrNum.Length - 1; i++)
			{
				if (arrNum[i] == tmp)
				{
					tmp = ra.Next(minValue, maxValue);
					NumberCode.getNum(arrNum, tmp, minValue, maxValue, ra);
				}
			}
			return tmp;
		}

		public static void SortByCount(string[] source)
		{
			Comparison<string> comparison = new Comparison<string>(NumberCode.function);
			Array.Sort<string>(source, comparison);
		}

		private static int function(string s1, string s2)
		{
			if (Convert.ToInt32(s1) - Convert.ToInt32(s2) != 0)
			{
				return Convert.ToInt32(s1) - Convert.ToInt32(s2);
			}
			return string.Compare(s1, s2);
		}
	}
}
