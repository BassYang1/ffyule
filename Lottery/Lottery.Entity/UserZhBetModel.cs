using System;

namespace Lottery.Entity
{
	[Serializable]
	public class UserZhBetModel
	{
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		public int UserId
		{
			get
			{
				return this._userid;
			}
			set
			{
				this._userid = value;
			}
		}

		public int LotteryId
		{
			get
			{
				return this._lotteryid;
			}
			set
			{
				this._lotteryid = value;
			}
		}

		public int PlayId
		{
			get
			{
				return this._playid;
			}
			set
			{
				this._playid = value;
			}
		}

		public string StartIssueNum
		{
			get
			{
				return this._startissuenum;
			}
			set
			{
				this._startissuenum = value;
			}
		}

		public int TotalNums
		{
			get
			{
				return this._totalnums;
			}
			set
			{
				this._totalnums = value;
			}
		}

		public decimal TotalSums
		{
			get
			{
				return this._totalsums;
			}
			set
			{
				this._totalsums = value;
			}
		}

		public int IsStop
		{
			get
			{
				return this._isstop;
			}
			set
			{
				this._isstop = value;
			}
		}

		public DateTime STime
		{
			get
			{
				return this._stime;
			}
			set
			{
				this._stime = value;
			}
		}

		private int _id;

		private int _userid;

		private int _lotteryid;

		private int _playid;

		private string _startissuenum;

		private int _totalnums;

		private decimal _totalsums;

		private int _isstop;

		private DateTime _stime;
	}
}
