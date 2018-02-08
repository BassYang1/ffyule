using System;

namespace Lottery.Utils
{
	public static class CheckSSC_DXDS
	{
		public static int P_2DXDS(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (Pos == "L")
			{
				LotteryNumber = array[0] + "," + array[1];
			}
			if (Pos == "R")
			{
				LotteryNumber = array[3] + "," + array[4];
			}
			string text = LotteryNumber.Replace("0", "小_双").Replace("1", "小_单").Replace("2", "小_双").Replace("3", "小_单").Replace("4", "小_双").Replace("5", "大_单").Replace("6", "大_双").Replace("7", "大_单").Replace("8", "大_双").Replace("9", "大_单");
			LotteryNumber.Replace("0", "双").Replace("1", "单").Replace("2", "双").Replace("3", "单").Replace("4", "双").Replace("5", "单").Replace("6", "双").Replace("7", "单").Replace("8", "双").Replace("9", "单");
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
			string[] array5 = array2[1].Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array4.Length; i++)
			{
				for (int j = 0; j < array4.Length; j++)
				{
					if (array3[0].IndexOf(array4[i]) != -1 && array3[1].IndexOf(array5[j]) != -1)
					{
						num++;
					}
				}
			}
			return num;
		}
	}
}
