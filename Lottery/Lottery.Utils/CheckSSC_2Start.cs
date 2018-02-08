using System;
using System.Linq;

namespace Lottery.Utils
{
	public static class CheckSSC_2Start
	{
		public static int P_2ZX(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array.Length == 3)
			{
				if (Pos == "L")
				{
					LotteryNumber = array[0] + "," + array[1];
				}
				if (Pos == "R")
				{
					LotteryNumber = array[1] + "," + array[2];
				}
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = array[0] + "," + array[1];
				}
				if (Pos == "R")
				{
					LotteryNumber = array[3] + "," + array[4];
				}
				if (Pos == "WQ")
				{
					LotteryNumber = array[0] + "," + array[1];
				}
				if (Pos == "WB")
				{
					LotteryNumber = array[0] + "," + array[2];
				}
				if (Pos == "WS")
				{
					LotteryNumber = array[0] + "," + array[3];
				}
				if (Pos == "WG")
				{
					LotteryNumber = array[0] + "," + array[4];
				}
				if (Pos == "QB")
				{
					LotteryNumber = array[1] + "," + array[2];
				}
				if (Pos == "QS")
				{
					LotteryNumber = array[1] + "," + array[3];
				}
				if (Pos == "QG")
				{
					LotteryNumber = array[1] + "," + array[4];
				}
				if (Pos == "BS")
				{
					LotteryNumber = array[2] + "," + array[3];
				}
				if (Pos == "BG")
				{
					LotteryNumber = array[2] + "," + array[4];
				}
				if (Pos == "SG")
				{
					LotteryNumber = array[3] + "," + array[4];
				}
			}
			string[] array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array3 = CheckNumber.Split(new char[]
			{
				','
			});
			if (array3.Length >= 2 && array3[0].IndexOf(array2[0]) != -1 && array3[1].IndexOf(array2[1]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_2DS(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array.Length == 3)
			{
				if (Pos == "L")
				{
					LotteryNumber = array[0] + "," + array[1];
				}
				if (Pos == "R")
				{
					LotteryNumber = array[1] + "," + array[2];
				}
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = array[0] + "," + array[1];
				}
				if (Pos == "R")
				{
					LotteryNumber = array[3] + "," + array[4];
				}
				if (Pos == "WQ")
				{
					LotteryNumber = array[0] + "," + array[1];
				}
				if (Pos == "WB")
				{
					LotteryNumber = array[0] + "," + array[2];
				}
				if (Pos == "WS")
				{
					LotteryNumber = array[0] + "," + array[3];
				}
				if (Pos == "WG")
				{
					LotteryNumber = array[0] + "," + array[4];
				}
				if (Pos == "QB")
				{
					LotteryNumber = array[1] + "," + array[2];
				}
				if (Pos == "QS")
				{
					LotteryNumber = array[1] + "," + array[3];
				}
				if (Pos == "QG")
				{
					LotteryNumber = array[1] + "," + array[4];
				}
				if (Pos == "BS")
				{
					LotteryNumber = array[2] + "," + array[3];
				}
				if (Pos == "BG")
				{
					LotteryNumber = array[2] + "," + array[4];
				}
				if (Pos == "SG")
				{
					LotteryNumber = array[3] + "," + array[4];
				}
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

		public static int P_2Z2(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array.Length == 3)
			{
				if (Pos == "L")
				{
					LotteryNumber = array[0] + "," + array[1];
				}
				if (Pos == "R")
				{
					LotteryNumber = array[1] + "," + array[2];
				}
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = array[0] + "," + array[1];
				}
				if (Pos == "R")
				{
					LotteryNumber = array[3] + "," + array[4];
				}
				if (Pos == "WQ")
				{
					LotteryNumber = array[0] + "," + array[1];
				}
				if (Pos == "WB")
				{
					LotteryNumber = array[0] + "," + array[2];
				}
				if (Pos == "WS")
				{
					LotteryNumber = array[0] + "," + array[3];
				}
				if (Pos == "WG")
				{
					LotteryNumber = array[0] + "," + array[4];
				}
				if (Pos == "QB")
				{
					LotteryNumber = array[1] + "," + array[2];
				}
				if (Pos == "QS")
				{
					LotteryNumber = array[1] + "," + array[3];
				}
				if (Pos == "QG")
				{
					LotteryNumber = array[1] + "," + array[4];
				}
				if (Pos == "BS")
				{
					LotteryNumber = array[2] + "," + array[3];
				}
				if (Pos == "BG")
				{
					LotteryNumber = array[2] + "," + array[4];
				}
				if (Pos == "SG")
				{
					LotteryNumber = array[3] + "," + array[4];
				}
			}
			string[] array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array3 = CheckNumber.Split(new char[]
			{
				','
			});
			if (array2[0] != array2[1])
			{
				for (int i = 0; i < array3.Length; i++)
				{
					if (array3[i].IndexOf(array2[0]) != -1 && array3[i].IndexOf(array2[1]) != -1)
					{
						num++;
					}
				}
			}
			return num;
		}

