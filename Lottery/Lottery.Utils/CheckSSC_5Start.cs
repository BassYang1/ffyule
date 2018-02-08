using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
	public static class CheckSSC_5Start
	{
		public static int P_5FS(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = CheckNumber.Split(new char[]
			{
				','
			});
			if (array2.Length >= 5 && array2[0].IndexOf(array[0]) != -1 && array2[1].IndexOf(array[1]) != -1 && array2[2].IndexOf(array[2]) != -1 && array2[3].IndexOf(array[3]) != -1 && array2[4].IndexOf(array[4]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_5DS(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = CheckNumber.Split(new char[]
			{
				','
			});
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i];
			}
			for (int j = 0; j < array2.Length; j++)
			{
				if (text == array2[j].Replace(",", ""))
				{
					num++;
				}
			}
			return num;
		}

		public static int P_5ZX120(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = CheckNumber.Split(new char[]
			{
				'_'
			});
			string[] array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < array.Length; j++)
				{
					for (int k = 0; k < array.Length; k++)
					{
						for (int l = 0; l < array.Length; l++)
						{
							for (int m = 0; m < array.Length; m++)
							{
								if (i != j && j != k && i != k && k != l && l != m)
								{
									string[] array3 = new string[]
									{
										string.Concat(new string[]
										{
											array[i],
											",",
											array[j],
											",",
											array[k],
											",",
											array[l],
											",",
											array[m]
										})
									};
									if (array3[0].Replace(",", "").Distinct<char>().Count<char>() == 5 && array2[0].IndexOf(array[i]) != -1 && array2[1].IndexOf(array[j]) != -1 && array2[2].IndexOf(array[k]) != -1 && array2[3].IndexOf(array[l]) != -1 && array2[4].IndexOf(array[m]) != -1)
									{
										num++;
									}
								}
							}
						}
					}
				}
			}
			return num;
		}

		public static int P_5ZX60(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = CheckNumber.Split(new char[]
			{
				','
			});
			string[] array2 = array[0].Split(new char[]
			{
				'_'
			});
			bool flag = false;
			string item = "";
			if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 4)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					if (Regex.Matches(LotteryNumber, array2[i]).Count == 2)
					{
						flag = true;
						item = array2[i];
					}
				}
			}
			if (flag)
			{
				string[] source = LotteryNumber.Split(new char[]
				{
					','
				});
				List<string> list = source.ToList<string>();
				list.Remove(item);
				list.Remove(item);
				if (array[1].IndexOf(list[0]) != -1 && array[1].IndexOf(list[1]) != -1 && array[1].IndexOf(list[2]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_5ZX30(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = CheckNumber.Split(new char[]
			{
				','
			});
			string[] array2 = array[0].Split(new char[]
			{
				'_'
			});
			bool flag = false;
			string item = "";
			string item2 = "";
			if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 3)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					if (Regex.Matches(LotteryNumber, array2[i]).Count == 2)
					{
						for (int j = 0; j < array2.Length; j++)
						{
							if (Regex.Matches(LotteryNumber, array2[j]).Count == 2 && i != j)
							{
								flag = true;
								item = array2[i];
								item2 = array2[j];
							}
						}
					}
				}
			}
			if (flag)
			{
				string[] source = LotteryNumber.Split(new char[]
				{
					','
				});
				List<string> list = source.ToList<string>();
				list.Remove(item);
				list.Remove(item);
				list.Remove(item2);
				list.Remove(item2);
				if (array[1].IndexOf(list[0]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_5ZX20(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = CheckNumber.Split(new char[]
			{
				','
			});
			string[] array2 = array[0].Split(new char[]
			{
				'_'
			});
			bool flag = false;
			string item = "";
			if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 3)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					if (Regex.Matches(LotteryNumber, array2[i]).Count == 3)
					{
						flag = true;
						item = array2[i];
					}
				}
			}
			if (flag)
			{
				string[] source = LotteryNumber.Split(new char[]
				{
					','
				});
				List<string> list = source.ToList<string>();
				list.Remove(item);
				list.Remove(item);
				list.Remove(item);
				if (array[1].IndexOf(list[0]) != -1 && array[1].IndexOf(list[1]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_5ZX10(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = CheckNumber.Split(new char[]
			{
				','
			});
			string[] array2 = array[0].Split(new char[]
			{
				'_'
			});
			bool flag = false;
			string item = "";
			if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 2)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					if (Regex.Matches(LotteryNumber, array2[i]).Count == 3)
					{
						flag = true;
						item = array2[i];
					}
				}
			}
			if (flag)
			{
				string[] source = LotteryNumber.Split(new char[]
				{
					','
				});
				List<string> list = source.ToList<string>();
				list.Remove(item);
				list.Remove(item);
				list.Remove(item);
				bool flag2 = false;
				string value = "";
				for (int j = 0; j < list.Count; j++)
				{
					if (Regex.Matches(LotteryNumber, list[j]).Count == 2)
					{
						flag2 = true;
						value = array2[j];
					}
				}
				if (flag2 && array[1].IndexOf(value) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_5ZX5(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = CheckNumber.Split(new char[]
			{
				','
			});
			string[] array2 = array[0].Split(new char[]
			{
				'_'
			});
			bool flag = false;
			string item = "";
			if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 2)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					if (Regex.Matches(LotteryNumber, array2[i]).Count == 4)
					{
						flag = true;
						item = array2[i];
					}
				}
			}
			if (flag)
			{
				string[] source = LotteryNumber.Split(new char[]
				{
					','
				});
				List<string> list = source.ToList<string>();
				list.Remove(item);
				list.Remove(item);
				list.Remove(item);
				list.Remove(item);
				if (array[1].IndexOf(list[0]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_5TS(string LotteryNumber, string CheckNumber, int count)
		{
			int num = 0;
			if (CheckNumber.Length > 1 && !CheckNumber.Contains("_"))
			{
				return 0;
			}
			string[] array = CheckNumber.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array.Length; i++)
			{
				char[] cc = array[i].ToCharArray();
				if (LotteryNumber.Count((char c) => c == cc[0]) == count)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_5ZH_5(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = CheckNumber.Split(new char[]
			{
				','
			});
			if (array2.Length >= 5 && array2[0].IndexOf(array[0]) != -1 && array2[1].IndexOf(array[1]) != -1 && array2[2].IndexOf(array[2]) != -1 && array2[3].IndexOf(array[3]) != -1 && array2[4].IndexOf(array[4]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_5ZH_4(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = CheckNumber.Split(new char[]
			{
				','
			});
			if (array2.Length >= 5 && array2[1].IndexOf(array[1]) != -1 && array2[2].IndexOf(array[2]) != -1 && array2[3].IndexOf(array[3]) != -1 && array2[4].IndexOf(array[4]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_5ZH_3(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = CheckNumber.Split(new char[]
			{
				','
			});
			if (array2.Length >= 5 && array2[2].IndexOf(array[2]) != -1 && array2[3].IndexOf(array[3]) != -1 && array2[4].IndexOf(array[4]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_5ZH_2(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = CheckNumber.Split(new char[]
			{
				','
			});
			if (array2.Length >= 5 && array2[3].IndexOf(array[3]) != -1 && array2[4].IndexOf(array[4]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_5ZH_1(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = CheckNumber.Split(new char[]
			{
				','
			});
			if (array2.Length >= 5 && array2[4].IndexOf(array[4]) != -1)
			{
				num++;
			}
			return num;
		}
	}
}
