using System;

namespace Lottery.DBUtility
{
	public class DbKeyItem
	{
		public DbKeyItem(string _fieldName, object _fieldValue)
		{
			this.fieldName = _fieldName;
			this.fieldValue = _fieldValue.ToString();
		}

		public string fieldName;

		public string fieldValue;
	}
}
