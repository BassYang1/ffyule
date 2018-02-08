using System;

namespace Lottery.Entity
{
	[Serializable]
	public class UserContractDetail
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

		public int UcId
		{
			get
			{
				return this._ucid;
			}
			set
			{
				this._ucid = value;
			}
		}

		public decimal MinMoney
		{
			get
			{
				return this._minmoney;
			}
			set
			{
				this._minmoney = value;
			}
		}

		public decimal Money
		{
			get
			{
				return this._money;
			}
			set
			{
				this._money = value;
			}
		}

		public int Sort
		{
			get
			{
				return this._sort;
			}
			set
			{
				this._sort = value;
			}
		}

		private int _id;

		private int _ucid;

		private decimal _minmoney;

		private decimal _money;

		private int _sort;
	}
}
