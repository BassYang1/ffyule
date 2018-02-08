using System;

namespace Lottery.Entity
{
	[Serializable]
	public class SiteModel
	{
		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		public string Dir
		{
			get
			{
				return this.m_Dir;
			}
			set
			{
				this.m_Dir = value;
			}
		}

		public int WebIsOpen
		{
			get
			{
				return this._webisopen;
			}
			set
			{
				this._webisopen = value;
			}
		}

		public string WebCloseSeason
		{
			get
			{
				return this._webcloseseason;
			}
			set
			{
				this._webcloseseason = value;
			}
		}

		public int ZHIsOpen
		{
			get
			{
				return this._zhisopen;
			}
			set
			{
				this._zhisopen = value;
			}
		}

		public int RegIsOpen
		{
			get
			{
				return this._regisopen;
			}
			set
			{
				this._regisopen = value;
			}
		}

		public int BetIsOpen
		{
			get
			{
				return this._betisopen;
			}
			set
			{
				this._betisopen = value;
			}
		}

		public string CSUrl
		{
			get
			{
				return this._csurl;
			}
			set
			{
				this._csurl = value;
			}
		}

		public int SignMinTotal
		{
			get
			{
				return this._SignMinTotal;
			}
			set
			{
				this._SignMinTotal = value;
			}
		}

		public int SignMaxTotal
		{
			get
			{
				return this._SignMaxTotal;
			}
			set
			{
				this._SignMaxTotal = value;
			}
		}

		public int SignNum
		{
			get
			{
				return this._SignNum;
			}
			set
			{
				this._SignNum = value;
			}
		}

		public decimal WarnTotal
		{
			get
			{
				return this._warntotal;
			}
			set
			{
				this._warntotal = value;
			}
		}

		public decimal MaxBet
		{
			get
			{
				return this._maxbet;
			}
			set
			{
				this._maxbet = value;
			}
		}

		public decimal MaxWin
		{
			get
			{
				return this._maxwin;
			}
			set
			{
				this._maxwin = value;
			}
		}

		public decimal MaxWinFK
		{
			get
			{
				return this._maxwinfk;
			}
			set
			{
				this._maxwinfk = value;
			}
		}

		public decimal MaxLevel
		{
			get
			{
				return this._maxlevel;
			}
			set
			{
				this._maxlevel = value;
			}
		}

		public decimal MinCharge
		{
			get
			{
				return this._mincharge;
			}
			set
			{
				this._mincharge = value;
			}
		}

		public int Points
		{
			get
			{
				return this._points;
			}
			set
			{
				this._points = value;
			}
		}

		public decimal PriceOutCheck
		{
			get
			{
				return this._priceoutcheck;
			}
			set
			{
				this._priceoutcheck = value;
			}
		}

		public decimal PriceOut
		{
			get
			{
				return this._priceout;
			}
			set
			{
				this._priceout = value;
			}
		}

		public decimal PriceOut2
		{
			get
			{
				return this._priceout2;
			}
			set
			{
				this._priceout2 = value;
			}
		}

		public int PriceNum
		{
			get
			{
				return this._pricenum;
			}
			set
			{
				this._pricenum = value;
			}
		}

		public string PriceTime1
		{
			get
			{
				return this._pricetime1;
			}
			set
			{
				this._pricetime1 = value;
			}
		}

		public string PriceTime2
		{
			get
			{
				return this._pricetime2;
			}
			set
			{
				this._pricetime2 = value;
			}
		}

		public decimal BankTime
		{
			get
			{
				return this._bankTime;
			}
			set
			{
				this._bankTime = value;
			}
		}

		public int PriceOutPerson
		{
			get
			{
				return this._priceoutperson;
			}
			set
			{
				this._priceoutperson = value;
			}
		}

		public string ClientVersion
		{
			get
			{
				return this._clientversion;
			}
			set
			{
				this._clientversion = value;
			}
		}

		public DateTime UpdateTime
		{
			get
			{
				return this._updatetime;
			}
			set
			{
				this._updatetime = value;
			}
		}

		public DateTime NewsUpdateTime
		{
			get
			{
				return this._newsupdatetime;
			}
			set
			{
				this._newsupdatetime = value;
			}
		}

		public int AutoLottery
		{
			get
			{
				return this._autolottery;
			}
			set
			{
				this._autolottery = value;
			}
		}

		public int ProfitModel
		{
			get
			{
				return this._profitmodel;
			}
			set
			{
				this._profitmodel = value;
			}
		}

		public int ProfitMargin
		{
			get
			{
				return this._profitmargin;
			}
			set
			{
				this._profitmargin = value;
			}
		}

		public int AutoRanking
		{
			get
			{
				return this._autoranking;
			}
			set
			{
				this._autoranking = value;
			}
		}

		public string CookieDomain
		{
			get
			{
				return this.m_CookieDomain;
			}
			set
			{
				this.m_CookieDomain = value;
			}
		}

		public string CookiePath
		{
			get
			{
				return this.m_CookiePath;
			}
			set
			{
				this.m_CookiePath = value;
			}
		}

		public string CookiePrev
		{
			get
			{
				return this.m_CookiePrev;
			}
			set
			{
				this.m_CookiePrev = value;
			}
		}

		public string CookieKeyCode
		{
			get
			{
				return this.m_CookieKeyCode;
			}
			set
			{
				this.m_CookieKeyCode = value;
			}
		}

		public string Version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		public string DebugKey
		{
			get
			{
				return this.m_DebugKey;
			}
			set
			{
				this.m_DebugKey = value;
			}
		}

		public int BetSQ
		{
			get
			{
				return this._betsq;
			}
			set
			{
				this._betsq = value;
			}
		}

		public decimal BetSQMoney
		{
			get
			{
				return this._betsqmoney;
			}
			set
			{
				this._betsqmoney = value;
			}
		}

		private string m_Name;

		private string m_Dir;

		private int _webisopen;

		private string _webcloseseason;

		private int _zhisopen;

		private int _regisopen;

		private int _betisopen;

		private string _csurl;

		private int _SignMinTotal;

		private int _SignMaxTotal;

		private int _SignNum;

		private decimal _warntotal;

		private decimal _maxbet;

		private decimal _maxwin;

		private decimal _maxwinfk;

		private decimal _maxlevel;

		private decimal _mincharge;

		private int _points;

		private decimal _priceoutcheck;

		private decimal _priceout;

		private decimal _priceout2;

		private int _pricenum;

		private string _pricetime1;

		private string _pricetime2;

		private decimal _bankTime;

		private int _priceoutperson;

		private string _clientversion;

		private DateTime _updatetime;

		private DateTime _newsupdatetime;

		private int _autolottery;

		private int _profitmodel = 1;

		private int _profitmargin = 10;

		private int _autoranking;

		private string m_CookieDomain;

		private string m_CookiePath;

		private string m_CookiePrev;

		private string m_CookieKeyCode;

		private string m_Version;

		private string m_DebugKey;

		private int _betsq;

		private decimal _betsqmoney;
	}
}
