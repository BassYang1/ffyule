using System;

namespace Lottery.Utils
{
	public static class CheckPK10_DXDS
	{
		public static int PK10_DS(string LotteryNumber, string CheckNumber, int Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (Pos == 1)
			{
				LotteryNumber = array[0];
			}
			if (Pos == 2)
			{
				LotteryNumber = array[1];
			}
			if (Pos == 3)
			{
				LotteryNumber = array[2];
			}
			string text = LotteryNumber.Replace("01", "单").Replace("02", "双").Replace("03", "单").Replace("04", "双").Replace("05", "单").Replace("06", "双").Replace("07", "单").Replace("08", "双").Replace("09", "单").Replace("10", "双");
			string[] array2 = text.Split(new char[]
			{
				','
			});
			string[] array3 = CheckNumber.Split(new char[]
			{
				','
			});
			string[] array4 = array2[0].Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array4.Length; i++)
			{
				for (int j = 0; j < array4.Length; j++)
				{
					if (array3[0].IndexOf(array4[i]) != -1)
					{
						num++;
					}
				}
			}
			return num;
		}

		public static int PK10_DX(string LotteryNumber, string CheckNumber, int Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (Pos == 1)
			{
				LotteryNumber = array[0];
			}
			if (Pos == 2)
			{
				LotteryNumber = array[1];
			}
			if (Pos == 3)
			{
				LotteryNumber = array[2];
			}
			string text = LotteryNumber.Replace("01", "小").Replace("02", "小").Replace("03", "小").Replace("04", "小").Replace("05", "小").Replace("06", "大").Replace("07", "大").Replace("08", "大").Replace("09", "大").Replace("10", "大");
			string[] array2 = text.Split(new char[]
			{
				','
			});
			string[] array3 = CheckNumber.Split(new char[]
			{
				','
			});
			string[] array4 = array2[0].Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array4.Length; i++)
			{
				for (int j = 0; j < array4.Length; j++)
				{
					if (array3[0].IndexOf(array4[i]) != -1)
					{
						num++;
					}
				}
			}
			return num;
		}
	}
}
