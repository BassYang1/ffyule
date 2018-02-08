using System;

namespace Lottery.Entity
{
	[Serializable]
	public class UserBetModel
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

		public decimal UserMoney
		{
			get
			{
				return this._usermoney;
			}
			set
			{
				this._usermoney = value;
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

		public string PlayCode
		{
			get
			{
				return this._playcode;
			}
			set
			{
				this._playcode = value;
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

		public string IssueNum
		{
			get
			{
				return this._issuenum;
			}
			set
			{
				this._issuenum = value;
			}
		}

		public decimal SingleMoney
		{
			get
			{
				return this._singlemoney;
			}
			set
			{
				this._singlemoney = value;
			}
		}

		public decimal Times
		{
			get
			{
				return this._times;
			}
			set
			{
				this._times = value;
			}
		}

		public int Num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		public string Detail
		{
			get
			{
				return this._detail;
			}
			set
			{
				this._detail = value;
			}
		}

		public int DX
		{
			get
			{
				return this._dx;
			}
			set
			{
				this._dx = value;
			}
		}

		public int DS
		{
			get
			{
				return this._ds;
			}
			set
			{
				this._ds = value;
			}
		}

		public decimal Total
		{
			get
			{
				return this._total;
			}
			set
			{
				this._total = value;
			}
		}

		public decimal Point
		{
			get
			{
				return this._point;
			}
			set
			{
				this._point = value;
			}
		}

		public decimal PointMoney
		{
			get
			{
				return this._pointmoney;
			}
			set
			{
				this._pointmoney = value;
			}
		}

		public decimal Bonus
		{
			get
			{
				return this._bonus;
			}
			set
			{
				this._bonus = value;
			}
		}

		public int WinNum
		{
			get
			{
				return this._winnum;
			}
			set
			{
				this._winnum = value;
			}
		}

		public decimal WinBonus
		{
			get
			{
				return this._winbonus;
			}
			set
			{
				this._winbonus = value;
			}
		}

		public decimal RealGet
		{
			get
			{
				return this._realget;
			}
			set
			{
				this._realget = value;
			}
		}

		public string Pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
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

		public DateTime STime2
		{
			get
			{
				return this._stime2;
			}
			set
			{
				this._stime2 = value;
			}
		}

		public int IsOpen
		{
			get
			{
				return this._isopen;
			}
			set
			{
				this._isopen = value;
			}
		}

		public int State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		public int IsDelay
		{
			get
			{
				return this._isdelay;
			}
			set
			{
				this._isdelay = value;
			}
		}

		public int IsWin
		{
			get
			{
				return this._iswin;
			}
			set
			{
				this._iswin = value;
			}
		}

		public DateTime STime9
		{
			get
			{
				return this._stime9;
			}
			set
			{
				this._stime9 = value;
			}
		}

		public bool IsCheat
		{
			get
			{
				return this._ischeat;
			}
			set
			{
				this._ischeat = value;
			}
		}

		public int ZHID
		{
			get
			{
				return this._zhid;
			}
			set
			{
				this._zhid = value;
			}
		}

		public int zhid2
		{
			get
			{
				return this._zhid2;
			}
			set
			{
				this._zhid2 = value;
			}
		}

		private int _id;

		private int _userid;

		private decimal _usermoney = 0.0000m;

		private int _playid;

		private string _playcode;

		private int _lotteryid;

		private string _issuenum;

		private decimal _singlemoney = 0.0000m;

		private decimal _times = 1m;

		private int _num = 1;

		private string _detail;

		private int _dx;

		private int _ds;

		private decimal _total = 0.0000m;

		private decimal _point = 0.0000m;

		private decimal _pointmoney = 0.0000m;

		private decimal _bonus = 0.0000m;

		private int _winnum;

		private decimal _winbonus;

		private decimal _realget;

		private string _pos;

		private DateTime _stime = DateTime.Now;

		private DateTime _stime2 = DateTime.Now;

		private int _isopen;

		private int _state;

		private int _isdelay;

		private int _iswin = -1;

		private DateTime _stime9;

		private bool _ischeat;

		private int _zhid;

		private int _zhid2;
	}
}
