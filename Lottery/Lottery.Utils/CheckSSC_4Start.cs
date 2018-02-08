using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
	public static class CheckSSC_4Start
	{
		public static int P_4FS(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (Pos == "L")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2],
					",",
					array[3]
				});
			}
			if (Pos == "R")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[1],
					",",
					array[2],
					",",
					array[3],
					",",
					array[4]
				});
			}
			if (Pos == "WQBS")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2],
					",",
					array[3]
				});
			}
			if (Pos == "WQBG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2],
					",",
					array[4]
				});
			}
			if (Pos == "WQSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[3],
					",",
					array[4]
				});
			}
			if (Pos == "WBSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[2],
					",",
					array[3],
					",",
					array[4]
				});
			}
			if (Pos == "QBSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[1],
					",",
					array[2],
					",",
					array[3],
					",",
					array[4]
				});
			}
			string[] array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array3 = CheckNumber.Split(new char[]
			{
				','
			});
			if (array3.Length >= 4 && array3[0].IndexOf(array2[0]) != -1 && array3[1].IndexOf(array2[1]) != -1 && array3[2].IndexOf(array2[2]) != -1 && array3[3].IndexOf(array2[3]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_4DS(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (Pos == "L")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2],
					",",
					array[3]
				});
			}
			if (Pos == "R")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[1],
					",",
					array[2],
					",",
					array[3],
					",",
					array[4]
				});
			}
			if (Pos == "WQBS")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2],
					",",
					array[3]
				});
			}
			if (Pos == "WQBG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2],
					",",
					array[4]
				});
			}
			if (Pos == "WQSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[3],
					",",
					array[4]
				});
			}
			if (Pos == "WBSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[2],
					",",
					array[3],
					",",
					array[4]
				});
			}
			if (Pos == "QBSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[1],
					",",
					array[2],
					",",
					array[3],
					",",
					array[4]
				});
			}
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

		public static int P_4ZX24(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (Pos == "L")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2],
					",",
					array[3]
				});
			}
			if (Pos == "R")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[1],
					",",
					array[2],
					",",
					array[3],
					",",
					array[4]
				});
			}
			if (Pos == "WQBS")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2],
					",",
					array[3]
				});
			}
			if (Pos == "WQBG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2],
					",",
					array[4]
				});
			}
			if (Pos == "WQSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[3],
					",",
					array[4]
				});
			}
			if (Pos == "WBSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[2],
					",",
					array[3],
					",",
					array[4]
				});
			}
			if (Pos == "QBSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[1],
					",",
					array[2],
					",",
					array[3],
					",",
					array[4]
				});
			}
			string[] array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			if (Convert.ToInt32(array2[0]) != Convert.ToInt32(array2[1]) && Convert.ToInt32(array2[0]) != Convert.ToInt32(array2[2]) && Convert.ToInt32(array2[0]) != Convert.ToInt32(array2[3]) && Convert.ToInt32(array2[1]) != Convert.ToInt32(array2[2]) && Convert.ToInt32(array2[1]) != Convert.ToInt32(array2[3]) && Convert.ToInt32(array2[2]) != Convert.ToInt32(array2[3]) && CheckNumber.Contains(array2[0]) && CheckNumber.Contains(array2[1]) && CheckNumber.Contains(array2[2]) && CheckNumber.Contains(array2[3]))
			{
				num++;
			}
			return num;
		}

		public static int P_4ZX12(string LotteryNumber, string CheckNumber, string Pos)
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
			string[] array3 = LotteryNumber.Split(new char[]
			{
				','
			});
			if (Pos == "L")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[1],
					",",
					array3[2],
					",",
					array3[3]
				});
			}
			if (Pos == "R")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[1],
					",",
					array3[2],
					",",
					array3[3],
					",",
					array3[4]
				});
			}
			if (Pos == "WQBS")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[1],
					",",
					array3[2],
					",",
					array3[3]
				});
			}
			if (Pos == "WQBG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[1],
					",",
					array3[2],
					",",
					array3[4]
				});
			}
			if (Pos == "WQSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[1],
					",",
					array3[3],
					",",
					array3[4]
				});
			}
			if (Pos == "WBSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[2],
					",",
					array3[3],
					",",
					array3[4]
				});
			}
			if (Pos == "QBSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[1],
					",",
					array3[2],
					",",
					array3[3],
					",",
					array3[4]
				});
			}
			if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 3)
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
				if (array[1].IndexOf(list[0]) != -1 && array[1].IndexOf(list[1]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_4ZX6(string LotteryNumber, string CheckNumber, string Pos)
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
			string[] array3 = LotteryNumber.Split(new char[]
			{
				','
			});
			if (Pos == "L")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[1],
					",",
					array3[2],
					",",
					array3[3]
				});
			}
			if (Pos == "R")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[1],
					",",
					array3[2],
					",",
					array3[3],
					",",
					array3[4]
				});
			}
			if (Pos == "WQBS")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[1],
					",",
					array3[2],
					",",
					array3[3]
				});
			}
			if (Pos == "WQBG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[1],
					",",
					array3[2],
					",",
					array3[4]
				});
			}
			if (Pos == "WQSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[1],
					",",
					array3[3],
					",",
					array3[4]
				});
			}
			if (Pos == "WBSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[2],
					",",
					array3[3],
					",",
					array3[4]
				});
			}
			if (Pos == "QBSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[1],
					",",
					array3[2],
					",",
					array3[3],
					",",
					array3[4]
				});
			}
			if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 2)
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
								string arg_312_0 = array2[i];
								string arg_317_0 = array2[j];
							}
						}
					}
				}
			}
			if (flag)
			{
				num++;
			}
			return num;
		}

		public static int P_4ZX4(string LotteryNumber, string CheckNumber, string Pos)
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
			string[] array3 = LotteryNumber.Split(new char[]
			{
				','
			});
			if (Pos == "L")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[1],
					",",
					array3[2],
					",",
					array3[3]
				});
			}
			if (Pos == "R")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[1],
					",",
					array3[2],
					",",
					array3[3],
					",",
					array3[4]
				});
			}
			if (Pos == "WQBS")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[1],
					",",
					array3[2],
					",",
					array3[3]
				});
			}
			if (Pos == "WQBG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[1],
					",",
					array3[2],
					",",
					array3[4]
				});
			}
			if (Pos == "WQSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[1],
					",",
					array3[3],
					",",
					array3[4]
				});
			}
			if (Pos == "WBSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[0],
					",",
					array3[2],
					",",
					array3[3],
					",",
					array3[4]
				});
			}
			if (Pos == "QBSG")
			{
				LotteryNumber = string.Concat(new string[]
				{
					array3[1],
					",",
					array3[2],
					",",
					array3[3],
					",",
					array3[4]
				});
			}
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
				if (array[1].IndexOf(list[0]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_4ZH_4(string LotteryNumber, string CheckNumber, string Pos)
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
			if (Pos == "L" && array2.Length >= 4 && array2[0].IndexOf(array[0]) != -1 && array2[1].IndexOf(array[1]) != -1 && array2[2].IndexOf(array[2]) != -1 && array2[3].IndexOf(array[3]) != -1)
			{
				num++;
			}
			if (Pos == "R" && array2.Length >= 4 && array2[1].IndexOf(array[1]) != -1 && array2[2].IndexOf(array[2]) != -1 && array2[3].IndexOf(array[3]) != -1 && array2[4].IndexOf(array[4]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_4ZH_3(string LotteryNumber, string CheckNumber, string Pos)
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
			if (Pos == "L" && array2.Length >= 4 && array2[1].IndexOf(array[1]) != -1 && array2[2].IndexOf(array[2]) != -1 && array2[3].IndexOf(array[3]) != -1)
			{
				num++;
			}
			if (Pos == "R" && array2.Length >= 4 && array2[2].IndexOf(array[2]) != -1 && array2[2].IndexOf(array[3]) != -1 && array2[3].IndexOf(array[4]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_4ZH_2(string LotteryNumber, string CheckNumber, string Pos)
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
			if (Pos == "L" && array2.Length >= 4 && array2[2].IndexOf(array[2]) != -1 && array2[3].IndexOf(array[3]) != -1)
			{
				num++;
			}
			if (Pos == "R" && array2.Length >= 4 && array2[2].IndexOf(array[3]) != -1 && array2[3].IndexOf(array[4]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_4ZH_1(string LotteryNumber, string CheckNumber, string Pos)
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
			if (Pos == "L" && array2.Length >= 4 && array2[3].IndexOf(array[3]) != -1)
			{
				num++;
			}
			if (Pos == "R" && array2.Length >= 4 && array2[3].IndexOf(array[4]) != -1)
			{
				num++;
			}
			return num;
		}
	}
}
