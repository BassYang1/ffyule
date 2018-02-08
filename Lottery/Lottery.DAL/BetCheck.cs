using System;

namespace Lottery.DAL
{
	public class BetCheck
	{
		public int BetErr
		{
			get
			{
				return this._BetErr;
			}
			set
			{
				this._BetErr = value;
			}
		}

		public int BetNum
		{
			get
			{
				return this._BetNum;
			}
			set
			{
				this._BetNum = value;
			}
		}

		public int BetPoint
		{
			get
			{
				return this._BetPoint;
			}
			set
			{
				this._BetPoint = value;
			}
		}

		public int IsOpen
		{
			get
			{
				return this._IsOpen;
			}
			set
			{
				this._IsOpen = value;
			}
		}

		public int IsEnable
		{
			get
			{
				return this._IsEnable;
			}
			set
			{
				this._IsEnable = value;
			}
		}

		private int _BetErr;

		private int _BetNum;

		private int _BetPoint;

		private int _IsOpen;

		private int _IsEnable;
	}
}
