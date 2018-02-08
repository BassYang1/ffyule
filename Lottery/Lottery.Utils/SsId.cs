using System;

namespace Lottery.Utils
{
	public class SsId
	{
		public static string Bet
		{
			get
			{
				return "B_" + SsId.GuidToLongID();
			}
		}

		public static string ZBet
		{
			get
			{
				return "Z_" + SsId.GuidToLongID();
			}
		}

		public static string Charge
		{
			get
			{
				return "C_" + SsId.GuidToLongID();
			}
		}

		public static string ChargeLog
		{
			get
			{
				return "L_" + SsId.GuidToLongID();
			}
		}

		public static string GetCash
		{
			get
			{
				return "G_" + SsId.GuidToLongID();
			}
		}

		public static string MoneyLog
		{
			get
			{
				return "M_" + SsId.GuidToLongID();
			}
		}

		public static string Act
		{
			get
			{
				return "A_" + SsId.GuidToLongID();
			}
		}

		public static string Admin
		{
			get
			{
				return "H_" + SsId.GuidToLongID();
			}
		}

		public static string GuidTo16String()
		{
			long num = 1L;
			byte[] array = Guid.NewGuid().ToByteArray();
			for (int i = 0; i < array.Length; i++)
			{
				byte b = array[i];
				num *= (long)(b + 1);
			}
			return string.Format("{0:x}", num - DateTime.Now.Ticks);
		}

		public static long GuidToLongID()
		{
			byte[] value = Guid.NewGuid().ToByteArray();
			return BitConverter.ToInt64(value, 0);
		}
	}
}
