using System;
using System.Collections.Generic;

namespace Lottery.Entity
{
	[Serializable]
	public class UserZhDetailModel
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

		public int Times
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

		public List<UserBetModel> Lists
		{
			get
			{
				return this.list;
			}
			set
			{
				this.list = value;
			}
		}

		private int _id;

		private string _issuenum;

		private int _times;

		private DateTime _stime;

		private List<UserBetModel> list = new List<UserBetModel>();
	}
}