		public static int P_2HE(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			int num2 = 0;
			if (array.Length == 3)
			{
				if (Pos == "L")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]);
				}
				if (Pos == "R")
				{
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]);
				}
			}
			else
			{
				if (Pos == "L")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]);
				}
				if (Pos == "R")
				{
					num2 = Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "WQ")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]);
				}
				if (Pos == "WB")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[2]);
				}
				if (Pos == "WS")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "WG")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "QB")
				{
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]);
				}
				if (Pos == "QS")
				{
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "QG")
				{
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "BS")
				{
					num2 = Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "BG")
				{
					num2 = Convert.ToInt32(array[2]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "SG")
				{
					num2 = Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]);
				}
			}
			string[] array2 = CheckNumber.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				if (Convert.ToInt32(array2[i]) == num2)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_2ZHE(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			int num2 = 0;
			if (array.Length == 3)
			{
				if (Pos == "L")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]);
				}
				if (Pos == "R")
				{
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]);
				}
			}
			else
			{
				if (Pos == "L")
				{
					if (Convert.ToInt32(array[0]) == Convert.ToInt32(array[1]))
					{
						return 0;
					}
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]);
				}
				if (Pos == "R")
				{
					if (Convert.ToInt32(array[3]) == Convert.ToInt32(array[4]))
					{
						return 0;
					}
					num2 = Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "WQ")
				{
					if (Convert.ToInt32(array[0]) == Convert.ToInt32(array[1]))
					{
						return 0;
					}
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]);
				}
				if (Pos == "WB")
				{
					if (Convert.ToInt32(array[0]) == Convert.ToInt32(array[2]))
					{
						return 0;
					}
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[2]);
				}
				if (Pos == "WS")
				{
					if (Convert.ToInt32(array[0]) == Convert.ToInt32(array[3]))
					{
						return 0;
					}
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "WG")
				{
					if (Convert.ToInt32(array[0]) == Convert.ToInt32(array[4]))
					{
						return 0;
					}
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "QB")
				{
					if (Convert.ToInt32(array[1]) == Convert.ToInt32(array[2]))
					{
						return 0;
					}
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]);
				}
				if (Pos == "QS")
				{
					if (Convert.ToInt32(array[1]) == Convert.ToInt32(array[3]))
					{
						return 0;
					}
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "QG")
				{
					if (Convert.ToInt32(array[1]) == Convert.ToInt32(array[4]))
					{
						return 0;
					}
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "BS")
				{
					if (Convert.ToInt32(array[2]) == Convert.ToInt32(array[3]))
					{
						return 0;
					}
					num2 = Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "BG")
				{
					if (Convert.ToInt32(array[2]) == Convert.ToInt32(array[4]))
					{
						return 0;
					}
					num2 = Convert.ToInt32(array[2]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "SG")
				{
					if (Convert.ToInt32(array[3]) == Convert.ToInt32(array[4]))
					{
						return 0;
					}
					num2 = Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]);
				}
			}
			string[] array2 = CheckNumber.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				if (Convert.ToInt32(array2[i]) == num2)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_2KD(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] source = new string[2];
			if (Pos == "L")
			{
				LotteryNumber = array[0] + "," + array[1];
			}
			if (Pos == "R")
			{
				LotteryNumber = array[3] + "," + array[4];
			}
			if (Pos == "WQ")
			{
				LotteryNumber = array[0] + "," + array[1];
			}
			if (Pos == "WB")
			{
				LotteryNumber = array[0] + "," + array[2];
			}
			if (Pos == "WS")
			{
				LotteryNumber = array[0] + "," + array[3];
			}
			if (Pos == "WG")
			{
				LotteryNumber = array[0] + "," + array[4];
			}
			if (Pos == "QB")
			{
				LotteryNumber = array[1] + "," + array[2];
			}
			if (Pos == "QS")
			{
				LotteryNumber = array[1] + "," + array[3];
			}
			if (Pos == "QG")
			{
				LotteryNumber = array[1] + "," + array[4];
			}
			if (Pos == "BS")
			{
				LotteryNumber = array[2] + "," + array[3];
			}
			if (Pos == "BG")
			{
				LotteryNumber = array[2] + "," + array[4];
			}
			if (Pos == "SG")
			{
				LotteryNumber = array[3] + "," + array[4];
			}
			source = LotteryNumber.Split(new char[]
			{
				','
			});
			int num2 = Convert.ToInt32(source.Max<string>()) - Convert.ToInt32(source.Min<string>());
			string[] array2 = CheckNumber.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				if (Convert.ToInt32(array2[i]) == num2)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_2ZBD(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = new string[2];
			if (Pos == "L")
			{
				LotteryNumber = array[0] + "," + array[1];
			}
			if (Pos == "R")
			{
				LotteryNumber = array[3] + "," + array[4];
			}
			if (Pos == "WQ")
			{
				LotteryNumber = array[0] + "," + array[1];
			}
			if (Pos == "WB")
			{
				LotteryNumber = array[0] + "," + array[2];
			}
			if (Pos == "WS")
			{
				LotteryNumber = array[0] + "," + array[3];
			}
			if (Pos == "WG")
			{
				LotteryNumber = array[0] + "," + array[4];
			}
			if (Pos == "QB")
			{
				LotteryNumber = array[1] + "," + array[2];
			}
			if (Pos == "QS")
			{
				LotteryNumber = array[1] + "," + array[3];
			}
			if (Pos == "QG")
			{
				LotteryNumber = array[1] + "," + array[4];
			}
			if (Pos == "BS")
			{
				LotteryNumber = array[2] + "," + array[3];
			}
			if (Pos == "BG")
			{
				LotteryNumber = array[2] + "," + array[4];
			}
			if (Pos == "SG")
			{
				LotteryNumber = array[3] + "," + array[4];
			}
			array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array2[0] != array2[1] && (array2[0] + "," + array2[1]).Contains(CheckNumber))
			{
				num++;
			}
			return num;
		}

		public static int P_2ZDS(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = new string[2];
			if (Pos == "L")
			{
				LotteryNumber = array[0] + "," + array[1];
			}
			if (Pos == "R")
			{
				LotteryNumber = array[3] + "," + array[4];
			}
			if (Pos == "WQ")
			{
				LotteryNumber = array[0] + "," + array[1];
			}
			if (Pos == "WB")
			{
				LotteryNumber = array[0] + "," + array[2];
			}
			if (Pos == "WS")
			{
				LotteryNumber = array[0] + "," + array[3];
			}
			if (Pos == "WG")
			{
				LotteryNumber = array[0] + "," + array[4];
			}
			if (Pos == "QB")
			{
				LotteryNumber = array[1] + "," + array[2];
			}
			if (Pos == "QS")
			{
				LotteryNumber = array[1] + "," + array[3];
			}
			if (Pos == "QG")
			{
				LotteryNumber = array[1] + "," + array[4];
			}
			if (Pos == "BS")
			{
				LotteryNumber = array[2] + "," + array[3];
			}
			if (Pos == "BG")
			{
				LotteryNumber = array[2] + "," + array[4];
			}
			if (Pos == "SG")
			{
				LotteryNumber = array[3] + "," + array[4];
			}
			array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			if (CheckNumber.Contains(array2[0] + array2[1]))
			{
				num++;
			}
			if (CheckNumber.Contains(array2[1] + array2[0]))
			{
				num++;
			}
			return num;
		}
	}
}
