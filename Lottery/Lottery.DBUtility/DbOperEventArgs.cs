using System;

namespace Lottery.DBUtility
{
	public class DbOperEventArgs : EventArgs
	{
		public DbOperEventArgs(int _id)
		{
			this.id = _id;
		}

		public int id;
	}
}
