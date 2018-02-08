using System;
using System.Collections.Generic;

namespace Lottery.Entity
{
	[Serializable]
	public class UserContract
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

		public int Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		public int ParentId
		{
			get
			{
				return this._parentid;
			}
			set
			{
				this._parentid = value;
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

		public int IsUsed
		{
			get
			{
				return this._isused;
			}
			set
			{
				this._isused = value;
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

		public List<UserContractDetail> UserContractDetails
		{
			get
			{
				return this._usercontractdetails;
			}
			set
			{
				this._usercontractdetails = value;
			}
		}

		private int _id;

		private int _type;

		private int _parentid;

		private int _userid;

		private int _isused;

		private DateTime _stime;

		private List<UserContractDetail> _usercontractdetails;
	}
}
