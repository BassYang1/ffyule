using System;

namespace Lottery.Entity
{
	public class KeyValue
	{
		public string tKey
		{
			get
			{
				return this.m_Key;
			}
			set
			{
				this.m_Key = value;
			}
		}

		public decimal tValue
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		private string m_Key;

		private decimal m_Value;
	}
}
